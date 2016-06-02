using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BDMultiTool.Core.Settings {
    class BdmtSettings {
        private MovableUserControl ownParentWindow;
        private SettingsWindow settingsWindow;

        public BdmtSettings() {
            settingsWindow = new SettingsWindow();
            ownParentWindow = App.overlay.addWindowToGrid(settingsWindow, "Settings", true);
        }

        public void showSettingsMenu() {
            ownParentWindow.Dispatcher.Invoke((Action)(() => {
                ownParentWindow.Visibility = Visibility.Visible;
            }));
        }
    }
}
