using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 100K boards
 * Output on my laptop:
Timing the creation of 100,000 boards.
10,000 boards created in 0.73 seconds for 13707 boards per second.
10,000 boards created in 0.72 seconds for 13832 boards per second.
10,000 boards created in 0.70 seconds for 14265 boards per second.
10,000 boards created in 0.80 seconds for 12423 boards per second.
10,000 boards created in 0.71 seconds for 14034 boards per second.
10,000 boards created in 0.72 seconds for 13810 boards per second.
10,000 boards created in 0.73 seconds for 13753 boards per second.
10,000 boards created in 0.75 seconds for 13396 boards per second.
10,000 boards created in 0.71 seconds for 14073 boards per second.
10,000 boards created in 0.72 seconds for 13892 boards per second.
100,000 boards created in 7.30 seconds for 13692 boards per second.
Press any key to continue . . .
 */

namespace SolutionCreation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Timing the creation of 100,000 boards.");
            var rnd = new Random(0);

            TimeSpan elapsed;
            var start = DateTime.Now;
            for (int i=0; i<10; i++)
            {
                var bStart = DateTime.Now;
                for (int j=0; j<10_000; j++)
                {
                    SudokuSharp.Factory.Solution(rnd);
                }
                elapsed = DateTime.Now - bStart;
                Console.WriteLine("10,000 boards created in {0:0.00} seconds for {1:0} boards per second.", elapsed.TotalSeconds, 10_000 / elapsed.TotalSeconds);
            }
            elapsed = DateTime.Now - start;
            Console.WriteLine("100,000 boards created in {0:0.00} seconds for {1:0} boards per second.", elapsed.TotalSeconds, 100_000 / elapsed.TotalSeconds);
        }
    }
}
