using MineSweeper_v01.Enums;

// ReSharper disable once CheckNamespace

namespace MineSweeper_v01
{
    public class Cell : ICell
    {
        public int RowLocationValue { get; }
        public int ColumnLocationValue { get; }
        public bool IsAMine { get; set; }
        public int NumberOfAdjacentMines { get; set; }
        public CellDisplayStatus DisplayStatus { get; set; } // ToDo: Enum?? revealed / notRevealed / markedAsMine
        // ToDo: Add bool IsMarkedAsMine property for stretch goal

        public Cell(int row, int column)
        {
            RowLocationValue = row;
            ColumnLocationValue = column;
            IsAMine = false;
            DisplayStatus = CellDisplayStatus.NotRevealed;
        }
    }
}
