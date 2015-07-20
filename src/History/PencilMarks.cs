using System.Runtime.Serialization;

namespace SudokuSharp.History
{
    [DataContract]
    internal abstract class PencilActionBase
    {
        internal PencilActionBase(PencilGrid Target, Location Where, int Value)
        {
            _target = Target;
            _where = Where;
            _value = Value;
        }
        
        [DataMember]
        protected PencilGrid _target;
        [DataMember]
        protected Location _where;
        [DataMember]
        protected int _value;
        [DataMember]
        protected bool _prior;
    }
    
    [DataContract]
    internal class PencilToggle : PencilActionBase, IHistoryAction
    {
        public PencilToggle(PencilGrid Target, Location Where, int Value) : base(Target, Where, Value) { }
        public void Apply() { _target.Toggle(_where, _value); }
        public void Undo() { Apply(); }
    }

    [DataContract]
    internal class PencilSet : PencilActionBase, IHistoryAction
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

    [DataContract]
    internal class PencilClear : PencilActionBase, IHistoryAction
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

