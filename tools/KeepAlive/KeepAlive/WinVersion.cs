using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace KeepAlive
{
    [StructLayout(LayoutKind.Sequential)]
    public class OSVERSIONINFO
    {
        public Int32 dwOSVersionInfoSize;
        public Int32 dwMajorVersion;
        public Int32 dwMinorVersion;
        public Int32 dwBuildNumber;
        public Int32 dwPlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public String szCSDVersion;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class OSVERSIONINFOEX : OSVERSIONINFO
    {
        public Int16 wServicePackMajor;
        public Int16 wServicePackMinor;
        public Int16 wSuiteMask;
        public Byte wProductType;
        public Byte wReserved;
    }

    public class WinVersion
    {
        [DllImport("kernel32.dll")]
        protected extern static Boolean GetVersionEx([In, Out] OSVERSIONINFO versionInfo);

        public const Int32 VER_PLATFORM_WIN32s = 0;
        public const Int32 VER_PLATFORM_WIN32_WINDOWS = 1;
        public const Int32 VER_PLATFORM_WIN32_NT = 2;

        public const Int32 VER_NT_WORKSTATION = 1;
        public const Int32 VER_NT_DOMAIN_CONTROLLER = 2;
        public const Int32 VER_NT_SERVER = 3;

        // Microsoft Small Business Server 
        public const Int32 VER_SUITE_SMALLBUSINESS = 1;
        // Win2k Adv Server or .Net Enterprise Server 
        public const Int32 VER_SUITE_ENTERPRISE = 2;
        // Terminal Services is installed.   
        public const Int32 VER_SUITE_TERMINAL = 16;
        // Win2k Datacenter 
        public const Int32 VER_SUITE_DATACENTER = 128;
        // Terminal server in remote admin mode 
        public const Int32 VER_SUITE_SINGLEUSERTS = 256;
        public const Int32 VER_SUITE_PERSONAL = 512;
        // Microsoft .Net webserver installed 
        public const Int32 VER_SUITE_BLADE = 1024;

        public WinVersion()
        {
        }

        public static bool IsServer()
        {
            OSVERSIONINFO versionInfo = new OSVERSIONINFOEX();
            Boolean bVersionInfoEx;

            versionInfo.dwOSVersionInfoSize = Marshal.SizeOf(versionInfo);
            bVersionInfoEx = GetVersionEx(versionInfo);

            return bVersionInfoEx && ((OSVERSIONINFOEX)versionInfo).wProductType == VER_NT_SERVER;
        }

        /* old version */
        //public static string GetVersionName()
        //{
        //    String name = String.Empty;
        //    Boolean success = true;
        //    Boolean bVersionInfoEx;

        //    OSVERSIONINFO versionInfo = new OSVERSIONINFOEX();
        //    versionInfo.dwOSVersionInfoSize = Marshal.SizeOf(versionInfo);
        //    bVersionInfoEx = GetVersionEx(versionInfo);

        //    if (!bVersionInfoEx)
        //    {
        //        versionInfo = new OSVERSIONINFO();
        //        versionInfo.dwOSVersionInfoSize = Marshal.SizeOf(versionInfo);
        //        success = GetVersionEx(versionInfo);

        //        if (!success)
        //        {
        //            return "未能成功获取操作系统版本信息";
        //        }
        //    }

        //    switch (versionInfo.dwPlatformId)
        //    {
        //        // Win NT家族 
        //        case VER_PLATFORM_WIN32_NT:
        //            if (versionInfo.dwMajorVersion == 5 &&
        //                versionInfo.dwMinorVersion == 2)
        //            {
        //                name = "Microsoft Windows Server 2003, ";
        //            }

        //            if (versionInfo.dwMajorVersion == 5 &&
        //                versionInfo.dwMinorVersion == 1)
        //            {
        //                name = "Microsoft Windows XP ";
        //            }

        //            if (versionInfo.dwMajorVersion == 5 &&
        //                versionInfo.dwMinorVersion == 0)
        //            {
        //                name = "Microsoft Windows 2000 ";
        //            }

        //            // 说明为Windows NT 4.0 SP6及更新的系统 
        //            if (bVersionInfoEx)
        //            {
        //                // 工作站类型 
        //                if (((OSVERSIONINFOEX)versionInfo).wProductType == VER_NT_WORKSTATION)
        //                {
        //                    if (versionInfo.dwMajorVersion == 4)
        //                    {
        //                        name += "Workstation 4.0 ";
        //                    }
        //                    else if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_PERSONAL) == VER_SUITE_PERSONAL)
        //                    {
        //                        name += "Home Edition ";
        //                    }
        //                    else
        //                    {
        //                        name += "Professional Edition ";
        //                    }
        //                }
        //                // 服务器类型 
        //                else if (((OSVERSIONINFOEX)versionInfo).wProductType == VER_NT_SERVER ||
        //                    ((OSVERSIONINFOEX)versionInfo).wProductType == VER_NT_DOMAIN_CONTROLLER)
        //                {
        //                    if (versionInfo.dwMajorVersion == 5 &&
        //                        versionInfo.dwMinorVersion == 2)
        //                    {
        //                        if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_DATACENTER) == VER_SUITE_DATACENTER)
        //                        {
        //                            name += "Datacenter Edition ";
        //                        }
        //                        else if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_ENTERPRISE) == VER_SUITE_ENTERPRISE)
        //                        {
        //                            name += "Enterprise Edition ";
        //                        }
        //                        else if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_BLADE) == VER_SUITE_BLADE)
        //                        {
        //                            name += "Web Edition ";
        //                        }
        //                        else
        //                        {
        //                            name += "Standard Edition ";
        //                        }
        //                    }
        //                    else if (versionInfo.dwMajorVersion == 5 &&
        //                        versionInfo.dwMinorVersion == 0)
        //                    {
        //                        if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_DATACENTER) == VER_SUITE_DATACENTER)
        //                        {
        //                            name += "Datacenter Server ";
        //                        }
        //                        else if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_ENTERPRISE) == VER_SUITE_ENTERPRISE)
        //                        {
        //                            name += "Advanced Server ";
        //                        }
        //                        else
        //                        {
        //                            name += "Server ";
        //                        }
        //                    }
        //                    // Windows NT 4.0 
        //                    else
        //                    {
        //                        if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_ENTERPRISE) == VER_SUITE_ENTERPRISE)
        //                        {
        //                            name += "Server 4.0, Enterprise Edition ";
        //                        }
        //                        else
        //                        {
        //                            name += "Server 4.0 ";
        //                        }
        //                    }
        //                }
        //            }
        //            break;

        //        // Win 9X家族 
        //        case VER_PLATFORM_WIN32_WINDOWS:
        //            if (versionInfo.dwMajorVersion == 4 && versionInfo.dwMinorVersion == 0)
        //            {
        //                name = "Microsoft Windows 95 ";
        //                if (versionInfo.szCSDVersion[1] == 'C' ||
        //                    versionInfo.szCSDVersion[1] == 'B')
        //                {
        //                    name += "OSR2 ";
        //                }
        //            }

        //            if (versionInfo.dwMajorVersion == 4 && versionInfo.dwMinorVersion == 10)
        //            {
        //                name = "Microsoft Windows 98 ";
        //                if (versionInfo.szCSDVersion[1] == 'A')
        //                {
        //                    name = "SE ";
        //                }
        //            }

        //            if (versionInfo.dwMajorVersion == 4 && versionInfo.dwMinorVersion == 90)
        //            {
        //                name = "Microsoft Windows Millennium Edition";
        //            }
        //            break;

        //        // 其他Win32系统 
        //        case VER_PLATFORM_WIN32s:
        //            name = "Microsoft Win32s";
        //            break;

        //        default:
        //            name = "Unknown System";
        //            break;
        //    }

        //    name += versionInfo.szCSDVersion;

        //    return name;
        //}
    }
}
