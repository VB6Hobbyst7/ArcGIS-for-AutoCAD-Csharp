using ArcGIS10Types;
using System;

namespace AFA
{
	public class ExportedFeaturesEventArgs : EventArgs
	{
		public RecordSet records
		{
			get;
			set;
		}

		public RecordSet densifiedRecords
		{
			get;
			set;
		}

		public int GeometryIndex
		{
			get;
			set;
		}

		public AGSMapLayer Layer
		{
			get;
			set;
		}

		public AGSSubType Subtype
		{
			get;
			set;
		}

		public MSCFeatureClass CadFC
		{
			get;
			set;
		}

		public string MapName
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public string CadLayerName
		{
			get;
			set;
		}

		public string TypeFieldName
		{
			get;
			set;
		}

		public string WKT
		{
			get;
			set;
		}

		public ExportedFeaturesEventArgs(string map, AGSMapLayer layer, AGSSubType subtype, RecordSet rs, RecordSet drs, int index)
		{
			this.MapName = map;
			this.Layer = layer;
			this.Subtype = subtype;
			this.records = rs;
			this.densifiedRecords = drs;
			this.GeometryIndex = index;
			this.ErrorMessage = null;
			this.WKT = null;
		}

		public ExportedFeaturesEventArgs(string map, AGSMapLayer layer, AGSSubType subtype, MSCFeatureClass fc, RecordSet rs, RecordSet drs, int index)
		{
			this.MapName = map;
			this.CadFC = fc;
			this.Layer = layer;
			this.Subtype = subtype;
			this.records = rs;
			this.densifiedRecords = drs;
			this.GeometryIndex = index;
			this.ErrorMessage = null;
			this.WKT = null;
		}

		public ExportedFeaturesEventArgs(string errMessage)
		{
			this.MapName = null;
			this.Layer = null;
			this.records = null;
			this.Subtype = null;
			this.CadFC = null;
			this.GeometryIndex = -1;
			this.ErrorMessage = errMessage;
			this.WKT = null;
		}
	}
}
