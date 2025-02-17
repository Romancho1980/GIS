
using System.Diagnostics;
using System.Drawing.Drawing2D;
using WinForm_ForTests.Data;

namespace WinForm_ForTests
{
    public partial class Form1 : Form
    {
        public float Zoom { get; set; } = 1;
        MyRectangle myRectangle;
        MousePosition myMousePosition;
        public Form1()
        {
            InitializeComponent();
            Init();
        }


        public void Init()
        {
            myRectangle = new MyRectangle(0, 0, 300, 200);
            myMousePosition = new();
        }
        protected override void OnPaint(PaintEventArgs e)
        {

        }
        private void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, new PointF(myRectangle.X1, myRectangle.Y1),
                                            new PointF(myRectangle.X2, myRectangle.Y1));

            e.Graphics.DrawLine(Pens.Black, new PointF(myRectangle.X2, myRectangle.Y1),
                                            new PointF(myRectangle.X2, myRectangle.Y2));

            e.Graphics.DrawLine(Pens.Black, new PointF(myRectangle.X2, myRectangle.Y2),
                                            new PointF(myRectangle.X1, myRectangle.Y2));

            e.Graphics.DrawLine(Pens.Black, new PointF(myRectangle.X1, myRectangle.Y2),
                                            new PointF(myRectangle.X1, myRectangle.Y1));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsState state = e.Graphics.Save();
            Draw(e);
            e.Graphics.TranslateTransform(200, 100);
            Draw(e);
            e.Graphics.ScaleTransform(1/Zoom, 1/Zoom);
            Draw(e);
            e.Graphics.Restore(state);
            Debug.WriteLine($"Zoom = {Zoom}");

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Debug.WriteLine($"{e.X} {e.Y}");
            float new_x;
            float new_y;
            new_x = (e.X - 200) * Zoom;
            new_y = (e.Y - 100) * Zoom;

            Debug.WriteLine($"New {new_x:F2} : {new_y:F2}");
            //Debug.WriteLine($"Zooming {}")
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Zoom in
            if (Zoom < 4.999F) Zoom += 0.2F;
            panel1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Zoom out
            if (Zoom > 1.0F) Zoom -= 0.2F;
            panel1.Refresh();
        }
    }
}
