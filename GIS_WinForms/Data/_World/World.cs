using GIS_WinForms.Data.Primitives;
using GIS_WinForms.Services.Algorythm;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data._World
{
    internal class World
    {
        public int Xmin { get; set; } = 350;
        public int Ymin { get; set; } = 150;
        public int Xmax { get; set; } = 1500;
        public int Ymax { get; set; } = 750;


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

        internal void AddPoint(Vertices vert)
        {
            world_vertices.Add(vert);   
            //world_vertices.Add(new Vertices(vert.X, vert.Y));
        }


        private bool ContainsPoint(Vertices vert)
        {
            //if (world_vertices.Contains(vert) == true) return true;

            //return false;

            if (world_vertices!= null)
                foreach(var vertices in world_vertices)
                {
                    if ((vertices.X == vert.X) && (vertices.Y == vert.Y)) return true;
                }

            return false;
        }

        internal bool TryAddPoint(Vertices vertices)
        {
            Debug.WriteLine($" Количество точек : {world_vertices.Count}");

            if (ContainsPoint(vertices) == false) // если точки нет в List , то
            {
                AddPoint(vertices);             // Добавляем точку
                return true;
            }
            
            return false;

        }

        internal bool TryAddSegment(Segment segment)
        {
            if (ContainSegment(segment) == false) // Если нет сегмента в списке
            {
                AddSegment(segment);              // то добавляем к коллекцию
                return true;
            }
            return false;
        }

        private void AddSegment(Segment segment)
        {
            world_segments.Add(segment);
        }

        private bool ContainSegment(Segment segment)
        {
            if (world_segments != null)
                foreach (var seg in world_segments)
                {
                    if (((seg.P1.X == segment.P1.X) && (seg.P1.Y == segment.P1.Y) &&
                        (seg.P2.X == segment.P2.X) && (seg.P2.Y == segment.P2.Y)) ||

                            ((seg.P1.X == segment.P2.X) && (seg.P1.Y == segment.P2.Y) &&
                            (seg.P2.X == segment.P1.X) && (seg.P2.Y == segment.P1.Y)))
                    {
                        return true;
                    }
                   // if ((vertices.X == vert.X) && (vertices.Y == vert.Y)) return true;
                }

            return false;
        }
    }
}
