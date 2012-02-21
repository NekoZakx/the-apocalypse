using Microsoft.Xna.Framework.Audio;

namespace The_Apocalypse
{
    class SFXBank
    {
        private SoundEffect[] sounds;
        private float volume = 1;
        private float pitch = 0;
        private float pan = 0;

        public SFXBank(SoundEffect[] sounds)
        {
            this.sounds = sounds;
        }

        public void setPitch(float pitch)
        {
            if (pitch >= -1 && pitch <= 1)
            {
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
                this.pan = pan;
            }
        }

        public float getPan()
        {
            return this.pan;
        }

        public void play(int soundIndex)
        {
            sounds[soundIndex].Play(this.volume, this.pitch, this.pan);
        }
    }
}