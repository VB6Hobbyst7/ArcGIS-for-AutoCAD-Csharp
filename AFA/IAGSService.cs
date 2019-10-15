namespace AFA
{
    public interface IAGSService : IAGSObject
	{
		StringList SupportedImageTypes
		{
			get;
			set;
		}

		AGSExportOptions ExportOptions
		{
			get;
			set;
		}

		string ErrorMessage
		{
			get;
			set;
		}

		string GetWKT();
	}
}
