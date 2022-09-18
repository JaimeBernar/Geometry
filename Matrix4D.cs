using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Matrix4D
    {
        public double A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P;
        public static Matrix4D Identity { get; } = new Matrix4D(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
        public static Matrix4D Zero { get; } = new Matrix4D(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        public Matrix4D(double a, double b, double c, double d, 
                        double e, double f, double g, double h, 
                        double i, double j, double k, double l,
                        double m, double n, double o ,double p)
        {
            this.A = a; this.B = b; this.C = c; this.D = d;
            this.E = e; this.F = f; this.G = g; this.H = h;
            this.I = i; this.J = j; this.K = k; this.L = l;
            this.M = m; this.N = n; this.O = o; this.P = p;
        }

        protected Matrix4D(Matrix3D matrix)
        {
            this.A = matrix.A;
            this.B = matrix.B;
            this.C = matrix.C;
            this.D = 0;
            this.E = matrix.D;
            this.F = matrix.E;
            this.G = matrix.F;
            this.H = 0;
            this.I = matrix.G;
            this.J = matrix.H;
            this.K = matrix.I;
            this.L = 0;
            this.M = 0;
            this.N = 0;
            this.O = 0;
            this.P = 1.0;
        }


        /// <summary>
        /// Computes the determinant of this matrix
        /// </summary>
        /// <returns>The value of the determinant</returns>
        public double Determinant()
        {
            double sum1 = A * E * I + B * F * G + C * H * D;
            double sum2 = -C * E * G - B * D * I - F * G * A;
            return sum1 + sum2;
        }

        /// <summary>
        /// Copy the values of the given matrix on this one
        /// </summary>
        /// <param name="matrix">The matrix with the new values</param>
        public void Copy(Matrix4D matrix)
        {
            this.A = matrix.A;
            this.B = matrix.B;
            this.C = matrix.C;
            this.D = matrix.D;
            this.E = matrix.E;
            this.F = matrix.F;
            this.G = matrix.G;
            this.H = matrix.H;
            this.I = matrix.I;
        }

        /// <summary>
        /// Inverts this matrix
        /// </summary>
        public void Invert()
        {
            this.Copy(this.Inverse());
        }

        /// <summary>
        /// Computes the inverse of this matrix
        /// </summary>
        public Matrix4D Inverse()
        {
            if (Determinant() == 0)
                throw new Exception("This matrix is singular and can´t be inverted");

            return 1.0 / Determinant() * this.Transpose();
        }

        /// <summary>
        /// Computes the transpose of this matrix
        /// </summary>
        public Matrix4D Transpose()
        {
            double a = this.A;
            double b = this.E;
            double c = this.I;
            double d = this.M;
            double e = this.B;
            double f = this.F;
            double g = this.J;
            double h = this.N;
            double i = this.C;
            double j = this.G;
            double k = this.K;
            double l = this.O;
            double m = this.D;
            double n = this.H;
            double o = this.L;
            double p = this.P;

            return new Matrix4D(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p);
        }

        /// <summary>
        /// Creates a Rotation Matrix around X Axis
        /// </summary>
        /// <param name="angleInRadians">The angle of rotation</param>
        /// <returns>The matrix of rotation</returns>
        public static Matrix4D CreateRotationX(double angleInRadians)
        {
            return new Matrix4D(Matrix3D.CreateRotationX(angleInRadians));
        }

        /// <summary>
        /// Creates a Rotation Matrix around Y Axis
        /// </summary>
        /// <param name="angleInRadians">The angle of rotation</param>
        /// <returns>The matrix of rotation</returns>
        public static Matrix4D CreateRotationY(double angleInRadians)
        {
            double sin = Math.Sin(angleInRadians);
            double cos = Math.Cos(angleInRadians);
            return new Matrix4D(Matrix3D.CreateRotationY(angleInRadians));
        }

        /// <summary>
        /// Creates a Rotation Matrix around Z Axis
        /// </summary>
        /// <param name="angleInRadians">The angle of rotation</param>
        /// <returns>The matrix of rotation</returns>
        public static Matrix4D CreateRotationZ(double angleInRadians)
        {
            return new Matrix4D(Matrix3D.CreateRotationZ(angleInRadians));
        }

        /// <summary>
        /// Creates a rotation matrix around the specified angle
        /// </summary>
        /// <param name="angle">The angle of rotation</param>
        /// <param name="axis">The axis of rotation</param>
        /// <returns>The matrix of rotation</returns>
        public static Matrix4D CreateRotationFromAxisAngle(double angle, Vector3D axis)
        {
            return new Matrix4D(Matrix3D.CreateRotationFromAxisAngle(angle,axis));
        }

        public static Matrix4D CreateScale(double sx, double sy, double sz)
        {
            return new Matrix4D(sx, 0, 0, 0, 0, sy, 0, 0, 0, 0, sz, 0, 0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a translation matrix
        /// </summary>
        /// <param name="x">The translation along the X Axis</param>
        /// <param name="y">The translation along the Y Axis</param>
        /// <returns>The computes translation matrix</returns>
        public static Matrix4D CreateTranslation(double x, double y, double z)
        {
            return new Matrix4D(1, 0, 0, x, 0, 1, 0, y, 0, 0, 1, z, 0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a shear matrix
        /// </summary>
        /// <param name="shearAngle"></param>
        /// <returns>The shear matrix</returns>
        public static Matrix4D CreateShearX(double shearAngle)
        {
            return new Matrix4D(Matrix3D.CreateShearX(shearAngle));
        }

        /// <summary>
        /// Creates a shear matrix
        /// </summary>
        /// <param name="shearAngle"></param>
        /// <returns>The shear matrix</returns>
        public static Matrix4D CreateShearY(double shearAngle)
        {
            return new Matrix4D(Matrix3D.CreateShearY(shearAngle));
        }


        /// <summary>
        /// Computes the multiplication of two matrices
        /// </summary>
        /// <param name="matA">The first matrix</param>
        /// <param name="matB">The second matrix</param>
        /// <returns>The resulting matrix</returns>
        public static Matrix4D operator *(Matrix4D matA, Matrix4D matB)
        {
            double a = matA.A * matB.A + matA.B * matB.E + matA.C * matB.I + matA.D * matB.M;
            double b = matA.A * matB.B + matA.B * matB.F + matA.C * matB.J + matA.D * matB.N;
            double c = matA.A * matB.C + matA.B * matB.G + matA.C * matB.K + matA.D * matB.O;                       
            double d = matA.A * matB.D + matA.B * matB.H + matA.C * matB.L + matA.D * matB.P;

            double e = matA.E * matB.A + matA.F * matB.E + matA.G * matB.I + matA.H * matB.M;
            double f = matA.E * matB.B + matA.F * matB.F + matA.G * matB.J + matA.H * matB.N;                     
            double g = matA.E * matB.C + matA.F * matB.G + matA.G * matB.K + matA.H * matB.O; 
            double h = matA.E * matB.D + matA.F * matB.H + matA.G * matB.L + matA.H * matB.P;

            double i = matA.I * matB.A + matA.J * matB.E + matA.K * matB.I + matA.L * matB.M;
            double j = matA.I * matB.B + matA.J * matB.F + matA.K * matB.J + matA.L * matB.N;
            double k = matA.I * matB.C + matA.J * matB.G + matA.K * matB.K + matA.L * matB.O; 
            double l = matA.I * matB.D + matA.J * matB.H + matA.K * matB.L + matA.L * matB.P;

            double m = matA.M * matB.A + matA.N * matB.E + matA.O * matB.I + matA.P * matB.M;
            double n = matA.M * matB.B + matA.N * matB.F + matA.O * matB.J + matA.P * matB.N;
            double o = matA.M * matB.C + matA.N * matB.G + matA.O * matB.K + matA.P * matB.O; 
            double p = matA.M * matB.D + matA.N * matB.H + matA.O * matB.L + matA.P * matB.P;

            return new Matrix4D(a, b, c, d, e, f, g, h, i, j,k,l,m,n,o,p);
        }

        /// <summary>
        /// Computes the addition of two matrices
        /// </summary>
        /// <param name="matA">The first matrix</param>
        /// <param name="matB">The second matrix</param>
        /// <returns>The resulting matrix</returns>
        public static Matrix4D operator +(Matrix4D matA, Matrix4D matB)
        {
            double a = matA.A + matB.A;
            double b = matA.B + matB.B;
            double c = matA.C + matB.C;
            double d = matA.D + matB.D;
            double e = matA.E + matB.E;
            double f = matA.F + matB.F;
            double g = matA.G + matB.G;
            double h = matA.H + matB.H;
            double i = matA.I + matB.I;
            double j = matA.J + matB.J;
            double k = matA.K + matB.K;
            double l = matA.L + matB.L;
            double m = matA.M + matB.M;
            double n = matA.N + matB.N;
            double o = matA.O + matB.O;
            double p = matA.P + matB.P;

            return new Matrix4D(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p);
        }

        public static Matrix4D operator -(Matrix4D matA, Matrix4D matB)
        {
            return matA + (-1) * matB;
        }

        public static Matrix4D operator *(Matrix4D mat, double scalar)
        {
            double a = mat.A + scalar;
            double b = mat.B + scalar;
            double c = mat.C + scalar;
            double d = mat.D + scalar;
            double e = mat.E + scalar;
            double f = mat.F + scalar;
            double g = mat.G + scalar;
            double h = mat.H + scalar;
            double i = mat.I + scalar;
            double j = mat.J + scalar;
            double k = mat.K + scalar;
            double l = mat.L + scalar;
            double m = mat.M + scalar;
            double n = mat.N + scalar;
            double o = mat.O + scalar;
            double p = mat.P + scalar;

            return new Matrix4D(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p);
        }

        public static Matrix4D operator *(double scalar, Matrix4D mat)
        {
            return mat * scalar;
        }

        public static Vector4D operator *(Vector4D vector, Matrix4D matrix)
        {
            double x = vector.X * matrix.A + vector.Y * matrix.B + vector.Z * matrix.C + vector.W * matrix.D;
            double y = vector.X * matrix.E + vector.Y * matrix.F + vector.Z * matrix.G + vector.W * matrix.H;
            double z = vector.X * matrix.I + vector.Y * matrix.J + vector.Z * matrix.K + vector.W * matrix.L;
            double w = vector.X * matrix.M + vector.Y * matrix.N + vector.Z * matrix.O + vector.W * matrix.P;

            return new Vector4D(x, y, z, w);
        }

        public static Vector4D operator *(Matrix4D matrix, Vector4D vector)
        {
            return vector * matrix;
        }
    }
}
