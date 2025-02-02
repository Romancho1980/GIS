using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WPF.Data.Primitives
{
    public class Line2D
    {
        public Point2D P1 { get; set; }
        public Point2D P2 { get; set; }


        public Point2D clipP1 { get; set; }
        public Point2D clipP2 { get; set; }

        // Линия внутри viewports 
        // Видимость отрезка
        public bool Visible { get; set; } = false;

        public string descr { get; set; } = "";

        public Line2D(Point2D p1,Point2D p2)
        {
            P1 = new Point2D();
            P2 = new Point2D();
            clipP1 = new Point2D();
            clipP2 = new Point2D();

            P1.X = p1.X;
            P1.Y = p1.Y;

            P2.X = p2.X;
            P2.Y = p2.Y;

            clipP1.X = p1.X;
            clipP1.Y = p1.Y;

            clipP2.X = p2.X;
            clipP2.Y = p2.Y;
        }
    }
}
