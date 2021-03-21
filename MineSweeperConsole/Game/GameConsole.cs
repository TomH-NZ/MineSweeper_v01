using System;
using MineSweeper.Factories;
using MineSweeper.Interfaces;
using MineSweeper.Player;

namespace MineSweeper.Game
{
    public class GameConsole
    {
        private readonly IConvertUserInput _convertUserInput = Factory.newUserInputConverter();
        private readonly IMineGenerator _mineGeneration = MineFactory.NewMineLocations();
        private readonly IDisplayGrid _gameGridDisplay = GridFactory.NewDisplayGrid();
        private readonly IUpdateCell _updateCell = Factory.newCellUpdater();
        private readonly IMineLogic _mineUpdater = MineFactory.NewMineChecker();
        private readonly IValidate _userInputValidation = Factory.NewUserInputValidation();
        private readonly IDisplay _gameDisplayLogic = Factory.NewDisplayLogic();
        private int _turnCount;
        

        public void NewGame()
        {
            //For the game, [0,0] is located in the top left corner, with the largest row/column being bottom right.
            //Player move is always entered as Row then Column.

            // ToDo: Insert fancy greeting using Figgle.  Add Figgle as module to project.
            // ToDo: use emoji for bomb??

            var gridSize = GetGridSize();
            var currentGameGrid = GridFactory.NewGameGrid(gridSize);
            
            var userInputMove = new PlayerMove(0, 0);
            
            while (!_userInputValidation.IsGameOver(currentGameGrid, userInputMove) && _turnCount < gridSize * gridSize - gridSize)
            {
                userInputMove = RunGame(gridSize, userInputMove, currentGameGrid);
            }

            Console.Clear();
            Console.WriteLine(_gameGridDisplay.GameOverDisplay(currentGameGrid));

            Console.WriteLine(
                currentGameGrid.GeneratedGameCell[userInputMove.Row, userInputMove.Column]
                    .IsMine
                    ? $"Sorry, you have lost.{Environment.NewLine}Game over!"
                    : $"Congrats!{Environment.NewLine}You have won!");
        }

        private PlayerMove RunGame(int gridSize, PlayerMove userInputMove, IGameGrid currentGameGrid)
        {
            if (_turnCount == 0)
            {
                _mineUpdater.UpdateCellWithMineStatus(_mineGeneration.MineLocations(gridSize), currentGameGrid);
            }

            do
            {
                Console.Clear();
                Console.WriteLine(_gameGridDisplay.GenerateGameDisplay(currentGameGrid));

                var inputMove = _gameDisplayLogic.ShowUserInputMessage(gridSize);

                userInputMove = _convertUserInput.ConvertUserInputToUserMove(inputMove);
                _turnCount++;
            } while (_userInputValidation.IsCellRevealed(currentGameGrid, userInputMove));

            _updateCell.DisplayStatusAfterUserMove(userInputMove, currentGameGrid);
            _updateCell.AdjacentMineTotalAfterUserMove(userInputMove, currentGameGrid);

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