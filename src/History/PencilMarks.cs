namespace SudokuSharp.History
{
    public abstract class PencilActionBase
    {
        public PencilActionBase(PencilGrid Target, Location Where, int Value)
        {
            _target = Target;
            _where = Where;
            _value = Value;
        }

        protected PencilGrid _target;
        protected Location _where;
        protected int _value;
        protected bool _prior;
    }

    public class PencilToggle : PencilActionBase, IHistoryAction
    {
        public PencilToggle(PencilGrid Target, Location Where, int Value) : base(Target, Where, Value) { }
        public void Apply() { _target.Toggle(_where, _value); }
        public void Undo() { Apply(); }
    }

    public class PencilSet : PencilActionBase, IHistoryAction
    {
        public PencilSet(PencilGrid Target, Location Where, int Value) : base(Target, Where, Value) { }
        public void Apply()
        {
            _prior = _target.Get(_where, _value);
            _target.Set(_where, _value);
        }
        public void Undo() {
            if (!_prior)
                _target.Clear(_where, _value);
        }
    }

    public class PencilClear : PencilActionBase, IHistoryAction
    {
        public PencilClear(PencilGrid Target, Location Where, int Value) : base(Target, Where, Value) { }
        public void Apply()
        {
            _prior = _target.Get(_where, _value);
            _target.Clear(_where, _value);
        }
        public void Undo()
        {
            if (_prior)
                _target.Set(_where, _value);
        }
    }
}

