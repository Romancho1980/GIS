using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIS_WinForms.Data._World;

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

        }
    }
}
