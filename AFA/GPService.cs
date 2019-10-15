namespace AFA
{
    internal class GPService : AGSService
	{
		public GPService(string name, AGSConnection parent) : base(name, parent)
		{
			base.Properties.Add("Type", "Geoprocessing Server");
		}

		public override string GetWKT()
		{
			return "";
		}
	}
}
