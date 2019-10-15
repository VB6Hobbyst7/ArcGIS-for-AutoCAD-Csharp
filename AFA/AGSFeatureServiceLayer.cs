using AFA.Resources;
using System.Collections.Generic;
using System.Text;

namespace AFA
{
    internal class AGSFeatureServiceLayer : AGSLayer, IAGSFeatureServiceLayer, IAGSLayer
	{
		public IAGSFeatureService FeatureService
		{
			get;
			set;
		}

		public bool IsSelected
		{
			get;
			set;
		}

		public bool SupportsQuery
		{
			get;
			set;
		}

		public bool SupportsEditing
		{
			get;
			set;
		}

		public AGSFeatureServiceLayer(int id, IAGSFeatureService fs)
		{
			base.Id = id;
			this.FeatureService = fs;
			base.Service = (this.FeatureService as AGSService);
			this.SupportsEditing = false;
			this.SupportsQuery = false;
			this.IsSelected = false;
			this.RefreshProperties();
		}

		private bool RefreshProperties()
		{
			bool result;
			try
			{
				string text = this.FeatureService.Parent.URL;
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"/",
					this.FeatureService.FullName,
					"/FeatureServer/",
					base.Id.ToString()
				});
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("?f={0}", "json");
				IDictionary<string, object> dictionary = base.Service.Parent.MakeDictionaryRequest(text + stringBuilder);
				if (dictionary != null)
				{
					if (this.PopulateProperties(dictionary))
					{
						result = true;
					}
					else
					{
						base.Properties.Add(AfaStrings.Error, AfaStrings.UnableToRetrieveServiceProperties);
						result = false;
					}
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				base.Properties.Add(AfaStrings.Error, AfaStrings.UnableToRetrieveServiceProperties);
				result = false;
			}
			return result;
		}

		private new bool PopulateProperties(IDictionary<string, object> results)
		{
			if (!base.PopulateProperties(results))
			{
				return false;
			}
			try
			{
				object obj;
				if (base.Properties.TryGetValue("Capabilities", out obj))
				{
					string text = obj as string;
					string[] array = text.Split(new char[]
					{
						','
					});
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string a = array2[i];
						if (a == "Query")
						{
							this.SupportsQuery = true;
							this.FeatureService.SupportsQuery = true;
						}
						if (a == "Editing")
						{
							this.SupportsEditing = true;
							this.FeatureService.SupportsEditing = true;
						}
					}
				}
				if (base.Properties.TryGetValue("Extent", out obj))
				{
					if (this.FeatureService.FullExtent == null)
					{
						this.FeatureService.FullExtent = (obj as Extent);
					}
					else
					{
						this.FeatureService.FullExtent = Extent.Union(this.FeatureService.FullExtent, obj as Extent);
					}
				}
				object value;
				if (results.TryGetValue("hasZ", out value))
				{
					base.Properties.Add("Has Z", value);
				}
				object value2;
				if (results.TryGetValue("allowGeometryUpdates", out value2))
				{
					base.Properties.Add("Allow Geometry Updates", value2);
				}
			}
			catch
			{
			}
			return true;
		}

		public override string GetWKT()
		{
			return base.Service.GetWKT();
		}
	}
}
