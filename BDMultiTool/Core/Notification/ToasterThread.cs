using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BDMultiTool.Core.Notification {
    class ToasterThread: MultiToolMarkUpThread {
        public static volatile bool keepWorking;
        public static volatile Toaster toaster;

        static ToasterThread() {
            keepWorking = true;
            toaster = new Toaster(5000);
            ThreadManager.registerThread(new Thread(new ToasterThread().work));
        }

        public ToasterThread() {

        }

        public void work() {
            while (keepWorking) {
                if (App.appCoreIsInitialized) {
                    toaster.update();
                    Thread.Sleep(100);
                } 

            }
        }
    }
}
