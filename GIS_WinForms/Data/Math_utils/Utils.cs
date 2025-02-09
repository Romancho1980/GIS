using GIS_WinForms.Data.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data.Math_utils
{
    public static class Utils
    {
        public static Vertices? getNearestPoint(Vertices loc, List<Vertices> points, int threshold = Int32.MaxValue)
        {
            var minDist = Int32.MaxValue;
            Vertices? nearest = null;

            foreach (var point in points)
            {
                double dist = distance(point, loc);
                if (dist < minDist && dist < threshold)
                {
                    minDist = Convert.ToInt32(dist);
                    nearest = point;
                }
            }
            return nearest;
        }

        private static double distance(Vertices p1, Vertices p2)
        {
            double dx = (p1.X - p2.X) * (p1.X - p2.X);
            double dy = (p1.Y - p2.Y) * (p1.Y - p2.Y);
            return Math.Sqrt(dx + dy);

        }
    }
}
