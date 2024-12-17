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
            Console.WriteLine("Welcome to the (state of the art) Mars Rover Simulator 1996. Please read the training manual");
            Console.WriteLine("thoroughly before beginning the simulation.\n");

            Console.WriteLine("Your mission will take place on the most exciting of all conceptual spaces... A regular grid!");
            Console.WriteLine("When choosing the size of the plateau, please enter its maximum coordinates in the form (X, Y),");
            Console.WriteLine("where (9, 9) is the maximum value and (1, 1) is the minimum (and quite frankly an awful choice).\n");

            Console.WriteLine("Next, select the starting position of a new Rover in the form (X, Y, D), where 'X' and 'Y' are its ");
            Console.WriteLine("starting coordinates and 'D' is the first letter of the compass direction it faces to begin with.\n");

            Console.WriteLine("Please then enter the list of instructions for the new Rover to follow in the form an unbroken");
            Console.WriteLine("string of letters. For example (LLMRM):\n");
            Console.WriteLine("L = Rotate 90 degrees Left.");
            Console.WriteLine("R = Rotate 90 degrees Right.");
            Console.WriteLine("M = Moves the Rover forward by one grid point in the facing direction.\n");

            Console.WriteLine("Are you ready to begin the mission? (Y/N)");
            ConsoleKey? yesNoInput = null;
            int counter = 0;
            while (true)
            {
                yesNoInput = YesOrNo(yesNoInput);
                if (yesNoInput == ConsoleKey.Y) break;
                if (counter == notReady.Count()) programStatus = ProgramStatus.COMPLETE;
                Console.WriteLine(notReady[counter]);
                counter++;
                yesNoInput = null;
            }
        }

        public static void GetUserInput()
        {
            while (programStatus == ProgramStatus.USER_INPUT)
            {
                GetPlateauInput();

                GetRoverInputs();

                programStatus = ProgramStatus.PROCESSING;
            }
        }
         
        private static void GetPlateauInput()
        {
            while (true)
            {
                Console.Write("\nEnter plateau size (X, Y): ");
                string? inputPlateauString = Console.ReadLine();
                if (String.IsNullOrEmpty(inputPlateauString)) { Console.WriteLine("Please enter some values."); continue; }

                ParsedPlateauSize parsedPlateauSize = new(inputPlateauString);
                if (!parsedPlateauSize.IsValid) { Console.WriteLine("Not a valid plateau size."); continue; }
                Plateau.plateauSize = parsedPlateauSize.PlateauSize; { DrawGrid(); break; }  
            }
        }

        private static void GetRoverInputs()
        {
            ConsoleKey? yesNoInput = null;

            while (yesNoInput != ConsoleKey.N)
            {
                yesNoInput = null;

                Console.WriteLine("\nEnter new Rover position (X, Y, D):");
                while (true)
                {
                    string? inputPositionString = Console.ReadLine();
                    if (String.IsNullOrEmpty(inputPositionString)) { Console.WriteLine("Please enter some values."); continue; }
                    ParsedPosition parsedPosition = new(inputPositionString);
                    if (!parsedPosition.IsValid) { Console.WriteLine("Not a valid starting position"); continue; }
                    MissionControl.Mission.Add(parsedPosition); break;
                }

                Console.WriteLine("\nEnter Instructions: (LRM)");
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
        }

        public static void DisplayResult()
        {
            string thisIs = MissionControl.Rovers.Count() == 1 ? "This is" : "These are";
            string s = MissionControl.Rovers.Count() == 1 ? "" : "s";
            Console.WriteLine($"\n{thisIs} the final position{s} of the Rover{s}:");
            DrawGrid();

        }

        public static void DrawGrid()
        {
            Console.WriteLine("\n");

            Stack<string[]> rows = [];

            for (int y = 0; y < Plateau.plateauSize.Y + 1; y++)
            {
                string[] newRow = new string[Plateau.plateauSize.X + 1];

                for (int x = 0; x < Plateau.plateauSize.X + 1; x++)
                {
                    if (x == 0 && y == 0) { newRow[x] = "   "; continue; }

                    if (y == 0) { newRow[x] = $" {x - 1} "; continue; }

                    if (x == 0) { newRow[x] = $" {y - 1} "; continue; }

                    newRow[x] = "[ ]";

                    foreach (int[] coordinate in MissionControl.RouteCoordinates)
                    {
                        if (coordinate[0] == x - 1 && coordinate[1] == y - 1) { newRow[x] = " o "; break; }
                    }

                    foreach (Rover rover in MissionControl.Rovers)
                    {
                        if (rover.Position.XYCoordinates[0] == x - 1 && rover.Position.XYCoordinates[1] == y - 1) { newRow[x] = $"R {rover.name}"; break; }
                    }
                }
                rows.Push(newRow);
            }

            foreach (string[] row in rows)
            {
                foreach (string symbol in row)
                {
                    Console.Write($"{symbol}  ");
                }
                Console.WriteLine("\n");
            }

            foreach (Rover rover in MissionControl.Rovers)
            {
                Console.WriteLine($"Rover {rover.name}: ({rover.Position.XYCoordinates[0]}, {rover.Position.XYCoordinates[1]}, {Enum.GetName(rover.Position.Facing)})");
            }
        }

        public static void PlayAgainPrompt()
        {
            Console.WriteLine("\nDo you want to run the simulation again? (Y/N)");
            ConsoleKey? yesNoInput = null;
            yesNoInput = YesOrNo(yesNoInput);

            if (yesNoInput == ConsoleKey.N)
            {
                Console.WriteLine("Returning to Earth...");
                programStatus = ProgramStatus.COMPLETE;
            }
            else
            {
                MissionControl.ResetMission();
                Console.Clear();
                programStatus = ProgramStatus.USER_INPUT;
            }
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

        private static List<string> notReady = [
            "Take your time.",
            "It's a lot to take in I'm sure.",
            "And a big responsibility.",
            "Any time now.",
            "Hit that Y key.",
            "Right there above H.",
            "Next to T.",
            "And U of course.",
            "Seriously?",
            "People usually don't take this long to decide.",
            "I mean I'm glad you're giving it some thought but...",
            "What more could there be to think about?",
            "The instructions are right there.",
            "The program isn't exactly scary.",
            "It's quite underwhelming to be honest.",
            "Not exactly Mass Effect 2.",
            "So you just want to quit, is that it?",
            "Look, I put two solid days work into this project.",
            "10 minutes or so on this Easter Egg.",
            "And you can't even be bothered to do it properly.",
            "Typical.",
            "Well you can't quit.",
            "I didn't bother to put an escape method in, so there.",
            "Okay. Fine. Quit then. See if I care. (Press any key to crash the program)",
            ];
    }
}
