namespace SpriteDemo
{
    partial class Escenario
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
            components = new System.ComponentModel.Container();
            visor = new Engine2D.GraphicsView();
            lblScore = new Label();
            gameTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // visor
            // 
            visor.Location = new Point(26, 70);
            visor.Name = "visor";
            visor.Scene = null;
            visor.Size = new Size(720, 434);
            visor.TabIndex = 0;
            visor.KeyDown += visor_KeyDown;
            visor.KeyUp += visor_KeyUp;
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Consolas", 12F);
            lblScore.ForeColor = Color.Lime;
            lblScore.Location = new Point(612, 26);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(120, 23);
            lblScore.TabIndex = 1;
            lblScore.Text = "Score : 00";
            // 
            // gameTimer
            // 
            gameTimer.Enabled = true;
            gameTimer.Interval = 20;
            gameTimer.Tick += gameTimer_Tick;
            // 
            // Escenario
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(782, 553);
            Controls.Add(lblScore);
            Controls.Add(visor);
            DoubleBuffered = true;
            Name = "Escenario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sprite_Demo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Engine2D.GraphicsView visor;
        private Label lblScore;
        private System.Windows.Forms.Timer gameTimer;
    }
}
