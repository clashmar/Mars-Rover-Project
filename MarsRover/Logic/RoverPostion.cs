using MarsRover.Enums;

namespace MarsRover.Logic
{
    public class RoverPostion(int[] coordinates, CompassDirection facing)
    {
        public int[] XYCoordinates { get; set; } = coordinates;
        public CompassDirection Facing { get; set; } = facing;
    }
}
