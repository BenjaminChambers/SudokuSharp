namespace SudokuSharp
{
    public partial class Board
    {
        // TODO:
        // Add Hidden Single filling, pairs, triples, quads
        // Add Locked Candidates
        // Add X-Wing, XY-Wing and XYZ-Wing
        // Add Swordfish
        // Add Nishio (proof by contradiction)

        private int FillNakedSingles()
        {
            int count = 0;

            int countThisRun = 0;
            do
            {
                countThisRun = 0;
                foreach (Location loc in Location.All)
                {
                    if (GetCell(loc)==0)
                    {
                        var possible = GetCandidates(loc);
                        if (possible.Count == 1)
                        {
                            PutCell(loc, possible[0]);
                            countThisRun++;
                        }
                    }
                }
                count += countThisRun;
            } while (countThisRun > 0);

            return count;
        }
    }
}
