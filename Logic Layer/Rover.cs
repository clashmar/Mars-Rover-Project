using MarsRover.Enums;
using MarsRover.Input_Layer;

namespace MarsRover.Logic_Layer
{
    public class Rover
    {
        public RoverPostion Position { get; set;}
        
        public bool IsObstructed = false;

        public int Name;

        public int Honker = 0;

        public Rover(RoverPostion position)
        {
            Position = position;
            MissionControl.Rovers.Add(this);
            Name = MissionControl.Rovers.Count;
        }

        public void ExecuteInstruction(Instruction instruction)
        {
            if (instruction == Instruction.M) MoveRover();
            else if(instruction == Instruction.H) Honker += 1;
            else RotateRover(instruction);
        }

        public void RotateRover(Instruction instruction)
        {
            if(instruction == Instruction.R)
            {
                Position.Facing = Position.Facing switch
                {
                    CompassDirection.N => CompassDirection.E,
                    CompassDirection.E => CompassDirection.S,
                    CompassDirection.S => CompassDirection.W,
                    CompassDirection.W => CompassDirection.N,
                    _ => Position.Facing
                };
            }
            else if(instruction == Instruction.L) 
            {
                Position.Facing = Position.Facing switch
                {
                    CompassDirection.N => CompassDirection.W,
                    CompassDirection.E => CompassDirection.N,
                    CompassDirection.S => CompassDirection.E,
                    CompassDirection.W => CompassDirection.S,
                    _ => Position.Facing
                };
            } 
            else
            {
                throw new ArgumentException("RotateRover method only accepts rotate instructions.");
            }
        }

        public void MoveRover()
        {
            int[] targetCoordinates = GetTargetCoordinates();

            if(!MissionControl.IsCoordinateSafe(targetCoordinates)) { IsObstructed = true; return; }

            MissionControl.RouteCoordinates.Add(Position.XYCoordinates);

            IsObstructed = false;
            Position.XYCoordinates = targetCoordinates;
        }

        public int[] GetTargetCoordinates()
        {
            return Position.Facing switch
            {
                CompassDirection.N => [Position.XYCoordinates[0], Position.XYCoordinates[1] + 1],
                CompassDirection.E => [Position.XYCoordinates[0] + 1, Position.XYCoordinates[1]],
                CompassDirection.S => [Position.XYCoordinates[0], Position.XYCoordinates[1] - 1],
                CompassDirection.W => [Position.XYCoordinates[0] - 1, Position.XYCoordinates[1]],
                _ => [Position.XYCoordinates[0], Position.XYCoordinates[1]]
            };
        }

        public string Honk()
        {
            string honk = "";

            for (int i = 0; i < Honker; i ++)
            {
                honk += "Honk! ";
            }

            return honk;
        }
    }
}
