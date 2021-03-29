using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace XenoLib
{
    /// <summary>
    /// DrawLine class
    /// </summary>
    public static class DrawLine
    {
        /// <summary>
        /// Draws a line
        /// </summary>
        /// <param name="renderer">Render IntPtr</param>
        /// <param name="a">Point a</param>
        /// <param name="b">Point b</param>
        /// <param name="colour">Colour of line</param>
        /// <param name="winx">Window X offset</param>
        /// <param name="winy">Window Y offset</param>
        public static void draw(IntPtr renderer, Point2D a, Point2D b, SDL.SDL_Color colour, int winx = 0, int winy = 0)
        {
            SDL.SDL_Color temp;
            SDL.SDL_GetRenderDrawColor(renderer, out temp.r, out temp.g, out temp.b, out temp.a);
            SDL.SDL_SetRenderDrawColor(renderer, colour.r, colour.g, colour.b, colour.a);
            SDL.SDL_RenderDrawLine(renderer, (int)a.X - winx, (int)a.Y - winy, (int)b.X - winx, (int)b.Y - winy);
            SDL.SDL_SetRenderDrawColor(renderer, temp.r, temp.g, temp.b, temp.a);
        }

        public static void drawCircle(IntPtr renderer, Point2D center, float radius, SDL.SDL_Color colour, int winx = 0, int winy =0)
        {
            Point2D a = new Point2D(0, 0);
            Point2D b = new Point2D(0, 0);
            a.X = (float)Math.Cos(0) + radius;
            a.Y = (float)Math.Sin(0) + radius;
            b.X = (float)Math.Cos(20) + radius;
            b.Y = (float)Math.Sin(20) + radius;
            for (int i = 0; i < 18; i++)
            {
                draw(renderer, a, b, colour, winx, winy);
                a.X = b.X;
                a.Y = b.Y;
                b.X = (float)Math.Cos(20 + (i * 20)) + radius;
                b.Y = (float)Math.Sin(20 + (i * 20)) + radius;
            }
        }
    }
}
