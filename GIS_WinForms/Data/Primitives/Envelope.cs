using GIS_WinForms.Data.Math_utils;
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
        Polygon _polygon;

        public Envelope(Segment skeleton,int width)
        {
            this._skeleton = skeleton;
            _polygon = new Polygon();

            _polygon=GeneratePolygon(width);
        }

        public Polygon GeneratePolygon(int width)
        {
            // const {p1,p2} = this.skeleton
            MyPoints p1 = new MyPoints(_skeleton.P1);
            MyPoints p2= new MyPoints(_skeleton.P2);

            int radius = width / 2;
            //double alpha = Math.Atan2(p1.Y - p2.Y, p1.X - p2.X);

            double alpha = Utils.Angle(Utils.Substract(p1, p2));

            double alpha_cw = alpha + Math.PI / 2;
            double alpha_ccw = alpha - Math.PI / 2;

            MyPoints p1_ccw = Utils.Translate(p1, alpha_ccw, radius);
            MyPoints p2_ccw = Utils.Translate(p2, alpha_ccw, radius);

            MyPoints p1_cw = Utils.Translate(p1, alpha_cw, radius);
            MyPoints p2_cw = Utils.Translate(p2, alpha_cw, radius);

            return new Polygon([p1_ccw, p2_ccw, p2_cw, p1_cw]);
        }

        public void DrawEnvelope(PaintEventArgs e)
        {
            _polygon.DrawPolygon(e);
        }
    }
}
