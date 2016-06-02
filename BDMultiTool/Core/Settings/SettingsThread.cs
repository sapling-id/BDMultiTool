using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BDMultiTool.Core.Settings {
    class SettingsThread : MultiToolMarkUpThread {
        public static volatile bool keepWorking;
        public static volatile BdmtSettings settings;

        static SettingsThread() {
            keepWorking = true;
            settings = new BdmtSettings();
            ThreadManager.registerThread(new Thread(new SettingsThread().work));
        }

        public SettingsThread() {

        }

        public void work() {
            //for potential usage
        }
    }
}
