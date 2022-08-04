using System.Drawing;

namespace Puzzle
{    
    public static class GameSettings
    {
        public static readonly string BACKGROUND_PICTURE_NAME = "background_tile.png";

        public static readonly int MIN_PIECE_WIDTH = 50;  
        public static readonly int MIN_PIECE_HEIGHT = 50;  

        public static int NUM_ROWS = 14;
        public static int NUM_COLUMNS = 7;
        
        public static readonly int SNAP_TOLERANCE = 15;
        public static readonly byte GHOST_PICTURE_ALPHA = 127;
        
        public static readonly int PIECE_OUTLINE_WIDTH =1;
        public static readonly bool DRAW_PIECE_OUTLINE = true;

        public static readonly int DROP_SHADOW_DEPTH = 3;
        public static readonly Color DROP_SHADOW_COLOR = Color.FromArgb(100, 50, 50, 50);

        public static bool DISPLAY_BACKGROUND = false;

        public static bool GameMode = true;
    }
}
