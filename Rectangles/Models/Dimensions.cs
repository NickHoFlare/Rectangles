namespace Rectangles.Models
{
    public interface IDimensions
    {
        int Length { get; }
        int Height { get; }
    }

    public class Dimensions : IDimensions
    {
        public Dimensions(int length, int height)
        {
            Length = length;
            Height = height;
        }

        public int Length { get; }
        public int Height { get; }
    }
}
