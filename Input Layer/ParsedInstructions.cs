using MarsRover.Enums;

namespace MarsRover.Input_Layer
{
    public class ParsedInstructions : ParsedInput, IParsable
    {
        public List<Instruction> Instructions { get; set; } = new();

        public bool IsValid { get; set; }  = false;

        public ParsedInstructions(string instructionInputString)
        {
            Parse(instructionInputString);
        } 

        public void Parse(string instructionInputString)
        {
            string[] instructionInputArray = instructionInputString
                .Select(c => c.ToString()).Where(c => c != "(" && c != ")" && c != ",")
                .ToArray();

            foreach(string s in instructionInputArray)
            {
                Instruction instruction = s switch
                {
                    "L" => Instruction.L,
                    "R" => Instruction.R,
                    "M" => Instruction.M,
                    _ => Instruction.INVALID
                };

                if (instruction == Instruction.INVALID)
                {
                    IsValid = false;
                    Instructions.Clear();
                    break;
                }
                IsValid = true;
                Instructions.Add(instruction);
            } 
        }
    }
}
