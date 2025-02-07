using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.ViewsElements
{
    public class CustomPanel:Panel
    {
        public CustomPanel()
        {
            Dock = DockStyle.None;
            Width = 905;
            Height = 485;
            Location = new Point(41,61);


            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Aqua);
            e.Graphics.DrawLine(Pens.Black, new PointF(0,0), new PointF(100,100));
        }
    }
}
