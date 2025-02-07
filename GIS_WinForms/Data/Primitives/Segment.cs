using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data.Primitives
{
    internal class Segment
    {
        public Point P1 { get; set; }
        public Point P2 { get; set; }

        public Segment(Point p1,Point p2)
        {
            P1 = new Point(p1.X, p1.Y);
            P2 = new Point(p2.X, p2.Y);
        }
        public void Draw(Graphics g,int width=2,string color="black")
        {

        }
    }
}
