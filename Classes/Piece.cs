using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Puzzle
{
    public class Piece
    {
        public int ID { get; set; }

        public int ClusterID { get; set; }

        public List<int> AdjacentPieceIDs { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Rectangle BoardLocation { get; set; }

        public Rectangle SourcePictureLocation { get; set; }

        public GraphicsPath MovableFigure { get; set; }

        public GraphicsPath StaticFigure { get; set; }

        public Bitmap Picture { get; set; }

        public int IndexX { get; set; }
        
        public int IndexY { get; set; }     
        
        public bool Active { get; set; }               
    }
}
