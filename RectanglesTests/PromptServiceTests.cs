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
    public class PromptServiceTests
    {
        private readonly ICliService _cliService;
        private readonly IActionsService _actionsService;
        private readonly IPromptService _promptService;

        public PromptServiceTests()
        {
            _cliService = Substitute.For<ICliService>();
            _actionsService = Substitute.For<IActionsService>();
            _promptService = new PromptService(_cliService, _actionsService);
        }

        [Theory]
        [InlineData("5,5", 5, 5)]
        [InlineData("25,25", 25, 25)]
        [InlineData("6,6", 6, 6)]
        [InlineData("5,25", 5, 25)]
        [InlineData("25,5", 25, 5)]
        public void GetCreateGridDimensions_WhenValidParamsProvided_ShouldReturnValidDimensions(string input, int expectedLength, int expectedHeight)
        {
            /* Arrange */
            var output = new StringWriter();
            Console.SetOut(output);

            var userInput = new StringReader(input);
            Console.SetIn(userInput);

            /* Act */
            var result = _promptService.GetCreateGridDimensions();

            /* Assert */
            result.Length.Should().Be(expectedLength);
            result.Height.Should().Be(expectedHeight);
        }

        [Theory]
        [InlineData("1,4")]
        [InlineData("100,28")]
        [InlineData("3,50")]
        [InlineData("-10,12")]
        [InlineData("0,15")]
        [InlineData("12,0")]
        [InlineData("6,7,8")]
        [InlineData("1")]
        [InlineData("abc,def")]
        [InlineData("10-10")]
        public void GetCreateGridDimensions_WhenInvalidParamsProvided_ShouldDisplayErrorMessage(string input)
        {
            /* Arrange */
            var output = new StringWriter();
            Console.SetOut(output);

            // We follow-up the invalid input with some valid input so that the while loop terminates
            var userInput = new StringReader(input + "\r\n" + "5,5");
            Console.SetIn(userInput);

            /* Act */
            var result = _promptService.GetCreateGridDimensions();

            /* Assert */
            _cliService.Received(1).DisplayError(Messages.InvalidInput);
            result.Length.Should().Be(5);
            result.Height.Should().Be(5);
        }

        [Theory]
        [InlineData("4,4", 4, 4)]
        [InlineData("9,9", 9, 9)]
        [InlineData("6,6", 6, 6)]
        [InlineData("5,9", 5, 9)]
        [InlineData("9,5", 9, 5)]
        public void GetCoordinates_WhenValidParamsProvided_ShouldReturnValidCoordinates(string input, int expectedX, int expectedY)
        {
            /* Arrange */
            var mockGrid = TestHelper.CreateMockGrid();
            var output = new StringWriter();
            Console.SetOut(output);

            var userInput = new StringReader(input);
            Console.SetIn(userInput);

            /* Act */
            var result = _promptService.GetCoordinates(GameAction.FindRectangle, mockGrid);

            /* Assert */
            result.X.Should().Be(expectedX);
            result.Y.Should().Be(expectedY);
        }

        [Theory]
        [InlineData("1,11")]
        [InlineData("12,4")]
        [InlineData("13,23")]
        [InlineData("-1,7")]
        [InlineData("0,15")]
        [InlineData("12,0")]
        [InlineData("6,7,8")]
        [InlineData("1")]
        [InlineData("abc,def")]
        [InlineData("10-10")]
        public void GetCoordinates_WhenInvalidParamsProvided_ShouldDisplayErrorMessage(string input)
        {
            /* Arrange */
            var mockGrid = TestHelper.CreateMockGrid();
            var output = new StringWriter();
            Console.SetOut(output);

            // We follow-up the invalid input with some valid input so that the while loop terminates
            var userInput = new StringReader(input + "\r\n" + "5,5");
            Console.SetIn(userInput);

            /* Act */
            var result = _promptService.GetCoordinates(GameAction.FindRectangle, mockGrid);

            /* Assert */
            _cliService.Received(1).DisplayError(Messages.InvalidInput);
            result.X.Should().Be(5);
            result.Y.Should().Be(5);
        }

        [Theory]
        [InlineData("4,4,4,4", 4, 4, 4, 4, 7, 7)]
        [InlineData("1,1,9,9", 1, 1, 9, 9, 9, 9)]
        [InlineData("6,6,0,0", 6, 6, 0, 0, 5, 5)]
        [InlineData("5,10,4,0", 5, 10, 4, 0, 8, 9)]
        public void GetRectangle_WhenValidParamsProvided_ShouldReturnRectangle(
            string input, int expectedLength, int expectedHeight, int expectedTopX, int expectedTopY, int expectedBotX, int expectedBotY)
        {
            /* Arrange */
            var mockGrid = TestHelper.CreateMockGrid();
            var output = new StringWriter();
            Console.SetOut(output);

            var userInput = new StringReader(input);
            Console.SetIn(userInput);

            /* Act */
            var result = _promptService.GetRectangle(mockGrid);

            /* Assert */
            result.TopLeft.X.Should().Be(expectedTopX);
            result.TopLeft.Y.Should().Be(expectedTopY);
            result.BottomRight.X.Should().Be(expectedBotX);
            result.BottomRight.Y.Should().Be(expectedBotY);
            result.Length.Should().Be(expectedLength);
            result.Height.Should().Be(expectedHeight);
            result.Guid.Should().NotBe(Guid.Empty);
        }

        [Theory]
        [InlineData("4,10,4,4")]
        [InlineData("4,4,4,4,4")]
        [InlineData("1,1,10,10")]
        [InlineData("6,6,-1,0")]
        [InlineData("5,-10,4,0")]
        [InlineData("5")]
        [InlineData("5,2")]
        [InlineData("5,2,4")]
        [InlineData("10+5")]
        public void GetRectangle_WhenInvalidParamsProvided_ShouldDisplayErrorMessage(string input)
        {
            /* Arrange */
            var mockGrid = TestHelper.CreateMockGrid();
            var output = new StringWriter();
            Console.SetOut(output);

            // We follow-up the invalid input with some valid input so that the while loop terminates
            var userInput = new StringReader(input + "\r\n" + "4,4,4,4");
            Console.SetIn(userInput);

            /* Act */
            var result = _promptService.GetRectangle(mockGrid);

            /* Assert */
            _cliService.Received(1).DisplayError(Messages.InvalidInput);
            result.TopLeft.X.Should().Be(4);
            result.TopLeft.Y.Should().Be(4);
        }
    }
}