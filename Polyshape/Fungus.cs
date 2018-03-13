using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;
using System.Drawing;
using System.Threading;

namespace Polyshape
{
    class Fungus
    {
        static Random rand = new Random();

        public Dictionary<Point, int> dict = new Dictionary<Point, int>();

        public Thread thread = null;

        public static CDrawer canvas = null;

        public Point point = new Point();

        public enum Colors {Red , Green, Blue };

        private Colors test;

        public List<Point> AdjP = new List<Point>();

        public Fungus(Point startP, CDrawer _canvas, Colors color)
        {
            point = startP;

            canvas = _canvas;

            test = color;

            thread = new Thread(new ThreadStart(FungusThread));
            thread.IsBackground = true;
            thread.Start();
        }


        private void FungusThread()
        {
            Dictionary<Point, int> tempD = new Dictionary<Point, int>();

            int c = 0;
            while (true)
            {
                AdjP = NeighbourPoint(point);

                AdjP.RemoveAll(o => o.X < 0 || o.X > canvas.m_ciWidth-1 || o.Y < 0 || o.Y > canvas.m_ciHeight-1);

                AdjP = Shuffle(AdjP);

                tempD = AdjP.ToDictionary(o => o, o => dict.ContainsKey(o) ? dict[o] : 0);

                List<KeyValuePair<Point, int>> temp = tempD.OrderBy(o => o.Value).ToList();

              
                point = temp.First().Key;

                if (dict.ContainsKey(point))
                {
                    if (dict[point] + 16 < 255)
                        dict[point] += 16;
                    else
                        dict[point] = 255;
                }
                else
                    dict.Add(point, 32);

                if (test == Colors.Red)
                    canvas.SetBBPixel(point.X, point.Y, Color.FromArgb(dict[point], 0, 0));
                else if (test == Colors.Blue)
                    canvas.SetBBPixel(point.X, point.Y, Color.FromArgb(0, 0, dict[point]));
                else if (test == Colors.Green)
                    canvas.SetBBPixel(point.X, point.Y, Color.FromArgb(0, dict[point], 0));

                //canvas.Render();
                Thread.Sleep(0);
                

            }
        }

        private List<Point> NeighbourPoint( Point HomePoint)
        {
            List<Point> tempList = new List<Point>();

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    //if(x!=0 && y!=0)
                    //    tempList.Add(new Point( HomePoint.X + x, HomePoint.Y + y));
                
                        tempList.Add(new Point(HomePoint.X + x, HomePoint.Y + y));
                }

            }

            tempList.Remove(HomePoint);

            return tempList;
        }

        public static List<Point> Shuffle(List<Point> sourcelist)
        {          
            List<Point> newList = sourcelist.ToList();

            int max = newList.Count();
            lock (rand)
            {
                for (int i = max - 1; i >= 0; i--)
                {
                    int j = rand.Next(i + 1);
                    Point temp = newList[i];
                    newList[i] = newList[j];
                    newList[j] = temp;
                }
            }
            return newList;
        }


    }
}
