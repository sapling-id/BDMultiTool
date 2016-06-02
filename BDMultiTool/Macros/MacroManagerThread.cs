using BDMultiTool.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BDMultiTool.Macros {
    class MacroManagerThread: MultiToolMarkUpThread{
        public static volatile bool keepWorking;
        public static volatile MacroManager macroManager;

        static MacroManagerThread(){
            keepWorking = true;
            macroManager = new MacroManager();
            ThreadManager.registerThread(new Thread(new MacroManagerThread().work));
        }

        public MacroManagerThread() {
            
        }

        public void work() {
            while(keepWorking) {
                if(App.appCoreIsInitialized && !App.minimized) {
                    macroManager.update();
                    Thread.Sleep(60);
                } else {
                    Thread.Sleep(300);
                }

            }
        }

    }
}
