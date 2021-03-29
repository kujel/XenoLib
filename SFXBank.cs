﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace XenoLib
{
    /// <summary>
    /// SFXBank class
    /// </summary>
    public static class SFXBank
    {
        //private
        static Dictionary<string, string> sfxs;

        //public
        /// <summary>
        /// SFXBank constructor
        /// </summary>
        static SFXBank()
        {
            sfxs = new Dictionary<string, string>();
        }

        /// <summary>
        /// Returns a SFX filePath or "" if none found
        /// </summary>
        /// <param name="name">Name/key of SFX</param>
        /// <returns>string</returns>
        public static string getSFX(string name)
        {
            string value;
            if(sfxs.TryGetValue(name, out value))
            {
                return value;
            }
            return "";
        }
        /// <summary>
        /// Adds a SFX filePath to SFXBank 
        /// </summary>
        /// <param name="name">Name/key</param>
        /// <param name="filePath">SFX filePath</param>
        public static void addSFX(string name, string filePath)
        {
            sfxs.Add(name, filePath);
        }
        /// <summary>
        /// Returns a list of all keys/names in SFXBank
        /// </summary>
        /// <returns>List of strings</returns>
        public static List<string> getKeys()
        {
            return sfxs.Keys.ToList();
        }
    }
}
