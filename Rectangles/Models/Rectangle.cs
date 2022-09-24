namespace Rectangles.Models
{
    public class Rectangle
    {
        public Rectangle(int x, int y, int length, int height)
        {
            TopLeft = new Coordinates(x, y);
            BottomRight = InitializeBottomRight(x, y, length, height);
            Length = length;
            Height = height;
            Guid = Guid.NewGuid();
        }

        public Coordinates TopLeft { get; }
        public Coordinates BottomRight { get; }
        public int Length { get; }
        public int Height { get; }
        public Guid Guid { get; }

        private Coordinates InitializeBottomRight(int x, int y, int length, int height)
        {
            return new Coordinates(x + (length - 1), y + (height - 1));
        }
    }
}
