using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeepAlive
{
    class Startup
    {
        static bool _autoStartup = false;
        public static bool AutoStartup
        {
            get
            {
                return _autoStartup;
            }
            set
            {
                string starupPath = Application.ExecutablePath;

                using (RegistryKey loca = Registry.LocalMachine)
                {
                    using (RegistryKey run = loca.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run"))
                    {
                        var containsKey = run.GetValueNames().Contains("KeepAlive");

                        if (value) // to set true
                        {
                            if (containsKey)
                            {
                                if (run.GetValueKind("KeepAlive") == RegistryValueKind.String && run.GetValue("KeepAlive").ToString() == starupPath)
                                {
                                    goto setValue;
                                }
                            }
                            else
                            {
                                run.SetValue("KeepAlive", starupPath);
                            }
                        }
                        else
                        {
                            if (!containsKey)
                            {
                                goto setValue;
                            }
                            else
                            {
                                run.DeleteValue("KeepAlive");
                            }
                        }
                    }
                }

            setValue:
                _autoStartup = value;
            }
        }
    }
}
