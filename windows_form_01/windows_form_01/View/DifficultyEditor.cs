using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using windows_form_01.Model;

namespace windows_form_01.View
{
    public partial class DifficultyEditor : UserControl
    {
        public GameDifficulty GameDifficulty { get; set; }
        public DifficultyEditor(GameDifficulty gameDifficulty)
        {
            InitializeComponent();

            this.GameDifficulty = gameDifficulty;

            Init();
        }

        private void Init()
        {
            widthText.Text = GameDifficulty.Width.ToString();
            heightText.Text = GameDifficulty.Height.ToString();
            minesText.Text = GameDifficulty.Mines.ToString();
        }

        public void Save()
        {
            int width;
            int height;
            int mines;

            int.TryParse(widthText.Text, out width);
            int.TryParse(heightText.Text, out height);
            int.TryParse(minesText.Text, out mines);



            GameDifficulty.Width = width;
            GameDifficulty.Height = height;
            GameDifficulty.Mines = mines;
        }
    }
}
