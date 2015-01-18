using System;
using System.Drawing;
using Point = System.Windows.Point;

namespace HelperFramework
{
	public static class Math
	{
		public static Double DegreesToRadians(Double degrees)
		{
			return ((degrees / 360) * 2 * System.Math.PI);
		}
		public static Double RadiansToDegrees(Double radians)
		{
			return radians * ((360 / 2) / System.Math.PI);
		}

		public static Vector CreateVectorFromAngle(Double angleInDegrees, Double length)
		{
			Double x = System.Math.Sin(DegreesToRadians(angleInDegrees)) * length;
			Double y = System.Math.Cos(DegreesToRadians(angleInDegrees)) * length;
			return new Vector(x, y);
		}
		public static Vector CreateVectorFromAngle(Double angleInDegrees, Vector vector)
		{
			angleInDegrees = GetAngleFromVector(vector) + DegreesToRadians(angleInDegrees);
			Double length = GetLengthFromVector(vector);
			Double x = System.Math.Sin((angleInDegrees)) * length;
			Double y = System.Math.Cos((angleInDegrees)) * length;
			return new Vector(x, y);
		}
		public static Vector CreateVectorFromAngle(Double angleInDegrees, Vector vector, Double length)
		{
			angleInDegrees = GetAngleFromVector(vector) + DegreesToRadians(angleInDegrees);
			Double x = System.Math.Sin((angleInDegrees)) * length;
			Double y = System.Math.Cos((angleInDegrees)) * length;
			return new Vector(x, y);
		}

		public static Double GetLengthFromVector(Vector vector)
		{
			Double x = vector.X * vector.X;
			Double y = vector.Y * vector.Y;
			return System.Math.Sqrt(x + y);
		}

		public static Double GetAngleFromVector(Vector vector)
		{
			return System.Math.Atan(vector.X / vector.Y);
		}

		public static Boolean ContainsPoint(Point centre, Double radius, Point hitTest)
		{
			Double x = hitTest.X - centre.X;
			Double y = hitTest.Y - centre.Y;
			return radius * radius >= (x * x + y * y);
		}

		/// <summary>
		/// Divides a rectangle with an value;
		/// </summary>
		/// <param name="source">The rectangle;</param>
		/// <param name="divider">The divider;</param>
		/// <returns>A new rectangle divided by the <paramref name="divider"/>;</returns>
		public static Rectangle Divide(Rectangle source, Int32 divider)
		{
			return new Rectangle(source.Left / divider, source.Top / divider, source.Width / divider, source.Height / divider);
		}
	}
}
