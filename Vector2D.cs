namespace Geometry
{
    public class Vector2D
    {
        public static Vector2D XAxis { get; } = new Vector2D(1, 0);
        public static Vector2D YAxis { get; } = new Vector2D(0, 1);

        public double X, Y;
        public bool IsValid => Length() > 0;

        public Vector2D() { }
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Vector2D(Point2D p0, Point2D p1) : this(p1.X - p0.X, p1.Y - p0.Y) { }
        public Vector2D(Vector2D another) : this(another.X, another.Y) { }
        public Vector2D(Segment2D segment) : this(segment.P1.X - segment.P0.X, segment.P1.Y - segment.P0.Y) { }

        /// <summary>
        /// Normalizes the current vector
        /// </summary>
        public virtual void Normalize()
        {
            double originalLength = Length();
            X /= originalLength;
            Y /= originalLength;
        }

        /// <summary>
        /// Computes the length of the vector
        /// </summary>
        public virtual double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        /// <summary>
        /// Negates the vector
        /// </summary>
        public virtual void Negate()
        {
            X *= -1;
            Y *= -1;
        }

        /// <summary>
        /// Check if the given vectors are orthogonal
        /// </summary>
        /// <param name="u">First vector</param>
        /// <param name="v">Second vector</param>
        /// <returns>True if the vectors are orthogonal, false otherwise</returns>

        public static bool AreOrthogonal(Vector2D u, Vector2D v)
        {
            return Dot(u, v) == 0;
        }

        /// <summary>
        /// Check if the given vectors are parallel
        /// </summary>
        /// <param name="u">First vector</param>
        /// <param name="v">Second vector</param>
        /// <returns>true if the vectors are parallel, false otherwise</returns>

        public static bool AreParallel(Vector2D u, Vector2D v)
        {
            return u.Y / u.X == v.Y / v.X;
        }

        /// <summary>
        /// Computes the dot product of the given vectors
        /// </summary>
        /// <param name="u">First vector</param>
        /// <param name="v">Second vector</param>
        /// <returns>The result of the dot product</returns>
        public static double Dot(Vector2D u, Vector2D v)
        {
            return u.X * v.X + u.Y * v.Y;
        }

        #region OVERRIDES
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            Vector2D compare = obj as Vector2D;

            return X == compare.X && Y == compare.Y;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = 23 * hash * X.GetHashCode();
            hash = 23 * hash * Y.GetHashCode();
            return hash;
        }

        public static Vector2D operator +(Vector2D u, Vector2D v)
        {
            return new Vector2D(u.X + v.X, u.Y + v.Y);
        }

        public static Vector2D operator -(Vector2D u, Vector2D v)
        {
            return new Vector2D(u.X - v.X, u.Y - v.Y);
        }

        public static double operator *(Vector2D u, Vector2D v)
        {
            return Dot(u, v);
        }

        public static Vector2D operator *(Vector2D v, double s)
        {
            return new Vector2D(s * v.X, s * v.Y);
        }

        public static Vector2D operator *(double s, Vector2D v)
        {
            return v * s;
        }

        public static Vector2D operator /(Vector2D v, double s)
        {
            return new Vector2D(v.X / s, v.Y / s);
        }

        public static Vector2D operator /(double s, Vector2D v)
        {
            return v / s;
        }
        #endregion
    }
}
