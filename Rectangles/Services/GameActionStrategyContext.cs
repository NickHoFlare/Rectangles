using Rectangles.Models;

namespace Rectangles.Services
{
    public interface IGameActionStrategyContext
    {
        IGameActionStrategyContext SetStrategy(IGameActionStrategy strategy);
        object? Execute(Grid? grid);
    }

    public class GameActionStrategyContext : IGameActionStrategyContext
    {
        private IGameActionStrategy _strategy;

        // Take MenuStrategy by default upon construction to avoid null reference exception
        public GameActionStrategyContext(ICliService cliService)
        {
            _strategy = new MenuStrategy(cliService);
        }

        public IGameActionStrategyContext SetStrategy(IGameActionStrategy strategy)
        {
            _strategy = strategy;
            return this;
        }

        public object? Execute(Grid? grid)
        {
            if (grid != null)
            {
                return _strategy.Execute(grid);
            }

            // If grid is null, something has gone wrong!
            throw new ArgumentNullException(nameof(grid));
        }
    }
}
