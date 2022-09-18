using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Vector4D : Vector3D
    {
        public double W;

        public Vector4D(double x, double y, double z, double w) : base(x,y,z)
        {
            this.W = w;
        }
        public Vector4D(Vector4D another) : this(another.X,another.Y,another.Z,another.W) { }

        #region OVERRIDES
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            Vector4D compare = obj as Vector4D;

            return X == compare.X && Y == compare.Y && Z == compare.Z && W == compare.Z;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = 23 * hash * X.GetHashCode();
            hash = 23 * hash * Y.GetHashCode();
            hash = 23 * hash * Z.GetHashCode();
            hash = 23 * hash * W.GetHashCode();
            return hash;
        }

        #endregion

        #region OPERATORS
        /// <summary>
        /// Computes the addition of two vectors
        /// </summary>
        /// <param name="u">First vector</param>
        /// <param name="v">Second vector</param>
        /// <returns>The resulting vector</returns>
        public static Vector4D operator +(Vector4D u, Vector4D v)
        {
            return new Vector4D(u.X + v.X, u.Y + v.Y, u.Z + v.Z, 1.0);
        }

        /// <summary>
        /// Computes the substraction of two vectors
        /// </summary>
        /// <param name="u">First vector</param>
        /// <param name="v">Second vector</param>
        /// <returns>Thre resulting vector</returns>
        public static Vector4D operator -(Vector4D u, Vector4D v)
        {
            return new Vector4D(u.X - v.X, u.Y - v.Y, u.Z - v.Z, 1.0);
        }

        /// <summary>
        /// Computes the dot product between two vectors
        /// </summary>
        /// <param name="u">First vector</param>
        /// <param name="v">Second vector</param>
        /// <returns>The dot product result</returns>
        public static double operator *(Vector4D u, Vector4D v)
        {
            return Dot(u, v);
        }

        /// <summary>
        /// Computes the multiplication between a vector and a scalar
        /// </summary>
        /// <param name="v">The vector</param>
        /// <param name="s">The scalar</param>
        /// <returns>The vector scaled</returns>
        public static Vector4D operator *(Vector4D v, double s)
        {
            return new Vector4D(s * v.X, s * v.Y, s * v.Z, 1.0);
        }

        /// <summary>
        /// Computes the multiplication between a vector and a scalar
        /// </summary>
        /// <param name="v">The vector</param>
        /// <param name="s">The scalar</param>
        /// <returns>The vector scaled</returns>
        public static Vector4D operator *(double s, Vector4D v)
        {
            return v * s;
        }

        /// <summary>
        /// Computes the division between a vector and a scalar
        /// </summary>
        /// <param name="v">The vector</param>
        /// <param name="s">The scalar</param>
        /// <returns>The vector scaled</returns>
        public static Vector4D operator /(Vector4D v, double s)
        {
            return (1.0 / s) * v;
        }

        /// <summary>
        /// Computes the division between a vector and a scalar
        /// </summary>
        /// <param name="v">The vector</param>
        /// <param name="s">The scalar</param>
        /// <returns>The vector scaled</returns>
        public static Vector4D operator /(double s, Vector4D v)
        {
            return v / s;
        }
        #endregion
    }
}
