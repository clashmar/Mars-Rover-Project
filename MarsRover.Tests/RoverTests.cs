using MarsRover.Enums;
using MarsRover.Logic;
using MarsRover.Logic_Layer;

namespace MarsRover.Tests
{
    public class RoverTests
    {
        [SetUp]
        public void Setup()
        {
            MissionControl.Rovers.Clear();
        }

        [TestCase(CompassDirection.N, Instruction.R, CompassDirection.E)]
        [TestCase(CompassDirection.E, Instruction.R, CompassDirection.S)]
        [TestCase(CompassDirection.S, Instruction.R, CompassDirection.W)]
        [TestCase(CompassDirection.W, Instruction.R, CompassDirection.N)]

        [TestCase(CompassDirection.N, Instruction.L, CompassDirection.W)]
        [TestCase(CompassDirection.E, Instruction.L, CompassDirection.N)]
        [TestCase(CompassDirection.S, Instruction.L, CompassDirection.E)]
        [TestCase(CompassDirection.W, Instruction.L, CompassDirection.S)]
        public void RotateRoverTest_ValidInstruction(CompassDirection startingDirection, Instruction instruction, CompassDirection expectedFinalDirection)
        {
            RoverPostion startingPosition = new([1, 1],startingDirection);
            Rover testRover = new(startingPosition);

            testRover.RotateRover(instruction);

            Assert.That(testRover.Position.Facing, Is.EqualTo(expectedFinalDirection));
        }

        [TestCase(Instruction.M)]
        [TestCase(Instruction.INVALID)]
        public void RotateRoverTest_InvalidInstruction(Instruction invalidInstruction)
        {
            RoverPostion startingPosition = new([1, 1], CompassDirection.N);
            Rover testRover = new(startingPosition);

            Assert.Throws<ArgumentException>(() => testRover.RotateRover(invalidInstruction));
        }
        [TestCase(1, 1, CompassDirection.N, 1, 2)]
        [TestCase(3, 2, CompassDirection.E, 4, 2)]
        [TestCase(7, 9, CompassDirection.S, 7, 8)]
        [TestCase(10, 6, CompassDirection.W, 9, 6)]
        [TestCase(11, 19, CompassDirection.INVALID, 11, 19)]
        public void GetTargetCoordinateTest(int roverCoordinateX, int roverCoordinateY, CompassDirection factingDirection, int targetX, int targetY)
        {
            RoverPostion startingPosition = new([roverCoordinateX, roverCoordinateY], factingDirection);
            Rover testRover = new(startingPosition);

            int[] targetCoordinate = testRover.GetTargetCoordinates();

            Assert.That(targetCoordinate[0], Is.EqualTo(targetX));
            Assert.That(targetCoordinate[1], Is.EqualTo(targetY));
        }

        [TestCase(6, 8, CompassDirection.N, 6, 9)]
        [TestCase(2, 5, CompassDirection.E, 3, 5)]
        [TestCase(7, 1, CompassDirection.S, 7, 0)]
        [TestCase(3, 4, CompassDirection.W, 2, 4)]
        public void MoveRoverTest_Unobstructed(int startingX, int startingY, CompassDirection facingDirection, int finalX, int finalY)
        {
            Plateau.plateauSize = new PlateauSize(20, 20);

            RoverPostion startingPosition = new([startingX, startingY], facingDirection);
            Rover testRover = new(startingPosition);

            testRover.MoveRover();

            Assert.That(testRover.Position.XYCoordinates[0], Is.EqualTo(finalX));
            Assert.That(testRover.Position.XYCoordinates[1], Is.EqualTo(finalY));
        }

        [TestCase(1, 1, 1, 1, true)]
        [TestCase(0, 0, 0, 0, true)]
        [TestCase(-2, -9, -2, -9, true)]

        [TestCase(1, 1, 1, 2, false)]
        [TestCase(4, 5, 5, 4, false)]
        [TestCase(10, 1, 6, 8, false)]
        public void IsCoordinateOccupiedTest(int roverCoordinateX, int roverCoordinateY, int targetCoordinateX, int targetCoordinateY, bool IsOccupied)
        {
            RoverPostion roverPosition = new([roverCoordinateX, roverCoordinateY], CompassDirection.N);
            Rover testRover = new(roverPosition);
            int[] targetCoordinate = [targetCoordinateX, targetCoordinateY];

            Assert.That(MissionControl.IsCoordinateOccupied(targetCoordinate), Is.EqualTo(IsOccupied));
        }

        [TestCase(0, 0, 1, 0, CompassDirection.E)]
        [TestCase(0, 0, 0, 1, CompassDirection.N)]
        [TestCase(2, 4, 2, 3, CompassDirection.S)]
        [TestCase(5, 3, 4, 3, CompassDirection.W)]
        public void MoveRoverTest_Obstructed(int movingRoverX, int movingRoverY, int obstructingRoverX, int obstructingRoverY, CompassDirection facingDirection)
        {
            Plateau.plateauSize = new PlateauSize(6, 6);
            RoverPostion movingRoverPosition = new([movingRoverX, movingRoverY], facingDirection);
            Rover movingRover = new(movingRoverPosition);

            RoverPostion obstructingRoverPosition = new([obstructingRoverX, obstructingRoverY], CompassDirection.E);
            Rover obstructingRover = new(obstructingRoverPosition);

            movingRover.MoveRover();

            Assert.That(movingRover.Position.XYCoordinates[0], Is.EqualTo(movingRoverX));
            Assert.That(movingRover.Position.XYCoordinates[1], Is.EqualTo(movingRoverY));
            Assert.That(movingRover.IsObstructed, Is.EqualTo(true));
        }

        [TestCase(5, 5, CompassDirection.N)]
        [TestCase(5, 5, CompassDirection.E)]
        [TestCase(0, 0, CompassDirection.W)]
        [TestCase(0, 0, CompassDirection.S)]
        public void MoveRover_OutOfBounds(int roverCoordinateX, int roverCoordinateY, CompassDirection facingDirection)
        {
            Plateau.plateauSize = new PlateauSize(6, 6);
            RoverPostion roverPosition = new([roverCoordinateX, roverCoordinateY], facingDirection);
            Rover testRover = new(roverPosition);

            testRover.MoveRover();

            Assert.That(testRover.Position.XYCoordinates[0], Is.EqualTo(roverCoordinateX));
            Assert.That(testRover.Position.XYCoordinates[1], Is.EqualTo(roverCoordinateY));
            Assert.That(testRover.IsObstructed, Is.EqualTo(true));
        }
    }
}
