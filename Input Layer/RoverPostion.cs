using MarsRover.Enums;

namespace MarsRover.Input_Layer
{
    public class RoverPostion(int[] coordinates, CompassDirection facing)
    {
        public int[] XYCoordinates { get; set; } = coordinates;
        public CompassDirection Facing { get; set; } = facing;
    }
}
