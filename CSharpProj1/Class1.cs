using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProj1
{
    public class TicTacToe
    {
        private int[,] field;

        public TicTacToe()
        {
            field = new int[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    field[i, j] = 0;
        }

        public bool Click(int row, int col, int player)
        {
            // Проверяем, что клетка пуста
            if (field[row, col] == 0)
            {
                field[row, col] = player;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Scan()
        {
            int winner = 0;

            // Проверка горизонтальных и вертикальных линий
            for (int i = 0; i < 3; i++)
            {
                if (field[i, 0] == field[i, 1] && field[i, 1] == field[i, 2] && field[i, 2] != 0)
                    winner = field[i, 0];

                if (field[0, i] == field[1, i] && field[1, i] == field[2, i] && field[2, i] != 0)
                    winner = field[0, i];
            }

            // Проверка диагональных линий
            if (field[0, 0] == field[1, 1] && field[1, 1] == field[2, 2] && field[2, 2] != 0)
                winner = field[0, 0];
            if (field[2, 0] == field[1, 1] && field[1, 1] == field[0, 2] && field[0, 2] != 0)
                winner = field[2, 0];

            bool draw = true;
            // Проверка на ничью
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (field[i, j] == 0)
                    {
                        draw = false;
                        break;
                    }
                }
            }

            if (draw) return 3;
            return winner;
        }
    }

}
