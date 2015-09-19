using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var solution = SudokuSharp.Board.CreateSolution(0);
            var puzzle = SudokuSharp.Board.CreatePuzzle(solution, 0, 20, 4);
        }
    }
}
