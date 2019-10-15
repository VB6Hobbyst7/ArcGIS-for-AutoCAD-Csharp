using ArcGIS10Types;
using System.Collections.Generic;

namespace AFA
{
    public class AGSMultipoint
	{
		public IList<AGSPoint> Points
		{
			get;
			set;
		}

		public AGSMultipoint(MultipointN srcMp)
		{
			Point[] pointArray = srcMp.PointArray;
			for (int i = 0; i < pointArray.Length; i++)
			{
				PointN srcPt = (PointN)pointArray[i];
				this.Points.Add(new AGSPoint(srcPt));
			}
		}

		public override string ToString()
		{
			string text = "<";
			foreach (AGSPoint current in this.Points)
			{
				text += string.Format("{0}, ", current);
			}
			text += ">";
			return text;
		}
	}
}
