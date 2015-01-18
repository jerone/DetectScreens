// provided by http://SilverlightRocks.com and http://SilverlightGames101.net
// free for any use, personal or commercial
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace HelperFramework
{
	// Summary:
	//     Represents a displacement in 2-D space.
	//    [Serializable]
	//    [TypeConverter(typeof(VectorConverter))]
	//    [ValueSerializer(typeof(VectorValueSerializer))]
	public struct Vector
	{
		public double X;
		public double Y;
		static Vector _zero = new Vector(0, 0);

		//
		// Summary:
		//     Initializes a new instance of the Vector structure.
		//
		// Parameters:
		//   y:
		//     The Vector.Y-offset of the new Vector.
		//
		//   x:
		//     The Vector.X-offset of the new Vector.
		public Vector(double x, double y)
		{
			X = x;
			Y = y;
		}

		static public Vector Zero
		{
			get
			{
				return _zero;
			}
		}

		// Summary:
		//     Negates the specified vector.
		//
		// Parameters:
		//   vector:
		//     The vector to negate.
		//
		// Returns:
		//     A vector with Vector.X and Vector.Y values
		//     opposite of the Vector.X and Vector.Y values
		//     of vector.
		public static Vector operator -(Vector vector)
		{
			return new Vector(-vector.X, -vector.Y);
		}
		//
		// Summary:
		//     Subtracts one specified vector from another.
		//
		// Parameters:
		//   vector2:
		//     The vector to subtract from vector1.
		//
		//   vector1:
		//     The vector from which vector2 is subtracted.
		//
		// Returns:
		//     The difference between vector1 and vector2.
		public static Vector operator -(Vector vector1, Vector vector2)
		{
			return Subtract(vector1, vector2);
		}
		//
		// Summary:
		//     Compares two vectors for inequality.
		//
		// Parameters:
		//   vector2:
		//     The second vector to compare.
		//
		//   vector1:
		//     The first vector to compare.
		//
		// Returns:
		//     true if the Vector.X and Vector.Y components
		//     of vector1 and vector2 are different; otherwise, false.
		public static bool operator !=(Vector vector1, Vector vector2)
		{
			return !Equals(vector1, vector2);
		}
		//
		// Summary:
		//     Multiplies the specified scalar by the specified vector and returns the resulting
		//     vector.
		//
		// Parameters:
		//   scalar:
		//     The scalar to multiply.
		//
		//   vector:
		//     The vector to multiply.
		//
		// Returns:
		//     The result of multiplying scalar and vector.
		public static Vector operator *(double scalar, Vector vector)
		{
			return Multiply(scalar, vector);
		}
		//
		// Summary:
		//     Multiplies the specified vector by the specified scalar and returns the resulting
		//     vector.
		//
		// Parameters:
		//   scalar:
		//     The scalar to multiply.
		//
		//   vector:
		//     The vector to multiply.
		//
		// Returns:
		//     The result of multiplying vector and scalar.
		public static Vector operator *(Vector vector, double scalar)
		{
			return new Vector(scalar * vector.X, scalar * vector.Y);
		}
		//
		// Summary:
		//     Transforms the coordinate space of the specified vector using the specified
		//     System.Windows.Media.Matrix.
		//
		// Parameters:
		//   matrix:
		//     The transformation to apply to vector.
		//
		//   vector:
		//     The vector to transform.
		//
		// Returns:
		//     The result of transforming vector by matrix.
		public static Vector operator *(Vector vector, Matrix matrix)
		{
			throw new NotImplementedException("not implemented yet, sorry");
		}
		//
		// Summary:
		//     Calculates the dot product of the two specified vector structures and returns
		//     the result as a System.Double.
		//
		// Parameters:
		//   vector2:
		//     The second vector to multiply.
		//
		//   vector1:
		//     The first vector to multiply.
		//
		// Returns:
		//     Returns a System.Double containing the scalar dot product of vector1 and
		//     vector2, which is calculated using the following formula:vector1.X * vector2.X
		//     + vector1.Y * vector2.Y
		public static double operator *(Vector vector1, Vector vector2)
		{
			return Multiply(vector1, vector2);
		}
		//
		// Summary:
		//     Divides the specified vector by the specified scalar and returns the resulting
		//     vector.
		//
		// Parameters:
		//   scalar:
		//     The scalar by which vector will be divided.
		//
		//   vector:
		//     The vector to divide.
		//
		// Returns:
		//     The result of dividing vector by scalar.
		public static Vector operator /(Vector vector, double scalar)
		{
			return new Vector(vector.X / scalar, vector.Y / scalar);
		}
		//
		// Summary:
		//     Translates a point by the specified vector and returns the resulting point.
		//
		// Parameters:
		//   point:
		//     The point to translate.
		//
		//   vector:
		//     The vector used to translate point.
		//
		// Returns:
		//     The result of translating point by vector.
		public static Point operator +(Vector vector, Point point)
		{
			return new Point(point.X + vector.X, point.Y + vector.Y);
		}
		public static Point operator +(Point point, Vector vector)
		{
			return new Point(point.X + vector.X, point.Y + vector.Y);
		}
		public static Point operator -(Point point, Vector vector)
		{
			return new Point(point.X - vector.X, point.Y - vector.Y);
		}
		//
		// Summary:
		//     Adds two vectors and returns the result as a vector.
		//
		// Parameters:
		//   vector2:
		//     The second vector to add.
		//
		//   vector1:
		//     The first vector to add.
		//
		// Returns:
		//     The sum of vector1 and vector2.
		public static Vector operator +(Vector vector1, Vector vector2)
		{
			return new Vector(vector1.X + vector2.X, vector1.Y + vector2.Y);
		}
		//
		// Summary:
		//     Compares two vectors for equality.
		//
		// Parameters:
		//   vector2:
		//     The second vector to compare.
		//
		//   vector1:
		//     The first vector to compare.
		//
		// Returns:
		//     true if the Vector.X and Vector.Y components
		//     of vector1 and vector2 are equal; otherwise, false.
		public static bool operator ==(Vector vector1, Vector vector2)
		{
			return Equals(vector1, vector2);
		}
		//
		// Summary:
		//     Creates a System.Windows.Point with the Vector.X and Vector.Y
		//     values of this vector.
		//
		// Parameters:
		//   vector:
		//     The vector to convert.
		//
		// Returns:
		//     A point with System.Windows.Point.X- and System.Windows.Point.Y-coordinate
		//     values equal to the Vector.X and Vector.Y offset
		//     values of vector.
		public static explicit operator Point(Vector vector)
		{
			return new Point(vector.X, vector.Y);
		}
		public static explicit operator Vector(Point point)
		{
			return new Vector(point.X, point.Y);
		}
		//
		// Summary:
		//     Creates a System.Windows.Size from the offsets of this vector.
		//
		// Parameters:
		//   vector:
		//     The vector to convert.
		//
		// Returns:
		//     A System.Windows.Size with a System.Windows.Size.Width equal to the absolute
		//     value of this vector's Vector.X property and a System.Windows.Size.Height
		//     equal to the absolute value of this vector's Vector.Y property.
		public static explicit operator Size(Vector vector)
		{
			return new Size(System.Math.Abs(vector.X), System.Math.Abs(vector.Y));
		}

		// Summary:
		//     Gets the length of this vector.
		//
		// Returns:
		//     The length of this vector.
		public double Length
		{
			get
			{
				return System.Math.Sqrt(LengthSquared);
			}
		}
		//
		// Summary:
		//     Gets the square of the length of this vector.
		//
		// Returns:
		//     The square of the Vector.Length of this vector.
		public double LengthSquared
		{
			get
			{
				return X * X + Y * Y;
			}
		}

		// Summary:
		//     Translates the specified point by the specified vector and returns the resulting
		//     point.
		//
		// Parameters:
		//   point:
		//     The point to translate.
		//
		//   vector:
		//     The amount to translate the specified point.
		//
		// Returns:
		//     The result of translating point by vector.
		public static Point Add(Vector vector, Point point)
		{
			return new Point(vector.X + point.X, vector.Y + point.Y);
		}
		//
		// Summary:
		//     Adds two vectors and returns the result as a Vector structure.
		//
		// Parameters:
		//   vector2:
		//     The second vector to add.
		//
		//   vector1:
		//     The first vector to add.
		//
		// Returns:
		//     The sum of vector1 and vector2.
		public static Vector Add(Vector vector1, Vector vector2)
		{
			return new Vector(vector1.X + vector2.X, vector1.Y + vector2.Y);
		}

		//
		// Summary:
		//     Retrieves the angle, expressed in degrees, between the two specified vectors.
		//
		// Parameters:
		//   vector2:
		//     The second vector to evaluate.
		//
		//   vector1:
		//     The first vector to evaluate.
		//
		// Returns:
		//     The angle, in degrees, between vector1 and vector2.
		public static double AngleBetween(Vector vector1, Vector vector2)
		{
			throw new NotImplementedException("not implemented yet");
		}

		//
		// Summary:
		//     Calculates the cross product of two vectors.
		//
		// Parameters:
		//   vector2:
		//     The second vector to evaluate.
		//
		//   vector1:
		//     The first vector to evaluate.
		//
		// Returns:
		//     The cross product of vector1 and vector2. The following formula is used to
		//     calculate the cross product: (Vector1.X * Vector2.Y) - (Vector1.Y * Vector2.X)
		public static double CrossProduct(Vector vector1, Vector vector2)
		{
			return (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
		}

		//
		// Summary:
		//     Calculates the determinant of two vectors.
		//
		// Parameters:
		//   vector2:
		//     The second vector to evaluate.
		//
		//   vector1:
		//     The first vector to evaluate.
		//
		// Returns:
		//     The determinant of vector1 and vector2.
		public static double Determinant(Vector vector1, Vector vector2)
		{
			throw new NotImplementedException("not implemented yet");
		}

		//
		// Summary:
		//     Divides the specified vector by the specified scalar and returns the result
		//     as a Vector.
		//
		// Parameters:
		//   scalar:
		//     The amount by which vector is divided.
		//
		//   vector:
		//     The vector structure to divide.
		//
		// Returns:
		//     The result of dividing vector by scalar.
		public static Vector Divide(Vector vector, double scalar)
		{
			return new Vector(vector.X / scalar, vector.Y / scalar);
		}

		//
		// Summary:
		//     Determines whether the specified System.Object is a Vector
		//     structure and, if it is, whether it has the same Vector.X
		//     and Vector.Y values as this vector.
		//
		// Parameters:
		//   o:
		//     The vector to compare.
		//
		// Returns:
		//     true if o is a Vector and has the same Vector.X
		//     and Vector.Y values as this vector; otherwise, false.
		public override bool Equals(object o)
		{
			if (o is Vector)
			{
				Vector v = (Vector)o;
				return Equals(v);
			}
			return false;
		}
		//
		// Summary:
		//     Compares two vectors for equality.
		//
		// Parameters:
		//   value:
		//     The vector to compare with this vector.
		//
		// Returns:
		//     true if value has the same Vector.X and Vector.Y
		//     values as this vector; otherwise, false.
		public bool Equals(Vector value)
		{
			return Equals(this, value);
		}
		//
		// Summary:
		//     Compares the two specified vectors for equality.
		//
		// Parameters:
		//   vector2:
		//     The second vector to compare.
		//
		//   vector1:
		//     The first vector to compare.
		//
		// Returns:
		//     true if t he Vector.X and Vector.Y components
		//     of vector1 and vector2 are equal; otherwise, false.
		public static bool Equals(Vector vector1, Vector vector2)
		{
			return (vector1.X == vector2.X && vector1.Y == vector2.Y);
		}

		//
		// Summary:
		//     Returns the hash code for this vector.
		//
		// Returns:
		//     The hash code for this instance.
		public override int GetHashCode()
		{
			int x = (int)X;
			int y = (int)Y;
			int fx = (int)((X - x) * 10000);
			int fy = (int)((Y - y) * 10000);
			return x ^ y ^ fx ^ fy;
		}

		//
		// Summary:
		//     Multiplies the specified scalar by the specified vector and returns the resulting
		//     Vector.
		//
		// Parameters:
		//   scalar:
		//     The scalar to multiply.
		//
		//   vector:
		//     The vector to multiply.
		//
		// Returns:
		//     The result of multiplying scalar and vector.
		public static Vector Multiply(double scalar, Vector vector)
		{
			return new Vector(scalar * vector.X, scalar * vector.Y);
		}
		//
		// Summary:
		//     Multiplies the specified vector by the specified scalar and returns the resulting
		//     Vector.
		//
		// Parameters:
		//   scalar:
		//     The scalar to multiply.
		//
		//   vector:
		//     The vector to multiply.
		//
		// Returns:
		//     The result of multiplying vector and scalar.
		public static Vector Multiply(Vector vector, double scalar)
		{
			return new Vector(scalar * vector.X, scalar * vector.Y);
		}
		//
		// Summary:
		//     Transforms the coordinate space of the specified vector using the specified
		//     System.Windows.Media.Matrix.
		//
		// Parameters:
		//   matrix:
		//     The transformation to apply to vector.
		//
		//   vector:
		//     The vector structure to transform.
		//
		// Returns:
		//     The result of transforming vector by matrix.
		public static Vector Multiply(Vector vector, Matrix matrix)
		{
			throw new NotImplementedException("not implemented yet");
		}
		//
		// Summary:
		//     Calculates the dot product of the two specified vectors and returns the result
		//     as a System.Double.
		//
		// Parameters:
		//   vector2:
		//     The second vector structure to multiply.
		//
		//   vector1:
		//     The first vector to multiply.
		//
		// Returns:
		//     A System.Double containing the scalar dot product of vector1 and vector2,
		//     which is calculated using the following formula: (vector1.X * vector2.X)
		//     + (vector1.Y * vector2.Y)
		public static double Multiply(Vector vector1, Vector vector2)
		{
			return (vector1.X * vector2.X + vector1.Y * vector2.Y);
		}

		//
		// Summary:
		//     Negates this vector.
		public void Negate()
		{
			X = -X;
			Y = -Y;
		}

		//
		// Summary:
		//     Normalizes this vector.
		public void Normalize()
		{
			double length = Length;
			X /= length;
			Y /= length;
		}

		//
		// Summary:
		//     Converts a string representation of a vector into the equivalent Vector
		//     structure.
		//
		// Parameters:
		//   source:
		//     The string representation of the vector.
		//
		// Returns:
		//     The equivalent Vector structure.
		public static Vector Parse(string source)
		{
			Vector v;
			try
			{
				string[] s = source.Split(",".ToCharArray());
				v.X = double.Parse(s[0]);
				v.Y = double.Parse(s[1]);
			}
			catch (Exception)
			{
				throw new FormatException("Vector string is not in correct format");
			}
			return v;
		}

		//
		// Summary:
		//     Subtracts the specified vector from another specified vector.
		//
		// Parameters:
		//   vector2:
		//     The vector to subtract from vector1.
		//
		//   vector1:
		//     The vector from which vector2 is subtracted.
		//
		// Returns:
		//     The difference between vector1 and vector2.
		public static Vector Subtract(Vector vector1, Vector vector2)
		{
			return new Vector(vector1.X - vector2.X, vector1.Y - vector2.Y);
		}

		public Point ToPoint()
		{
			return new Point(X, Y);
		}

		//
		// Summary:
		//     Returns the string representation of this Vector structure.
		//
		// Returns:
		//     A string that represents the Vector.X and Vector.Y
		//     values of this Vector.
		public override string ToString()
		{
			return X.ToString(CultureInfo.InvariantCulture) + "," + Y.ToString(CultureInfo.InvariantCulture);
		}
		//
		// Summary:
		//     Returns the string representation of this Vector structure
		//     with the specified formatting information.
		//
		// Parameters:
		//   provider:
		//     The culture-specific formatting information.
		//
		// Returns:
		//     A string that represents the Vector.X and Vector.Y
		//     values of this Vector.
		public string ToString(IFormatProvider provider)
		{
			throw new NotImplementedException("not implemented yet");
		}
	}
}