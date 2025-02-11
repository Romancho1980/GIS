using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpConsole.Sandbox
{
    internal class vert//:ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public vert()
        {
            X = 0;
            Y = 0;
        }
        public vert(int x,int y)
        {
            X = x;
            Y = y; 
        }


        public override string ToString()
        {
            return $"\t{X} : {Y}";
        }

        //public object Clone()
        //{
        //  //  return new vert { X=this.X, Y=this.Y};
        //}
    }
}
