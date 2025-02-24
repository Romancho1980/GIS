namespace GIS_WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label1 = new Label();
            button4 = new Button();
            button5 = new Button();
            DisposeGraph = new Button();
            SaveGraph = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(107, 599);
            button1.Name = "button1";
            button1.Size = new Size(107, 23);
            button1.TabIndex = 1;
            button1.Text = "Add Point";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(262, 599);
            button2.Name = "button2";
            button2.Size = new Size(127, 23);
            button2.TabIndex = 2;
            button2.Text = "Add Segment";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(434, 599);
            button3.Name = "button3";
            button3.Size = new Size(136, 23);
            button3.TabIndex = 3;
            button3.Text = "Remove Segment";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            label1.Location = new Point(354, -1);
            label1.Name = "label1";
            label1.Size = new Size(262, 54);
            label1.TabIndex = 4;
            label1.Text = "World Editor";
            // 
            // button4
            // 
            button4.Location = new Point(605, 599);
            button4.Name = "button4";
            button4.Size = new Size(136, 23);
            button4.TabIndex = 5;
            button4.Text = "Remove Point";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(781, 599);
            button5.Name = "button5";
            button5.Size = new Size(107, 23);
            button5.TabIndex = 6;
            button5.Text = "Clear All";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // DisposeGraph
            // 
            DisposeGraph.Location = new Point(202, 560);
            DisposeGraph.Name = "DisposeGraph";
            DisposeGraph.Size = new Size(107, 23);
            DisposeGraph.TabIndex = 7;
            DisposeGraph.Text = "Очистить";
            DisposeGraph.UseVisualStyleBackColor = true;
            DisposeGraph.Click += DisposeGraph_Click;
            // 
            // SaveGraph
            // 
            SaveGraph.Location = new Point(621, 560);
            SaveGraph.Name = "SaveGraph";
            SaveGraph.Size = new Size(107, 23);
            SaveGraph.TabIndex = 8;
            SaveGraph.Text = "Сохранить";
            SaveGraph.UseVisualStyleBackColor = true;
            SaveGraph.Click += SaveGraph_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(997, 638);
            Controls.Add(SaveGraph);
            Controls.Add(DisposeGraph);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Paint += Form1_Paint;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label1;
        private Button button4;
        private Button button5;
        private Button DisposeGraph;
        private Button SaveGraph;
    }
}
