using ArcGIS10Types;
using System.Collections.Generic;

namespace AFA
{
    internal class AGSPolygon
	{
		private IList<AGSRing> Rings
		{
			get;
			set;
		}

		public AGSPolygon(PolygonN srcPoly)
		{
			if (srcPoly.RingArray != null)
			{
				this.Rings = new List<AGSRing>();
				Ring[] ringArray = srcPoly.RingArray;
				for (int i = 0; i < ringArray.Length; i++)
				{
					Ring srcRing = ringArray[i];
					this.Rings.Add(new AGSRing(srcRing));
				}
			}
		}

		public override string ToString()
		{
			string text = "\nRings:";
			foreach (AGSRing current in this.Rings)
			{
				text += string.Format("\n{0}", current);
			}
			return text;
		}
	}
}
