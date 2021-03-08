
// ReSharper disable once CheckNamespace
namespace MineSweeper_v01
{
    public class Validate : IValidate
    {
        public bool IsValidUserMove(string userMove)
        {
            var output = false;
            var validatedUserInput = int.TryParse(userMove, out var number);

            if (validatedUserInput && number >= 0 && number <= 9)
            {
                output = true;
            }

            return output;
        }

        public bool IsCorrectInitialGridSize(string userInput)
        {
            var output = false;
            var validatedUserInput = int.TryParse(userInput, out var number);

            if (validatedUserInput && number >= 2 && number <= 10)
            {
                output = true;
            }

            return output;
        }

        public bool IsPlayerDead(IGameGrid gameGrid, string userInput)
        {
            var output = false;
            var inputMove = userInput.Split(',');
            int.TryParse(inputMove[0], out var row);
            int.TryParse(inputMove[1], out var column);

            if (gameGrid.GeneratedGameCell[row, column].IsAMine)
            {
                output = true;
            }
            
            return output;
        }
    }
}