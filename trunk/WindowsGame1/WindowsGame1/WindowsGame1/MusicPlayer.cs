using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    class MusicPlayer
    {
        Song[] songs;

        public MusicPlayer(Song[] songsArray)
        {
            songs = songsArray;
        }

        public void play(int songIndex)
        {
            MediaPlayer.Play(songs[songIndex]);
        }

        public void play(int songIndex, bool repeat)
        {
            MediaPlayer.IsRepeating = repeat;
            MediaPlayer.Play(songs[songIndex]);
        }

        public void pause()
        {
            MediaPlayer.Pause();
        }

        public void stop()
        {
            MediaPlayer.Stop();
        }

        public void shuffle()
        {
            int loops = songs.Length * 2;
            //Je suis rendu ici.
        }
    }
}
