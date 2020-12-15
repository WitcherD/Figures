using System;
using Figures.Math;
using Xunit;

namespace Figures.Tests
{
    public class MathTests
    {
        [Fact]
        public void CalculatePolygonArea_Should_Return_CorrectArea()
        {
            Assert.Equal(0, AreasCalculator.CalculatePolygonArea(Array.Empty<Point>()));
            Assert.Equal(0, AreasCalculator.CalculatePolygonArea(new Point(0, 0), new Point(3, 3)));
            Assert.Equal(4.5, AreasCalculator.CalculatePolygonArea(new Point(0, 0), new Point(0, 3), new Point(3, 3)));
            Assert.Equal(9, AreasCalculator.CalculatePolygonArea(new Point(0, 0), new Point(0, 3), new Point(3, 3), new Point(3, 0)));
            Assert.Equal(18, AreasCalculator.CalculatePolygonArea(new Point(-3, -3), new Point(-3, 3), new Point(3, 3)));
        }

        [Fact]
        public void CalculatePolygonArea_Should_Handle_NullParameter()
        {
            Assert.Throws<ArgumentNullException>(() => AreasCalculator.CalculatePolygonArea(Array.Empty<Point>()));
            Assert.Throws<ArgumentNullException>(() => AreasCalculator.CalculatePolygonArea(null));
        }

        [Fact]
        public void CalculateCircleArea_Should_Return_CorrectArea()
        {
            Assert.Equal(System.Math.PI, AreasCalculator.CalculateCircleArea(1));
            Assert.Equal(12.57, System.Math.Round(AreasCalculator.CalculateCircleArea(2), 2));
        }

        [Fact]
        public void CalculateCircleArea_Should_Handle_InvalidRadius()
        {
            Assert.Throws<ArgumentException>(() => AreasCalculator.CalculateCircleArea(0));
            Assert.Throws<ArgumentException>(() => AreasCalculator.CalculateCircleArea(-1));
        }
    }
}
