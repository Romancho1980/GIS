using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data._World
{
    public class GraphEditor
    {
        Graph graph;
        public GraphEditor(Graph graph, int width, int height)
        {
            this.graph = graph;
        }

        internal void display(PaintEventArgs e)
        {
            graph.Draw(e);
        }
    }
}
