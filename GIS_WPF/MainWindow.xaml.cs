using GIS_WPF.Data.Primitives;
using GIS_WPF.Data.Services;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GIS_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         * Координаты ViewPort
         */
        int Xmin = 350;
        int Ymin = 150;
        int Xmax = 1500;
        int Ymax = 750;

        List<Point2D> viewport_points = new List<Point2D>();
        List<Point2D> buffer_points   = new List<Point2D>();

        List<Line2D> viewport_lines = new List<Line2D>();
        List<Line2D> buffer_lines   = new List<Line2D>();

        Cohen_Sutherland cohen;
        public MainWindow()
        {
            viewport_points.Add(new Point2D(Xmin, Ymin));
            viewport_points.Add(new Point2D(Xmax, Ymin));
            viewport_points.Add(new Point2D(Xmax, Ymax));
            viewport_points.Add(new Point2D(Xmin, Ymax));

            viewport_lines.Add(new Line2D(viewport_points[0], viewport_points[1]));
            viewport_lines.Add(new Line2D(viewport_points[1], viewport_points[2]));
            viewport_lines.Add(new Line2D(viewport_points[2], viewport_points[3]));
            viewport_lines.Add(new Line2D(viewport_points[3], viewport_points[0]));

            buffer_points.Add(new Point2D(380, 30));
            buffer_points.Add(new Point2D(950, 900));
            buffer_points.Add(new Point2D(100,  300));


            buffer_lines.Add(new Line2D(buffer_points[0], buffer_points[1]));
            buffer_lines.Add(new Line2D(buffer_points[1], buffer_points[2]));
            buffer_lines.Add(new Line2D(buffer_points[2], buffer_points[0]));

            cohen=new Cohen_Sutherland(buffer_lines,viewport_points);

            cohen.ClipLine();

            InitializeComponent();
         //   Check_Lines();
            Draw();
        }

        public int getXorY(Point2D point)
        {
            int number = 0;
            if (point.X < Xmin)
                number = 1;
            else
                if (point.X > Xmax)
                number = 2;

            if (point.Y < Ymin)
                number = number | 8;
            else
                if (point.Y > Ymax)
                number = number | 4;

            return number;
        }

        public void Cohen(Line2D line)
        {
            int num1 = getXorY(line.P1);
            int num2 = getXorY(line.P2);
            if ((num1 == 0) && (num2 == 0))
            {
                line.descr = "inside viewport";
                line.Visible = true;
            }
            else
                if ((num1 & num2) != 0)
            {
                line.descr = "не пересекает viewport";
                line.Visible = false ;

            }
            else
               // if ((num1 & num2) != 0)
            {
                line.descr = "возможно пересекает viewport";
                line.Visible = true;

            }

            if (line.Visible==true) //Пересекает и находим точки пересечения с Viewport'ом
            {
                for(int i = 0; i < 4; i++)
                {
                    int num=getXorY(line.P1);
                    if (num != 0) // т.е. Первая точка линии точно не попадает в Viewport
                    {

                    }
                }
            }
        }
        //private void Check_Lines()
        //{
        //    const byte LEFT_OF_VIEWPORT  = 1; // левее
        //    const byte RIGHT_OF_VIEWPORT = 2; // правее
        //    const byte ABOVE_OF_VIEWPORT = 4; // выше
        //    const byte BELOW_OF_VIEWPORT = 8; // ниже

        //    int[] tmp= new int[3];
        //    int iterrator = 0;

        //    //foreach(var pt in viewport_lines)
        //    for (int i = 0; i < buffer_lines.Count; i++) 
        //    {
        //        Cohen(buffer_lines[i]);
        //        //int num1 = getXorY(buffer_lines[i].P1);
        //        //int num2 = getXorY(buffer_lines[i].P2);
        //        //if ((num1 == 0) && (num2 == 0))
        //        //{
        //        //    buffer_lines[i].descr = "Inside ViewPort";
        //        //    buffer_lines[i].line = 1;
        //        //}
        //        //else
        //        //    if ((num1 & num2) != 0)
        //        //{
        //        //    buffer_lines[i].descr = "Не пересекает ViewPort";
        //        //    buffer_lines[i].line = 2;

        //        //}
        //        //else
        //        //    if ((num1 & num2) != 0)
        //        //{
        //        //    buffer_lines[i].descr = "Возможно пересекает ViewPort";
        //        //    buffer_lines[i].line = 3;

        //        //}
        //    }

        //    //foreach(var pt in buffer_points)
        //    //{
        //    //    if (pt.X < Xmin)
        //    //        tmp[iterrator] = 1;
        //    //    else
        //    //        if (pt.X > Xmax)
        //    //        tmp[iterrator] = 2;

        //    //    if (pt.Y < Ymin)
        //    //        tmp[iterrator] = tmp[iterrator] | 8;
        //    //    else
        //    //        if(pt.Y > Ymax)
        //    //        tmp[iterrator] = tmp[iterrator] | 4;

        //    //    iterrator++;    
        //    //}
        //}

        private void Draw()
        {
            foreach(var pt in viewport_lines)
            {
                Line line = new Line();
                line.X1 = pt.P1.X;
                line.Y1 = pt.P1.Y;
                line.X2 = pt.P2.X;
                line.Y2 = pt.P2.Y;
                line.Stroke = Brushes.Black;
                grid1.Children.Add(line);
            }
            //for (int i = 0; i < viewport_points.Count; i++)
            //{
            //    Line line = new Line();
            //    line.X1 = viewport_points[i].X;
            //    line.Y1 = viewport_points[i].Y;
            //    line.X2 = viewport_points[(i + 1) % viewport_points.Count].X;
            //    line.Y2 = viewport_points[(i + 1) % viewport_points.Count].Y;
            //    line.Stroke = Brushes.Black;
            //    grid1.Children.Add(line);

            //}

            //for (int i = 0; i < buffer_points.Count; i++)
            foreach(var pt in buffer_lines)
            {
                Line line = new Line();
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 10;
                ellipse.Height = 10;
                ellipse.Fill = Brushes.Red;
                //canvas1.Children.Add(ellipse);
                //line.X1 = buffer_points[i].X;
                //line.Y1 = buffer_points[i].Y;
                //line.X2 = buffer_points[(i + 1) % buffer_points.Count].X;
                //line.Y2 = buffer_points[(i + 1) % buffer_points.Count].Y;

                //line.X1 = pt.P1.X;
                //line.Y1 = pt.P1.Y;
                //line.X2 = pt.P2.X;
                //line.Y2 = pt.P2.Y;
                line.Stroke = Brushes.Red;

                if (pt.Visible == false) 
                    line.Stroke = Brushes.Purple;
                else
                    if (pt.Visible == true)
                    line.Stroke = Brushes.Green;
                else
                    if (pt.Visible == true)
                    line.Stroke = Brushes.Blue;

                if (pt.Visible == true)
                {
                    line.X1 = pt.clipP1.X;
                    line.Y1 = pt.clipP1.Y;
                    line.X2 = pt.clipP2.X;
                    line.Y2 = pt.clipP2.Y;

                   // grid1.Children.Add(line);
                    grid1.Children.Add(ellipse);
                }
            }
        }
    }
}