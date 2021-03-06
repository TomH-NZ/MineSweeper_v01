using System;
using MineSweeper.Factories;
using MineSweeper.Interfaces;
using MineSweeper.Player;

namespace MineSweeper.Game
{
    public class GameConsole
    {
        private readonly IValidate _userInputValidation = Factory.NewUserInputValidation();
        private readonly IMessageDisplay _gameMessageDisplay = Factory.NewMessageDisplay();
        
        public void NewGame()
        {
            //For the game, [0,0] is located in the top left corner, with the largest row/column being bottom right.
            //Player move is always entered as Row then Column.

            var gridSize = GetGridSize();
            var currentGameGrid = GridFactory.NewGameGrid(gridSize);
            
            var userInputMove = new PlayerMove(0, 0);
            var maxNonMineCells = gridSize * gridSize - gridSize;
            var turnCount = 0;
            
            
            while (!_userInputValidation.IsGameOver(currentGameGrid, userInputMove) && turnCount < maxNonMineCells)
            {
                var runAtGameStart = turnCount == 0;
                userInputMove = RunGame(userInputMove, currentGameGrid, runAtGameStart);
                turnCount++;
            }

            Console.Clear();
            Console.WriteLine(_gameMessageDisplay.EndGameMessage(currentGameGrid, userInputMove));
        }
        
        private PlayerMove RunGame(PlayerMove userInputMove, IGameGrid currentGameGrid, bool runAtGameStart)
        {
            var cellUpdater = Factory.NewCellUpdater();
            var mineUpdater = MineFactory.NewMineChecker();
            var mineGeneration = MineFactory.NewMineLocations();
            var gameGridDisplay = GridFactory.NewDisplayGrid();
            var convertUserInput = Factory.NewUserInputConverter();
            
            if (runAtGameStart) 
            {
                mineUpdater.UpdateCellWithMineStatus(mineGeneration.MineLocations(currentGameGrid.Size), currentGameGrid);
                cellUpdater.UpdateAdjacentMineTotalAtGameStart(currentGameGrid);
            }

            do
            {
                Console.Clear();
                Console.WriteLine(gameGridDisplay.GenerateGameDisplay(currentGameGrid));

                var inputMove = _gameMessageDisplay.ShowUserInputMessage(currentGameGrid.Size);

                userInputMove = convertUserInput.ConvertInputToUserMove(inputMove);
            } while (_userInputValidation.IsCellRevealed(currentGameGrid, userInputMove));

            cellUpdater.UpdateDisplayStatusAfterUserMove(userInputMove, currentGameGrid);

            return userInputMove;
        }

        private int GetGridSize()
        {
            var userInputGridSize = "";
            int size;
            
            while (!_userInputValidation.IsInitialGridSizeValid(userInputGridSize, out size))
            {
                Console.WriteLine("Please enter a grid size between 2 and 10: ");
                userInputGridSize = Console.ReadLine();
            }

            return size;
        }
    }
}