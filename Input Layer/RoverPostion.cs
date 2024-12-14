using MarsRover.Enums;

namespace MarsRover.Input_Layer
{
    public class RoverPostion(int x, int y, CompassDirection facing)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public CompassDirection Facing { get; set; } = facing;
    }
}
