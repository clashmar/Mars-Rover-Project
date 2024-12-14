namespace MarsRover.Input_Layer
{
    public class ParsedPlateauSize
    {
        public PlateauSize PlateauSize { get; set; } = new(0, 0);

        public bool IsValid = false;

        public ParsedPlateauSize(string plateauInputString)
        {
            ParsePlateauSize(plateauInputString);
        }

        private void ParsePlateauSize(string plateauInput)
        {
            string[] plateauInputArray = plateauInput.Split(' ');

            if (plateauInputArray.Length != 2) return;

            bool xIsValid = int.TryParse(plateauInputArray[0], out int resultX);
            bool yIsValid = int.TryParse(plateauInputArray[1], out int resultY);

            if(!xIsValid || !yIsValid) return;

            if(resultX <= 0 || resultY <= 0) return;

            IsValid = true;
            this.PlateauSize = new(resultX, resultY);
        }
    }
}
