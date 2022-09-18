using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    //|A B C|
    //|D E F|
    //|G H I|
    public class Matrix3D 
    {
        public static Matrix3D Identity { get; } = new Matrix3D(1,0,0,0,1,0,0,0,1);
        public static Matrix3D Zero { get; } = new Matrix3D(0, 0, 0, 0, 0, 0, 0, 0, 0);


        public Matrix3D(double a, double b, double c, double d, double e, double f, double g, double h, double i)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
            this.E = e;
            this.F = f;
            this.G = g;
            this.H = h;
            this.I = i;
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
        public void Copy(Matrix3D matrix)
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
        public  Matrix3D Inverse()
        {
            if (Determinant() == 0)
                throw new Exception("This matrix is singular and can´t be inverted");
                        
            return 1.0 / Determinant() * this.Transpose();
        }

        /// <summary>
        /// Computes the transpose of this matrix
        /// </summary>
        public Matrix3D Transpose()
        {
            double b = this.D;
            double c = this.G;
            double d = this.B;
            double f = this.H;
            double g = this.C;
            double h = this.F;

            return new Matrix3D(this.A, b, c, d, this.E, f, g, h, this.I);
        }

        /// <summary>
        /// Creates a Rotation Matrix around X Axis
        /// </summary>
        /// <param name="angleInRadians">The angle of rotation</param>
        /// <returns>The matrix of rotation</returns>
        public static Matrix3D CreateRotationX(double angleInRadians)
        {
            double sin = Math.Sin(angleInRadians);
            double cos = Math.Cos(angleInRadians);
            return new Matrix3D(1,0,0,0,cos,-sin,0,sin,cos);
        }

        /// <summary>
        /// Creates a Rotation Matrix around Y Axis
        /// </summary>
        /// <param name="angleInRadians">The angle of rotation</param>
        /// <returns>The matrix of rotation</returns>
        public static Matrix3D CreateRotationY(double angleInRadians)
        {
            double sin = Math.Sin(angleInRadians);
            double cos = Math.Cos(angleInRadians);
            return new Matrix3D(cos,0,sin,0,1,0,-sin,0,cos);
        }

        /// <summary>
        /// Creates a Rotation Matrix around Z Axis
        /// </summary>
        /// <param name="angleInRadians">The angle of rotation</param>
        /// <returns>The matrix of rotation</returns>
        public static Matrix3D CreateRotationZ(double angleInRadians)
        {
            double sin = Math.Sin(angleInRadians);
            double cos = Math.Cos(angleInRadians);
            return new Matrix3D(cos,-sin,0,sin,cos,0,0,0,1);
        }

        /// <summary>
        /// Creates a rotation matrix around the specified angle
        /// </summary>
        /// <param name="angle">The angle of rotation</param>
        /// <param name="axis">The axis of rotation</param>
        /// <returns>The matrix of rotation</returns>
        public static Matrix3D CreateRotationFromAxisAngle(double angle, Vector3D axis)
        {
            axis.Normalize();
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            double a = cos + axis.X * axis.X * (1.0 - cos);
            double b = axis.X * axis.Y * (1.0 - cos) - axis.Z * sin;
            double c = axis.X * axis.Z * (1.0 - cos) + axis.Y * sin;
            double d = axis.Y * axis.X * (1.0 - cos) + axis.Z * sin;
            double e = cos + axis.Y * axis.Y * (1 - cos);
            double f = axis.Y * axis.Z * (1.0 - cos) - axis.X * sin;
            double g = axis.Z * axis.X * (1.0 - cos) - axis.Y * sin; 
            double h = axis.Z * axis.Y * (1.0 - cos) + axis.X * sin; ;
            double i = cos + axis.Z * axis.Z * (1.0 - cos);

            return new Matrix3D(a, b, c, d, e, f, g, h, i);
        }

        public static Matrix3D CreateScale(double sx, double sy)
        {
            return new Matrix3D(sx,0,0,0,sy,0,0,0,1);
        }

        /// <summary>
        /// Creates a translation matrix
        /// </summary>
        /// <param name="x">The translation along the X Axis</param>
        /// <param name="y">The translation along the Y Axis</param>
        /// <returns>The computes translation matrix</returns>
        public static Matrix3D CreateTranslation(double x, double y)
        {
            return new Matrix3D(1,0,x,0,1,y,0,0,1);
        }

        /// <summary>
        /// Creates a shear matrix
        /// </summary>
        /// <param name="shearAngle"></param>
        /// <returns>The shear matrix</returns>
        public static Matrix3D CreateShearX(double shearAngle)
        {
            return new Matrix3D(1,Math.Tan(shearAngle),0,0,1,0,0,0,1);
        }

        /// <summary>
        /// Creates a shear matrix
        /// </summary>
        /// <param name="shearAngle"></param>
        /// <returns>The shear matrix</returns>
        public static Matrix3D CreateShearY(double shearAngle)
        {
            return new Matrix3D(1,0,0,Math.Tan(shearAngle),1,0,0,0,1);
        }

        /// <summary>
        /// Creates a reflect matrix about origin
        /// </summary>
        /// <returns>The reflect matrix</returns>
        public static Matrix3D CreateReflectAboutOrigin()
        {
            return new Matrix3D(-1,0,0,0,-1,0,0,0,1);
        }

        /// <summary>
        /// Creates a reflect matrix about x axis
        /// </summary>
        /// <returns>The reflect matrix</returns>
        public static Matrix3D CreateReflectAboutXAxis()
        {
            return new Matrix3D(1,0,0,0,-1,0,0,0,1);
        }

        /// <summary>
        /// Creates a reflect matrix about y axis
        /// </summary>
        /// <returns></returns>
        public static Matrix3D CreateReflectAboutYAxis()
        {
            return new Matrix3D(-1,0,0,0,1,0,0,0,1);
        }

        /// <summary>
        /// Computes the multiplication of two matrices
        /// </summary>
        /// <param name="matA">The first matrix</param>
        /// <param name="matB">The second matrix</param>
        /// <returns>The resulting matrix</returns>
        public static Matrix3D operator *(Matrix3D matA, Matrix3D matB)
        {
            double a = matA.A * matB.A + matA.B * matB.D + matA.C * matB.G;
            double b = matA.A * matB.B + matA.B * matB.E + matA.C * matB.H;
            double c = matA.A * matB.C + matA.B * matB.F + matA.C * matB.I;
            
            double d = matA.D * matB.A + matA.E * matB.D + matA.F * matB.G;
            double e = matA.D * matB.B + matA.E * matB.E + matA.F * matB.H;
            double f = matA.D * matB.C + matA.E * matB.F + matA.F * matB.I;
            
            double g = matA.G * matB.A + matA.H * matB.D + matA.I * matB.G;
            double h = matA.G * matB.B + matA.H * matB.E + matA.I * matB.H;
            double i = matA.G * matB.C + matA.H * matB.F + matA.I * matB.I;


            return new Matrix3D(a, b, c, d, e, f, g, h, i);
        }

        /// <summary>
        /// Computes the addition of two matrices
        /// </summary>
        /// <param name="matA">The first matrix</param>
        /// <param name="matB">The second matrix</param>
        /// <returns>The resulting matrix</returns>
        public static Matrix3D operator +(Matrix3D matA, Matrix3D matB)
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

            return new Matrix3D(a, b, c, d, e, f ,g ,h ,i);
        }

        public static Matrix3D operator -(Matrix3D matA, Matrix3D matB)
        {
            double a = matA.A - matB.A;
            double b = matA.B - matB.B;
            double c = matA.C - matB.C;
            double d = matA.D - matB.D;
            double e = matA.E - matB.E;
            double f = matA.F - matB.F;
            double g = matA.G - matB.G;
            double h = matA.H - matB.H;
            double i = matA.I - matB.I;

            return new Matrix3D(a, b, c, d, e, f, g, h, i);
        }

        public static Matrix3D operator *(Matrix3D mat, double scalar)
        {
            double a = mat.A * scalar;
            double b = mat.B * scalar;
            double c = mat.C * scalar;
            double d = mat.D * scalar;
            double e = mat.E * scalar;
            double f = mat.F * scalar;
            double g = mat.G * scalar;
            double h = mat.H * scalar;
            double i = mat.I * scalar;

            return new Matrix3D(a, b, c, d, e, f, g, h, i);
        }

        public static Matrix3D operator *(double scalar, Matrix3D mat)
        {
            return mat * scalar;
        }

        public static Vector3D operator *(Vector3D vector, Matrix3D matrix)
        {
            double x = vector.X * matrix.A + vector.Y * matrix.B + vector.Z * matrix.C;
            double y = vector.X * matrix.D + vector.Y * matrix.E + vector.Z * matrix.F;
            double z = vector.X * matrix.G + vector.Y * matrix.H + vector.Z * matrix.I;

            return new Vector3D(x, y, z);
        }

        public static Vector3D operator *(Matrix3D matrix, Vector3D vector)
        {
            return vector * matrix;
        }

        public double A, B, C, D, E, F, G, H, I;
    }
}
