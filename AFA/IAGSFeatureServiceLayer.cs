namespace AFA
{
    public interface IAGSFeatureServiceLayer : IAGSLayer
	{
		IAGSFeatureService FeatureService
		{
			get;
			set;
		}

		bool IsSelected
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
	}
}
