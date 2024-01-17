using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperApp.Service.Interfaces
{
   public interface IMineSweeperServices
    {
        public char[,] InitializeUserGrid(int size);
        public char[,] InitializeMinefield(int size, int mines);
        public void DisplayGrid(char[,] grid);
        public void UncoverSquare(char[,] minefield, char[,] userGrid, int row, int col);
        public bool CheckForWin(char[,] userGrid, int mines);
        public bool IsValidInput(string userInput, int size);
        public bool IsValidgridSize(int size);
        public bool IsValidNumberOfMines(int size, int mines);
    }
}
