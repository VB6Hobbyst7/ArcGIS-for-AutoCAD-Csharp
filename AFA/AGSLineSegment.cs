using ArcGIS10Types;

namespace AFA
{
    public class AGSLineSegment : AGSSegment
	{
		public AGSLineSegment(Line srcSeg)
		{
			base.StartPoint = new AGSPoint(srcSeg.FromPoint as PointN);
			base.EndPoint = new AGSPoint(srcSeg.ToPoint as PointN);
		}

		public AGSLineSegment(AGSPoint f, AGSPoint t)
		{
			base.StartPoint = f;
			base.EndPoint = t;
		}

		public AGSLineSegment(PointN f, PointN t)
		{
			base.StartPoint = new AGSPoint(f);
			base.EndPoint = new AGSPoint(t);
		}

		public override string ToString()
		{
			return string.Format("   Line: s={0}, e={1}", base.StartPoint, base.EndPoint);
		}
	}
}
