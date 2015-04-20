using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windows_form_01.Model
{
    public class GameOptions
    {
        public GameDifficulty GameDifficulty { get; set; }
        public String Name { get; set; }
        
        private GameOptions()
        {
            this.GameDifficulty = new GameDifficulty("4x4", 4, 4, 0, false);
            this.Name = "Anonymous";
        }

        private static GameOptions instance;
        public static GameOptions Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameOptions();
                }
                return instance;
            }
        }
    }
}
