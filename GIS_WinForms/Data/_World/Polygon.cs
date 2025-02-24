using GIS_WinForms.Data._World.AUX_Classes;
using GIS_WinForms.Data.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data._World
{
    public class Polygon
    {
        public List<Point> points;

        public Polygon(List<Vertices> pts)
        {
            //this.points = pts.
        }

        public void Draw(PaintEventArgs e, PolyOptions? polyOptions = null)
        {
            if (polyOptions == null)
            {
                polyOptions = new PolyOptions
                {
                    Stroke = "blue",
                    LineWidth = 2,
                    Fill = Color.FromArgb(0, 0, 255, (int)(255 * 0.3)),
                };
            }
            Point[] pts=points.ToArray();
            Color fillcolor = Color.FromArgb(0, 0, (int)(255 * 0.3));
            Color col = Color.Red ;

            if (polyOptions.Stroke == "blue") col = Color.Blue;
            Pen pen = new Pen(col);

            Brush brush = new SolidBrush(fillcolor);
            e.Graphics.FillPolygon(Brushes.Yellow,pts);

            // Рисуем контур
            e.Graphics.DrawPolygon(Pens.Black, pts);
        }
    }
}
