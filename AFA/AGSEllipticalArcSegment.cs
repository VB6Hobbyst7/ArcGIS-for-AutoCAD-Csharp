using ArcGIS10Types;

namespace AFA
{
    public class AGSEllipticalArcSegment : AGSSegment
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

		private double MinorMajorRatio
		{
			get;
			set;
		}

		private double Rotation
		{
			get;
			set;
		}

		private bool IsStandard
		{
			get;
			set;
		}

		public AGSEllipticalArcSegment(EllipticArc srcSeg)
		{
			base.StartPoint = new AGSPoint(srcSeg.FromPoint as PointN);
			base.EndPoint = new AGSPoint(srcSeg.ToPoint as PointN);
			this.Center = new AGSPoint(srcSeg.CenterPoint as PointN);
			this.IsMinor = srcSeg.IsMinor;
			this.IsCounterClockwise = srcSeg.IsCounterClockwise;
			this.MinorMajorRatio = srcSeg.MinorMajorRatio;
			this.Rotation = srcSeg.Rotation;
			this.IsStandard = srcSeg.EllipseStd;
		}

		public override string ToString()
		{
			return string.Format("   Elliptical Arc: radius={0}, center={1}, IsMinor={2}, IsCounterClockwise={3}, Ratio={4}, Rotation={5}, IsStandard={6}, s={7}, e={8}, ", new object[]
			{
				this.Radius,
				this.Center,
				this.IsMinor,
				this.IsCounterClockwise,
				this.MinorMajorRatio,
				this.Rotation,
				this.IsStandard,
				base.StartPoint,
				base.EndPoint
			});
		}
	}
}
