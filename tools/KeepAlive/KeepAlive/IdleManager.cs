using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace KeepAlive
{
    class IdleManager
    {
        const uint ES_SYSTEM_REQUIRED = 0x00000001;
        const uint ES_DISPLAY_REQUIRED = 0x00000002;
        const uint ES_CONTINUOUS = 0x80000000;

        [DllImport(@"kernel32.dll")]
        extern public static uint SetThreadExecutionState(uint esFlags);
        [DllImport("kernel32")]
        static extern uint GetTickCount();

        static bool _keepAlive = false;
        public static bool KeepAlive
        {
            get
            {
                return _keepAlive;
            }
            set
            {
                if (value)
                {
                    SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED);
                }
                else
                {
                    SetThreadExecutionState(ES_CONTINUOUS);
                }
                _keepAlive = value;
            }
        }

        static bool _autoManageCpuUsage = false;
        static Thread cpuManagerThread = null;
        public static bool AutoManageCpuUsage
        {
            get
            {
                return _autoManageCpuUsage;
            }
            set
            {
                if (value)
                {
                    if (cpuManagerThread == null)
                    {
                        cpuManagerThread = new Thread(CpuManageEntry);
                        cpuManagerThread.Start();
                    }
                }
                _autoManageCpuUsage = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dwTime;
        }
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        static uint GetLastInputTime()
        {
            uint idleTime = 0;
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            uint envTicks = GetTickCount();

            if (GetLastInputInfo(ref lastInputInfo))
            {
                uint lastInputTick = lastInputInfo.dwTime;

                idleTime = envTicks - lastInputTick;
            }

            return idleTime;
        }

        static bool cpuManiaSwitch = false;
        static uint triggerTimeSpan = 30 * 60 * 1000;
        static uint checkTimeSpan = 1 * 60 * 1000;
        static Thread[] maniaThreads = null;
        static object[] maniaLocks = null;
        static private PerformanceCounter pcCpuLoad;
        static void CpuManageEntry()
        {
            if (maniaThreads == null)
            {
                var thread = new Thread(CpuManiaEntry);
                int cpuCount = Environment.ProcessorCount;
                maniaThreads = new Thread[cpuCount];
                maniaLocks = new object[cpuCount];
                for (int i = 0; i < cpuCount; i++)
                {
                    maniaThreads[i] = new Thread(CpuManiaEntry);
                    maniaLocks[i] = new object();
                    Monitor.Enter(maniaLocks[i]);
                    maniaThreads[i].Start(maniaLocks[i]);
                }

                pcCpuLoad = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                pcCpuLoad.MachineName = ".";
            }

            while (true)
            {
                var t = GetLastInputTime();
                if (cpuManiaSwitch)
                {
                    Thread.Sleep(1 * 250);
                    if (t < triggerTimeSpan)
                    {
                        DateTime d1 = DateTime.Now;
                        foreach (var o in maniaLocks)
                            Monitor.Enter(o);
                        TimeSpan ts = DateTime.Now - d1;
                        cpuManiaSwitch = false;
                    }
                }
                else
                {
                    Thread.Sleep((int)checkTimeSpan);
                    if (_autoManageCpuUsage && t > triggerTimeSpan)
                    {
                        DateTime d1 = DateTime.Now;
                        foreach (var o in maniaLocks)
                            Monitor.Exit(o);
                        TimeSpan ts = DateTime.Now - d1;
                        cpuManiaSwitch = true;
                    }
                }
            }
        }
        static void CpuManiaEntry(object lockObj)
        {
            var r = new Random();
            int processTime = 100;
            while (true)
            {
                lock (lockObj)
                {
                    var cpu = pcCpuLoad.NextValue();
                    if (cpu > 0.5)
                    {
                        if (processTime > 50)
                        {
                            processTime--;
                        }
                    }
                    else
                    {
                        if (processTime < 150)
                        {
                            processTime++;
                        }
                    }

                    var start = DateTime.Now;
                    while (true)
                    {
                        if (processTime <= 50) break;
                        if ((DateTime.Now - start).TotalMilliseconds > processTime)
                        {
                            break;
                        }
                    }
                    Thread.Sleep(1 * 100);
                }
            }
        }
    }
}
