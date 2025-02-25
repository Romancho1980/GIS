using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data.Primitives.AUX_Classes
{
    public class PolyOptions
    {
        public string Stroke { get; set; }
        public int LineWidth { get; set; }
        public Color Fill { get; set; }

        public PolyOptions()
        {
            Stroke = string.Empty;
            LineWidth = 0;
            Fill = Color.White;
        }

        public PolyOptions(string stroke, int linewidth, Color color)
        {
            Stroke = stroke;
            LineWidth = linewidth;
            Fill = color;
        }
    }
}
