using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSharp
{
    // Tu be used
    class CandidateSet
    {

        // Key = Location
        // Value = Possible values
        Dictionary<int, HashSet<int>> _data;
    }
}
