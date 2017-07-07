using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Output on my laptop running 10x10_000:
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
            int Batches = 10;
            int BatchSize = 10000;

            Console.WriteLine("Timing the creation of {0:N0} boards.", Batches*BatchSize);
            var rnd = new Random(0);

            TimeSpan elapsed;
            var start = DateTime.Now;
            for (int i=0; i< Batches; i++)
            {
                var bStart = DateTime.Now;
                for (int j=0; j< BatchSize; j++)
                {
                    SudokuSharp.Factory.Solution(rnd);
                }
                elapsed = DateTime.Now - bStart;
                Console.WriteLine("{0:N0} boards created in {1:0.00} seconds for {2:N0} boards per second.", BatchSize, elapsed.TotalSeconds, BatchSize / elapsed.TotalSeconds);
            }
            elapsed = DateTime.Now - start;
            Console.WriteLine("{0:N0} boards created in {1:0.00} seconds for {2:N0} boards per second.", Batches*BatchSize, elapsed.TotalSeconds, (Batches*BatchSize) / elapsed.TotalSeconds);
        }
    }
}
