using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;
using System.Drawing;

namespace Polyshape
{
    public interface IRender
    {
        void Render(CDrawer canvas);
    }

    public interface IAnimate
    {
        void Tick();
    }

    public abstract class Shape : IRender
    {

        public PointF point { get; set; }

        public Color color { get; private set; }

        public Shape obj { get; set; } = null;

        public Shape(PointF p, Color c, Shape o)
        {
            point = p;
            color = c;
            obj = o;
        }

        virtual public void Render(CDrawer canvas)
        {
            if (obj != null)
                canvas.AddLine((int)point.X, (int)point.Y, (int)obj.point.X, (int)obj.point.Y, Color.White);
        }
    }

    public class FixedSquare : Shape
    {
        public FixedSquare(PointF p, Color c, Shape obj)
            :base(p,c,obj)
        {
        }

        public override void Render(CDrawer canvas)
        {
            canvas.AddCenteredRectangle((int)point.X, (int)point.Y, 20, 20, color);
            base.Render(canvas);
        }


    }

    public abstract class AniShape : Shape, IAnimate
    {
        public double value { get; private set; }

        public double position { get; private set; }

        public AniShape(double val, double pos, PointF p, Color c, Shape o)
            :base(p,c,o)
        {
            value = val;
            position = pos;
        }

        virtual public void Tick()
        {
            position += value;
        }
    }


    public class AniPoly : AniShape
    {
        public int side { get; private set; }

        public AniPoly(PointF p, Color c, int s, Shape o, double value, double position)
            :base(value, position, p, c, o)
        {
            if (s < 3)
                throw new ArgumentException("Input > 3 of side");
            else
                side = s;
        }

        public override void Render(CDrawer canvas)
        {
            
            canvas.AddPolygon((int)point.X, (int)point.Y, 25, side, position, color);
            base.Render(canvas);
            base.Tick();
        }

        public override void Tick()
        {
            base.Tick();
        }

    }


    public abstract class AniChild : AniShape
    {
        public double distance { get; private set; }
      
        public AniChild(double val, double pos, PointF p ,Color c, Shape o, double dis)   
            :base(val, pos, p, c, o)    
        {
            if (o.Equals(null))
                throw new ArgumentException("Need Parrent");
            else
                distance = dis;
        }
      
    }

    public class AniHighlight : AniChild
    {


        public AniHighlight(Color c, double dis, Shape o, double val)
            : base(val, 0, new PointF(o.point.X + (float)dis, o.point.Y + (float)dis), c, o, dis)
        {
        }
        public override void Render(CDrawer canvas)
        {
            canvas.AddPolygon((int)point.X - 25, (int)point.Y - 25, 25, 3, position, color);
            base.Render(canvas);
            Tick();
        }


        public override void Tick()
        {
            base.Tick();

        }
    }


    public abstract class AniBall : AniChild
    {
        

        public AniBall(double val, double pos, PointF p, Color c, Shape o, double dis)
            :base(val, pos, p,c,o,dis)
        {
            
        }

        public override void Render(CDrawer canvas)
        {
            
            canvas.AddCenteredEllipse((int)point.X, (int)point.Y, 20, 20, color);
            base.Render(canvas);
        }

    }

    public class OrbitBall : AniBall
    {
        public OrbitBall(Color c, double dis, Shape o, double val, double pos)
            :base(val, pos, new PointF(o.point.X+(float)dis, o.point.Y+(float)dis),c,o,dis)
        {

        }

        public override void Render(CDrawer canvas)
        {
            Tick();
            base.Render(canvas);
            
        }

        public override void Tick()
        {
            
            point = new PointF(obj.point.X + (float)(Math.Sin(position) * distance), obj.point.Y + (float)(Math.Cos(position) * distance));
            base.Tick();

        }

    }


    public class VWobbleBall : AniBall
    {
        public VWobbleBall(Color c, double dis, Shape o, double val, double pos)
            :base(val, pos, new PointF(o.point.X + (float)val, o.point.Y + (float)val), c, o, dis)
        {

        }

        public override void Render(CDrawer canvas)
        {
            base.Render(canvas);
            Tick();
        }

        public override void Tick()
        {
            
            point = new PointF(obj.point.X, obj.point.Y + (float)(Math.Cos(position) * distance ));
            base.Tick();
        }

    }

    public class HWobbleBall : AniBall
    {
        public HWobbleBall(Color c, double dis, Shape o, double val, double pos)
            : base(val, pos, new PointF(o.point.X + (float)val, o.point.Y + (float)val), c, o, dis)
        {

        }

        public override void Render(CDrawer canvas)
        {
            
            Tick();
            base.Render(canvas);
        }

        public override void Tick()
        {
            point = new PointF(obj.point.X + (float)(Math.Sin(position) * distance), obj.point.Y );
            base.Tick();
        }

    }

}
