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
            
            // Grid is null, trigger first-time setup of grid
            if (_grid == null)
            {
                FirstTimeSetup();
            }

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
                        _actionsService.PlaceRectangle();
                        break;
                    case GameAction.FindRectangle:
                        _actionsService.FindRectangle();
                        break;
                    case GameAction.RemoveRectangle:
                        _actionsService.RemoveRectangle();
                        break;
                    case GameAction.DisplayGrid:
                        _actionsService.DisplayGrid();
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

        private void FirstTimeSetup()
        {
            _cliService.DisplayMessage(GameAction.Welcome);
            _grid = _actionsService.CreateGrid();
        }
    }
}
