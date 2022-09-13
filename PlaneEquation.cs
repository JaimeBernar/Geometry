namespace Geometry
{
    public class PlaneEquation : Vector3D
    {
        public double D;
        public PlaneEquation(Point3D P, Vector3D N) : base(N)
        {
            Normalize();
            D = -(X * P.X + Y * P.Y + Z * P.Z);
            p = P;
        }
        public PlaneEquation(double a, double b, double c, double d) : base(a, b, c)
        {
            Normalize();
            D = d;
        }
        public PlaneEquation(PlaneEquation another) : this(another.X, another.Y, another.Z, another.D) { }


        public void UpdateEquation()
        {
            D = -(X * p.X + Y * p.Y + Z * p.Z);
        }

        public override void Negate()
        {
            base.Negate();
            UpdateEquation();
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z},{D} | L={Length()}";
        }

        private readonly Point3D p;
    }
}
