using MarsRover.Enums;
using MarsRover.Input_Layer;

namespace MarsRover.Logic_Layer
{
    public static class MissionControl
    {
        public static List<Rover> Rovers = [];

        public static List<string> Mission = [];

        public static void ExecuteMissionList(List<string> input)
        {
            ParsedPlateauSize parsedPlateauSize = new(input[0]);
            if (!parsedPlateauSize.IsValid) { Console.WriteLine("Not a valid plateau size."); return; }
            Plateau.plateauSize = parsedPlateauSize.PlateauSize;

            for (int i = 1; i < input.Count; i++)
            {
                if (i % 2 == 1)
                {
                    ParsedPosition parsedPosition = new(input[i]);
                    if (!parsedPosition.IsValid) { Console.WriteLine("This position is not valid."); return; }
                    if (!MissionControl.IsCoordinateSafe(parsedPosition.Position.XYCoordinates)) { Console.WriteLine($"\nRover {MissionControl.Rovers.Count + 1} could not land!"); return; };
                    Rover newRover = new(parsedPosition.Position);

                    ParsedInstructions parsedInstructions = new(input[i + 1]);
                    if (!parsedInstructions.IsValid) { Console.WriteLine("These instructions are invalid."); return; }
                    foreach (Instruction instruction in parsedInstructions.Instructions)
                    {
                        newRover.ExecuteInstruction(instruction);
                    }
                }
            }
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
