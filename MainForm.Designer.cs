namespace Puzzle
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tm = new System.Windows.Forms.Timer(this.components);
            this.lbTime = new System.Windows.Forms.Label();
            this.glassButton4 = new Puzzle.GlassButton();
            this.glassButton3 = new Puzzle.GlassButton();
            this.glassButton2 = new Puzzle.GlassButton();
            this.glassButton1 = new Puzzle.GlassButton();
            this.SuspendLayout();
            // 
            // tm
            // 
            this.tm.Interval = 1000;
            this.tm.Tick += new System.EventHandler(this.tm_Tick);
            // 
            // lbTime
            // 
            this.lbTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTime.BackColor = System.Drawing.Color.Transparent;
            this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbTime.Location = new System.Drawing.Point(3, 12);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(864, 20);
            this.lbTime.TabIndex = 0;
            this.lbTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // glassButton4
            // 
            this.glassButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.glassButton4.BorderThickness = 1;
            this.glassButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton4.GlowColor = System.Drawing.Color.Fuchsia;
            this.glassButton4.Location = new System.Drawing.Point(691, 9);
            this.glassButton4.Name = "glassButton4";
            this.glassButton4.Size = new System.Drawing.Size(99, 44);
            this.glassButton4.TabIndex = 4;
            this.glassButton4.Text = "Reset";
            this.glassButton4.Visible = false;
            this.glassButton4.Click += new System.EventHandler(this.glassButton4_Click);
            // 
            // glassButton3
            // 
            this.glassButton3.BorderThickness = 1;
            this.glassButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton3.GlowColor = System.Drawing.Color.Lime;
            this.glassButton3.Location = new System.Drawing.Point(77, 9);
            this.glassButton3.Name = "glassButton3";
            this.glassButton3.Size = new System.Drawing.Size(147, 44);
            this.glassButton3.TabIndex = 3;
            this.glassButton3.Text = "Change Bitmap";
            this.glassButton3.Visible = false;
            this.glassButton3.Click += new System.EventHandler(this.glassButton3_Click);
            // 
            // glassButton2
            // 
            this.glassButton2.BorderThickness = 1;
            this.glassButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton2.Location = new System.Drawing.Point(9, 9);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.Size = new System.Drawing.Size(65, 44);
            this.glassButton2.TabIndex = 2;
            this.glassButton2.Text = "Menu";
            this.glassButton2.Click += new System.EventHandler(this.glassButton2_Click);
            // 
            // glassButton1
            // 
            this.glassButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.glassButton1.BorderThickness = 1;
            this.glassButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton1.GlowColor = System.Drawing.Color.Red;
            this.glassButton1.Location = new System.Drawing.Point(794, 9);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.Size = new System.Drawing.Size(65, 44);
            this.glassButton1.TabIndex = 1;
            this.glassButton1.Text = "Exit";
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Puzzle.Properties.Resources.Texture;
            this.ClientSize = new System.Drawing.Size(868, 566);
            this.Controls.Add(this.glassButton4);
            this.Controls.Add(this.glassButton3);
            this.Controls.Add(this.glassButton2);
            this.Controls.Add(this.glassButton1);
            this.Controls.Add(this.lbTime);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Puzzle";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ClientSizeChanged += new System.EventHandler(this.MainForm_ClientSizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Timer tm;
        private System.Windows.Forms.Label lbTime;
        private GlassButton glassButton2;
        private GlassButton glassButton3;
        private GlassButton glassButton1;
        private GlassButton glassButton4;
    }
}

