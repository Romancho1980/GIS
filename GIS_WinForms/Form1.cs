using GIS_WinForms.ViewsElements;

namespace GIS_WinForms
{
    public partial class Form1 : Form
    {
        //Graphics graphics;
        CustomPanel panel;
        public Form1()
        {
            panel = new CustomPanel();
            this.Controls.Add(panel);
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel.addRandomPoint();
        }
    }
}
