using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Puzzle.Properties;

namespace Puzzle
{
    public partial class FrmMenu : Form
    {
        private IMain _FrmMain;
        public FrmMenu(IMain f)
        {
            InitializeComponent();
            _FrmMain = f;
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void LoadSetting()
        {
            //cbxHint.Checked = Settings.Default.ShowImageHint;
            Settings.Default.ShowImageHint = false;
            Settings.Default.Save();
            //StaticValue.TotalMus = Settings.Default.TimeForPlay;
            //GameSettings.NUM_COLUMNS = Settings.Default.NumberOfColumn;
            // GameSettings.NUM_ROWS    = Settings.Default.NumberOfRow;
            // GameSettings.DISPLAY_BACKGROUND = Settings.Default.ShowBackground;
            numTime.Value = StaticValue.TotalMus;
           
            GameSettings.GameMode = cbxGameMode.Checked;
            SetSize();

            //cbxShowBg.Checked = GameSettings.DISPLAY_BACKGROUND;
        }

        private void SetSize()
        {
            if (GameSettings.GameMode)
            {
                GameSettings.NUM_COLUMNS = 6;
                GameSettings.NUM_ROWS = 12;
            }
            else
            {
                GameSettings.NUM_COLUMNS = 5;
                GameSettings.NUM_ROWS = 10;
            }
        }
        private void cbxHint_CheckedChanged(object sender, EventArgs e)
        {
            //Settings.Default.ShowImageHint = cbxHint.Checked;
            //Settings.Default.Save();
        }

        private void numTime_ValueChanged(object sender, EventArgs e)
        {
            StaticValue.TotalMus = (int)numTime.Value;
            Settings.Default.TimeForPlay = StaticValue.TotalMus;
            Settings.Default.Save();
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {
            _FrmMain.ExitProgram();
        }

        private void numNumOfRows_ValueChanged(object sender, EventArgs e)
        {
            //GameSettings.NUM_ROWS = (int)numNumOfRows.Value;
            //Settings.Default.NumberOfRow = GameSettings.NUM_ROWS;
            //GameSettings.NUM_COLUMNS = GameSettings.NUM_ROWS;
           // Settings.Default.NumberOfColumn = GameSettings.NUM_COLUMNS;
           // Settings.Default.Save();
        }

        private void cbxShowBg_CheckedChanged(object sender, EventArgs e)
        {
            //GameSettings.DISPLAY_BACKGROUND = cbxShowBg.Checked;
            //Settings.Default.ShowBackground = GameSettings.DISPLAY_BACKGROUND;
            //Settings.Default.Save();
        }

        private void cbxGameMode_CheckedChanged(object sender, EventArgs e)
        {
            GameSettings.GameMode = cbxGameMode.Checked;
            SetSize();
        }
    }
}
