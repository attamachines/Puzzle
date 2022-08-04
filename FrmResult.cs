using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Puzzle
{
    public partial class FrmResult : Form
    {
        private List<PieceCluster> _PictureList = new List<PieceCluster> { };
        private int _WidthPic;
        private int _HeightPic;
        private Bitmap _PicResult;
        public FrmResult()
        {
            InitializeComponent();
        }

        public List<PieceCluster> PictureList
        {
            get
            {
                return _PictureList;
            }

            set
            {
                _PictureList = value;
            }
        }

        public bool GameMode
        {
            get
            {
                return _GameMode;
            }

            set
            {
                _GameMode = value;
            }
        }

        public int WidthPic
        {
            get
            {
                return _WidthPic;
            }

            set
            {
                _WidthPic = value;
            }
        }

        public int HeightPic
        {
            get
            {
                return _HeightPic;
            }

            set
            {
                _HeightPic = value;
            }
        }

        public Bitmap PicResult
        {
            get
            {
                return _PicResult;
            }

            set
            {
                _PicResult = value;
            }
        }

        private bool _GameMode = false;

        private void FrmResult_Load(object sender, EventArgs e)
        {

        }

        private void glassButton3_Click(object sender, EventArgs e)
        {

        }

        private void glassButton1_Click(object sender, EventArgs e)
        {

        }

        private void glassButton2_Click(object sender, EventArgs e)
        {

        }

        protected override void OnResize(EventArgs e)
        {
            if (_PicResult == null) return;

            int hieght = pictureBox1.Height;

            double factor = hieght*1.0f / _PicResult.Height;

            Bitmap bm = new Bitmap((int)(_PicResult.Width * factor), hieght);

            using (Graphics g = Graphics.FromImage(bm))
            {
                g.DrawImage(_PicResult, new Rectangle(0, 0, bm.Width, bm.Height));
            }

            pictureBox1.Image = bm;
        }
    }
}
