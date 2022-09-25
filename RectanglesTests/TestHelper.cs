using Rectangles.Models;
using System;

namespace RectanglesTests
{
    public static class TestHelper
    {
        /// <summary>
        /// ##########
        /// ##########
        /// ##11######
        /// ##11######
        /// #22222####
        /// #222223333
        /// #222223333
        /// ######3333
        /// ######3333
        /// ######3333
        /// </summary>
        /// <returns></returns>
        public static Grid CreateMockGrid()
        {
            var grid = new Grid(10, 10);
            var rectangle1 = new Rectangle(2, 2, 2, 2);
            var rectangle2 = new Rectangle(1, 4, 5, 3);
            var rectangle3 = new Rectangle(6, 5, 4, 5);
            grid.Rectangles[rectangle1.Guid] = rectangle1;
            grid.Rectangles[rectangle2.Guid] = rectangle2;
            grid.Rectangles[rectangle3.Guid] = rectangle3;

            grid = UpdateRectangleOccupancy(rectangle1, grid);
            grid = UpdateRectangleOccupancy(rectangle2, grid);
            grid = UpdateRectangleOccupancy(rectangle3, grid);

            return grid;
        }

        public static Grid UpdateRectangleOccupancy(Rectangle rectangle, Grid grid, bool clearRectangle = false)
        {
            for (var i = rectangle.TopLeft.X; i <= rectangle.BottomRight.X; i++)
            {
                for (var j = rectangle.TopLeft.Y; j <= rectangle.BottomRight.Y; j++)
                {
                    grid.Occupancy[j][i] = clearRectangle ? Guid.Empty : rectangle.Guid;
                }
            }
            return grid;
        }
    }
}
