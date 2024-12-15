﻿using MarsRover.Enums;

namespace MarsRover.Input_Layer
{
    public class ParsedPosition
    {
        public RoverPostion Position { get; set; } = new RoverPostion([-1, -1], CompassDirection.INVALID);

        public bool IsValid = false;

        public ParsedPosition(string positionInputString)
        {
            ParsePosition(positionInputString);
        }

        private void ParsePosition(string positionInputString)
        {
            string[] positionInputArray = positionInputString.Split(' ');

            if (positionInputArray.Length != 3) return;

            bool xIsValid = int.TryParse(positionInputArray[0], out int resultX);
            bool yIsValid = int.TryParse(positionInputArray[1], out int resultY);
            if (!xIsValid || !yIsValid) return;
            if (resultX <= 0 || resultY <= 0) return;

            ParsedCompassDirection compassDirection = new ParsedCompassDirection(positionInputArray[2]);
            if (compassDirection.Direction == CompassDirection.INVALID) return;

            IsValid = true;
            Position = new([resultX, resultY], compassDirection.Direction);
        }
    }
}
