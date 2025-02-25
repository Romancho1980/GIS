using GIS_WinForms.Data.Math_utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data.Primitives
{
    public class Envelope
    {
        private Polygon _polygon;
        private Segment _skeleton;

        public Envelope(Segment skeleton, int width)
        {
            this._skeleton = skeleton;
            _polygon = new Polygon();

            _polygon = GeneratePolygon(width);
        }

        public void DrawEnvelope(PaintEventArgs e)
        {
            _polygon.DrawPolygon(e);
        }

        public Polygon GeneratePolygon(int width)
        {
            MyPoints p1 = new MyPoints(_skeleton.P1);
            MyPoints p2 = new MyPoints(_skeleton.P2);

            int radius = width / 2;

            double alpha = Utils.Angle(Utils.Substract(p1, p2)); // Требуется ускорение вычисления Atan2. В будущем. :)

            double alpha_cw = alpha + Math.PI / 2;
            double alpha_ccw = alpha - Math.PI / 2;

            double step = Math.PI / 3;
            double eps = step / 2;

            Polygon poly = new();

            for (double i = alpha_ccw; i < alpha_cw + eps; i += step)
            {
                poly._points.Add(Utils.Translate(p1, i, radius));
            }

            for (double i = alpha_ccw; i < alpha_cw + eps; i += step) 
            {
                poly._points.Add(Utils.Translate(p2, Math.PI + i, radius));
            }

            return poly;

            //MyPoints p1_ccw = Utils.Translate(p1, alpha_ccw, radius);
            //MyPoints p2_ccw = Utils.Translate(p2, alpha_ccw, radius);

            //MyPoints p1_cw = Utils.Translate(p1, alpha_cw, radius);
            //MyPoints p2_cw = Utils.Translate(p2, alpha_cw, radius);

            //return new Polygon([p1_cw, p2_cw, p2_ccw, p1_ccw]);
        }
    }
}