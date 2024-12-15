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
            UserInterface.DisplayWelcome();

            UserInterface.programStatus = ProgramStatus.USER_INPUT;
            while (UserInterface.programStatus != ProgramStatus.COMPLETE)
            {
                UserInterface.GetUserInput();
                UserInterface.DisplayResult();
                UserInterface.PlayAgainPrompt();
                MissionControl.ResetMission();
            }
        }
    }
}
