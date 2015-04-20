using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using windows_form_01.View;
using windows_form_01.Model;

namespace windows_form_01.Controller
{
    public class GameOptionsController
    {
        private IGameOptionsForm gameOptionsForm;
        public GameOptionsController(IGameOptionsForm gameOptionsForm)
        {
            this.gameOptionsForm = gameOptionsForm;
        }

        public bool IsValid()
        {
            if (!gameOptionsForm.GameDifficulty.IsValid())
            {
                MessageBox.Show("Invalid difficulty data");
                return false;
            }

            if (gameOptionsForm.Name.Length == 0)
            {
                MessageBox.Show("Invalid name");
                return false;
            }

            GameOptions gameOptions = GameOptions.Instance;
            gameOptions.GameDifficulty = gameOptionsForm.GameDifficulty;
            gameOptions.Name = gameOptionsForm.Name;

            return true;
        }

    }


}
