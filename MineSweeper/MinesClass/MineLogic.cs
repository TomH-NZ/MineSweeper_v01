using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace MineSweeper_v01
{
    public class MineLogic : IMineLogic
    {
        public void UpdateCellWithMineStatus(IEnumerable<Cell> mineLocations, IGameGrid gameGrid)
        {
            var inputMineLocations = mineLocations.ToList();

            for (var row = 0; row < gameGrid.Size; row++)
            {
                for (var column = 0; column < gameGrid.Size; column++)
                {
                    if (inputMineLocations.Contains(gameGrid.GeneratedGameCell[row,column]))
                    {
                        gameGrid.GeneratedGameCell[row, column].IsAMine = true;
                    }
                }
            }
        }

        public int CalculateAdjacentMineTotal(IGameGrid gameGrid, string playerMove)
        {
            var inputMove = playerMove.Split(',');
            var adjacentMinesOutput = 0;
            int.TryParse(inputMove[0], out var row);
            int.TryParse(inputMove[1], out var column);

            var coordinateVariables = new List<int> {-1, 0, 1};

            foreach (var rowVariable in coordinateVariables)
            {
                foreach (var columnVariable in coordinateVariables)
                {
                    if (GreaterThanLowerGridBoundary(row, column, rowVariable, columnVariable) && LowerThanUpperGridBoundary(row, column, rowVariable, columnVariable, gameGrid) 
                        && gameGrid.GeneratedGameCell[row + rowVariable, column + columnVariable].IsAMine)
                    {
                        adjacentMinesOutput += 1;
                    }
                }
            }
            
            return adjacentMinesOutput;
        }

        private bool GreaterThanLowerGridBoundary(int row, int column, int rowVariable, int columnVariable) // ToDo: reduce number of input variables.  how?
        {
            var lowerBoundaryOutput = row + rowVariable >= 0 && column + columnVariable>= 0;

            return lowerBoundaryOutput;
        }

        private bool LowerThanUpperGridBoundary(int row, int column, int rowVariable, int columnVariable,
            IGameGrid gameGrid)
        {
            var upperBoundaryOutput = row + rowVariable < gameGrid.Size && column + columnVariable < gameGrid.Size;

            return upperBoundaryOutput;
        }
    }
}

// ToDo: after integrating adjacent mine logic start writing user input process
