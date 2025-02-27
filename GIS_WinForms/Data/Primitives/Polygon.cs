using GIS_WinForms.Data.Math_utils;
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
        public List<MyPoints> _points;

        public List<Segment> _segments;

        public Polygon()
        {
            _points = new List<MyPoints>();
        }

        public Polygon(List<MyPoints> myPoints)
        {
            _points = myPoints;

            _segments=new List<Segment>();

            for (int i = 1; i < myPoints.Count + 1; i++) 
            {
                _segments.Add(new Segment(myPoints[i - 1], myPoints[i % myPoints.Count]));
            }
            //_points=new MyPoints[myPoints.Count];
        }

        public void ConvertListToPoint(List<MyPoints> myPoints)
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
            Pen pen = new Pen(col,polyOptions.LineWidth);

            Brush brush = new SolidBrush(fillcolor);
            e.Graphics.FillPolygon(brush, _vertices);

            // Рисуем контур
            e.Graphics.DrawPolygon(pen, _vertices);
        }



        public static List<MyPoints> breakPolygon(Polygon poly1, Polygon poly2)
        {
            var segm1 = poly1._segments;
            var segm2 = poly2._segments;
            List<MyPoints> intersection = new List<MyPoints>();

            for (int i = 0; i < segm1.Count; i++) 
                for (int j = 0; j < segm2.Count; j++) 
                {
                    var inter = Utils.getInterSection(segm1[i].P1, segm1[i].P2,
                                                     segm2[j].P1, segm2[j].P2);

                    double off;
                    if (inter.HasValue == true) 
                        off = inter.Value.Offset;
                    if (inter.HasValue == true && inter.Value.Offset !=1 && inter.Value.Offset!=0)
                    {
                        MyPoints point = new(inter.Value.X,inter.Value.Y);
                        intersection.Add(point);
                    }
                }
            return intersection;
        }
    }
}
