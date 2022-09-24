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
        private readonly IActionsService _actionsService;
        private Grid? _grid;
        
        public RectanglesGame(ICliService cliService, IActionsService actionsService)
        {
            _cliService = cliService;
            _actionsService = actionsService;
        }

        public void Run()
        {
            var activeGame = true;
            
            // trigger first-time setup of grid
            FirstTimeSetup();

            if (_grid != null)
            {
                // Begin game loop
                while (activeGame)
                {
                    Console.Write(Messages.CommandPrompt);

                    var input = Console.ReadLine() ?? string.Empty;

                    switch (input.ToUpperInvariant())
                    {
                        case GameAction.Menu:
                            _cliService.DisplayMessage(GameAction.Menu);
                            break;
                        case GameAction.PlaceRectangle:
                            _actionsService.PlaceRectangle(_grid);
                            break;
                        case GameAction.FindRectangle:
                            _actionsService.FindRectangle(_grid);
                            break;
                        case GameAction.RemoveRectangle:
                            _actionsService.RemoveRectangle(_grid);
                            break;
                        case GameAction.DisplayGrid:
                            _actionsService.DisplayGrid(_grid);
                            break;
                        case GameAction.CreateGrid:
                            _grid = _actionsService.CreateGrid();
                            break;
                        case GameAction.Exit:
                            _cliService.DisplayMessage(GameAction.Exit);
                            activeGame = false;
                            break;
                        default:
                            _cliService.DisplayMessage(GameAction.Unknown);
                            break;
                    }
                }
            }
            else
            {
                _cliService.DisplayMessage(GameAction.Exception);
            }
        }

        private void FirstTimeSetup()
        {
            _cliService.DisplayMessage(GameAction.Welcome);
            _grid = _actionsService.CreateGrid();
            _cliService.DisplayMessage(GameAction.Menu);
        }
    }
}
