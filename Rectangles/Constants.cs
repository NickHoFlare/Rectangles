namespace Rectangles
{
    public static class Constants
    {
        public static class Messages 
        {
            public const string Welcome = 
@"+-----------------------+
| Welcome to RECTANGLES |
+-----------------------+";
            public const string Menu =
@"Please pick an option:
PLACE a rectangle
FIND a rectangle
REMOVE a rectangle
DISPLAY the grid
LIST all rectangles
CREATE a new grid (reset)
show this MENU

EXIT rectangles";
            public const string FirstGridCreationInstruction = "First of all, let's create a grid.";
            public const string GridCreationInstruction = @"Please provide a length and height in the format ""length,height""";
            public const string AdditionalGridCreationInstruction = "A grid must have a width and height of no less than 5 and no greater than 25";
            public const string ExampleGridInput = "Example: 10,12";
            public const string RectangleCreationInstruction = @"Please provide a length, height, and x/y coordinates (top left corner of rectangle) in the format ""length,height,x,y""";
            public const string AdditionalRectangleCreationInstruction = "Rectangles must not overlap, nor extend beyond the edge of the grid";
            public const string ExampleRectangleInput = "Example: 2,3,4,4";
            public const string CoordinatesInstruction = @"Please provide 0-indexed x and y coordinates in the format ""x,y""";
            public const string ExampleCoordinatesInput = "Example: 6,10";
            public const string CommandPrompt = "> ";
            public const string UnrecognizedInput = "Input not recognized. Please try again";
            public const string InvalidInput = "Invalid input. Please try again";
            public const string Exception = "Something went wrong. Press any key to exit";
        }

        public static class GameAction
        {
            public const string Welcome = "WELCOME";
            public const string Menu = "MENU";
            public const string PlaceRectangle = "PLACE";
            public const string FindRectangle = "FIND";
            public const string RemoveRectangle = "REMOVE";
            public const string DisplayGrid = "DISPLAY";
            public const string ListRectangles = "LIST";
            public const string CreateGrid = "CREATE";
            public const string Exit = "EXIT";
            public const string Unknown = "UNKNOWN";
            public const string Exception = "EXCEPTION";
        }

        public static class Ascii
        {
            // <summary>
            /// ASCII 35 translates to '#'. This represents an empty cell in the grid.
            /// </summary>
            public const string Empty = "#";

            /// <summary>
            /// ASCII 36 translates to '&'. This shall represent the 'first' rectangle. 
            /// Subsequent characters shall represent further unique rectangles.
            /// </summary>
            public const int First = 36;
        }
    }
}
