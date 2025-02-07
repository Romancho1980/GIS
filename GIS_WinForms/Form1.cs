using GIS_WinForms.ViewsElements;

namespace GIS_WinForms
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        public Form1()
        {
            this.Controls.Add(new CustomPanel());
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
           
        }
    }
}
