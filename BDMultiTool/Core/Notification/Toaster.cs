using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BDMultiTool.Core.Notification {
    class Toaster {
        private const int BORDER_OFFSET = 10;
        private long showDuration;
        private bool notified;
        private Stopwatch stopwatch;
        private NotificationWindow notificationWindow;

        public Toaster(long showDuration) {
            notified = false;
            stopwatch = new Stopwatch();
            notificationWindow = new NotificationWindow();
            notificationWindow.Left = SystemParameters.WorkArea.Right - notificationWindow.Width - BORDER_OFFSET;
            notificationWindow.Top = SystemParameters.WorkArea.Bottom - notificationWindow.Height - BORDER_OFFSET;
            notificationWindow.Height = 0;
            notificationWindow.Show();
            this.showDuration = showDuration;
        }

        public void update() {
            if(notified) {
                if(stopwatch.ElapsedMilliseconds >= showDuration) {
                    notified = false;
                    stopwatch.Reset();
                    notificationWindow.Dispatcher.Invoke((Action)(() => {
                        ((Storyboard)notificationWindow.FindResource("SlideOut")).Begin(notificationWindow);
                    }));

                }
            }
        }

        public void popToast(String title, String text) {
            if(notified) {
                notificationWindow.Dispatcher.Invoke((Action)(() => {
                    ((Storyboard)notificationWindow.FindResource("SlideOut")).Begin(notificationWindow);
                }));
            }
            notificationWindow.Dispatcher.Invoke((Action)(() => {
                notificationWindow.notificationTitle.Content = title;
                notificationWindow.contentTextBox.Text = text;
                ((Storyboard)notificationWindow.FindResource("SlideIn")).Begin(notificationWindow);
            }));

            SoundNotification.playNotificationSound();

            notified = true;
            stopwatch.Restart();
        }
    }
}
