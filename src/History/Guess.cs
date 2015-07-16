using System.Runtime.Serialization;

namespace SudokuSharp.History
{
    [DataContract]
    public class Guess : IHistoryAction
    {
        public Guess(Board Target, Location Where, int Value)
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

        [DataMember]
        private Board _target;
        [DataMember]
        private Location _where;
        [DataMember]
        private int _value;
        [DataMember]
        private int _prior;
    }
}
