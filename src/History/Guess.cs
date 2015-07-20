using System.Runtime.Serialization;

namespace SudokuSharp.History
{
    /// <summary>
    /// A <see cref="Guess"/> is a number placed on the board by the user. It may or may not be correct.
    /// This class implements <see cref="IHistoryAction"/> for use in undo/redo lists.
    /// </summary>
    [DataContract]
    public class Guess : IHistoryAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Guess"/> class.
        /// </summary>
        /// <param name="Target">The board being played.</param>
        /// <param name="Where">The <see cref="Location"/> of the guess.</param>
        /// <param name="Value">The number to place.</param>
        public Guess(Board Target, Location Where, int Value)
        {
            _target = Target;
            _where = Where;
            _value = Value;
        }

        /// <summary>
        /// Performs this action.
        /// Inherited from <see cref="IHistoryAction"/>.
        /// </summary>
        public void Apply()
        {
            _prior = _target[_where];
            _target[_where] = _value;
        }
        /// <summary>
        /// Undoes this instance, returning the target <see cref="Board"/> to its prior state.
        /// Inherited from <see cref="IHistoryAction"/>
        /// </summary>
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
