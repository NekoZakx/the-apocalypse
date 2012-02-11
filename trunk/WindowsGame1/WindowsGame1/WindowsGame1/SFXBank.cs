using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace WindowsGame1
{
    class SFXBank
    {
        SoundEffect[] sounds;
        SoundEffectInstance[] sfi;

        public SFXBank(SoundEffect[] sounds)
        {
            this.sounds = sounds;
            sfi = new SoundEffectInstance[this.sounds.Length];
            for (int i = 0; i < sfi.Length; i++)
                sfi[i] = sounds[i].CreateInstance();
        }

        public void setLoop(int soundIndex, bool loop)
        {
            sfi[soundIndex].IsLooped = loop;
        }

        public void setLoop(bool loop)
        {
            for (int i = 0; i < sfi.Length; i++)
                sfi[i].IsLooped = loop;
        }

        public void setPitch(int soundIndex, float pitch)
        {
            if (pitch >= -1 && pitch <= 1)
                sfi[soundIndex].Pitch = pitch;
        }

        public void setPitch(float pitch)
        {
            if (pitch >= -1 && pitch <= 1)
            {
                for (int i = 0; i < sfi.Length; i++)
                    sfi[i].Pitch = pitch;
            }
        }

        public void setVolume(int soundIndex, float volume)
        {
            if (volume >= 0 && volume <= 1)
                sfi[soundIndex].Volume = volume;
        }

        public void setVolume(float volume)
        {
            if (volume >= 0 && volume <= 1)
            {
                for (int i = 0; i < sfi.Length; i++)
                    sfi[i].Volume = volume;
            }
        }

        public void play(int soundIndex)
        {
            sfi[soundIndex].Play();
        }

        public void stop(int soundIndex)
        {
            sfi[soundIndex].Stop();
        }

        public void stopAll()
        {
            for (int i = 0; i < sfi.Length; i++)
                sfi[i].Stop();
        }
    }
}
