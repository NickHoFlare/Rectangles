using Rectangles.Models;
using static Rectangles.Constants;

namespace Rectangles.Services
{
    public interface IGameActionStrategy
    {
        object? Execute(Grid grid);
    }

    public class MenuStrategy : IGameActionStrategy
    {
        private readonly ICliService _cliService;

        public MenuStrategy(ICliService cliService)
        {
            _cliService = cliService;
        }

        public object? Execute(Grid grid)
        {
            _cliService.DisplayMessage(GameAction.Menu);
            return null;
        }
    }

    public class PlaceRectangleStrategy : IGameActionStrategy
    {
        private readonly ICliService _cliService;
        private readonly IPromptService _promptService;
        private readonly IActionsService _actionsService;

        public PlaceRectangleStrategy(ICliService cliService, IPromptService promptService, IActionsService actionsService)
        {
            _cliService = cliService;
            _promptService = promptService;
            _actionsService = actionsService;
        }

        public object? Execute(Grid grid)
        {
            var rectangle = _promptService.GetRectangle(grid);
            rectangle = _actionsService.PlaceRectangle(rectangle, grid);

            if (rectangle != null)
            {
                _cliService.DisplaySuccess($"New rectangle {rectangle.Guid} placed at {rectangle.TopLeft.X},{rectangle.TopLeft.Y}");
            }
            else
            {
                _cliService.DisplayError("There is already a rectangle within the desired rectangle footprint, unable to place new rectangle.");
            }
            return null;
        }
    }

    public class FindRectangleStrategy : IGameActionStrategy
    {
        private readonly ICliService _cliService;
        private readonly IPromptService _promptService;
        private readonly IActionsService _actionsService;

        public FindRectangleStrategy(ICliService cliService, IPromptService promptService, IActionsService actionsService)
        {
            _cliService = cliService;
            _promptService = promptService;
            _actionsService = actionsService;
        }

        public object? Execute(Grid grid)
        {
            var coordinates = _promptService.GetCoordinates(GameAction.FindRectangle, grid);
            var rectangle = _actionsService.FindRectangle(coordinates, grid);

            if (rectangle == null)
            {
                _cliService.DisplaySuccess($"Did not find a rectangle at coordinates {coordinates.X},{coordinates.Y}");
            }
            else
            {
                _cliService.DisplaySuccess($"Found a rectangle {rectangle.Guid} at coordinates {coordinates.X},{coordinates.Y}!");
            }
            return null;
        }
    }

    public class RemoveRectangleStrategy : IGameActionStrategy
    {
        private readonly ICliService _cliService;
        private readonly IPromptService _promptService;
        private readonly IActionsService _actionsService;

        public RemoveRectangleStrategy(ICliService cliService, IPromptService promptService, IActionsService actionsService)
        {
            _cliService = cliService;
            _promptService = promptService;
            _actionsService = actionsService;
        }

        public object? Execute(Grid grid)
        {
            var coordinates = _promptService.GetCoordinates(GameAction.RemoveRectangle, grid);
            var removedRectangle = _actionsService.RemoveRectangle(coordinates, grid);

            if (removedRectangle == null)
            {
                _cliService.DisplayError($"Did not find a rectangle at the given coordinates");
            }
            else
            {
                _cliService.DisplaySuccess($"Removed rectangle {removedRectangle.Guid}");
            }
            return null;
        }
    }

    public class DisplayGridStrategy : IGameActionStrategy
    {
        private readonly IActionsService _actionsService;

        public DisplayGridStrategy(IActionsService actionsService)
        {
            _actionsService = actionsService;
        }

        public object? Execute(Grid grid)
        {
            _actionsService.DisplayGrid(grid);
            return null;
        }
    }

    public class ListRectanglesStrategy : IGameActionStrategy
    {
        private readonly IActionsService _actionsService;

        public ListRectanglesStrategy(IActionsService actionsService)
        {
            _actionsService = actionsService;
        }

        public object? Execute(Grid grid)
        {
            _actionsService.ListRectangles(grid);
            return null;
        }
    }

    public class CreateGridStrategy : IGameActionStrategy
    {
        private readonly ICliService _cliService;
        private readonly IPromptService _promptService;
        private readonly IActionsService _actionsService;

        public CreateGridStrategy(ICliService cliService, IPromptService promptService, IActionsService actionsService)
        {
            _cliService = cliService;
            _promptService = promptService;
            _actionsService = actionsService;
        }

        public object Execute(Grid grid)
        {
            var dimensions = _promptService.GetCreateGridDimensions();
            var newGrid = _actionsService.CreateGrid(dimensions);
            _cliService.DisplaySuccess($"New grid created with length of {dimensions.Length} and height of {dimensions.Height}");
            return newGrid;
        }
    }

    public class ExitStrategy : IGameActionStrategy
    {
        public object? Execute(Grid grid)
        {
            return null;
        }
    }
}
