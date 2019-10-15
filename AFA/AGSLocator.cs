using AFA.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AFA
{
	public class AGSLocator
	{
		public static bool bLocationsLoaded = false;

		private static short cVer = 1;

		private static AGSConnection ArcGISOnlineLocatorConnection;

		private static string ArcGISOnlineLocatorsURL = "http://tasks.arcgisonline.com/arcgis/rest/services";

		private static AGSConnection ArcGISComLocatorConnection;

		private static AGSConnection ArcGISGeocoderConnection;

		private static string ArcGISComLocatorsURL = "http://tasks.arcgis.com/arcgis/rest/services";

		private static string ArcGISGeocoderURL = "http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer";

		public string Name
		{
			get;
			set;
		}

		public string URL
		{
			get;
			set;
		}

		public string UserName
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}

		public string Domain
		{
			get;
			set;
		}

		public bool HasError
		{
			get
			{
				return this.ErrorMessage.Length > 0;
			}
		}

		public IDictionary<string, string> Properties
		{
			get;
			set;
		}

		public IWebProxy Proxy
		{
			get;
			set;
		}

		public ICredentials Credentials
		{
			get;
			set;
		}

		public bool IsVerified
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public AGSConnection ParentConnection
		{
			get;
			set;
		}

		private string Token
		{
			get;
			set;
		}

		private int Timeout
		{
			get;
			set;
		}

		private string TokenServicesURL
		{
			get;
			set;
		}

		private bool IsTokenBasedSecurity
		{
			get;
			set;
		}

		public AGSLocator(string name, string url)
		{
			this.Name = name;
			this.ErrorMessage = "";
			if (string.IsNullOrEmpty(this.Name))
			{
				this.IsVerified = false;
				this.ErrorMessage = AfaStrings.InvalidServerURL;
				return;
			}
			if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
			{
				this.IsVerified = false;
				this.ErrorMessage = AfaStrings.InvalidServerURL;
				return;
			}
			this.URL = url;
			try
			{
				string[] separator = new string[]
				{
					"/rest/services"
				};
				string[] array = url.Split(separator, StringSplitOptions.None);
				string text = array[0] + "/rest/services";
				if (text.ToLower() == AGSLocator.ArcGISOnlineLocatorsURL)
				{
					if (AGSLocator.ArcGISOnlineLocatorConnection == null)
					{
						AGSLocator.ArcGISOnlineLocatorConnection = new AGSConnection("ArcGIS Online Locators", AGSLocator.ArcGISOnlineLocatorsURL);
					}
					this.ParentConnection = AGSLocator.ArcGISOnlineLocatorConnection;
				}
				else if (text.ToLower() == AGSLocator.ArcGISComLocatorsURL)
				{
					if (AGSLocator.ArcGISComLocatorConnection == null)
					{
						AGSLocator.ArcGISComLocatorConnection = new AGSConnection("ArcGIS.Com Locators", AGSLocator.ArcGISComLocatorsURL);
					}
					this.ParentConnection = AGSLocator.ArcGISComLocatorConnection;
				}
				else if (text.ToLower() == AGSLocator.ArcGISGeocoderURL)
				{
					if (AGSLocator.ArcGISGeocoderConnection == null)
					{
						AGSLocator.ArcGISGeocoderConnection = new AGSConnection("ArcGIS Geocoder", AGSLocator.ArcGISGeocoderURL);
					}
				}
				else
				{
					this.ParentConnection = AGSConnection.FindExistingConnection(text);
					if (this.ParentConnection == null)
					{
						try
						{
							this.ParentConnection = new AGSConnection(name + " Connection", text);
							if (this.ParentConnection == null)
							{
								this.IsVerified = false;
								this.ErrorMessage = AfaStrings.InvalidServerURL;
								return;
							}
							App.Connections.Add(this.ParentConnection);
						}
						catch
						{
							this.IsVerified = false;
							this.ErrorMessage = AfaStrings.InvalidServerURL;
							return;
						}
					}
				}
			}
			catch
			{
				this.IsVerified = false;
				this.ErrorMessage = AfaStrings.InvalidServerURL;
				return;
			}
			this.Properties = new Dictionary<string, string>();
			if (!this.GetServerInfo())
			{
				this.IsVerified = false;
				return;
			}
			this.IsVerified = true;
		}

		private bool GetServerInfo()
		{
			bool result;
			try
			{
				this.IsVerified = false;
				this.ErrorMessage = "";
				if (string.IsNullOrEmpty(this.URL))
				{
					this.ErrorMessage = AfaStrings.InvalidServerURL;
					result = false;
				}
				else
				{
					string uRL = this.URL;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("?f={0}", "json");
					this.IsTokenBasedSecurity = false;
					IDictionary<string, object> dictionary = this.ParentConnection.MakeDictionaryRequest(uRL + stringBuilder);
					if (dictionary == null)
					{
						ErrorReport.ShowErrorMessage(AfaStrings.InvalidLocationService);
						result = false;
					}
					else if (!dictionary.ContainsKey("singleLineAddressField"))
					{
						ErrorReport.ShowErrorMessage(AfaStrings.InvalidLocationService);
						result = false;
					}
					else
					{
						if (dictionary.ContainsKey("serviceDescription"))
						{
							try
							{
								object obj;
								dictionary.TryGetValue("serviceDescription", out obj);
								this.Description = (obj as string);
							}
							catch
							{
								this.Description = null;
							}
						}
						this.IsVerified = true;
						result = true;
					}
				}
			}
			catch
			{
				this.ErrorMessage = AfaStrings.ErrorConnectingToServer;
				result = false;
			}
			return result;
		}

		public static AGSLocator ReadFromFile(string fileName)
		{
			AGSLocator result;
			try
			{
				using (StreamReader streamReader = new StreamReader(fileName))
				{
					string text = streamReader.ReadLine();
					short.Parse(text);
					string name = streamReader.ReadLine();
					string url = streamReader.ReadLine();
					AGSLocator aGSLocator = new AGSLocator(name, url);
					text = streamReader.ReadLine();
					bool flag = bool.Parse(text);
					if (flag)
					{
						aGSLocator.UserName = streamReader.ReadLine();
					}
					result = aGSLocator;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static string LocatorFolderLocation()
		{
			string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			text = Path.Combine(text, "ESRI");
			text = Path.Combine(text, "ArcGIS for AutoCAD");
			text = Path.Combine(text, "1.0");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			text = Path.Combine(text, "Locators");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		public static void LoadLocators()
		{
			string path = AGSLocator.LocatorFolderLocation();
			try
			{
				string[] files = Directory.GetFiles(path, "*.agl");
				if (files.Length == 0)
				{
					AGSLocator.CreateDefaultLocators();
					files = Directory.GetFiles(path, "*.agl");
				}
				string[] array = files;
				for (int i = 0; i < array.Length; i++)
				{
					string fileName = array[i];
					AGSLocator aGSLocator = AGSLocator.ReadFromFile(fileName);
					if (aGSLocator != null && aGSLocator.IsVerified)
					{
						App.Locators.Add(aGSLocator);
						if (aGSLocator.Name == "ArcGIS World Geocoder")
						{
							App.DefaultLocator = aGSLocator;
						}
					}
				}
				if (App.DefaultLocator == null && App.Locators.Count > 0)
				{
					App.DefaultLocator = App.Locators[0];
				}
			}
			catch
			{
			}
		}

		public static void CreateDefaultLocators()
		{
			AGSLocator.CreateDefaultGeocoder();
		}

		public static string CreateDefaultGeocoder()
		{
			string text = "ArcGIS World Geocoder";
			string value = " http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer";
			string result;
			try
			{
				string path = AGSLocator.LocatorFolderLocation();
				string text2 = Path.Combine(path, text + ".AGL");
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(AGSLocator.cVer.ToString());
				stringBuilder.AppendLine(text);
				stringBuilder.AppendLine(value);
				stringBuilder.AppendLine(false.ToString());
				using (StreamWriter streamWriter = new StreamWriter(text2))
				{
					streamWriter.Write(stringBuilder.ToString());
				}
				result = text2;
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorSavingConnectionFile);
				result = "";
			}
			return result;
		}

		public static string CreateDefaultESRIWorldPlacesLocator()
		{
			string text = "ESRI World Places";
			string value = "http://tasks.arcgisonline.com/ArcGIS/rest/services/Locators/ESRI_Places_World/GeocodeServer/";
			string result;
			try
			{
				string path = AGSLocator.LocatorFolderLocation();
				string text2 = Path.Combine(path, text + ".AGL");
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(AGSLocator.cVer.ToString());
				stringBuilder.AppendLine(text);
				stringBuilder.AppendLine(value);
				stringBuilder.AppendLine(false.ToString());
				using (StreamWriter streamWriter = new StreamWriter(text2))
				{
					streamWriter.Write(stringBuilder.ToString());
				}
				result = text2;
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorSavingConnectionFile);
				result = "";
			}
			return result;
		}

		public static string CreateDefaultWorldLocator()
		{
			string text = "ArcGIS Online World Locator";
			string value = "http://tasks.arcgis.com/ArcGIS/rest/services/WorldLocator/GeocodeServer/";
			string result;
			try
			{
				string path = AGSLocator.LocatorFolderLocation();
				string text2 = Path.Combine(path, text + ".AGL");
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(AGSLocator.cVer.ToString());
				stringBuilder.AppendLine(text);
				stringBuilder.AppendLine(value);
				stringBuilder.AppendLine(false.ToString());
				using (StreamWriter streamWriter = new StreamWriter(text2))
				{
					streamWriter.Write(stringBuilder.ToString());
				}
				result = text2;
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorSavingConnectionFile);
				result = "";
			}
			return result;
		}

		public static string CreateNorthAmericanAddressLocator()
		{
			string text = "ArcGIS North American Address Locator";
			string value = "http://tasks.arcgisonline.com/ArcGIS/rest/services/Locators/TA_Address_NA_10/GeocodeServer/";
			string result;
			try
			{
				string path = AGSLocator.LocatorFolderLocation();
				string text2 = Path.Combine(path, text + ".AGL");
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(AGSLocator.cVer.ToString());
				stringBuilder.AppendLine(text);
				stringBuilder.AppendLine(value);
				stringBuilder.AppendLine(false.ToString());
				using (StreamWriter streamWriter = new StreamWriter(text2))
				{
					streamWriter.Write(stringBuilder.ToString());
				}
				result = text2;
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorSavingConnectionFile);
				result = "";
			}
			return result;
		}
	}
}
