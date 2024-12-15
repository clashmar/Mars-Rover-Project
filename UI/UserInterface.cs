using MarsRover.Enums;
using MarsRover.Input_Layer;
using MarsRover.Logic_Layer;
namespace MarsRover.UI
{
    public static class UserInterface
    {
        public static ProgramStatus programStatus = ProgramStatus.PROCESSING;

        public static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to the (state of the art) Mars Rover Simulator 1996.\n");

            Console.WriteLine("Please enter the size of the plateau on Mars you will be navigating in the form 'X Y', " +
                "where X and Y are the maximum coordinates of a rectangular grid:");
        }

        public static void GetUserInput()
        {
            while (programStatus == ProgramStatus.USER_INPUT)
            {
                while (true)
                {
                    string? inputPlateauString = Console.ReadLine();
                    if (String.IsNullOrEmpty(inputPlateauString)) { Console.WriteLine("Please enter some values."); continue; }

                    ParsedPlateauSize parsedPlateauSize = new(inputPlateauString);

                    if (!parsedPlateauSize.IsValid) { Console.WriteLine("Not a valid plateau size."); continue; }
                    MissionControl.Mission.Add(parsedPlateauSize); break;
                }

                ConsoleKey? yesNoInput = null;

                while (yesNoInput != ConsoleKey.N)
                {
                    yesNoInput = null;

                    Console.WriteLine("\nPlease enter the starting position of the new Rover in the form 'X Y P', where X and Y" +
                        " are its starting coordinates and P is the first letter of the compass direction it faces to begin with:");
                    while (true)
                    {
                        string? inputPositionString = Console.ReadLine();
                        if (String.IsNullOrEmpty(inputPositionString)) { Console.WriteLine("Please enter some values."); continue; }
                        ParsedPosition parsedPosition = new(inputPositionString);
                        if (!parsedPosition.IsValid) { Console.WriteLine("Not a valid starting position"); continue; }
                        MissionControl.Mission.Add(parsedPosition); break;
                    }

                    Console.WriteLine("\nPlease enter the list of instructions for the new Rover to follow in the form an unbroken string of letters." +
                        "L = Rotate 90 degrees Left, R = Rotate 90 degrees Right, M = Moves the Rover forward by one grid point.");
                    while (true)
                    {
                        string? inputInstructionString = Console.ReadLine();
                        if (String.IsNullOrEmpty(inputInstructionString)) { Console.WriteLine("Please enter some values."); continue; }
                        ParsedInstructions parsedInstructions = new(inputInstructionString);
                        if (!parsedInstructions.IsValid) { Console.WriteLine("These instructions are invalid"); continue; }
                        MissionControl.Mission.Add(parsedInstructions); break;
                    }

                    Console.WriteLine("\nDo you want to add another Rover? (Y/N)");
                    yesNoInput = YesOrNo(yesNoInput);
                }

                programStatus = ProgramStatus.PROCESSING;
            }
        }

        public static void DisplayResult()
        {
            MissionControl.ExecuteMissionList();
            Console.WriteLine("\nThese are the final positions of the Rovers:\n");

            foreach (string position in MissionControl.CurrentPositionStrings())
            {
                Console.WriteLine(position);
            }
        }

        public static void PlayAgainPrompt()
        {
            ConsoleKey? yesNoInput = null;

            Console.WriteLine("\nDo you want to run the simulation again? (Y/N)");
            yesNoInput = YesOrNo(yesNoInput);

            if(yesNoInput == ConsoleKey.N) programStatus = ProgramStatus.COMPLETE;
            if (yesNoInput == ConsoleKey.Y) programStatus = ProgramStatus.USER_INPUT;
        }

        private static ConsoleKey? YesOrNo(ConsoleKey? yesNoInput)
        {
            while (yesNoInput != ConsoleKey.Y && yesNoInput != ConsoleKey.N)
            {
                yesNoInput = Console.ReadKey(true).Key;
                if (yesNoInput != ConsoleKey.Y && yesNoInput != ConsoleKey.N) Console.WriteLine("Input Invalid.");
            }
            return yesNoInput;
        }
    }
}
