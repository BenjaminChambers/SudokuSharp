using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SudokuSharp
{
    public class Scratchpad
    {
        public Scratchpad(int Order)
        {
            this.Order = Order;
            Size = Order * Order;
            int Limit = Size * Size;

            _data = new BitArray[Limit];
            for (int i = 0; i < Limit; i++)
                _data[i] = new BitArray(Size+1);
        }

        public Scratchpad(Board Source)
            : this(Source.Order)
        {

        }

        public BitArray this[int Location]
            => _data[Location];
        public readonly int Order;
        public readonly int Size;

        private readonly BitArray[] _data;
    }
}
