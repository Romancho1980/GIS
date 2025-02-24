using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data.Primitives
{
    public class Vertices
    {

       public Point point;

        public Vertices()
        {
            point = new Point(0,0);
        }

        public Vertices(int x, int y)
        {
            point = new Point(x,y);
        }

        public void getValue(Vertices? data)
        {
            if (data != null)
            {
                point.X = data.point.X;
                point.Y = data.point.Y;
            }
        }

        public bool Equals(Vertices vert)
        {
            if (vert.point.X == point.X && vert.point.Y == point.Y) return true;
            return false;
        }
        public override string ToString()
        {
            return $"{point.X} : {point.Y}";
        }
        public void Draw(PaintEventArgs e, int size = 15, string color = "Black", bool outline = false)
        {
            int radius = size ;
            int centerX = radius / 2;
            int centerY = radius / 2;

            // Create pen.
            Color color1 = Color.Blue;
            if (color.Equals("Black")) color1 = Color.Black;
            if (color.Equals("Red")) color1 = Color.Red;
            if (color.Equals("Yellow")) color1 = Color.Yellow;

            Pen blackPen = new Pen(color1, 3);
            Brush brush = new SolidBrush(color1);

            // Rectangle with specifies x1,
            // y1, x2, y2 respectively
            //Rectangle rect = new Rectangle(0, 0, 100, 200);

            //// Create start and sweep angles on ellipse.
            //float startAngle = 45.0F;
            //float sweepAngle = 270.0F;

            // Draw arc to screen.
            //e.Graphics.DrawArc(blackPen, rect,
            //          startAngle, sweepAngle);

            //e.Graphics.DrawArc(blackPen, X - centerX, Y - centerY, radius, radius, 0, 360);
            e.Graphics.FillEllipse(brush, point.X - centerX, point.Y - centerY, radius, radius);
            if (outline == true)
            {
                Pen pen = new Pen(Color.RebeccaPurple, 2);
                Rectangle rect = new Rectangle(point.X - centerX, point.Y - centerY, size, size);

                e.Graphics.DrawEllipse(pen, rect);
            }
            //    Brush brush1 = new SolidBrush(Color.Yellow);

            //    e.Graphics.FillEllipse(brush1, X - centerX*0.7F, Y - centerY*0.7F, radius*0.7F, radius*0.7F);
            //    e.Graphics.FillEllipse(brush, X - centerX*0.5F, Y - centerY*0.5F, radius*0.5F, radius*0.5F);
            //}
        }
    }
}
