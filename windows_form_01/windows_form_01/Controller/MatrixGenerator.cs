using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using windows_form_01.Model;

namespace windows_form_01.Controller
{
    public class MatrixGenerator
    {
        private GameDifficulty gameDifficulty;
        private int width, height, count;
        private MineField[] matrix;

        public MatrixGenerator(GameDifficulty gameDifficulty)
        {
            this.gameDifficulty = gameDifficulty;

            width = gameDifficulty.Width;
            height = gameDifficulty.Height;
            count = width * height;
        }

        public MineField[] createMatrix()
        {
            matrix = new MineField[count];

            randomizeMines();
            calculateNeightborMines();

            return matrix;
        }

        private void calculateNeightborMines()
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    int index = i * width + j;
                    MineField mineField = matrix[index];
                    mineField.NeighborMineCount = calculateNeighborMineCount(index);
                }
            }
        }

        private int calculateNeighborMineCount(int index)
        {
            int mineCount = 0;

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
                    MineField mineField = matrix[i];
                    if (mineField.HasMine)
                    {
                        mineCount++;
                    }
                }
            }

            return mineCount;
        }

        private void randomizeMines()
        {
            Random random = new Random();

            int[] indexes = new int[count];
            for (int i = 0; i < count; ++i)
            {
                indexes[i] = i;
                matrix[i] = new MineField(i);
            }

            for (int i = 0; i < gameDifficulty.Mines; ++i)
            {
                int index = random.Next(count - i);
                int indexValue = indexes[index];
                indexes[index] = indexes[count - i - 1];

                //Console.WriteLine("mine:" + indexValue);

                MineField mineField = matrix[indexValue];
                mineField.HasMine = true;
            }
        }

    }
}
