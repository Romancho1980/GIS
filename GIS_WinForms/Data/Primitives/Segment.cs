using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data.Primitives
{
    public class Segment
    {
        public Vertices P1 { get; set; }
        public Vertices P2 { get; set; }


        /// P1_Clip и P2_Clip 
        /// Отрезок, который получен после алгоритма Cohen_Sutherland
        /// То есть- это точки, где отрезок пересекает Viewport
        public Vertices P1_Clip { get; set; } 
        public Vertices P2_Clip { get; set; }  

        public PointF P1pointF {  get; set; }
        public PointF P2pointF {  get; set; }

        // Пересекает ли отрезок Viewport ?
        public bool Visible { get; set; } = true;

        // Просто описание чего-то
        public string descr { get; set; } = "";



        public Segment()
        {
            P1 = new Vertices();
            P2 = new Vertices();
            P1_Clip = new Vertices();
            P2_Clip = new Vertices();

            P1pointF= new PointF(); 
            P2pointF= new PointF(); 

        }
        public Segment(Vertices p1, Vertices p2)
        {
            P1 = new Vertices(p1.X, p1.Y);
            P2 = new Vertices(p2.X, p2.Y);

            P1_Clip=new Vertices(p1.X, p1.Y);
            P2_Clip=new Vertices(p2.X, p2.Y);


            P1pointF = new PointF(p1.X,p1.Y);
            P2pointF = new PointF(p2.X,p2.Y);
        }

        public Segment(int x1, int y1, int x2, int y2)
        {
            P1=new Vertices(x1,y1);
            P2=new Vertices(x2,y2);

            P1_Clip= new Vertices(x1,y1);
            P2_Clip = new Vertices(x2, y2);

            P1pointF = new PointF(x1, y1);
            P2pointF = new PointF(x2, y2);
        }

        public void Draw(PaintEventArgs e, int width = 2, string color = "black")
        {
            // Draw Lipped by Cohen_Sutherland Algorythm

            //e.Graphics.DrawLine(Pens.Black, new PointF(P1_Clip.X,P1_Clip.Y),
            //                                new PointF(P2_Clip.X,P2_Clip.Y));


            e.Graphics.DrawLine(Pens.Black, P1pointF,P2pointF);
        }


        internal void ClipAndDrawSegment(PaintEventArgs e)
        {
            if (Visible==true) 
            {

            }
        }
    }
}
