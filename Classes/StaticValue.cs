using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle
{
    public class StaticValue
    {
        public static string[] BitmapFiles;
        public static int ImageIndex = 0;
        public static int Top = 60;
        public static int Bottom = 20;
        public static DateTime Start;
        public static int TotalMus =10;
        public static string ClapSound = "Sound\\Clap.wav";
        public static string DiesSound = "Sound\\Dies.wav";
        public static string WinpSound = "Sound\\Win.wav";

    }

    public enum FEGameMode
    {
        SIX_TEW = 1,
        FIVE_TEN = 2
    }

    public enum ESound
    {
        Clap = 1,
        Dies = 2,
        Win = 3
    }
}
