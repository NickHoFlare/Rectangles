namespace Rectangles.Models
{
    public class Grid
    {
        public Grid(int length, int height)
        {
            Occupancy = InitializeOccupancy(length, height);
            Rectangles = new Dictionary<Guid, Rectangle>();
            Length = length;
            Height = height;
        }

        /// <summary>
        /// Occupancy is represented by a 2D list of size length x height
        /// aka, list representing Occupancy should be of size (length) and each sublist should be of size (height)
        /// </summary>
        public List<List<Guid>> Occupancy { get; set; }
        public Dictionary<Guid, Rectangle> Rectangles { get; set; }
        public int Length { get; }
        public int Height { get; }

        /// <summary>
        /// The occupancy 2D list needs to be initialized upon Grid creation
        /// so as to ensure that a value exists for each "cell" in the grid, 
        /// and is ready to be referenced.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private List<List<Guid>> InitializeOccupancy(int length, int height)
        {
            var list = new List<List<Guid>>();

            for (var i = 0; i < length; i++)
            {
                var column = new List<Guid>();
                for (var j = 0; j < height; j++)
                {
                    column.Add(Guid.Empty);
                }
                list.Add(column);
            }

            return list;
        }
    }
}
