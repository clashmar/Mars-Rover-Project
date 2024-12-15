using MarsRover.UI;
using MarsRover.Enums;
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
                MissionControl.ExecuteMissionList();
                UserInterface.DisplayResult();
                UserInterface.PlayAgainPrompt();
            }
        }
    }
}
