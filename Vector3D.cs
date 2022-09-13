namespace Geometry
{
    public class Vector3D : Vector2D
    {
        public static new Vector3D XAxis { get; } = new Vector3D(1, 0, 0);
        public static new Vector3D YAxis { get; } = new Vector3D(0, 1, 0);
        public static Vector3D ZAxis { get; } = new Vector3D(0, 0, 1);

        public double Z;

        public Vector3D() { }
        public Vector3D(double x, double y, double z) : base(x, y) { Z = z; }
        public Vector3D(Point3D p0, Point3D p1) : this(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z) { }
        public Vector3D(Segment3D segment) : this(segment.P1.X - segment.P0.X, segment.P1.Y - segment.P0.Y, segment.P1.Z - segment.P0.Z) { }
        public Vector3D(Vector3D another) : this(another.X, another.Y, another.Z) { }

        /// <summary>
        /// Normalizes the current vector
        /// </summary>
        public override void Normalize()
        {
            double originalLength = Length();
            X /= originalLength;
            Y /= originalLength;
            Z /= originalLength;
        }

        /// <summary>
        /// Computes the length of the vector
        /// </summary>
        public override double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        /// <summary>
        /// Negates the vector
        /// </summary>
        public override void Negate()
        {
            X *= -1;
            Y *= -1;
            Z *= -1;
        }

        /// <summary>
        /// Check if the given vectors are orthogonal
        /// </summary>
        /// <param name="u">First vector</param>
        /// <param name="v">Second vector</param>
        /// <returns>True if the vectors are orthogonal, false otherwise</returns>
        public static bool AreOrthogonal(Vector3D u, Vector3D v)
        {
            return Dot(u, v) == 0;
        }

        /// <summary>
        /// Check if the given vectors are parallel
        /// </summary>
        /// <param name="u">First vector</param>
        /// <param name="v">Second vector</param>
        /// <returns>true if the vectors are parallel, false otherwise</returns>
        public static bool AreParallel(Vector3D u, Vector3D v)
        {
            return !Cross(u, v).IsValid;
        }

        /// <summary>
        /// Computes the dot product of the given vectors
        /// </summary>
        /// <param name="u">First vector</param>
        /// <param name="v">Second vector</param>
        /// <returns>The result of the dot product</returns>
        public static double Dot(Vector3D u, Vector3D v)
        {
            return u.X * v.X + u.Y * v.Y + u.Z * v.Z;
        }

        /// <summary>
        /// Computes the cross product of the given vectors
        /// </summary>
        /// <param name="u">First vector</param>
        /// <param name="v">Second vector</param>
        /// <returns>The resulting vector</returns>
        public static Vector3D Cross(Vector3D u, Vector3D v)
        {
            return new Vector3D(u.Y * v.Z - u.Z * v.Y, u.Z * v.X - u.X * v.Z, u.X * v.Y - u.Y * v.X);
        }

        /// <summary>
        /// Computes the cross product of the given vectors
        /// </summary>
        /// <param name="u">First vector</param>
        /// <param name="v">Second vector</param>
        /// <returns>The resulting vector already normalized</returns>
        public static Vector3D CrossNormalized(Vector3D u, Vector3D v)
        {
            Vector3D vect = Cross(u, v);
            vect.Normalize();
            return vect;
        }

        #region OVERRIDES
        public override string ToString()
        {
            return $"{X},{Y},{Z} | L={Length()}";
        }
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            Vector3D? compare = obj as Vector3D;
                        
            return X == compare.X
                   && Y == compare.Y
                   && Z == compare.Z;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = 23 * hash * X.GetHashCode();
            hash = 23 * hash * Y.GetHashCode();
            hash = 23 * hash * Z.GetHashCode();
            return hash;
        }

        /// <summary>
        /// Performs a vector addition operation
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>The resulting vector</returns>
        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        /// <summary>
        /// Perfoms a vector substraction operation
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>The resulting vector</returns>
        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        /// <summary>
        /// Performs a dot product between two vectors
        /// </summary>
        /// <param name="u">First vector</param>
        /// <param name="v">Second vector</param>
        /// <returns></returns>
        public static double operator *(Vector3D u, Vector3D v)
        {
            return Dot(u, v);
        }

        /// <summary>
        /// Performs a scalar * vector multiplication operation
        /// </summary>
        /// <param name="v">The vector</param>
        /// <param name="s">The scalar factor</param>
        /// <returns>The resulting vector scaled by the factor</returns>
        public static Vector3D operator *(Vector3D v, double s)
        {
            return new Vector3D(s * v.X, s * v.Y, s * v.Z);
        }

        /// <summary>
        /// Performs a scalar * vector multiplication operation
        /// </summary>
        /// <param name="v">The vector</param>
        /// <param name="s">The scalar factor</param>
        /// <returns>The resulting vector scaled by the factor</returns>
        public static Vector3D operator *(double s, Vector3D v)
        {
            return v * s;
        }

        /// <summary>
        /// Performs a division of vectors
        /// </summary>
        /// <param name="v">The first vector</param>
        /// <param name="s">The second vector</param>
        /// <returns>The resulting vector</returns>
        public static Vector3D operator /(Vector3D v, double s)
        {
            return new Vector3D(v.X / s, v.Y / s, v.Z / s);
        }

        /// <summary>
        /// Performs a division of vectors
        /// </summary>
        /// <param name="v">The first vector</param>
        /// <param name="s">The second vector</param>
        /// <returns>The resulting vector</returns>
        public static Vector3D operator /(double s, Vector3D v)
        {
            return v / s;
        }

        #endregion

    }
}
