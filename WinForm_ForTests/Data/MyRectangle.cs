using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_ForTests.Data
{
    internal class MyRectangle
    {
        public int X1 { get; set; } = 0;
        public int X2 { get; set; } = 0;
        public int Y1 { get; set; } = 0;
        public int Y2 { get; set; } = 0;

        public MyRectangle(int x1, int y1, int x2, int y2)
        {
            X1= x1;
            X2= x2;
            Y1= y1;
            Y2= y2;
        }
    }
}
