using GIS_WinForms.Data.Primitives;
using GIS_WinForms.ViewsElements;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data._World
{
    public class GraphEditor
    {
        Graph graph;
        CustomPanel customPanel;

        public Vertices? selected { get; set; }
        public Vertices? hovered { get; set; }

        public GraphEditor(CustomPanel panel,Graph graph, int width, int height)
        {
            this.customPanel = panel;
            this.graph = graph;
            selected = null;
            hovered = new();
            customPanel.MouseDown += CustomPanel_MouseDown;
            customPanel.MouseMove += CustomPanel_MouseMove;
        }

        private void CustomPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            Debug.WriteLine("Mouse Moved");

            Vertices mouse = new Vertices(e.X, e.Y);
            hovered = Math_utils.Utils.getNearestPoint(mouse, graph.vertices,10);
            customPanel.Refresh();
        }

        private void CustomPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            Debug.WriteLine("Mouse Down on Panel");

            Vertices mouse = new Vertices(e.X,e.Y);
            hovered = Math_utils.Utils.getNearestPoint(mouse, graph.vertices,10);
            if (hovered != null) 
            {
                selected=hovered;
                customPanel.Refresh();
                return;
            }
            graph.AddPoint(mouse);
            selected = mouse;
            customPanel.Refresh();
        }

        //private Vertices getNearestPoint(Vertices mouse, List<Vertices> vertices)
        //{
        //}

        internal void display(PaintEventArgs e)
        {
            if (hovered != null)
            {
                hovered.Draw(e, 40, "Red", false);
            }


            if (selected != null)
            {
                selected.Draw(e,40,"Red",true);
            }

            graph.Draw(e);
        }
    }
}
