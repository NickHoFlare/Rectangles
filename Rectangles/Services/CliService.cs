using static Rectangles.Constants;

namespace Rectangles.Services
{
    public interface ICliService
    {
        void DisplayMessage(string action);
    }

    public class CliService : ICliService
    {
        public void DisplayMessage(string action)
        {
            switch (action)
            {
                case GameAction.Welcome:
                    Console.WriteLine(Messages.Welcome);
                    Console.WriteLine(Messages.FirstGridCreationInstruction);
                    break;
                case GameAction.Menu:
                    Console.WriteLine(Messages.Menu);
                    break;
                case GameAction.PlaceRectangle:
                    Console.WriteLine(Messages.EntityCreationInstruction);
                    break;
                case GameAction.FindRectangle:
                case GameAction.RemoveRectangle:
                    Console.WriteLine(Messages.CoordinatesInstruction);
                    break;
                case GameAction.DisplayGrid:
                    Console.WriteLine("TODO: Display grid!");
                    break;
                case GameAction.CreateGrid:
                    Console.WriteLine(Messages.EntityCreationInstruction);
                    Console.WriteLine(Messages.AdditionalGridCreationInstruction);
                    break;
                default:
                    Console.WriteLine("Input not recognized. Please try again.");
                    break;
            }
        }
    }
}
