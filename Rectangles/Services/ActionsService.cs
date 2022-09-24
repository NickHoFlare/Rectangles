using Rectangles.Models;
using System.Text.RegularExpressions;
using static Rectangles.Constants;

namespace Rectangles.Services
{
    public interface IActionsService
    {
        void DisplayGrid();
        void RemoveRectangle();
        void FindRectangle();
        void PlaceRectangle();
        Grid CreateGrid();
    }

    public class ActionsService : IActionsService
    {
        private readonly ICliService _cliService;

        public ActionsService(ICliService cliService)
        {
            _cliService = cliService;
        }

        public Grid CreateGrid()
        {
            var validParams = false;
            var length = 0;
            var height = 0;

            while (!validParams)
            {
                _cliService.DisplayMessage(GameAction.CreateGrid);
                Console.Write(Messages.CommandPrompt);

                // Grid creation format: length,height
                var regex = new Regex(@"^(\d+),(\d+)$");

                var input = Console.ReadLine() ?? string.Empty;

                var matches = regex.Matches(input);
                if (matches.Count > 0)
                {
                    var validLength = int.TryParse(matches[0].Groups[1].Value, out length);
                    var validHeight = int.TryParse(matches[0].Groups[2].Value, out height);

                    // Grid creation params should only be in the format length,height,
                    // and length/height values must be between 5 and 25
                    validParams = validLength && validHeight && ValidateGridCreationInput(length, height);
                }

                if (!validParams)
                {
                    _cliService.DisplayError("Invalid length or height, please try again");
                }
            }

            var grid = new Grid(length, height);

            _cliService.DisplaySuccess($"New grid created with length of {length} and height of {height}");

            return grid;
        }

        private bool ValidateGridCreationInput(int length, int height)
        {
            return length >= 5 && length <= 25 && height >= 5 && height <= 25;
        }

        public void DisplayGrid()
        {
            _cliService.DisplayMessage(GameAction.DisplayGrid);
            // TODO
        }

        public void FindRectangle()
        {
            _cliService.DisplayMessage(GameAction.FindRectangle);
            // TODO
        }

        public void PlaceRectangle()
        {
            _cliService.DisplayMessage(GameAction.PlaceRectangle);
            // TODO
        }

        public void RemoveRectangle()
        {
            _cliService.DisplayMessage(GameAction.RemoveRectangle);
            // TODO
        }
    }
}
