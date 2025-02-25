using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data.Primitives
{
    public class MyPoints
    {
        public int X;
        public int Y;

        public MyPoints()
        {
            X = 0;
            Y = 0;
        }

        public MyPoints(MyPoints p1)
        {
            X = p1.X;
            Y = p1.Y;
        }

        public MyPoints(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public MyPoints(double x, double y)
        {
            X = (int)x;
            Y = (int)y;
        }
    }
}
