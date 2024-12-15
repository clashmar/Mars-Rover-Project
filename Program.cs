using MarsRover.UI;
using MarsRover.Enums;

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
            }
        }
    }
}
