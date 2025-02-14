using GIS_WinForms.Data._World;
using GIS_WinForms.Data.Primitives;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace GIS_WinForms.ViewsElements
{
    public class CustomPanel:Panel
    {
      //  World world;

        Graph graph;

        GraphEditor graphEditor;
        Viewport viewport;

        private void InitPanel()
        {
            Dock = DockStyle.None;
            Width = 905;
            Height = 485;
            Location = new Point(41, 61);
            DoubleBuffered = true;

            // Сделаем по индусски.
            // Если сработал вызов события от мыши - прокрутка колеса, то изменяем масштаб окна, для отсечения в 
            // алгоритме Кохена - Сазерленда :)
            this.MouseWheel += CustomPanel_MouseWheel;

        }


        private void CustomPanel_MouseWheel(object? sender, MouseEventArgs e)
        {
            // Измением масштаб окна
            //graph.Xmin = graph.Xmin / Convert.ToInt32(viewport.zoom);
            //graph.Ymin = graph.Xmin / Convert.ToInt32(viewport.zoom);

            //graph.XmaxScaled = graph.Xmax * Convert.ToInt32(viewport.zoom);
            //graph.YmaxScaled = graph.Ymax * Convert.ToInt32(viewport.zoom);


            int Xmax = graph.Xmax * Convert.ToInt32(viewport.zoom);
            int Ymax = graph.Ymax * Convert.ToInt32(viewport.zoom);

            graph.XmaxScaled = Xmax;
            graph.YmaxScaled = Ymax;

            graph.ChangeViewportForCohenSutherlandAlgorythm(Xmax, Ymax);

            Debug.WriteLine($"{graph.XmaxScaled} : {graph.YmaxScaled}");


        }

        public CustomPanel()
        {
            InitPanel();
            graph = new Graph(this.Size.Width, this.Size.Height);
            viewport = new Viewport(this); // Init Viewport и передам CustomPanel
                                           //   world = new World(this.Size.Width,this.Size.Height);
                                           //graphEditor = new GraphEditor(this,graph,this.Size.Width, this.Size.Height);
            graphEditor = new GraphEditor(this, viewport, graph, this.Size.Width, this.Size.Height);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsState state = e.Graphics.Save();
            e.Graphics.Clear(Color.Aqua);
            //e.Graphics.DrawLine(Pens.Black, new PointF(0, 0), new PointF(graph.Xmax, graph.Ymax));
            //e.Graphics.DrawLine(Pens.Black, new PointF(0, graph.Ymax), new PointF(graph.Xmax, 0));

            // this.Scale(SizeF(viewport.zoom));

            e.Graphics.TranslateTransform(viewport.Center.X,viewport.Center.Y);
            e.Graphics.ScaleTransform(1 / viewport.zoom, 1 / viewport.zoom);

            Vertices tmp_Offset = new Vertices();
            tmp_Offset = viewport.getOffset();

//            e.Graphics.TranslateTransform(viewport.drag.offset.X, viewport.drag.offset.Y);
            e.Graphics.TranslateTransform(tmp_Offset.X, tmp_Offset.Y);
            graphEditor.display(e);
            e.Graphics.Restore(state);
            //graph.Draw(e);
            //world.Draw(e);
        }

        internal void addRandomPoint()
        {
            Random rnd = new Random();
            //double a1, a2;
            //int x1, y1;
            double rnd1= rnd.NextDouble();
            double rnd2= rnd.NextDouble();


            //rnd1 = 0.5; // Для тестирования
            //rnd2 = 0.5;
            bool success= graph.TryAddPoint(new Vertices(Convert.ToInt32(rnd1* graph.Xmax), 
                                        Convert.ToInt32(rnd2* graph.Ymax)));

            Debug.WriteLine($" Success : {success}");

            Refresh();
        }

        internal void addRandomSegment()
        {
            Random rnd = new Random();
            bool Success = false;
            double rndIndex1=rnd.NextDouble() * (graph.vertices.Count-1);
            rnd.NextDouble();
            double rndIndex2=rnd.NextDouble() * (graph.vertices.Count-1);

            int index1 = Convert.ToInt32(rndIndex1);
            int index2 = Convert.ToInt32(rndIndex2);


            //index1 = 1; // For Debuging
            //index2 = 3;

            Debug.WriteLine($"rndIndex1: {index1}");
            Debug.WriteLine($"rndIndex2: {index2}");

            if (index1>=0  && index2>=0)
            if (index1!=index2)
            {
                Success= graph.TryAddSegment(new Segment(graph.vertices[index1],
                                                graph.vertices[index2]));
            }

            Debug.WriteLine($"Success: {Success}");
            Debug.WriteLine($"Количество Сегментов {graph.segments.Count}");
            Refresh();
        }

        internal void removeRandomSegment()
        {
            if (graph.segments.Count==0)
            {
                Debug.WriteLine("No Segments");
                return;
            }

            Random rnd = new Random();
            bool Success = false;
            double rndIndex1 = rnd.NextDouble() * (graph.segments.Count - 1);
            int index1 = Convert.ToInt32(rndIndex1);
            if (index1 <= graph.segments.Count - 1) graph.RemoveSegment(graph.segments[index1]);

            Debug.WriteLine($"Removed Index: {index1} ");
            Debug.WriteLine($"Num of Segments: {graph.segments.Count}");
            Refresh();


        }

        internal void removeRandomPoint()
        {
            if (graph.vertices.Count == 0)
            {
                Debug.WriteLine("No any Vertices... List is empty");
                return;
            }

            Random rnd = new Random();
            bool Success = false;
            double rndIndex1 = rnd.NextDouble() * (graph.vertices.Count - 1);
            int index1 = Convert.ToInt32(rndIndex1);
            graph.RemoveVectices(graph.vertices[index1]);

            Debug.WriteLine($"Removed Index: {index1} ");
            Debug.WriteLine($"Num of Vertices: {graph.vertices.Count}");
            Refresh();

        }

        internal void ClearAll()
        {
            //world.world_segments.Clear();
            //world.world_vertices.Clear();
            graph.segments.Clear();
            graph.vertices.Clear();
            Refresh();
        }
    }
}
