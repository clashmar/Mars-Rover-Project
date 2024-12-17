using MarsRover.Enums;
using MarsRover.Input_Layer;
using MarsRover.Logic_Layer;

namespace MarsRover.Tests
{
    public class PlateauTests
    {
        [TestCase(5, 6)]
        [TestCase(6, 5)]
        [TestCase(3, -1)]
        [TestCase(-1, 0)]
        public void IsOutOfBoundsTest_True(int targetCoordinateX, int targetCoordinateY)
        {
            Plateau.plateauSize = new PlateauSize(6, 6);
            int[] targetCoordinate = [targetCoordinateX, targetCoordinateY];

            Assert.That(Plateau.IsOutOfBounds(targetCoordinate), Is.EqualTo(true));
        }

        [TestCase(3, 7)]
        [TestCase(5, 9)]
        [TestCase(9, 6)]
        [TestCase(0, 0)]
        public void IsOutOfBoundsTest_False(int targetCoordinateX, int targetCoordinateY)
        {
            Plateau.plateauSize = new PlateauSize(10, 10);
            int[] targetCoordinate = [targetCoordinateX, targetCoordinateY];

            Assert.That(Plateau.IsOutOfBounds(targetCoordinate), Is.EqualTo(false));
        }
    }
}
