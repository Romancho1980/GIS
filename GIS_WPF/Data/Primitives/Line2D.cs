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

        public int line { get; set; } = 0;

        // Линия внутри viewports 
        // 0 - null - Не инициализирована
        // 1 - Внутри
        // 2 - Не пересекает
        // 3 - Возможно пересекает

        public string descr { get; set; } = "";

        public Line2D(Point2D p1,Point2D p2)
        {
            P1 = p1;
            P2 = p2;
        }
    }
}
