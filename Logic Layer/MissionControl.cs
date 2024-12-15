using MarsRover.Input_Layer;

namespace MarsRover.Logic_Layer
{
    public static class MissionControl
    {
        public static List<Rover> Rovers = [];

        public static List<string> Mission = [];

        public static bool IsCoordinateOccupied(int[] targetCoordinate)
        {
            foreach (Rover rover in MissionControl.Rovers)
            {
                if (rover.Position.XYCoordinates[0] == targetCoordinate[0] && rover.Position.XYCoordinates[1] == targetCoordinate[1]) return true;
            }

            return false;
        }

        public static bool IsCoordinateSafe(int[] targetCoordinates)
        {
            if(Plateau.IsOutOfBounds(targetCoordinates)) return false;
            if (IsCoordinateOccupied(targetCoordinates)) return false;
            return true;
        }
        
        public static List<string> CurrentPositionStrings()
        {
            List<string> positionStrings = [];

            foreach(Rover rover in Rovers)
            {
                positionStrings.Add($"{rover.Position.XYCoordinates[0]} {rover.Position.XYCoordinates[1]} {Enum.GetName(rover.Position.Facing)}");
            }

            return positionStrings;
        }
    }
}
