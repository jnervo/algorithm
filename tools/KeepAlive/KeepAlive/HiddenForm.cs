using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace KeepAlive
{
    public partial class HiddenForm : Form
    {
        public NotifyIcon notifyIcon;

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(false);
        }

        public HiddenForm()
        {
            var menuList = new List<MenuItem>();
            var menuItemKeepAlive = new MenuItem("Keep this machine alive", KeepAliveEventHandler);
            var menuItemManageCPU = new MenuItem("Manage Idle CPU", ManageCpuEventHandler);
            var menuItemSplit1 = new MenuItem("-");
            var menuItemStartup = new MenuItem("Start Keep-Alive automatically when Windows start", StartupEventHandler);
            var menuItemExit = new MenuItem("Exit Keep-Alive", ExitEventHandler);
            menuList.Add(menuItemKeepAlive);
            menuList.Add(menuItemManageCPU);
            menuList.Add(menuItemSplit1);
            menuList.Add(menuItemStartup);
            menuList.Add(menuItemExit);

            notifyIcon = new NotifyIcon();

            notifyIcon.Text = "Keep-Alive";
            notifyIcon.ContextMenu = new ContextMenu(menuList.ToArray());
            notifyIcon.Visible = true;
            notifyIcon.Icon = Properties.Resources.logo;

            Timer timer = new Timer();
            timer.Tick += NotifyIconClick;

            notifyIcon.Click += (s, e) =>
            {
                if (object.ReferenceEquals(s, notifyIcon)
                    && e is MouseEventArgs
                    && (e as MouseEventArgs).Button == System.Windows.Forms.MouseButtons.Left)
                {
                    timer.Stop();
                    timer.Interval = (int)GetDoubleClickTime();
                    timer.Start();
                }
            };
            notifyIcon.DoubleClick += (s, e) =>
            {
                if (object.ReferenceEquals(s, notifyIcon)
                    && e is MouseEventArgs
                    && (e as MouseEventArgs).Button == System.Windows.Forms.MouseButtons.Left)
                {
                    timer.Stop();
                    Exit();
                }
            };

            // keep alive
            SetKeepAlive(menuItemKeepAlive, true);

            // auto-startup
            SetStartup(menuItemStartup, true);

            // CPU management
            if (WinVersion.IsServer())
            {
                SetManageCpu(menuItemManageCPU, true);
            }
        }

        void NotifyIconClick(object sender, EventArgs e)
        {
            Type t = typeof(NotifyIcon);
            MethodInfo mi = t.GetMethod("ShowContextMenu", BindingFlags.NonPublic | BindingFlags.Instance);
            mi.Invoke(notifyIcon, null);

            (sender as Timer).Stop();
        }

        public void KeepAliveEventHandler(object o, EventArgs e)
        {
            var current = (o as MenuItem).Checked;
            SetKeepAlive(o as MenuItem, !current);
        }
        void SetKeepAlive(MenuItem i, bool alive)
        {
            IdleManager.KeepAlive = alive;
            i.Checked = alive;
        }

        public void ExitEventHandler(object o, EventArgs e)
        {
            Exit();
        }
        void Exit()
        {
            notifyIcon.Visible = false;
            Environment.Exit(0);
        }

        public void StartupEventHandler(object o, EventArgs e)
        {
            var current = (o as MenuItem).Checked;
            SetStartup(o as MenuItem, !current);
        }
        void SetStartup(MenuItem i, bool value)
        {
            try
            {
                Startup.AutoStartup = value;
                i.Checked = value;
            }
            catch
            {
                i.Enabled = false;
            }
        }

        public void ManageCpuEventHandler(object o, EventArgs e)
        {
            var current = (o as MenuItem).Checked;
            SetManageCpu(o as MenuItem, !current);
        }
        void SetManageCpu(MenuItem i, bool value)
        {
            IdleManager.AutoManageCpuUsage = value;
            i.Checked = value;
        }

        #region import
        [DllImport(@"user32.dll")]
        extern public static uint GetDoubleClickTime();

        #endregion
    }
}
