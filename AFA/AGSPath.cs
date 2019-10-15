using ArcGIS10Types;
using System.Collections.Generic;

namespace AFA
{
    public class AGSPath
	{
		public IList<AGSSegment> Segments
		{
			get;
			set;
		}

		public AGSPath()
		{
		}

		public AGSPath(Path srcP)
		{
			this.Segments = new List<AGSSegment>();
			if (srcP.SegmentArray != null)
			{
				Segment[] segmentArray = srcP.SegmentArray;
				for (int i = 0; i < segmentArray.Length; i++)
				{
					Segment segment = segmentArray[i];
					Line line = segment as Line;
					CircularArc circularArc = segment as CircularArc;
					EllipticArc ellipticArc = segment as EllipticArc;
					BezierCurve bezierCurve = segment as BezierCurve;
					if (line != null)
					{
						this.Segments.Add(new AGSLineSegment(line));
					}
					else if (circularArc != null)
					{
						if (circularArc.IsLine)
						{
							this.Segments.Add(new AGSLineSegment(circularArc.FromPoint as PointN, circularArc.ToPoint as PointN));
						}
						else
						{
							this.Segments.Add(new AGSCircularArcSegment(circularArc));
						}
					}
					else if (ellipticArc != null)
					{
						this.Segments.Add(new AGSEllipticalArcSegment(ellipticArc));
					}
					else if (bezierCurve != null)
					{
						this.Segments.Add(new AGSBezierSegment(bezierCurve));
					}
				}
				return;
			}
			if (srcP.PointArray != null)
			{
				int num = srcP.PointArray.Length;
				AGSPoint f = new AGSPoint(srcP.PointArray[0] as PointN);
				for (int j = 1; j < num; j++)
				{
					AGSPoint aGSPoint = new AGSPoint(srcP.PointArray[j] as PointN);
					this.Segments.Add(new AGSLineSegment(f, aGSPoint));
					f = aGSPoint;
				}
			}
		}

		public override string ToString()
		{
			string text = "Path: ";
			foreach (AGSSegment current in this.Segments)
			{
				text += string.Format("\n  Segment: {0}", current);
			}
			return text;
		}
	}
}
