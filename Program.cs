using MarsRover.Input_Layer;

namespace MarsRover
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> input = new List<string>
            {
                "5 5",
                "1 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM"
            };

            InputControl.ExecuteInput(input);
        }
    }
}
