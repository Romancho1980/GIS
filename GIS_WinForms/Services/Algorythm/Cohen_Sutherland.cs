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

            Xmin = _viewPort[0].point.X;
            Ymin = _viewPort[0].point.Y;
            Xmax = _viewPort[2].point.X;
            Ymax = _viewPort[2].point.Y;
        }

        public Cohen_Sutherland(List<Vertices> viewPort)
        {
            _line = new List<Segment>();
            _viewPort = viewPort;

            Xmin = _viewPort[0].point.X;
            Ymin = _viewPort[0].point.Y;
            Xmax = _viewPort[2].point.X;
            Ymax = _viewPort[2].point.Y;
        }
        public Cohen_Sutherland(List<Segment> line, List<Vertices> viewPort)
        {
            _line = line;
            _viewPort = viewPort;


            Xmin = _viewPort[0].point.X;
            Ymin = _viewPort[0].point.Y;
            Xmax = _viewPort[2].point.X;
            Ymax = _viewPort[2].point.Y;
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


        public int ComputeCode(Vertices pt)
        {


            int ResultNumber = 0;

            if (pt.point.X < Xmin)
                ResultNumber = LEFT_OF_VIEWPORT;
            else
                if (pt.point.X > Xmax)
                ResultNumber = RIGHT_OF_VIEWPORT;

            if (pt.point.Y < Ymin)
                ResultNumber = ResultNumber | BELOW_OF_VIEWPORT;
            else
                if (pt.point.Y > Ymax)
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
                        x = line.P1.point.X + (line.P2.point.X - line.P1.point.X) * (Ymax - line.P1.point.Y) / (line.P2.point.Y - line.P1.point.Y);
                        y = Ymax;
                    }
                    else if ((codeOut & BELOW_OF_VIEWPORT) != 0)
                    {
                        x = line.P1.point.X + (line.P2.point.X - line.P1.point.X) * (Ymin - line.P1.point.Y) / (line.P2.point.Y - line.P1.point.Y);
                        y = Ymin;
                    }
                    else if ((codeOut & RIGHT_OF_VIEWPORT) != 0)
                    {
                        y = line.P1.point.Y + (line.P2.point.Y - line.P1.point.Y) * (Xmax - line.P1.point.X) / (line.P2.point.X - line.P1.point.X);
                        x = Xmax;
                    }
                    else if ((codeOut & LEFT_OF_VIEWPORT) != 0)
                    {
                        y = line.P1.point.Y + (line.P2.point.Y - line.P1.point.Y) * (Xmin - line.P1.point.X) / (line.P2.point.X - line.P1.point.X);
                        x = Xmin;
                    }

                    if (codeOut == Code_P1)
                    {
                        line.P1_Clip.point.X = x;
                        line.P1_Clip.point.Y = y;

                        //line.P1.X = x;
                        //line.P1.Y = y;

                        Code_P1 = ComputeCode(line.P1_Clip);
                    }
                    else
                    {
                        line.P2_Clip.point.X = x;
                        line.P2_Clip.point.Y = y;

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
                newP1.point.X = line.P1.point.X;
                newP1.point.Y = line.P1.point.Y;

                newP2.point.X = line.P2.point.X;
                newP2.point.Y = line.P2.point.Y;

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

                        C.point.X = line.P1.point.X;
                        C.point.Y = line.P1.point.Y;
                        //C = line.P1;
                    }
                    else
                    {
                        code = codeB;
                        C = line.P2;
                    }

                    if (code == LEFT_OF_VIEWPORT)
                    {
                        C.point.Y += (newP1.point.Y - newP2.point.Y) * (Xmin - C.point.X) / (newP1.point.X - newP2.point.X);
                        C.point.X = Xmin;
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
                        C.point.X += (newP1.point.X - newP2.point.X) * (Ymin - C.point.Y) / (newP1.point.Y - newP2.point.Y);
                        C.point.Y = Ymin;
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

        public void ChangeViewportSize(int xmax, int ymax)
        {
            Xmax = xmax;
            Ymax = ymax;
        }
    }
}
