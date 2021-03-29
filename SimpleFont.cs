using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace XenoLib
{
    //**************************************************************************
    //Note: only renders in colour of source texture, change source texture for
    //alternate colours
    //**************************************************************************
    /// <summary>
    /// Simple font drawing class, font chars are 32 x 32 pixel white chars and ordered as fallows
    /// a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z,
    /// A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
    /// 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, !, @, #, $, %, ^, &, *, (, ), -, +. _, ?, :, ;,
    /// ", |, \, /, WHITESPACE, {, }, [, ], ., GREATER THAN LEFT, GREATER THAN RIGHT, Comma, =, ~, '
    /// </summary>
    public static class SimpleFont
    {
        //private
        static Texture2D source;
        static string sourceName;
        static Rectangle srcRect;
        static Rectangle destRect;
        static Point2D srcPos;
        static int charSize;
        static int charPos;
        static SDL.SDL_Color oldColour;

        //public
        /// <summary>
        /// SimpleFont constructor
        /// </summary>
        static SimpleFont()
        {
            source = TextureBank.getTexture("white");
            sourceName = "white";
            srcRect = new Rectangle(0, 0, 0, 0);
            destRect = new Rectangle(0, 0, 0, 0);
            charSize = 0;
            charPos = 0;
        }
        /// <summary>
        /// Loads a specified font source from TextureBank
        /// </summary>
        /// <param name="name">Font name</param>
        public static void loadFont(string name)
        {
            source = TextureBank.getTexture(name);
            sourceName = name;
        }
        /// <summary>
        /// Draws a string at a position provided a font source as a Texture2D (depricated)
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="str">String to draw</param>
        /// <param name="x">X position to draw string at</param>
        /// <param name="y">Y position to draw string at</param>
        /// <param name="colour">Colour to render (depricated)</param>
        /// <param name="scaler">Scales size of string to render with 1 being 32x32 pixel chars</param>
        public static void DrawString(IntPtr renderer, string str, int x, int y, SDL.SDL_Color colour = default(SDL.SDL_Color), float scaler = 1.0f)
        {
            charSize = 0;
            charPos = x;
            for (int i = 0; i < str.Length; i++)
            {
                srcPos = GetCharPos(str[i]);
                srcRect.X = srcPos.X;
                srcRect.Y = srcPos.Y;
                charPos += (int)(charSize * scaler);
                charSize = GetCharWidth(str[i]);
                destRect.Height = 32 * scaler;
                DrawChar(renderer, new Point2D(charPos, y), colour, scaler);
            }
        }
        /// <summary>
        /// Draws a string at a position provided a font source as a Texture2D (depricated)
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="str">String to draw</param>
        /// <param name="x">X position to draw string at</param>
        /// <param name="y">Y position to draw string at</param>
        /// <param name="colour">Colour to render (depricated)</param>
        /// <param name="scaler">Scales size of string to render with 1 being 32x32 pixel chars</param>
        public static void DrawString(IntPtr renderer, string str, float x, float y, SDL.SDL_Color colour = default(SDL.SDL_Color), float scaler = 1.0f)
        {
            charSize = 0;
            charPos = (int)x;
            for (int i = 0; i < str.Length; i++)
            {
                srcPos = GetCharPos(str[i]);
                srcRect.X = srcPos.X;
                srcRect.Y = srcPos.Y;
                charPos += (int)(charSize * scaler);
                charSize = GetCharWidth(str[i]);
                destRect.Height = 32 * scaler;
                DrawChar(renderer, new Point2D(charPos, y), colour, scaler);
            }
        }
        /// <summary>
        /// Draws a string at a position provided a font source as a Texture2D
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="str">String to draw</param>
        /// <param name="x">X position to draw string at</param>
        /// <param name="y">Y position to draw string at</param>
        /// <param name="scaler">Scales size of string to render with 1 being 32x32 pixel chars</param>
        public static void DrawString(IntPtr renderer, string str, int x, int y, float scaler = 1.0f)
        {
            charSize = 0;
            charPos = x;
            for (int i = 0; i < str.Length; i++)
            {
                srcPos = GetCharPos(str[i]);
                srcRect.X = srcPos.X;
                srcRect.Y = srcPos.Y;
                charPos += (int)(charSize * scaler);
                charSize = GetCharWidth(str[i]);
                destRect.Height = 32 * scaler;
                DrawChar(renderer, new Point2D(charPos, y), scaler);
            }
        }
        /// <summary>
        /// Draws a string at a position provided a font source as a Texture2D
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="str">String to draw</param>
        /// <param name="x">X position to draw string at</param>
        /// <param name="y">Y position to draw string at</param>
        /// <param name="scaler">Scales size of string to render with 1 being 32x32 pixel chars</param>
        public static void DrawString(IntPtr renderer, string str, float x, float y, float scaler = 1.0f)
        {
            charSize = 0;
            charPos = (int)x;
            for (int i = 0; i < str.Length; i++)
            {
                srcPos = GetCharPos(str[i]);
                srcRect.X = srcPos.X;
                srcRect.Y = srcPos.Y;
                charPos += (int)(charSize * scaler);
                charSize = GetCharWidth(str[i]);
                destRect.Height = 32 * scaler;
                DrawChar(renderer, new Point2D(charPos, y), scaler);
            }
        }
        /// <summary>
        /// Returns the source position of a provided char, used internally
        /// </summary>
        /// <param name="c">Char to find</param>
        /// <returns>Returns position as a Point2D</returns>
        public static Point2D GetCharPos(char c)
        {
            Point2D pos = new Point2D(0, 0);
            switch(c)
            { 
                case 'a':
                    pos.X = 6;
                    pos.Y = 0;
                    break;
                case 'b':
                    pos.X = 39;
                    pos.Y = 0;
                    break;
                case 'c':
                    pos.X = 71;
                    pos.Y = 0;
                    break;
                case 'd':
                    pos.X = 100;
                    pos.Y = 0;
                    break;
                case 'e':
                    pos.X = 133;
                    pos.Y = 0;
                    break;
                case 'f':
                    pos.X = 168;
                    pos.Y = 0;
                    break;
                case 'g':
                    pos.X = 198;
                    pos.Y = 0;
                    break;
                case 'h':
                    pos.X = 230;
                    pos.Y = 0;
                    break;
                case 'i':
                    pos.X = 268;
                    pos.Y = 0;
                    break;
                case 'j':
                    pos.X = 299;
                    pos.Y = 0;
                    break;
                case 'k':
                    pos.X = 326;
                    pos.Y = 0;
                    break;
                case 'l':
                    pos.X = 364;
                    pos.Y = 0;
                    break;
                case 'm':
                    pos.X = 385;
                    pos.Y = 0;
                    break;
                case 'n':
                    pos.X = 422;
                    pos.Y = 0;
                    break;
                case 'o':
                    pos.X = 453;
                    pos.Y = 0;
                    break;
                case 'p':
                    pos.X = 487;
                    pos.Y = -4;
                    break;
                case 'q':
                    pos.X = 517;
                    pos.Y = -4;
                    break;
                case 'r':
                    pos.X = 554;
                    pos.Y = 0;
                    break;
                case 's':
                    pos.X = 584;
                    pos.Y = 0;
                    break;
                case 't':
                    pos.X = 615;
                    pos.Y = 0;
                    break;
                case 'u':
                    pos.X = 646;
                    pos.Y = 0;
                    break;
                case 'v':
                    pos.X = 677;
                    pos.Y = 0;
                    break;
                case 'w':
                    pos.X = 705;
                    pos.Y = 0;
                    break;
                case 'x':
                    pos.X = 741;
                    pos.Y = 0;
                    break;
                case 'y':
                    pos.X = 775;
                    pos.Y = 0;
                    break;
                case 'z':
                    pos.X = 9;
                    pos.Y = 32;
                    break;
                case 'A':
                    pos.X = 35;
                    pos.Y = 32;
                    break;
                case 'B':
                    pos.X = 70;
                    pos.Y = 32;
                    break;
                case 'C':
                    pos.X = 100;
                    pos.Y = 32;
                    break;
                case 'D':
                    pos.X = 132;
                    pos.Y = 32;
                    break;
                case 'E':
                    pos.X = 167;
                    pos.Y = 32;
                    break;
                case 'F':
                    pos.X = 199;
                    pos.Y = 32;
                    break;
                case 'G':
                    pos.X = 227;
                    pos.Y = 32;
                    break;
                case 'H':
                    pos.X = 260;
                    pos.Y = 32;
                    break;
                case 'I':
                    pos.X = 299;
                    pos.Y = 32;
                    break;
                case 'J':
                    pos.X = 329;
                    pos.Y = 32;
                    break;
                case 'K':
                    pos.X = 357;
                    pos.Y = 32;
                    break;
                case 'L':
                    pos.X = 391;
                    pos.Y = 32;
                    break;
                case 'M':
                    pos.X = 416;
                    pos.Y = 32;
                    break;
                case 'N':
                    pos.X = 452;
                    pos.Y = 32;
                    break;
                case 'O':
                    pos.X = 483;
                    pos.Y = 32;
                    break;
                case 'P':
                    pos.X = 518;
                    pos.Y = 32;
                    break;
                case 'Q':
                    pos.X = 545;
                    pos.Y = 32;
                    break;
                case 'R':
                    pos.X = 581;
                    pos.Y = 32;
                    break;
                case 'S':
                    pos.X = 615;
                    pos.Y = 32;
                    break;
                case 'T':
                    pos.X = 645;
                    pos.Y = 32;
                    break;
                case 'U':
                    pos.X = 676;
                    pos.Y = 32;
                    break;
                case 'V':
                    pos.X = 707;
                    pos.Y = 32;
                    break;
                case 'W':
                    pos.X = 736;
                    pos.Y = 32;
                    break;
                case 'X':
                    pos.X = 772;
                    pos.Y = 32;
                    break;
                case 'Y':
                    pos.X = 4;
                    pos.Y = 64;
                    break;
                case 'Z':
                    pos.X = 38;
                    pos.Y = 64;
                    break;
                case '0':
                    pos.X = 69;
                    pos.Y = 64;
                    break;
                case '1':
                    pos.X = 102;
                    pos.Y = 64;
                    break;
                case '2':
                    pos.X = 134;
                    pos.Y = 64;
                    break;
                case '3':
                    pos.X = 166;
                    pos.Y = 64;
                    break;
                case '4':
                    pos.X = 197;
                    pos.Y = 64;
                    break;
                case '5':
                    pos.X = 230;
                    pos.Y = 64;
                    break;
                case '6':
                    pos.X = 261;
                    pos.Y = 64;
                    break;
                case '7':
                    pos.X = 294;
                    pos.Y = 64;
                    break;
                case '8':
                    pos.X = 325;
                    pos.Y = 64;
                    break;
                case '9':
                    pos.X = 358;
                    pos.Y = 64;
                    break;
                case '!':
                    pos.X = 395;
                    pos.Y = 64;
                    break;
                case '@':
                    pos.X = 416;
                    pos.Y = 64;
                    break;
                case '#':
                    pos.X = 454;
                    pos.Y = 64;
                    break;
                case '$':
                    pos.X = 486;
                    pos.Y = 64;
                    break;
                case '%':
                    pos.X = 514;
                    pos.Y = 64;
                    break;
                case '^':
                    pos.X = 549;
                    pos.Y = 64;
                    break;
                case '&':
                    pos.X = 578;
                    pos.Y = 64;
                    break;
                case '*':
                    pos.X = 615;
                    pos.Y = 64;
                    break;
                case '(':
                    pos.X = 648;
                    pos.Y = 64;
                    break;
                case ')':
                    pos.X = 684;
                    pos.Y = 64;
                    break;
                case '-':
                    pos.X = 711;
                    pos.Y = 64;
                    break;
                case '+':
                    pos.X = 741;
                    pos.Y = 64;
                    break;
                case '_':
                    pos.X = 769;
                    pos.Y = 96;
                    break;
                case '?':
                    pos.X = 9;
                    pos.Y = 96;
                    break;
                case ':':
                    pos.X = 43;
                    pos.Y =96;
                    break;
                case ';':
                    pos.X = 75;
                    pos.Y = 96;
                    break;
                case '"':
                    pos.X = 105;
                    pos.Y = 96;
                    break;
                case '|':
                    pos.X = 138;
                    pos.Y = 96;
                    break;
                case '\\':
                    pos.X = 166;
                    pos.Y = 96;
                    break;
                case '/':
                    pos.X = 198;
                    pos.Y = 96;
                    break;
                case ' ':
                    pos.X = 227;
                    pos.Y = 96;
                    break;
                case '{':
                    pos.X = 263;
                    pos.Y = 96;
                    break;
                case '}':
                    pos.X = 300;
                    pos.Y = 96;
                    break;
                case '[':
                    pos.X = 329;
                    pos.Y = 96;
                    break;
                case ']':
                    pos.X = 362;
                    pos.Y = 96;
                    break;
                case '.':
                    pos.X = 395;
                    pos.Y = 96;
                    break;
                case '<':
                    pos.X = 421;
                    pos.Y = 96;
                    break;
                case '>':
                    pos.X = 455;
                    pos.Y = 96;
                    break;
                case ',':
                    pos.X = 491;
                    pos.Y = 96;
                    break;
                case '=':
                    pos.X = 518;
                    pos.Y = 96;
                    break;
                case '~':
                    pos.X = 550;
                    pos.Y = 96;
                    break;
                case '\'':
                    pos.X = 588;
                    pos.Y = 96;
                    break;
            }
            return pos;
        }
        /// <summary>
        /// Returns the source width of a provided char, used internally
        /// </summary>
        /// <param name="c">Char to find</param>
        /// <returns>Returns width as an int</returns>
        public static int GetCharWidth(char c)
        {
            
            switch (c)
            {
                case 'a':
                    return 18;
                case 'b':
                    return 20;
                case 'c':
                    return 18;
                case 'd':
                    return 20;
                case 'e':
                    return 20;
                case 'f':
                    return 14;
                case 'g':
                    return 20;
                case 'h':
                    return 20;
                case 'i':
                    return 8;
                case 'j':
                    return 12;
                case 'k':
                    return 20;
                case 'l':
                    return 12;
                case 'm':
                    return 30;
                case 'n':
                    return 20;
                case 'o':
                    return 23;
                case 'p':
                    return 24;
                case 'q':
                    return 18;
                case 'r':
                    return 16;
                case 's':
                    return 18;
                case 't':
                    return 16;
                case 'u':
                    return 20;
                case 'v':
                    return 22;
                case 'w':
                    return 28;
                case 'x':
                    return 20;
                case 'y':
                    return 20;
                case 'z':
                    return 18;
                case 'A':
                    return 25;
                case 'B':
                    return 22;
                case 'C':
                    return 21;
                case 'D':
                    return 24;
                case 'E':
                    return 18;
                case 'F':
                    return 17;
                case 'G':
                    return 25;
                case 'H':
                    return 23;
                case 'I':
                    return 9;
                case 'J':
                    return 14;
                case 'K':
                    return 21;
                case 'L':
                    return 18;
                case 'M':
                    return 31;
                case 'N':
                    return 24;
                case 'O':
                    return 27;
                case 'P':
                    return 21;
                case 'Q':
                    return 28;
                case 'R':
                    return 21;
                case 'S':
                    return 19;
                case 'T':
                    return 22;
                case 'U':
                    return 24;
                case 'V':
                    return 26;
                case 'W':
                    return 32;
                case 'X':
                    return 24;
                case 'Y':
                    return 23;
                case 'Z':
                    return 20;
                case '0':
                    return 21;
                case '1':
                    return 20;
                case '2':
                    return 21;
                case '3':
                    return 21;
                case '4':
                    return 24;
                case '5':
                    return 21;
                case '6':
                    return 21;
                case '7':
                    return 19;
                case '8':
                    return 21;
                case '9':
                    return 21;
                case '!':
                    return 20;
                case '@':
                    return 31;
                case '#':
                    return 21;
                case '$':
                    return 21;
                case '%':
                    return 29;
                case '^':
                    return 22;
                case '&':
                    return 28;
                case '*':
                    return 17;
                case '(':
                    return 12;
                case ')':
                    return 12;
                case '-':
                    return 19;
                case '+':
                    return 21;
                case '_':
                    return 30;
                case '?':
                    return 18;
                case ':':
                    return 10;
                case ';':
                    return 12;
                case '"':
                    return 14;
                case '|':
                    return 10;
                case '\\':
                    return 20;
                case '/':
                    return 20;
                case ' ':
                    return 26;
                case '{':
                    return 15;
                case '}':
                    return 15;
                case '[':
                    return 13;
                case ']':
                    return 13;
                case '.':
                    return 10;
                case '<':
                    return 20;
                case '>':
                    return 20;
                case ',':
                    return 12;
                case '=':
                    return 20;
                case '~':
                    return 21;
                case '\'':
                    return 9;
            }
            return 0;
        }
        /// <summary>
        /// Draws a single char, used internally
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="pos">Position of char to render</param>
        /// <param name="colour">Colour to draw char</param>
        /// <param name="scaler">Scaling value</param>
        public static void DrawChar(IntPtr renderer, Point2D pos, SDL.SDL_Color colour, float scaler)
        {
            srcRect.Width = charSize;
            srcRect.Height = 32;
            destRect.X = pos.X;
            destRect.Y = pos.Y;
            destRect.Width = charSize * scaler;
            SDL.SDL_GetTextureColorMod(source.texture, out oldColour.r, out oldColour.g, out oldColour.b);
            SDL.SDL_SetTextureColorMod(renderer, colour.r, colour.g, colour.b);
            SimpleDraw.draw(renderer, source, srcRect, destRect);
            SDL.SDL_SetTextureColorMod(source.texture, oldColour.r, oldColour.g, oldColour.b);
            //SpriteBatch.Draw(fontSrc, pos, new Vector2(scaler, scaler), colour, new Vector2(0, 0), srcRect);
        }
        /// <summary>
        /// Draws a single char, used internally
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="pos">Position of char to render</param>
        /// <param name="scaler">Scaling value</param>
        public static void DrawChar(IntPtr renderer, Point2D pos, float scaler)
        {
            srcRect.Width = charSize;
            srcRect.Height = 32;
            destRect.X = pos.X;
            destRect.Y = pos.Y;
            destRect.Width = charSize * scaler;
            SimpleDraw.draw(renderer, source, srcRect, destRect);
        }
        /// <summary>
        /// Calculates the width of rendered text
        /// </summary>
        /// <param name="str">String reference</param>
        /// <param name="scaler">Scaler value</param>
        /// <returns>Integer</returns>
        public static int stringRenderWidth(string str, float scaler)
        {
            charSize = 0;
            charPos = 0;
            for (int i = 0; i < str.Length; i++)
            {
                //charPos += (int)(charSize * scaler);
                charSize = GetCharWidth(str[i]);
                charPos += (int)(charSize * scaler);
            }
            return charPos;
        }
        /// <summary>
        /// Loads coloured font sheets into the TextureBank, named by colour;
        /// black, white, green, yellow, pink, red, gray, orange, purple, brown
        /// </summary>
        /// <param name="path">Graphics folder path</param>
        /// <param name="renderer">Renderer reference</param>
        public static void loadFontColours(string path, IntPtr renderer)
        {
            string file = path;
            file += "my font white.png";
            TextureBank.addTexture("white", TextureLoader.load(file, renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font black.png";
            TextureBank.addTexture("black", TextureLoader.load(file, renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font green.png";
            TextureBank.addTexture("green", TextureLoader.load(file, renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font yellow.png";
            TextureBank.addTexture("yellow", TextureLoader.load(file, renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font pink.png";
            TextureBank.addTexture("pink", TextureLoader.load(file, renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font red.png";
            TextureBank.addTexture("red", TextureLoader.load(file, renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font gray.png";
            TextureBank.addTexture("gray", TextureLoader.load(file, renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font orange.png";
            TextureBank.addTexture("orange", TextureLoader.load(file, renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font purple.png";
            TextureBank.addTexture("purple", TextureLoader.load(file, renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font brown.png";
            TextureBank.addTexture("brown", TextureLoader.load(file, renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
        }
        /// <summary>
        /// Draws a coloured string at specified location
        /// *** LoadFontColours must be called before use and font graphics must
        /// use name of "my font 'colour'" ***
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="text">String to render</param>
        /// <param name="x">X position to render at</param>
        /// <param name="y">Y position to render at</param>
        /// <param name="colour">Colour of font to use</param>
        /// <param name="scaler">Scaling value</param>
        public static void drawColourString(IntPtr renderer, string text, float x, float y, string colour, float scaler = 1)
        {
            //set colour of text
            sourceName = colour;
            source = TextureBank.getTexture(colour);

            DrawString(renderer, text, x, y, scaler);

            //reset colour of text to white
            sourceName = "white";
            source = TextureBank.getTexture("white");
        }
    }

    /// <summary>
    /// DynamicFont class, adds colour shading of white sources and varied source values
    /// </summary>
    public static class DynamicFont
    {
        //private
        static List<Rectangle> boxes;
        static List<string> chars;
        static Texture2D source;
        static string sourceName;
        static Rectangle srcRect;
        static Rectangle destRect;
        static Point2D srcPos;
        static int charSize;
        static int charPos;
        //static SDL.SDL_Color oldColour;

        //public
        static DynamicFont()
        {
            boxes = new List<Rectangle>();
            chars = new List<string>();
            source = TextureBank.getTexture("white");
            sourceName = "white";
            srcRect = new Rectangle(0, 0, 0, 0);
            destRect = new Rectangle(0, 0, 0, 0);
            charSize = 0;
            charPos = 0;
        }
        /// <summary>
        /// DynamicFont from file contructor
        /// </summary>
        /// <param name="sr">StreamReader reference</param>
        public static void loadFontFile(System.IO.StreamReader sr)
        {
            boxes = new List<Rectangle>();
            chars = new List<string>();
            srcRect = new Rectangle(0, 0, 0, 0);
            destRect = new Rectangle(0, 0, 0, 0);
            charSize = 0;
            charPos = 0;

            sr.ReadLine();
            sourceName = sr.ReadLine();
            source = TextureBank.getTexture(sourceName);
            int tmp = Convert.ToInt32(sr.ReadLine());
            string str = "";
            for(int c = 0; c < tmp; c++)
            {
                str = sr.ReadLine();
                chars.Add(str);
            }
            tmp = Convert.ToInt32(sr.ReadLine());
            Rectangle rect = null;
            for (int b = 0; b < tmp; b++)
            {
                rect = new Rectangle(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()),
                    Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
                boxes.Add(rect);
            }
        }
        /// <summary>
        /// Saves DynamicFont data
        /// </summary>
        /// <param name="sw">StreamWriter reference</param>
        public static void saveData(System.IO.StreamWriter sw)
        {
            sw.WriteLine("======Dynamic Font Data======");
            sw.WriteLine(sourceName);
            sw.WriteLine(chars.Count);
            for(int c = 0; c < chars.Count; c++)
            {
                sw.WriteLine(chars[c]);
            }
            for (int b = 0; b < boxes.Count; b++)
            {
                sw.WriteLine(boxes[b].X);
                sw.WriteLine(boxes[b].Y);
                sw.WriteLine(boxes[b].Width);
                sw.WriteLine(boxes[b].Height);
            }
        }
        /// <summary>
        /// Sets font values to defualts for defualt font source
        /// </summary>
        public static void setDefualts()
        {
            Point2D pos = null;
            Rectangle rect = null;
            int width = 0;
            string str = "";
            pos = GetCharDefualtPos('a');
            width = GetCharDefualtWidth('a');
            str = "a";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('b');
            width = GetCharDefualtWidth('b');
            str = "b";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('c');
            width = GetCharDefualtWidth('c');
            str = "c";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('d');
            width = GetCharDefualtWidth('d');
            str = "d";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('e');
            width = GetCharDefualtWidth('e');
            str = "e";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('f');
            width = GetCharDefualtWidth('f');
            str = "f";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('g');
            width = GetCharDefualtWidth('g');
            str = "g";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('h');
            width = GetCharDefualtWidth('h');
            str = "h";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('i');
            width = GetCharDefualtWidth('i');
            str = "i";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('j');
            width = GetCharDefualtWidth('j');
            str = "j";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('k');
            width = GetCharDefualtWidth('k');
            str = "k";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('l');
            width = GetCharDefualtWidth('l');
            str = "l";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('m');
            width = GetCharDefualtWidth('m');
            str = "m";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('n');
            width = GetCharDefualtWidth('n');
            str = "n";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('o');
            width = GetCharDefualtWidth('o');
            str = "o";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('p');
            width = GetCharDefualtWidth('p');
            str = "p";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('q');
            width = GetCharDefualtWidth('q');
            str = "q";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('r');
            width = GetCharDefualtWidth('r');
            str = "r";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('s');
            width = GetCharDefualtWidth('s');
            str = "s";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('t');
            width = GetCharDefualtWidth('t');
            str = "t";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('u');
            width = GetCharDefualtWidth('u');
            str = "u";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('v');
            width = GetCharDefualtWidth('v');
            str = "v";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('w');
            width = GetCharDefualtWidth('w');
            str = "w";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('x');
            width = GetCharDefualtWidth('x');
            str = "x";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('y');
            width = GetCharDefualtWidth('y');
            str = "y";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('z');
            width = GetCharDefualtWidth('z');
            str = "z";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('A');
            width = GetCharDefualtWidth('A');
            str = "A";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('B');
            width = GetCharDefualtWidth('B');
            str = "B";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('C');
            width = GetCharDefualtWidth('C');
            str = "C";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('D');
            width = GetCharDefualtWidth('D');
            str = "D";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('E');
            width = GetCharDefualtWidth('E');
            str = "E";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('F');
            width = GetCharDefualtWidth('F');
            str = "F";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('G');
            width = GetCharDefualtWidth('G');
            str = "G";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('H');
            width = GetCharDefualtWidth('H');
            str = "H";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('I');
            width = GetCharDefualtWidth('I');
            str = "I";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('J');
            width = GetCharDefualtWidth('J');
            str = "J";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('K');
            width = GetCharDefualtWidth('K');
            str = "K";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('L');
            width = GetCharDefualtWidth('L');
            str = "L";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('M');
            width = GetCharDefualtWidth('M');
            str = "M";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('N');
            width = GetCharDefualtWidth('N');
            str = "N";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('O');
            width = GetCharDefualtWidth('O');
            str = "O";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('P');
            width = GetCharDefualtWidth('P');
            str = "P";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('Q');
            width = GetCharDefualtWidth('Q');
            str = "Q";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('R');
            width = GetCharDefualtWidth('R');
            str = "R";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('S');
            width = GetCharDefualtWidth('S');
            str = "S";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('T');
            width = GetCharDefualtWidth('T');
            str = "T";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('U');
            width = GetCharDefualtWidth('U');
            str = "U";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('V');
            width = GetCharDefualtWidth('V');
            str = "V";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('W');
            width = GetCharDefualtWidth('W');
            str = "W";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('X');
            width = GetCharDefualtWidth('X');
            str = "X";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('Y');
            width = GetCharDefualtWidth('Y');
            str = "Y";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('Z');
            width = GetCharDefualtWidth('Z');
            str = "Z";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('0');
            width = GetCharDefualtWidth('0');
            str = "0";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('1');
            width = GetCharDefualtWidth('1');
            str = "1";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('2');
            width = GetCharDefualtWidth('2');
            str = "2";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('3');
            width = GetCharDefualtWidth('3');
            str = "3";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('4');
            width = GetCharDefualtWidth('4');
            str = "4";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('5');
            width = GetCharDefualtWidth('5');
            str = "5";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('6');
            width = GetCharDefualtWidth('6');
            str = "6";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('7');
            width = GetCharDefualtWidth('7');
            str = "7";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('8');
            width = GetCharDefualtWidth('8');
            str = "8";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('9');
            width = GetCharDefualtWidth('9');
            str = "9";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('!');
            width = GetCharDefualtWidth('!');
            str = "!";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('@');
            width = GetCharDefualtWidth('@');
            str = "@";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('#');
            width = GetCharDefualtWidth('#');
            str = "#";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('$');
            width = GetCharDefualtWidth('$');
            str = "$";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('%');
            width = GetCharDefualtWidth('%');
            str = "%";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('^');
            width = GetCharDefualtWidth('^');
            str = "^";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('&');
            width = GetCharDefualtWidth('&');
            str = "&";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('*');
            width = GetCharDefualtWidth('*');
            str = "*";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('(');
            width = GetCharDefualtWidth('(');
            str = "(";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos(')');
            width = GetCharDefualtWidth(')');
            str = ")";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('+');
            width = GetCharDefualtWidth('+');
            str = "+";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('-');
            width = GetCharDefualtWidth('-');
            str = "-";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('_');
            width = GetCharDefualtWidth('_');
            str = "_";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('?');
            width = GetCharDefualtWidth('?');
            str = "?";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos(':');
            width = GetCharDefualtWidth(':');
            str = ":";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos(';');
            width = GetCharDefualtWidth(';');
            str = ";";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('"');
            width = GetCharDefualtWidth('"');
            str = "\"";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('|');
            width = GetCharDefualtWidth('|');
            str = "|";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('\\');
            width = GetCharDefualtWidth('\\');
            str = "\\";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('/');
            width = GetCharDefualtWidth('/');
            str = "/";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos(' ');
            width = GetCharDefualtWidth(' ');
            str = " ";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('[');
            width = GetCharDefualtWidth('[');
            str = "[";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos(']');
            width = GetCharDefualtWidth(']');
            str = "]";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('{');
            width = GetCharDefualtWidth('{');
            str = "{";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('}');
            width = GetCharDefualtWidth('}');
            str = "}";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('>');
            width = GetCharDefualtWidth('>');
            str = ">";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('<');
            width = GetCharDefualtWidth('<');
            str = "<";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos(',');
            width = GetCharDefualtWidth(',');
            str = ",";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('=');
            width = GetCharDefualtWidth('=');
            str = "=";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('`');
            width = GetCharDefualtWidth('`');
            str = "`";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
            pos = GetCharDefualtPos('\'');
            width = GetCharDefualtWidth('\'');
            str = "'";
            rect = new Rectangle(pos.X, pos.Y, width, 32);
            boxes.Add(rect);
            chars.Add(str);
        }
        /// <summary>
        /// Find a matching char if any and set it's source values
        /// </summary>
        /// <param name="c">Char to set</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position </param>
        /// <param name="w">int width</param>
        /// <param name="h">int height</param>
        public static void setChar(char c, int x, int y, int w, int h)
        {
            for(int i = 0; i < chars.Count; i++)
            {
                if(chars[i] == c.ToString())
                {
                    boxes[i].X = x;
                    boxes[i].Y = y;
                    boxes[i].Width = w;
                    boxes[i].Height = h;
                    break;
                }
            }
        }
        /// <summary>
        /// Returns the source defualt position of a provided char, used internally
        /// </summary>
        /// <param name="c">Char to find</param>
        /// <returns>Returns position as a Point2D</returns>
        public static Point2D GetCharDefualtPos(char c)
        {
            Point2D pos = new Point2D(0, 0);
            switch (c)
            {
                case 'a':
                    pos.X = 6;
                    pos.Y = 0;
                    break;
                case 'b':
                    pos.X = 39;
                    pos.Y = 0;
                    break;
                case 'c':
                    pos.X = 71;
                    pos.Y = 0;
                    break;
                case 'd':
                    pos.X = 100;
                    pos.Y = 0;
                    break;
                case 'e':
                    pos.X = 133;
                    pos.Y = 0;
                    break;
                case 'f':
                    pos.X = 168;
                    pos.Y = 0;
                    break;
                case 'g':
                    pos.X = 198;
                    pos.Y = 0;
                    break;
                case 'h':
                    pos.X = 230;
                    pos.Y = 0;
                    break;
                case 'i':
                    pos.X = 268;
                    pos.Y = 0;
                    break;
                case 'j':
                    pos.X = 299;
                    pos.Y = 0;
                    break;
                case 'k':
                    pos.X = 326;
                    pos.Y = 0;
                    break;
                case 'l':
                    pos.X = 364;
                    pos.Y = 0;
                    break;
                case 'm':
                    pos.X = 385;
                    pos.Y = 0;
                    break;
                case 'n':
                    pos.X = 422;
                    pos.Y = 0;
                    break;
                case 'o':
                    pos.X = 453;
                    pos.Y = 0;
                    break;
                case 'p':
                    pos.X = 487;
                    pos.Y = -4;
                    break;
                case 'q':
                    pos.X = 517;
                    pos.Y = -4;
                    break;
                case 'r':
                    pos.X = 554;
                    pos.Y = 0;
                    break;
                case 's':
                    pos.X = 584;
                    pos.Y = 0;
                    break;
                case 't':
                    pos.X = 615;
                    pos.Y = 0;
                    break;
                case 'u':
                    pos.X = 646;
                    pos.Y = 0;
                    break;
                case 'v':
                    pos.X = 677;
                    pos.Y = 0;
                    break;
                case 'w':
                    pos.X = 705;
                    pos.Y = 0;
                    break;
                case 'x':
                    pos.X = 741;
                    pos.Y = 0;
                    break;
                case 'y':
                    pos.X = 775;
                    pos.Y = 0;
                    break;
                case 'z':
                    pos.X = 9;
                    pos.Y = 32;
                    break;
                case 'A':
                    pos.X = 35;
                    pos.Y = 32;
                    break;
                case 'B':
                    pos.X = 70;
                    pos.Y = 32;
                    break;
                case 'C':
                    pos.X = 100;
                    pos.Y = 32;
                    break;
                case 'D':
                    pos.X = 132;
                    pos.Y = 32;
                    break;
                case 'E':
                    pos.X = 167;
                    pos.Y = 32;
                    break;
                case 'F':
                    pos.X = 199;
                    pos.Y = 32;
                    break;
                case 'G':
                    pos.X = 227;
                    pos.Y = 32;
                    break;
                case 'H':
                    pos.X = 260;
                    pos.Y = 32;
                    break;
                case 'I':
                    pos.X = 299;
                    pos.Y = 32;
                    break;
                case 'J':
                    pos.X = 329;
                    pos.Y = 32;
                    break;
                case 'K':
                    pos.X = 357;
                    pos.Y = 32;
                    break;
                case 'L':
                    pos.X = 391;
                    pos.Y = 32;
                    break;
                case 'M':
                    pos.X = 416;
                    pos.Y = 32;
                    break;
                case 'N':
                    pos.X = 452;
                    pos.Y = 32;
                    break;
                case 'O':
                    pos.X = 483;
                    pos.Y = 32;
                    break;
                case 'P':
                    pos.X = 518;
                    pos.Y = 32;
                    break;
                case 'Q':
                    pos.X = 545;
                    pos.Y = 32;
                    break;
                case 'R':
                    pos.X = 581;
                    pos.Y = 32;
                    break;
                case 'S':
                    pos.X = 615;
                    pos.Y = 32;
                    break;
                case 'T':
                    pos.X = 645;
                    pos.Y = 32;
                    break;
                case 'U':
                    pos.X = 676;
                    pos.Y = 32;
                    break;
                case 'V':
                    pos.X = 707;
                    pos.Y = 32;
                    break;
                case 'W':
                    pos.X = 736;
                    pos.Y = 32;
                    break;
                case 'X':
                    pos.X = 772;
                    pos.Y = 32;
                    break;
                case 'Y':
                    pos.X = 4;
                    pos.Y = 64;
                    break;
                case 'Z':
                    pos.X = 38;
                    pos.Y = 64;
                    break;
                case '0':
                    pos.X = 67;
                    pos.Y = 64;
                    break;
                case '1':
                    pos.X = 102;
                    pos.Y = 64;
                    break;
                case '2':
                    pos.X = 134;
                    pos.Y = 64;
                    break;
                case '3':
                    pos.X = 166;
                    pos.Y = 64;
                    break;
                case '4':
                    pos.X = 197;
                    pos.Y = 64;
                    break;
                case '5':
                    pos.X = 230;
                    pos.Y = 64;
                    break;
                case '6':
                    pos.X = 261;
                    pos.Y = 64;
                    break;
                case '7':
                    pos.X = 294;
                    pos.Y = 64;
                    break;
                case '8':
                    pos.X = 325;
                    pos.Y = 64;
                    break;
                case '9':
                    pos.X = 358;
                    pos.Y = 64;
                    break;
                case '!':
                    pos.X = 395;
                    pos.Y = 64;
                    break;
                case '@':
                    pos.X = 416;
                    pos.Y = 64;
                    break;
                case '#':
                    pos.X = 454;
                    pos.Y = 64;
                    break;
                case '$':
                    pos.X = 486;
                    pos.Y = 64;
                    break;
                case '%':
                    pos.X = 514;
                    pos.Y = 64;
                    break;
                case '^':
                    pos.X = 549;
                    pos.Y = 64;
                    break;
                case '&':
                    pos.X = 578;
                    pos.Y = 64;
                    break;
                case '*':
                    pos.X = 615;
                    pos.Y = 64;
                    break;
                case '(':
                    pos.X = 648;
                    pos.Y = 64;
                    break;
                case ')':
                    pos.X = 684;
                    pos.Y = 64;
                    break;
                case '-':
                    pos.X = 711;
                    pos.Y = 64;
                    break;
                case '+':
                    pos.X = 741;
                    pos.Y = 64;
                    break;
                case '_':
                    pos.X = 769;
                    pos.Y = 96;
                    break;
                case '?':
                    pos.X = 9;
                    pos.Y = 96;
                    break;
                case ':':
                    pos.X = 43;
                    pos.Y = 96;
                    break;
                case ';':
                    pos.X = 75;
                    pos.Y = 96;
                    break;
                case '"':
                    pos.X = 105;
                    pos.Y = 96;
                    break;
                case '|':
                    pos.X = 138;
                    pos.Y = 96;
                    break;
                case '\\':
                    pos.X = 166;
                    pos.Y = 96;
                    break;
                case '/':
                    pos.X = 198;
                    pos.Y = 96;
                    break;
                case ' ':
                    pos.X = 227;
                    pos.Y = 96;
                    break;
                case '{':
                    pos.X = 263;
                    pos.Y = 96;
                    break;
                case '}':
                    pos.X = 300;
                    pos.Y = 96;
                    break;
                case '[':
                    pos.X = 329;
                    pos.Y = 96;
                    break;
                case ']':
                    pos.X = 362;
                    pos.Y = 96;
                    break;
                case '.':
                    pos.X = 395;
                    pos.Y = 96;
                    break;
                case '<':
                    pos.X = 421;
                    pos.Y = 96;
                    break;
                case '>':
                    pos.X = 455;
                    pos.Y = 96;
                    break;
                case ',':
                    pos.X = 491;
                    pos.Y = 96;
                    break;
                case '=':
                    pos.X = 518;
                    pos.Y = 96;
                    break;
                case '~':
                    pos.X = 550;
                    pos.Y = 96;
                    break;
                case '\'':
                    pos.X = 588;
                    pos.Y = 96;
                    break;
            }
            return pos;
        }
        /// <summary>
        /// Returns the source defualt width of a provided char, used internally
        /// </summary>
        /// <param name="c">Char to find</param>
        /// <returns>Returns width as an int</returns>
        public static int GetCharDefualtWidth(char c)
        {

            switch (c)
            {
                case 'a':
                    return 18;
                case 'b':
                    return 20;
                case 'c':
                    return 18;
                case 'd':
                    return 20;
                case 'e':
                    return 20;
                case 'f':
                    return 14;
                case 'g':
                    return 20;
                case 'h':
                    return 20;
                case 'i':
                    return 8;
                case 'j':
                    return 12;
                case 'k':
                    return 20;
                case 'l':
                    return 12;
                case 'm':
                    return 30;
                case 'n':
                    return 20;
                case 'o':
                    return 23;
                case 'p':
                    return 24;
                case 'q':
                    return 18;
                case 'r':
                    return 16;
                case 's':
                    return 18;
                case 't':
                    return 16;
                case 'u':
                    return 20;
                case 'v':
                    return 22;
                case 'w':
                    return 28;
                case 'x':
                    return 20;
                case 'y':
                    return 20;
                case 'z':
                    return 18;
                case 'A':
                    return 25;
                case 'B':
                    return 22;
                case 'C':
                    return 21;
                case 'D':
                    return 24;
                case 'E':
                    return 18;
                case 'F':
                    return 17;
                case 'G':
                    return 25;
                case 'H':
                    return 23;
                case 'I':
                    return 9;
                case 'J':
                    return 14;
                case 'K':
                    return 21;
                case 'L':
                    return 18;
                case 'M':
                    return 31;
                case 'N':
                    return 24;
                case 'O':
                    return 27;
                case 'P':
                    return 21;
                case 'Q':
                    return 28;
                case 'R':
                    return 21;
                case 'S':
                    return 19;
                case 'T':
                    return 22;
                case 'U':
                    return 24;
                case 'V':
                    return 26;
                case 'W':
                    return 32;
                case 'X':
                    return 24;
                case 'Y':
                    return 23;
                case 'Z':
                    return 20;
                case '0':
                    return 21;
                case '1':
                    return 20;
                case '2':
                    return 21;
                case '3':
                    return 21;
                case '4':
                    return 24;
                case '5':
                    return 21;
                case '6':
                    return 21;
                case '7':
                    return 19;
                case '8':
                    return 21;
                case '9':
                    return 21;
                case '!':
                    return 20;
                case '@':
                    return 31;
                case '#':
                    return 21;
                case '$':
                    return 21;
                case '%':
                    return 29;
                case '^':
                    return 22;
                case '&':
                    return 28;
                case '*':
                    return 17;
                case '(':
                    return 12;
                case ')':
                    return 12;
                case '-':
                    return 19;
                case '+':
                    return 21;
                case '_':
                    return 30;
                case '?':
                    return 18;
                case ':':
                    return 12;
                case ';':
                    return 12;
                case '"':
                    return 14;
                case '|':
                    return 10;
                case '\\':
                    return 20;
                case '/':
                    return 20;
                case ' ':
                    return 26;
                case '{':
                    return 15;
                case '}':
                    return 15;
                case '[':
                    return 13;
                case ']':
                    return 13;
                case '.':
                    return 10;
                case '<':
                    return 20;
                case '>':
                    return 20;
                case ',':
                    return 12;
                case '=':
                    return 20;
                case '~':
                    return 21;
                case '\'':
                    return 9;
            }
            return 0;
        }
        /// <summary>
        /// Loads a specified font source from TextureBank
        /// </summary>
        /// <param name="name">Font name</param>
        public static void loadFont(string name)
        {
            source = TextureBank.getTexture(name);
            sourceName = name;
        }
        /// <summary>
        /// Loads the source directly from file path
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="name">File path of font source</param>
        /// <param name="transparent">Transparent colour</param>
        /// <param name="width">Width of source texture</param>
        /// <param name="height">Height of source texture</param>
        public static void loadSourceDirectly(IntPtr renderer, string name, XENOCOLOURS transparent, int width, int height)
        {
            //save sourceName
            string[] strArray = StringParser.parse(name);
            string[] strArray2 = StringParser.parse(strArray[strArray.Length - 1]);
            sourceName = strArray2[1];
            //load source
            IntPtr temp2 = default(IntPtr);
            IntPtr surface = SDL_image.IMG_Load(name);
            IntPtr format = SDL.SDL_AllocFormat(SDL.SDL_PIXELFORMAT_RGB888);
            SDL.SDL_Color colour = ColourBank.getColour(transparent);
            SDL.SDL_SetColorKey(surface, 1, SDL.SDL_MapRGB(format, colour.r, colour.g, colour.b));
            temp2 = SDL.SDL_CreateTextureFromSurface(renderer, surface);
            Texture2D tex = new Texture2D(temp2, width, height);
            SDL.SDL_FreeSurface(surface);
            source = tex;
        }
        /// <summary>
        /// Mods source by colour provided
        /// </summary>
        /// <param name="colour">Colour flag</param>
        public static void setWhiteToColour(XENOCOLOURS colour)
        {
            SDL.SDL_Color c = ColourBank.getColour(colour);
            SDL.SDL_SetTextureColorMod(source.texture, c.r, c.g, c.b);
        }
        /// <summary>
        /// Draws a string at a position provided a font source as a Texture2D
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="str">String to draw</param>
        /// <param name="x">X position to draw string at</param>
        /// <param name="y">Y position to draw string at</param>
        /// <param name="scaler">Scales size of string to render with 1 being 32x32 pixel chars</param>
        public static void DrawString(IntPtr renderer, string str, int x, int y, float scaler = 1.0f)
        {
            charSize = 0;
            charPos = x;
            for (int i = 0; i < str.Length; i++)
            {
                srcPos = GetCharPos(str[i]);
                srcRect.X = srcPos.X;
                srcRect.Y = srcPos.Y;
                charPos += (int)(charSize * scaler);
                charSize = GetCharWidth(str[i]);
                destRect.Height = 32 * scaler;
                DrawChar(renderer, new Point2D(charPos, y), scaler);
            }
        }
        /// <summary>
        /// Draws a string at a position provided a font source as a Texture2D
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="str">String to draw</param>
        /// <param name="x">X position to draw string at</param>
        /// <param name="y">Y position to draw string at</param>
        /// <param name="scaler">Scales size of string to render with 1 being 32x32 pixel chars</param>
        public static void DrawString(IntPtr renderer, string str, float x, float y, float scaler = 1.0f)
        {
            charSize = 0;
            charPos = (int)x;
            for (int i = 0; i < str.Length; i++)
            {
                srcPos = GetCharPos(str[i]);
                srcRect.X = srcPos.X;
                srcRect.Y = srcPos.Y;
                charPos += (int)(charSize * scaler);
                charSize = GetCharWidth(str[i]);
                destRect.Height = 32 * scaler;
                DrawChar(renderer, new Point2D(charPos, y), scaler);
            }
        }
        /// <summary>
        /// Draws a string at a position provided a font source as a Texture2D
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="str">String to draw</param>
        /// <param name="x">X position to draw string at</param>
        /// <param name="y">Y position to draw string at</param>
        /// <param name="width">Max width of render space</param>
        /// <param name="scaler">Scales size of string to render with 1 being 32x32 pixel chars</param>
        public static void DrawString(IntPtr renderer, string str, float x, float y, float width, float scaler = 1.0f)
        {
            charSize = 0;
            charPos = (int)x;
            int srcIndex = 0;
            for (int i = 0; i < str.Length; i++)
            {
                srcPos = GetCharPos(str[i]);
                if (charPos + GetCharWidth(str[i]) <= x + width)
                {
                    srcIndex = getCharIndex(str[i]);
                    srcRect.X = srcPos.X;
                    srcRect.Y = srcPos.Y;
                    srcRect.Width = boxes[srcIndex].Width;
                    srcRect.Height = boxes[srcIndex].Height;
                    charPos += (int)(charSize * scaler);
                    charSize = GetCharWidth(str[i]);
                    destRect.Width = charSize * scaler;
                    destRect.Height = 32 * scaler;
                    DrawChar(renderer, new Point2D(charPos, y), scaler);
                }
                else
                {
                    srcIndex = getCharIndex(str[i]);
                    float tmp = (x + width) - (charPos + boxes[srcIndex].Width);
                    srcRect.X = srcPos.X;
                    srcRect.Y = srcPos.Y;
                    srcRect.Width = boxes[srcIndex].Width;
                    srcRect.Height = boxes[srcIndex].Height;
                    destRect.Width = tmp * scaler;
                    destRect.Height = boxes[srcIndex].Height * scaler;
                    DrawChar(renderer, new Point2D(charPos, y), scaler);
                    break;//no more characters can be drawn so break from loop
                }
            }
        }
        /// <summary>
        /// Returns the source position of a provided char, used internally
        /// </summary>
        /// <param name="c">Char to find</param>
        /// <returns>Returns position as a Point2D</returns>
        public static Point2D GetCharPos(char c)
        {
            Point2D pos = new Point2D();
            for(int i = 0; i < chars.Count; i++)
            {
                if(chars[i] == c.ToString())
                {
                    pos.X = boxes[i].X;
                    pos.Y = boxes[i].Y;
                    break;
                }
            }
            return pos;
        }
        /// <summary>
        /// Returns the source width of a provided char, used internally
        /// </summary>
        /// <param name="c">Char to find</param>
        /// <returns>Returns width as an int</returns>
        public static int GetCharWidth(char c)
        {
            for (int i = 0; i < chars.Count; i++)
            {
                if (chars[i] == c.ToString())
                {
                    return (int)boxes[i].Width;
                }
            }
            return 0;
        }
        /// <summary>
        /// Draws a single char, used internally
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="pos">Position of char to render</param>
        /// <param name="scaler">Scaling value</param>
        public static void DrawChar(IntPtr renderer, Point2D pos, float scaler)
        {
            destRect.X = pos.X;
            destRect.Y = pos.Y;;
            SimpleDraw.draw(renderer, source, srcRect, destRect);
        }
        /// <summary>
        /// Returns the source box index value provided a char
        /// </summary>
        /// <param name="c">Character to get index of</param>
        /// <returns>Integar</returns>
        public static int getCharIndex(char c)
        {
            switch (c)
            {
                case 'a':
                    return 0;
                case 'b':
                    return 1;
                case 'c':
                    return 2;
                case 'd':
                    return 3;
                case 'e':
                    return 4;
                case 'f':
                    return 5;
                case 'g':
                    return 6;
                case 'h':
                    return 7;
                case 'i':
                    return 8;
                case 'j':
                    return 9;
                case 'k':
                    return 10;
                case 'l':
                    return 11;
                case 'm':
                    return 12;
                case 'n':
                    return 13;
                case 'o':
                    return 14;
                case 'p':
                    return 15;
                case 'q':
                    return 16;
                case 'r':
                    return 17;
                case 's':
                    return 18;
                case 't':
                    return 19;
                case 'u':
                    return 20;
                case 'v':
                    return 21;
                case 'w':
                    return 22;
                case 'x':
                    return 23;
                case 'y':
                    return 24;
                case 'z':
                    return 25;
                case 'A':
                    return 26;
                case 'B':
                    return 27;
                case 'C':
                    return 28;
                case 'D':
                    return 29;
                case 'E':
                    return 30;
                case 'F':
                    return 31;
                case 'G':
                    return 32;
                case 'H':
                    return 33;
                case 'I':
                    return 34;
                case 'J':
                    return 35;
                case 'K':
                    return 36;
                case 'L':
                    return 37;
                case 'M':
                    return 38;
                case 'N':
                    return 39;
                case 'O':
                    return 40;
                case 'P':
                    return 41;
                case 'Q':
                    return 42;
                case 'R':
                    return 43;
                case 'S':
                    return 44;
                case 'T':
                    return 45;
                case 'U':
                    return 46;
                case 'V':
                    return 47;
                case 'W':
                    return 48;
                case 'X':
                    return 49;
                case 'Y':
                    return 50;
                case 'Z':
                    return 51;
                case '0':
                    return 52;
                case '1':
                    return 53;
                case '2':
                    return 54;
                case '3':
                    return 55;
                case '4':
                    return 56;
                case '5':
                    return 57;
                case '6':
                    return 58;
                case '7':
                    return 59;
                case '8':
                    return 60;
                case '9':
                    return 61;
                case '!':
                    return 62;
                case '@':
                    return 63;
                case '#':
                    return 64;
                case '$':
                    return 65;
                case '%':
                    return 66;
                case '^':
                    return 67;
                case '&':
                    return 68;
                case '*':
                    return 69;
                case '(':
                    return 70;
                case ')':
                    return 71;
                case '-':
                    return 72;
                case '+':
                    return 73;
                case '_':
                    return 74;
                case '?':
                    return 75;
                case ':':
                    return 76;
                case ';':
                    return 77;
                case '"':
                    return 78;
                case '|':
                    return 79;
                case '\\':
                    return 80;
                case '/':
                    return 81;
                case ' ':
                    return 82;
                case '{':
                    return 83;
                case '}':
                    return 84;
                case '[':
                    return 85;
                case ']':
                    return 86;
                case '.':
                    return 87;
                case '<':
                    return 88;
                case '>':
                    return 89;
                case ',':
                    return 90;
                case '=':
                    return 91;
                case '~':
                    return 92;
                case '\'':
                    return 93;
            }
            return 0;
        }
        /// <summary>
        /// Calculates the width of rendered text
        /// </summary>
        /// <param name="str">String reference</param>
        /// <param name="scaler">Scaler value</param>
        /// <returns>Integer</returns>
        public static int stringRenderWidth(string str, float scaler)
        {
            charSize = 0;
            charPos = 0;
            for (int i = 0; i < str.Length; i++)
            {
                //charPos += (int)(charSize * scaler);
                charSize = GetCharWidth(str[i]);
                charPos += (int)(charSize * scaler);
            }
            return charPos;
        }
        /// <summary>
        /// Loads coloured font sheets into the TextureBank, named by colour;
        /// black, white, green, yellow, pink, red, gray, orange, purple, brown
        /// </summary>
        /// <param name="path">Graphics folder path</param>
        /// <param name="renderer">Renderer reference</param>
        public static void loadFontColours(string path, IntPtr renderer)
        {
            string file = path;
            file += "my font white.png";
            TextureBank.addTexture("white", TextureLoader.load("white", renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font black.png";
            TextureBank.addTexture("black", TextureLoader.load("black", renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font green.png";
            TextureBank.addTexture("green", TextureLoader.load("green", renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font yellow.png";
            TextureBank.addTexture("yellow", TextureLoader.load("yellow", renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font pink.png";
            TextureBank.addTexture("pink", TextureLoader.load("pink", renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font red.png";
            TextureBank.addTexture("red", TextureLoader.load("red", renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font gray.png";
            TextureBank.addTexture("gray", TextureLoader.load("gray", renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font orange.png";
            TextureBank.addTexture("orange", TextureLoader.load("orange", renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font purple.png";
            TextureBank.addTexture("purple", TextureLoader.load("purple", renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
            file = path;
            file += "my font brown.png";
            TextureBank.addTexture("brown", TextureLoader.load("brown", renderer, ColourBank.getColour(XENOCOLOURS.MAGENTA), 800, 128));
        }
        /// <summary>
        /// Draws a coloured string at specified location
        /// *** LoadFontColours must be called before use and font graphics must
        /// use name of "my font 'colour'" ***
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="text">String to render</param>
        /// <param name="x">X position to render at</param>
        /// <param name="y">Y position to render at</param>
        /// <param name="colour">Colour of font to use</param>
        /// <param name="scaler">Scaling value</param>
        public static void drawColourString(IntPtr renderer, string text, float x, float y, string colour, float scaler = 1)
        {
            //set colour of text
            sourceName = colour;
            source = TextureBank.getTexture(colour);

            DrawString(renderer, text, x, y, scaler);

            //reset colour of text to white
            sourceName = "white";
            source = TextureBank.getTexture("white");
        }
        /// <summary>
        /// Draws a coloured string at specified location
        /// *** LoadFontColours must be called before use and font graphics must
        /// use name of "my font 'colour'" ***
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="text">String to render</param>
        /// <param name="x">X position to render at</param>
        /// <param name="y">Y position to render at</param>
        /// <param name="colour">Colour of font to use</param>
        /// <param name="width">Max width of render space</param>
        /// <param name="scaler">Scaling value</param>
        public static void drawColourString(IntPtr renderer, string text, float x, float y, string colour, int width, float scaler = 1)
        {
            //set colour of text
            sourceName = colour;
            source = TextureBank.getTexture(colour);

            DrawString(renderer, text, x, y, width, scaler);

            //reset colour of text to white
            sourceName = "white";
            source = TextureBank.getTexture("white");
        }
    }

    /// <summary>
    /// Stores and renders a text message at a position for a set number of ticks
    /// </summary>
    public class SimpleTextObject
    {
        //protected
        protected string message;
        protected string colour;
        protected Point2D center;
        protected int width;
        protected int ticks;
        protected int maxTicks;
        protected float scaler;

        //public
        /// <summary>
        /// SimpleTextObject constructor
        /// </summary>
        /// <param name="x">X position of center</param>
        /// <param name="y">Y position of center</param>
        /// <param name="colour">Colour of font</param>
        /// <param name="maxTicks">Max number of ticks</param>
        /// <param name="message">Message to display</param>
        /// <param name="scaler">Scaler value</param>
        public SimpleTextObject(float x, float y, string colour, int maxTicks, string message, float scaler = 1)
        {
            center = new Point2D(x, y);
            this.width = SimpleFont.stringRenderWidth(message, scaler);
            this.message = message;
            this.colour = colour;
            this.maxTicks = maxTicks;
            this.scaler = scaler;
            ticks = 0;
        }
        /// <summary>
        /// Draws the SimpleTextObject
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="winx">Window X offset</param>
        /// <param name="winy">Window Y offset</param>
        /// <returns>True if finished else false</returns>
        public bool draw(IntPtr renderer, float winx = 0, float winy = 0)
        {
            SimpleFont.drawColourString(renderer, message, center.X - (width / 2) - winx, center.Y - winy, colour, scaler);
            ticks++;
            if(ticks >= maxTicks)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Ticks up count but does not render
        /// </summary>
        /// <returns></returns>
        public bool tickUp()
        {
            ticks++;
            if (ticks >= maxTicks)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Center property
        /// </summary>
        public Point2D Center
        {
            get { return center; }
        }
    }
    /// <summary>
    /// Provides a means to add text objects anywhere on screen or off screen 
    /// for a specified number of frames 
    /// </summary>
    public static class TextObjects
    {
        //private
        static List<SimpleTextObject> textObjects;
        static int numTicks;

        //public
        /// <summary>
        /// TextObjects constructor
        /// </summary>
        static TextObjects()
        {
            textObjects = new List<SimpleTextObject>();
            numTicks = 180;
        }
        /// <summary>
        /// Adds a text object to render
        /// </summary>
        /// <param name="text">Text to render</param>
        /// <param name="colour">Colour of font</param>
        /// <param name="x">Center X position</param>
        /// <param name="y">Center Y position</param>
        /// <param name="scaler">Scaler value</param>
        public static void addTextObject(string text, string colour, float x, float y, float scaler)
        {
            textObjects.Add(new SimpleTextObject(x, y, colour, numTicks, text, scaler));
        }
        /// <summary>
        /// Renders or updates all SimpleTextObjects stored, removing finished ones
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="wx">Window position in pixels</param>
        /// <param name="wy">Window position in pixels</param>
        /// <param name="ww">Window width in pixels </param>
        /// <param name="wh">Window height in pixels</param>
        public static void drawTextObjects(IntPtr renderer, int wx, int wy, int ww, int wh)
        {
            for(int i = 0; i < textObjects.Count; i++)
            {
                if(textObjects[i].Center.X >= wx && textObjects[i].Center.X <= wx + ww)
                {
                    if (textObjects[i].Center.Y >= wy && textObjects[i].Center.Y <= wy + wh)
                    {
                        if(textObjects[i].draw(renderer, wx, wy) == true)
                        {
                            textObjects.RemoveAt(i);
                        }
                    }
                    else
                    {
                        if(textObjects[i].tickUp() == true)
                        {
                            textObjects.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    if (textObjects[i].tickUp() == true)
                    {
                        textObjects.RemoveAt(i);
                    }
                }
            }
        }
    }
}
