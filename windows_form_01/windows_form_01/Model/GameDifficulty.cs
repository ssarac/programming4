using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windows_form_01.Model
{
    public class GameDifficulty
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Mines { get; set; }
        public bool Editable { get; set; } 

        public GameDifficulty(string name, int width, int height, int mines, bool editable)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Mines = mines;
            this.Editable = editable;
        }

        public bool IsValid()
        {
            bool isValidWidth = Width > 0 && Width < 16;
            bool isValidHeight = Height > 0 && Height < 16;
            bool isValidMines = Mines >= 0 && Mines <= Width * Height;

            bool isValid = isValidWidth && isValidHeight && isValidMines;
            return isValid;
        }

        private static GameDifficulty[] difficulties;
        public static GameDifficulty[] Difficulties
        {
            get
            {
                if (difficulties == null)
                {
                    difficulties = new GameDifficulty[4];
                    difficulties[0] = new GameDifficulty("Beginner", 4, 6, 1, false);
                    difficulties[1] = new GameDifficulty("Intermediate", 8, 8, 12, false);
                    difficulties[2] = new GameDifficulty("Hard", 10, 10, 50, false);
                    difficulties[3] = new GameDifficulty("Not standard", 0, 0, 0, true);
                }
                return difficulties;
            }
        }

    }
}
