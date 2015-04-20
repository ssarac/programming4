using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace windows_form_01.Model
{
    public class GameModel
    {
        public int OpenedCount { get; set; }
        public int TotalFieldCount { get; set; }
        public int MineCount { get; set; }
        public int MarkedMineCount { get; set; }
        public bool IsGameEnd { get; set; }
        public int TimeSecond { get; set; }

        public int GetRemainedMineCount()
        {
            return MineCount - MarkedMineCount;
        }

        public bool IsAllMinesDiscovered()
        {
                bool isDiscovered = (GetRemainedMineCount() == 0) &&  TotalFieldCount == (MarkedMineCount + OpenedCount);
                Console.WriteLine("total:" + TotalFieldCount + " " + GetRemainedMineCount() + " " + MarkedMineCount + " " + OpenedCount);
                return isDiscovered;
        }

        private MineField[] matrix;
        public MineField[] Matrix 
        {
            get { return matrix; }

            set
            {
                matrix = value;
                OnMatrixChanged();
            }
        }

        private TimeSpan time;
        public TimeSpan Time 
        {
            get { return time; }

            set
            {
                time = value;
                OnTimeChanged();
            }
        }

        private GameModel()
        {

        }

        public void Reset()
        {
            IsGameEnd = false;
            OpenedCount = 0;
            MarkedMineCount = 0;
            Time = TimeSpan.Zero;
            TimeSecond = 0;
        }

        public void GameEnd()
        {
            IsGameEnd = true;
            OnGameEnded();
        }

        public void OpenField(MineField mineField)
        {
            OpenedCount++;
            mineField.Opened = true;
            OnFieldOpened(mineField);
        }

        public void MarkField(MineField mineField)
        {
            OnFieldMarked(mineField);
        }

        private static GameModel instance;
        public static GameModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameModel();
                }

                return instance;
            }
        }

        public event EventHandler TimeChanged;
        private void OnTimeChanged()
        {
            if (TimeChanged != null)
            {
                TimeChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler MatrixChanged;
        private void OnMatrixChanged()
        {
            if (MatrixChanged != null)
            {
                MatrixChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler GameEnded;
        private void OnGameEnded()
        {
            if (GameEnded != null)
            {
                GameEnded(this, EventArgs.Empty);
            }
        }

        public event EventHandler<FieldEventArgs> FieldOpened;
        private void OnFieldOpened(MineField field)
        {
            if (FieldOpened != null)
            {
                FieldOpened(this, new FieldEventArgs { Field=field });
            }
        }

        public event EventHandler<FieldEventArgs> FieldMarked;
        private void OnFieldMarked(MineField field)
        {
            if (FieldMarked != null)
            {
                FieldMarked(this, new FieldEventArgs { Field = field });
            }
        }

        public class FieldEventArgs : EventArgs
        {
            public MineField Field { get; set; }
        }

    }


}
