using NSubstitute;
using Rectangles.Models;
using Rectangles.Services;
using System;
using System.IO;
using Xunit;
using static Rectangles.Constants;
using FluentAssertions;

namespace RectanglesTests
{
    public class ActionsServiceTests
    {
        private readonly ICliService _cliService;
        private readonly IActionsService _actionsService;

        public ActionsServiceTests()
        {
            _cliService = Substitute.For<ICliService>();
            _actionsService = new ActionsService(_cliService);
        }

        [Theory]
        [InlineData(5,5)]
        [InlineData(25,25)]
        [InlineData(10,15)]
        [InlineData(25,5)]
        [InlineData(5,8)]
        public void CreateGrid_ShouldCreateNewGrid(int length, int height)
        {
            /* Arrange */
            var dimensions = new Dimensions(length, height);

            /* Act */
            var result = _actionsService.CreateGrid(dimensions);

            /* Assert */
            result.Should().NotBeNull();
            result.Length.Should().Be(length);
            result.Height.Should().Be(height);
            result.Occupancy.Count.Should().Be(height);
            result.Occupancy.ForEach(row =>
            {
                row.Count.Should().Be(length);
                row.ForEach(cell => cell.Should().Be(Guid.Empty));
            });
        }

        [Theory]
        [InlineData(2,2)]
        [InlineData(3,3)]
        [InlineData(1,4)]
        [InlineData(5,6)]
        [InlineData(6,5)]
        [InlineData(9,9)]
        public void FindRectangle_WhenRectanglePresent_ShouldFindRectangle(int x, int y)
        {
            /* Arrange */
            var mockGrid = TestHelper.CreateMockGrid();
            var coordinates = new Coordinates(x, y);

            /* Act */
            var result = _actionsService.FindRectangle(coordinates, mockGrid);

            /* Assert */
            result.Should().NotBeNull();
            result.Guid.Should().NotBe(Guid.Empty);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(4, 3)]
        [InlineData(0, 9)]
        [InlineData(5, 7)]
        [InlineData(9, 0)]
        [InlineData(7, 1)]
        public void FindRectangle_WhenRectangleNotPresent_ShouldNotFindRectangle(int x, int y)
        {
            /* Arrange */
            var mockGrid = TestHelper.CreateMockGrid();
            var coordinates = new Coordinates(x, y);

            /* Act */
            var result = _actionsService.FindRectangle(coordinates, mockGrid);

            /* Assert */
            result.Should().BeNull();
        }

        [Theory]
        [InlineData(0, 0, 4, 2)]
        [InlineData(6, 4, 4, 1)]
        [InlineData(0, 7, 5, 2)]
        [InlineData(0, 9, 6, 1)]
        public void PlaceRectangle_WhenNoOverlappingRectangle_ShouldPlaceRectangle(int x, int y, int length, int height)
        {
            /* Arrange */
            var mockGrid = TestHelper.CreateMockGrid();
            var newRectangle = new Rectangle(x, y, length, height);

            /* Act */
            var result = _actionsService.PlaceRectangle(newRectangle, mockGrid);

            /* Assert */
            result.Should().NotBeNull();
            result.Guid.Should().NotBe(Guid.Empty);
            mockGrid.Rectangles.ContainsKey(newRectangle.Guid).Should().BeTrue();
            mockGrid.Occupancy[y][x].Should().Be(result.Guid);
        }

        [Theory]
        [InlineData(0, 0, 4, 3)]
        [InlineData(6, 4, 4, 2)]
        [InlineData(0, 6, 5, 2)]
        [InlineData(0, 9, 7, 1)]
        public void PlaceRectangle_WhenOverlappingRectangle_ShouldNotPlaceRectangle(int x, int y, int length, int height)
        {
            /* Arrange */
            var mockGrid = TestHelper.CreateMockGrid();
            var newRectangle = new Rectangle(x, y, length, height);

            /* Act */
            var result = _actionsService.PlaceRectangle(newRectangle, mockGrid);

            /* Assert */
            result.Should().BeNull();
            mockGrid.Rectangles.ContainsKey(newRectangle.Guid).Should().BeFalse();
            mockGrid.Occupancy[y][x].Should().Be(Guid.Empty);
        }

        [Fact]
        public void ListRectangles_ShouldShowNumberOfRectanglesFound()
        {
            /* Arrange */
            var mockGrid = TestHelper.CreateMockGrid();

            /* Act */
            _actionsService.ListRectangles(mockGrid);

            /* Assert */
            _cliService.Received(1).DisplaySuccess($"{mockGrid.Rectangles.Count} rectangles found");
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(1, 4)]
        [InlineData(5, 6)]
        [InlineData(6, 5)]
        [InlineData(9, 9)]
        public void RemoveRectangle_WhenRectanglePresent_ShouldRemoveRectangle(int x, int y)
        {
            /* Arrange */
            var mockGrid = TestHelper.CreateMockGrid();
            var coordinates = new Coordinates(x, y);

            /* Act */
            var result = _actionsService.RemoveRectangle(coordinates, mockGrid);

            /* Assert */
            result.Should().NotBeNull();
            result.Guid.Should().NotBe(Guid.Empty);
            mockGrid.Rectangles.ContainsKey(result.Guid).Should().BeFalse();
            mockGrid.Occupancy[y][x].Should().Be(Guid.Empty);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(4, 3)]
        [InlineData(0, 9)]
        [InlineData(5, 7)]
        [InlineData(9, 0)]
        [InlineData(7, 1)]
        public void RemoveRectangle_WhenNoRectanglePresent_ShouldNotRemoveRectangle(int x, int y)
        {
            /* Arrange */
            var mockGrid = TestHelper.CreateMockGrid();
            var coordinates = new Coordinates(x, y);

            /* Act */
            var result = _actionsService.RemoveRectangle(coordinates, mockGrid);

            /* Assert */
            result.Should().BeNull();
            mockGrid.Occupancy[y][x].Should().Be(Guid.Empty);
        }
    }
}