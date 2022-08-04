using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Puzzle
{
    public partial class FrmBitmaps : Form
    {
        public FrmBitmaps()
        {
            InitializeComponent();
        }

        private void FrmBitmaps_Load(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            path += "\\Bitmaps";
            StaticValue.BitmapFiles = Directory.GetFiles(path,"*.JPG");
            
            if (StaticValue.BitmapFiles.Length>0)
            {
                for(int i =0;i< StaticValue.BitmapFiles.Length;i++)
                {
                    Image im = Image.FromFile(StaticValue.BitmapFiles[i]);
                    imlBitmaps.Images.Add(im);
                    ListViewItem item = new ListViewItem();
                    item.ImageIndex = i;
                    ltvBitmap.Items.Add(item);
                }
            }

            StaticValue.ImageIndex = 0;
            Close();
        }

        private void ltvBitmap_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                StaticValue.ImageIndex = ltvBitmap.SelectedItems[0].ImageIndex;
                Close();
            }
            catch
            {
            }
           
        }
    }
}
