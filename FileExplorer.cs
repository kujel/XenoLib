using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDL2;

namespace XenoLib
{
    /// <summary>
    /// FileTtype enumerator
    /// </summary>
    public enum fileType { dir = 0, png, wav, mp3, txt, none }
    /// <summary>
    /// FileExplorer class
    /// </summary>
    public class FileExplorer
    {
        //protected
        protected Point2D pos;
        protected Rectangle box;
        protected ScrollingList2 fileList;
        protected List<SimpleButton4> buttons;
        protected bool done;
        protected List<string> files;
        protected string path;
        protected CoolDown cool;
        protected SDL.SDL_Color white;
        protected SDL.SDL_Color black;
        protected SDL.SDL_Color red;
        protected SDL.SDL_Color magenta;
        protected Rectangle highLightBox;
        protected Rectangle boarder;
        protected Rectangle mBox;

        //public
        /// <summary>
        /// FileExplorer constructor
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="downButtonPressed">Texture2D reference</param>
        /// <param name="downButtonDepressed">Texture2D reference</param>
        /// <param name="upButtonPressed">Texture2D reference</param>
        /// <param name="upButtonDepressed">Texture2D reference</param>
        /// <param name="backButtonPressed">Texture2D reference</param>
        /// <param name="backButtonDepressed">Texture2D reference</param>
        /// <param name="exitButton">Texture2D reference</param>
        /// <param name="size">Number of items displayed</param>
        public FileExplorer(int x, int y, int w, int h, Texture2D downButtonPressed, Texture2D downButtonDepressed, Texture2D upButtonPressed, Texture2D upButtonDepressed, Texture2D backButtonPressed, Texture2D backButtonDepressed, Texture2D exitButton, int size = 9)
        {
            pos = new Point2D(x, y);
            box = new Rectangle(x, y, w, h);
            fileList = new ScrollingList2(downButtonPressed, downButtonDepressed, upButtonPressed, upButtonDepressed, exitButton, x, y, w, h, w, 32, size, 32);
            buttons = new List<SimpleButton4>();
            buttons.Add(new SimpleButton4(backButtonPressed, backButtonDepressed, (int)pos.X + w - 64, (int)pos.Y + h, "back"));
            //buttons.Add(new SimpleButton2(TextureBank.getTexture("bbp"), TextureBank.getTexture("bbd"), pos.X + w - 160, pos.Y + h + 32, "done"));
            done = false;
            files = new List<string>();
            path = AppDomain.CurrentDomain.BaseDirectory;
            string[] fileNames = System.IO.Directory.GetFiles(path);
            for (int i = 0; i < fileNames.Length; i++)
            {
                char[] spliter = { '\\' };
                string[] temp2 = StringParser.parse(fileNames[i], spliter);
                fileList.addOption(temp2[temp2.Length - 1]);
            }
            cool = new CoolDown(12);
            //delay = new CoolDown(15);
            white.r = 255;
            white.g = 255;
            white.b = 255;
            white.a = 1;
            black.r = 0;
            black.g = 0;
            black.b = 0;
            black.a = 1;
            red.r = 255;
            red.g = 0;
            red.b = 0;
            red.a = 1;
            magenta.r = 255;
            magenta.g = 0;
            magenta.b = 255;
            magenta.a = 1;
            highLightBox = new Rectangle(x, y, w, 32);
            boarder = new Rectangle(x, y, w, 32 * size);
            mBox = new Rectangle(0, 0, 2, 2);
        }
        /// <summary>
        /// Updates internal state
        /// </summary>
        /// <param name="cursor">SimpleCursor reference</param>
        /// <param name="projectName">Project name</param>
        /// <param name="renderer">Tenderer reference</param>
        /// <param name="textureWidth">Texture width (for loading)</param>
        /// <param name="textureHeight">Texture height (for loading)</param>
        public void update(SimpleCursor cursor, string projectName, IntPtr renderer, int textureWidth = 128, int textureHeight = 128)
        {
            string temp = "";
            if (!cool.Active)
            {
                temp = fileList.update(cursor.getMBS(), cursor.Box);
            }
            if (temp != "")
            {
                cool.activate();
                if (System.IO.Directory.Exists(temp))
                {
                    temp = path + temp;
                }
                else
                {
                    temp = path + "\\" + temp;
                }
                if (System.IO.Directory.Exists(temp))
                {
                    try
                    {
                        path = temp;
                        fileList.Clear();
                        string[] fileNames = mergeLists(System.IO.Directory.GetFiles(temp), System.IO.Directory.GetDirectories(temp));
                        for (int i = 0; i < fileNames.Length; i++)
                        {
                            char[] spliter = { '\\' };
                            string[] temp2 = StringParser.parse(fileNames[i], spliter);
                            fileList.addOption(temp2[temp2.Length - 1]);
                        }
                    } catch(Exception)
                    {
                        char[] spliter = { '\\' };
                        string[] temp2 = StringParser.parse(path, spliter);
                        path = buildPath(temp2, temp2.Length - 2);
                    }
                }
                else
                {
                    char[] spliter = { '.' };
                    string[] temp2 = StringParser.parse(temp, spliter);
                    string[] temp3;
                    switch (temp2[temp2.Length - 1])
                    {
                        case "png":
                            spliter = new char[] { '\\' };
                            temp3 = StringParser.parse(temp2[0], spliter);
                            TextureBank.addTexture(temp3[temp3.Length - 1], TextureLoader.load(temp, renderer, magenta, textureWidth, textureHeight));
                            System.IO.File.Copy(temp, AppDomain.CurrentDomain.BaseDirectory + projectName + "\\graphics\\" + temp3[temp3.Length - 1] + ".png", true);
                            break;
                        case "wav":
                            spliter = new char[] { '\\' };
                            temp3 = StringParser.parse(temp2[0], spliter);
                            SFXBank.addSFX(temp3[temp3.Length - 1], AppDomain.CurrentDomain.BaseDirectory + projectName + "\\audio\\" + temp3[temp3.Length - 1] + ".wav");
                            System.IO.File.Copy(temp, AppDomain.CurrentDomain.BaseDirectory + projectName + "\\audio\\" + temp3[temp3.Length - 1] + ".wav", true);
                            break;
                        case "mp3":
                            spliter = new char[] { '\\' };
                            temp3 = StringParser.parse(temp2[0], spliter);
                            //MP3Bank.addSong(temp3[temp3.Length-1], new System.IO.FileStream(temp, System.IO.FileMode.Open));
                            MP3Bank.addSong(temp3[0], AppDomain.CurrentDomain.BaseDirectory + projectName + "\\audio\\" + temp3[temp3.Length - 1] + ".mp3");
                            System.IO.File.Copy(temp, AppDomain.CurrentDomain.BaseDirectory + projectName + "\\audio\\" + temp3[temp3.Length - 1] + ".mp3", true);
                            break;
                        case "txt":
                            spliter = new char[] { '\\' };
                            temp3 = StringParser.parse(temp2[0], spliter);
                            System.IO.File.Copy(temp, AppDomain.CurrentDomain.BaseDirectory + projectName + "\\data\\" + temp3[temp3.Length - 1] + ".txt", true);
                            break;
                    }
                }
            }
            string temp4 = "";
            for (int i = 0; i < buttons.Count; i++)
            {
                temp4 = buttons[i].click();
                if(temp4 != "")
                {
                    switch (temp4)
                    {
                        case "done":
                            done = true;
                            break;
                        case "back":
                            char[] spliter = { '\\' };
                            string[] temp5 = StringParser.parse(path, spliter);
                            if (temp5.Length > 1)
                            {
                                path = buildPath(temp5, temp5.Length - 2);
                                /*
                                for (int k = 0; k < temp5.Length - 2; k++)
                                {
                                    path += temp5[k];
                                    path += '\\';
                                }
                                 */
                            }
                            if (path == "" || path == " ")
                            {
                                break;
                            }
                            fileList.Clear();
                            string[] fileNames = mergeLists(System.IO.Directory.GetFiles(path), System.IO.Directory.GetDirectories(path));
                            for (int v = 0; v < fileNames.Length; v++)
                            {
                                char[] spliter2 = { '\\' };
                                string[] temp2 = StringParser.parse(fileNames[v], spliter2);
                                fileList.addOption(temp2[temp2.Length - 1]);
                            }
                            break;
                    }
                    break;
                }
            }
            cool.update();
            buttons[0].update();
            //buttons[1].update();
        }
        /// <summary>
        /// Updates internal state
        /// </summary>
        /// <param name="cursor">SimplePointer reference</param>
        /// <param name="projectName">Project name</param>
        /// <param name="renderer">Tenderer reference</param>
        /// <param name="textureWidth">Texture width (for loading)</param>
        /// <param name="textureHeight">Texture height (for loading)</param>
        public void update2(SimplePointer cursor, string projectName, IntPtr renderer, int textureWidth = 128, int textureHeight = 128)
        {
            string temp = "";
            if (!cool.Active)
            {
                mBox.X = MouseHandler.getMouseX();
                mBox.Y = MouseHandler.getMouseY();
                temp = fileList.update(MouseHandler.getLeft(), mBox);
            }
            if (temp != "")
            {
                cool.activate();
                if (System.IO.Directory.Exists(temp))
                {
                    temp = path + temp;
                }
                else
                {
                    temp = path + "\\" + temp;
                }
                if (System.IO.Directory.Exists(temp))
                {
                    try
                    {
                        path = temp;
                        fileList.Clear();
                        string[] fileNames = mergeLists(System.IO.Directory.GetFiles(temp), System.IO.Directory.GetDirectories(temp));
                        for (int i = 0; i < fileNames.Length; i++)
                        {
                            char[] spliter = { '\\' };
                            string[] temp2 = StringParser.parse(fileNames[i], spliter);
                            fileList.addOption(temp2[temp2.Length - 1]);
                        }
                    }
                    catch (Exception)
                    {
                        char[] spliter = { '\\' };
                        string[] temp2 = StringParser.parse(path, spliter);
                        path = buildPath(temp2, temp2.Length - 2);
                    }
                }
                else
                {
                    char[] spliter = { '.' };
                    string[] temp2 = StringParser.parse(temp, spliter);
                    string[] temp3;
                    switch (temp2[temp2.Length - 1])
                    {
                        case "png":
                            spliter = new char[] { '\\' };
                            temp3 = StringParser.parse(temp2[0], spliter);
                            TextureBank.addTexture(temp3[temp3.Length - 1], TextureLoader.load(temp, renderer, magenta, textureWidth, textureHeight));
                            System.IO.File.Copy(temp, AppDomain.CurrentDomain.BaseDirectory + projectName + "\\graphics\\" + temp3[temp3.Length - 1] + ".png", true);
                            break;
                        case "wav":
                            spliter = new char[] { '\\' };
                            temp3 = StringParser.parse(temp2[0], spliter);
                            SFXBank.addSFX(temp3[temp3.Length - 1], AppDomain.CurrentDomain.BaseDirectory + projectName + "\\audio\\" + temp3[temp3.Length - 1] + ".wav");
                            System.IO.File.Copy(temp, AppDomain.CurrentDomain.BaseDirectory + projectName + "\\audio\\" + temp3[temp3.Length - 1] + ".wav", true);
                            break;
                        case "mp3":
                            spliter = new char[] { '\\' };
                            temp3 = StringParser.parse(temp2[0], spliter);
                            //MP3Bank.addSong(temp3[temp3.Length-1], new System.IO.FileStream(temp, System.IO.FileMode.Open));
                            MP3Bank.addSong(temp3[0], AppDomain.CurrentDomain.BaseDirectory + projectName + "\\audio\\" + temp3[temp3.Length - 1] + ".mp3");
                            System.IO.File.Copy(temp, AppDomain.CurrentDomain.BaseDirectory + projectName + "\\audio\\" + temp3[temp3.Length - 1] + ".mp3", true);
                            break;
                        case "txt":
                            spliter = new char[] { '\\' };
                            temp3 = StringParser.parse(temp2[0], spliter);
                            System.IO.File.Copy(temp, AppDomain.CurrentDomain.BaseDirectory + projectName + "\\data\\" + temp3[temp3.Length - 1] + ".txt", true);
                            break;
                    }
                }
            }
            string temp4 = "";
            for (int i = 0; i < buttons.Count; i++)
            {
                temp4 = buttons[i].click();
                if (temp4 != "")
                {
                    switch (temp4)
                    {
                        case "done":
                            done = true;
                            break;
                        case "back":
                            char[] spliter = { '\\' };
                            string[] temp5 = StringParser.parse(path, spliter);
                            if (temp5.Length > 1)
                            {
                                path = buildPath(temp5, temp5.Length - 2);
                                /*
                                for (int k = 0; k < temp5.Length - 2; k++)
                                {
                                    path += temp5[k];
                                    path += '\\';
                                }
                                 */
                            }
                            if (path == "" || path == " ")
                            {
                                break;
                            }
                            fileList.Clear();
                            string[] fileNames = mergeLists(System.IO.Directory.GetFiles(path), System.IO.Directory.GetDirectories(path));
                            for (int v = 0; v < fileNames.Length; v++)
                            {
                                char[] spliter2 = { '\\' };
                                string[] temp2 = StringParser.parse(fileNames[v], spliter2);
                                fileList.addOption(temp2[temp2.Length - 1]);
                            }
                            break;
                    }
                    break;
                }
            }
            cool.update();
            buttons[0].update();
            //buttons[1].update();
        }
        /// <summary>
        /// Draws FileExplorer
        /// </summary>
        /// <param name="renderer">Renderer</param>
        public void draw(IntPtr renderer)
        {
            fileList.draw(renderer);
            buttons[0].draw(renderer);
            buttons[0].darwName(renderer);
            //sb.DrawString(font, buttons[0].Name, new Vector2(buttons[0].X + 3, buttons[0].Y + 3), Color.White);
            //buttons[1].draw(sb);
            //sb.DrawString(font, buttons[1].Name, new Vector2(buttons[1].X + 3, buttons[1].Y + 3), Color.White);
            char[] spliter = { '\\' };
            string[] temp = StringParser.parse(path, spliter);
            if (temp.Length >= 2)
            {
                SimpleFont.DrawString(renderer, temp[temp.Length - 2], (int)pos.X, (int)pos.Y - 32, 1);
                //sb.DrawString(font, temp[temp.Length - 2], new Vector2(pos.X, pos.Y - 32), colour);
            }
            if (cool.Active)
            {
                highLightBox.Y = fileList.Y + 32 * (fileList.Index - fileList.Start);
                DrawRects.drawRect(renderer, highLightBox, red, false);
                //Line.drawSquare(sb, pixel, fileList.X, fileList.Y + 32 * (fileList.Index - fileList.Start), 240, 32, Color.Red, 2);
            }
            DrawRects.drawRect(renderer, boarder, white, false);
        }
        /// <summary>
        /// Draws FileExplorer
        /// </summary>
        /// <param name="renderer">Renderer</param>
        /// <param name="colour">Background colour</param>
        public void draw(IntPtr renderer, XENOCOLOURS colour)
        {
            fileList.draw(renderer, colour);
            buttons[0].draw(renderer);
            buttons[0].darwName(renderer);
            //sb.DrawString(font, buttons[0].Name, new Vector2(buttons[0].X + 3, buttons[0].Y + 3), Color.White);
            //buttons[1].draw(sb);
            //sb.DrawString(font, buttons[1].Name, new Vector2(buttons[1].X + 3, buttons[1].Y + 3), Color.White);
            char[] spliter = { '\\' };
            string[] temp = StringParser.parse(path, spliter);
            if (temp.Length >= 2)
            {
                SimpleFont.DrawString(renderer, temp[temp.Length - 2], (int)pos.X, (int)pos.Y - 32, 1);
                //sb.DrawString(font, temp[temp.Length - 2], new Vector2(pos.X, pos.Y - 32), colour);
            }
            if (cool.Active)
            {
                highLightBox.Y = fileList.Y + 32 * (fileList.Index - fileList.Start);
                DrawRects.drawRect(renderer, highLightBox, red, false);
                //Line.drawSquare(sb, pixel, fileList.X, fileList.Y + 32 * (fileList.Index - fileList.Start), 240, 32, Color.Red, 2);
            }
            DrawRects.drawRect(renderer, boarder, white, false);
        }
        /// <summary>
        /// Returns an array of strings containing the file names in a provided directory path
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>String array</returns>
        public string[] getContents(string filePath)
        {
            string[] t1 = System.IO.Directory.GetFiles(filePath);
            string[] t2 = System.IO.Directory.GetDirectories(filePath);
            return mergeLists(t1, t2);
        }
        /// <summary>
        /// Returns an arrary containing file names minus paths and extintions
        /// </summary>
        /// <param name="filePath">File path to search</param>
        /// <returns>String arrary</returns>
        public string[] getNames(string filePath)
        {
            string[] tmp1 = getContents(filePath);
            string[] tmp2 = new string[tmp1.Length];
            char[] slash = { '\\' };
            char[] dot = { '.' };
            for (int i = 0; i < tmp1.Length; i++)
            {
                tmp2[i] = StringParser.doubleParse(tmp1[i], slash, dot)[tmp1.Length - 1][0];
            }
            return tmp2;
        }
        /// <summary>
        /// Returns a limited number of accepted file type values provided a file path
        /// </summary>
        /// <param name="pathName">File path</param>
        /// <returns>FileType enumeration</returns>
        public fileType getType(string pathName)
        {
            if (System.IO.Directory.Exists(pathName))
            {
                return fileType.dir;
            }
            char[] splitter = {'.'};
            string[] temp = StringParser.parse(pathName, splitter);
            string ft = temp[temp.Length - 1];
            switch(ft)
            {
                case "png":
                    return fileType.png;
                case "wav":
                    return fileType.wav;
                case "mp3":
                    return fileType.mp3;
                case "txt":
                    return fileType.txt;
            }
            return fileType.none;
        }
        /// <summary>
        /// Builds a file path from provided segments
        /// </summary>
        /// <param name="segments">String arrary</param>
        /// <param name="index">Index value</param>
        /// <returns>String</returns>
        protected string buildPath(string[] segments, int index)
        {
            string temp = "";
            for (int i = 0; i < index; i++)
            {
                temp += segments[i];
                temp += "\\";
            }
            return temp;
        }
        /// <summary>
        /// Merges two string arrays into one string arrary
        /// </summary>
        /// <param name="t1">String array 1</param>
        /// <param name="t2">String array 2</param>
        /// <returns>String array</returns>
        protected string[] mergeLists(string[] t1, string[] t2)
        {
            string[] t3 = new string[t1.Length + t2.Length];
            int start = t1.Length;
            for (int i = 0; i < t1.Length; i++)
            {
                t3[i] = t1[i];
            }
            for (int i = 0; i < t2.Length; i++)
            {
                t3[start + i] = t2[i];
            }
            return t3;
        }
        /// <summary>
        /// Sets fileList and path provided an existing path
        /// </summary>
        /// <param name="path"></param>
        public void setFileListAtPath(string path)
        {
            fileList.Clear();
            if (System.IO.Directory.Exists(path) == true)
            {
                this.path = path;
                string[] fileNames = System.IO.Directory.GetFiles(path);
                for (int i = 0; i < fileNames.Length; i++)
                {
                    char[] spliter = { '\\' };
                    string[] temp2 = StringParser.parse(fileNames[i], spliter);
                    fileList.addOption(temp2[temp2.Length - 1]);
                }
            }
        }
        /// <summary>
        /// Updates the CoolDown for FileExplorer
        /// </summary>
        public void coolOff()
        {
            cool.update();
        }
        /// <summary>
        /// Done property
        /// </summary>
        public bool Done
        {
            get { return done; }
            set { done = value; }
        }
        /// <summary>
        /// Count property
        /// </summary>
        public int Count
        {
            get { return files.Count; }
        }
    }
    /// <summary>
    /// TextFileExplorer
    /// </summary>
    public class TextFileExplorer
    {
        //protected
        protected Point2D pos;
        protected Rectangle box;
        protected ScrollingList2 fileList;
        protected List<SimpleButton4> buttons;
        protected bool done;
        protected List<string> files;
        protected string path;
        protected bool active;
        protected CoolDown cool;
        protected SDL.SDL_Color white;
        protected SDL.SDL_Color black;
        protected SDL.SDL_Color red;
        protected Rectangle highLightBox;
        protected Rectangle boarder;
        protected Rectangle mBox;

        //public
        /// <summary>
        /// TextFileExplorer constructor
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y postion</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="downButtonPressed">Texture2D reference</param>
        /// <param name="downButtonDepressed">Texture2D reference</param>
        /// <param name="upButtonPressed">Texture2D reference</param>
        /// <param name="upButtonDepressed">Texture2D reference</param>
        /// <param name="backButtonPressed">Texture2D reference</param>
        /// <param name="backButtonDepressed">Texture2D reference</param>
        /// <param name="exitButton">Texture2D reference</param>
        /// <param name="size">Number of displayed items</param>
        /// <param name="shift">Shift down value</param>
        public TextFileExplorer(int x, int y, int w, int h, Texture2D downButtonPressed, Texture2D downButtonDepressed, Texture2D upButtonPressed, Texture2D upButtonDepressed, Texture2D backButtonPressed, Texture2D backButtonDepressed, Texture2D exitButton, int size = 9, int shift = 32)
        {
            pos = new Point2D(x, y);
            box = new Rectangle(x, y, w, h);
            fileList = new ScrollingList2(downButtonPressed, downButtonDepressed, upButtonPressed, upButtonDepressed, exitButton, x, y, w, h, w, 32, size, shift);
            buttons = new List<SimpleButton4>();
            buttons.Add(new SimpleButton4(backButtonPressed, backButtonDepressed, (int)pos.X + w - 64, (int)pos.Y + h, "back"));
            //buttons.Add(new SimpleButton2(TextureBank.getTexture("bbp"), TextureBank.getTexture("bbd"), pos.X + w - 160, pos.Y + h + 32, "done"));
            done = false;
            files = new List<string>();
            path = AppDomain.CurrentDomain.BaseDirectory;
            string[] fileNames = System.IO.Directory.GetFiles(path);
            for (int i = 0; i < fileNames.Length; i++)
            {
                char[] spliter = { '\\' };
                string[] temp2 = StringParser.parse(fileNames[i], spliter);
                fileList.addOption(temp2[temp2.Length - 1]);
            }
            active = false;
            cool = new CoolDown(12);
            white.r = 255;
            white.g = 255;
            white.b = 255;
            white.a = 1;
            black.r = 0;
            black.g = 0;
            black.b = 0;
            black.a = 1;
            red.r = 255;
            red.g = 0;
            red.b = 0;
            red.a = 1;
            highLightBox = new Rectangle(x, y, w, 32);
            boarder = new Rectangle(x, y, w, 32 * size);
            mBox = new Rectangle(0, 0, 2, 2);
        }
        /// <summary>
        /// Updates internal state
        /// </summary>
        /// <param name="cursor">SimpleCursor reference</param>
        /// <returns>String</returns>
        public string update(SimpleCursor cursor)
        {
            string temp = "";
            if (!cool.Active)
            {
                temp = fileList.update(cursor.getMBS(), cursor.Box);
            }
            if (temp != "")
            {
                cool.activate();
                if (System.IO.Directory.Exists(path + temp))
                {
                    temp = path + temp;
                }
                else
                {
                    temp = path + "\\" + temp;
                }
                if (System.IO.Directory.Exists(temp))
                {
                    try
                    {
                        path = temp;
                        fileList.Clear();
                        string[] fileNames = mergeLists(System.IO.Directory.GetFiles(temp), System.IO.Directory.GetDirectories(temp));
                        for (int i = 0; i < fileNames.Length; i++)
                        {
                            char[] spliter = { '\\' };
                            string[] temp2 = StringParser.parse(fileNames[i], spliter);
                            fileList.addOption(temp2[temp2.Length - 1]);
                        }
                    }
                    catch (Exception)
                    {
                        char[] spliter = { '\\' };
                        string[] temp2 = StringParser.parse(path, spliter);
                        path = buildPath(temp2, temp2.Length - 2);
                    }
                }
                else
                {
                    char[] spliter = { '.' };
                    string[] temp2 = StringParser.parse(temp, spliter);
                    string[] temp3;
                    if (temp2[temp2.Length - 1] == "txt")
                    {
                        spliter = new char[] { '\\' };
                        temp3 = StringParser.parse(temp2[0], spliter);
                        return path + "\\" + temp3[temp3.Length - 1] + ".txt";
                    }
                }
            }
            string temp4 = "";
            for (int i = 0; i < buttons.Count; i++)
            {
                temp4 = buttons[i].click();
                if (temp4 != "")
                {
                    switch (temp4)
                    {
                        case "done":
                            done = true;
                            break;
                        case "back":
                            char[] spliter = { '\\' };
                            string[] temp5 = StringParser.parse(path, spliter);
                            if (temp5.Length > 1)
                            {
                                path = buildPath(temp5, temp5.Length - 2);
                                /*
                                for (int k = 0; k < temp5.Length - 2; k++)
                                {
                                    path += temp5[k];
                                    path += '\\';
                                }
                                 */
                            }
                            if (path == "" || path == " ")
                            {
                                break;
                            }
                            fileList.Clear();
                            string[] fileNames = mergeLists(System.IO.Directory.GetFiles(path), System.IO.Directory.GetDirectories(path));
                            for (int v = 0; v < fileNames.Length; v++)
                            {
                                char[] spliter2 = { '\\' };
                                string[] temp2 = StringParser.parse(fileNames[v], spliter2);
                                fileList.addOption(temp2[temp2.Length - 1]);
                            }
                            break;
                    }
                    break;
                }
            }
            buttons[0].update();
            cool.update();
            //buttons[1].update();
            return "";
        }
        /// <summary>
        /// Updates internal state (returns file paths for files that don't end .txt)
        /// </summary>
        /// <param name="cursor">SimpleCursor reference</param>
        /// <returns>String</returns>
        public string update2(SimpleCursor cursor)
        {
            string temp = "";
            if (!cool.Active)
            {
                temp = fileList.update(cursor.getMBS(), cursor.Box);
            }
            if (temp != "")
            {
                cool.activate();
                if (System.IO.Directory.Exists(path + temp))
                {
                    temp = path + temp;
                }
                else
                {
                    temp = path + "\\" + temp;
                }
                if (System.IO.Directory.Exists(temp))
                {
                    try
                    {
                        path = temp;
                        fileList.Clear();
                        string[] fileNames = mergeLists(System.IO.Directory.GetFiles(temp), System.IO.Directory.GetDirectories(temp));
                        for (int i = 0; i < fileNames.Length; i++)
                        {
                            char[] spliter = { '\\' };
                            string[] temp2 = StringParser.parse(fileNames[i], spliter);
                            fileList.addOption(temp2[temp2.Length - 1]);
                        }
                    }
                    catch (Exception)
                    {
                        char[] spliter = { '\\' };
                        string[] temp2 = StringParser.parse(path, spliter);
                        path = buildPath(temp2, temp2.Length - 2);
                    }
                }
                else
                {
                    return temp;
                }
            }
            string temp4 = "";
            for (int i = 0; i < buttons.Count; i++)
            {
                temp4 = buttons[i].click();
                if (temp4 != "")
                {
                    switch (temp4)
                    {
                        case "done":
                            done = true;
                            break;
                        case "back":
                            char[] spliter = { '\\' };
                            string[] temp5 = StringParser.parse(path, spliter);
                            if (temp5.Length > 1)
                            {
                                path = buildPath(temp5, temp5.Length - 2);
                            }
                            if (path == "" || path == " ")
                            {
                                break;
                            }
                            fileList.Clear();
                            string[] fileNames = mergeLists(System.IO.Directory.GetFiles(path), System.IO.Directory.GetDirectories(path));
                            for (int v = 0; v < fileNames.Length; v++)
                            {
                                char[] spliter2 = { '\\' };
                                string[] temp2 = StringParser.parse(fileNames[v], spliter2);
                                fileList.addOption(temp2[temp2.Length - 1]);
                            }
                            break;
                    }
                    break;
                }
            }
            buttons[0].update();
            cool.update();
            //buttons[1].update();
            return temp;

        }
        /// <summary>
        /// Updates internal state (returns file paths for files that don't end .txt)
        /// </summary>
        /// <param name="cursor">SimplePointer reference</param>
        /// <returns>String</returns>
        public string update2(SimplePointer cursor)
        {
            string temp = "";
            if (!cool.Active)
            {
                mBox.X = MouseHandler.getMouseX();
                mBox.Y = MouseHandler.getMouseY();
                temp = fileList.update(MouseHandler.getLeft(), mBox);
            }
            if (temp != "")
            {
                cool.activate();
                if (System.IO.Directory.Exists(path + temp))
                {
                    temp = path + temp;
                }
                else
                {
                    temp = path + "\\" + temp;
                }
                if (System.IO.Directory.Exists(temp))
                {
                    try
                    {
                        path = temp;
                        fileList.Clear();
                        string[] fileNames = mergeLists(System.IO.Directory.GetFiles(temp), System.IO.Directory.GetDirectories(temp));
                        for (int i = 0; i < fileNames.Length; i++)
                        {
                            char[] spliter = { '\\' };
                            string[] temp2 = StringParser.parse(fileNames[i], spliter);
                            fileList.addOption(temp2[temp2.Length - 1]);
                        }
                    }
                    catch (Exception)
                    {
                        char[] spliter = { '\\' };
                        string[] temp2 = StringParser.parse(path, spliter);
                        path = buildPath(temp2, temp2.Length - 2);
                    }
                }
                else
                {
                    return temp;
                }
            }
            string temp4 = "";
            for (int i = 0; i < buttons.Count; i++)
            {
                temp4 = buttons[i].click();
                if (temp4 != "")
                {
                    switch (temp4)
                    {
                        case "done":
                            done = true;
                            break;
                        case "back":
                            char[] spliter = { '\\' };
                            string[] temp5 = StringParser.parse(path, spliter);
                            if (temp5.Length > 1)
                            {
                                path = buildPath(temp5, temp5.Length - 2);
                            }
                            if (path == "" || path == " ")
                            {
                                break;
                            }
                            fileList.Clear();
                            string[] fileNames = mergeLists(System.IO.Directory.GetFiles(path), System.IO.Directory.GetDirectories(path));
                            for (int v = 0; v < fileNames.Length; v++)
                            {
                                char[] spliter2 = { '\\' };
                                string[] temp2 = StringParser.parse(fileNames[v], spliter2);
                                fileList.addOption(temp2[temp2.Length - 1]);
                            }
                            break;
                    }
                    break;
                }
            }
            buttons[0].update();
            cool.update();
            //buttons[1].update();
            return temp;

        }
        /// <summary>
        /// Draws TextFileExplorer
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="cursor">SimpleCursor reference</param>
        public void draw(IntPtr renderer, SimpleCursor cursor)
        {
            fileList.drawArrows(renderer);
            buttons[0].draw(renderer);
            buttons[0].darwName(renderer);
            char[] spliter = { '\\' };
            string[] temp = StringParser.parse(path, spliter);
            if (temp.Length >= 2)
            {
                SimpleFont.DrawString(renderer, temp[temp.Length - 2], (int)pos.X, (int)pos.Y - 32, 1);
            }
            if (cursor != null)
            {
                if (cool.Active)
                {
                    highLightBox.X = cursor.X;
                    highLightBox.Y = cursor.Y;
                    highLightBox.Width = 6;
                    highLightBox.Height = 6;
                    DrawRects.drawRect(renderer, highLightBox, red, false);
                }
            }
            DrawRects.drawRect(renderer, boarder, white, false);
        }
        /// <summary>
        /// Draws TextFileExplorer
        /// </summary>
        /// <param name="renderer">Renderer reference</param>
        /// <param name="cursor">SimpleCursor reference</param>
        /// <param name="colour">Background colour</param>
        public void draw(IntPtr renderer, SimpleCursor cursor, XENOCOLOURS colour)
        {
            fileList.drawArrows(renderer, colour);
            buttons[0].draw(renderer);
            buttons[0].darwName(renderer);
            char[] spliter = { '\\' };
            string[] temp = StringParser.parse(path, spliter);
            if (temp.Length >= 2)
            {
                SimpleFont.DrawString(renderer, temp[temp.Length - 2], (int)pos.X, (int)pos.Y - 32, 1);
                
            }
            if (cursor != null)
            {
                if (cool.Active)
                {
                    highLightBox.X = cursor.X;
                    highLightBox.Y = cursor.Y;
                    highLightBox.Width = 6;
                    highLightBox.Height = 6;
                    DrawRects.drawRect(renderer, highLightBox, red, false);//Line.drawSquare(sb, pixel, cursor.X, cursor.Y, 6, 6, Color.Red, 2);
                }
            }
            DrawRects.drawRect(renderer, boarder, white, false);
        }
        /// <summary>
        /// Returns a string array of file names contained in director of provided file path
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>String arrary</returns>
        public string[] getContents(string filePath)
        {
            string[] t1 = System.IO.Directory.GetFiles(filePath);
            string[] t2 = System.IO.Directory.GetDirectories(filePath);
            return mergeLists(t1, t2);
        }
        /// <summary>
        /// Returns an arrary containing file names minus paths and extintions
        /// </summary>
        /// <param name="filePath">File path to search</param>
        /// <returns>String arrary</returns>
        public string[] getNames(string filePath)
        {
            string[] tmp1 = getContents(filePath); 
            string[] tmp2 = new string[tmp1.Length];
            char[] slash = {'\\'};
            char[] dot = {'.'};
            for(int i = 0; i < tmp1.Length; i++)
            {
                tmp2[i] = StringParser.doubleParse(tmp1[i], slash, dot)[tmp1.Length - 1][0];
            }
            return tmp2;
        }
        /// <summary>
        /// Returns a limited number of accpeted file types
        /// </summary>
        /// <param name="pathName">File path</param>
        /// <returns>FileType enumeration</returns>
        public fileType getType(string pathName)
        {
            if (System.IO.Directory.Exists(pathName))
            {
                return fileType.dir;
            }
            char[] splitter = { '.' };
            string[] temp = StringParser.parse(pathName, splitter);
            string ft = temp[temp.Length - 1];
            switch (ft)
            {
                case "png":
                    return fileType.png;
                case "wav":
                    return fileType.wav;
                case "mp3":
                    return fileType.mp3;
                case "txt":
                    return fileType.txt;
            }
            return fileType.none;
        }
        /// <summary>
        /// Builds a file path from provided segments
        /// </summary>
        /// <param name="segments">String array</param>
        /// <param name="index">Index value</param>
        /// <returns>String</returns>
        protected string buildPath(string[] segments, int index)
        {
            string temp = "";
            for (int i = 0; i < index; i++)
            {
                temp += segments[i];
                if (i + 1 != index)
                {
                    temp += "\\";
                }
            }
            return temp;
        }
        /// <summary>
        /// Merge two string arrays into one
        /// </summary>
        /// <param name="t1">String array 1</param>
        /// <param name="t2">String array 2</param>
        /// <returns>String arrary</returns>
        protected string[] mergeLists(string[] t1, string[] t2)
        {
            string[] t3 = new string[t1.Length + t2.Length];
            int start = t1.Length;
            for (int i = 0; i < t1.Length; i++)
            {
                t3[i] = t1[i];
            }
            for (int i = 0; i < t2.Length; i++)
            {
                t3[start + i] = t2[i];
            }
            return t3;
        }
        /// <summary>
        /// Sets filelist to current working directory 
        /// </summary>
        public void setFileListDefualt()
        {
            path = AppDomain.CurrentDomain.BaseDirectory;
            string[] fileNames = System.IO.Directory.GetFiles(path);
            for (int i = 0; i < fileNames.Length; i++)
            {
                char[] spliter = { '\\' };
                string[] temp2 = StringParser.parse(fileNames[i], spliter);
                fileList.addOption(temp2[temp2.Length - 1]);
            }
        }
        /// <summary>
        /// Sets fileList and path provided an existing path
        /// </summary>
        /// <param name="path"></param>
        public void setFileListAtPath(string path)
        {
            fileList.Clear();
            if (System.IO.Directory.Exists(path) == true)
            {
                this.path = path;
                string[] fileNames = System.IO.Directory.GetFiles(path);
                for (int i = 0; i < fileNames.Length; i++)
                {
                    char[] spliter = { '\\' };
                    string[] temp2 = StringParser.parse(fileNames[i], spliter);
                    fileList.addOption(temp2[temp2.Length - 1]);
                }
            }
        }
        /// <summary>
        /// Activates TexFileExplorer
        /// </summary>
        public void activate()
        {
            active = true;
        }
        /// <summary>
        /// Deactivates TextFileExplorer
        /// </summary>
        public void deactivate()
        {
            active = false;
        }
        /// <summary>
        /// Updates the CoolDown for FileExplorer
        /// </summary>
        public void coolOff()
        {
            cool.update();
        }
        /// <summary>
        /// Done property
        /// </summary>
        public bool Done
        {
            get { return done; }
            set { done = value; }
        }
        /// <summary>
        /// Active property
        /// </summary>
        public bool Active
        {
            get { return active; }
        }
        /// <summary>
        /// Path property
        /// </summary>
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        /// <summary>
        /// Count property
        /// </summary>
        public int Count
        {
            get { return files.Count; }
        }
    }
}
