namespace Geometry
{
    public class Point2D
    {
        public double X, Y;

        public Point2D() { }
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Point2D(Point2D another) : this(another.X, another.Y) { }

        /// <summary>
        /// Computes the Euclidian distance between two points
        /// </summary>
        /// <param name="b">The second point</param>
        /// <returns>The Euclidian distance</returns>
        public double DistanceTo(Point2D b)
        {
            return Math.Sqrt((X - b.X) * (X - b.X) + (Y - b.Y) * (Y - b.Y));
        }

        /// <summary>
        /// Computes Euclidian distance between two points
        /// </summary>
        /// <param name="pointA">Point A</param>
        /// <param name="pointB">Point B</param>
        /// <returns>The Euclidian distance</returns>
        public static double DistanceBetween(Point2D pointA, Point2D pointB)
        {
            return pointA.DistanceTo(pointB);
        }

        #region OVERRIDES
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            Point2D compare = obj as Point2D;

            return X == compare.X && Y == compare.Y;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = 23 * hash * X.GetHashCode();
            hash = 23 * hash * Y.GetHashCode();
            return hash;
        }
        #endregion

        #region OPERATORS
        public static Point2D operator +(Point2D p, Vector2D v)
        {
            return new Point2D(p.X + v.X, p.Y + v.Y);
        }

        public static Point2D operator +(Point2D a, Point2D b)
        {
            return new Point2D(a.X + b.X, a.Y + b.Y);
        }

        public static Point2D operator +(Vector2D v, Point2D p)
        {
            return new Point2D(p.X + v.X, p.Y + v.Y);
        }

        public static Point2D operator -(Vector2D v, Point2D p)
        {
            return new Point2D(p.X - v.X, p.Y - v.Y);
        }

        public static Point2D operator -(Point2D p, Vector2D v)
        {
            return new Point2D(p.X - v.X, p.Y - v.Y);
        }

        public static Point2D operator -(Point2D a, Point2D b)
        {
            return new Point2D(a.X - b.X, a.Y - b.Y);
        }

        public static Point2D operator *(Point2D p, double s)
        {
            return new Point2D(s * p.X, s * p.Y);
        }

        public static Point2D operator *(double s, Point2D p)
        {
            return new Point2D(s * p.X, s * p.Y);
        }

        public static Point2D operator /(Point2D p, double s)
        {
            return new Point2D(p.X / s, p.Y / s);
        }
        #endregion

    }
}
