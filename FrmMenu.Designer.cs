namespace Puzzle
{
    partial class FrmMenu
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
            this.cbxHint = new JCS.ToggleSwitch();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numTime = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxGameMode = new JCS.ToggleSwitch();
            this.label3 = new System.Windows.Forms.Label();
            this.glassButton2 = new Puzzle.GlassButton();
            this.glassButton1 = new Puzzle.GlassButton();
            ((System.ComponentModel.ISupportInitialize)(this.numTime)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxHint
            // 
            this.cbxHint.BackColor = System.Drawing.Color.Transparent;
            this.cbxHint.Checked = true;
            this.cbxHint.Enabled = false;
            this.cbxHint.Location = new System.Drawing.Point(22, 23);
            this.cbxHint.Name = "cbxHint";
            this.cbxHint.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxHint.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxHint.Size = new System.Drawing.Size(50, 19);
            this.cbxHint.Style = JCS.ToggleSwitch.ToggleSwitchStyle.Carbon;
            this.cbxHint.TabIndex = 1;
            this.cbxHint.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.cbxHint_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(88, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Show picture hint";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(307, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Time for play";
            // 
            // numTime
            // 
            this.numTime.Location = new System.Drawing.Point(241, 24);
            this.numTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTime.Name = "numTime";
            this.numTime.Size = new System.Drawing.Size(50, 20);
            this.numTime.TabIndex = 4;
            this.numTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTime.ValueChanged += new System.EventHandler(this.numTime_ValueChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label5.Location = new System.Drawing.Point(11, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(380, 1);
            this.label5.TabIndex = 10;
            // 
            // cbxGameMode
            // 
            this.cbxGameMode.BackColor = System.Drawing.Color.Transparent;
            this.cbxGameMode.Checked = true;
            this.cbxGameMode.Location = new System.Drawing.Point(22, 71);
            this.cbxGameMode.Name = "cbxGameMode";
            this.cbxGameMode.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGameMode.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGameMode.Size = new System.Drawing.Size(50, 19);
            this.cbxGameMode.Style = JCS.ToggleSwitch.ToggleSwitchStyle.Carbon;
            this.cbxGameMode.TabIndex = 11;
            this.cbxGameMode.CheckedChanged += new JCS.ToggleSwitch.CheckedChangedDelegate(this.cbxGameMode_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(82, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Size 6 x 12 / 5 x 10";
            // 
            // glassButton2
            // 
            this.glassButton2.BorderThickness = 1;
            this.glassButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton2.Location = new System.Drawing.Point(201, 144);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.Size = new System.Drawing.Size(110, 39);
            this.glassButton2.SpecialSymbol = Puzzle.GlassButton.SpecialSymbols.Stop;
            this.glassButton2.TabIndex = 5;
            this.glassButton2.Text = "Exit";
            this.glassButton2.Click += new System.EventHandler(this.glassButton2_Click);
            // 
            // glassButton1
            // 
            this.glassButton1.BorderThickness = 1;
            this.glassButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.glassButton1.Location = new System.Drawing.Point(85, 144);
            this.glassButton1.Name = "glassButton1";
            this.glassButton1.Size = new System.Drawing.Size(110, 39);
            this.glassButton1.TabIndex = 0;
            this.glassButton1.Text = "Play";
            this.glassButton1.Click += new System.EventHandler(this.glassButton1_Click);
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Puzzle.Properties.Resources.Texture;
            this.ClientSize = new System.Drawing.Size(384, 186);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxGameMode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.glassButton2);
            this.Controls.Add(this.numTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxHint);
            this.Controls.Add(this.glassButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMenu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start";
            this.Load += new System.EventHandler(this.FrmMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GlassButton glassButton1;
        private JCS.ToggleSwitch cbxHint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numTime;
        private GlassButton glassButton2;
        private System.Windows.Forms.Label label5;
        private JCS.ToggleSwitch cbxGameMode;
        private System.Windows.Forms.Label label3;
    }
}