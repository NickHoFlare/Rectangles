using Rectangles.Models;
using static Rectangles.Constants;

namespace Rectangles.Services
{
    public interface IActionsService
    {
        void DisplayGrid();
        void RemoveRectangle();
        void FindRectangle();
        void PlaceRectangle();
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
            _cliService.DisplayMessage(GameAction.CreateGrid);
            // TODO
            return new Grid();
        }

        public void DisplayGrid()
        {
            _cliService.DisplayMessage(GameAction.DisplayGrid);
            // TODO
        }

        public void FindRectangle()
        {
            _cliService.DisplayMessage(GameAction.FindRectangle);
            // TODO
        }

        public void PlaceRectangle()
        {
            _cliService.DisplayMessage(GameAction.PlaceRectangle);
            // TODO
        }

        public void RemoveRectangle()
        {
            _cliService.DisplayMessage(GameAction.RemoveRectangle);
            // TODO
        }
    }
}
