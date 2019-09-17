using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Output on my laptop configured for Any CPU:
1500 Solutions created in 0.1079379 seconds.
2250 Solutions created in 0.1589069 seconds.
3375 Solutions created in 0.2158762 seconds.
5062 Solutions created in 0.2868344 seconds.
7593 Solutions created in 0.5286969 seconds.
11389 Solutions created in 0.6246541 seconds.
17083 Solutions created in 1.0523807 seconds.
25624 Solutions created in 1.7070172 seconds.
38436 Solutions created in 2.4116147 seconds.
57654 Solutions created in 3.6908902 seconds.

Solutions per second: 15759.75956997
Press any key to continue . . .


 * Output on my laptop configured for x64:
1500 Solutions created in 0.101942 seconds.
2250 Solutions created in 0.098945 seconds.
3375 Solutions created in 0.1359213 seconds.
5062 Solutions created in 0.216873 seconds.
7593 Solutions created in 0.3218158 seconds.
11389 Solutions created in 0.5167026 seconds.
17083 Solutions created in 0.8365183 seconds.
25624 Solutions created in 1.0693871 seconds.
38436 Solutions created in 1.3722106 seconds.
57654 Solutions created in 2.0388269 seconds.
86481 Solutions created in 3.0472469 seconds.

Solutions per second: 26285.030953305
Press any key to continue . . .


 * Output on my laptop configured for x86:
1500 Solutions created in 0.1379207 seconds.
2250 Solutions created in 0.2028841 seconds.
3375 Solutions created in 0.2138911 seconds.
5062 Solutions created in 0.3188165 seconds.
7593 Solutions created in 0.5796789 seconds.
11389 Solutions created in 0.7285657 seconds.
17083 Solutions created in 1.0793813 seconds.
25624 Solutions created in 1.6210681 seconds.
38436 Solutions created in 2.3956313 seconds.
57654 Solutions created in 3.6518867 seconds.

Solutions per second: 15550.803824477
Press any key to continue . . .
 */

namespace SolutionCreation
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random(0);

            int BatchSize = 1_000;

            var results = new List<(int Batch, double Seconds)>();

            TimeSpan Elapsed;
            do
            {
                BatchSize = (3 * BatchSize) / 2;
                var start = DateTime.Now;
                for (int i = 0; i < BatchSize; i++)
                    SudokuSharp.Factory.Solution(rnd);
                Elapsed = DateTime.Now - start;
                Console.WriteLine($"{BatchSize} Solutions created in {Elapsed.TotalSeconds} seconds.");
                results.Add((BatchSize, Elapsed.TotalSeconds));
            } while (Elapsed.TotalSeconds < 3.0f);

            Console.WriteLine();
            Console.WriteLine($"Solutions per second: {results.Sum(x => x.Batch) / results.Sum(x => x.Seconds)}");
        }
    }
}
