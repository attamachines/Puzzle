namespace Puzzle
{
    partial class FrmBitmaps
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
            this.ltvBitmap = new System.Windows.Forms.ListView();
            this.imlBitmaps = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ltvBitmap
            // 
            this.ltvBitmap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ltvBitmap.FullRowSelect = true;
            this.ltvBitmap.GridLines = true;
            this.ltvBitmap.LargeImageList = this.imlBitmaps;
            this.ltvBitmap.Location = new System.Drawing.Point(12, 12);
            this.ltvBitmap.MultiSelect = false;
            this.ltvBitmap.Name = "ltvBitmap";
            this.ltvBitmap.Size = new System.Drawing.Size(710, 485);
            this.ltvBitmap.TabIndex = 0;
            this.ltvBitmap.UseCompatibleStateImageBehavior = false;
            this.ltvBitmap.SelectedIndexChanged += new System.EventHandler(this.ltvBitmap_SelectedIndexChanged);
            // 
            // imlBitmaps
            // 
            this.imlBitmaps.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imlBitmaps.ImageSize = new System.Drawing.Size(128, 128);
            this.imlBitmaps.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FrmBitmaps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Puzzle.Properties.Resources.Texture;
            this.ClientSize = new System.Drawing.Size(734, 509);
            this.ControlBox = false;
            this.Controls.Add(this.ltvBitmap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBitmaps";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select picture";
            this.Load += new System.EventHandler(this.FrmBitmaps_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ltvBitmap;
        private System.Windows.Forms.ImageList imlBitmaps;
    }
}