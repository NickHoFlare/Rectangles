namespace Rectangles
{
    public interface IRectanglesService
    {
        void Run();
    }

    public class RectanglesService : IRectanglesService
    {
        public RectanglesService()
        {
        }

        public void Run()
        {
            Console.WriteLine("Hello from RectanglesService");
        }
    }
}
