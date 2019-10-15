using AFA.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AFA
{
	public class AGSFolder : IAGSFolder
	{
		public string Name
		{
			get;
			set;
		}

		public string FullName
		{
			get;
			set;
		}

		public AGSConnection Parent
		{
			get;
			set;
		}

		public AGSType Type
		{
			get;
			set;
		}

		public string URL
		{
			get;
			set;
		}

		public string ErrMsg
		{
			get;
			set;
		}

		public IList<object> Children
		{
			get;
			set;
		}

		public IList<object> Items
		{
			get
			{
				IList<object> list = new List<object>();
				foreach (object current in this.Children)
				{
					list.Add(current);
				}
				return list;
			}
		}

		public AGSFolder(string name, string URL, AGSConnection parent)
		{
			this.Parent = parent;
			this.FullName = name;
			this.Name = name;
			this.URL = URL;
			this.Type = AGSType.AGSFolder;
			this.Children = new List<object>();
			this.ErrMsg = "";
		}

		public override string ToString()
		{
			return this.Name;
		}

		public void Clear()
		{
			if (this.Children != null)
			{
				this.Children.Clear();
			}
			this.ErrMsg = "";
		}

		public bool LoadChildren()
		{
			bool result;
			try
			{
				if (this.Parent.ReportCheckCancel())
				{
					this.EmptyChildren();
					result = true;
				}
				else
				{
					this.Parent.ReportExportStatus(AfaStrings.RequestingFolderDetails);
					string uRL = this.URL;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("?f={0}", "json");
					IDictionary<string, object> dictionary = this.Parent.MakeDictionaryRequest(uRL + stringBuilder);
					if (dictionary != null)
					{
						result = this.PopulateChildren(dictionary);
					}
					else
					{
						this.EmptyChildren();
						result = false;
					}
				}
			}
			catch
			{
				this.EmptyChildren();
				result = false;
			}
			return result;
		}

		public void EmptyChildren()
		{
			if (this.Children != null)
			{
				this.Children.Clear();
			}
		}

		public static string RestToSoapURL(string restURL)
		{
			if (string.IsNullOrEmpty(restURL))
			{
				return restURL;
			}
			string result;
			try
			{
				Uri uri = new Uri(restURL);
				string host = uri.Host;
				string[] segments = uri.Segments;
				string text = "http://" + host;
				string[] array = segments;
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i];
					if (!(text2.ToLower() == "rest/"))
					{
						text += text2;
					}
				}
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private bool PopulateChildren(IDictionary<string, object> results)
		{
			if (results == null)
			{
				return false;
			}
			if (this.Parent.ReportCheckCancel())
			{
				this.EmptyChildren();
				return true;
			}
			bool result;
			try
			{
				if (results.ContainsKey("services"))
				{
					string text = "";
					IEnumerable<object> enumerable = results["services"] as IEnumerable<object>;
					using (IEnumerator<object> enumerator = enumerable.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IDictionary<string, object> dictionary = (IDictionary<string, object>)enumerator.Current;
							if (this.Parent.ReportCheckCancel())
							{
								this.EmptyChildren();
								result = true;
								return result;
							}
							if (dictionary.ContainsKey("url"))
							{
								text = AGSFolder.RestToSoapURL(dictionary["url"] as string);
							}
							if (dictionary.ContainsKey("name"))
							{
								string text2 = dictionary["name"] as string;
								string fileName = Path.GetFileName(text2);
								this.Parent.ReportExportStatus(AfaStrings.ReadingServiceDetailsFrom + fileName);
								IAGSService iAGSService = null;
								try
								{
									string a = dictionary["type"] as string;
									if (a == "MapServer")
									{
										iAGSService = new AGSMapService(text2, text, this.Parent);
									}
									else if (a == "FeatureServer")
									{
										iAGSService = new AGSFeatureService(text2, text, this.Parent);
									}
									else if (a == "ImageServer")
									{
										iAGSService = new AGSImageService(text2, this.Parent);
									}
									else if (a == "GeometryServer")
									{
										if (string.IsNullOrEmpty(text))
										{
											text = this.Parent.Soap_URL + "/" + text2 + "/GeometryServer";
										}
										this.Parent.GeometryService = new AGSGeometryServer(this.Parent, text);
									}
								}
								catch
								{
									iAGSService = null;
								}
								if (iAGSService != null)
								{
									this.Children.Add(iAGSService);
								}
							}
						}
					}
				}
				if (results.ContainsKey("folders"))
				{
					IEnumerable<object> enumerable2 = results["folders"] as IEnumerable<object>;
					using (IEnumerator<object> enumerator2 = enumerable2.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							string text3 = (string)enumerator2.Current;
							string uRL = this.URL + "/" + text3;
							IAGSFolder iAGSFolder = new AGSFolder(text3, uRL, this.Parent);
							this.Parent.ReportExportStatus(AfaStrings.ReadingServicesInFolder + text3);
							if (this.Parent.ReportCheckCancel())
							{
								this.EmptyChildren();
								result = true;
								return result;
							}
							iAGSFolder.LoadChildren();
							if (this.Parent.ReportCheckCancel())
							{
								this.EmptyChildren();
								result = true;
								return result;
							}
							this.Children.Add(iAGSFolder);
						}
					}
				}
				result = true;
			}
			catch (SystemException)
			{
				this.EmptyChildren();
				result = false;
			}
			return result;
		}
	}
}
