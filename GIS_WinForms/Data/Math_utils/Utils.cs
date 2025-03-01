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
        public static MyPoints? getNearestPoint(MyPoints loc, List<MyPoints> points, int threshold = Int32.MaxValue)
        {
            var minDist = Int32.MaxValue;
            MyPoints? nearest = null;

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
        public static MyPoints Substract(MyPoints p1, MyPoints p2)
        {
            return new MyPoints(p1.X - p2.X, p1.Y - p2.Y);
        }

        // Сложение векторов
        public static MyPoints Add(MyPoints p1, MyPoints p2)
        {
            return new MyPoints(p1.X + p2.X, p1.Y + p2.Y);
        }

        // Середина отрезка
        public static MyPoints Average(MyPoints p1, MyPoints p2)
        {
            return new MyPoints((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }


        // Расстояние между двумя точками, в данном методе извлекаю квадратный корень
        private static double distance(MyPoints p1, MyPoints p2)
        {
            double dx = (p1.X - p2.X) * (p1.X - p2.X);
            double dy = (p1.Y - p2.Y) * (p1.Y - p2.Y);
            return Math.Sqrt(dx + dy);

        }

        // Расстояние между точками.
        // Не вычисляю квардратный корень для быстродействия, т.к. метод применяется для нахождения ближаёшей точки, 
        // поэтому решил (точнее DeepSeek подсказал) не извлекать корень
        private static double distance_wo_sqrt(MyPoints p1, MyPoints p2)
        {
            double dx = (p1.X - p2.X) * (p1.X - p2.X);
            double dy = (p1.Y - p2.Y) * (p1.Y - p2.Y);
            return (dx + dy);
        }

        public static MyPoints Scale(MyPoints p, float scaler)
        {
            // return new Vertices(Convert.ToInt32(p.X * scaler), Convert.ToInt32(p.Y * scaler));
            return new MyPoints((Int32)(p.X * scaler), (Int32)(p.Y * scaler));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="alpha_ccw"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static MyPoints Translate(MyPoints loc, double angle, int offset)
        {
            return new MyPoints(loc.X + Math.Cos(angle) * offset,
                                loc.Y + Math.Sin(angle) * offset);
        }

        public static double Angle(MyPoints p)
        {
            return Math.Atan2(p.Y, p.X);
        }

        public static double Lerp(int a, int b, double t)
        {
            return a + (b - a) * t;
        }

        public static (double X, double Y, double Offset)? getInterSection(MyPoints A, MyPoints B, MyPoints C, MyPoints D)
        {
            double tTop = (D.X - C.X) * (A.Y - C.Y) - (D.Y - C.Y) * (A.X - C.X);
            double uTop = (C.Y - A.Y) * (A.X - B.X) - (C.X - A.X) * (A.Y - B.Y);
            double bottom = (D.Y - C.Y) * (B.X - A.X) - (D.X - C.X) * (B.Y - A.Y);

            if (bottom != 0)
            {
                double t = tTop / bottom;
                double u = uTop / bottom;
                if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
                {
                    return (
                        X: Lerp(A.X, B.X, t),
                        Y: Lerp(A.Y, B.Y, t),
                        Offset: t
                        );
                }
            }

            return null;
        }

        public static string GetRandomColor()
        {
            // Создаём объект Random для генерации случайных чисел
            Random random = new Random();

            // Генерируем случайный hue в диапазоне от 290 до 550 (290 + 260)
            double hue = 290 + random.NextDouble() * 260;

            // Форматируем строку в формате HSL
            return string.Format("hsl({0},100%,60%)", hue);
        }

       
    }
}
