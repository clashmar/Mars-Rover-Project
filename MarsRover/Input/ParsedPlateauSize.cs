using MarsRover.Logic;

namespace MarsRover.Input_Layer
{
    public class ParsedPlateauSize : ParsedInput, IParsable
    {
        public PlateauSize PlateauSize { get; set; } = new(0, 0);

        public bool IsValid { get; set; } = false;

        public ParsedPlateauSize(string plateauInputString)
        {
            Parse(plateauInputString);
        }

        public void Parse(string plateauInput)
        {
            string[] plateauInputArray = FilterBracketsAndCommas(plateauInput.Split(' '));

            if (plateauInputArray.Length != 2) return;

            bool xIsValid = int.TryParse(plateauInputArray[0], out int resultX);
            bool yIsValid = int.TryParse(plateauInputArray[1], out int resultY);

            if(!xIsValid || !yIsValid) return;

            if(resultX <= 0 || resultY <= 0 || resultX > 9 || resultY > 9) return;

            IsValid = true;
            this.PlateauSize = new(resultX+1, resultY+1);
        }
    }
}
