namespace Rectangles.Models
{
    public interface ICoordinates
    {
        int X { get; }
        int Y { get; }
    }

    public class Coordinates : ICoordinates
    {
        public Coordinates(int x, int y)
        {
            X = x; 
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }
}
