using ArcGIS10Types;

namespace AFA
{
    public class AGSBezierSegment : AGSSegment
	{
		public AGSBezierSegment(BezierCurve srcSeg)
		{
			base.StartPoint = new AGSPoint(srcSeg.FromPoint as PointN);
			base.EndPoint = new AGSPoint(srcSeg.ToPoint as PointN);
		}

		public override string ToString()
		{
			return string.Format("   Bezier: s={0}, e={1}", base.StartPoint, base.EndPoint);
		}
	}
}
