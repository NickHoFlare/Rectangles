namespace Rectangles.Models
{
    public class Grid
    {
        public Grid(int length, int height)
        {
            Occupancy = InitializeOccupancy(height, length);
            Rectangles = new Dictionary<Guid, Rectangle>();
            Length = length;
            Height = height;
        }

        /// <summary>
        /// Occupancy is represented by a 2D list of size height x length
        /// aka, list representing Occupancy should be of size (height) and each sublist should be of size (length)
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
        /// <param name="height"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private List<List<Guid>> InitializeOccupancy(int height, int length)
        {
            var list = new List<List<Guid>>();

            for (var i = 0; i < height; i++)
            {
                var row = new List<Guid>();
                for (var j = 0; j < length; j++)
                {
                    row.Add(Guid.Empty);
                }
                list.Add(row);
            }

            return list;
        }
    }
}
