using GIS_WPF.Data.Primitives;
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
        int Xmin = 50;
        int Ymin = 50;
        int Xmax = 750;
        int Ymax = 350;

        List<Point2D> viewport = new List<Point2D>();
        List<Point2D> buffer   = new List<Point2D>();
        public MainWindow()
        {
            viewport.Add(new Point2D(Xmin, Ymin));
            viewport.Add(new Point2D(Xmax, Ymin));
            viewport.Add(new Point2D(Xmax, Ymax));
            viewport.Add(new Point2D(Xmin, Ymax));

            buffer.Add(new Point2D(150, 30));
            buffer.Add(new Point2D(250, 200));
            buffer.Add(new Point2D(10,  200));

            InitializeComponent();
            Draw();
        }

        private void Draw()
        {
            for (int i = 0; i < viewport.Count; i++)
            {
                Line line = new Line();
                line.X1 = viewport[i].X;
                line.Y1 = viewport[i].Y;
                line.X2 = viewport[(i+1)%viewport.Count].X;
                line.Y2 = viewport[(i+1)%viewport.Count].Y;
                line.Stroke = Brushes.Black;
                grid1.Children.Add(line);

            }

            for(int i = 0; i < buffer.Count; i++)
            {
                Line line = new Line();
                line.X1 = buffer[i].X;
                line.Y1 = buffer[i].Y;
                line.X2 = buffer[(i + 1) % buffer.Count].X;
                line.Y2 = buffer[(i + 1) % buffer.Count].Y;
                line.Stroke = Brushes.Red;
                grid1.Children.Add(line);
            }
            //Line vertL = new Line();
            //vertL.X1 = 10;
            //vertL.Y1 = 150;
            //vertL.X2 = 10;
            //vertL.Y2 = 10;
            //vertL.Stroke = Brushes.Black;
            //grid1.Children.Add(vertL);
        }
    }
}