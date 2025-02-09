using GIS_WinForms.Data._World;
using GIS_WinForms.Data.Primitives;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace GIS_WinForms.ViewsElements
{
    public class CustomPanel:Panel
    {
        World world;

        Graph graph;

        private void InitPanel()
        {
            Dock = DockStyle.None;
            Width = 905;
            Height = 485;
            Location = new Point(41, 61);
            DoubleBuffered = true;

        }
        public CustomPanel()
        {
            InitPanel();
            graph = new Graph(this.Size.Width, this.Size.Height);
            world = new World(this.Size.Width,this.Size.Height);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Aqua);
            world.Draw(e);
            //e.Graphics.DrawLine(Pens.Black, new PointF(0,0), new PointF(this.Size.Width,this.Size.Height));
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
            bool success=world.TryAddPoint(new Vertices(Convert.ToInt32(rnd1*world.Xmax), 
                                        Convert.ToInt32(rnd2*world.Ymax)));

            Debug.WriteLine($" Success : {success}");

            Refresh();
        }

        internal void addRandomSegment()
        {
            Random rnd = new Random();
            bool Success = false;
            double rndIndex1=rnd.NextDouble() * (world.world_vertices.Count-1);
            rnd.NextDouble();
            double rndIndex2=rnd.NextDouble() * (world.world_vertices.Count-1);

            int index1 = Convert.ToInt32(rndIndex1);
            int index2 = Convert.ToInt32(rndIndex2);


            index1 = 1;
            index2 = 3;

            Debug.WriteLine($"rndIndex1: {index1}");
            Debug.WriteLine($"rndIndex2: {index2}");

            if (index1!=index2)
            {
                Success=world.TryAddSegment(new Segment(world.world_vertices[index1],
                                                world.world_vertices[index2]));
            }

            Debug.WriteLine($"Success: {Success}");
            Refresh();
        }
    }
}
