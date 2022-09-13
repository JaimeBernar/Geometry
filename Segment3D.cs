namespace Geometry
{
    public class Segment3D
    {
        public Point3D P0;
        public Point3D P1;

        public Point3D MidPoint => P0 + (P1 - P0) / 2.0;
        public bool IsPoint => P0 == P1;
        public Vector3D Direction => new Vector3D(P0, P1);


        #region CONSTRUCTORS
        public Segment3D() { }

        public Segment3D(Vector3D v1)
        {
            P0 = new Point3D();
            P1 = new Point3D(v1.X, v1.Y, v1.Z);
        }

        public Segment3D(Point3D p0, Point3D p1)
        {
            P0 = p0;
            P1 = p1;
        }

        public Segment3D(double x0, double y0, double z0, double x1, double y1, double z1)
        {
            P0 = new Point3D(x0, y0, z0);
            P1 = new Point3D(x1, y1, z1);
        }

        protected Segment3D(Segment3D another) : this(another.P0, another.P1) { }
        #endregion

        /// <summary>
        /// Computes the length of the segment
        /// </summary>
        public double Length()
        {
            return Math.Sqrt((P1.X - P0.X) * (P1.X - P0.X) + (P1.Y - P0.Y) * (P1.Y - P0.Y) + (P1.Z - P0.Z) * (P1.Z - P0.Z));
        }

        /// <summary>
        /// Computes the distance between two segments
        /// </summary>
        /// <param name="segA">First segment</param>
        /// <param name="segB">Second segment</param>
        /// <returns>The distance</returns>
        public static double DistanceBetween(Segment3D segA, Segment3D segB)
        {
            Vector3D vectA = new Vector3D(segA);
            Vector3D vectB = new Vector3D(segB);
            //https://es.wikipedia.org/wiki/Rectas_que_se_cruzan
            return new Vector3D(segA.P0, segB.P0) * (Vector3D.Cross(vectA, vectB) / Vector3D.Cross(vectA, vectB).Length());
        }

        /// <summary>
        /// Computes the intersection between two segments
        /// </summary>
        /// <param name="segA">First segment</param>
        /// <param name="segB">Second segment</param>
        /// <param name="infinite">if the segments must be considered infinite</param>
        /// <param name="pointOnA">Point of intersection on segment A</param>
        /// <param name="pointOnB">Point of intersection on segment B</param>
        /// <returns>True if the intersection succeeded, false otherwise</returns>
        public static bool Intersection(Segment3D segA, Segment3D segB, bool infinite, out Point3D pointOnA, out Point3D pointOnB)
        {
            throw new NotImplementedException();
            //Check if the segments are in the same plane, then compute the intersection
            //If infinite = false -> Check if the result points lays in a rectangular box that rounds the segment if not discard

            if (DistanceBetween(segA, segB) != 0)
            {
                pointOnA = null;
                pointOnB = null;
                return false;
            }
            else
            {
                pointOnA = null;
                pointOnB = null;
                return true;
            }
        }
    }
}
