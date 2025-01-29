using GIS_WPF.Data.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WPF.Data.Services
{
    /// <summary>
    /// Алгоритм Коэна — Сазерленда
    /// Алгоритм отсечения отрезков, то есть алгоритм, позволяющий определить часть отрезка, 
    /// которая пересекает прямоугольник. Был разработан Дэном Коэном и Айвеном Сазерлендом
    /// в Гарварде в 1966—1968 гг., и опубликован на конференции AFIPS в 1968
    /// </summary>
    public class Cohen_Sutherland
    {
        // Отрезок, которые надо разрезать
        private List<Line2D> _line;

        // Координаты Viewport'а
        private List<Point2D> _viewPort;

        private int Xmin;
        private int Ymin;
        private int Xmax;
        private int Ymax;


        public Cohen_Sutherland(List<Line2D> line,List<Point2D> viewPort)
        {
            _line = line;
            _viewPort = viewPort;


            Xmin = _viewPort[0].X;
            Ymin = _viewPort[0].Y;
            Xmax = _viewPort[2].X;
            Ymax = _viewPort[2].Y;
        }

        public List<Point2D> ClipLine()
        {
            foreach(var pt in _line) 
            {
                Check_Line(pt);
            }

            return null;
        }

        public int getXorY(Point2D point)
        {
            byte LEFT_OF_VIEWPORT = 1; // левее
            byte RIGHT_OF_VIEWPORT = 2; // правее
            byte ABOVE_OF_VIEWPORT = 4; // выше
            byte BELOW_OF_VIEWPORT = 8; // ниже

            int ResultNumber = 0;

            if (point.X < Xmin)
                ResultNumber = LEFT_OF_VIEWPORT;
            else
                if (point.X > Xmax)
                ResultNumber = RIGHT_OF_VIEWPORT;

            if (point.Y < Ymin)
                ResultNumber = ResultNumber | BELOW_OF_VIEWPORT;
            else
                if (point.Y > Ymax)
                ResultNumber = ResultNumber | ABOVE_OF_VIEWPORT;

            return ResultNumber;
        }
        private void Check_Line(Line2D line)
        {
            int[] tmp = new int[3];

            //foreach(var pt in viewport_lines)

                int num1 = getXorY(line.P1);
                int num2 = getXorY(line.P2);
                if ((num1 == 0) && (num2 == 0))
                {
                    line.descr = "Inside ViewPort";
                    line.line = 1;
                }
                else
                    if ((num1 & num2) != 0)
                {
                    line.descr = "Не пересекает ViewPort";
                    line.line = 2;

                }
                else
                    if ((num1 & num2) != 0)
                {
                    line.descr = "Возможно пересекает ViewPort";
                    line.line = 3;
                }
            }
        }
    }
