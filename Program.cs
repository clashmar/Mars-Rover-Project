using MarsRover.UI;
using MarsRover.Enums;
using MarsRover.Input_Layer;
using MarsRover.Logic_Layer;


namespace MarsRover
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserInterface.programStatus = ProgramStatus.USER_INPUT;
            UserInterface.GetUserInput();
            UserInterface.DisplayResult();
        }
    }
}
