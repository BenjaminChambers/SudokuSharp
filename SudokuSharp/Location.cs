using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSharp
{
    static public class Location
    {
        static public int Index(int Order, int Row, int Column)
            => (Order * Order) * Row + Column;
        static public int Row(int Order, int Index)
            => Index / (Order * Order);
        static public int Column(int Order, int Index)
            => Index % (Order * Order);
        static public int Zone(int Order, int Index)
            => Row(Order, Index) - (Row(Order, Index) % Order) + (Column(Order, Index) / Order);

        static public IEnumerable<int> RowIndices(int Order, int Row)
        {
            int offset = Row * Order * Order;

            foreach (var x in Enumerable.Range(0, Order * Order))
                yield return offset + x;
        }

        static public IEnumerable<int> ColumnIndices(int Order, int Column)
        {
            int Size = Order * Order;
            for (int x = Column; x < Size * Size; x += Size)
                yield return x;
        }

        static public IEnumerable<int> ZoneIndices(int Order, int Zone)
        {
            int Size = Order * Order;

            var zRow = Zone - (Zone % Order);
            var zCol = (Zone % Order) * Order;
            for (int iR = 0; iR < Order; iR++)
                for (int iC = 0; iC < Order; iC++)
                    yield return (zRow + iR) * Size + zCol + iC;
        }

        static public IEnumerable<int> BlockingIndices(int Order, int Index)
        {
            int Size = Order * Order;

            int r = Row(Order, Index);
            int c = Column(Order, Index);

            // Walk the row, skipping Index
            for (int x = r * Size; x < Index; x++) yield return x;
            for (int x = Index + 1; x < (r + 1) * Size; x++) yield return x;

            // Then the column
            for (int x = c; x < Index; x += Size) yield return x;
            for (int x = Index + Size; x < Size * Size; x += Size) yield return x;

            throw new NotImplementedException();
            // Then the zone, skipping the row and column already enumerated
        }
    }
}
