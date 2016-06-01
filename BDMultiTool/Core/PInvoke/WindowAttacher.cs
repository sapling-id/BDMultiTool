using BDMultiTool.Utilities.Core;
using InputManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BDMultiTool.Utilities {
    public class WindowAttacher {
    private IntPtr windowHandle;
        private WindowObserver windowEventHook;
        private Window overlayWindow;
        private const uint WM_KEYDOWN = 0x100;
        private const uint WM_KEYUP = 0x101;
        private const uint WM_SETTEXT = 0x000c;

        public WindowAttacher(IntPtr windowHandle, Window overlayWindow) {
            if(windowHandle.Equals(IntPtr.Zero)) {
                Debug.WriteLine("could not pinvoke!");
                System.Windows.MessageBox.Show("Make sure BDO isn't initially minimized and this application is running as admin.", "Could not attach to BDO", MessageBoxButton.OK, MessageBoxImage.Error);
                App.exit();
            } else {
                this.windowHandle = windowHandle;
                this.overlayWindow = overlayWindow;
                windowEventHook = new WindowObserver(windowHandle, observedWindowEvent);
                SetForegroundWindow(windowHandle);

                updateOverlay();
                overlayWindow.Topmost = true;

                Toaster.popToast("Info", "Welcome to BDMT v" + App.version);
            }

        }

        private void observedWindowEvent(int windowEvent) {
            switch (windowEvent) {
                case WindowEventTypes.EVENT_OBJECT_FOCUS: {
                        updateOverlay();
                        overlayWindow.Show();
                        App.minimized = false;
                    }
                    break;
                case WindowEventTypes.EVENT_OBJECT_HIDE: {
                        App.minimized = true;
                        overlayWindow.Hide();

                        Toaster.popToast("Info", "BDMT has been minimized!");
                    }
                    break;
                case WindowEventTypes.EVENT_OBJECT_LOCATIONCHANGE: {
                        updateOverlay();
                    }
                    break;
                case WindowEventTypes.EVENT_OBJECT_SHOW: {

                    }
                    break;
                case WindowEventTypes.EVENT_SYSTEM_FOREGROUND: {

                    }
                    break;
                case 0: {
                        overlayWindow.Hide();
                    }
                    break;
                default:break;
            }
        }


        public void sendKeypress(System.Windows.Forms.Keys keyToSend) {
            SetForegroundWindow(windowHandle);
            Keyboard.KeyPress(keyToSend);
            Thread.Sleep(50);
        }

        public void sendKeyDown(System.Windows.Forms.Keys keyToSend) {
            SetForegroundWindow(windowHandle);
            Keyboard.KeyDown(keyToSend);
            Thread.Sleep(50);
        }

        public void sendKeyUp(System.Windows.Forms.Keys keyToSend) {
            SetForegroundWindow(windowHandle);
            Keyboard.KeyUp(keyToSend);
            Thread.Sleep(50);
        }

        private void updateOverlay() {
            Size tempSize = getWindowSize();
            Point tempLocation = getWindowLocation();
            overlayWindow.Width = tempSize.Width-2;
            overlayWindow.Height = tempSize.Height;

            overlayWindow.Left = tempLocation.X+1;
            overlayWindow.Top = tempLocation.Y-1;
        }

        public static IntPtr getHandleByWindowTitleBeginningWith(String title) {
            foreach (Process currentProcess in Process.GetProcesses()) {
                if(currentProcess.MainWindowTitle.StartsWith(title)) {
                    Debug.WriteLine("currentProcessWindow: " + currentProcess.MainWindowTitle);
                    return currentProcess.MainWindowHandle;
                }
            }

            return IntPtr.Zero;
        }

        private Size getWindowSize() {
            RECT rectStructure;
            Size windowSize = new Size();
            GetWindowRect(windowHandle, out rectStructure);

            windowSize.Width = rectStructure.Right - rectStructure.Left;
            windowSize.Height = rectStructure.Bottom - rectStructure.Top;

            return windowSize;
        }

        private Point getWindowLocation() {
            RECT rectStructure;
            Point windowLocation = new Point();
            GetWindowRect(windowHandle, out rectStructure);

            windowLocation.X = rectStructure.Left;
            windowLocation.Y = rectStructure.Top;

            return windowLocation;
        }

        [DllImport("user32.dll")]
        private static extern bool SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

    }
}
