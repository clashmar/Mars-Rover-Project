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

        //ExectuteInstructionMethod

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
            int[] targetCoordinate = Position.Facing switch
            {
                CompassDirection.N => targetCoordinate = [Position.X, Position.Y + 1],
                CompassDirection.E => targetCoordinate = [Position.X + 1, Position.Y],
                CompassDirection.S => targetCoordinate = [Position.X, Position.Y - 1],
                CompassDirection.W => targetCoordinate = [Position.X - 1, Position.Y],
                _ => targetCoordinate = [Position.X, Position.Y]
            };

            if(IsCoordinateOccupied(targetCoordinate)) { IsObstructed = true; return; }
            if(Plateau.IsOutOfBounds(targetCoordinate)) { IsObstructed= true; return; }

            Position.X = targetCoordinate[0];
            Position.Y = targetCoordinate[1];
        }

        public bool IsCoordinateOccupied(int[] targetCoordinate)
        {
            foreach(Rover rover in MissionControl.Rovers)
            {
                if(rover.Position.X == targetCoordinate[0] && rover.Position.Y == targetCoordinate[1]) return true;
            }

            return false;
        }
    }
}
