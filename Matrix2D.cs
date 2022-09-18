using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    //|A B|
    //|C D|
    public class Matrix2D 
    {
        public double A, B, C, D;
        public static Matrix2D Identity { get; } = new Matrix2D(1,0,0,1);
        public static Matrix2D Zero { get; } = new Matrix2D(0, 0, 0, 0);


        public Matrix2D(double a, double b, double c, double d)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
        }

        /// <summary>
        /// Computes the determinant of this matrix
        /// </summary>
        /// <returns>The value of the determinant</returns>
        public double Determinant()
        {
            return A * D - B * C;
        }

        /// <summary>
        /// Copy the values of the given matrix on this one
        /// </summary>
        /// <param name="matrix">The matrix with the new values</param>

        public void Copy(Matrix2D matrix)
        {
            this.A = matrix.A; 
            this.B = matrix.B; 
            this.C = matrix.C;
            this.D = matrix.D;
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
        /// <returns>The inverse of this matrix</returns>
        /// <exception cref="Exception">If the given matrix can´t be inverted</exception>
        public Matrix2D Inverse()
        {
            if (Determinant() == 0)
                throw new Exception("This matrix is singular and can´t be inverted");

            return 1.0 / Determinant() * new Matrix2D(this.D,-this.B,-this.C,this.A);
        }


        /// <summary>
        /// Computes the transpose of this matrix
        /// </summary>
        /// <returns>The transpose of this matrix</returns>
        public Matrix2D Transpose()
        {
            return new Matrix2D(this.A, this.C, this.B, this.D);
        }


        /// <summary>
        /// Computes the multiplication of two matrices
        /// </summary>
        /// <param name="matA">The first matrix</param>
        /// <param name="matB">The second matrix</param>
        /// <returns>The resulting matrix</returns>
        public static Matrix2D operator *(Matrix2D matA, Matrix2D matB)
        {
            double a = matA.A * matB.A + matA.B * matB.C;
            double b = matA.A * matB.B + matA.B * matB.D;
            double c = matA.C * matB.A + matA.D * matB.C;
            double d = matA.C * matB.B + matA.D * matB.D;

            return new Matrix2D(a, b, c, d);
        }

        /// <summary>
        /// Computes the addition of two matrices
        /// </summary>
        /// <param name="matA">The first matrix</param>
        /// <param name="matB">The second matrix</param>
        /// <returns>The resulting matrix</returns>
        public static Matrix2D operator +(Matrix2D matA, Matrix2D matB)
        {
            double a = matA.A + matB.A;
            double b = matA.B + matB.B;
            double c = matA.C + matB.C;
            double d = matA.D + matB.D;

            return new Matrix2D(a, b, c, d);
        }

        /// <summary>
        /// Computes the difference between two matrices
        /// </summary>
        /// <param name="matA">The first matrix</param>
        /// <param name="matB">The second matrix</param>
        /// <returns>The resulting matrix</returns>
        public static Matrix2D operator -(Matrix2D matA, Matrix2D matB)
        {
            double a = matA.A - matB.A;
            double b = matA.C - matB.C;
            double c = matA.B - matB.B;
            double d = matA.D - matB.D;

            return new Matrix2D(a, b, c, d);
        }

        /// <summary>
        /// Computes the multiplication of a matrix and a scalar 
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <param name="scalar">The scalar</param>
        /// <returns>The scaled matrix</returns>
        public static Matrix2D operator *(Matrix2D mat, double scalar)
        {
            double a = mat.A * scalar;
            double b = mat.B * scalar;
            double c = mat.C * scalar;
            double d = mat.D * scalar;

            return new Matrix2D(a,b,c,d);
        }

        /// <summary>
        /// Computes the multiplication of a matrix and a scalar 
        /// </summary>
        /// <param name="mat">The matrix</param>
        /// <param name="scalar">The scalar</param>
        /// <returns>The scaled matrix</returns>
        public static Matrix2D operator *(double scalar, Matrix2D mat)
        {
            return mat * scalar;
        }


    }
}
