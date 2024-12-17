using MarsRover.Enums;
using MarsRover.Input_Layer;

namespace MarsRover.Tests
{
    public class ParserTests
    {
        [TestCase("(4, 5)", 5, 6)]
        [TestCase("(9, 9)", 10, 10)]
        [TestCase("7, 3", 8, 4)]
        [TestCase("1 1", 2, 2)]
        public void ParsePlateauSizeTest_Valid(string inputString, int expectedX, int expectedY)
        {
            ParsedPlateauSize parsedPlateauSize = new(inputString);

            Assert.That(parsedPlateauSize.PlateauSize.X, Is.EqualTo(expectedX));
            Assert.That(parsedPlateauSize.PlateauSize.Y, Is.EqualTo(expectedY));
            Assert.That(parsedPlateauSize.IsValid, Is.EqualTo(true));
        }

        [TestCase("1")]
        [TestCase("(1, 2, 4)")]
        [TestCase("(0 1)")]
        [TestCase("(6, -1")]
        [TestCase(" 1 2")]
        [TestCase("(1,  2")]
        [TestCase("(1, 2 )")]
        [TestCase("(1, P)")]
        [TestCase("(!, 7)")]
        [TestCase("")]
        public void ParsePlateauSizeTest_Invalid(string inputString)
        {
            ParsedPlateauSize parsedPlateauSize = new(inputString);

            Assert.That(parsedPlateauSize.PlateauSize.X, Is.EqualTo(0));
            Assert.That(parsedPlateauSize.PlateauSize.Y, Is.EqualTo(0));
            Assert.That(parsedPlateauSize.IsValid, Is.EqualTo(false));
        }

        [TestCase("L", Instruction.L)]
        [TestCase("R", Instruction.R)]
        [TestCase("M", Instruction.M)]
        public void ParseInstructionTest_ValidSingle(string testString, Instruction expectedInstruction)
        {
            ParsedInstructions parsedInstruction = new(testString);

            Assert.That(parsedInstruction.Instructions[0], Is.EqualTo(expectedInstruction));
            Assert.That(parsedInstruction.IsValid, Is.EqualTo(true));
        }

        [TestCase("!")]
        [TestCase("r")]
        [TestCase("2")]
        [TestCase("RMR2")]
        [TestCase("MMLR M")]
        [TestCase(" LLR")]
        [TestCase("LMR ")]
        [TestCase("")]
        public void ParseInstruction_Invalid(string testString)
        {
            ParsedInstructions parsedInstruction = new(testString);

            Assert.That(parsedInstruction.Instructions.Count, Is.EqualTo(0));
            Assert.That(parsedInstruction.IsValid, Is.EqualTo(false));
        }

        [Test]
        public void ParseInstructionTest_ValidMultiple()
        {
            ParsedInstructions parsedInstruction = new("(LMRML)");
            List<Instruction> expectedInstructions = new List<Instruction>
            {
                Instruction.L,
                Instruction.M,
                Instruction.R,
                Instruction.M,
                Instruction.L,
            };

            Assert.That(parsedInstruction.Instructions, Is.EquivalentTo(expectedInstructions));
            Assert.That(parsedInstruction.IsValid, Is.EqualTo(true));
        }

        [TestCase("N", CompassDirection.N)]
        [TestCase("E", CompassDirection.E)]
        [TestCase("S", CompassDirection.S)]
        [TestCase("W", CompassDirection.W)]
        [TestCase("West", CompassDirection.INVALID)]
        [TestCase(" ", CompassDirection.INVALID)]
        [TestCase("N!", CompassDirection.INVALID)]
        [TestCase("2", CompassDirection.INVALID)]
        public void ParseCompassDirectionTest(string testString, CompassDirection expectedCompassDirection)
        {
            ParsedCompassDirection parsedCompassDirection = new(testString);

            Assert.That(parsedCompassDirection.Direction, Is.EqualTo(expectedCompassDirection));
        }

        [TestCase("(2, 7, N)", 2, 7, CompassDirection.N)]
        [TestCase("(9, 10, E)", 9, 10, CompassDirection.E)]
        [TestCase("1 6 S", 1, 6, CompassDirection.S)]
        [TestCase("21 3 W", 21, 3, CompassDirection.W)]
        public void ParsePositionTest_Valid(string inputString, int expectedX, int expectedY, CompassDirection expectedDirection)
        {
            ParsedPosition parsedPosition = new(inputString);

            Assert.That(parsedPosition.Position.XYCoordinates[0], Is.EqualTo(expectedX));
            Assert.That(parsedPosition.Position.XYCoordinates[1], Is.EqualTo(expectedY));
            Assert.That(parsedPosition.Position.Facing, Is.EqualTo(expectedDirection));
            Assert.That(parsedPosition.IsValid, Is.EqualTo(true));
        }

        [TestCase("(1, 3, !)", -1, -1, CompassDirection.INVALID)]
        [TestCase("5 10 7 N", -1, -1, CompassDirection.INVALID)]
        [TestCase("(-1, 3, E)", -1, -1, CompassDirection.INVALID)]
        [TestCase("1 -3 S", -1, -1, CompassDirection.INVALID)]
        [TestCase("( 4, 6,  W)", -1, -1, CompassDirection.INVALID)]
        [TestCase("", -1, -1, CompassDirection.INVALID)]
        public void ParsePositionTest_Invalid(string inputString, int expectedX, int expectedY, CompassDirection expectedDirection)
        {
            ParsedPosition parsedPosition = new(inputString);

            Assert.That(parsedPosition.Position.XYCoordinates[0], Is.EqualTo(expectedX));
            Assert.That(parsedPosition.Position.XYCoordinates[1], Is.EqualTo(expectedY));
            Assert.That(parsedPosition.Position.Facing, Is.EqualTo(expectedDirection));
            Assert.That(parsedPosition.IsValid, Is.EqualTo(false));
        }
    }
}