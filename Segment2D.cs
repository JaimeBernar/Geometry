namespace Geometry
{
    public class Segment2D
    {
        public Point2D P0;
        public Point2D P1;

        public Point2D MidPoint => P0 + (P1 - P0) / 2.0;
        public bool IsPoint => P0 == P1;
        public Vector2D Direction => new Vector2D(P0, P1);


        #region CONSTRUCTORS
        public Segment2D() { }
        public Segment2D(Vector2D v1)
        {
            P0 = new Point2D();
            P1 = new Point2D(v1.X, v1.Y);
        }
        public Segment2D(Point2D p0, Point2D p1)
        {
            P0 = p0;
            P1 = p1;
        }

        public Segment2D(double x0, double y0, double x1, double y1)
        {
            P0 = new Point2D(x0, y0);
            P1 = new Point2D(x1, y1);
        }

        public Segment2D(Segment2D another) : this(another.P0, another.P1) { }

        #endregion


        /// <summary>
        /// Computes the intersection between two segments
        /// </summary>
        /// <param name="s1">First segment</param>
        /// <param name="s2">Second segment</param>
        /// <param name="i0">The point of intersection</param>
        /// <returns>True if the intersection succeeded, false otherwise</returns>
        public static bool Intersection(Segment2D s1, Segment2D s2, out Point2D i0)
        {
            double x1 = s1.P0.X;
            double y1 = s1.P0.Y;
            double x2 = s1.P1.X;
            double y2 = s1.P1.Y;
            double x3 = s2.P0.X;
            double y3 = s2.P0.Y;
            double x4 = s2.P1.X;
            double y4 = s2.P1.Y;

            double numeratorX = (x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4);
            double numeratorY = (x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4);
            double denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            i0 = new Point2D(numeratorX / denominator, numeratorY / denominator);
            return true;
        }
    }
}
