using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using windows_form_01.Controller;
using windows_form_01.Model;
using System.Runtime.InteropServices;

namespace windows_form_01.View
{
    public partial class MainForm : Form
    {
        private GameController gameController;
        private GameModel gameModel;

        public MainForm()
        {
            InitializeComponent();

            toolStripStatusLabel.Text = "Please start the game";
            toolStripTimeLabel.Text = "Time:";

            gameController = GameController.Instance;
            gameModel = GameModel.Instance;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        private void MainForm_Load(object sender, EventArgs e)
        {
            //AllocConsole();

            gameModel.MatrixChanged += GameModel_MatrixChanged;
            gameModel.TimeChanged += GameModel_TimeChanged;
            gameModel.FieldOpened += gameModel_FieldOpened;
            gameModel.FieldMarked += gameModel_FieldMarked;
            gameModel.GameEnded += gameModel_GameEnded;
        }

        private void gameModel_GameEnded(object sender, EventArgs e)
        {
            //minePanel.Enabled = false;
        }

        private void gameModel_FieldMarked(object sender, GameModel.FieldEventArgs e)
        {
            MineField mineField = e.Field;

            Button button = (Button)minePanel.Controls[mineField.Index];

            Color color = mineField.Marked ? Color.OrangeRed : SystemColors.ActiveCaption;
            button.BackColor = color;

            updateStatusLabel();
        }

        private void gameModel_FieldOpened(object sender, GameModel.FieldEventArgs e)
        {
            MineField mineField = e.Field;

            int index = mineField.Index;

            Label label = new Label();
           
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Dock = DockStyle.Fill;


            if (mineField.HasMine)
            {
                label.Font = new Font(FontFamily.GenericSerif, 6);
                label.Text = "MINE";
                label.BackColor = Color.Red;

            }
            else
            {
                label.Font = new Font(FontFamily.GenericSerif, 10);
                int mineCount = mineField.NeighborMineCount;
                label.Text = mineCount == 0 ? String.Empty : mineCount.ToString();
            }

            minePanel.SuspendLayout();
            minePanel.Controls.RemoveAt(index);
            minePanel.Controls.Add(label);
            minePanel.Controls.SetChildIndex(label, index);
            minePanel.ResumeLayout();

            Console.WriteLine("gameModel_FieldOpened:" + mineField.Index);
        }

        private void startGame()
        {
            minePanel.Enabled = true;
            createItems();
            updateStatusLabel();
        }

        private void updateStatusLabel()
        {
            toolStripStatusLabel.Text = "Mines left:" + gameModel.GetRemainedMineCount().ToString();
            statusStrip1.Refresh();
        }

        private void GameModel_MatrixChanged(object sender, EventArgs e)
        {
            startGame();
        }

        private void GameModel_TimeChanged(object sender, EventArgs e)
        {
            toolStripTimeLabel.Text = gameModel.Time.ToString(@"hh\:mm\:ss");
            statusStrip1.Refresh();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameOptionsForm gameOptionsForm = new GameOptionsForm();
            gameOptionsForm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
        }

        private void highscoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HighScoreForm highScoreForm = new HighScoreForm();
            highScoreForm.ShowDialog();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameController.StartGame();
        }

        private void createItems()
        {
            GameDifficulty gameDifficulty = GameOptions.Instance.GameDifficulty;
            int width = gameDifficulty.Width;
            int height = gameDifficulty.Height;

            MineField[] matrix = gameModel.Matrix;

            MinimumSize = new Size(100, 100);

            minePanel.AutoSize = true;

            minePanel.Controls.Clear();
            minePanel.ColumnStyles.Clear();
            minePanel.RowStyles.Clear();

            minePanel.ColumnCount = width;
            minePanel.RowCount = height;

            minePanel.SuspendLayout();

            int index = 0;
            for (int i = 0; i < height; ++i)
            {
                minePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
               
                for (int j = 0; j < width; ++j)
                {
                    minePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
                    index = i * width + j;
                    MineField mineField = matrix[index];

                    var button = new Button
                    {   
                        
                        Tag = index,
                        Dock = DockStyle.Fill,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        UseVisualStyleBackColor = true
                    };



                    button.MouseUp += button_mouseClick;
                    button.MouseEnter += button_MouseEnter;
                    button.MouseLeave += button_MouseLeave;

                    minePanel.Controls.Add(button);
                }
            }

            minePanel.ResumeLayout();

            Size = PreferredSize;
            MinimumSize = PreferredSize;
            
            float heightPercent = (float)100/(float)height;
            foreach (RowStyle rowStyle in minePanel.RowStyles)
            {
                rowStyle.SizeType = SizeType.Percent;
                rowStyle.Height = heightPercent;
            }

            float widthPercent = (float)100 / (float)width;
            foreach (ColumnStyle columnStyle in minePanel.ColumnStyles)
            {
                columnStyle.SizeType = SizeType.Percent;
                columnStyle.Width = widthPercent;
            }
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            if (gameModel.IsGameEnd) return;

            Button button = (Button)sender;
            int index = (int)button.Tag;

            MineField mineField = gameModel.Matrix[index];

            if (!mineField.Marked)
            {
                button.BackColor = SystemColors.Control;
            }
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            if (gameModel.IsGameEnd) return;

            Button button = (Button)sender;
            int index = (int)button.Tag;

            MineField mineField = gameModel.Matrix[index];

            if (!mineField.Marked)
            {
                button.BackColor = SystemColors.ActiveCaption;
            }
        }

        private void button_mouseClick(object sender, MouseEventArgs e)
        {
            if (gameModel.IsGameEnd) return;

            Button button = (Button)sender;
            int index = (int)button.Tag;

            if (e.Button == MouseButtons.Left)
            {
                gameController.OpenField(index);
            }
            else if (e.Button == MouseButtons.Right)
            {
                gameController.ToggleMarkField(index);
            }
        }
    }
}
