namespace SudokuSharp.History
{
    public interface IHistoryAction
    {
        void Apply();
        void Undo();
    }
}
