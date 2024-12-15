using MarsRover.Input_Layer;

namespace MarsRover.Logic_Layer
{
    public static class MissionControl
    {
        public static List<Rover> Rovers = new List<Rover>();

        public static bool IsCoordinateOccupied(int[] targetCoordinate)
        {
            foreach (Rover rover in MissionControl.Rovers)
            {
                if (rover.Position.X == targetCoordinate[0] && rover.Position.Y == targetCoordinate[1]) return true;
            }

            return false;
        }

        public static bool IsSafeToLand(RoverPostion postion)
        {
            int[] landingCoordinate = [postion.X, postion.Y];
            if (IsCoordinateOccupied(landingCoordinate)) return false;
            if(Plateau.IsOutOfBounds(landingCoordinate)) return false;
            return true;
        }
    }
}
