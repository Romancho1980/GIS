using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data.Primitives
{
    public class Vertices
    {

        public int X { get; set; }
        public int Y { get; set; }

        public Vertices()
        {
            X = 0;
            Y = 0;
        }

        public Vertices(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Vertices vert)
        {
            if (vert.X == X && vert.Y == Y) return true;
            return false;
        }
        public void Draw(PaintEventArgs e,int size=18,string color="Black")
        {
            int radius = size / 2;
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
            Rectangle rect = new Rectangle(0, 0, 100, 200);

            // Create start and sweep angles on ellipse.
            float startAngle = 45.0F;
            float sweepAngle = 270.0F;

            // Draw arc to screen.
            //e.Graphics.DrawArc(blackPen, rect,
            //          startAngle, sweepAngle);

            //e.Graphics.DrawArc(blackPen, X - centerX, Y - centerY, radius, radius, 0, 360);
            e.Graphics.FillEllipse(brush, X - centerX, Y - centerY, radius, radius);

        }
    }
}
