using Rectangles.Models;
using static Rectangles.Constants;

namespace Rectangles.Services
{
    public interface IActionsService
    {
        Rectangle? RemoveRectangle(Coordinates coordinates, Grid grid);
        Rectangle? FindRectangle(Coordinates coordinates, Grid grid);
        Rectangle? PlaceRectangle(Rectangle rectangle, Grid grid);
        void DisplayGrid(Grid grid);
        void ListRectangles(Grid grid);
        Grid CreateGrid(Dimensions dimensions);
    }

    public class ActionsService : IActionsService
    {
        private readonly ICliService _cliService;

        public ActionsService(ICliService cliService)
        {
            _cliService = cliService;
        }

        public Grid CreateGrid(Dimensions dimensions)
        {
            return new Grid(dimensions.Length, dimensions.Height);
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

        public Rectangle? FindRectangle(Coordinates coordinates, Grid grid)
        {
            // 2D arrays are referenced by array[row][column]. y represents row, x represents column
            var rectangleId = grid.Occupancy[coordinates.Y][coordinates.X];

            if (rectangleId != Guid.Empty)
            {
                return grid.Rectangles[rectangleId];
            }

            return null;
        }

        public Rectangle? PlaceRectangle(Rectangle rectangle, Grid grid)
        {
            if (ConfirmNotOverlapping(rectangle, grid))
            {
                grid.Rectangles[rectangle.Guid] = rectangle;
                UpdateRectangleOccupancy(rectangle, grid);
                return rectangle;
            }
            
            return null;
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

        public void ListRectangles(Grid grid)
        {
            _cliService.DisplaySuccess($"{grid.Rectangles.Count} rectangles found");
            foreach (var rectangle in grid.Rectangles)
            {
                Console.WriteLine($"{rectangle.Key} -> ({rectangle.Value.TopLeft.X},{rectangle.Value.TopLeft.Y})");
            }
        }

        public Rectangle? RemoveRectangle(Coordinates coordinates, Grid grid)
        {
            var rectangle = FindRectangle(coordinates, grid);

            if (rectangle == null)
            {
                return null;
            }
            else
            {
                UpdateRectangleOccupancy(rectangle, grid, clearRectangle: true);
                grid.Rectangles.Remove(rectangle.Guid);
                return rectangle;
            }
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
    }
}
