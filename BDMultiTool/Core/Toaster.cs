using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BDMultiTool.Utilities.Core {
    class Toaster {
        private Toaster() {

        }

        public static void popToast(String title, String text) {
            CustomNotifyIcon.getInstance().notify(title, text);
        }
    }
}
