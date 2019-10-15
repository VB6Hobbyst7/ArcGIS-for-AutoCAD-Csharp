using System.Collections.Generic;

namespace AFA
{
    public interface IAGSMapService : IAGSService, IAGSObject
	{
		IDictionary<int, object> MapLayers
		{
			get;
			set;
		}

		bool IsVisible
		{
			get;
			set;
		}

		bool HasTiles
		{
			get;
			set;
		}

		bool SupportsMap
		{
			get;
			set;
		}

		bool SupportsQuery
		{
			get;
			set;
		}

		bool SupportsData
		{
			get;
			set;
		}

		ExportedImage OutputImage
		{
			get;
			set;
		}

		bool IsFixedScale
		{
			get;
			set;
		}

		bool ExportFeatures(AGSExportOptions eo, List<int> layerIDs, ref string errList);
	}
}
