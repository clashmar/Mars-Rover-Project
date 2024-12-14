using MarsRover.Enums;
using MarsRover.Input_Layer;

namespace MarsRover.Logic_Layer
{
    public class Rover(RoverPostion position)
    {
        public RoverPostion Postion { get; set; } = position;

        //ExectuteInstructionMethod

        public void RotateRover(Instruction instruction)
        {
            if(instruction == Instruction.R)
            {
                Postion.Facing = Postion.Facing switch
                {
                    CompassDirection.N => CompassDirection.E,
                    CompassDirection.E => CompassDirection.S,
                    CompassDirection.S => CompassDirection.W,
                    CompassDirection.W => CompassDirection.N,
                    _ => Postion.Facing
                };
            }
            else if(instruction == Instruction.L) 
            {
                Postion.Facing = Postion.Facing switch
                {
                    CompassDirection.N => CompassDirection.W,
                    CompassDirection.E => CompassDirection.N,
                    CompassDirection.S => CompassDirection.E,
                    CompassDirection.W => CompassDirection.S,
                    _ => Postion.Facing
                };
            } 
            else
            {
                throw new ArgumentException("RotateRover method only accepts rotate instructions.");
            }
        }

        public void MoveRover(Instruction instruction)
        {

        }
    }
}
