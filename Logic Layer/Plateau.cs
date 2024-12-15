using MarsRover.Input_Layer;

namespace MarsRover.Logic_Layer
{
    public static class Plateau
    {
        public static PlateauSize plateauSize = new(0, 0);
        public static bool IsOutOfBounds(int[] targetCoordinate)
        {
            if (targetCoordinate[0] >= plateauSize.X || targetCoordinate[1] >= plateauSize.Y) return true;
            if (targetCoordinate[0] < 0 || targetCoordinate[1] < 0) return true;

            return false;
        }
    }
}
