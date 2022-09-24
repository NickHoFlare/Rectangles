namespace Rectangles.Models
{
    public class Rectangle
    {
        public Rectangle(int x, int y, int length, int height)
        {
            TopLeft = new Coordinates(x, y);
            Length = length;
            Height = height;
            Guid = Guid.NewGuid();
        }

        public Coordinates TopLeft { get; }
        public int Length { get; }
        public int Height { get; }
        public Guid Guid { get; }
    }
}
