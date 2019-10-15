using Autodesk.AutoCAD.DatabaseServices;
using System.Collections.Generic;

namespace AFA
{
    public class CadFeature
	{
		public List<Entity> EntList
		{
			get;
			set;
		}

		public List<CadField> Fields
		{
			get;
			set;
		}

		public int SvcOID
		{
			get;
			set;
		}
	}
}
