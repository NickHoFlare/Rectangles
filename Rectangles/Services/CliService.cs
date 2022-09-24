using static Rectangles.Constants;

namespace Rectangles.Services
{
    public interface ICliService
    {
        /// <summary>
        /// This is used when displaying canned messages to welcome the user, and in response to user input
        /// </summary>
        /// <param name="action"></param>
        void DisplayMessage(string action);

        /// <summary>
        /// This is used to make "error" messages more prominent to the user
        /// </summary>
        /// <param name="message"></param>
        void DisplayError(string message);

        /// <summary>
        /// This is used to make "success" messages more prominent to the user
        /// </summary>
        /// <param name="message"></param>
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
            // TODO: Use Strategy pattern
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
                case GameAction.CreateGrid:
                    Console.WriteLine(Messages.GridCreationInstruction);
                    Console.WriteLine(Messages.AdditionalGridCreationInstruction);
                    Console.WriteLine(Messages.ExampleGridInput);
                    break;
                default:
                    Console.WriteLine(Messages.UnrecognizedInput);
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
