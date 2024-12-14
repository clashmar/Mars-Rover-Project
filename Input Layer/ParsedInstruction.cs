using MarsRover.Enums;

namespace MarsRover.Input_Layer
{
    public class ParsedInstruction
    {
        public List<Instruction> Instructions { get; set; } = new();

        public bool IsInvalid = false;

        public ParsedInstruction(string instructionInputString)
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

                if (instruction == Instruction.INVALID) IsInvalid = true;

                Instructions.Add(instruction);
            } 
        }
    }
}
