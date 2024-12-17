using MarsRover.Enums;
using MarsRover.Input_Layer;
using MarsRover.Logic_Layer;
using System.Collections;

namespace MarsRover.Tests
{
    public class IntegrationTestData
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(new List<string> {
                    "5 5",
                    "1 2 N",
                    "LMLMLMLMM",
                    "3 3 E",
                    "MMRMMRMRRM" })
                    .Returns(new List<string> {
                    "1 3 N",
                    "5 1 E"}).SetName("Two Rovers");

                yield return new TestCaseData(new List<string> {
                    "9 9",
                    "0 0 E",
                    "MMRM",
                    "2 3 N",
                    "MMR",
                    "6 8 W",
                    "MMRM"})
                    .Returns(new List<string> {
                    "2 0 S",
                    "2 5 E",
                    "4 9 N"}).SetName("Three Rovers");

                yield return new TestCaseData(new List<string> {
                    "4 4",
                    "2 2 N",
                    "R",
                    "2 2 N",
                    "MMRM"})
                    .Returns(new List<string> {
                    "2 2 E",}).SetName("Land Rover on Another Rover");

                yield return new TestCaseData(new List<string> {
                    "4 4",
                    "0 0 S",
                    "MRMRM",
                    "4 4 N",
                    "MRMRM"})
                    .Returns(new List<string> {
                    "0 1 N",
                    "4 3 S"}).SetName("Attempted Movement Out of Bounds");

                yield return new TestCaseData(new List<string> {
                    "5 2",
                    "0 7 N",
                    "MRM"})
                    .Returns(new List<string> {}).SetName("Land Rover Out of Bounds");
            }
        }
    }

    [TestFixture]
    public class IntegrationTests
    {
        [SetUp]
        public void Setup()
        {
            MissionControl.Rovers.Clear();
        }

        [TestCaseSource(typeof(IntegrationTestData), nameof(IntegrationTestData.TestCases))]
        public List<string> ExecuteInput(List<string> input)
        {
            ParsedPlateauSize parsedPlateauSize = new(input[0]);

            Plateau.plateauSize = parsedPlateauSize.PlateauSize;

            for (int i = 1; i < input.Count; i++)
            {
                if (i % 2 == 1)
                {
                    ParsedPosition parsedPosition = new(input[i]);

                    if (!MissionControl.IsCoordinateSafe(parsedPosition.Position.XYCoordinates)) continue;

                    Rover newRover = new(parsedPosition.Position);

                    ParsedInstructions parsedInstructions = new(input[i + 1]);

                    foreach (Instruction instruction in parsedInstructions.Instructions)
                    {
                        newRover.ExecuteInstruction(instruction);
                    }
                }
            }

            return MissionControl.CurrentPositionStrings();
        }
    }
}
