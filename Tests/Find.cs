using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SudokuSharp;
using System.Linq;

namespace Tests
{
    public partial class BoardTests
    {
        [TestMethod, TestCategory("Find")]
        public void FindEmptyLocations()
        {
            for (int iter = 0; iter < Iterations; iter++)
            {
                var src = Factory.Solution(rnd);

                for (int i = 0; i < NumBatches; i++)
                {
                    var work = new Board(src);
                    for (int cut = 0; cut < 60; cut++)
                        work[rnd.Next(81)] = 0;

                    for (int j = 0; j < BatchSize; j++)
                    {
                        var result = work.Find.EmptyLocations();
                    }
                }
            }
        }
    }
}
