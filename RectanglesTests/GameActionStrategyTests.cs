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
    public class GameActionStrategyTests
    {
        private readonly ICliService _cliService;
        private readonly IPromptService _promptService;
        private readonly IActionsService _actionsService;

        public GameActionStrategyTests()
        {
            _cliService = Substitute.For<ICliService>();
            _promptService = Substitute.For<IPromptService>();
            _actionsService = Substitute.For<IActionsService>();
        }

        [Fact]
        public void MenuStrategy_WhenExecuted_ShouldDisplayMenu()
        {
            /* Arrange */
            var strategy = new MenuStrategy(_cliService);

            /* Act */
            var result = strategy.Execute(new Grid(0, 0));

            /* Assert */
            result.Should().BeNull();
            _cliService.Received(1).DisplayMessage(GameAction.Menu);
        }

        [Fact]
        public void PlaceRectangleStrategy_WhenRectangleIsNotNull_ShouldDisplaySuccessMessage()
        {
            /* Arrange */
            _promptService.GetRectangle(Arg.Any<Grid>()).Returns(new Rectangle(0, 0, 1, 1));
            _actionsService.PlaceRectangle(Arg.Any<Rectangle>(), Arg.Any<Grid>()).Returns(new Rectangle(0, 0, 1, 1));

            var strategy = new PlaceRectangleStrategy(_cliService, _promptService, _actionsService);

            /* Act */
            var result = strategy.Execute(new Grid(0, 0));

            /* Assert */
            result.Should().BeNull();
            _cliService.Received(1).DisplaySuccess(Arg.Is<string>(str => str.StartsWith("New rectangle")));
        }

        [Fact]
        public void PlaceRectangleStrategy_WhenRectangleIsNull_ShouldDisplayErrorMessage()
        {
            /* Arrange */
            _promptService.GetRectangle(Arg.Any<Grid>()).Returns(new Rectangle(0, 0, 1, 1));
            _actionsService.PlaceRectangle(Arg.Any<Rectangle>(), Arg.Any<Grid>()).Returns((Rectangle)null);

            var strategy = new PlaceRectangleStrategy(_cliService, _promptService, _actionsService);

            /* Act */
            var result = strategy.Execute(new Grid(0, 0));

            /* Assert */
            result.Should().BeNull();
            _cliService.Received(1).DisplayError(Arg.Is<string>(str => str.StartsWith("There is already")));
        }

        [Fact]
        public void FindRectangleStrategy_WhenRectangleIsNotNull_ShouldDisplayRectangleFoundSuccessMessage()
        {
            /* Arrange */
            _promptService.GetCoordinates(GameAction.FindRectangle, Arg.Any<Grid>()).Returns(new Coordinates(0, 0));
            _actionsService.FindRectangle(Arg.Any<Coordinates>(), Arg.Any<Grid>()).Returns(new Rectangle(0, 0, 1, 1));

            var strategy = new FindRectangleStrategy(_cliService, _promptService, _actionsService);

            /* Act */
            var result = strategy.Execute(new Grid(0, 0));

            /* Assert */
            result.Should().BeNull();
            _cliService.Received(1).DisplaySuccess(Arg.Is<string>(str => str.StartsWith("Found a rectangle")));
        }

        [Fact]
        public void FindRectangleStrategy_WhenRectangleIsNull_ShouldDisplayRectangleNotFoundSuccessMessage()
        {
            /* Arrange */
            _promptService.GetCoordinates(GameAction.FindRectangle, Arg.Any<Grid>()).Returns(new Coordinates(0, 0));
            _actionsService.FindRectangle(Arg.Any<Coordinates>(), Arg.Any<Grid>()).Returns((Rectangle)null);

            var strategy = new FindRectangleStrategy(_cliService, _promptService, _actionsService);

            /* Act */
            var result = strategy.Execute(new Grid(0, 0));

            /* Assert */
            result.Should().BeNull();
            _cliService.Received(1).DisplaySuccess(Arg.Is<string>(str => str.StartsWith("Did not find a rectangle")));
        }

        [Fact]
        public void RemoveRectangleStrategy_WhenRectangleIsNotNull_ShouldDisplaySuccessMessage()
        {
            /* Arrange */
            _promptService.GetCoordinates(GameAction.FindRectangle, Arg.Any<Grid>()).Returns(new Coordinates(0, 0));
            _actionsService.RemoveRectangle(Arg.Any<Coordinates>(), Arg.Any<Grid>()).Returns(new Rectangle(0, 0, 1, 1));

            var strategy = new RemoveRectangleStrategy(_cliService, _promptService, _actionsService);

            /* Act */
            var result = strategy.Execute(new Grid(0, 0));

            /* Assert */
            result.Should().BeNull();
            _cliService.Received(1).DisplaySuccess(Arg.Is<string>(str => str.StartsWith("Removed rectangle")));
        }

        [Fact]
        public void RemoveRectangleStrategy_WhenRectangleIsNull_ShouldDisplayErrorMessage()
        {
            /* Arrange */
            _promptService.GetCoordinates(GameAction.FindRectangle, Arg.Any<Grid>()).Returns(new Coordinates(0, 0));
            _actionsService.RemoveRectangle(Arg.Any<Coordinates>(), Arg.Any<Grid>()).Returns((Rectangle)null);

            var strategy = new RemoveRectangleStrategy(_cliService, _promptService, _actionsService);

            /* Act */
            var result = strategy.Execute(new Grid(0, 0));

            /* Assert */
            result.Should().BeNull();
            _cliService.Received(1).DisplayError(Arg.Is<string>(str => str.StartsWith("Did not find a rectangle")));
        }

        [Fact]
        public void DisplayGridStrategy_WhenExecuted_ShouldDisplayGrid()
        {
            /* Arrange */
            var strategy = new DisplayGridStrategy(_actionsService);

            /* Act */
            var result = strategy.Execute(new Grid(0, 0));

            /* Assert */
            result.Should().BeNull();
            _actionsService.Received(1).DisplayGrid(Arg.Any<Grid>());
        }

        [Fact]
        public void ListRectanglesStrategy_WhenExecuted_ShouldListRectangles()
        {
            /* Arrange */
            var strategy = new ListRectanglesStrategy(_actionsService);

            /* Act */
            var result = strategy.Execute(new Grid(0, 0));

            /* Assert */
            result.Should().BeNull();
            _actionsService.Received(1).ListRectangles(Arg.Any<Grid>());
        }

        [Fact]
        public void CreateGridStrategy_WhenExecuted_ShouldCreateGrid()
        {
            /* Arrange */
            _actionsService.CreateGrid(Arg.Any<Dimensions>()).Returns(new Grid(5, 5));
            _promptService.GetCreateGridDimensions().Returns(new Dimensions(5, 5));
            var strategy = new CreateGridStrategy(_cliService, _promptService, _actionsService);

            /* Act */
            var result = strategy.Execute(new Grid(0, 0));

            /* Assert */
            result.Should().NotBeNull();
            _cliService.Received(1).DisplaySuccess(Arg.Is<string>(str => str.StartsWith("New grid created")));
        }

        [Fact]
        public void ExitStrategy_WhenExecuted_ShouldDoNothing()
        {
            /* Arrange */
            var strategy = new ExitStrategy();

            /* Act */
            var result = strategy.Execute(new Grid(0, 0));

            /* Assert */
            result.Should().BeNull();
        }
    }
}