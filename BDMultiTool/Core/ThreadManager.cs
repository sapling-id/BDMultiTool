using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BDMultiTool.Utilities {
    public class ThreadManager {
        private static ThreadManager ownInstance;
        private LinkedList<Thread> bdmtThreads;

        private ThreadManager() {
            bdmtThreads = new LinkedList<Thread>();
        }

        private static ThreadManager getInstance() {
            if(ownInstance == null) {
                ownInstance = new ThreadManager();
            }

            return ownInstance;
        }

        public static void registerThread(Thread threadToBeregisterd) {
            threadToBeregisterd.Start();
            getInstance().bdmtThreads.AddLast(threadToBeregisterd);
        }
    }
}
