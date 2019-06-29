using System.Numerics;

namespace DefaultNamespace
{
    public class Road
    {
        public int RoadLength;
        public Position Position;

        public Road(int roadLength,Position position)
        {
            RoadLength = roadLength;
            Position = position;
        }
    }
}