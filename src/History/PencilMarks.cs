using System.Runtime.Serialization;

namespace SudokuSharp.History
{
    /// <summary>
    /// The base class for PencilMark history items. It contains logic useful for all three.
    /// </summary>
    [DataContract]
    public abstract class PencilActionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PencilActionBase"/> class.
        /// </summary>
        /// <param name="Target">The <see cref="PencilGrid"/> being marked.</param>
        /// <param name="Where">The <see cref="Location"/> of the mark.</param>
        /// <param name="Value">The number being marked.</param>
        public PencilActionBase(PencilGrid Target, Location Where, int Value)
        {
            _target = Target;
            _where = Where;
            _value = Value;
        }

        /// <summary>
        /// Internal. Do not reference.
        /// </summary>
        [DataMember]
        protected PencilGrid _target;
        /// <summary>
        /// Internal. Do not reference.
        /// </summary>
        [DataMember]
        protected Location _where;
        /// <summary>
        /// Internal. Do not reference.
        /// </summary>
        [DataMember]
        protected int _value;
        /// <summary>
        /// Internal. Do not reference.
        /// </summary>
        [DataMember]
        protected bool _prior;
    }

    [DataContract]
    public class PencilToggle : PencilActionBase, IHistoryAction
    {
        public PencilToggle(PencilGrid Target, Location Where, int Value) : base(Target, Where, Value) { }
        public void Apply() { _target.Toggle(_where, _value); }
        public void Undo() { Apply(); }
    }

    [DataContract]
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

    [DataContract]
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

