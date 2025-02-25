using GIS_WinForms.Data.Primitives.AUX_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data.Primitives
{
    public class Polygon
    {
        Point[] _vertices;
        List<MyPoints> _points;

        public Polygon()
        {
            _points = new List<MyPoints>();
        }

        public Polygon(List<MyPoints> myPoints) : this()
        {
            _points = myPoints;
            //_points=new MyPoints[myPoints.Count];
        }

        private void ConvertListToPoint(List<MyPoints> myPoints)
        {
            _vertices = new Point[myPoints.Count];
            for (int i = 0; i < myPoints.Count; i++)
            {
                _vertices[i] = new Point(myPoints[i].X, myPoints[i].Y);
            }


        }

        public void DrawPolygon(PaintEventArgs e, PolyOptions? polyOptions = null)
        {
            if (polyOptions == null)
            {
                polyOptions = new PolyOptions
                {
                    Stroke = "blue",
                    LineWidth = 2,
                    Fill = Color.FromArgb((int)(255 * 0.3), 0, 0, 255)
                };
            }
            //Point[] pts = _pointsList.ToArray();
            ConvertListToPoint(_points);
            Color fillcolor = Color.FromArgb((int)(255*0.3), 0, 0, 255);
            Color col = Color.Yellow;

            if (polyOptions.Stroke == "blue") col = Color.Blue;

            col = Color.Red;
            Pen pen = new Pen(col);

            Brush brush = new SolidBrush(fillcolor);
            e.Graphics.FillPolygon(brush, _vertices);

            // Рисуем контур
            e.Graphics.DrawPolygon(pen, _vertices);
        }
    }
}
