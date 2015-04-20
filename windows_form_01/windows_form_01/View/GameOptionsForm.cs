using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using windows_form_01.Model;
using windows_form_01.Controller;

namespace windows_form_01.View
{
    public partial class GameOptionsForm : Form, IGameOptionsForm
    {
        public GameDifficulty GameDifficulty { get; set; }
        public GameOptionsController Controller{ get; set; }

        public string Name 
        { 
            get { return nameText.Text; }
            set { nameText.Text = value; }
        }
        
        public GameOptionsForm()
        {
            InitializeComponent();

            Init();

            this.Controller = new GameOptionsController(this);
        }

        private void Init()
        {
            GameDifficulty = GameOptions.Instance.GameDifficulty;

            GameOptions gameOptions = GameOptions.Instance;
            nameText.Text = gameOptions.Name;

            InitRadioButtons();
        }

        private void InitRadioButtons()
        {
            GameDifficulty[] difficulties = GameDifficulty.Difficulties;

            Point leftPos = new Point(15, 15);
            Point rightPos = new Point(120, 15);

            bool found = false;
            RadioButton firstRadioButton = null;

            foreach (GameDifficulty gameDifficulty in difficulties)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = gameDifficulty.Name;
                radioButton.CheckedChanged += radioButton_CheckedChanged;

                if (firstRadioButton == null)
                {
                    firstRadioButton = radioButton;
                }

                if (gameDifficulty.Editable)
                {
                    radioButton.Location = new Point(rightPos.X, rightPos.Y);
                    rightPos.Y += radioButton.Size.Height;

                    DifficultyEditor difficultyEditor = new DifficultyEditor(gameDifficulty);
                    difficultyEditor.Location = new Point(rightPos.X, rightPos.Y);
                    difficultyEditor.Enabled = false;

                    radioButton.Tag = difficultyEditor;
                    difficultyEditor.Tag = gameDifficulty;

                    difficultyGroup.Controls.Add(radioButton);
                    difficultyGroup.Controls.Add(difficultyEditor);
                }
                else
                {
                    radioButton.Location = new Point(leftPos.X, leftPos.Y);
                    leftPos.Y += radioButton.Size.Height;

                    radioButton.Tag = gameDifficulty;
                    difficultyGroup.Controls.Add(radioButton);
                }

                if (gameDifficulty == GameDifficulty)
                {
                    radioButton.Select();
                    found = true;
                }
            }

            if (!found && firstRadioButton != null)
            {
                firstRadioButton.Select();
            }
        }

        private DifficultyEditor currentDifficultyEditor = null;
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (currentDifficultyEditor != null)
            {
                currentDifficultyEditor.Enabled = false;
                currentDifficultyEditor = null;
            }

            if (radioButton.Tag is GameDifficulty)
            {
                GameDifficulty difficulty = (GameDifficulty)radioButton.Tag;
                GameDifficulty = difficulty;
            }
            else if (radioButton.Tag is DifficultyEditor)
            {
                DifficultyEditor difficultyEditor = (DifficultyEditor)radioButton.Tag;
                difficultyEditor.Enabled = true;
                GameDifficulty = difficultyEditor.GameDifficulty;

                this.currentDifficultyEditor = difficultyEditor;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (currentDifficultyEditor != null)
            {
                currentDifficultyEditor.Save();
            }

            if(Controller.IsValid())
            {
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
