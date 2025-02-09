using GIS_WinForms.Data.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data._World
{
    internal class Graph
    {
        int Xmin = 350;
        int Ymin = 150;
        int Xmax = 1500;
        int Ymax = 750;

        public List<Vertices> viewport_points;

        public List<Segment> segments;
        public List<Vertices> vertices;

        public Graph(List<Vertices> vert, List<Segment> seg)
        {
            if (vert != null)
            {
                vertices = new List<Vertices>();
                vertices = vert;
            }
            if (seg != null)
            {
                segments = new List<Segment>();
                segments = seg;
            }
        }

        public Graph(int width,int height)
        {

            Xmin = 0;
            Ymin = 0;

            Xmax = width;
            Ymax = height;

            viewport_points = new List<Vertices>();
            segments = new List<Segment>();
            vertices = new List<Vertices>();

            //_cohen_Sutherland = new Cohen_Sutherland();

            Generate_World();

        }
    }
}
