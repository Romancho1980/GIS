using GIS_WinForms.Data.Primitives;
using GIS_WinForms.Services.Algorythm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data._World
{
    internal class World
    {
        int Xmin = 350;
        int Ymin = 150;
        int Xmax = 1500;
        int Ymax = 750;

        public List<Vertices> viewport_points;

        public List<Segment> world_segments;
        public List<Vertices> world_vertices;

        private Cohen_Sutherland _cohen_Sutherland;


        // public Graphics graphics { get; set; }

        private void fill_viewport()
        {
            viewport_points.Add(new Vertices(Xmin, Ymin));
            viewport_points.Add(new Vertices(Xmax, Ymin));
            viewport_points.Add(new Vertices(Xmax, Ymax));
            viewport_points.Add(new Vertices(Xmin, Ymax));
        }

        private void fill_world_segments()
        {
            //world_segments.Add(new Segment(380,30,850,600));
            //world_segments.Add(new Segment(850,600,100,600));
            //world_segments.Add(new Segment(100,600,380,30));


            world_segments.Add(new Segment(world_vertices[0], world_vertices[1]));
            world_segments.Add(new Segment(world_vertices[0], world_vertices[2]));
            world_segments.Add(new Segment(world_vertices[0], world_vertices[3]));
            world_segments.Add(new Segment(world_vertices[1], world_vertices[2]));

        }
        private void fill_world_vertices()
        {
            //world_vertices.Add(new Vertices(380, 30));
            //world_vertices.Add(new Vertices(850, 600));
            //world_vertices.Add(new Vertices(100, 600));

            world_vertices.Add(new Vertices(200,200));
            world_vertices.Add(new Vertices(500, 200));
            world_vertices.Add(new Vertices(400, 400));
            world_vertices.Add(new Vertices(100, 300));
        }

        private void InitCohen_SutherlandAlgor()
        {
            _cohen_Sutherland.SetViewport(viewport_points);
        }

        private void Generate_World()
        {
            fill_viewport();
            fill_world_vertices();
            fill_world_segments();

            InitCohen_SutherlandAlgor();
        }


        public World()
        {
            viewport_points = new List<Vertices>();
            world_segments = new List<Segment>();
            world_vertices = new List<Vertices>();

            _cohen_Sutherland = new Cohen_Sutherland();

            Generate_World();
        }

        public World(int width, int height)
        {
            Xmin = 0;
            Ymin = 0;

            Xmax = width;
            Ymax = height;

            viewport_points = new List<Vertices>();
            world_segments = new List<Segment>();
            world_vertices = new List<Vertices>();

            _cohen_Sutherland = new Cohen_Sutherland();

            Generate_World();

        }

        public void Draw(PaintEventArgs e)
        {
            if (e != null)
            {
                Draw_Segments(e);
                Draw_Vertices(e);
            }
        }

        private bool isPointInViewport(Vertices vert)
        {
            if ((vert.X >= Xmin) & (vert.X <= Xmax))
                if ((vert.Y >= Ymin) & (vert.Y <= Ymax))
                    return true;

            return false;
        }

        private void Draw_Vertices(PaintEventArgs e)
        {
            // throw new NotImplementedException();
            foreach (var vert in world_vertices)
            {
                if (isPointInViewport(vert) == true)
                    vert.Draw(e,30,"Black");
            }
        }

        private void Draw_Segments(PaintEventArgs e)
        {
            foreach (var seg in world_segments) 
            {
                //seg.ClipAndDrawSegment(e);
                _cohen_Sutherland.ClipSegment(seg);
                if (seg.Visible!= false) seg.Draw(e);
            }

        }
    }
}
