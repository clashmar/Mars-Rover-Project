using MarsRover.Enums;
using MarsRover.Input_Layer;

namespace MarsRover.Logic_Layer
{
    public class Rover
    {
        public RoverPostion Position { get; set;}
        
        public bool IsObstructed = false;

        public Rover(RoverPostion position)
        {
            Position = position;
            MissionControl.Rovers.Add(this);
        }

        public void ExecuteInstruction(Instruction instruction)
        {
            if (instruction == Instruction.M) MoveRover();
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
            int[] targetCoordinate = GetTargetCoordinate();

            if(Plateau.IsOutOfBounds(targetCoordinate)) { IsObstructed= true; return; }
            if(MissionControl.IsCoordinateOccupied(targetCoordinate)) { IsObstructed = true; return; }

            IsObstructed = false;
            Position.X = targetCoordinate[0];
            Position.Y = targetCoordinate[1];
        }

        public int[] GetTargetCoordinate()
        {
            return Position.Facing switch
            {
                CompassDirection.N => [Position.X, Position.Y + 1],
                CompassDirection.E => [Position.X + 1, Position.Y],
                CompassDirection.S => [Position.X, Position.Y - 1],
                CompassDirection.W => [Position.X - 1, Position.Y],
                _ => [Position.X, Position.Y]
            };
        }
    }
}
