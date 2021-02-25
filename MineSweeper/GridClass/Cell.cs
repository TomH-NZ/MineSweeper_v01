
// ReSharper disable once CheckNamespace
namespace MineSweeper_v01
{
    public class Cell
    {
        public int RowLocationValue { get; }
        public int ColumnLocationValue { get; }
        public CellStatus CellStatus { get; }
        // ToDo: Add int field for adjacent mines. Add to constructor
        

        public Cell(int row, int column, CellStatus cellStatus)
        {
            RowLocationValue = row;
            ColumnLocationValue = column;
            CellStatus = cellStatus;
        }
    }
}

// ToDo: populate cell with adjacent mines total at time of grid generation. Order would go Grid Generation, mine generation, adjacent mine calculation