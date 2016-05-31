using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BDMultiTool.Utilities {
    class WindowObserver {
        private static IntPtr windowHandle;
        private static IntPtr eventHook;
        private static Action<int> callback;
        private static WinEventProc eventListener;


        internal enum SetWinEventHookFlags {
            WINEVENT_INCONTEXT = 4,
            WINEVENT_OUTOFCONTEXT = 0,
            WINEVENT_SKIPOWNPROCESS = 2,
            WINEVENT_SKIPOWNTHREAD = 1
        }

        public WindowObserver(IntPtr windowHandle, Action<int> callback) {
            WindowObserver.windowHandle = windowHandle;
            WindowObserver.callback = callback;
            eventListener = new WinEventProc(WindowEventCallback);
            int processId;
            int threadId = GetWindowThreadProcessId(windowHandle, out processId);

            eventHook = SetWinEventHook(1, 0x7fffffff, IntPtr.Zero, eventListener, processId, threadId, SetWinEventHookFlags.WINEVENT_OUTOFCONTEXT);
            Debug.WriteLine("hooked to window: " + eventHook);
        }

        private static void WindowEventCallback(IntPtr hWinEventHook, int iEvent, IntPtr hWnd, int idObject, int idChild, int dwEventThread, int dwmsEventTime) {
            if(hWnd == WindowObserver.windowHandle) {
                callback(iEvent);
                //Debug.WriteLine("Event on BDO window: " + iEvent.ToString("X4"));
            }

            //Debug.WriteLine("Event: " + iEvent.ToString("X4"));
        }


        [DllImport("USER32.DLL")]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        delegate void WinEventProc(IntPtr hWinEventHook, int iEvent, IntPtr hWnd, int idObject, int idChild, int dwEventThread, int dwmsEventTime);

        [DllImport("USER32.DLL", SetLastError = true)]
        static extern IntPtr SetWinEventHook(int eventMin, int eventMax, IntPtr hmodWinEventProc, WinEventProc lpfnWinEventProc, int idProcess, int idThread, SetWinEventHookFlags dwflags);

        [DllImport("USER32.DLL", SetLastError = true)]
        static extern int UnhookWinEvent(IntPtr hWinEventHook);
    }
}
