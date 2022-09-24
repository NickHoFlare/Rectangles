using Rectangles.Models;
using System.Text.RegularExpressions;
using static Rectangles.Constants;

namespace Rectangles.Services
{
    public interface IActionsService
    {
        void DisplayGrid(Grid grid);
        void RemoveRectangle(Grid grid);
        void FindRectangle(Grid grid);
        void PlaceRectangle(Grid grid);
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
                    _cliService.DisplayError(Messages.InvalidInput);
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
            var coordinates = ObtainCoordinatesFromInput(GameAction.RemoveRectangle, grid);

            var rectangleId = GetRectangleId(coordinates, grid);
            if (rectangleId == Guid.Empty)
            {
                _cliService.DisplaySuccess($"Did not find a rectangle at coordinates {coordinates.X},{coordinates.Y}");
            }
            else
            {
                _cliService.DisplaySuccess($"Found a rectangle {rectangleId} at coordinates {coordinates.Y},{coordinates.Y}!");
            }
        }

        public void PlaceRectangle(Grid grid)
        {
            var validParams = false;
            var length = -1;
            var height = -1;
            var x = -1;
            var y = -1;

            while (!validParams)
            {
                DisplayRectangleActionIntroPrompt(GameAction.PlaceRectangle, grid);

                // Coordinates format: x,y
                var regex = new Regex(@"^(\d+),(\d+),(\d+),(\d+)$");

                var input = Console.ReadLine() ?? string.Empty;

                var matches = regex.Matches(input);
                if (matches.Count > 0)
                {
                    var validLength = int.TryParse(matches[0].Groups[1].Value, out length);
                    var validHeight = int.TryParse(matches[0].Groups[2].Value, out height);
                    var validX = int.TryParse(matches[0].Groups[3].Value, out x);
                    var validY = int.TryParse(matches[0].Groups[4].Value, out y);

                    // Coordinates params should only be in the format length,height,x,y,
                    // length/height values are non-zero, and the rectangle's footprint should fit in the grid
                    validParams = validLength && validHeight && validX && validY && ValidatePlaceRectangleInput(length, height, x, y, grid);
                }

                if (!validParams)
                {
                    _cliService.DisplayError(Messages.InvalidInput);
                }
            }

            var rectangle = new Rectangle(x, y, length, height);
            var isNotOverlapping = ConfirmNotOverlapping(rectangle, grid);
            if (isNotOverlapping)
            {
                grid.Rectangles[rectangle.Guid] = rectangle;
                UpdateRectangleOccupancy(rectangle, grid);
            }
            else
            {
                _cliService.DisplayError($"There is already a rectangle present at {x},{y}, unable to place new rectangle.");
            }
        }

        private bool ValidatePlaceRectangleInput(int length, int height, int x, int y, Grid grid)
        {
            var bottomRight = new Coordinates(x + (length-1), y + (height-1));

            return length > 0 && height > 0 && bottomRight.X < grid.Length && bottomRight.Y < grid.Height;
        }

        private bool ConfirmNotOverlapping(Rectangle rectangle, Grid grid)
        {
            for (var i = rectangle.TopLeft.X; i <= rectangle.BottomRight.X; i++)
            {
                for (var j = rectangle.TopLeft.Y; j <= rectangle.BottomRight.Y; j++)
                {
                    if (grid.Occupancy[j][i] != Guid.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void RemoveRectangle(Grid grid)
        {
            var coordinates = ObtainCoordinatesFromInput(GameAction.RemoveRectangle, grid);

            var rectangleId = GetRectangleId(coordinates, grid);
            if (rectangleId == Guid.Empty)
            {
                _cliService.DisplaySuccess($"Did not find a rectangle at coordinates {coordinates.X},{coordinates.Y}");
            }
            else
            {
                UpdateRectangleOccupancy(grid.Rectangles[rectangleId], grid, clearRectangle: true);
                _cliService.DisplaySuccess($"Removed rectangle at coordinates {coordinates.X},{coordinates.Y}");
            }
        }

        /// <summary>
        /// Given a user's action and the Grid, prompt the user to provide x,y coordinates.
        /// This functionality can be shared amongst several game actions e.g. Find and Remove Rectangle
        /// </summary>
        /// <param name="gameAction"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        private Coordinates ObtainCoordinatesFromInput(string gameAction, Grid grid)
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

        private Guid GetRectangleId(Coordinates coordinates, Grid grid)
        {
            // 2D arrays are referenced by array[row][column]. y represents row, x represents column
            return grid.Occupancy[coordinates.Y][coordinates.X];
        }

        /// <summary>
        /// Given a Rectangle and Grid objects, edit the occupancy of the grid,
        /// in accordance to the Rectangle's footprint on the grid.
        /// Assign the new Rectangle's GUID by default.
        /// If an optional clearRectangle flag is provided, clear the cell to Guid.Empty
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="grid"></param>
        /// <param name="clearRectangle"></param>
        private void UpdateRectangleOccupancy(Rectangle rectangle, Grid grid, bool clearRectangle = false)
        {
            for (var i = rectangle.TopLeft.X; i <= rectangle.BottomRight.X; i++)
            {
                for (var j = rectangle.TopLeft.Y; j <= rectangle.BottomRight.Y; j++)
                {
                    grid.Occupancy[j][i] = clearRectangle ? Guid.Empty : rectangle.Guid;
                }
            }
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
