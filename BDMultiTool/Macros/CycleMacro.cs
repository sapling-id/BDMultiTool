using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMultiTool.Macros {
    public class CycleMacro {
        private LinkedList<System.Windows.Forms.Keys> key;
        public bool paused { get; set; }
        public long interval { get; set;}
        public long lifetime { get; set; }
        public String name { get; set; }
        private bool initialized;
        private Stopwatch stopWatchTotalTime;
        private Stopwatch stopWatch;

        public CycleMacro(params System.Windows.Forms.Keys[] keys) {
            key = new LinkedList<System.Windows.Forms.Keys>(keys);
            name = "N/A";
            stopWatch = new Stopwatch();
            stopWatchTotalTime = new Stopwatch();
            initialized = false;
            interval = 2000;
            lifetime = -1;
            paused = true;
        }

        public CycleMacro() {
            key = new LinkedList<System.Windows.Forms.Keys>();
            name = "N/A";
            stopWatch = new Stopwatch();
            stopWatchTotalTime = new Stopwatch();
            initialized = false;
            interval = 2000;
            lifetime = -1;
            paused = true;
        }

        public void addKey(System.Windows.Forms.Keys key) {
            this.key.AddLast(key);
        }

        public void start() {
            stopWatch.Start();
            if(!initialized) {
                initialized = true;
                if(lifetime > 0) {
                    stopWatchTotalTime.Start();
                }
            }
            paused = false;
        }

        public void pause() {
            stopWatch.Stop();
            stopWatchTotalTime.Stop();
            paused = true;
        }

        public void resume() {
            stopWatch.Start();
            stopWatchTotalTime.Start();
            paused = false;
        }

        public String getKeyString() {
            StringBuilder stringBuilder = new StringBuilder();
            bool firstItem = true;
            foreach(System.Windows.Forms.Keys currentKey in key) {
                if(!firstItem) {
                    stringBuilder.Append(", ");
                } else {
                    firstItem = false;
                }
                stringBuilder.Append(currentKey.ToString());
            }

            return stringBuilder.ToString();
        }

        public TimeSpan getRemainingCoolDown() {
            return TimeSpan.FromMilliseconds(interval - stopWatch.ElapsedMilliseconds);
        }

        public String getRemainingCoolDownFormatted() {
            return getFormattedTimeSpan(getRemainingCoolDown());
        }

        public TimeSpan getRemainingLifeTime() {
            if(lifetime > 0) {
                return TimeSpan.FromMilliseconds(lifetime - stopWatchTotalTime.ElapsedMilliseconds);
            } else {
                return new TimeSpan();
            }
        }

        public String getRemainingLifeTimeFormatted() {
            if(lifetime < 0) {
                return "";
            } else {
                return getFormattedTimeSpan(getRemainingLifeTime());
            }
            
        }

        private String getFormattedTimeSpan(TimeSpan timeSpan) {
            StringBuilder stringBuilder = new StringBuilder();
            if (timeSpan.TotalMinutes >= 60) {
                stringBuilder.Append(">");
            }
            if(timeSpan.TotalSeconds <= 1) {
                if (timeSpan.Milliseconds < 1000) {
                    stringBuilder.Append(" ");
                }
                if (timeSpan.Milliseconds < 100) {
                    stringBuilder.Append(" ");
                }
                if (timeSpan.Milliseconds < 10) {
                    stringBuilder.Append(" ");
                }
                stringBuilder.Append(timeSpan.Milliseconds);
                stringBuilder.Append(" ms");
            } else {
                if (timeSpan.Minutes < 10) {
                    stringBuilder.Append("0");
                }
                stringBuilder.Append(timeSpan.Minutes);
                stringBuilder.Append(":");
                if (timeSpan.Seconds < 10) {
                    stringBuilder.Append("0");
                }
                stringBuilder.Append(timeSpan.Seconds);
            }

            return stringBuilder.ToString();
        }

        public float getLifeTimePercentage() {
            if (lifetime > 0) {

                return ((float)stopWatchTotalTime.ElapsedMilliseconds/(float)lifetime) * 100f;
            } else {
                return 100;
            }
        }

        public String getLifeTimePercentageTwoDigitFormat() {
            return getLifeTimePercentage().ToString("0.00");
        }

        public float getCoolDownPercentage() {
            return ((float)stopWatch.ElapsedMilliseconds / (float)interval) * 100f;
        }

        public String getCoolDownPercentageTwoDigitFormat() {
            return getCoolDownPercentage().ToString("0.00");
        }

        public bool lifeTimeOver() {
            if(lifetime > 0) {
                return stopWatchTotalTime.ElapsedMilliseconds >= lifetime;
            } else {
                return false;
            }
        }

        public bool isReady() {
            if(stopWatch.ElapsedMilliseconds >= interval) {
                return true;
            } else {
                return false;
            }
        }

        public void reset() {
            stopWatch.Reset();
            paused = true;
        }

        public void resetAll() {
            stopWatchTotalTime.Reset();
            reset();
        }

        public System.Windows.Forms.Keys[] getKeys() {
            return key.ToArray<System.Windows.Forms.Keys>();
        }
    }
}
