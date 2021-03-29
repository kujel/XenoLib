﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace XenoLib
{
    static class SFXEngine
    {
        //private
        static List<string> sounds;
        static bool c1;
        static bool c2;
        static bool c3;
        static bool c4;
        static IntPtr s1;
        static IntPtr s2;
        static IntPtr s3;
        static IntPtr s4;

        //public
        /// <summary>
        /// SFXEngine constructor
        /// </summary>
        static SFXEngine()
        {
            sounds = new List<string>();
            c1 = false;
            c2 = false;
            c3 = false;
            c4 = false;
            s1 = default(IntPtr);
            s2 = default(IntPtr);
            s3 = default(IntPtr);
            s4 = default(IntPtr);
        }
        /// <summary>
        /// Add a sound to be played
        /// </summary>
        /// <param name="str">Sound name/filePath</param>
        public static void addSound(string str)
        {
            if (!c1)
            {
                s1 = SDL_mixer.Mix_LoadWAV(str);
            }
            else if (!c2)
            {
                s2 = SDL_mixer.Mix_LoadWAV(str);
            }
            else if (!c3)
            {
                s3 = SDL_mixer.Mix_LoadWAV(str);
            }
            else if (!c4)
            {
                s4 = SDL_mixer.Mix_LoadWAV(str);
            }
            else
            {
                sounds.Add(str);
            }
        }
        /// <summary>
        /// Update SoundEngine internal states
        /// </summary>
        public static void update()
        {
            if (sounds.Count > 0)
            {
                if (!c1)
                {
                    if (sounds.Count > 0)
                    {
                        s1 = SDL_mixer.Mix_LoadWAV(sounds[0]);
                        SDL_mixer.Mix_PlayChannel(1, s1, 0);
                        sounds.RemoveAt(0);
                    }
                }
                if (!c2)
                {
                    if (sounds.Count > 0)
                    {
                        s2 = SDL_mixer.Mix_LoadWAV(sounds[0]);
                        SDL_mixer.Mix_PlayChannel(2, s2, 0);
                        sounds.RemoveAt(0);
                    }
                }
                if (!c3)
                {
                    if (sounds.Count > 0)
                    {
                        s3 = SDL_mixer.Mix_LoadWAV(sounds[0]);
                        SDL_mixer.Mix_PlayChannel(3, s3, 0);
                        sounds.RemoveAt(0);
                    }
                }
                if (!c4)
                {
                    if (sounds.Count > 0)
                    {
                        s4 = SDL_mixer.Mix_LoadWAV(sounds[0]);
                        SDL_mixer.Mix_PlayChannel(4, s4, 0);
                        sounds.RemoveAt(0);
                    }
                }
                if (c1)
                {
                    if (SDL_mixer.Mix_Playing(1) != 1)
                    {
                        SDL_mixer.Mix_FreeChunk(s1);
                        c1 = false;
                    }
                }
                if (c2)
                {
                    if (SDL_mixer.Mix_Playing(2) != 1)
                    {
                        SDL_mixer.Mix_FreeChunk(s2);
                        c2 = false;
                    }
                }
                if (c3)
                {
                    if (SDL_mixer.Mix_Playing(3) != 1)
                    {
                        SDL_mixer.Mix_FreeChunk(s3);
                        c3 = false;
                    }
                }
                if (c4)
                {
                    if (SDL_mixer.Mix_Playing(4) != 1)
                    {
                        SDL_mixer.Mix_FreeChunk(s4);
                        c4 = false;
                    }
                }
            }
        }
    }
}

