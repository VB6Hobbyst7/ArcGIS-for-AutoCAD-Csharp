using ArcGIS10Types;

namespace AFA
{
    public class AGSCircularArcSegment : AGSSegment
	{
		private AGSPoint Center
		{
			get;
			set;
		}

		private double Radius
		{
			get;
			set;
		}

		private double FromAngle
		{
			get;
			set;
		}

		private double ToAngle
		{
			get;
			set;
		}

		private bool IsMinor
		{
			get;
			set;
		}

		private bool IsCounterClockwise
		{
			get;
			set;
		}

		public AGSCircularArcSegment(CircularArc srcSeg)
		{
			base.StartPoint = new AGSPoint(srcSeg.FromPoint as PointN);
			base.EndPoint = new AGSPoint(srcSeg.ToPoint as PointN);
			this.Center = new AGSPoint(srcSeg.CenterPoint as PointN);
			this.FromAngle = srcSeg.FromAngle;
			this.ToAngle = srcSeg.ToAngle;
			this.IsMinor = srcSeg.IsMinor;
			this.IsCounterClockwise = srcSeg.IsCounterClockwise;
		}

		public override string ToString()
		{
			return string.Format("   Circular Arc: radius={0}, center={1}, fromAngle={2}, ToAngle={3}, IsMinor={4}, IsCounterClockwise={5}, s={6}, e={7}, ", new object[]
			{
				this.Radius,
				this.Center,
				this.FromAngle,
				this.ToAngle,
				this.IsMinor,
				this.IsCounterClockwise,
				base.StartPoint,
				base.EndPoint
			});
		}
	}
}
