using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_WinForms.Data
{
    public class AUX_Polygon
    {
        public Color Fill { get; set; } = Color.FromArgb((int)0.3 * 255, 0, 0, 255);
        public int LineWidth { get; set; } = 2;
        public string Stroke { get; set; } = "blue";
    }
    public class Polygon
    {
        public void Draw(Graphics g,AUX_Polygon aux_Polygon)
        {

        }
    }
}
