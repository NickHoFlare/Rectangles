using Rectangles.Models;
using System.Text.RegularExpressions;
using static Rectangles.Constants;

namespace Rectangles.Services
{
    public interface IPromptService
    {
        Rectangle GetRectangle(Grid grid);
        Dimensions GetCreateGridDimensions();
        Coordinates GetCoordinates(string gameAction, Grid grid);
    }

    public class PromptService : IPromptService
    {
        private readonly ICliService _cliService;

        public PromptService(ICliService cliService, IActionsService actionService)
        {
            _cliService = cliService;
        }

        public Dimensions GetCreateGridDimensions()
        {
            var validParams = false;
            var length = -1;
            var height = -1;

            while (!validParams)
            {
                DisplayIntroPrompt(GameAction.CreateGrid);

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
                    _cliService.DisplayError(Messages.InvalidInput);
                }
            }

            return new Dimensions(length, height);
        }

        private bool ValidateGridCreationInput(int length, int height)
        {
            return length >= 5 && length <= 25 && height >= 5 && height <= 25;
        }

        /// <summary>
        /// Given a user's action and the Grid, prompt the user to provide x,y coordinates.
        /// This functionality can be shared amongst several game actions e.g. Find and Remove Rectangle
        /// </summary>
        /// <param name="gameAction"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        public Coordinates GetCoordinates(string gameAction, Grid grid)
        {
            var validParams = false;
            var x = -1;
            var y = -1;

            while (!validParams)
            {
                DisplayRectangleActionIntroPrompt(gameAction, grid);

                // Coordinates format: x,y
                var regex = new Regex(@"^(\d+),(\d+)$");

                var input = Console.ReadLine() ?? string.Empty;

                var matches = regex.Matches(input);
                if (matches.Count > 0)
                {
                    var validX = int.TryParse(matches[0].Groups[1].Value, out x);
                    var validY = int.TryParse(matches[0].Groups[2].Value, out y);

                    // Coordinates params should only be in the format x,y,
                    // and x/y values should not exceed the bounds of the grid
                    validParams = validX && validY && ValidateCoordinatesInput(x, y, grid);
                }

                if (!validParams)
                {
                    _cliService.DisplayError(Messages.InvalidInput);
                }
            }

            return new Coordinates(x, y);
        }

        private bool ValidateCoordinatesInput(int x, int y, Grid grid)
        {
            return x >= 0 && x < grid.Length && y >= 0 && y < grid.Height;
        }

        public Rectangle GetRectangle(Grid grid)
        {
            var validParams = false;
            var length = -1;
            var height = -1;
            var x = -1;
            var y = -1;

            while (!validParams)
            {
                DisplayRectangleActionIntroPrompt(GameAction.PlaceRectangle, grid);

                // Rectangles format: length,height,x,y
                var regex = new Regex(@"^(\d+),(\d+),(\d+),(\d+)$");

                var input = Console.ReadLine() ?? string.Empty;

                var matches = regex.Matches(input);
                if (matches.Count > 0)
                {
                    var validLength = int.TryParse(matches[0].Groups[1].Value, out length);
                    var validHeight = int.TryParse(matches[0].Groups[2].Value, out height);
                    var validX = int.TryParse(matches[0].Groups[3].Value, out x);
                    var validY = int.TryParse(matches[0].Groups[4].Value, out y);

                    // Rectangle params should only be in the format length,height,x,y,
                    // length/height values are non-zero, and the rectangle's footprint should fit in the grid
                    validParams = validLength && validHeight && validX && validY && ValidatePlaceRectangleInput(length, height, x, y, grid);
                }

                if (!validParams)
                {
                    _cliService.DisplayError(Messages.InvalidInput);
                }
            }

            return new Rectangle(x, y, length, height);
        }

        private bool ValidatePlaceRectangleInput(int length, int height, int x, int y, Grid grid)
        {
            var bottomRight = new Coordinates(x + (length - 1), y + (height - 1));

            return length > 0 && height > 0 && bottomRight.X < grid.Length && bottomRight.Y < grid.Height;
        }

        /// <summary>
        /// Depending on which action the user chose from the main menu,
        /// this displays the introductory prompt for that action.
        /// </summary>
        /// <param name="gameAction"></param>
        private void DisplayIntroPrompt(string gameAction)
        {
            _cliService.DisplayMessage(gameAction);
            Console.Write(Messages.CommandPrompt);
        }

        /// <summary>
        /// Does the same thing as DisplayIntroPrompt, but also provides information about an existing grid
        /// </summary>
        /// <param name="gameAction"></param>
        private void DisplayRectangleActionIntroPrompt(string gameAction, Grid grid)
        {
            _cliService.DisplaySuccess($"The size of your grid is length:{grid.Length} and height:{grid.Height}");
            DisplayIntroPrompt(gameAction);
        }
    }
}
