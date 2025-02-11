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
    public class Viewport
    {

        public CustomPanel panel { get; private set; }
        public float zoom { get; private set; }

        public Viewport(CustomPanel panel)
        {
            this.panel = panel;
            this.zoom = 1;
            addEventListener();
        }

        private void addEventListener()
        {
            panel.MouseWheel += HandleMouseWheel;
        }

        public Vertices getMouse(MouseEventArgs e)
        {
            return new Vertices(Convert.ToInt32(e.X*zoom),
                                Convert.ToInt32(e.Y*zoom));
        }

        private void HandleMouseWheel(object? sender, MouseEventArgs e)
        {
            var direction=Math.Sign(e.Delta);
            var step = 0.1F;
            zoom += direction * step;
            zoom = Math.Max(1,Math.Min(5,zoom));
            panel.Refresh();
        }
    }
}
