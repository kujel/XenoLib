using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading;
using WMPLib;
using SDL2;
//using Microsoft.Xna.Framework.Audio;

namespace XenoLib
{
    /// <summary>
    /// SFXPlayer class
    /// </summary>
    public static class SFXPlayer
    {
        //private
        static List<Thread> threads;
        //public
        /// <summary>
        /// SFXPlayer constructor
        /// </summary>
        static SFXPlayer()
        {
            threads = new List<Thread>();
        }
        /// <summary>
        /// updates SoundPlayer's internal state
        /// </summary>
        public static void update()
        {
            for(int i = 0; i < threads.Count; i++)
            {
                if(threads[i].IsAlive == false)
                {
                    threads.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// Play a sound provided a file path
        /// </summary>
        /// <param name="path">Sound filePath</param>
        public static void play(string path)
        {
            if (path != "" && path != " " && path != null)
            {
                WindowsMediaPlayer player = new WindowsMediaPlayer();
                player.URL = path;
                Thread thread = new Thread(delegate () { player.controls.play(); });
                thread.Start();
                threads.Add(thread);
            }
        }
    }
}
