using GIS_WinForms.Data.Primitives;
using GIS_WinForms.Services.Algorythm;
using System.Diagnostics;

namespace GIS_WinForms.Data._World
{
    public class Graph
    {
        public int Xmin { get; set; } = 350;
        public int Ymin { get; set; } = 150;
        public int Xmax { get; set; } = 1500;
        public int Ymax { get; set; } = 750;


        public int XmaxScaled { get; set; }
        public int YmaxScaled { get; set; }

        public List<Vertices> viewport_points;

        public List<Segment> segments;
        //public List<Vertices> vertices;
        public List<Vertices> points;
        private Cohen_Sutherland _cohen_Sutherland;


        public void ChangeViewportForCohenSutherlandAlgorythm(int Xmax,int Ymax)
        {
            _cohen_Sutherland.ChangeViewportSize(Xmax, Ymax);
        }
        public Graph(List<Vertices> vert, List<Segment> seg)
        {
            if (vert != null)
            {
                points = new List<Vertices>();
                points = vert;
            }
            if (seg != null)
            {
                segments = new List<Segment>();
                segments = seg;
            }
        }


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


            segments.Add(new Segment(points[0], points[1]));
            segments.Add(new Segment(points[0], points[2]));
            segments.Add(new Segment(points[0], points[3]));
            segments.Add(new Segment(points[1], points[2]));

        }
        private void fill_world_vertices()
        {
            //world_vertices.Add(new Vertices(380, 30));
            //world_vertices.Add(new Vertices(850, 600));
            //world_vertices.Add(new Vertices(100, 600));

            points.Add(new Vertices(200, 200));
            points.Add(new Vertices(500, 200));
            points.Add(new Vertices(400, 400));
            points.Add(new Vertices(100, 300));
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


        public Graph()
        {
            viewport_points = new List<Vertices>();
            segments = new List<Segment>();
            points = new List<Vertices>();

            _cohen_Sutherland = new Cohen_Sutherland();

            Generate_World();
        }

        public Graph(int width, int height)
        {
            Xmin = 0;
            Ymin = 0;

            Xmax = width;
            Ymax = height;

            XmaxScaled = Xmax;
            YmaxScaled = Ymax;

            viewport_points = new List<Vertices>();
            segments = new List<Segment>();
            points = new List<Vertices>();

            _cohen_Sutherland = new Cohen_Sutherland();

            Generate_World();

        }
        //public Graph(int width,int height)
        //{

        //    Xmin = 0;
        //    Ymin = 0;

        //    Xmax = width;
        //    Ymax = height;

        //    viewport_points = new List<Vertices>();
        //    segments = new List<Segment>();
        //    vertices = new List<Vertices>();

        //    //_cohen_Sutherland = new Cohen_Sutherland();

        //    //Generate_World();

        //}

        public void Draw(PaintEventArgs e,bool outline=false)
        {
            if (e != null)
            {
                Draw_Segments(e);
                Draw_Vertices(e,outline);
            }
        }

        private bool isPointInViewport(Vertices vert)
        {
            // Использую масштабированные координаты Viewport'а
            if ((vert.point.X >= Xmin) & (vert.point.X <= XmaxScaled)) 
                if ((vert.point.Y >= Ymin) & (vert.point.Y <= YmaxScaled))
                    return true;

            //if ((vert.X >= Xmin) & (vert.X <= Xmax))
            //    if ((vert.Y >= Ymin) & (vert.Y <= Ymax))
            //        return true;


            return false;
        }

        private void Draw_Vertices(PaintEventArgs e,bool outline=false)
        {
            // throw new NotImplementedException();
            foreach (var vert in points)
            {
               // if (isPointInViewport(vert) == true)
                    vert.Draw(e, 20, "Black",outline);
            }
        }

        private void Draw_Segments(PaintEventArgs e)
        {
            foreach (var seg in segments)
            {
                ////seg.ClipAndDrawSegment(e);
                //_cohen_Sutherland.ClipSegment(seg);
                //if (seg.Visible != false) 
                //    seg.Draw(e);

                seg.Draw(e); // Не используя алгоритм отсечения

            }

        }

        internal void AddPoint(Vertices vert)
        {
           // Vertices newVertices = new Vertices(vert.X,vert.Y);
            points.Add(vert);
            //vertices.Add(new Vertices(vert.X,vert.Y));
            //world_vertices.Add(new Vertices(vert.X, vert.Y));
        }


        private bool ContainsPoint(Vertices vert)
        {
            //if (world_vertices.Contains(vert) == true) return true;

            //return false;

            if (points != null)
                foreach (var local_vert in points)
                {
                    if ((local_vert.point.X == vert.point.X) && (local_vert.point.Y == vert.point.Y)) return true;
                }

            return false;
        }

        internal bool TryAddPoint(Vertices vert)
        {
            Debug.WriteLine($" Количество точек : {points.Count}");

            if (ContainsPoint(vert) == false) // если точки нет в List , то
            {
                AddPoint(vert);             // Добавляем точку
                return true;
            }

            return false;

        }

        internal bool TryAddSegment(Segment seg)
        {
            if (ContainSegment(seg) == false) // Если нет сегмента в списке
            {
                AddSegment(seg);              // то добавляем к коллекцию
                return true;
            }
            return false;
        }

        private void AddSegment(Segment seg)
        {
            segments.Add(seg);
        }

        // Проверка, что есть такой сегмент в графе или нет
        private bool ContainSegment(Segment seg)
        {
            if (segments != null)
                foreach (var local_seg in segments)
                {
                    if (((local_seg.P1.point.X == seg.P1.point.X) && (local_seg.P1.point.Y == seg.P1.point.Y) &&
                        (local_seg.P2.point.X == seg.P2.point.X) && (local_seg.P2.point.Y == seg.P2.point.Y)) ||

                            ((local_seg.P1.point.X == seg.P2.point.X) && (local_seg.P1.point.Y == seg.P2.point.Y) &&
                            (local_seg.P2.point.X == seg.P1.point.X) && (local_seg.P2.point.Y == seg.P1.point.Y)))
                    {
                        return true;
                    }
                    // if ((vertices.X == vert.X) && (vertices.Y == vert.Y)) return true;
                }

            return false;
        }

        //Удаляем сегмент (ребро) графа
        internal void RemoveSegment(Segment segm)
        {
            segments.Remove(segm);
        }

        // Удаляем вершину графа
        internal void RemoveVectices(Vertices vert)
        {
            points.Remove(vert);
            // + Удалить нужно Сегменты, которые содержат эту вершину
            List<Segment> seg = new List<Segment>();
            seg = getSegmentsWithPoint(vert); // Можно отрефакторить и не возвращать список, а удалить "в методе". :)
            if (seg != null)
                foreach (var segm in seg)
                    segments.Remove(segm);

        }

        // Получить сегменты, в которых есть удаляемая точка
        private List<Segment> getSegmentsWithPoint(Vertices vert)
        {
            List<Segment> tmp = new List<Segment>();
            foreach (var seg in segments)
            {
                if (seg.IncludesPoint(vert) == true) tmp.Add(seg);
            }
            return tmp;
        }
    }
}
