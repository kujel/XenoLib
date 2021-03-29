using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using SDL2;
//using Microsoft.Xna.Framework;

namespace XenoLib
{
    /// <summary>
    /// Helper functions class
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Returns a number divisible by probided scaler value less than provided value
        /// </summary>
        /// <param name="value">Value to reduce</param>
        /// <param name="scale">Scaling value</param>
        /// <returns>Int</returns>
        public static int forceDivisible(int value, double scale)
        {
            int temp = value;
            while(scale % value != 0)
            {
                value--;
            }
            return temp;
        }
        /// <summary>
        /// Tests if a position is in domain of specified ranges
        /// </summary>
        /// <param name="x">X value</param>
        /// <param name="y">Y value</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <returns>Boolean</returns>
        public static bool inDomain(int x, int y, int w, int h)
        {
            if (x < 0 || x > w)
            {
                return false;
            }
            if (y < 0 || y > h)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Gets a list of points generating a circle centered around a point
        /// on a data grid
        /// </summary>
        /// <param name="grid">DataGrid reference</param>
        /// <param name="center">Center of circle</param>
        /// <param name="radius">Radius of circle</param>
        /// <returns>List of Point2D objects</returns>
        public static List<Point2D> getCircleArea<T>(DataGrid<T> grid, Point2D center, float radius)
        {
            List<Point2D> points = new List<Point2D>();
            int width = (int)(radius * 2) + 1;
            int height = (int)(radius * 2) + 1;
            Point2D p = null;
            for (int x = (int)(center.X - radius); x < (int)(center.X - radius - 1) + width; x++)
            {
                for (int y = (int)(center.Y - radius); y < (int)(center.Y - radius - 1) + height; y++)
                {
                    if (inDomain(x, y, grid.Width, grid.Height) == true)
                    {
                        p = new Point2D(x, y);
                        if (insideCircle(center, p, radius) == true)
                        {
                            points.Add(p);
                        }
                    }
                }
            }
            return points;
        }
        /// <summary>
        /// Aids getCircleArea
        /// </summary>
        /// <param name="center">Center of circle</param>
        /// <param name="p">Point to be checked</param>
        /// <param name="radius">Radius of circle</param>
        /// <returns>Boolean</returns>
        static bool insideCircle(Point2D center, Point2D p, float radius)
        {
            float dx = center.X - p.X;
            float dy = center.Y - p.Y;
            float dist = (dx * dx) + (dy * dy);
            if (dist <= radius * radius)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns a list of valid points in radius for x and y positions
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="radius">Radius around x, y position</param>
        /// <param name="curve">How shallow or sharp the curve is</param>
        /// <param name="w">Width of grid</param>
        /// <param name="h">Height of grid</param>
        /// <returns>List of Point2D objects</returns>
        public static List<Point2D> getAreaIn2DGrid(int x, int y, int radius, int curve, int w, int h)
        {
            List<Point2D> temp = new List<Point2D>();
            if (radius < 3)
            {
                radius = 3;
            }
            int tempW = curve + 1;
            //int tempShift = 1;
            int txs = x - curve;
            for (int ty = y - radius; ty < y * 2 + 1; ty++)
            {
                for (int tx = txs; tx < txs + tempW; tx++)
                {
                    if (inDomain(tx, ty, w, h))
                    {
                        temp.Add(new Point2D(tx, ty));
                    }
                }
                if (ty < y - 1)
                {
                    txs -= curve;
                    tempW += (curve * 2);
                }
                else if (ty > y + 1)
                {
                    txs += curve;
                    tempW -= (curve * 2);
                }
            }
            return temp;
        }
        /// <summary>
        /// Scans a DataGrid of type K objects for type T objects
        /// </summary>
        /// <typeparam name="T">Object type to scan for</typeparam>
        /// <typeparam name="K">Object type stored in DataGrid</typeparam>
        /// <param name="grid">DataGrid reference</param>
        /// <returns>List of Point2D objects</returns>
        public static List<Point2D> scanDataGrid<T, K>(DataGrid<K> grid)
        {
            List<Point2D> temp = new List<Point2D>();
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    if (grid.Grid[x, y] is T)
                    {
                        temp.Add(new Point2D(x, y));
                    }
                }
            }
            return temp;
        }
        /// <summary>
        /// Scales up a provided value by provided scaler
        /// </summary>
        /// <param name="value">Value to scale up</param>
        /// <param name="scale">Scaler value</param>
        /// <returns>Int</returns>
        public static int scaleUp(int value, double scale)
        {
            return (int)(value * scale);
        }
        /// <summary>
        /// Scales down a provided value by provided scaler
        /// </summary>
        /// <param name="value">Value to scale down</param>
        /// <param name="scale">Scaler value</param>
        /// <returns>Int</returns>
        public static int scaleDwon(int value, double scale)
        {
            return (int)(value / scale);
        }
        /// <summary>
        /// Calculates number of radians per slice provided
        /// </summary>
        /// <param name="numSlices">Number of slices</param>
        /// <returns>Double</returns>
        public static double radiansPerSlice(int numSlices)
        {
            return (2*Math.PI) / numSlices;
        }
        /// <summary>
        /// Calculate facing between two points
        /// </summary>
        /// <param name="p1">Point 1</param>
        /// <param name="p2">Point 2</param>
        /// <returns>Facing8</returns>
        public static facing8 p1FaceP2(Point2D p1, Point2D p2)
        {
            float x = p1.X - p2.X;
            float y = p1.Y - p2.Y;
            double angle = Math.Atan(y / x);
            return (facing8)(int)(angle / ((2 * Math.PI) / 8));
        }
        /// <summary>
        /// Convert degrees to radians
        /// </summary>
        /// <param name="value">Degree value to convert</param>
        /// <returns>Double</returns>
        public static double degreesToRadians(double value)
        {
            return value * (PI / 180);
        }
        /// <summary>
        /// Convert radians to degrees
        /// </summary>
        /// <param name="value">Radian value to convert</param>
        /// <returns>Double</returns>
        public static double RadiansToDegrees(double value)
        {
            return value * (180 / PI);
        }
        /// <summary>
        /// Return an approximation of PI
        /// </summary>
        public static double PI
        {
            //3.14159265
            get { return Math.PI; }
        }
        /// <summary>
        /// Takes an angle in degrees and limits it to -360 to 360
        /// </summary>
        /// <param name="angle">Angle to limit</param>
        /// <returns>Double</returns>
        public static double limitRangeTo360(double angle)
        {
            double tmp = 0;
            if (angle > 360)
            {
                tmp = angle - 360;
                while(tmp > 360)
                {
                    tmp = tmp - 360;
                }
            }
            else if(angle < -360)
            {
                tmp = angle + 360;
                while (tmp < 360)
                {
                    tmp = tmp + 360;
                }
            }
            else
            {
                tmp = angle;
            }
            return tmp;
        }
        /// <summary>
        /// Calculate one position on x axis in specified direction
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="angle">Angle of movement</param>
        /// <param name="scaler">Scaling value ie: tile width</param>
        /// <returns>X value</returns>
        public static float getXValue(float x, double angle, int scaler)
        {
            return x + ((float)Math.Cos(degreesToRadians(angle)) * (float)scaler);
        }
        /// <summary>
        /// Calculate one position on x axis in specified direction
        /// </summary>
        /// <param name="y">Y position</param>
        /// <param name="angle">Angle of movement</param>
        /// <param name="scaler">Scaling value ie: tile width</param>
        /// <returns>Y value</returns>
        public static float getYValue(float y, double angle, int scaler)
        {
            return y + ((float)Math.Sin(degreesToRadians(angle)) * (float)scaler);
        }
        /// <summary>
        /// Extracts a file name minus extention and file path
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>String</returns>
        public static string extractFileName(string filePath)
        {
            char[] dot = {'.'};
            char[] slash = {'\\'};
            string[] strs = StringParser.parse(filePath, dot);
            string[] strs2 = StringParser.parse(strs[0], slash);
            string str = strs2[strs2.Length - 1];
            return str;
        }
        /// <summary>
        /// Extracts a file's extention
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>String</returns>
        public static string extractExtention(string filePath)
        {
            char[] dot = { '.' };
            string[] strs = StringParser.parse(filePath, dot);
            return strs[strs.Length - 1];
        }
        /// <summary>
        /// Returns a file name plus extention provided a full file path
        /// </summary>
        /// <param name="filePath">Path of file to extract proper name of</param>
        /// <returns>String</returns>
        public static string extractFileNameProper(string filePath)
        {
            char[] slash = { '\\' };
            string[] strs = StringParser.parse(filePath, slash);
            return strs[strs.Length - 1];
        }
        /// <summary>
        /// Checks if a string contains any numbers
        /// </summary>
        /// <param name="str">String to check</param>
        /// <returns>Boolean</returns>
        public static bool containsNumbers(string str)
        {
            for(int i = 0; i < str.Length; i++)
            {
                switch(str[i])
                {
                    case '0':
                        return true;
                    case '1':
                        return true;
                    case '2':
                        return true;
                    case '3':
                        return true;
                    case '4':
                        return true;
                    case '5':
                        return true;
                    case '6':
                        return true;
                    case '7':
                        return true;
                    case '8':
                        return true;
                    case '9':
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Checks if a string contains only numbers
        /// </summary>
        /// <param name="str">String to check</param>
        /// <returns>Boolean</returns>
        public static bool containsOnlyNumbers(string str)
        {
            bool found = false;//found a number at position in string
            for (int i = 0; i < str.Length; i++)
            {
                found = false;//start of each new cycle set found to false
                switch (str[i])
                {
                    case '0':
                        found = true;
                        break;
                    case '1':
                        found = true;
                        break;
                    case '2':
                        found = true;
                        break;
                    case '3':
                        found = true;
                        break;
                    case '4':
                        found = true;
                        break;
                    case '5':
                        found = true;
                        break;
                    case '6':
                        found = true;
                        break;
                    case '7':
                        found = true;
                        break;
                    case '8':
                        found = true;
                        break;
                    case '9':
                        found = true;
                        break;
                }
                if(found == false)//A number was not found at postion
                {
                    return false;
                }
            }
            return true;//Pass through and only found numbers
        }
        /// <summary>
        /// Copys files from one folder to another folder
        /// </summary>
        /// <param name="currentDir">File path of folder to copy from</param>
        /// <param name="targetDir">File path of folder to copy to</param>
        public static void copyFolder(string currentDir, string targetDir)
        {
            //copy all cell files from current dir to save dir
            string[] fileNames = Directory.GetFiles(currentDir);
            for (int f = 0; f < fileNames.Length; f++)
            {
                File.Copy(fileNames[f], targetDir + extractFileNameProper(fileNames[f]), true);
            }
        }
        /// <summary>
        /// Fast copys folder to folder
        /// </summary>
        /// <param name="SolutionDirectory">Folder to copy from</param>
        /// <param name="TargetDirectory">Folder to copy to</param>
        public static void fastCopyFolder(string SolutionDirectory, string TargetDirectory)
        {
            // Use ProcessStartInfo class

            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.CreateNoWindow = false;

            startInfo.UseShellExecute = false;

            //Give the name as Xcopy

            startInfo.FileName = "xcopy";

            //make the window Hidden

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            //Send the Source and destination as Arguments to the process

            startInfo.Arguments = "\"" + SolutionDirectory + "\"" + " " + "\"" + TargetDirectory + "\"" + @" /e /y /I";

            try

            {
                // Start the process with the info we specified.

                // Call WaitForExit and then the using statement will close.

                using (Process exeProcess = Process.Start(startInfo))

                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception exp)

            {
                throw exp;
            }
        }
    }
}
