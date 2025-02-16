
using System.Drawing.Drawing2D;

namespace WinForm_ForTests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        protected override void OnPaint(PaintEventArgs e)
        {

        }
        private void Draw(PaintEventArgs e, int x1, int y1, int x2, int y2)
        {
            e.Graphics.DrawLine(Pens.Black, new PointF(x1, y1), new PointF(x2, y1));
            e.Graphics.DrawLine(Pens.Black, new PointF(x2, y1), new PointF(x2, y2));
            e.Graphics.DrawLine(Pens.Black, new PointF(x2, y2), new PointF(x1, y2));
            e.Graphics.DrawLine(Pens.Black, new PointF(x1, y2), new PointF(x1, y1));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsState state=e.Graphics.Save();
            e.Graphics.TranslateTransform(100, 100);
            e.Graphics.ScaleTransform(0.2F,0.2F);
            Draw(e, 0, 0, 300, 200);
            

            e.Graphics.Restore(state);

        }
    }
}
