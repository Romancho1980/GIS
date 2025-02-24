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
        // Нахождение ближайшей точки из коллекции point к точке с координатами loc
        public static Vertices? getNearestPoint(Vertices loc, List<Vertices> points, int threshold = Int32.MaxValue)
        {
            var minDist = Int32.MaxValue;
            Vertices? nearest = null;

            foreach (var point in points)
            {
               // double dist = distance(point, loc);
                double dist = distance_wo_sqrt(point, loc);
                if (dist < minDist && dist < threshold)
                {
                    minDist = Convert.ToInt32(dist);
                    nearest = point;
                }
            }
            return nearest;
        }

        // Вычитание векторов
        public static Vertices Substract(Vertices p1, Vertices p2)
        {
            return new Vertices(p1.point.X - p2.point.X, p1.point.Y - p2.point.Y);
        }

        // Сложение векторов
        public static Vertices Add(Vertices p1, Vertices p2)
        {
            return new Vertices(p1.point.X + p2.point.X, p1.point.Y + p2.point.Y);
        }

        // Расстояние между двумя точками, в данном методе извлекаю квадратный корень
        private static double distance(Vertices p1, Vertices p2)
        {
            double dx = (p1.point.X - p2.point.X) * (p1.point.X - p2.point.X);
            double dy = (p1.point.Y - p2.point.Y) * (p1.point.Y - p2.point.Y);
            return Math.Sqrt(dx + dy);

        }

        // Расстояние между точками.
        // Не вычисляю квардратный корень для быстродействия, т.к. метод применяется для нахождения ближаёшей точки, 
        // поэтому решил (точнее DeepSeek подсказал) не извлекать корень
        private static double distance_wo_sqrt(Vertices p1, Vertices p2)
        {
            double dx = (p1.point.X - p2.point.X) * (p1.point.X - p2.point.X);
            double dy = (p1.point.Y - p2.point.Y) * (p1.point.Y - p2.point.Y);
            return (dx + dy);
        }

        public static Vertices Scale(Vertices p, float scaler)
        {
            // return new Vertices(Convert.ToInt32(p.X * scaler), Convert.ToInt32(p.Y * scaler));
            return new Vertices((Int32)(p.point.X * scaler), (Int32)(p.point.Y * scaler));
        }
    }
}
