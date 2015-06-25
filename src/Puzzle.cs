using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSharp
{
    public partial class Puzzle
    {
        public Puzzle() { }
        public Puzzle(Puzzle src)
        {
            Array.Copy(src.data, this.data, 81);
        }

        public int GetCell(Location where) { return data[where.Index]; }
        public void PutCell(Location where, int value) { data[where.Index] = value; }

        public int[] GetRow(int Row)
        {
            int[] result = new int[9];

            Array.Copy(data, Row * 9, result, 0, 9);

            return result;
        }
        public int[] GetColumn(int Column)
        {
            int[] result = new int[9];
            int idx = Column;
            for (int i = 0; i < 9; i++)
            {
                result[i] = data[idx];
                idx += 9;
            }
            return result;
        }
        public int[] GetZone(int Zone)
        {
            int[] result = new int[9];
            Array.Copy(data, ZoneIndices[Zone], result, 0, 3);
            Array.Copy(data, ZoneIndices[Zone] + 9, result, 3, 3);
            Array.Copy(data, ZoneIndices[Zone] + 18, result, 6, 3);

            return result;
        }
        public List<int> GetCandidates(Location where)
        {
            List<int> result = new List<int>();
            if (data[where.Index] > 0)
                return result;

            bool[] blocking = new bool[10];
            int[] blockingIndices = where.GetConflictingIndices();

            foreach (int idx in blockingIndices)
            {
                blocking[data[idx]] = true;
            }

            for (int i = 1; i < 10; i++)
                if (!blocking[i])
                    result.Add(i);

            return result;
        }
        public static bool AreDistinct(Puzzle first, Puzzle second)
        {
            for (int i = 0; i < 81; i++)
                if (!(first.data[i] == second.data[i]))
                    return true;
            return false;
        }

        public override string ToString()
        {
            string result = data[0].ToString();
            for (int i = 1; i < 81; i++)
            {
                if (i % 27 == 0)
                    result += "\n\n";
                else
                    if (i % 9 == 0)
                    result += "\n";
                else
                        if (i % 3 == 0)
                    result += " ";

                result += data[i];
            }

            return result;
        }

        private int[] data = new int[81];

        private static int[] ZoneIndices = { 0, 3, 6, 27, 30, 33, 54, 57, 60 };
    }
}
