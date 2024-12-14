using MarsRover.Enums;

namespace MarsRover.Input_Layer
{
    public class ParsedCompassDirection
    {
        public CompassDirection Direction { get; set; } = CompassDirection.INVALID;

        public ParsedCompassDirection(string directionInputString)
        {
            ParseCompassDirection(directionInputString);
        }

        private void ParseCompassDirection(string directionInputString)
        {
            Direction = directionInputString switch
            {
                "N" => CompassDirection.N,
                "E" => CompassDirection.E,
                "S" => CompassDirection.S,
                "W" => CompassDirection.W,
                _ => CompassDirection.INVALID
            };
        }
    }
}
