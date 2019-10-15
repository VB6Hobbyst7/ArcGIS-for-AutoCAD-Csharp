using AFA.Resources;
using System.Collections.Generic;
using System.Text;

namespace AFA
{
    internal class AGSTemplate
	{
		public string Name
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public void Initialize(Dictionary<string, object> dict)
		{
			foreach (KeyValuePair<string, object> current in dict)
			{
				if (current.Key == "name")
				{
					this.Name = (current.Value as string);
				}
				else if (current.Key == "description")
				{
					this.Description = (current.Value as string);
				}
			}
		}

		public override string ToString()
		{
			string result;
			try
			{
				if (this.Name == null || this.Description == null)
				{
					result = "Invalid Template";
				}
				else
				{
					string arg = "";
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("{0}, {1}", this.Name, this.Description);
					result = arg + stringBuilder;
				}
			}
			catch
			{
				result = AfaStrings.InvalidTemplate;
			}
			return result;
		}
	}
}
