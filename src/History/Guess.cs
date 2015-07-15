namespace SudokuSharp.History
{
    public class Guess : IHistoryAction
    {
        public Guess(Grid Target, Location Where, int Value)
        {
            _target = Target;
            _where = Where;
            _value = Value;
        }

        public void Apply()
        {
            _prior = _target[_where];
            _target[_where] = _value;
        }
        public void Undo()
        {
            _target[_where] = _prior;
        }

        private Grid _target;
        private Location _where;
        private int _value;
        private int _prior;
    }
}
