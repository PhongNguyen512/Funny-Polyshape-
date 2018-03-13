using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GDIDrawer;
using System.Threading;

namespace Polyshape
{
    class Program
    {
        static void Main(string[] args)
        {
            CDrawer canvas = new CDrawer(1000, 1000);
            Fungus _fungii;
            //_fungii = new Fungus(new Point(0, 0), canvas, Fungus.Colors.Red);
            _fungii = new Fungus(new Point(0, 0), canvas, Fungus.Colors.Green);
            _fungii = new Fungus(new Point(0, 0), canvas, Fungus.Colors.Blue);



            List<IRender> _shapes = new List<IRender>();
            _shapes.Add(new FixedSquare(new Point(450, 500), Color.Red, null));
            _shapes.Add(new FixedSquare(new Point(550, 500), Color.Red, (FixedSquare)_shapes[0]));

            List<Shape> local = new List<Shape>();
            local.Add(new OrbitBall(Color.Yellow, 50, (Shape)_shapes[0], -0.1, Math.PI));
            local.Add(new OrbitBall(Color.Pink, 50, local[0], -0.15, Math.PI));
            local.Add(new OrbitBall(Color.Blue, 50, local[1], -0.2, Math.PI));
            local.Add(new OrbitBall(Color.Green, 50, local[2], -0.25, Math.PI));
            _shapes.AddRange(local);


            local = new List<Shape>();
            local.Add(new OrbitBall(Color.Yellow, 50, (Shape)_shapes[1], 0.05, 0));
            local.Add(new OrbitBall(Color.Pink, 50, local[0], 0.075, 0));
            local.Add(new OrbitBall(Color.Blue, 50, local[1], 0.1, 0));
            local.Add(new OrbitBall(Color.Green, 50, local[2], 0.125, 0));
            _shapes.AddRange(local);

            local = new List<Shape>();
            local.Add(new FixedSquare(new PointF(200, 500), Color.Cyan, null));
            local.Add(new VWobbleBall(Color.Red, 100, local[0], 0.1, 0));
            local.Add(new HWobbleBall(Color.Red, 100, local[1], 0.15, 0));
            local.Add(new OrbitBall(Color.LightBlue, 25, local[2], 0.2, 0));
            _shapes.AddRange(local);

            List<Shape> localA = new List<Shape>();
            List<Shape> localB = new List<Shape>();
            for (int i = 50; i < 1000; i += 50)
                localA.Add(new FixedSquare(new PointF(i, 100), Color.Cyan, null));
            _shapes.AddRange(localA);
            double so = 0;
            foreach (Shape s in localA)
                localB.Add(new VWobbleBall(Color.Purple, 50, s, 0.1, so += 0.7));
            _shapes.AddRange(localB);


            local = new List<Shape>();
            local.Add(new FixedSquare(new PointF(800, 500), Color.GreenYellow, null));
            local.Add(new OrbitBall(Color.Yellow, 30, local[0], 0.1, 0));
            local.Add(new OrbitBall(Color.Yellow, 30, local[0], 0.1, Math.PI / 2));
            local.Add(new OrbitBall(Color.Yellow, 30, local[0], 0.1, Math.PI));
            local.Add(new OrbitBall(Color.Yellow, 30, local[0], 0.1, 3 * Math.PI / 2));
            local.Add(new OrbitBall(Color.Yellow, 60, local[0], -0.05, 0));
            local.Add(new OrbitBall(Color.Yellow, 60, local[0], -0.05, Math.PI / 2));
            local.Add(new OrbitBall(Color.Yellow, 60, local[0], -0.05, 3 * Math.PI));
            local.Add(new OrbitBall(Color.Yellow, 60, local[0], -0.05, 3 * Math.PI / 2));
            local.Add(new OrbitBall(Color.Yellow, 90, local[0], 0.025, 0));
            local.Add(new OrbitBall(Color.Yellow, 90, local[0], 0.025, Math.PI / 2));
            local.Add(new OrbitBall(Color.Yellow, 90, local[0], 0.025, Math.PI));
            local.Add(new OrbitBall(Color.Yellow, 90, local[0], 0.025, 3 * Math.PI / 2));
            _shapes.AddRange(local);


            _shapes.Add(new AniPoly(new PointF(100, 300), Color.Tomato, 3, null, 0.1, 0));
            _shapes.Add(new AniPoly(new PointF(135, 300), Color.Tomato, 3, null, -0.1, 1));
            _shapes.Add(new AniPoly(new PointF(170, 300), Color.Tomato, 3, null, 0.1, 0));



            local = new List<Shape>();
            local.Add(new FixedSquare(new PointF(500, 200), Color.Wheat, null));
            for (int i = 1; i < 20; ++i)
                local.Add(new HWobbleBall(Color.Orange, 25, local[i - 1], 0.1, 0));
            _shapes.AddRange(local);


            local = new List<Shape>();
            local.Add(new FixedSquare(new PointF(800, 300), Color.LightCoral, null));
            local.Add(new AniHighlight(Color.Yellow, 30, local[0], -0.2));
            _shapes.AddRange(local);






            do
            {
                _shapes.ForEach(o => o.Render(canvas));



                Thread.Sleep(35);
                canvas.Clear();
            } while (true);




            Console.ReadKey();
        }
    }
}
