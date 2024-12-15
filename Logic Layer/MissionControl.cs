using MarsRover.Enums;
using MarsRover.Input_Layer;

namespace MarsRover.Logic_Layer
{
    public static class MissionControl
    {
        public static List<Rover> Rovers = [];

        public static List<IParsed> Mission = [];

        public static void ExecuteMissionList()
        {
            if (Mission[0] is ParsedPlateauSize parsedPlateauSize) Plateau.plateauSize = parsedPlateauSize.PlateauSize;

            for (int i = 1; i < Mission.Count; i++)
            {
                if (i % 2 == 1)
                {
                    if (Mission[i] is ParsedPosition parsedPosition)
                    {
                        if (!MissionControl.IsCoordinateSafe(parsedPosition.Position.XYCoordinates)) { Console.WriteLine($"\nRover {MissionControl.Rovers.Count + 1} could not land!"); return; };
                        Rover newRover = new(parsedPosition.Position);
                    }

                    if (Mission[i+1] is ParsedInstructions parsedInstructions)
                    {
                        foreach (Instruction instruction in parsedInstructions.Instructions)
                        {
                            MissionControl.Rovers.Last().ExecuteInstruction(instruction);
                        }
                    }
                }
            }
        }

        public static void ResetMission()
        {
            Rovers.Clear();
            Mission.Clear();
            Plateau.plateauSize = new(0,0);
        }

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
