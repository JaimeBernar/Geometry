using System.Runtime.CompilerServices;

namespace Geometry
{
    public class Point3D : Point2D
    {
        public double Z;
        public Point3D() { }
        public Point3D(Point3D another) : this(another.X, another.Y, another.Z) { }
        public Point3D(Point2D another) : this(another.X, another.Y, 0) { }
        public Point3D(double x, double y) : base(x, y) { }
        public Point3D(double x, double y, double z) : base(x, y) { Z = z; }

        /// <summary>
        /// Computes minimum distance from this point to a 3D segment
        /// </summary>
        /// <param name="segment">the 3D Segment</param>
        /// <returns>The distance</returns>
        public double DistanceTo(Segment3D segment)
        {
            return DistanceTo(ProjectTo(segment));
        }

        /// <summary>
        /// Computes minimum distance from this point to a plane
        /// </summary>
        /// <param name="plane">the plane</param>
        /// <returns>The distance</returns>
        public double DistanceTo(Plane plane)
        {
            //https://www.cuemath.com/geometry/distance-between-point-and-plane/
            double numerator = (plane.Equation.X * X + plane.Equation.Y * Y + plane.Equation.Z * Z + plane.Equation.D);
            double denominator = Math.Sqrt(plane.Equation.X * plane.Equation.X + plane.Equation.Y * plane.Equation.Y + plane.Equation.Z * plane.Equation.Z);
            return numerator / denominator;
        }

        /// <summary>
        /// Computes the Euclidian distance between two points
        /// </summary>
        /// <param name="segment">The 3D point</param>
        /// <returns>The Euclidian distance</returns>
        public double DistanceTo(Point3D b)
        {
            return Math.Sqrt((X - b.X) * (X - b.X) + (Y - b.Y) * (Y - b.Y) + (Z - b.Z) * (Z - b.Z));
        }

        /// <summary>
        /// Computes the Euclidian distance between two points
        /// </summary>
        /// <param name="pointA">Point A</param>
        /// <param name="pointB">Point B</param>
        /// <returns>The Euclidian distance</returns>
        public static double DistanceBetween(Point3D pointA, Point3D pointB)
        {
            return pointA.DistanceTo(pointB);
        }

        /// <summary>
        /// Projects this point to a 3D segment
        /// </summary>
        /// <param name="segment">The 3D segment used for the projection</param>
        /// <returns>the projected point</returns>
        public Point3D ProjectTo(Segment3D segment)
        {
            Vector3D vect = new Vector3D(segment.P0, this);
            return segment.P0 + Vector3D.Dot(vect, segment.Direction) / Vector3D.Dot(segment.Direction, segment.Direction) * segment.Direction;
        }

        /// <summary>
        /// Projects this point to plane
        /// </summary>
        /// <param name="segment">The plane used for the projection</param>
        /// <returns>the projected point in the plane</returns>
        public Point3D ProjectTo(Plane plane)
        {
            Vector3D OP = new Vector3D(plane.Origin, this);
            double distance = Vector3D.Dot(OP, plane.Equation);
            return this - distance * plane.Equation;
        }

        #region OVERRIDES
        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            Point3D compare = obj as Point3D;
            return X == compare.X && Y == compare.Y && Z == compare.Z;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = 23 * hash * X.GetHashCode();
            hash = 23 * hash * Y.GetHashCode();
            hash = 23 * hash * Z.GetHashCode();
            return hash;
        }

        public static Point3D operator +(Point3D a, Point3D b)
        {
            return new Point3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Point3D operator -(Point3D a, Point3D b)
        {
            return new Point3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Point3D operator +(Vector3D v, Point3D p)
        {
            return new Point3D(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }

        public static Point3D operator +(Point3D p, Vector3D v)
        {
            return v + p;
        }

        public static Point3D operator -(Vector3D v, Point3D p)
        {
            return new Point3D(p.X - v.X, p.Y - v.Y, p.Z - v.Z);
        }

        public static Point3D operator -(Point3D p, Vector3D v)
        {
            return v - p;
        }

        public static Point3D operator *(Point3D p, double s)
        {
            return new Point3D(s * p.X, s * p.Y, s * p.Z);
        }

        public static Point3D operator *(double s, Point3D p)
        {
            return p * s;
        }

        public static Point3D operator /(Point3D p, double s)
        {
            return new Point3D(1 / s * p.X, 1 / s * p.Y, 1 / s * p.Z);
        }

        #endregion

    }
}
