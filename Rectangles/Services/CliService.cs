using static Rectangles.Constants;

namespace Rectangles.Services
{
    public interface ICliService
    {
        void DisplayMessage(string action);
        void DisplayError(string message);
        void DisplaySuccess(string message);
    }

    /// <summary>
    /// This is a wrapper of Console.WriteLine, which decorates the usual console messages with informative symbology
    /// for emphasis or prominence
    /// </summary>
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
                    Console.WriteLine(Messages.RectangleCreationInstruction);
                    Console.WriteLine(Messages.AdditionalRectangleCreationInstruction);
                    Console.WriteLine(Messages.ExampleRectangleInput);
                    break;
                case GameAction.FindRectangle:
                case GameAction.RemoveRectangle:
                    Console.WriteLine(Messages.CoordinatesInstruction);
                    Console.WriteLine(Messages.ExampleCoordinatesInput);
                    break;
                case GameAction.DisplayGrid:
                    Console.WriteLine("TODO: Display grid!");
                    break;
                case GameAction.CreateGrid:
                    Console.WriteLine(Messages.GridCreationInstruction);
                    Console.WriteLine(Messages.AdditionalGridCreationInstruction);
                    Console.WriteLine(Messages.ExampleGridInput);
                    break;
                default:
                    Console.WriteLine("Input not recognized. Please try again.");
                    break;
            }
        }

        public void DisplayError(string message)
        {
            Console.WriteLine($"XXX {message} XXX");
        }

        public void DisplaySuccess(string message)
        {
            Console.WriteLine($"!!! {message} !!!");
        }
    }
}
