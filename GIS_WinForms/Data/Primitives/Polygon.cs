using GIS_WinForms.Data.Math_utils;
using GIS_WinForms.Data.Primitives.AUX_Classes;

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

            _segments = new List<Segment>();

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
            // Color fillcolor = Color.FromArgb((int)(255*0.3), 0, 0, 255);

            Color fillcolor = polyOptions.Fill;
            Color col = Color.Yellow;

            if (polyOptions.Stroke == "blue") col = Color.Blue;
            else
                if (polyOptions.Stroke.Equals("#BBB"))
                col = Color.FromArgb(0xB, 0xB, 0xB);
            else
                if (polyOptions.Stroke.Equals("red"))
                col = Color.Red;


            //col = Color.Red;
            Pen pen = new Pen(col, polyOptions.LineWidth);

            Brush brush = new SolidBrush(fillcolor);
            e.Graphics.FillPolygon(brush, _vertices);

            // Рисуем контур
            e.Graphics.DrawPolygon(pen, _vertices);
        }


        public void DrawSegments(PaintEventArgs e)
        {
            foreach (var seg in _segments)
            {
                seg.Draw(e, 5, "random");
            }
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
                    if (inter.HasValue == true && inter.Value.Offset != 1 && inter.Value.Offset != 0)
                    {
                        MyPoints point = new(inter.Value.X, inter.Value.Y);
                        intersection.Add(point);

                        MyPoints aux = segm1[i].P2;
                        segm1[i].P2 = point;
                        //segm1.RemoveRange(i + 1, 0);
                        segm1.Insert(i + 1, new Segment(point, aux));

                        aux = segm2[j].P2;
                        segm2[j].P2 = point;
                        //segm2.RemoveRange(j + 1, 0);
                        segm2.Insert(j + 1, new Segment(point, aux));
                    }
                }
            return intersection;
        }

        public static List<Segment> Union(List<Polygon> polys)
        {
            Polygon.multiBreak(polys);
            List<Segment> Keptsegments = new List<Segment>();

            for (int i = 0; i < polys.Count; i++)
                foreach (Segment seg in polys[i]._segments)
                {
                    bool kept = true;
                    for (int j = 0; j < polys.Count; j++)
                    {
                        if (i != j)
                        {
                            if (polys[j].containsSegment(seg))
                            {
                                kept = false;
                                break;
                            }
                        }
                    }
                    if (kept == true)
                    {
                        Keptsegments.Add(seg);
                    }

                }
            return Keptsegments;
        }
        public bool containsSegment(Segment seg)
        {
            MyPoints midpoint = Math_utils.Utils.Average(seg.P1, seg.P2);
            return this.containsPoint(midpoint);
        }

        public bool containsPoint(MyPoints midpoint)
        {
            MyPoints outerPoint = new MyPoints(-1000, -1000);
            int intersectionCount = 0;
            foreach (var seg in _segments)
            {
                var intersect = Math_utils.Utils.getInterSection(outerPoint, midpoint, seg.P1, seg.P2);
                if (intersect != null)
                {
                    intersectionCount++;
                }
            }

            return ((intersectionCount % 2) == 1);
        }

        public static void multiBreak(List<Polygon> polys)
        {
            for (int i = 0; i < polys.Count - 1; i++)
                for (int j = i; j < polys.Count; j++)
                    Polygon.breakPolygon(polys[i], polys[j]);
        }
    }
}
