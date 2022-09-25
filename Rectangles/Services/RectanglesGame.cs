using Rectangles.Models;
using static Rectangles.Constants;

namespace Rectangles.Services
{
    public interface IRectanglesGame
    {
        void Run();
    }

    public class RectanglesGame : IRectanglesGame
    {
        private readonly ICliService _cliService;
        private readonly IPromptService _promptService;
        private readonly IActionsService _actionsService;
        private readonly IGameActionStrategyContext _gameActionStrategyContext;
        private Grid? _grid;
        
        public RectanglesGame(ICliService cliService, IPromptService promptService, IActionsService actionsService, IGameActionStrategyContext gameActionStrategyContext)
        {
            _cliService = cliService;
            _promptService = promptService;
            _actionsService = actionsService;
            _gameActionStrategyContext = gameActionStrategyContext;
        }

        public void Run()
        {
            var activeGame = true;

            try
            {
                // trigger first-time setup of grid
                FirstTimeSetup();
            
                // Begin game loop. grid should be initialized at this point.
                while (activeGame)
                {
                    Console.Write(Messages.CommandPrompt);

                    var input = Console.ReadLine() ?? string.Empty;
                
                    switch (input.ToUpperInvariant())
                    {
                        case GameAction.Menu:
                            _gameActionStrategyContext
                                .SetStrategy(new MenuStrategy(_cliService))
                                .Execute(_grid);
                            break;
                        case GameAction.PlaceRectangle:
                            _gameActionStrategyContext
                                .SetStrategy(new PlaceRectangleStrategy(_cliService, _promptService, _actionsService))
                                .Execute(_grid);
                            break;
                        case GameAction.FindRectangle:
                            _gameActionStrategyContext
                                .SetStrategy(new FindRectangleStrategy(_cliService, _promptService, _actionsService))
                                .Execute(_grid);
                            break;
                        case GameAction.RemoveRectangle:
                            _gameActionStrategyContext
                                .SetStrategy(new RemoveRectangleStrategy(_cliService, _promptService, _actionsService))
                                .Execute(_grid);
                            break;
                        case GameAction.DisplayGrid:
                            _gameActionStrategyContext
                                .SetStrategy(new DisplayGridStrategy(_actionsService))
                                .Execute(_grid);
                            break;
                        case GameAction.ListRectangles:
                            _gameActionStrategyContext
                                .SetStrategy(new ListRectanglesStrategy(_actionsService))
                                .Execute(_grid);
                            break;
                        case GameAction.CreateGrid:
                            _grid = _gameActionStrategyContext
                                .SetStrategy(new CreateGridStrategy(_cliService, _promptService, _actionsService))
                                .Execute(_grid) as Grid;
                            break;
                        case GameAction.Exit:
                            _gameActionStrategyContext
                                .SetStrategy(new ExitStrategy())
                                .Execute(_grid);
                            activeGame = false;
                            break;
                        default:
                            _cliService.DisplayMessage(GameAction.Unknown);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _cliService.DisplayError(ex.Message);
                _cliService.DisplayMessage(GameAction.Exception);
            }
        }

        private void FirstTimeSetup()
        {
            _cliService.DisplayMessage(GameAction.Welcome);
            var dimensions = _promptService.GetCreateGridDimensions();
            _grid = _actionsService.CreateGrid(dimensions);
            _cliService.DisplayMessage(GameAction.Menu);
        }
    }
}
