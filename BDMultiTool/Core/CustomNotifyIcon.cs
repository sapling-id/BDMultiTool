using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;

namespace BDMultiTool.Core {
    class CustomNotifyIcon {
        private NotifyIcon notifyIcon;
        private static CustomNotifyIcon ownInstance;

        private CustomNotifyIcon() {
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Text = "BDMT v" + App.version;
            notifyIcon.Icon = BDMultiTool.Properties.Resources.trayIcon;
            notifyIcon.ContextMenu = createTrayIconContextMenu();
            notifyIcon.Visible = true;
        }

        public void notify(String title, String text) {
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.BalloonTipTitle = "BDMT " + title;
            notifyIcon.BalloonTipText = text;
            notifyIcon.ShowBalloonTip(5000);
        }

        public static CustomNotifyIcon getInstance() {
            if (ownInstance == null) {
                ownInstance = new CustomNotifyIcon();
            }

            return ownInstance;
        }

        private System.Windows.Forms.ContextMenu createTrayIconContextMenu() {
            System.Windows.Forms.ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();

            System.Windows.Forms.MenuItem infoLabel = new System.Windows.Forms.MenuItem();
            infoLabel.Text = "BDMT v" + App.version;

            System.Windows.Forms.MenuItem exitMenuItem = new System.Windows.Forms.MenuItem();
            exitMenuItem.Text = "Exit";
            exitMenuItem.Click += trayIconExit_Click;

            contextMenu.MenuItems.Add(infoLabel);
            contextMenu.MenuItems.Add(exitMenuItem);

            return contextMenu;
        }

        private void trayIconExit_Click(object sender, EventArgs e) {
            App.exit();
        }

        public void disposeNotifyIcon() {
            notifyIcon.Dispose();
        }

        public static void dispose() {
            getInstance().disposeNotifyIcon();
        }
    }
}
