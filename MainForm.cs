using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Media;
using ImageProcessing;
using Puzzle.Properties;
using System.IO;

namespace Puzzle
{   
    public partial class MainForm : Form,IMain
    {
        #region Variable

        private bool _victoryAnnounced;

        private bool _canMovePiece;

        private int _previousMouseX, _previousMouseY;

        private int _previousClientWidth, _previousClientHeight;

        private int _puzzlePictureWidth, _puzzlePictureHeight;

        private Bitmap _board;

        private Bitmap _backBuffer;

        private Bitmap _background;

        private Bitmap _sourcePicture;

        private PieceCluster _currentCluster;

        List<PieceCluster> _clusters = new List<PieceCluster>();

        private bool _IsClose = false;
        private Bitmap _ImageResult;
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        public void ExitProgram()
        {
            _IsClose = true;
            Close();
        }

        #region Event

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetScreen();

            _previousClientWidth = this.ClientSize.Width;
            _previousClientHeight = this.ClientSize.Height;
            _puzzlePictureWidth = this.ClientSize.Width;
            _puzzlePictureHeight = this.ClientSize.Height - StaticValue.Top - StaticValue.Bottom;


            ConfigSetting();
        }

        private void ConfigSetting()
        {
            using (FrmMenu frm = new FrmMenu(this))
            {
                frm.ShowDialog(this);
            }

            if (_IsClose) return;

            GetBitmap();
        }

        private void GetBitmap()
        {
            using (FrmBitmaps frm = new FrmBitmaps())
            {
                frm.ShowDialog(this);
            }

            ShowBitmap();
            StaticValue.Start = DateTime.Now.AddMinutes(StaticValue.TotalMus);
            tm.Start();
        }
        private void tm_Tick(object sender, EventArgs e)
        {
            int ts = (int)((StaticValue.Start - DateTime.Now).TotalSeconds);
            lbTime.Text = TimeSpan.FromSeconds(ts).ToString();
            if (ts == 0)
            {
                PlaySound(ESound.Dies);
                tm.Stop();
                if (MessageBox.Show("Time over! Choose Yes to Continue or No to Exit Program!", "Time over!",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                           MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    GetBitmap();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void PlaySound(ESound es)
        {
            string soundPath = string.Empty;
            switch (es)
            {
                case ESound.Clap:
                    soundPath = StaticValue.ClapSound;
                    break;
                case ESound.Dies:
                    soundPath = StaticValue.DiesSound;
                    break;
                case ESound.Win:
                    soundPath = StaticValue.WinpSound;
                    break;
            }
            SoundPlayer play = new SoundPlayer();
            play.SoundLocation = soundPath;
            play.Play();
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {
            tm.Stop();
            lbTime.Text = string.Empty;
            ConfigSetting();
        }

        private void glassButton4_Click(object sender, EventArgs e)
        {
            lbTime.Text = string.Empty;
            ShowBitmap();
            StaticValue.Start = DateTime.Now.AddMinutes(StaticValue.TotalMus);
        }

        private void glassButton3_Click(object sender, EventArgs e)
        {
            tm.Stop();
            lbTime.Text = string.Empty;
            GetBitmap();
        }

        private void ShowBitmap()
        {
            if (_sourcePicture != null)
            {
                _sourcePicture.Dispose();
            }

            _sourcePicture = new Bitmap(StaticValue.BitmapFiles[StaticValue.ImageIndex]);

            if (_sourcePicture.Width > _puzzlePictureWidth || _sourcePicture.Height > _puzzlePictureHeight)
            {
                _sourcePicture = ImageUtilities.ResizeImage(_sourcePicture, _puzzlePictureWidth, _puzzlePictureHeight, false);
            }

            try
            {
                _ImageResult = _sourcePicture;
                CreatePuzzle();
                DisplayPuzzle(Settings.Default.ShowImageHint);
                _victoryAnnounced = false;
            }
            catch (Exception ex)
            {
                _sourcePicture.Dispose();
                MessageBox.Show(ex.Message);
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (_board != null)
            {
                using (Graphics gfx = this.CreateGraphics())
                {
                    gfx.DrawImageUnscaled(_board, 0, 0);
                }
            }            
        }

        private void MainForm_ClientSizeChanged(object sender, EventArgs e)
        {
            if (_previousClientWidth != this.ClientSize.Width || _previousClientHeight != this.ClientSize.Height)
            {
                _previousClientWidth = this.ClientSize.Width;
                _previousClientHeight = this.ClientSize.Height;

                DisplayPuzzle(Settings.Default.ShowImageHint);                
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            #region Determine which cluster is selected (mouse down)

            int selectedIndex = -1;

            for (int index = (_clusters.Count - 1); index >= 0; index--)
            {
                if (_clusters[index].MovableFigure.IsVisible(e.X, e.Y) && _clusters[index].Active)
                {
                    selectedIndex = index;
                    break;
                }
            }

            #endregion

            #region Bring up the selected cluster

            if (selectedIndex >= 0)
            {
                _currentCluster = _clusters[selectedIndex];

                _clusters.RemoveAt(selectedIndex);
                _clusters.Add(_currentCluster);

                #region Back buffer

                using (Graphics gfx = Graphics.FromImage(_backBuffer))
                {
                    Rectangle currentClusterBoardLocation = _currentCluster.BoardLocation;

                    gfx.DrawImage(_background, currentClusterBoardLocation, currentClusterBoardLocation, GraphicsUnit.Pixel);

                    #region Redraw the pieces

                    Region currentClusterBoardLocationRegion = new Region(_currentCluster.BoardLocation);

                    foreach (PieceCluster cluster in _clusters)
                    {
                        if (!cluster.Active) continue;

                        if (cluster != _currentCluster)
                        {
                            Region clusterRegion = new Region(cluster.MovableFigure);
                            clusterRegion.Intersect(currentClusterBoardLocationRegion);

                            if (!clusterRegion.IsEmpty(gfx))
                            {                                
                                gfx.SetClip(clusterRegion, CombineMode.Replace);
                                gfx.DrawImageUnscaled(cluster.Picture, cluster.BoardLocation);
                            }
                        }
                    }

                    #endregion
                }

                #endregion

                #region Board

                Matrix matrix = new Matrix();
                SolidBrush shadowBrush = new SolidBrush(GameSettings.DROP_SHADOW_COLOR);

                using (Graphics gfx = Graphics.FromImage(_board))
                {
                    #region Drop shadow

                    matrix.Reset();
                    matrix.Translate(GameSettings.DROP_SHADOW_DEPTH, GameSettings.DROP_SHADOW_DEPTH);

                    GraphicsPath shadowFigure = (GraphicsPath)_currentCluster.MovableFigure.Clone();
                    shadowFigure.Transform(matrix);

                    gfx.ResetClip();
                    gfx.SetClip(shadowFigure);
                    gfx.FillPath(shadowBrush, shadowFigure);

                    #endregion

                    #region Cluster picture

                    gfx.ResetClip();
                    gfx.SetClip(_currentCluster.MovableFigure);
                    gfx.DrawImageUnscaled(_currentCluster.Picture, _currentCluster.BoardLocation);

                    #endregion
                }

                #endregion

                #region Form

                using (Graphics gfx = this.CreateGraphics())
                {
                    gfx.DrawImageUnscaled(_board, 0, 0);
                }

                #endregion

                _previousMouseX = e.X;
                _previousMouseY = e.Y;

                _canMovePiece = true;
            }

            #endregion
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_canMovePiece)
            {
                int offsetX = e.X - _previousMouseX;
                int offsetY = e.Y - _previousMouseY;

                Rectangle currentClusterBoardLocation = _currentCluster.BoardLocation;

                int clusterOldX = currentClusterBoardLocation.X;
                int clusterOldY = currentClusterBoardLocation.Y;

                int clusterNewX = currentClusterBoardLocation.X + offsetX;
                int clusterNewY = currentClusterBoardLocation.Y + offsetY;

                Rectangle clusterOldBoardLocation = new Rectangle(clusterOldX, clusterOldY,
                                                        currentClusterBoardLocation.Width + GameSettings.DROP_SHADOW_DEPTH,
                                                        currentClusterBoardLocation.Height + GameSettings.DROP_SHADOW_DEPTH);

                Rectangle clusterNewBoardLocation = new Rectangle(clusterNewX, clusterNewY,
                                                        currentClusterBoardLocation.Width + GameSettings.DROP_SHADOW_DEPTH,
                                                        currentClusterBoardLocation.Height + GameSettings.DROP_SHADOW_DEPTH);

                Rectangle combinedClusterRect = Rectangle.Union(clusterOldBoardLocation, clusterNewBoardLocation);
                SolidBrush shadowBrush = new SolidBrush(GameSettings.DROP_SHADOW_COLOR);
                Matrix matrix = new Matrix();

                using (Graphics gfx = Graphics.FromImage(_board))
                {
                    gfx.DrawImage(_backBuffer, combinedClusterRect, combinedClusterRect, GraphicsUnit.Pixel);

                    #region Drop shadow

                    matrix.Reset();
                    matrix.Translate(offsetX + GameSettings.DROP_SHADOW_DEPTH, offsetY + GameSettings.DROP_SHADOW_DEPTH);

                    GraphicsPath shadowFigure = (GraphicsPath)_currentCluster.MovableFigure.Clone();
                    shadowFigure.Transform(matrix);
                    gfx.FillPath(shadowBrush, shadowFigure);

                    #endregion

                    #region Cluster

                    _currentCluster.BoardLocation = new Rectangle(clusterNewX, clusterNewY, _currentCluster.Width, _currentCluster.Height);

                    matrix.Reset();
                    matrix.Translate(offsetX, offsetY);
                    _currentCluster.MovableFigure.Transform(matrix);

                    gfx.ResetClip();
                    gfx.SetClip(_currentCluster.MovableFigure);
                    gfx.DrawImageUnscaled(_currentCluster.Picture, _currentCluster.BoardLocation);

                    #endregion

                    #region Update individual jigsaw piece location

                    foreach (Piece piece in _currentCluster.Pieces)
                    {
                        
                        int pieceNewX = piece.BoardLocation.X + offsetX;
                        int pieceNewY = piece.BoardLocation.Y + offsetY;
                        piece.BoardLocation = new Rectangle(pieceNewX, pieceNewY, piece.Width, piece.Height);

                        matrix.Reset();
                        matrix.Translate(offsetX, offsetY);
                        piece.MovableFigure.Transform(matrix);                        
                    }
                    
                    #endregion
                }

                using (Graphics gfx = this.CreateGraphics())
                {
                    gfx.DrawImage(_board, combinedClusterRect, combinedClusterRect, GraphicsUnit.Pixel);
                }

                _previousMouseX = e.X;
                _previousMouseY = e.Y;
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (_canMovePiece)
            {
                _previousMouseX = e.X;
                _previousMouseY = e.Y;

                #region Draw the "dropped" moving cluster at its final position into back buffer

                using (Graphics gfx = Graphics.FromImage(_backBuffer))
                {
                    gfx.ResetClip();
                    gfx.SetClip(_currentCluster.MovableFigure);
                    gfx.DrawImageUnscaled(_currentCluster.Picture, _currentCluster.BoardLocation);
                }

                #endregion

                #region Sync the board, the back buffer, and the display

                using (Graphics gfx = Graphics.FromImage(_board))
                {
                    gfx.DrawImageUnscaled(_backBuffer, 0, 0);
                }

                using (Graphics gfx = this.CreateGraphics())
                {
                    gfx.DrawImageUnscaled(_backBuffer, 0, 0);
                }

                #endregion

                #region Snapping and combining adjacent pieces

                Matrix matrix = new Matrix();                

                List<int> adjacentClusterIDs = new List<int>();

                for (int i = 0; i < _currentCluster.Pieces.Count; i++)
                {
                    Piece currentPiece = _currentCluster.Pieces[i];

                    foreach (int pieceID in currentPiece.AdjacentPieceIDs)
                    {
                        Piece adjacentPiece = GetPieceByID(pieceID);

                        if (adjacentPiece != null && (adjacentPiece.ClusterID != currentPiece.ClusterID))
                        {                                                        
                            #region Make sure the adjacent piece is located at the correct "side" of the current piece

                            Rectangle adjacentPieceMovableFigureBoardLocation = Rectangle.Truncate(adjacentPiece.MovableFigure.GetBounds());
                            Rectangle currentPieceMovableFigureBoardLocation = Rectangle.Truncate(currentPiece.MovableFigure.GetBounds());

                            if (Math.Abs(currentPiece.SourcePictureLocation.X - adjacentPiece.SourcePictureLocation.X) <= 2)
                            {
                                int figureYDifferenceSign = Math.Sign(currentPieceMovableFigureBoardLocation.Y - adjacentPieceMovableFigureBoardLocation.Y);
                                int sourcePictureYDifferenceSign = Math.Sign(currentPiece.SourcePictureLocation.Y - adjacentPiece.SourcePictureLocation.Y);

                                if (figureYDifferenceSign != sourcePictureYDifferenceSign)
                                {
                                    continue;
                                }
                            }
                            else if (Math.Abs(currentPiece.SourcePictureLocation.Y - adjacentPiece.SourcePictureLocation.Y) <= 2)
                            {
                                int figureXDifferenceSign = Math.Sign(currentPieceMovableFigureBoardLocation.X - adjacentPieceMovableFigureBoardLocation.X);
                                int sourceImageXDifferenceSign = Math.Sign(currentPiece.SourcePictureLocation.X - adjacentPiece.SourcePictureLocation.X);

                                if (figureXDifferenceSign != sourceImageXDifferenceSign)
                                {
                                    continue;
                                }
                            }

                            #endregion

                            #region Determine if the adjacent piece should be snapped to the current cluster

                            GraphicsPath combinedMovableFigure = new GraphicsPath();
                            combinedMovableFigure.AddPath(adjacentPiece.MovableFigure, false);
                            combinedMovableFigure.AddPath(currentPiece.MovableFigure, false);

                            Rectangle combinedMovableFigureBoardLocation = Rectangle.Truncate(combinedMovableFigure.GetBounds());

                            Rectangle combinedSourcePictureLocation = Rectangle.Union(adjacentPiece.SourcePictureLocation, currentPiece.SourcePictureLocation);

                            if (Math.Abs(combinedMovableFigureBoardLocation.Width - combinedSourcePictureLocation.Width) <= GameSettings.SNAP_TOLERANCE &&
                                Math.Abs(combinedMovableFigureBoardLocation.Height - combinedSourcePictureLocation.Height) <= GameSettings.SNAP_TOLERANCE)
                            {
                                PieceCluster adjacentPieceCluster = GetPieceClusterByID(adjacentPiece.ClusterID);

                                adjacentClusterIDs.Add(adjacentPieceCluster.ID);                                    

                                foreach (Piece piece in adjacentPieceCluster.Pieces)
                                {
                                    piece.ClusterID = currentPiece.ClusterID;                                       
                                }
                            }

                            #endregion                            
                        }
                    }
                }
                
                if (adjacentClusterIDs.Count > 0)
                {
                    PlaySound(ESound.Clap);
                    #region Remove the adjacent cluster from the list after combining with the current cluster

                    foreach (int clusterID in adjacentClusterIDs)
                    {
                        PieceCluster adjacentCluster = GetPieceClusterByID(clusterID);

                        foreach (Piece piece in adjacentCluster.Pieces)
                        {
                            _currentCluster.Pieces.Add(piece);
                        }
                        
                        RemovePieceGroupByID(clusterID);
                    }

                    #endregion

                    GraphicsPath combinedStaticFigure = new GraphicsPath();                   
                    Rectangle combinedBoardLocation = _currentCluster.BoardLocation;
                    Rectangle combinedSourcePictureLocation = _currentCluster.SourcePictureLocation;

                    foreach (Piece piece in _currentCluster.Pieces)
                    {
                        combinedStaticFigure.AddPath(piece.StaticFigure, false);
                        combinedBoardLocation = Rectangle.Union(combinedBoardLocation, piece.BoardLocation);
                        combinedSourcePictureLocation = Rectangle.Union(combinedSourcePictureLocation, piece.SourcePictureLocation);                        
                    }

                    _currentCluster.BoardLocation = new Rectangle(combinedBoardLocation.X, combinedBoardLocation.Y,
                                                                        combinedSourcePictureLocation.Width,
                                                                        combinedSourcePictureLocation.Height);

                    _currentCluster.SourcePictureLocation = combinedSourcePictureLocation;
                    _currentCluster.Width = combinedSourcePictureLocation.Width;
                    _currentCluster.Height = combinedSourcePictureLocation.Height;
                    _currentCluster.StaticFigure = (GraphicsPath)combinedStaticFigure.Clone();
                    _currentCluster.MovableFigure = (GraphicsPath)combinedStaticFigure.Clone();

                    Rectangle combinedStaticFigureLocation = Rectangle.Truncate(combinedStaticFigure.GetBounds());
                                                            
                    matrix.Reset();
                    matrix.Translate(0 - combinedStaticFigureLocation.X, 0 - combinedStaticFigureLocation.Y);
                    _currentCluster.MovableFigure.Transform(matrix);

                    matrix.Reset();
                    matrix.Translate(combinedBoardLocation.X, combinedBoardLocation.Y);
                    _currentCluster.MovableFigure.Transform(matrix);                    
                    
                    #region Construct cluster picture
                    
                    matrix.Reset();
                    matrix.Translate(0 - combinedStaticFigureLocation.X, 0 - combinedStaticFigureLocation.Y);
                    GraphicsPath translatedCombinedStaticFigure = (GraphicsPath)combinedStaticFigure.Clone();
                    translatedCombinedStaticFigure.Transform(matrix);                    
                    
                    Bitmap clusterPicture = new Bitmap(combinedSourcePictureLocation.Width, combinedSourcePictureLocation.Height);

                    using (Graphics gfx = Graphics.FromImage(clusterPicture))
                    {
                        gfx.FillRectangle(Brushes.White, 0, 0, clusterPicture.Width, clusterPicture.Height);

                        gfx.ResetClip();
                        gfx.SetClip(translatedCombinedStaticFigure);
                        gfx.DrawImage(_sourcePicture, new Rectangle(0, 0, clusterPicture.Width, clusterPicture.Height),
                                combinedStaticFigureLocation, GraphicsUnit.Pixel);

                        if (GameSettings.DRAW_PIECE_OUTLINE)
                        {
                            Pen outlinePen = new Pen(Color.Black)
                            {
                                Width = GameSettings.PIECE_OUTLINE_WIDTH,
                                Alignment = PenAlignment.Inset
                            };

                            gfx.SmoothingMode = SmoothingMode.AntiAlias;
                            gfx.DrawPath(outlinePen, translatedCombinedStaticFigure);
                        }
                    }

                    Bitmap modifiedClusterPicture = (Bitmap)clusterPicture.Clone();
                    ImageUtilities.EdgeDetectHorizontal(modifiedClusterPicture);
                    ImageUtilities.EdgeDetectVertical(modifiedClusterPicture);
                    clusterPicture = ImageUtilities.AlphaBlendMatrix(modifiedClusterPicture, clusterPicture, 200);

                    #endregion

                    _currentCluster.Picture = (Bitmap)clusterPicture.Clone();

                    foreach (Piece piece in _currentCluster.Pieces)
                    {
                        int offsetX = piece.SourcePictureLocation.X - combinedSourcePictureLocation.X;
                        int offsetY = piece.SourcePictureLocation.Y - combinedSourcePictureLocation.Y;

                        int newLocationX = combinedBoardLocation.X + offsetX;
                        int newLocationY = combinedBoardLocation.Y + offsetY;

                        piece.BoardLocation = new Rectangle(newLocationX, newLocationY, piece.Width, piece.Height);

                        Rectangle movableFigureBoardLocation = Rectangle.Truncate(piece.MovableFigure.GetBounds());

                        matrix.Reset();
                        matrix.Translate(0 - movableFigureBoardLocation.X, 0 - movableFigureBoardLocation.Y);
                        piece.MovableFigure.Transform(matrix);

                        matrix.Reset();
                        matrix.Translate(newLocationX, newLocationY);
                        piece.MovableFigure.Transform(matrix);
                    }

                    #region Redraw

                    #region Back buffer

                    Rectangle areaToClear = new Rectangle(combinedBoardLocation.X, combinedBoardLocation.Y,
                                                            combinedBoardLocation.Width + GameSettings.DROP_SHADOW_DEPTH,
                                                            combinedBoardLocation.Height + GameSettings.DROP_SHADOW_DEPTH);

                    using (Graphics gfx = Graphics.FromImage(_backBuffer))
                    {
                        gfx.DrawImage(_background, areaToClear, areaToClear, GraphicsUnit.Pixel);                        

                        #region Redraw the pieces

                        Region regionToRedraw = new Region(areaToClear);

                        foreach (PieceCluster cluster in _clusters)
                        {
                            if (!cluster.Active) continue;
                            Region clusterRegion = new Region(cluster.MovableFigure);
                            clusterRegion.Intersect(regionToRedraw);

                            if (!clusterRegion.IsEmpty(gfx))
                            {
                                gfx.SetClip(clusterRegion, CombineMode.Replace);
                                gfx.DrawImageUnscaled(cluster.Picture, cluster.BoardLocation);
                            }
                        }

                        #endregion
                    }

                    #endregion

                    #region Board

                    using (Graphics gfx = Graphics.FromImage(_board))
                    {
                        gfx.DrawImageUnscaled(_backBuffer, 0, 0);
                    }

                    #endregion

                    #region Form

                    using (Graphics gfx = this.CreateGraphics())
                    {
                        gfx.DrawImageUnscaled(_backBuffer, 0, 0);
                    }

                    #endregion

                    #endregion
                }

                #endregion
                
                _canMovePiece = false;
                _currentCluster = null;

                #region Victory announcement


                if (_clusters.Count == (GameSettings.GameMode?5:5))
                {
                    if (_victoryAnnounced == false)
                    {
                        PlaySound(ESound.Win);
                        _victoryAnnounced = true;
                        if (MessageBox.Show("Xin chúc mừng bạn! Bạn có muốn xem thành quả của mình không?\n Chọn Yes để xem và No để chơi lại", "Chúc mừng!",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                          MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            using (FrmResult frm = new FrmResult())
                            {
                                frm.PicResult = _ImageResult;
                                frm.GameMode = GameSettings.GameMode;
                                if (frm.ShowDialog(this) == DialogResult.OK)
                                {
                                    ConfigSetting();
                                }
                                else
                                {
                                    this.Close();
                                }

                            }
                        }
                        else
                        {
                            ConfigSetting();
                        }
                        
                            
                    }
                }

                #endregion
            }
        }

        #endregion

        #region Helper
        
        private void CreatePuzzle()
        {
            #region Some validation

            if (_sourcePicture == null)
            {
                throw new Exception("Please provide source picture.");               
            }
            
            if (GameSettings.NUM_ROWS <= 1 || GameSettings.NUM_COLUMNS <= 1)
            {
                throw new Exception("GameSettings.NUM_COLUMNS and GameSettings.NUM_ROWS must be at least 2."); 
            }
            
            //if (GameSettings.NUM_COLUMNS != GameSettings.NUM_ROWS)
            //{
            //    throw new Exception("GameSettings.NUM_COLUMNS and GameSettings.NUM_ROWS must be the same.");
            //}

            #endregion

            #region Make sure the piece size is not too small

            int pieceWidth = _sourcePicture.Width / GameSettings.NUM_COLUMNS;
            int pieceHeight = _sourcePicture.Height / GameSettings.NUM_ROWS;

            if (pieceWidth < GameSettings.MIN_PIECE_WIDTH || pieceHeight < GameSettings.MIN_PIECE_HEIGHT)
            {
                throw new Exception("The picture is too small. Please select a bigger picture.");                
            }

            int lastColPieceWidth = pieceWidth + (_sourcePicture.Width % GameSettings.NUM_COLUMNS);
            int lastRowPieceHeight = pieceHeight + (_sourcePicture.Height % GameSettings.NUM_ROWS);

            #endregion

            #region Construct jigsaw pieces

            int lastRow = (GameSettings.NUM_ROWS - 1);
            int lastCol = (GameSettings.NUM_COLUMNS - 1);

            _currentCluster = null;
            _clusters = new List<PieceCluster>();

            Matrix matrix = new Matrix();

            Pen outlinePen = new Pen(Color.Black)
            {
                Width = GameSettings.PIECE_OUTLINE_WIDTH,
                Alignment = PenAlignment.Inset
            };

            int pieceID = 0;            

            for (int row = 0; row < GameSettings.NUM_ROWS; row++)
            {
                bool topCurveFlipVertical = (row % 2 == 0);
                bool bottomCurveFlipVertical = (row % 2 != 0);
                
                for (int col = 0; col < GameSettings.NUM_COLUMNS; col++)
                {
                    bool leftCurveFlipHorizontal = (col % 2 != 0);
                    bool rightCurveFlipHorizontal = (col % 2 == 0);

                    if (row % 2 == 0)
                    {
                        leftCurveFlipHorizontal = (col % 2 == 0);
                        rightCurveFlipHorizontal = (col % 2 != 0);
                    }

                    topCurveFlipVertical = !topCurveFlipVertical;
                    bottomCurveFlipVertical = !bottomCurveFlipVertical;

                    GraphicsPath figure = new GraphicsPath();

                    
                    int offsetX = (col * pieceWidth);
                    int offsetY = (row * pieceHeight);
                    int horizontalCurveLength = (col == lastCol ? lastColPieceWidth : pieceWidth);
                    int verticalCurveLength = (row == lastRow ? lastRowPieceHeight : pieceHeight);

                    #region Top

                    if (row == 0)
                    {
                        int startX = offsetX;
                        int startY = offsetY;
                        int endX = offsetX + horizontalCurveLength;
                        int endY = offsetY;

                        figure.AddLine(startX, startY, endX, endY);
                    }
                    else
                    {
                        BezierCurve topCurve = BezierCurve.CreateHorizontal(horizontalCurveLength);

                        if (topCurveFlipVertical)
                        {
                            topCurve.FlipVertical();
                        }

                        topCurve.Translate(offsetX, offsetY);
                        figure.AddBeziers(topCurve.Points);
                    }

                    #endregion

                    #region Right

                    if (col == lastCol)
                    {
                        int startX = offsetX + lastColPieceWidth;
                        int startY = offsetY;
                        int endX = offsetX + lastColPieceWidth;
                        int endY = offsetY + verticalCurveLength;

                        figure.AddLine(startX, startY, endX, endY);
                    }
                    else
                    {
                        BezierCurve verticalCurve = BezierCurve.CreateVertical(verticalCurveLength);

                        if (rightCurveFlipHorizontal)
                        {
                            verticalCurve.FlipHorizontal();
                        }

                        verticalCurve.Translate(offsetX + pieceWidth, offsetY);
                        figure.AddBeziers(verticalCurve.Points);
                    }

                    #endregion

                    #region Bottom

                    if (row == lastRow)
                    {
                        int startX = offsetX;
                        int startY = offsetY + lastRowPieceHeight;
                        int endX = offsetX + horizontalCurveLength;
                        int endY = offsetY + lastRowPieceHeight;

                        figure.AddLine(endX, endY, startX, startY);
                    }
                    else
                    {
                        BezierCurve bottomCurve = BezierCurve.CreateHorizontal(horizontalCurveLength);
                        bottomCurve.FlipHorizontal();

                        if (bottomCurveFlipVertical)
                        {
                            bottomCurve.FlipVertical();
                        }

                        bottomCurve.Translate(offsetX + horizontalCurveLength, offsetY + pieceHeight);
                        figure.AddBeziers(bottomCurve.Points);
                    }

                    #endregion

                    #region Left

                    if (col == 0)
                    {
                        int startX = offsetX;
                        int startY = offsetY;
                        int endX = offsetX;
                        int endY = offsetY + verticalCurveLength;

                        figure.AddLine(endX, endY, startX, startY);
                    }
                    else
                    {
                        BezierCurve verticalCurve = BezierCurve.CreateVertical(verticalCurveLength);
                        verticalCurve.FlipVertical();

                        if (leftCurveFlipHorizontal)
                        {
                            verticalCurve.FlipHorizontal();
                        }

                        verticalCurve.Translate(offsetX, offsetY + verticalCurveLength);
                        figure.AddBeziers(verticalCurve.Points);
                    }

                    #endregion

                    #region Jigsaw information

                    #region Determine adjacent piece IDs for the current piece

                    List<Coordinate> adjacentCoords = new List<Coordinate>
                    {
                        new Coordinate(col, row - 1),
                        new Coordinate(col + 1, row),
                        new Coordinate(col, row + 1),
                        new Coordinate(col - 1, row)
                    };

                    List<int> adjacentPieceIDs = DetermineAdjacentPieceIDs(adjacentCoords, GameSettings.NUM_COLUMNS);

                    #endregion

                    #region Construct piece picture

                    Rectangle figureLocation = Rectangle.Truncate(figure.GetBounds());

                    matrix.Reset();
                    matrix.Translate(0 - figureLocation.X, 0 - figureLocation.Y);
                    GraphicsPath translatedFigure = (GraphicsPath)figure.Clone();
                    translatedFigure.Transform(matrix);

                    Rectangle translatedFigureLocation = Rectangle.Truncate(translatedFigure.GetBounds());

                    Bitmap piecePicture = new Bitmap(figureLocation.Width, figureLocation.Height);

                    using (Graphics gfx = Graphics.FromImage(piecePicture))
                    {
                        gfx.FillRectangle(Brushes.White, 0, 0, piecePicture.Width, piecePicture.Height);
                        gfx.ResetClip();
                        gfx.SetClip(translatedFigure);
                        gfx.DrawImage(_sourcePicture, new Rectangle(0, 0, piecePicture.Width, piecePicture.Height),
                                figureLocation, GraphicsUnit.Pixel);

                        if (GameSettings.DRAW_PIECE_OUTLINE)
                        {
                            gfx.SmoothingMode = SmoothingMode.AntiAlias;
                            gfx.DrawPath(outlinePen, translatedFigure);
                        }
                    }

                    Bitmap modifiedPiecePicture = (Bitmap)piecePicture.Clone();
                    ImageUtilities.EdgeDetectHorizontal(modifiedPiecePicture);
                    ImageUtilities.EdgeDetectVertical(modifiedPiecePicture);
                    piecePicture = ImageUtilities.AlphaBlendMatrix(modifiedPiecePicture, piecePicture, 200);

                    #endregion

                    #region Piece and cluster information

                    bool isHidden = false;

                    if (GameSettings.GameMode)
                    {
                        if ((col==2 && row == 2) 
                            || (col==2 && row == 3) 
                            || (col==3 && row ==2) 
                            || (col == 3 && row == 3))
                        {
                            isHidden = true;
                        }
                    }
                    else
                    {
                        if ((col == 2 && row == 2) 
                            || (col==1 && row == 1) 
                            || (col == 1 && row == 2) 
                            || (col == 2 && row == 1))
                        {
                            isHidden = true;
                        }
                    }
                       

                    Piece piece = new Piece
                    {
                        ID = pieceID,
                        ClusterID = pieceID,
                        Width = figureLocation.Width,
                        Height = figureLocation.Height,
                        BoardLocation = translatedFigureLocation,
                        SourcePictureLocation = figureLocation,
                        MovableFigure = (GraphicsPath)translatedFigure.Clone(),
                        StaticFigure = (GraphicsPath)figure.Clone(),
                        Picture = (Bitmap)piecePicture.Clone(),
                        AdjacentPieceIDs = adjacentPieceIDs,
                        IndexX = col,
                        IndexY = row,
                        Active = !isHidden

                        
                    };

                    PieceCluster cluster = new PieceCluster
                    {
                        ID = pieceID,
                        Width = figureLocation.Width,
                        Height = figureLocation.Height,
                        BoardLocation = translatedFigureLocation,
                        SourcePictureLocation = figureLocation,
                        MovableFigure = (GraphicsPath)translatedFigure.Clone(),
                        StaticFigure = (GraphicsPath)figure.Clone(),
                        Picture = (Bitmap)piecePicture.Clone(),
                        Pieces = new List<Piece> { piece },
                        IndexX = col,
                        IndexY = row,
                        Active = !isHidden
                    };

                    #endregion

                    _clusters.Add(cluster);

                    #endregion
                   
                    pieceID++;
                }
            }

            #endregion

            #region Scramble jigsaw pieces

            Random random = new Random();

            int boardWidth = this.ClientSize.Width;
            int boardHeight = this.ClientSize.Height;

            foreach (PieceCluster cluster in _clusters)
            {
                int locationX = random.Next(1, boardWidth);
                int locationY = random.Next(1, boardHeight);

                #region Make sure the piece is within client rectangle bounds

                if ((locationX + cluster.Width) > boardWidth)
                {
                    locationX = locationX - ((locationX + cluster.Width) - boardWidth);
                }

                if ((locationY + cluster.Height) > boardHeight)
                {
                    locationY = locationY - ((locationY + cluster.Height) - boardHeight);
                }

                #endregion

                for (int index = 0; index < cluster.Pieces.Count; index++)
                {
                    Piece piece = cluster.Pieces[index];
                    piece.BoardLocation = new Rectangle(locationX, locationY, piece.Width, piece.Height);

                    matrix.Reset();
                    matrix.Translate(locationX, locationY);
                    piece.MovableFigure.Transform(matrix);
                }

                cluster.BoardLocation = new Rectangle(locationX, locationY, cluster.Width, cluster.Height);

                matrix.Reset();
                matrix.Translate(locationX, locationY);
                cluster.MovableFigure.Transform(matrix);
            }

            #endregion            
        }

        private ResponseMessage DisplayPuzzle(bool showGhostPicture)
        {
            if (_sourcePicture == null)
            {
                return new ResponseMessage
                {
                    Message = "Please provide source picture."
                };
            }

            int boardWidth = this.ClientSize.Width;
            int boardHeight = this.ClientSize.Height;

            _board = new Bitmap(boardWidth, boardHeight);
            _backBuffer = new Bitmap(boardWidth, boardHeight);
            _background = new Bitmap(boardWidth, boardHeight);

            #region Background tile image

            using (Graphics gfx = Graphics.FromImage(_background))
            {
                if (File.Exists(GameSettings.BACKGROUND_PICTURE_NAME))
                {
                    Bitmap tileImage = new Bitmap("background_tile.png");
                    TextureBrush tileBrush = new TextureBrush(tileImage);
                    gfx.FillRectangle(tileBrush, 0, 0, _background.Width, _background.Height);
                }
                else
                {
                    SolidBrush colorBrush = new SolidBrush(Color.FromArgb(0, 138, 184));

                    gfx.FillRectangle(colorBrush, 0, 0, _background.Width, _background.Height);
                }
            }

            if (showGhostPicture)            
            {
                _background = ImageUtilities.AlphaBlendMatrix(_background, _sourcePicture, GameSettings.GHOST_PICTURE_ALPHA);
            }

            #endregion

            #region Board, backbuffer, and the form

            using (Graphics gfx = Graphics.FromImage(_board))
            {                
                gfx.DrawImageUnscaled(_background, 0, 0);
                
                foreach (PieceCluster cluster in _clusters)
                {
                    if (cluster.Active)
                    {
                        gfx.ResetClip();
                        gfx.SetClip(cluster.MovableFigure);
                        gfx.DrawImage(cluster.Picture, cluster.BoardLocation);
                    }
                }
            }

            using (Graphics gfx = Graphics.FromImage(_backBuffer))
            {
                gfx.DrawImageUnscaled(_board, 0, 0);
            }

            using (Graphics gfx = this.CreateGraphics())
            {
                gfx.DrawImageUnscaled(_board, 0, 0);
            }

            #endregion

            return new ResponseMessage
            {
                Okay = true
            };  
        }

        private List<int> DetermineAdjacentPieceIDs(List<Coordinate> coords, int numColumns)
        {
            List<int> pieceIDs = new List<int>();
            
            foreach (Coordinate coord in coords)
            {
                if (coord.Y >= 0 && coord.Y < GameSettings.NUM_ROWS)
                {
                    if (coord.X >= 0 && coord.X < GameSettings.NUM_COLUMNS)
                    {
                        int pieceID = (coord.Y * numColumns) + coord.X;
                        pieceIDs.Add(pieceID);
                    }
                }
            }

            return pieceIDs;
        }

        private Piece GetPieceByID(int pieceID)
        {
            foreach (PieceCluster cluster in _clusters)
            {
                foreach (Piece piece in cluster.Pieces)
                {
                    if (piece.ID == pieceID)
                    {
                        return piece;
                    }
                }
            }

            return null;
        }

        

        private PieceCluster GetPieceClusterByID(int groupID)
        {
            foreach (PieceCluster group in _clusters)
            {
                if (group.ID == groupID)
                {
                    return group;
                }
            }

            return null;
        }

        private bool RemovePieceGroupByID(int groupID)
        {
            for (int i = 0; i < _clusters.Count; i++)
            {
                if (_clusters[i].ID == groupID)
                {                    
                    _clusters.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        private void SetScreen()
        {
            //FormBorderStyle = FormBorderStyle.None;
            //Rectangle formrect = Screen.GetBounds(this);
            //Location = formrect.Location;
            //Size = formrect.Size;
        }
        #endregion                                        
    }
}
