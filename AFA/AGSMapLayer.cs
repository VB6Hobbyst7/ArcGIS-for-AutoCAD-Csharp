using AFA.Resources;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AFA
{
    public class AGSMapLayer : AGSLayer
	{
		public IAGSMapService Map
		{
			get;
			set;
		}

		public IList<int> ChildLayerIds
		{
			get;
			set;
		}

		public bool DefaultVisibility
		{
			get;
			set;
		}

		public bool IsVisible
		{
			get;
			set;
		}

		public bool SupportsQuery
		{
			get;
			set;
		}

		public bool SupportsData
		{
			get;
			set;
		}

		public DataTable IdentifyResults
		{
			get;
			set;
		}

		public bool IsParentVisible
		{
			get
			{
				if (base.parentLayerId == -1)
				{
					return this.Map.IsVisible;
				}
				object obj;
				if (this.Map.MapLayers.TryGetValue(base.parentLayerId, out obj))
				{
					AGSMapLayer aGSMapLayer = obj as AGSMapLayer;
					return aGSMapLayer.IsVisible && aGSMapLayer.IsParentVisible;
				}
				return false;
			}
		}

		public new IList<object> Items
		{
			get
			{
				IList<object> list = new List<object>();
				foreach (int current in this.ChildLayerIds)
				{
					object item;
					if (this.Map.MapLayers.TryGetValue(current, out item))
					{
						list.Add(item);
					}
				}
				return list;
			}
		}

		public override string GetWKT()
		{
			return base.Service.GetWKT();
		}

		public AGSMapLayer(int id, AGSService map)
		{
			base.Id = id;
			base.Service = map;
			this.Map = (base.Service as AGSMapService);
			this.ChildLayerIds = new List<int>();
			this.RefreshProperties();
		}

		private void RefreshProperties()
		{
			try
			{
				string text = this.Map.Parent.URL;
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"/",
					this.Map.FullName,
					"/MapServer/",
					base.Id.ToString()
				});
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("?f={0}", "json");
				IDictionary<string, object> dictionary = base.Service.Parent.MakeDictionaryRequest(text + stringBuilder);
				if (dictionary != null)
				{
					this.PopulateProperties(dictionary);
				}
				else
				{
					base.Properties.Add(AfaStrings.Error, AfaStrings.UnableToRetrieveServiceProperties);
				}
			}
			catch
			{
				ErrorReport.ShowErrorMessage(base.Service.Parent.ErrorMessage);
			}
		}

		private new void PopulateProperties(IDictionary<string, object> results)
		{
			base.PopulateProperties(results);
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
					}
					if (a == "Data")
					{
						this.SupportsData = true;
					}
				}
			}
		}
	}
}
