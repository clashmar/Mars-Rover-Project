using MarsRover.Enums;

namespace MarsRover.Input_Layer
{
    public class ParsedInstructions
    {
        public List<Instruction> Instructions { get; set; } = new();

        public bool IsValid = false;

        public ParsedInstructions(string instructionInputString)
        {
            ParseInstruction(instructionInputString);
        } 

        private void ParseInstruction(string instructionInputString)
        {
            foreach(char c in  instructionInputString)
            {
                Instruction instruction = c switch
                {
                    'L' => Instruction.L,
                    'R' => Instruction.R,
                    'M' => Instruction.M,
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
