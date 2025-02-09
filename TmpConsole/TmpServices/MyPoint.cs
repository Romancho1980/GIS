using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpConsole.TmpServices
{
    internal class MyPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MyPoint()
        {
            X = 0;
            Y = 0;
        }

        public MyPoint(int x, int y)
        {
            X = x;
            Y = y;
        }


        public void Change(MyPoint point,int num)
        {
            point.X = num;
            point.Y = num;
        }
    }
}
