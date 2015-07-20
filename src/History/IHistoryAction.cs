namespace SudokuSharp.History
{
    /// <summary>
    /// This interface is used in the Undo / Redo lists of the Puzzle class.
    /// </summary>
    public interface IHistoryAction
    {
        /// <summary>
        /// Applies this instance.
        /// </summary>
        void Apply();
        /// <summary>
        /// Undoes this instance.
        /// </summary>
        void Undo();
    }
}
