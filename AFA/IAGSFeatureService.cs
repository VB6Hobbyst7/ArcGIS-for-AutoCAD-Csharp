using System.Collections.Generic;

namespace AFA
{
    public interface IAGSFeatureService : IAGSService, IAGSObject
	{
		IDictionary<int, object> MapLayers
		{
			get;
			set;
		}

		bool SupportsQuery
		{
			get;
			set;
		}

		bool SupportsEditing
		{
			get;
			set;
		}

		Extent FullExtent
		{
			get;
			set;
		}

		bool ExportFeatures(AGSExportOptions eo, List<int> layerIDs, ref string errList);

		bool AddService(AGSExportOptions eo, List<int> layerIDs, ref string errList);
	}
}
