using System.Collections.Generic;

namespace AFA
{
    public class FCView
	{
		public FCTag FC
		{
			get;
			set;
		}

		public List<FCTag> SubTypes
		{
			get;
			set;
		}

		public FCView(MSCFeatureClass fc)
		{
			this.FC = new FCTag(fc);
			this.SubTypes = new List<FCTag>();
			foreach (MSCFeatureClass current in fc.SubTypes)
			{
				this.SubTypes.Add(new FCTag(current));
			}
		}

		public override string ToString()
		{
			return this.FC.ToString();
		}
	}
}
