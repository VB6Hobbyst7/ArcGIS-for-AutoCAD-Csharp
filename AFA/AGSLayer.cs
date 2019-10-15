using System;
using System.Collections.Generic;
using System.Text;

namespace AFA
{
	public abstract class AGSLayer
	{
		public string Name
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public IDictionary<string, object> Properties
		{
			get;
			set;
		}

		public IAGSService Service
		{
			get;
			set;
		}

		public int parentLayerId
		{
			get;
			set;
		}

		public IDictionary<string, AGSField> Fields
		{
			get;
			set;
		}

		public SubTypeDictionary Subtypes
		{
			get;
			set;
		}

		public AGSRenderer Renderer
		{
			get;
			set;
		}

		public string DisplayField
		{
			get;
			set;
		}

		public string IDField
		{
			get;
			set;
		}

		public string GeomField
		{
			get;
			set;
		}

		public string TypeIDField
		{
			get;
			set;
		}

		public string GeometryType
		{
			get;
			set;
		}

		public IDictionary<string, object> ExportProperties
		{
			get;
			set;
		}

		public bool IsFeatureLayer
		{
			get;
			set;
		}

		public bool IsGroupLayer
		{
			get
			{
				bool result;
				try
				{
					if (this.Properties.ContainsKey("Type"))
					{
						if ("Group Layer" == this.Properties["Type"].ToString())
						{
							result = true;
						}
						else
						{
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
					result = false;
				}
				return result;
			}
		}

		public abstract string GetWKT();

		public AGSLayer()
		{
			this.Properties = new Dictionary<string, object>();
			this.Fields = new Dictionary<string, AGSField>();
			this.parentLayerId = -1;
		}

		public IList<object> Items()
		{
			return new List<object>();
		}

		public bool PopulateProperties(IDictionary<string, object> results)
		{
			try
			{
				this.Properties.Add("ID", this.Id);
				object obj;
				if (results.TryGetValue("name", out obj))
				{
					this.Properties.Add("Layer Name", obj);
				}
				if (results.TryGetValue("type", out obj))
				{
					this.Properties.Add("Type", obj);
					if (obj.ToString() == "Feature Layer")
					{
						this.IsFeatureLayer = true;
					}
					else
					{
						this.IsFeatureLayer = false;
					}
				}
				if (results.TryGetValue("geometryType", out obj) && obj != null)
				{
					this.Properties.Add("Geometry Type", obj);
					this.GeometryType = (obj as string);
				}
				if (results.TryGetValue("capabilities", out obj))
				{
					string value = obj as string;
					this.Properties.Add("Capabilities", value);
				}
			}
			catch
			{
				bool result = false;
				return result;
			}
			try
			{
				object obj;
				if (results.TryGetValue("description", out obj))
				{
					string value2 = obj as string;
					if (!string.IsNullOrEmpty(value2))
					{
						this.Properties.Add("Description", obj);
					}
				}
				if (results.TryGetValue("copyrightText", out obj))
				{
					string value3 = obj as string;
					if (!string.IsNullOrEmpty(value3))
					{
						this.Properties.Add("Copyright", obj);
					}
				}
				if (results.TryGetValue("definitionExpression", out obj))
				{
					this.Properties.Add("Definition Expression", obj);
				}
				if (results.TryGetValue("parentLayer", out obj))
				{
					IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
					if (dictionary != null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (KeyValuePair<string, object> arg_196_0 in dictionary)
						{
							object obj2;
							if (dictionary.TryGetValue("id", out obj2))
							{
								stringBuilder.AppendFormat("ID: {0}", obj2);
								this.parentLayerId = (int)obj2;
							}
							if (dictionary.TryGetValue("name", out obj2))
							{
								stringBuilder.AppendFormat(", Name: {0}", obj2);
							}
						}
						if (stringBuilder.Length > 0)
						{
							this.Properties.Add("Parent Layer", stringBuilder);
						}
					}
				}
				results.TryGetValue("relationships", out obj);
				if (results.TryGetValue("minScale", out obj))
				{
					this.Properties.Add("Minimum Scale", obj);
				}
				if (results.TryGetValue("maxScale", out obj))
				{
					this.Properties.Add("Maximum Scale", obj);
				}
				if (results.TryGetValue("extent", out obj))
				{
					try
					{
						IDictionary<string, object> dictionary2 = obj as IDictionary<string, object>;
						if (dictionary2 != null)
						{
							Extent extent = new Extent(dictionary2);
							if (extent.IsValid())
							{
								this.Properties.Add("Extent", extent);
							}
						}
					}
					catch
					{
					}
				}
				if (results.TryGetValue("drawingInfo", out obj))
				{
					try
					{
						if (obj != null)
						{
							IDictionary<string, object> dictionary3 = obj as IDictionary<string, object>;
							foreach (KeyValuePair<string, object> current in dictionary3)
							{
								if (current.Key == "renderer")
								{
									IDictionary<string, object> dictionary4 = current.Value as IDictionary<string, object>;
									object obj3;
									if (dictionary4.TryGetValue("type", out obj3))
									{
										string a = obj3 as string;
										this.Renderer = null;
										if (a == "simple")
										{
											this.Renderer = new AGSSimpleRenderer(dictionary4);
										}
										else if (a == "uniqueValue")
										{
											this.Renderer = new AGSUniqueValueRenderer(dictionary4);
										}
										else if (a == "classBreaks")
										{
											this.Renderer = new AGSClassBreaksRenderer(dictionary4);
										}
										if (this.Renderer != null)
										{
											this.Properties.Add("Renderer", this.Renderer);
										}
									}
								}
								else if (current.Key == "transparency")
								{
									this.Properties.Add("Transparency", current.Value);
								}
								else if (current.Key == "labellingInfo")
								{
									IDictionary<string, object> dictionary5 = current.Value as IDictionary<string, object>;
									foreach (KeyValuePair<string, object> current2 in dictionary5)
									{
										if (current2.Key == "labelPlacement")
										{
											this.Properties.Add("Renderer Label Placement", current2.Value);
										}
										else if (current2.Key == "labelExpression")
										{
											this.Properties.Add("Renderer Label Expression", current2.Value);
										}
										else if (current2.Key == "useCodedValues")
										{
											this.Properties.Add("Renderer Label Uses Coded Values", current2.Value);
										}
									}
								}
							}
						}
					}
					catch
					{
					}
				}
				if (results.TryGetValue("htmlPopupType", out obj))
				{
					this.Properties.Add("HTML Popup Type", obj);
				}
				if (results.TryGetValue("objectIdField", out obj))
				{
					this.Properties.Add("Object Id Field", obj);
				}
				if (results.TryGetValue("globalIdField", out obj))
				{
					string value4 = obj as string;
					if (!string.IsNullOrEmpty(value4))
					{
						this.Properties.Add("Global Id Field", obj);
					}
				}
				if (results.TryGetValue("displayField", out obj))
				{
					this.Properties.Add("Display Field", obj);
					this.DisplayField = (obj as string);
				}
				if (results.TryGetValue("fields", out obj) && obj != null)
				{
					this.BuildFields(obj as IList<object>);
					int num = 1;
					foreach (KeyValuePair<string, AGSField> current3 in this.Fields)
					{
						if (current3.Value != null)
						{
							this.Properties.Add("Field " + num.ToString(), current3.Value.ToString());
							num++;
						}
					}
				}
				if (results.TryGetValue("typeIdField", out obj))
				{
					this.Properties.Add("Type Id Field", obj);
					this.TypeIDField = (string)obj;
				}
				if (results.TryGetValue("types", out obj) && obj != null)
				{
					IList<object> list = obj as IList<object>;
					this.Subtypes = new SubTypeDictionary();
					using (IEnumerator<object> enumerator5 = list.GetEnumerator())
					{
						while (enumerator5.MoveNext())
						{
							IDictionary<string, object> dictionary6 = (IDictionary<string, object>)enumerator5.Current;
							if (dictionary6 != null)
							{
								AGSSubType aGSSubType = new AGSSubType();
								aGSSubType.Initialize(dictionary6 as Dictionary<string, object>);
								this.Subtypes.Add(aGSSubType.Name, aGSSubType);
							}
						}
					}
					this.Properties.Add("Subtypes", this.Subtypes);
				}
				if (this.Fields != null && !string.IsNullOrEmpty(this.TypeIDField) && this.Fields.ContainsKey(this.TypeIDField))
				{
					this.Fields[this.TypeIDField].IsTypeField = true;
				}
			}
			catch (SystemException)
			{
				bool result = false;
				return result;
			}
			return true;
		}

		protected void BuildFields(IList<object> list)
		{
			this.Fields.Clear();
			try
			{
				foreach (object current in list)
				{
					Dictionary<string, object> dict = current as Dictionary<string, object>;
					AGSField aGSField = new AGSField();
					aGSField.Initialize(dict);
					if (!this.Fields.ContainsKey(aGSField.Name))
					{
						this.Fields.Add(aGSField.Name, aGSField);
						if (aGSField.IsOID)
						{
							this.IDField = aGSField.Name;
						}
						if (aGSField.IsGeom)
						{
							this.GeomField = aGSField.Name;
						}
					}
				}
			}
			catch (SystemException)
			{
			}
		}
	}
}
