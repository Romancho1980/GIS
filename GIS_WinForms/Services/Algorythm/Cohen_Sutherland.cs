using GIS_WinForms.Data.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Services.Algorythm
{
    public class Cohen_Sutherland
    {
        private List<Segment> _line;

        // Координаты Viewport'а
        private List<Vertices> _viewPort;

        private int Xmin;
        private int Ymin;
        private int Xmax;
        private int Ymax;



        byte LEFT_OF_VIEWPORT = 1; // левее
        byte RIGHT_OF_VIEWPORT = 2; // правее
        byte BELOW_OF_VIEWPORT = 8; // ниже
        byte ABOVE_OF_VIEWPORT = 4; // выше


        public Cohen_Sutherland()
        {
            _line = new List<Segment>();
            _viewPort= new List<Vertices>();
        }

        public void SetViewport(List<Vertices> Viewport)
        {
            _viewPort = Viewport;

            Xmin = _viewPort[0].X;
            Ymin = _viewPort[0].Y;
            Xmax = _viewPort[2].X;
            Ymax = _viewPort[2].Y;
        }

        public Cohen_Sutherland(List<Vertices> viewPort)
        {
            _line = new List<Segment>();
            _viewPort = viewPort;

            Xmin = _viewPort[0].X;
            Ymin = _viewPort[0].Y;
            Xmax = _viewPort[2].X;
            Ymax = _viewPort[2].Y;
        }
        public Cohen_Sutherland(List<Segment> line, List<Vertices> viewPort)
        {
            _line = line;
            _viewPort = viewPort;


            Xmin = _viewPort[0].X;
            Ymin = _viewPort[0].Y;
            Xmax = _viewPort[2].X;
            Ymax = _viewPort[2].Y;
        }

        //public List<Point2D> ClipLine()
        public void ClipLine()
        {
            //foreach(var pt in _line) 
            //{
            //    Check_Line(pt,);
            //}

            for (int i = 0; i < _line.Count(); i++)
            {
                //Check_Line(_line[i],i);
                if (Check_Line_ver2(_line[i], i) == true) _line[i].Visible = true;
                else _line[i].Visible = false;
            }
        }


        public int ComputeCode(Vertices point)
        {


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

        private bool Check_Line_ver2(Segment line, int index)
        {
            int Code_P1 = ComputeCode(line.P1);
            int Code_P2 = ComputeCode(line.P2);
            int result=Code_P1 & Code_P2;

            bool accept = false;
            while (true)
            {
                if ((Code_P1 == 0) && (Code_P2 == 0))
                {
                    // отрезок полностью видим
                    accept = true;
                    break;
                }
                else if ((Code_P1 & Code_P2) != 0)
                {
                    //отрезок полностью невидим
                    accept = false;
                    break;
                }
                else
                {
                    int x = 0, y = 0;
                    int codeOut = Code_P1 != 0 ? Code_P1 : Code_P2;

                    if ((codeOut & ABOVE_OF_VIEWPORT) != 0)
                    {
                        x = line.P1.X + (line.P2.X - line.P1.X) * (Ymax - line.P1.Y) / (line.P2.Y - line.P1.Y);
                        y = Ymax;
                    }
                    else if ((codeOut & BELOW_OF_VIEWPORT) != 0)
                    {
                        x = line.P1.X + (line.P2.X - line.P1.X) * (Ymin - line.P1.Y) / (line.P2.Y - line.P1.Y);
                        y = Ymin;
                    }
                    else if ((codeOut & RIGHT_OF_VIEWPORT) != 0)
                    {
                        y = line.P1.Y + (line.P2.Y - line.P1.Y) * (Xmax - line.P1.X) / (line.P2.X - line.P1.X);
                        x = Xmax;
                    }
                    else if ((codeOut & LEFT_OF_VIEWPORT) != 0)
                    {
                        y = line.P1.Y + (line.P2.Y - line.P1.Y) * (Xmin - line.P1.X) / (line.P2.X - line.P1.X);
                        x = Xmin;
                    }

                    if (codeOut == Code_P1)
                    {
                        line.P1_Clip.X = x;
                        line.P1_Clip.Y = y;

                        //line.P1.X = x;
                        //line.P1.Y = y;

                        Code_P1 = ComputeCode(line.P1_Clip);
                    }
                    else
                    {
                        line.P2_Clip.X = x;
                        line.P2_Clip.Y = y;

                        //line.P2.X = x;
                        //line.P2.Y = y;

                        Code_P2 = ComputeCode(line.P2_Clip);
                    }
                }
            }
            return accept;
        }
        private void Check_Line(Segment line, int index)
        {
            int[] tmp = new int[3];

            //foreach(var pt in viewport_lines)

            int num1 = ComputeCode(line.P1);
            int num2 = ComputeCode(line.P2);
            if ((num1 == 0) && (num2 == 0))
            {
                line.descr = "Inside ViewPort";
                line.Visible = true;
            }
            else
                if ((num1 & num2) != 0)
            {
                line.descr = "Не пересекает ViewPort";
                line.Visible = false;

            }
            else
            //     if ((num1 & num2) != 0)
            {
                line.descr = "Возможно пересекает ViewPort";
                line.Visible = true; ;
                int codeA = ComputeCode(line.P1);
                int codeB = ComputeCode(line.P2);
                int code;
                Vertices C = new();
                Vertices newP1, newP2;
                newP1 = new Vertices();
                newP2 = new Vertices();

                //newP1=line.P1;
                newP1.X = line.P1.X;
                newP1.Y = line.P1.Y;

                newP2.X = line.P2.X;
                newP2.Y = line.P2.Y;

                // Определяем координаты пересечения прямой - line и Viewport'а

                //bool asd;

                while ((codeA != 0) || (codeB != 0))
                {
                    if ((codeA & codeB) != 0)
                    {
                        break;
                    }
                    if (codeA != 0)
                    {
                        code = codeA;

                        C.X = line.P1.X;
                        C.Y = line.P1.Y;
                        //C = line.P1;
                    }
                    else
                    {
                        code = codeB;
                        C = line.P2;
                    }

                    if (code == LEFT_OF_VIEWPORT)
                    {
                        C.Y += (newP1.Y - newP2.Y) * (Xmin - C.X) / (newP1.X - newP2.X);
                        C.X = Xmin;
                    }
                    else
                    if (code == RIGHT_OF_VIEWPORT)
                    {

                    }
                    else
                    if (code == ABOVE_OF_VIEWPORT)
                    {

                    }
                    else
                    if (code == BELOW_OF_VIEWPORT)
                    {
                        // точка расположена ниже viewport'a
                        C.X += (newP1.X - newP2.X) * (Ymin - C.Y) / (newP1.Y - newP2.Y);
                        C.Y = Ymin;
                    }

                    if (code == codeA)
                    {
                        codeA = ComputeCode(C);
                        newP1 = C;
                        _line[index].P1 = newP1;
                    }
                    else
                    {
                        codeB = ComputeCode(C);
                        newP2 = C;
                        _line[index].P2 = newP2;

                    }
                }

            }
        }
        public void ClipSegment(Segment seg)
        {
            if (Check_Line_ver2(seg, 0) == true) seg.Visible = true;
            else seg.Visible = false;


        }
    }
}
