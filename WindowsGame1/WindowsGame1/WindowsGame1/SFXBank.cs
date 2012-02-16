using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace WindowsGame1
{
    class SFXBank
    {
        private SoundEffect[] sounds;       
        //private SoundEffectInstance[] sfi;
        private float volume = 1;
        private float pitch = 0;
        private float pan = 0;

        public SFXBank(SoundEffect[] sounds)
        {
            this.sounds = sounds;
            /*sfi = new SoundEffectInstance[this.sounds.Length];
            for (int i = 0; i < sfi.Length; i++)
                sfi[i] = sounds[i].CreateInstance();*/
        }

        public void setPitch(float pitch)
        {
            if (pitch >= -1 && pitch <= 1)
            {
                /*for (int i = 0; i < sfi.Length; i++)
                    sfi[i].Pitch = pitch;*/
                this.pitch = pitch;
            }
        }

        public float getPitch()
        {
            return this.pitch;
        }

        public void setVolume(float volume)
        {
            if (volume >= 0 && volume <= 1)
            {
                /*for (int i = 0; i < sfi.Length; i++)
                    sfi[i].Volume = volume;*/
                this.volume = volume;
            }
        }

        public float getVolume()
        {
            return this.volume;
        }

        public void setPan(float pan)
        {
            if (pan >= -1 && pan <= 1)
            {
                /*for (int i = 0; i < sfi.Length; i++)
                    sfi[i].pan = pan;*/
                this.pan = pan;
            }
        }

        public float getPan()
        {
            return this.pan;
        }

        /*public SoundState getState(int soundIndex)
        {
            return sfi[soundIndex].State;
        }*/

        public void play(int soundIndex)
        {
            //sfi[soundIndex].Play();
            sounds[soundIndex].Play(this.volume, this.pitch, this.pan);
        }

        /*public void stop(int soundIndex)
        {
            sfi[soundIndex].Stop();
        }*/

        /*public void stopAll()
        {
            for (int i = 0; i < sfi.Length; i++)
                sfi[i].Stop();
        }*/
    }
}
