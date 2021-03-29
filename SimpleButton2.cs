using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDL2;

namespace XenoLib
{
    /// <summary>
    /// SimpleButton2 class
    /// </summary>
    public class SimpleButton2
    {
        //protected
        protected Texture2D depressed;
        protected Texture2D pressed;
        protected Rectangle box;
        protected Rectangle box2;
        protected string name;
        protected CoolDown cool;
        protected SDL.SDL_Color black;

        //public
        /// <summary>
        /// SimpleButton2 constructor
        /// </summary>
        /// <param name="depressed">Texture2D reference</param>
        /// <param name="pressed">Texture2D reference</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="name">Button name</param>
        public SimpleButton2(Texture2D depressed, Texture2D pressed, int x, int y, string name)
        {
            this.depressed = depressed;
            this.pressed = pressed;
            this.name = name;
            box = new Rectangle(0, 0, depressed.width, depressed.height);
            box2 = new Rectangle(x, y, depressed.width, depressed.height);
            cool = new CoolDown(7);
            black.r = 0;
            black.g = 0;
            black.b = 0;
            black.a = 1;
        }
        /// <summary>
        /// Updates internal state
        /// </summary>
        public void update()
        {
            cool.update();
        }
        /// <summary>
        /// Checks if button was pressed
        /// </summary>
        /// <param name="mBox">Rectangle reference</param>
        /// <param name="state">MBS</param>
        /// <returns>String</returns>
        public string click(Rectangle mBox, MBS state)
        {
            if (state == MBS.left)
            {
                if (!cool.Active)
                {
                    if (box.intersects(mBox))
                    {
                        cool.activate();
                        return name;
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// Draws SimpleButton2
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        public void draw(IntPtr renderer)
        {
            if (cool.Active)
            {
                SimpleDraw.draw(renderer, pressed, box, box2);
                //sb.Draw(pressed, box, Color.White);
            }
            else
            {
                SimpleDraw.draw(renderer, depressed, box, box2);
                //sb.Draw(depressed, box, Color.White);
            }
        }
        /// <summary>
        /// Draws SimpleButton2 name over button
        /// </summary>
        /// <param name="renderer"></param>
        public void darwName(IntPtr renderer)
        {
            SimpleFont.DrawString(renderer, name, (int)box2.X + 2, (int)box2.Y + 2, black);
            //sb.DrawString(font, name, new Vector2(X + 2, Y + 2), Color.White);
        }
        /// <summary>
        /// Name property
        /// </summary>
        public string Name
        {
            get { return name; }
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
    }
    /// <summary>
    /// SimpleButton3 class
    /// </summary>
    public class SimpleButton3
    {
        //protected
        protected Texture2D depressed;
        protected Texture2D pressed;
        protected Rectangle box;
        protected Rectangle box2;
        protected string name;
        protected CoolDown cool;
        protected SDL.SDL_Color black;

        //public
        /// <summary>
        /// SimpleButton3 contsructor
        /// </summary>
        /// <param name="depressed">Texture2D reference</param>
        /// <param name="pressed">Texture2D reference</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="name">Name of button</param>
        public SimpleButton3(Texture2D depressed, Texture2D pressed, int x, int y, string name)
        {
            this.depressed = depressed;
            this.pressed = pressed;
            this.name = name;
            box = new Rectangle(0, 0, depressed.width, depressed.height);
            box2 = new Rectangle(x, y, depressed.width, depressed.height);
            cool = new CoolDown(7);
            black.r = 0;
            black.g = 0;
            black.b = 0;
            black.a = 1;
        }
        /// <summary>
        /// Updates internal state
        /// </summary>
        public void update()
        {
            cool.update();
        }
        /// <summary>
        /// Checks if button clicked
        /// </summary>
        /// <param name="mBox">Rectangle reference</param>
        /// <param name="eve">SDL.EVent reference</param>
        /// <returns>String</returns>
        public string click(Rectangle mBox, SDL.SDL_Event eve)
        {
            if (eve.button.state == SDL.SDL_BUTTON_LEFT)
            {
                if (!cool.Active)
                {
                    if (box.intersects(mBox))
                    {
                        cool.activate();
                        return name;
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// Draws SimpleButton3
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        public void draw(IntPtr renderer)
        {
            if (cool.Active)
            {
                SimpleDraw.draw(renderer, pressed, box, box2);
                //sb.Draw(pressed, box, Color.White);
            }
            else
            {
                SimpleDraw.draw(renderer, depressed, box, box2);
                //sb.Draw(depressed, box, Color.White);
            }
        }
        /// <summary>
        /// Draws SimpleButton3 name over button
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        public void darwName(IntPtr renderer)
        {
            SimpleFont.DrawString(renderer, name, box2.X, box2.Y, black);
            //sb.DrawString(font, name, new Vector2(X + 2, Y + 2), Color.White);
        }
        /// <summary>
        /// Name property
        /// </summary>
        public string Name
        {
            get { return name; }
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
    }
    /// <summary>
    /// SimpleButton4 class
    /// </summary>
    public class SimpleButton4
    {
        //protected
        protected Texture2D depressed;
        protected Texture2D pressed;
        protected Rectangle box;
        protected Rectangle box2;
        protected string name;
        protected CoolDown cool;
        protected SDL.SDL_Color black;

        //public
        /// <summary>
        /// SimpleButton4 contsructor
        /// </summary>
        /// <param name="depressed">Texture2D reference</param>
        /// <param name="pressed">Texture2D reference</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="name">Name of button</param>
        /// <param name="name">InputDelay value</param>
        public SimpleButton4(Texture2D depressed, Texture2D pressed, int x, int y, string name, int inputDelay = 10)
        {
            this.depressed = depressed;
            this.pressed = pressed;
            this.name = name;
            box = new Rectangle(0, 0, depressed.width, depressed.height);
            box2 = new Rectangle(x, y, depressed.width, depressed.height);
            cool = new CoolDown(inputDelay);
            black.r = 0;
            black.g = 0;
            black.b = 0;
            black.a = 1;
        }
        /// <summary>
        /// Updates internal state
        /// </summary>
        public void update()
        {
            cool.update();
        }
        /// <summary>
        /// Checks if button clicked
        /// </summary>
        /// <returns>String</returns>
        public string click()
        {
            Rectangle mBox = new Rectangle(MouseHandler.getMouseX(), MouseHandler.getMouseY(), 2, 2);
            if (MouseHandler.getLeft())
            {
                if (!cool.Active)
                {
                    if (box2.intersects(mBox))
                    {
                        cool.activate();
                        return name;
                    }
                }
            }
            return "";
        }
        /// <summary>
        /// Draws SimpleButton4
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        public void draw(IntPtr renderer)
        {
            if (cool.Active)
            {
                SimpleDraw.draw(renderer, pressed, box, box2);
                //sb.Draw(pressed, box, Color.White);
            }
            else
            {
                SimpleDraw.draw(renderer, depressed, box, box2);
                //sb.Draw(depressed, box, Color.White);
            }
        }
        /// <summary>
        /// Draws SimpleButton3 name over button
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="center">Center name flag</param>
        public void darwName(IntPtr renderer, bool center = true)
        {
            if (center == true)
            {
                int mid = SimpleFont.stringRenderWidth(name, 1) / 2;
                SimpleFont.DrawString(renderer, name, box2.X + ((box2.Width / 2) - mid), box2.Y, 1);
                //sb.DrawString(font, name, new Vector2(X + 2, Y + 2), Color.White);
            }
            else
            {
                SimpleFont.DrawString(renderer, name, box2.X, box2.Y, 1);
            }
        }
        /// <summary>
        /// Draws SimpleButton4 name over button
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        public void drawName(IntPtr renderer)
        {
            int mid = SimpleFont.stringRenderWidth(name, 1) / 2;
            SimpleFont.DrawString(renderer, name, box2.X + ((box2.Width / 2) - mid), box2.Y, 1);//sb.DrawString(font, name, new Vector2(X + 2, Y + 2), Color.White);
        }
        /// <summary>
        /// Resets button state
        /// </summary>
        public void reset()
        {
            cool.reset();
        }
        /// <summary>
        /// Name property
        /// </summary>
        public string Name
        {
            get { return name; }
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
    }
}
