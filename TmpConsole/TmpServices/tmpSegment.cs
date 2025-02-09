using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmpConsole.TmpServices
{
    internal class tmpSegment
    {
        public List<MyPoint> points;
        public tmpSegment()
        {
            points = new List<MyPoint>();
            points.Add(new MyPoint(10, 10));
            points.Add(new MyPoint(20, 20));
            points.Add(new MyPoint(30, 30));
            points.Add(new MyPoint(40, 40));

            var tmp = new MyPoint(50, 50);
            tmp.Change(tmp, 100);
        }
    }
}
