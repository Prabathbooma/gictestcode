using MineSweeperApp.Service.Interfaces;


namespace MineSweeperApp.Service.Implements
{
    public class MineSweeperServices : IMineSweeperServices
    {
        public char[,] InitializeMinefield(int size, int mines)
        {
            char[,] minefield = new char[size, size];
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    minefield[i, j] = ' ';
                }
            }

            for (int k = 0; k < mines; k++)
            {
                int i, j;
                do
                {
                    i = random.Next(size);
                    j = random.Next(size);
                } while (minefield[i, j] == '*');

                minefield[i, j] = '*';
            }

            return minefield;
        }

        public char[,] InitializeUserGrid(int size)
        {
            char[,] userGrid = new char[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    userGrid[i, j] = '-';
                }
            }

            return userGrid;
        }

        public void DisplayGrid(char[,] grid)
        {
            int size = grid.GetLength(0);

            Console.WriteLine("Here is your minefield:");
            Console.Write("  ");
            for (int i = 1; i <= size; i++)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();

            for (int i = 0; i < size; i++)
            {
                Console.Write((char)('A' + i) + " ");
                for (int j = 0; j < size; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void UncoverSquare(char[,] minefield, char[,] userGrid, int row, int col)
        {
            int size = minefield.GetLength(0);

            if (row < 0 || row >= size || col < 0 || col >= size || userGrid[row, col] != '-')
            {
                return;
            }

            if (minefield[row, col] == '*')
            {
                return;
            }

            int minesCount = CountAdjacentMines(minefield, row, col);
            userGrid[row, col] = minesCount > 0 ? (char)(minesCount + '0') : '0';

            if (minesCount == 0)
            {
                for (int i = Math.Max(0, row - 1); i <= Math.Min(size - 1, row + 1); i++)
                {
                    for (int j = Math.Max(0, col - 1); j <= Math.Min(size - 1, col + 1); j++)
                    {
                        UncoverSquare(minefield, userGrid, i, j);
                    }
                }
            }
        }

        private static int CountAdjacentMines(char[,] minefield, int row, int col)
        {
            int count = 0;
            int size = minefield.GetLength(0);

            for (int i = Math.Max(0, row - 1); i <= Math.Min(size - 1, row + 1); i++)
            {
                for (int j = Math.Max(0, col - 1); j <= Math.Min(size - 1, col + 1); j++)
                {
                    if (minefield[i, j] == '*')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public bool CheckForWin(char[,] userGrid, int mines)
        {
            int size = userGrid.GetLength(0);
            int uncoveredCount = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (userGrid[i, j] != '-' && userGrid[i, j] != '*')
                    {
                        uncoveredCount++;
                    }
                }
            }

            return uncoveredCount == size * size - mines;
        }

        public bool IsValidInput(string userInput, int size)
        {
            if (userInput.Length < 2)
            {
                Console.WriteLine("Incorrect input.");
                return false;
            }

            int row = userInput[0] - 'A';
            int col;

            if (!int.TryParse(userInput.Substring(1), out col) || col < 1 || col > size)
            {
                Console.WriteLine("Incorrect input.");
                return false;
            }

            return true;
        }

        public bool IsValidgridSize(int size)
        {
            if (size > 2 && size <= 10)
                return true;

            else if (size <= 2)
                Console.WriteLine("Minimum size of grid is 2.");

            else if (size > 10)
                Console.WriteLine("Maximum size of grid is 10.");

            return false;
        }
        public bool IsValidNumberOfMines(int size, int mines)
        {
            if (mines >= 1 && mines <= size * size * 35 / 100)
                return true;

            else if (mines == 0)
                Console.WriteLine("There must be at least 1 mine.");
            else
                Console.WriteLine($"Incorrect input. Maximum number of mines is {size * size * 35 / 100}.");

            return false;
        }

       

    }
}
