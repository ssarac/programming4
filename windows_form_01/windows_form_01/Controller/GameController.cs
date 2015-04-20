using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using windows_form_01.Model;

namespace windows_form_01.Controller
{
    public class GameController
    {
        private Timer timer;
        private DateTime startTime;

        private GameModel gameModel;
        private RecursiveOpener recursiveOpener;

        private GameController()
        {
            gameModel = GameModel.Instance;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
        }

        private static GameController instance;
        public static GameController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameController();
                }
                return instance;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
           // gameModel.Time = DateTime.Now.Subtract(startTime);
            gameModel.TimeSecond ++;
            gameModel.Time = TimeSpan.FromSeconds(gameModel.TimeSecond);
        }

        private void initGameModel()
        {
            GameDifficulty gameDifficulty = GameOptions.Instance.GameDifficulty;
            MatrixGenerator matrixGenerator = new MatrixGenerator(gameDifficulty);

            gameModel.Reset();
            gameModel.TotalFieldCount = gameDifficulty.Width * gameDifficulty.Height;
            gameModel.MineCount = gameDifficulty.Mines;
            gameModel.Matrix = matrixGenerator.createMatrix();
        }

        public void StartGame()
        {
            initGameModel();

            recursiveOpener = new RecursiveOpener(gameModel, GameOptions.Instance.GameDifficulty);

            startTime = DateTime.Now;
            timer.Start();
        }

        public void StopGame()
        {
            timer.Stop();
            gameModel.GameEnd();
        }

        public void OpenField(int index)
        {
            MineField mineField = gameModel.Matrix[index];

            if (!mineField.Marked)
            {
                recursiveOpener.Open(mineField);
            }

            if (mineField.HasMine)
            {
                StopGame();
                showFailPopup();
            }
            else if (gameModel.IsAllMinesDiscovered())
            {
                StopGame();
                saveGameScore();
                showWinPopup();
            }
        }

        public void ToggleMarkField(int index)
        {
            MineField mineField = gameModel.Matrix[index];
            mineField.Marked = !mineField.Marked;

            int i = mineField.Marked ? 1 : -1;
            gameModel.MarkedMineCount += i;

            gameModel.MarkField(mineField);

            if (gameModel.IsAllMinesDiscovered())
            {
                StopGame();
                saveGameScore();
                showWinPopup();
            }
        }

        private void saveGameScore()
        {
            GameOptions gameOptions = GameOptions.Instance;
            GameScore gameScore = new GameScore(gameOptions.Name, gameModel.Time);

            GameScore.AddScore(gameScore);
        }

        private void showFailPopup()
        {
            MessageBox.Show("You Lose! :(");
        }

        private void showWinPopup()
        {
            MessageBox.Show("You Won! :)");
        }
    }
}
