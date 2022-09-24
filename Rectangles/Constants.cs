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
1: PLACE a rectangle
2: FIND a rectangle
3: REMOVE a rectangle
4: DISPLAY the grid
5: CREATE a new grid (reset)";
            public const string CoordinatesInstruction = @"Please provide 0-indexed x and y coordinates in the format ""x,y""";
            public const string EntityCreationInstruction = @"Please provide a length and height in the format ""length,height""";
            public const string FirstGridCreationInstruction = "First of all, let's create a grid.";
            public const string AdditionalGridCreationInstruction = "A grid must have a width and height of no less than 5 and no greater than 25";
            public const string ExampleCoordinates = "Example: 6,10";
            public const string CommandPrompt = "> ";
        }

        public static class GameAction
        {
            public const string Welcome = "WELCOME";
            public const string Menu = "MENU";
            public const string PlaceRectangle = "PLACE";
            public const string FindRectangle = "FIND";
            public const string RemoveRectangle = "REMOVE";
            public const string DisplayGrid = "DISPLAY";
            public const string CreateGrid = "CREATE";
            public const string Exit = "EXIT";
            public const string Unknown = "UNKNOWN";
        }
    }
}
