using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDL2;
//using Microsoft.Xna.Framework.Graphics;

namespace XenoLib
{
    /// <summary>
    /// A wraper for a Texture2D dictionary
    /// </summary>
    public static class TextureBank
    {
        //protected
        static Dictionary<string, Texture2D> textures;
        
        //public
        /// <summary>
        /// TextureBank constuctor
        /// </summary>
        static TextureBank()
        {
            textures = new Dictionary<string, Texture2D>();
        }
        /// <summary>
        /// Checks for a Texture2D store by the given name and returns it if there
        /// </summary>
        /// <param name="name">name of desired texture</param>
        /// <returns>Texture2D</returns>
        public static Texture2D getTexture(string name)
        {
            Texture2D img;
            if (textures.ContainsKey(name))
            {
                textures.TryGetValue(name, out img);
                return img;
            }
            else
            {
                return default(Texture2D);
            }
        }
        /// <summary>
        /// Adds a texture to the bank if the provided name doesn't already exist and returns true else returns false
        /// </summary>
        /// <param name="name">Texture name</param>
        /// <param name="img">Texture to be added</param>
        /// <returns>True if succeeds or false if fails</returns>
        public static bool addTexture(string name, Texture2D img)
        {
            if (textures.ContainsKey(name))
            {
                return false;
            }
            else
            {
                textures.Add(name, img);
                return true;
            }
        }
        /// <summary>
        /// Checks if a provided key exists in TextureBank
        /// </summary>
        /// <param name="name">Asset name</param>
        /// <returns>Boolean</returns>
        public static bool containsKey(string name)
        {
            return textures.ContainsKey(name);
        }
        /// <summary>
        /// Returns an array of asset keys
        /// </summary>
        /// <returns>String arrary</returns>
        public static String[] getKeys()
        {
            return textures.Keys.ToArray<string>(); 
        }
    }
}
