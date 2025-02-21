using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_ForTests.Data
{
    public class Element
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Element(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
