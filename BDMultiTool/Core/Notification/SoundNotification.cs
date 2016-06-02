using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMultiTool.Core.Notification {
    class SoundNotification {
        private static NVorbis.NAudioSupport.VorbisWaveReader vorbisReader;
        private static NAudio.Wave.WaveOut waveOut;

        static SoundNotification() {
            vorbisReader = new NVorbis.NAudioSupport.VorbisWaveReader(BDMTConstants.WORKSPACE_PATH + BDMTConstants.NOTIFICATION_SOUND_FILE);
        }

        public static void playNotificationSound() {
            waveOut = new NAudio.Wave.WaveOut();
            waveOut.PlaybackStopped += onPlaybackStopped;
            waveOut.Init(vorbisReader);
            waveOut.Volume = 0.65f;
            waveOut.Play();
        }

        private static void onPlaybackStopped(object sender, EventArgs e) {
            waveOut.PlaybackStopped -= onPlaybackStopped;
            waveOut = null;
        }
    }
}
