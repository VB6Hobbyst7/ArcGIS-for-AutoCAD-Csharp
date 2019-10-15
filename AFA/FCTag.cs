namespace AFA
{
    public class FCTag
	{
		private FCTag ParentTag;

		public string fcName
		{
			get;
			set;
		}

		private bool IsFeatureService
		{
			get;
			set;
		}

		public FCTag(MSCFeatureClass fc)
		{
			this.fcName = fc.Name;
			this.IsFeatureService = (fc.GetType() == typeof(MSCFeatureService));
			if (fc.ParentFC == null)
			{
				this.fcName = fc.Name;
			}
			this.ParentTag = null;
			if (fc.IsSubType)
			{
				MSCFeatureClass arg_56_0 = fc.ParentFC;
				this.ParentTag = new FCTag(fc.ParentFC);
			}
		}

		public override string ToString()
		{
			return this.fcName.ToString();
		}

		public MSCFeatureClass GetFeatureClass(MSCDataset ds)
		{
			if (this.IsFeatureService)
			{
				MSCFeatureService result = null;
				if (ds.FeatureServices.TryGetValue(this.fcName, out result))
				{
					return result;
				}
				return null;
			}
			else if (this.ParentTag == null)
			{
				MSCFeatureClass result2 = null;
				if (ds.FeatureClasses.TryGetValue(this.fcName, out result2))
				{
					return result2;
				}
				return null;
			}
			else
			{
				if (this.ParentTag != null)
				{
					return this.GetFeatureClass(ds, this.ParentTag);
				}
				return null;
			}
		}

		public MSCFeatureClass GetFeatureClass(MSCDataset ds, FCTag parentTag)
		{
			MSCFeatureClass featureClass = parentTag.GetFeatureClass(ds);
			foreach (MSCFeatureClass current in featureClass.SubTypes)
			{
				if (current.Name == this.fcName)
				{
					return current;
				}
			}
			return null;
		}
	}
}
