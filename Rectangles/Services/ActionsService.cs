using Rectangles.Models;
using System.Text.RegularExpressions;
using static Rectangles.Constants;

namespace Rectangles.Services
{
    public interface IActionsService
    {
        void DisplayGrid(Grid grid);
        void RemoveRectangle();
        void FindRectangle(Grid grid);
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

        public void DisplayGrid(Grid grid)
        {
            var currentChar = Ascii.First;
            var seenRectangles = new Dictionary<Guid, char>();

            foreach (var row in grid.Occupancy)
            {
                var rowString = string.Empty;
                foreach (var cell in row)
                {
                    if (cell == Guid.Empty)
                    {
                        rowString += Ascii.Empty;
                        continue;
                    }
                    
                    if (seenRectangles.ContainsKey(cell))
                    {
                        rowString += seenRectangles[cell];
                        continue;
                    }

                    var currentSymbol = (char)currentChar++;
                    rowString += currentSymbol;
                    seenRectangles[cell] = currentSymbol;
                }
                Console.WriteLine(rowString);
            }
        }

        public void FindRectangle(Grid grid)
        {
            var validParams = false;
            var x = -1;
            var y = -1;

            while (!validParams)
            {
                _cliService.DisplaySuccess($"The size of your grid is length:{grid.Length} and height:{grid.Height}");
                DisplayIntroPrompt(GameAction.FindRectangle);

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
                    validParams = validX && validY && ValidateFindRectangleInput(x, y, grid);
                }

                if (!validParams)
                {
                    _cliService.DisplayError("Invalid coordinates, please try again");
                }
            }

            // 2D arrays are referenced by array[row][column]. y represents row, x represents column
            var rectangleId = grid.Occupancy[y][x];
            if (rectangleId == Guid.Empty)
            {
                _cliService.DisplaySuccess($"Could not find a rectangle at coordinates {x},{y}");
            }
            else
            {
                _cliService.DisplaySuccess($"Found a rectangle {rectangleId} at coordinates {x},{y}!");
            }
        }

        private bool ValidateFindRectangleInput(int x, int y, Grid grid)
        {
            return x >= 0 && x < grid.Length && y >= 0 && y < grid.Height;
        }

        public void PlaceRectangle()
        {
            DisplayIntroPrompt(GameAction.PlaceRectangle);
            // TODO
        }

        public void RemoveRectangle()
        {
            DisplayIntroPrompt(GameAction.RemoveRectangle);
            // TODO
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
    }
}
