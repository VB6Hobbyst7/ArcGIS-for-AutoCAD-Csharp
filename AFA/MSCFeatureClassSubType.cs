using ArcGIS10Types;
using Autodesk.AutoCAD.DatabaseServices;

namespace AFA
{
    public class MSCFeatureClassSubType : MSCFeatureClass
	{
		public object TypeValue
		{
			get;
			set;
		}

		public string CadLayerName
		{
			get;
			set;
		}

		public DataObject Prototype
		{
			get;
			set;
		}

		public new ResultBuffer FCQuery
		{
			get
			{
				ResultBuffer resultBuffer = new ResultBuffer(new TypedValue[]
				{
					new TypedValue(-4, "<and")
				});
				TypedValue[] typeQuery = base.TypeQuery;
				for (int i = 0; i < typeQuery.Length; i++)
				{
					resultBuffer.Add(typeQuery[i]);
				}
				resultBuffer.Add(new TypedValue(8, this.CadLayerName));
				resultBuffer.Add(new TypedValue(-4, "and>"));
				return resultBuffer;
			}
		}

		public MSCFeatureClassSubType(string name, MSCDataset parent, ObjectId id, Transaction t) : base(name, parent, id, t)
		{
		}

		public MSCFeatureClassSubType(MSCDataset parent) : base(parent)
		{
		}
	}
}
