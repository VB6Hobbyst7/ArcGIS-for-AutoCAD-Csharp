using ArcGIS10Types;
using System.Collections.Generic;

namespace AFA
{
    internal class AGSPolyline
	{
		private IList<AGSPath> Paths
		{
			get;
			set;
		}

		public AGSPolyline(PolylineN srcPLine)
		{
			if (srcPLine.PathArray != null)
			{
				this.Paths = new List<AGSPath>();
				Path[] pathArray = srcPLine.PathArray;
				for (int i = 0; i < pathArray.Length; i++)
				{
					Path srcP = pathArray[i];
					this.Paths.Add(new AGSPath(srcP));
				}
			}
		}

		public override string ToString()
		{
			string text = "\nPaths:";
			foreach (AGSPath current in this.Paths)
			{
				text += string.Format("\n{0}", current);
			}
			return text;
		}
	}
}
