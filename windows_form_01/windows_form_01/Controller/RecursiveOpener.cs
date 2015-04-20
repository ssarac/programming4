using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windows_form_01.Model;

namespace windows_form_01.Controller
{
    public class RecursiveOpener
    {
        private GameModel gameModel;
        private GameDifficulty gameDifficulty;

        private int width, height, count;
        private MineField[] matrix;

        public RecursiveOpener(GameModel gameModel, GameDifficulty gameDifficulty)
        {
            this.gameModel = gameModel;
            this.gameDifficulty = gameDifficulty;

            width = gameDifficulty.Width;
            height = gameDifficulty.Height;
            count = width * height;

            matrix = gameModel.Matrix;
        }


        public void Open(MineField mineField)
        {
            gameModel.OpenField(mineField);

            if (mineField.HasMine) return;
            if (mineField.NeighborMineCount != 0) return;

            int index = mineField.Index;

            bool hasLeft = (index % width) != 0;
            bool hasRight = (index % width) != (width - 1);

            int top = index - width;
            int topLeft = -1;
            int topRight = -1;
            int bottom = index + width;
            int bottomLeft = -1;
            int bottomRight = -1;
            int left = -1;
            int right = -1;

            if (hasLeft)
            {
                topLeft = top - 1;
                bottomLeft = bottom - 1;
                left = index - 1;
            }

            if (hasRight)
            {
                topRight = top + 1;
                bottomRight = bottom + 1;
                right = index + 1;
            }

            int[] indexes = new int[]{
                topLeft, top, topRight,
                left, right,
                bottomLeft, bottom, bottomRight
            };

            foreach (int i in indexes)
            {
                if (i >= 0 && i < count)
                {
                    MineField mf = matrix[i];
                    if (!mf.HasMine && !mf.Opened && mf.NeighborMineCount == 0)
                    {
                        Open(mf);
                    }
                }
            }

        }
    }
}
