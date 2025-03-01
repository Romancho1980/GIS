using GIS_WinForms.Data.Math_utils;
using GIS_WinForms.Data.Primitives.AUX_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data.Primitives
{
    public class Envelope
    {
        private Segment _skeleton;
        public Polygon _polygon;

        public Envelope(Segment skeleton,int width,int roundness=1)
        {
            this._skeleton = skeleton;
            _polygon = new Polygon();

            _polygon=GeneratePolygon(width,roundness);
        }

        public Polygon GeneratePolygon(int width, int roundness)
        {
            MyPoints p1 = new MyPoints(_skeleton.P1);
            MyPoints p2 = new MyPoints(_skeleton.P2);

            int radius = width / 2;

            double alpha = Utils.Angle(Utils.Substract(p1, p2)); // Требуется ускорение вычисления Atan2. В будущем. :)

            double alpha_cw = alpha + Math.PI / 2;
            double alpha_ccw = alpha - Math.PI / 2;

            double step = Math.PI / Math.Max(1, roundness);

            double eps = step / 2;

            List < MyPoints> poly = new();

            for (double i = alpha_ccw; i < alpha_cw + eps; i += step)
            {
                poly.Add(Utils.Translate(p1, i, radius));
            }

            for (double i = alpha_ccw; i < alpha_cw + eps; i += step)
            {
                poly.Add(Utils.Translate(p2, Math.PI + i, radius));
            }

            return new Polygon(poly);
        }

        public void DrawEnvelope(PaintEventArgs e,PolyOptions options)
        {
            _polygon.DrawPolygon(e,options);
            //_polygon.DrawSegments(e);
        }
    }
}
