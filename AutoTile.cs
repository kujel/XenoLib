﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SDL2;

namespace XenoLib
{
    /// <summary>
    /// AutoTile class
    /// </summary>
    public class AutoTile
    {
        //protected
        protected Texture2D src;
        protected Point2D pos;
        protected Rectangle srcBox;
        protected Rectangle destBox;
        protected int frame;

        //public
        /// <summary>
        /// AutoTile constructor
        /// </summary>
        /// <param name="src">Texture2D reference</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="frame">Tile frame</param>
        public AutoTile(Texture2D src, int x, int y, int w, int h, int frame = 0)
        {
            this.src = src;
            pos = new Point2D(x, y);
            srcBox = new Rectangle(0, 0, w, h);
            destBox = new Rectangle(x, y, w, h);
            this.frame = frame;
        }
        /// <summary>
        /// AutoTile from file constructor
        /// </summary>
        /// <param name="src">Texture2D reference</param>
        /// <param name="sr">StreamReader reference</param>
        public AutoTile(Texture2D src, StreamReader sr)
        {
            this.src = src;
            string b1 = sr.ReadLine();
            string b2 = sr.ReadLine();
            pos = new Point2D(Convert.ToInt32(b1), Convert.ToInt32(b2));
            destBox = new Rectangle(pos.X, pos.Y, Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));
            srcBox = new Rectangle(0, 0, destBox.Width, destBox.Height);
            frame = Convert.ToInt32(sr.ReadLine());
        }
        /// <summary>
        /// Save data
        /// </summary>
        /// <param name="sw">StreamWriter reference</param>
        public void saveData(StreamWriter sw)
        {
            sw.WriteLine((int)pos.X);
            sw.WriteLine((int)pos.Y);
            sw.WriteLine(destBox.Width);
            sw.WriteLine(destBox.Height);
            sw.WriteLine(frame);
        }
        /// <summary>
        /// Draw AutoTile
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="winx">Window x offset</param>
        /// <param name="winy">Window y offset</param>
        public void draw(IntPtr renderer, int winx = 0, int winy = 0)
        {
            //int modx = winx % ((int)destBox.Width - 1);
            //int mody = winy % ((int)destBox.Height - 1);
            destBox.X = pos.X - winx;// + modx);
            destBox.Y = pos.Y - winy;// + mody);
            srcBox.X = frame * srcBox.Width;
            SimpleDraw.draw(renderer, src, srcBox, destBox);
        }
        /// <summary>
        /// Frame property
        /// </summary>
        public int Frame
        {
            get { return frame; }
            set { frame = value; }
        }
        /// <summary>
        /// X property
        /// </summary>
        public float X
        {
            get { return pos.X; }
            set { pos.X = value; }
        }
        /// <summary>
        /// Y property
        /// </summary>
        public float Y
        {
            get { return pos.Y; }
            set { pos.Y = value; }
        }
    }
    /// <summary>
    /// AutoTileSys class
    /// </summary>
    public class AutoTileSys
    {
        //protected
        protected Texture2D src;
        protected int width;
        protected int height;
        protected int tileWidth;
        protected int tileHeight;
        protected DataGrid<AutoTile> grid;

        //public
        /// <summary>
        /// AutoTileSys constructor
        /// </summary>
        /// <param name="src">Texture2D reference</param>
        /// <param name="w">Width in tiles</param>
        /// <param name="h">Height in tiles</param>
        /// <param name="tw">Tile width</param>
        /// <param name="th">Tile height</param>
        public AutoTileSys(Texture2D src, int w, int h, int tw, int th)
        {
            this.src = src;
            width = w;
            height = h;
            tileWidth = th;
            tileHeight = th;
            grid = new DataGrid<AutoTile>(w, h);
        }
        /// <summary>
        /// AutoTileSys from file constructor
        /// </summary>
        /// <param name="src">Texture2D reference</param>
        /// <param name="sr">StreamReader reference</param>
        public AutoTileSys(Texture2D src, StreamReader sr)
        {
            this.src = src;
            sr.ReadLine();//discard testing data
            width = Convert.ToInt32(sr.ReadLine());
            height = Convert.ToInt32(sr.ReadLine());
            tileWidth = Convert.ToInt32(sr.ReadLine());
            tileHeight = Convert.ToInt32(sr.ReadLine());
            grid = new DataGrid<AutoTile>(width, height);
            for(int i = 0; i < width; i++)
            {
                for (int k = 0; k < height; k++)
                {
                    string temp = sr.ReadLine();
                    if (temp == "null")
                    {
                        grid.Grid[i, k] = null;
                    }
                    else
                    {
                        grid.Grid[i, k] = new AutoTile(src, i * tileWidth, k * tileHeight, tileWidth, tileHeight, Convert.ToInt32(temp));
                    }
                }
            }
        }
        /// <summary>
        /// Save data
        /// </summary>
        /// <param name="sw">StreamWriter reference</param>
        public void saveData(StreamWriter sw)
        {
            sw.WriteLine("======AutoTile Data======");
            sw.WriteLine(width);
            sw.WriteLine(height);
            sw.WriteLine(tileWidth);
            sw.WriteLine(tileHeight);
            for (int i = 0; i < width; i++)
            {
                for (int k = 0; k < height; k++)
                {
                    if (grid.Grid[i, k] == null)
                    {
                        sw.WriteLine("null");
                    }
                    else
                    {
                        sw.WriteLine(grid.Grid[i, k].Frame);
                    }
                }
            }
        }
        /// <summary>
        /// Draw the AutoTileSys provided window's position, dimensions and per tile shift values
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="winx">Window x position</param>
        /// <param name="winy">Window y position</param>
        /// <param name="winWidth">Window width in tiles</param>
        /// <param name="winHeight">Window height in tiles</param>
        /// <param name="shiftx">X shift per tile value</param>
        /// <param name="shifty">Y shift per tile value</param>
        public void draw(IntPtr renderer, int winx, int winy, int winWidth, int winHeight, int shiftx = 0, int shifty = 0)
        {
            //win(x/y) is tile (x/y) to draw so remove win(x/y) to shift to zero, shift(x/y) is window offset
            int offsetX = winx + shiftx;//modx = winx % tileWidth;
            int offsetY = winy + shifty;//mody = winy % tileHeight;
            for (int x = winx / tileWidth; x < winx / tileWidth + winWidth; x++)
            {
                for (int y = winy / tileHeight; y < winy / tileHeight + winHeight; y++)
                {
                    if (inDomain(x, y))
                    {
                        if (grid.Grid[x, y] != null)
                        {
                            grid.Grid[x, y].draw(renderer, offsetX, offsetY);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Add an AutoTile at x, y position
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        public void addTile(int x, int y)
        {
            if(inDomain(x, y))
            {
                grid.Grid[x, y] = new AutoTile(src, x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                setTileFrame(x, y);
                //set all tile frames around this tile
                setTileFrame(x - 1, y - 1);//up-left
                setTileFrame(x, y - 1);//up
                setTileFrame(x + 1, y - 1);//up-right
                setTileFrame(x + 1, y);//right
                setTileFrame(x + 1, y + 1);//down-right
                setTileFrame(x, y + 1);//down
                setTileFrame(x - 1, y + 1);//down-left
                setTileFrame(x - 1, y);//left
            }
        }
        /// <summary>
        /// Fill AutoTileSys with tiles
        /// </summary>
        public void fill(int frame = 13)
        {
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid.Grid[x, y] = new AutoTile(src, x * tileWidth, y * tileHeight, tileWidth, tileHeight, frame);
                }
            }
        }
        /// <summary>
        /// Remove an AutoTile at x, y position
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        public void removeTile(int x, int y)
        {
            if (inDomain(x, y))
            {
                grid.Grid[x, y] = null;
                //set all tile frames around this tile
                setTileFrame(x - 1, y - 1);//up-left
                setTileFrame(x, y - 1);//up
                setTileFrame(x + 1, y - 1);//up-right
                setTileFrame(x + 1, y);//right
                setTileFrame(x + 1, y + 1);//down-right
                setTileFrame(x, y + 1);//down
                setTileFrame(x - 1, y + 1);//down-left
                setTileFrame(x - 1, y);//left
            }
        }
        /// <summary>
        /// Sets the frame of tile at position x, y
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        public void setTileFrame(int x, int y)
        {
            bool alpha = false;
            bool beta = false;
            bool delta = false;
            bool gamma = false;
            if(inDomain(x, y))
            {
                if (grid.Grid[x, y] != null)
                {
                    if (inDomain(x, y - 1))
                    {
                        if (grid.Grid[x, y - 1] != null)
                        {
                            alpha = true;
                        }
                    }
                    else
                    {
                        alpha = true;
                    }
                    if (inDomain(x + 1, y))
                    {
                        if (grid.Grid[x + 1, y] != null)
                        {
                            beta = true;
                        }
                    }
                    else
                    {
                        beta = true;
                    }
                    if (inDomain(x, y + 1))
                    {
                        if (grid.Grid[x, y + 1] != null)
                        {
                            delta = true;
                        }
                    }
                    else
                    {
                        delta = true;
                    }
                    if (inDomain(x - 1, y))
                    {
                        if (grid.Grid[x - 1, y] != null)
                        {
                            gamma = true;

                        }
                    }
                    else
                    {
                        gamma = true;
                    }
                    //all sides empty, check corners
                    if (!alpha && !beta && !delta && !gamma)
                    {
                        if (inDomain(x - 1, y - 1))
                        {
                            if (grid.Grid[x - 1, y - 1] != null)
                            {
                                alpha = true;
                            }
                        }
                        else
                        {
                            alpha = true;
                        }
                        if (inDomain(x + 1, y - 1))
                        {
                            if (grid.Grid[x + 1, y - 1] != null)
                            {
                                beta = true;
                            }
                        }
                        else
                        {
                            beta = true;
                        }
                        if (inDomain(x + 1, y + 1))
                        {
                            if (grid.Grid[x + 1, y + 1] != null)
                            {
                                delta = true;
                            }
                        }
                        else
                        {
                            delta = true;
                        }
                        if (inDomain(x - 1, y + 1))
                        {
                            if (grid.Grid[x - 1, y + 1] != null)
                            {
                                gamma = true;
                            }
                        }
                        else
                        {
                            gamma = true;
                        }
                        if (!alpha && !beta && !delta && !gamma) // tile has no tiles around it
                        {
                            grid.Grid[x, y].Frame = 0;
                        }
                        else
                        {
                            setTileCorners(x, y, alpha, beta, delta, gamma);
                        }
                    }
                    else
                    {
                        setTileSides(x, y, alpha, beta, delta, gamma);
                    }
                }
            }
        }
        /// <summary>
        /// Sets frame of tile at position x, y based on what tiles are at its sides
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="alpha">Side alpha (up) flag</param>
        /// <param name="beta">Side beta (right) flag</param>
        /// <param name="delta">Side delta (down) flag</param>
        /// <param name="gamma">Side gamma (left) flag</param>
        public void setTileSides(int x, int y, bool alpha, bool beta, bool delta, bool gamma)
        {
            if(alpha && !beta && !delta && !gamma)
            {
                grid.Grid[x, y].Frame = 1;
            }
            else if (!alpha && beta && !delta && !gamma)
            {
                grid.Grid[x, y].Frame = 2;
            }
            else if (!alpha && !beta && delta && !gamma)
            {
                grid.Grid[x, y].Frame = 3;
            }
            else if (!alpha && !beta && !delta && gamma)
            {
                grid.Grid[x, y].Frame = 4;
            }
            else if (alpha && beta && !delta && !gamma)
            {
                grid.Grid[x, y].Frame = 5;
            }
            else if (!alpha && beta && delta && !gamma)
            {
                grid.Grid[x, y].Frame = 6;
            }
            else if (!alpha && !beta && delta && gamma)
            {
                grid.Grid[x, y].Frame = 7;
            }
            else if (alpha && !beta && !delta && gamma)
            {
                grid.Grid[x, y].Frame = 8;
            }
            else if (alpha && beta && delta && !gamma)
            {
                grid.Grid[x, y].Frame = 9;
            }
            else if (!alpha && beta && delta && gamma)
            {
                grid.Grid[x, y].Frame = 10;
            }
            else if (alpha && !beta && delta && gamma)
            {
                grid.Grid[x, y].Frame = 11;
            }
            else if (alpha && beta && !delta && gamma)
            {
                grid.Grid[x, y].Frame = 12;
            }
            else if (alpha && beta && delta && gamma)
            {
                grid.Grid[x, y].Frame = 13;
            }
        }
        /// <summary>
        /// Sets frame of tile at position x, y based on what tiles are at its corners
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="alpha">Corner alpha (up-left) flag</param>
        /// <param name="beta">Corner beta (up-right) flag</param>
        /// <param name="delta">Corner delta (down-right) flag</param>
        /// <param name="gamma">Corner gamma (down-left) flag</param>
        public void setTileCorners(int x, int y, bool alpha, bool beta, bool delta, bool gamma)
        {
            if (alpha && !beta && !delta && !gamma)
            {
                grid.Grid[x, y].Frame = 14;
            }
            else if (!alpha && beta && !delta && !gamma)
            {
                grid.Grid[x, y].Frame = 15;
            }
            else if (!alpha && !beta && delta && !gamma)
            {
                grid.Grid[x, y].Frame = 16;
            }
            else if (!alpha && !beta && !delta && gamma)
            {
                grid.Grid[x, y].Frame = 17;
            }
            else if (alpha && beta && !delta && !gamma)
            {
                grid.Grid[x, y].Frame = 18;
            }
            else if (!alpha && beta && delta && !gamma)
            {
                grid.Grid[x, y].Frame = 19;
            }
            else if (!alpha && !beta && delta && gamma)
            {
                grid.Grid[x, y].Frame = 20;
            }
            else if (alpha && !beta && !delta && gamma)
            {
                grid.Grid[x, y].Frame = 21;
            }
            else if (alpha && beta && delta && !gamma)
            {
                grid.Grid[x, y].Frame = 22;
            }
            else if (!alpha && beta && delta && gamma)
            {
                grid.Grid[x, y].Frame = 23;
            }
            else if (alpha && !beta && delta && gamma)
            {
                grid.Grid[x, y].Frame = 24;
            }
            else if (alpha && beta && !delta && gamma)
            {
                grid.Grid[x, y].Frame = 25;
            }
            else if (alpha && beta && delta && !gamma)
            {
                grid.Grid[x, y].Frame = 26;
            }
            else if (alpha && beta && delta && gamma)
            {
                grid.Grid[x, y].Frame = 26;
            }
        }
        /// <summary>
        /// Checks if a point is in the grid
        /// </summary>
        /// <param name="x">X value</param>
        /// <param name="y">Y value</param>
        /// <returns>Boolean</returns>
        public bool inDomain(int x, int y)
        {
            if(x >= 0 && x < width)
            {
                if (y >= 0 && y < height)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Check if there is a tile at location
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <returns>Boolean</returns>
        public bool isTile(int x, int y)
        {
            if(inDomain(x, y))
            {
                if(grid.Grid[x, y] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// Tests if a rectangle intersects with any tiles
        /// </summary>
        /// <param name="rect">Rectangle reference</param>
        /// <returns>Boolean</returns>
        public bool intersectRect(Rectangle rect)
        {
            if(rect.Width / tileWidth > 1)
            {
                if(rect.Height / tileHeight > 1)
                {
                    //width and height are greater than one tile
                    for(int x = (int)rect.X / tileWidth; x < (int)rect.X / tileWidth + (int)rect.Width / tileWidth; x++)
                    {
                        for(int y = (int)rect.Y / tileHeight; y < (int)rect.Y / tileHeight + (int)rect.Height / tileHeight; y++)
                        {
                            if(grid.Grid[x, y] != null)
                            {
                                return true;
                            }
                        }
                    }
                }
                else//height is not greater than one tile but width is
                {
                    for(int x = (int)rect.X / tileWidth; x < (int)rect.X / tileWidth + (int)rect.Width / tileWidth; x++)
                    {
                        if(grid.Grid[x, (int)rect.Y / tileHeight] != null)
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                if(rect.Height / tileHeight > 1)
                {
                    //width is not greater than one tile but height is
                    for(int y = (int)rect.Y / tileHeight; y < (int)rect.Y / tileHeight + (int)rect.Height / tileHeight; y++)
                    {
                        if(grid.Grid[(int)rect.X / tileWidth, y] != null)
                        {
                            return true;
                        }
                    }
                }
            }
            //rect is one tile or smaller
            if(grid.Grid[(int)rect.X / tileWidth, (int)rect.Y / tileHeight] != null)
            {
                return true;
            }
            return false;
        }
    }
}
