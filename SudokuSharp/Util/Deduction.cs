using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSharp.Util
{
    public struct Deduction
    {
        Conclusion Conclusion;
        int Location;
        int Value;
    }
}
