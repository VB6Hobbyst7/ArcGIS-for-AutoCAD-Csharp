using ArcGIS10Types;
using System;

namespace AFA
{
	public class ExportedImageEventArgs : EventArgs
	{
		public PointN[] Points
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public string MapName
		{
			get;
			set;
		}

		public AGSExportOptions ExportOptions
		{
			get;
			set;
		}

		public AGSService MapService
		{
			get;
			set;
		}

		public ExportedImageEventArgs(AGSService mapService, AGSExportOptions eo, PointN[] points)
		{
			this.MapName = mapService.Name;
			this.MapService = mapService;
			this.ExportOptions = (AGSExportOptions)Utility.CloneObject(eo);
			this.Points = points;
			this.ErrorMessage = "";
		}
	}
}
