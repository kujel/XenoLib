using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDL2;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

namespace XenoLib
{
    /// <summary>
    /// ScrollingList2 class
    /// </summary>
    public class ScrollingList2
    {
        //protected
        protected List<string> options;
        protected List<Rectangle> testBoxes;
        protected int start;
        protected int range;
        protected SimpleButton4 up;
        protected SimpleButton4 down;
        protected SimpleButton4 done;
        protected Texture2D pixel;
        protected Rectangle box;
        protected int index;
        protected int boxW;
        protected int boxH;
        protected int width;
        protected int height;
        protected bool active;
        protected int shift;
        protected SDL.SDL_Color white;
        protected SDL.SDL_Color black;
        protected SDL.SDL_Color gray;

        //public
        /// <summary>
        /// ScrollingList2 constructor
        /// </summary>
        /// <param name="dbp">Down button pressed Texture2D reference</param>
        /// <param name="dbd">Down button depressed Texture2D reference</param>
        /// <param name="ubp">Up button pressed Texture2D reference</param>
        /// <param name="ubd">Up button depressed Texture2D reference</param>
        /// <param name="db">Done button Texture2D reference</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="boxW">Box width</param>
        /// <param name="boxH">Box Height</param>
        /// <param name="range">Range of options drawn</param>
        /// <param name="shift">TestBox downward shift</param>
        public ScrollingList2(Texture2D dbp, Texture2D dbd, Texture2D ubp, Texture2D ubd, Texture2D db, int x, int y, int width, int height, int boxW, int boxH, int range, int shift = 0)
        {
            options = new List<string>();
            testBoxes = new List<Rectangle>();
            start = 0;
            this.range = range;
            up = new SimpleButton4(ubd, ubp, x + width, y, "up");
            down = new SimpleButton4(dbp, dbd, x + width, y + height - 32, "down");
            done = new SimpleButton4(db, db, x + width + 32, y, "done");
            box = new Rectangle(x, y, width, height);
            index = 0;
            this.boxW = boxW;
            this.boxH = boxH;
            this.width = width;
            this.height = height;
            active = false;
            this.shift = shift;
            white.r = 255;
            white.g = 255;
            white.b = 255;
            white.a = 1;
            black.r = 0;
            black.g = 0;
            black.b = 0;
            black.a = 1;
            gray.r = 155;
            gray.g = 155;
            gray.b = 155;
            gray.a = 1;
        }
        /// <summary>
        /// Adds an option to ScrollingList2
        /// </summary>
        /// <param name="option">Option as string to add</param>
        public void addOption(string option)
        {
            if (!contains(option))
            {
                options.Add(option);
                testBoxes.Add(new Rectangle(box.X, box.Y + options.Count * boxH - shift, boxW, boxH));
            }
        }
        /// <summary>
        /// Checks if ScrollingList2 contains option
        /// </summary>
        /// <param name="option">Option as string to check</param>
        /// <returns>Boolean</returns>
        public bool contains(string option)
        {
            for (int i = 0; i < options.Count; i++)
            {
                if (options[i] == option)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Updates internal state
        /// </summary>
        /// <param name="state">MBS</param>
        /// <param name="mBox">Rectangle reference</param>
        /// <returns>String</returns>
        public string update(MBS state, Rectangle mBox)
        {
            if (up.click() == "up")
            {
                if (start > 0)
                {
                    start--;
                }
            }
            if (down.click() == "down")
            {
                if (options.Count > range)
                {
                    if (start + range < options.Count)
                    {
                        start++;
                    }
                }
            }
            if (done.click() == "done")
            {
                deactivate();
            }
            up.update();
            down.update();
            done.update();
            if (state == MBS.left)
            {
                if (options.Count == 0)
                {
                    return "";
                }
                if (range < testBoxes.Count)
                {
                    for (int i = start; i < start + range; i++)
                    {
                        if (testBoxes[i - start].intersects(mBox))
                        {
                            index = i;
                            return options[i];
                        }
                    }
                }
                else
                {
                    for (int i = start; i < testBoxes.Count; i++)
                    {
                        if (testBoxes[i - start].intersects(mBox))
                        {
                            index = i;
                            return options[i];
                        }
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// Updates internal state
        /// </summary>
        /// <param name="state">Left mouse button state</param>
        /// <param name="mBox">Rectangle reference</param>
        /// <returns>String</returns>
        public string update(bool state, Rectangle mBox)
        {
            if (up.click() == "up")
            {
                if (start > 0)
                {
                    start--;
                }
            }
            if (down.click() == "down")
            {
                if (options.Count > range)
                {
                    if (start + range < options.Count)
                    {
                        start++;
                    }
                }
            }
            if (done.click() == "done")
            {
                deactivate();
            }
            up.update();
            down.update();
            done.update();
            if (state == true)
            {
                if (options.Count == 0)
                {
                    return "";
                }
                if (range < testBoxes.Count)
                {
                    for (int i = start; i < start + range; i++)
                    {
                        if (testBoxes[i - start].intersects(mBox))
                        {
                            index = i;
                            return options[i];
                        }
                    }
                }
                else
                {
                    for (int i = start; i < testBoxes.Count; i++)
                    {
                        if (testBoxes[i - start].intersects(mBox))
                        {
                            index = i;
                            return options[i];
                        }
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// Draws scrollingList2
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        public void draw(IntPtr renderer)
        {
            DrawRects.drawRect(renderer, box, white, true);
            //sb.Draw(pixel, box, Color.White);
            up.draw(renderer);
            down.draw(renderer);
            done.draw(renderer);
            Point2D temp = new Point2D(box.X, box.Y);
            if (options.Count > range)
            {
                if (start + range > options.Count)
                {
                    for (int i = start; i < options.Count; i++)
                    {
                        temp.Y = testBoxes[i].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], (int)temp.X, (int)temp.Y, black);
                            //sb.DrawString(font, options[i - start], temp, Color.Black);
                        }
                    }
                }
                else
                {
                    for (int i = start; i < start + range; i++)
                    {
                        temp.Y = testBoxes[i - start].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], (int)temp.X, (int)temp.Y, black);
                            //sb.DrawString(font, options[i], temp, Color.Black);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < options.Count; i++)
                {
                    temp.Y = testBoxes[i].Y;
                    if (options[i] != null)
                    {
                        SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                        //sb.DrawString(font, options[i], temp, Color.Black);
                    }
                }
            }
        }
        /// <summary>
        /// Draws scrollingList2
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="colour">Background colour</param>
        public void draw(IntPtr renderer, XENOCOLOURS colour)
        {
            DrawRects.drawRect(renderer, box, ColourBank.getColour(colour), true);
            //sb.Draw(pixel, box, Color.White);
            up.draw(renderer);
            down.draw(renderer);
            done.draw(renderer);
            Point2D temp = new Point2D(box.X, box.Y);
            if (options.Count > range)
            {
                if (start + range > options.Count)
                {
                    for (int i = start; i != options.Count; i++)
                    {
                        temp.Y = testBoxes[i].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], (int)temp.X, (int)temp.Y, black);
                            //sb.DrawString(font, options[i - start], temp, Color.Black);
                        }
                    }
                }
                else
                {
                    for (int i = start; i < start + range; i++)
                    {
                        temp.Y = testBoxes[i - start].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], (int)temp.X, (int)temp.Y, black);
                            //sb.DrawString(font, options[i], temp, Color.Black);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < options.Count; i++)
                {
                    temp.Y = testBoxes[i].Y;
                    if (options[i] != null)
                    {
                        SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                        //sb.DrawString(font, options[i], temp, Color.Black);
                    }
                }
            }
        }
        /// <summary>
        /// Draws scrollingList2
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="colour">Background colour</param>
        /// <param name="scaler">Scaler value</param>
        public void draw(IntPtr renderer, XENOCOLOURS colour, float scaler)
        {
            DrawRects.drawRect(renderer, box, ColourBank.getColour(colour), true);
            //sb.Draw(pixel, box, Color.White);
            up.draw(renderer);
            down.draw(renderer);
            done.draw(renderer);
            Point2D temp = new Point2D(box.X, box.Y);
            if (options.Count > range)
            {
                if (start + range > options.Count)
                {
                    for (int i = start; i != options.Count; i++)
                    {
                        temp.Y = testBoxes[i].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], (int)temp.X, (int)temp.Y, black, scaler);
                            //sb.DrawString(font, options[i - start], temp, Color.Black);
                        }
                    }
                }
                else
                {
                    for (int i = start; i < start + range; i++)
                    {
                        temp.Y = testBoxes[i - start].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], (int)temp.X, (int)temp.Y, black, scaler);
                            //sb.DrawString(font, options[i], temp, Color.Black);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < options.Count; i++)
                {
                    temp.Y = testBoxes[i].Y;
                    if (options[i] != null)
                    {
                        SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black, scaler);
                        //sb.DrawString(font, options[i], temp, Color.Black);
                    }
                }
            }
        }
        /// <summary>
        /// Draws scrollingList2
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="colour">Background colour</param>
        /// <param name="textColour">Text colour</param>
        /// <param name="scaler">Scaler value</param>
        public void draw(IntPtr renderer, XENOCOLOURS colour, XENOCOLOURS textColour, float scaler)
        {
            DrawRects.drawRect(renderer, box, ColourBank.getColour(colour), true);
            //sb.Draw(pixel, box, Color.White);
            up.draw(renderer);
            down.draw(renderer);
            done.draw(renderer);
            Point2D temp = new Point2D(box.X, box.Y);
            if (options.Count > range)
            {
                if (start + range > options.Count)
                {
                    for (int i = start; i != options.Count; i++)
                    {
                        temp.Y = testBoxes[i].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], (int)temp.X, (int)temp.Y, ColourBank.getColour(textColour), scaler);
                            //sb.DrawString(font, options[i - start], temp, Color.Black);
                        }
                    }
                }
                else
                {
                    for (int i = start; i < start + range; i++)
                    {
                        temp.Y = testBoxes[i - start].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], (int)temp.X, (int)temp.Y, ColourBank.getColour(textColour), scaler);
                            //sb.DrawString(font, options[i], temp, Color.Black);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < options.Count; i++)
                {
                    temp.Y = testBoxes[i].Y;
                    if (options[i] != null)
                    {
                        SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, ColourBank.getColour(textColour), scaler);
                        //sb.DrawString(font, options[i], temp, Color.Black);
                    }
                }
            }
        }
        /// <summary>
        /// Draws scrollingList2 (without buttons)
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        public void drawNoButtons(IntPtr renderer)
        {
            DrawRects.drawRect(renderer, box, white, true);
            //sb.Draw(pixel, box, Color.White);
            //up.draw(sb);
            //down.draw(sb);
            //done.draw(sb);
            Point2D temp = new Point2D(box.X, box.Y);
            if (options.Count >= range)
            {
                if (start + range >= options.Count)
                {
                    for (int i = start; i < options.Count; i++)
                    {
                        temp.Y = testBoxes[i].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                            //sb.DrawString(font, options[i - start], temp, Color.Black);
                        }
                    }
                }
                else
                {
                    for (int i = start; i < start + range; i++)
                    {
                        temp.Y = testBoxes[i - start].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                            //sb.DrawString(font, options[i], temp, Color.Black);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < options.Count; i++)
                {
                    temp.Y = testBoxes[i].Y;
                    if (options[i] != null)
                    {
                        SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                        //sb.DrawString(font, options[i], temp, Color.Black);
                    }
                }
            }
        }
        /// <summary>
        /// Draws scrollingList2 (without buttons)
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="colour">Background colour</param>
        public void drawNoButtons(IntPtr renderer, XENOCOLOURS colour)
        {
            DrawRects.drawRect(renderer, box, ColourBank.getColour(colour), true);
            //sb.Draw(pixel, box, Color.White);
            //up.draw(sb);
            //down.draw(sb);
            //done.draw(sb);
            Point2D temp = new Point2D(box.X, box.Y);
            if (options.Count >= range)
            {
                if (start + range >= options.Count)
                {
                    for (int i = start; i < options.Count; i++)
                    {
                        temp.Y = testBoxes[i].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                            //sb.DrawString(font, options[i - start], temp, Color.Black);
                        }
                    }
                }
                else
                {
                    for (int i = start; i < start + range; i++)
                    {
                        temp.Y = testBoxes[i - start].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                            //sb.DrawString(font, options[i], temp, Color.Black);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < options.Count; i++)
                {
                    temp.Y = testBoxes[i].Y;
                    if (options[i] != null)
                    {
                        SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                        //sb.DrawString(font, options[i], temp, Color.Black);
                    }
                }
            }
        }
        /// <summary>
        /// Draws scrollingList2 (without buttons)
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="colour">Background colour</param>
        /// <param name="scaler">Scaler value</param>
        public void drawNoButtons(IntPtr renderer, XENOCOLOURS colour, float scaler)
        {
            DrawRects.drawRect(renderer, box, ColourBank.getColour(colour), true);
            //sb.Draw(pixel, box, Color.White);
            //up.draw(sb);
            //down.draw(sb);
            //done.draw(sb);
            Point2D temp = new Point2D(box.X, box.Y);
            if (options.Count >= range)
            {
                if (start + range >= options.Count)
                {
                    for (int i = start; i < options.Count; i++)
                    {
                        temp.Y = testBoxes[i].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black, scaler);
                            //sb.DrawString(font, options[i - start], temp, Color.Black);
                        }
                    }
                }
                else
                {
                    for (int i = start; i < start + range; i++)
                    {
                        temp.Y = testBoxes[i - start].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black, scaler);
                            //sb.DrawString(font, options[i], temp, Color.Black);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < options.Count; i++)
                {
                    temp.Y = testBoxes[i].Y;
                    if (options[i] != null)
                    {
                        SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black, scaler);
                        //sb.DrawString(font, options[i], temp, Color.Black);
                    }
                }
            }
        }
        /// <summary>
        /// Draws scrollingList2 (with arrow buttons but no done button)
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        public void drawArrows(IntPtr renderer)
        {
            DrawRects.drawRect(renderer, box, white, true);
            //sb.Draw(pixel, box, Color.White);
            up.draw(renderer);
            down.draw(renderer);
            //done.draw(sb);
            Point2D temp = new Point2D (box.X, box.Y);
            if (options.Count > range)
            {
                if (start + range > options.Count)
                {
                    for (int i = start; i < options.Count; i++)
                    {
                        temp.Y = testBoxes[i].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                            //sb.DrawString(font, options[i - start], temp, Color.Black);
                        }
                    }
                }
                else
                {
                    for (int i = start; i < start + range; i++)
                    {
                        temp.Y = testBoxes[i - start].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                            //sb.DrawString(font, options[i], temp, Color.Black);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < options.Count; i++)
                {
                    temp.Y = testBoxes[i].Y;
                    if (options[i] != null)
                    {
                        SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                        //sb.DrawString(font, options[i], temp, Color.Black);
                    }
                }
            }
        }
        /// <summary>
        /// Draws scrollingList2 (with arrow buttons but no done button)
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="colour">Background colour</param>
        public void drawArrows(IntPtr renderer, XENOCOLOURS colour)
        {
            DrawRects.drawRect(renderer, box, ColourBank.getColour(colour), true);
            //sb.Draw(pixel, box, Color.White);
            up.draw(renderer);
            down.draw(renderer);
            //done.draw(sb);
            Point2D temp = new Point2D(box.X, box.Y);
            if (options.Count > range)
            {
                if (start + range > options.Count)
                {
                    for (int i = start; i < options.Count; i++)
                    {
                        temp.Y = testBoxes[i].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                            //sb.DrawString(font, options[i - start], temp, Color.Black);
                        }
                    }
                }
                else
                {
                    for (int i = start; i < start + range; i++)
                    {
                        temp.Y = testBoxes[i - start].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                            //sb.DrawString(font, options[i], temp, Color.Black);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < options.Count; i++)
                {
                    temp.Y = testBoxes[i].Y;
                    if (options[i] != null)
                    {
                        SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black);
                        //sb.DrawString(font, options[i], temp, Color.Black);
                    }
                }
            }
        }
        /// <summary>
        /// Draws scrollingList2 (with arrow buttons but no done button)
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="colour">Background colour</param>
        /// <param name="scaler">Scaler value</param>
        public void drawArrows(IntPtr renderer, XENOCOLOURS colour, float scaler)
        {
            DrawRects.drawRect(renderer, box, ColourBank.getColour(colour), true);
            //sb.Draw(pixel, box, Color.White);
            up.draw(renderer);
            down.draw(renderer);
            //done.draw(sb);
            Point2D temp = new Point2D(box.X, box.Y);
            if (options.Count > range)
            {
                if (start + range > options.Count)
                {
                    for (int i = start; i < options.Count; i++)
                    {
                        temp.Y = testBoxes[i].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black, scaler);
                            //sb.DrawString(font, options[i - start], temp, Color.Black);
                        }
                    }
                }
                else
                {
                    for (int i = start; i < start + range; i++)
                    {
                        temp.Y = testBoxes[i - start].Y;
                        if (options[i] != null)
                        {
                            SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black, scaler);
                            //sb.DrawString(font, options[i], temp, Color.Black);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < options.Count; i++)
                {
                    temp.Y = testBoxes[i].Y;
                    if (options[i] != null)
                    {
                        SimpleFont.DrawString(renderer, options[i], temp.X, temp.Y, black, scaler);
                        //sb.DrawString(font, options[i], temp, Color.Black);
                    }
                }
            }
        }
        /// <summary>
        /// Activates ScrollingList2
        /// </summary>
        public void activate()
        {
            active = true;
            start = 0;
        }
        /// <summary>
        /// Deactivates ScrollingList2
        /// </summary>
        public void deactivate()
        {
            active = false;
        }
        /// <summary>
        /// Updates all ScrollingList2 boxes
        /// </summary>
        public void updateBoxes()
        {
            for (int i = 0; i < testBoxes.Count; i++)
            {
                testBoxes[i] = new Rectangle(box.X, box.Y + i * boxH, boxW, boxH);
            }
            up.X = box.X + width;
            up.Y = box.Y;
            down.X = box.X + width;
            down.Y = box.Y + height - boxH;
            done.X = box.X + width + 32;
            done.Y = box.Y;
        }
        /// <summary>
        /// Clear all contests of ScrollingList2
        /// </summary>
        public void Clear()
        {
            options.Clear();
            testBoxes.Clear();
            start = 0;
        }
        /// <summary>
        /// Sets position of ScrollingList2
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setPos(int x, int y)
        {
            box.X = x;
            box.Y = y;
            updateBoxes();
        }
        /// <summary>
        /// Sets size of ScrollingList2
        /// </summary>
        /// <param name="w">width</param>
        /// <param name="h">height</param>
        public void setSize(int w, int h)
        {
            box.Width = w;
            width = w;
            box.Height = h;
            height = h;
            up.X = box.X + width;
            up.Y = box.Y;
            down.X = box.X + width;
            down.Y = box.Y + height - 32;
            done.X = box.X + width + 32;
            done.Y = box.Y;
        }
        /// <summary>
        /// Active property
        /// </summary>
        public bool Active
        {
            get { return active; }
        }
        /// <summary>
        /// X property
        /// </summary>
        public float X
        {
            get { return box.X; }
            set { box.X = value; }
        }
        /// <summary>
        /// Y property
        /// </summary>
        public float Y
        {
            get { return box.Y; }
            set { box.Y = value; }
        }
        /// <summary>
        /// Index property
        /// </summary>
        public int Index
        {
            get { return index; }
        }
        /// <summary>
        /// Start property
        /// </summary>
        public int Start
        {
            get { return start; }
            set {
                    start = value;
                    if (start + range >= options.Count)
                    {
                        start = options.Count - (range + 1);
                    }
                    if (start < 0)
                        {
                            start = 0;
                        }
                    }
        }
        /// <summary>
        /// Count property
        /// </summary>
        public int Count
        {
            get { return options.Count; }
        }
        /// <summary>
        /// Range property
        /// </summary>
        public int Range
        {
            get { return range; }
            set {
                    range = value;
                    if(range < 0)
                    {
                        range = 1;
                    }
                    if(range > options.Count)
                    {
                        range = options.Count;
                    }
                }
        }
    }
}
