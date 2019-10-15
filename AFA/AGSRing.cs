using ArcGIS10Types;

namespace AFA
{
    public class AGSRing : AGSPath
	{
		public AGSRing(Ring srcRing) : base(srcRing)
		{
		}

		public override string ToString()
		{
			string text = "Ring: ";
			if (base.Segments != null)
			{
				foreach (AGSSegment current in base.Segments)
				{
					text += string.Format("\n  Segment: {0}", current);
				}
			}
			return text;
		}
	}
}
