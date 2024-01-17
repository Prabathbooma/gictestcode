using MineSweeperApp.Service.Implements;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MineSweeperApp.UnitTest")]
namespace MineSweeperApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                PlayGame();

                Console.WriteLine("Press any key to play again...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        static void PlayGame()
        {
            //inject the service 
            var mineSweeperServices = new MineSweeperServices();

            Console.WriteLine("Welcome to Minesweeper!");
            int size = 0;
            int mines = 0;

            // Get valid grid size from user
            while (true)
            {
                Console.Write("Enter the size of the grid (e.g., 4 for a 4x4 grid): ");
                int.TryParse(Console.ReadLine(), out size);
                if (mineSweeperServices.IsValidgridSize(size))
                    break;
            }

            // Get valid number of mines from user
            while (true)
            {
                Console.Write($"Enter the number of mines to place on the grid (maximum is {size * size * 35 / 100}): ");
                int.TryParse(Console.ReadLine(), out mines);
                if (mineSweeperServices.IsValidNumberOfMines(size, mines))
                    break;
            }

            var minefield = mineSweeperServices.InitializeMinefield(size, mines);
            var userGrid = mineSweeperServices.InitializeUserGrid(size);

            while (true)
            {
                mineSweeperServices.DisplayGrid(userGrid);

                string userInput;
                do
                {
                    Console.Write("Select a square to reveal (e.g., A1): ");
                    userInput = Console.ReadLine().ToUpper();
                } while (!mineSweeperServices.IsValidInput(userInput, size));

                int row = userInput[0] - 'A';
                int col = int.Parse(userInput.Substring(1)) - 1;

                if (minefield[row, col] == '*')
                {
                    Console.WriteLine("Oh no, you detonated a mine! Game over.");
                    mineSweeperServices.DisplayGrid(minefield);
                    break;
                }

                mineSweeperServices.UncoverSquare(minefield, userGrid, row, col);

                if (mineSweeperServices.CheckForWin(userGrid, mines))
                {
                    mineSweeperServices.DisplayGrid(userGrid);
                    Console.WriteLine("Congratulations, you have won the game!");
                    break;
                }
            }
        }
    }
}
