using System;

namespace Figures.Math
{
    public static class AreasCalculator
    {
        public static double CalculatePolygonArea(params Point[] polygon)
        {
            if (polygon == null || polygon.Length == 0)
                throw new ArgumentNullException("Polygon must consist of 1 or more points", nameof(polygon));

            double area = 0;
            for (var i = 0; i < polygon.Length; i++)
            {
                var j = (i + 1) % polygon.Length;

                area += polygon[i].X * polygon[j].Y;
                area -= polygon[i].Y * polygon[j].X;
            }
            area /= 2;
            return area < 0 ? -area : area;
        }

        public static double CalculateCircleArea(double radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Radius must be greater than 0", nameof(radius));
            
            return System.Math.PI * (radius * radius);
        }
    }
}
