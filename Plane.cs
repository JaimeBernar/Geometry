namespace Geometry
{
    public class Plane
    {
        public static Plane XY { get; } = new Plane(new Vector3D(0, 0, 1));
        public static Plane YX { get; } = new Plane(new Vector3D(0, 0, -1));
        public static Plane YZ { get; } = new Plane(new Vector3D(1, 0, 0));
        public static Plane XZ { get; } = new Plane(new Vector3D(0, -1, 0));
        public static Plane ZX { get; } = new Plane(new Vector3D(0, 1, 1));
        public static Plane ZY { get; } = new Plane(new Vector3D(-1, 0, 0));

        public Point3D Origin { get; set; }
        public Vector3D AxisX { get; private set; }
        public Vector3D AxisY { get; private set; }
        public PlaneEquation Equation { get; }


        #region CONSTRUCTORS
        public Plane(Vector3D N)
        {
            Origin = new Point3D();
            Equation = new PlaneEquation(new Point3D(), N);
            Regen();
        }
        public Plane(Point3D P, Vector3D N)
        {
            Origin = P;
            Equation = new PlaneEquation(P, N);
            Regen();
        }

        public Plane(Point3D P, Vector3D X, Vector3D Y)
        {
            Origin = P;
            Equation = new PlaneEquation(P, Vector3D.Cross(X, Y));
            Regen();
        }

        public Plane(Point3D P, Point3D Q, Point3D R)
        {
            Origin = new Point3D();
            Vector3D vect1 = new Vector3D(P, Q);
            Vector3D vect2 = new Vector3D(P, R);
            Vector3D normal = Vector3D.Cross(vect1, vect2);
            Equation = new PlaneEquation(new Point3D(), normal);
            Regen();
        }

        public Plane(Plane another) : this(another.Origin, another.Equation) { }
        #endregion

        /// <summary>
        /// Computes the intersection between two planes
        /// </summary>
        /// <param name="plane1">First plane</param>
        /// <param name="plane2">Second planet</param>
        /// <param name="segment">The resulting segment if the operation succeeded</param>
        /// <returns>True is the operation succeeded, false otherwise</returns>
        public static bool Intersection(Plane plane1, Plane plane2, out Segment3D segment)
        {
            if (Vector3D.AreParallel(plane1.Equation, plane2.Equation))
            {
                segment = null;
                return false;
            }
            else
            {
                //Choose base plane 
                if (plane1.Equation == new Vector3D(0, 0, 1) || plane2.Equation == new Vector3D(0, 0, 1)) //Choose Y
                {

                }
                else if (plane1.Equation == new Vector3D(0, 1, 0) || plane2.Equation == new Vector3D(0, 1, 0)) //Choose 
                {

                }
                else if (plane1.Equation == new Vector3D(0, 0, 1) || plane2.Equation == new Vector3D(0, 0, 1))
                {

                }
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Computes the distance from this plane to a 3D point
        /// </summary>
        /// <param name="pt">The 3D point</param>
        /// <returns>The distance</returns>
        public double DistanceTo(Point3D pt)
        {
            double numerator = (Equation.X * pt.X + Equation.Y * pt.Y + Equation.Z * pt.Z + Equation.D);
            double denominator = Math.Sqrt(Equation.X * Equation.X + Equation.Y * Equation.Y + Equation.Z * Equation.Z);
            return numerator / denominator;
        }

        /// <summary>
        /// Rotates the plane by a given angle from a given axis that cross the origin
        /// </summary>
        /// <param name="angleInRadians">The angle of rotation in radians</param>
        /// <param name="axis">The axis of rotation</param>
        public void Rotate(double angleInRadians, Vector3D axis)
        {
            Rotate(angleInRadians, axis, new Point3D());
        }

        /// <summary>
        /// Rotates the plane by a given angle from a given axis
        /// </summary>
        /// <param name="angleInRadians">The angle of rotation in radians</param>
        /// <param name="axis">The axis of rotation</param>
        public void Rotate(double angleInRadians, Vector3D axis, Point3D center)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Translates the plane by a given distance
        /// </summary>
        /// <param name="dx">Distance in X direction</param>
        /// <param name="dy">Distance in Y direction</param>
        /// <param name="dz">Distance in Z direction</param>
        public void Translate(double dx, double dy, double dz = 0)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Translates the plane by a given distance
        /// </summary>
        /// <param name="delta">The distance</param>
        public void Translate(Vector3D delta)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Translates the plane to a given point
        /// </summary>
        /// <param name="pt"></param>
        public void TranslateTo(Point3D pt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Regens the plane recalculating the orthogonal axes
        /// </summary>
        private void Regen()
        {
            Vector3D temporal = new Vector3D(0, 0, 1);
            if (Vector3D.AreParallel(temporal, Equation))
            {
                temporal = new Vector3D(1, 0, 0);
                AxisX = Vector3D.Cross(Equation, temporal);
                AxisY = Vector3D.Cross(Equation, AxisX);
            }
            else
            {
                AxisX = Vector3D.Cross(temporal, Equation);
                AxisY = Vector3D.Cross(Equation, AxisX);
            }
            AxisX.Normalize();
            AxisY.Normalize();
        }

        /// <summary>
        /// Flips the plane recalculating the equation and the orthogonal axes
        /// </summary>
        public void Flip()
        {
            Equation.Negate();
            Regen();
        }


        public Vector2D Project(Vector3D P)
        {
            double x = AxisX * P;
            double y = AxisY * P;
            return new Vector2D(x, y);
        }

        public Point2D Project(Point3D P)
        {
            double x = AxisX * new Vector3D(Origin, P);
            double y = AxisY * new Vector3D(Origin, P);
            return new Point2D(x, y);
        }



        public override string ToString()
        {
            return $"Origin = {Origin} | Normal = {Equation}";
        }


        public static implicit operator Plane(devDept.Geometry.Plane d)
        {
            return new Plane(d.Origin, d.Equation);
        }

        public static implicit operator devDept.Geometry.Plane(Plane d)
        {
            return new devDept.Geometry.Plane(d.Origin, new devDept.Geometry.Vector3D(d.Equation.X, d.Equation.Y, d.Equation.Z));
        }
    }
}
