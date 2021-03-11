using System;
using MineSweeper_v01.Enums;

namespace MineSweeper_v01
{
    public class GameConsole
    {
        public void NewGame()
        {
            //For the game, [0,0] is located in the top left corner, with the largest row/column being bottom right.
            //Player move is always entered as Row then Column.
            
            // ToDo: Insert fancy greeting using Figgle.  Add Figgle as module to project.

            var userInputGridSize = "";
            var userInputValidation = Factory.NewUserInputValidation();
            
            while (!userInputValidation.IsInitialGridSizeValid(userInputGridSize))
            {
                Console.WriteLine("Please enter a grid size between 2 and 10: ");
                userInputGridSize = Console.ReadLine();
                userInputValidation.IsInitialGridSizeValid(userInputGridSize);
            }

            int.TryParse(userInputGridSize, out var gridSize);
            var newGameGrid = GridFactory.NewGameGrid(gridSize);
            
            var gameGridDisplay = GridFactory.NewGridDisplay();
            var mineGeneration = MineFactory.NewMineLocations();
            var mineUpdater = MineFactory.NewMineChecker();
            
            var rowOutput = 0;
            var columnOutput = 0;
            var userInputMove = new PlayerMove(rowOutput, columnOutput);
            var turnCount = 0;
            
            while (!userInputValidation.IsGameOver(newGameGrid, userInputMove))
            {
                Console.Clear();
                
                if (turnCount == 0)
                {
                    mineUpdater.UpdateCellWithMineStatus(mineGeneration.MineLocations(gridSize), newGameGrid);
                }
                turnCount++;
                
                Console.WriteLine(gameGridDisplay.GenerateGameDisplay(newGameGrid));
                
                var rowInput = "";
                while (!userInputValidation.IsUserMoveValid(rowInput, newGameGrid.Size))
                {
                    Console.WriteLine($"Please enter a row ( 0 - {gridSize - 1 }): ");
                    rowInput += Console.ReadLine();
                    
                }
                
                var columnInput = "";
                while (!userInputValidation.IsUserMoveValid(columnInput, newGameGrid.Size))
                {
                    Console.WriteLine($"Please enter a column ( 0 - {gridSize - 1 }): ");
                    columnInput = Console.ReadLine();
                }

                int.TryParse(rowInput, out var row);
                int.TryParse(columnInput, out var column);
                userInputMove = new PlayerMove(row, column);
                

                newGameGrid.GeneratedGameCell[userInputMove.Row, userInputMove.Column].DisplayStatus = CellDisplayStatus.Revealed;
                newGameGrid.GeneratedGameCell[userInputMove.Row, userInputMove.Column].AdjacentMinesTotal
                    = mineUpdater.CalculateAdjacentMineTotal(newGameGrid, userInputMove);

                if (turnCount == gridSize * gridSize - gridSize)
                {
                    break;
                }
                userInputValidation.IsGameOver(newGameGrid, userInputMove);
            }
            // ToDo: add bool HasPlayerLost method. If true, run GameOver message 

            if (newGameGrid.GeneratedGameCell[userInputMove.Row, userInputMove.Column].IsMine)
            {
                Console.Clear();
                Console.WriteLine(gameGridDisplay.GameOverDisplay(newGameGrid)); // ToDo: Write logic to display full grid after mine selected.
                Console.WriteLine("Sorry, you have lost.");
                Console.WriteLine("Game Over!");
            }
            else
            {
                Console.WriteLine("Congrats, you have won!");
            }
            
        }
    }
}