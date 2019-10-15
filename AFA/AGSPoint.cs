using ArcGIS10Types;

namespace AFA
{
    public class AGSPoint
	{
		private decimal X
		{
			get;
			set;
		}

		private decimal Y
		{
			get;
			set;
		}

		private decimal Z
		{
			get;
			set;
		}

		public AGSPoint(PointN srcPt)
		{
			this.X = (decimal)srcPt.X;
			this.Y = (decimal)srcPt.X;
			this.Z = (decimal)srcPt.X;
		}

		public override string ToString()
		{
			return string.Format("[{0}, {1}, {2}]", this.X, this.Y, this.Z);
		}
	}
}
