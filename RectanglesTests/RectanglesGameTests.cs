using NSubstitute;
using Rectangles.Models;
using Rectangles.Services;
using System;
using System.IO;
using Xunit;
using static Rectangles.Constants;

namespace RectanglesTests
{
    public class RectanglesGameTests
    {
        private readonly IRectanglesGame _rectanglesGame;
        private readonly IPromptService _promptService;
        private readonly IActionsService _actionsService;
        private readonly ICliService _cliService;
        private readonly IGameActionStrategyContext _gameActionStrategyContext;
        
        public RectanglesGameTests()
        {
            _promptService = Substitute.For<IPromptService>();
            _actionsService = Substitute.For<IActionsService>();
            _cliService = Substitute.For<ICliService>();
            _gameActionStrategyContext = Substitute.For<IGameActionStrategyContext>();
            _rectanglesGame = new RectanglesGame(_cliService, _promptService, _actionsService, _gameActionStrategyContext);
        }

        [Theory]
        [InlineData("Menu")]
        [InlineData("MENU")]
        [InlineData("menu")]
        [InlineData("mEnU")]
        public void Run_WhenFirstTimeGridCreationSucceedsAndUserRequestsMenu_ShouldInvokeCliServiceDisplayMenu(string input)
        {
            /* Arrange */
            _actionsService.CreateGrid(Arg.Any<Dimensions>()).Returns(new Grid(0,0));
            _gameActionStrategyContext.SetStrategy(Arg.Any<MenuStrategy>()).Execute(Arg.Any<Grid>()).Returns(null);
            var output = new StringWriter();
            Console.SetOut(output);
            var userInput = new StringReader(input + "\r\n" + GameAction.Exit);
            Console.SetIn(userInput);

            /* Act */
            _rectanglesGame.Run();

            /* Assert */
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<MenuStrategy>());
            // cliService receives a call because FirstTimeSetup displays the menu as well
            _cliService.Received(1).DisplayMessage(GameAction.Menu);
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<ExitStrategy>());
        }

        [Theory]
        [InlineData("Place")]
        [InlineData("PLACE")]
        [InlineData("place")]
        [InlineData("pLaCe")]
        public void Run_WhenFirstTimeGridCreationSucceedsAndUserRequestsPlaceRectangle_ShouldInvokeActionServicePlaceRectangle(string input)
        {
            /* Arrange */
            _actionsService.CreateGrid(Arg.Any<Dimensions>()).Returns(new Grid(0, 0));
            var output = new StringWriter();
            Console.SetOut(output);
            var userInput = new StringReader(input + "\r\n" + GameAction.Exit);
            Console.SetIn(userInput);

            /* Act */
            _rectanglesGame.Run();

            /* Assert */
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<PlaceRectangleStrategy>());
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<ExitStrategy>());
        }

        [Theory]
        [InlineData("Find")]
        [InlineData("FIND")]
        [InlineData("find")]
        [InlineData("fInD")]
        public void Run_WhenFirstTimeGridCreationSucceedsAndUserRequestsFindRectangle_ShouldInvokeActionServiceFindRectangle(string input)
        {
            /* Arrange */
            _actionsService.CreateGrid(Arg.Any<Dimensions>()).Returns(new Grid(0, 0));
            var output = new StringWriter();
            Console.SetOut(output);
            var userInput = new StringReader(input + "\r\n" + GameAction.Exit);
            Console.SetIn(userInput);

            /* Act */
            _rectanglesGame.Run();

            /* Assert */
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<FindRectangleStrategy>());
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<ExitStrategy>());
        }

        [Theory]
        [InlineData("Remove")]
        [InlineData("REMOVE")]
        [InlineData("remove")]
        [InlineData("rEmOvE")]
        public void Run_WhenFirstTimeGridCreationSucceedsAndUserRequestsRemoveRectangle_ShouldInvokeActionServiceRemoveRectangle(string input)
        {
            /* Arrange */
            _actionsService.CreateGrid(Arg.Any<Dimensions>()).Returns(new Grid(0, 0));
            var output = new StringWriter();
            Console.SetOut(output);
            var userInput = new StringReader(input + "\r\n" + GameAction.Exit);
            Console.SetIn(userInput);

            /* Act */
            _rectanglesGame.Run();

            /* Assert */
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<RemoveRectangleStrategy>());
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<ExitStrategy>());
        }

        [Theory]
        [InlineData("Display")]
        [InlineData("DISPLAY")]
        [InlineData("display")]
        [InlineData("dIsPlAy")]
        public void Run_WhenFirstTimeGridCreationSucceedsAndUserRequestsDisplayGrid_ShouldInvokeActionServiceDisplayGrid(string input)
        {
            /* Arrange */
            _actionsService.CreateGrid(Arg.Any<Dimensions>()).Returns(new Grid(0, 0));
            var output = new StringWriter();
            Console.SetOut(output);
            var userInput = new StringReader(input + "\r\n" + GameAction.Exit);
            Console.SetIn(userInput);

            /* Act */
            _rectanglesGame.Run();

            /* Assert */
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<DisplayGridStrategy>());
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<ExitStrategy>());
        }

        [Theory]
        [InlineData("List")]
        [InlineData("LIST")]
        [InlineData("list")]
        [InlineData("LiSt")]
        public void Run_WhenFirstTimeGridCreationSucceedsAndUserRequestsListRectangles_ShouldInvokeActionServiceListRectangles(string input)
        {
            /* Arrange */
            _actionsService.CreateGrid(Arg.Any<Dimensions>()).Returns(new Grid(0, 0));
            var output = new StringWriter();
            Console.SetOut(output);
            var userInput = new StringReader(input + "\r\n" + GameAction.Exit);
            Console.SetIn(userInput);

            /* Act */
            _rectanglesGame.Run();

            /* Assert */
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<ListRectanglesStrategy>());
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<ExitStrategy>());
        }

        [Theory]
        [InlineData("Create")]
        [InlineData("CREATE")]
        [InlineData("create")]
        [InlineData("cReAtE")]
        public void Run_WhenFirstTimeGridCreationSucceedsAndUserRequestsCreateGrid_ShouldInvokeActionServiceCreateGrid(string input)
        {
            /* Arrange */
            _actionsService.CreateGrid(Arg.Any<Dimensions>()).Returns(new Grid(5, 5));
            var output = new StringWriter();
            Console.SetOut(output);
            var userInput = new StringReader(input + "\r\n" + GameAction.Exit);
            Console.SetIn(userInput);

            /* Act */
            _rectanglesGame.Run();

            /* Assert */
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<CreateGridStrategy>());
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<ExitStrategy>());
        }

        [Theory]
        [InlineData("")]
        [InlineData("asdf")]
        [InlineData("rectangle")]
        [InlineData("123")]
        public void Run_WhenFirstTimeGridCreationSucceedsAndUserTypesGibberish_ShouldInvokeCliServiceDisplayUnrecognizedInput(string input)
        {
            /* Arrange */
            _actionsService.CreateGrid(Arg.Any<Dimensions>()).Returns(new Grid(0, 0));
            var output = new StringWriter();
            Console.SetOut(output);
            var userInput = new StringReader(input + "\r\n" + GameAction.Exit);
            Console.SetIn(userInput);

            /* Act */
            _rectanglesGame.Run();

            /* Assert */
            _cliService.Received(1).DisplayMessage(GameAction.Unknown);
            _gameActionStrategyContext.Received(1).SetStrategy(Arg.Any<ExitStrategy>());
        }

        [Fact]
        public void Run_WhenFirstTimeSetupGridCreationFails_ShouldInvokeExceptionCliMessage()
        {
            /* Arrange */
            var mockStrategyContext = new GameActionStrategyContext(_cliService);
            _gameActionStrategyContext.SetStrategy(Arg.Any<IGameActionStrategy>()).Returns(mockStrategyContext);
            _gameActionStrategyContext.Execute(Arg.Any<Grid>()).Returns(null);

            _actionsService.CreateGrid(Arg.Any<Dimensions>()).Returns((Grid)null);
            var output = new StringWriter();
            Console.SetOut(output);
            var userInput = new StringReader("Menu");
            Console.SetIn(userInput);

            /* Act */
            _rectanglesGame.Run();

            /* Assert */
            _cliService.Received(1).DisplayError(Arg.Any<string>());
            _cliService.Received(1).DisplayMessage(GameAction.Exception);
        }
    }
}