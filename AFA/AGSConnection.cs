using AFA.Resources;
using AFA.UI;
using ArcGIS10Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace AFA
{
	public class AGSConnection : ILongProcessCommand
	{
		private static short cVer = 1;

		public static bool bConnectionsLoaded = false;

		public bool FoldersLoaded;

		public bool PropertiesLoaded;

		public bool IsPortal;

		private AGSGeometryServer localGeometryServer;

		private bool IsTokenBasedSecurity;

		private ProgressIndicator _progress;

		private static bool bGettingToken;

		private bool ProxyChecked;

		private bool ProxyState;

		private int TokenErrorCount;

		public event CommandStartedEventHandler CommandStarted;

		public event CommandEndedEventHandler CommandEnded;

		public event CommandProgressEventHandler CommandProgress;

		public event CommandProgressUpdateValuesEventHandler CommandUpdateProgressValues;

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

		public string Soap_URL
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

		public string Token
		{
			get;
			private set;
		}

		public bool PromptForCredentials
		{
			get;
			set;
		}

		public bool ConnectionFailed
		{
			get;
			set;
		}

		public IDictionary<string, string> Properties
		{
			get;
			set;
		}

		public IAGSFolder folder
		{
			get;
			set;
		}

		public string Version
		{
			get;
			set;
		}

		public ICredentials Credentials
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public AGSGeometryServer GeometryService
		{
			get
			{
				if (this.localGeometryServer == null)
				{
					return AGSGeometryServer.GetSampleServer();
				}
				return this.localGeometryServer;
			}
			set
			{
				this.localGeometryServer = value;
			}
		}

		private string TokenServicesURL
		{
			get;
			set;
		}

		private static string ReplaceSpecialCharacters(string inputString)
		{
			string text = inputString.Trim();
			text = text.Replace("[:; \\/:*?\"<>|&']", "_");
			text = text.Replace(":", "_");
			text = text.Replace(";", "_");
			return text.Replace("&", "_");
		}

		private AGSConnection()
		{
			this.IsTokenBasedSecurity = false;
			this.PromptForCredentials = true;
			this.Version = "";
		}

		public AGSConnection(string name, string TestURL)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception(AfaStrings.InvalidConnectionName);
			}
			this.Name = name;
			this.ErrorMessage = "";
			this.Version = "";
			this.PromptForCredentials = true;
			this.SetURL(TestURL);
			this.folder = new AGSFolder(name, this.URL, this);
			this.Properties = new Dictionary<string, string>();
		}

		public bool SetURL(string TestURL)
		{
			if (!Uri.IsWellFormedUriString(TestURL, UriKind.Absolute))
			{
				return false;
			}
			int num = TestURL.ToLower().LastIndexOf("/rest/services");
			if (num != -1)
			{
				this.URL = TestURL;
				this.Soap_URL = TestURL.Substring(0, num) + "/services";
				return true;
			}
			this.Soap_URL = TestURL;
			num = this.Soap_URL.ToLower().LastIndexOf("/services");
			if (num == -1)
			{
				this.ConnectionFailed = false;
				this.ErrorMessage = AfaStrings.InvalidServerURL;
				return false;
			}
			this.URL = TestURL.ToLower().Substring(0, num) + "/rest/services";
			return false;
		}

		public static string FixName(string name)
		{
			string text = AGSConnection.ReplaceSpecialCharacters(name);
			int num = 1;
			while (AGSConnection.NameAlreadyExists(text))
			{
				text += num.ToString();
				num++;
			}
			return text;
		}

		public static string BuildConnectionURL(string mapServiceURL)
		{
			string result;
			try
			{
				Uri uri = new Uri(mapServiceURL);
				string host = uri.Host;
				string[] segments = uri.Segments;
				string text = "http://" + host;
				if (!uri.IsDefaultPort)
				{
					text = text + ":" + uri.Port.ToString();
				}
				string[] array = segments;
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i];
					if (!(text2.ToLower() == "rest/"))
					{
						if (text2.ToLower() == "services/")
						{
							text += text2;
							break;
						}
						text += text2;
					}
				}
				string text3 = text;
				result = text3;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static bool NameAlreadyExists(string testName)
		{
			if (!AGSConnection.bConnectionsLoaded)
			{
				AGSConnection.LoadConnections();
			}
			foreach (AGSConnection current in App.Connections)
			{
				if (current.Name == testName)
				{
					return true;
				}
			}
			return false;
		}

		public bool ContainsConnection(string Soap_URL, string userName)
		{
			Uri uri = new Uri(Soap_URL);
			string text = uri.ToString();
			char[] trimChars = new char[]
			{
				'/',
				'\\'
			};
			text = text.TrimEnd(trimChars);
			foreach (AGSConnection current in App.Connections)
			{
				Uri uri2 = new Uri(current.Soap_URL);
				string text2 = uri2.ToString();
				text2 = text2.TrimEnd(trimChars);
				if (string.Compare(text2, text, true) == 0)
				{
					if (string.IsNullOrEmpty(userName))
					{
						bool result = true;
						return result;
					}
					if (current.UserName == userName)
					{
						bool result = true;
						return result;
					}
				}
			}
			return false;
		}

		public static AGSConnection FindExistingConnection(string RestURL)
		{
			if (!AGSConnection.bConnectionsLoaded)
			{
				AGSConnection.LoadConnections();
			}
			Uri uri = new Uri(RestURL);
			string text = uri.ToString();
			char[] trimChars = new char[]
			{
				'/',
				'\\'
			};
			text = text.TrimEnd(trimChars);
			foreach (AGSConnection current in App.Connections)
			{
				Uri uri2 = new Uri(current.URL);
				string text2 = uri2.ToString();
				text2 = text2.TrimEnd(trimChars);
				if (string.Compare(text2, text, true) == 0)
				{
					return current;
				}
			}
			return null;
		}

		public static AGSConnection FindExistingConnection(string Soap_URL, string userName)
		{
			if (!AGSConnection.bConnectionsLoaded)
			{
				AGSConnection.LoadConnections();
			}
			Uri uri = new Uri(Soap_URL);
			string text = uri.ToString();
			char[] trimChars = new char[]
			{
				'/',
				'\\'
			};
			text = text.TrimEnd(trimChars);
			foreach (AGSConnection current in App.Connections)
			{
				Uri uri2 = new Uri(current.Soap_URL);
				string text2 = uri2.ToString();
				text2 = text2.TrimEnd(trimChars);
				if (string.Compare(text2, text, true) == 0)
				{
					if (string.IsNullOrEmpty(userName))
					{
						AGSConnection result = current;
						return result;
					}
					if (current.UserName == userName)
					{
						AGSConnection result = current;
						return result;
					}
				}
			}
			return null;
		}

		private static AGSConnection CreateNewConnection(string Soap_URL, string suggestedName, string userName)
		{
			string text = suggestedName;
			if (string.IsNullOrEmpty(suggestedName) && Soap_URL.StartsWith("http://"))
			{
				text = Soap_URL.Substring("http://".Length);
				int num = text.LastIndexOf("/arcgis/services");
				if (num > 0)
				{
					text = text.Remove(num);
				}
			}
			AGSConnection result;
			try
			{
				AGSConnection aGSConnection = new AGSConnection(text, Soap_URL);
				aGSConnection.Soap_URL = Soap_URL;
				aGSConnection.UserName = userName;
				if (aGSConnection.Refresh())
				{
					App.Connections.Add(aGSConnection);
					aGSConnection.SaveToFile();
				}
				result = aGSConnection;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static AGSConnection GetConnection(string Soap_URL, string suggestedName, string userName)
		{
			if (string.IsNullOrEmpty(Soap_URL))
			{
				return null;
			}
			AGSConnection aGSConnection = AGSConnection.FindExistingConnection(Soap_URL, userName);
			if (aGSConnection == null)
			{
				aGSConnection = AGSConnection.CreateNewConnection(Soap_URL, suggestedName, userName);
				Application.DoEvents();
			}
			else if (aGSConnection.ConnectionFailed)
			{
				return aGSConnection;
			}
			aGSConnection.LoadConnectionProperties();
			return aGSConnection;
		}

		public static AGSConnection ReestablishConnection(string Soap_URL, string suggestedName, string userName)
		{
			if (string.IsNullOrEmpty(Soap_URL))
			{
				return null;
			}
			AGSConnection aGSConnection = AGSConnection.FindExistingConnection(Soap_URL, userName);
			if (aGSConnection == null)
			{
				aGSConnection = AGSConnection.CreateNewConnection(Soap_URL, suggestedName, userName);
				Application.DoEvents();
			}
			aGSConnection.LoadConnectionProperties();
			return aGSConnection;
		}

		public bool ChallengeWithPort()
		{
			return this.ChallengeWithPort(true);
		}

		public bool ChallengeWithPort(bool prompt)
		{
			string soap_URL = this.Soap_URL;
			string uRL = this.URL;
			bool result;
			try
			{
				UriBuilder uriBuilder = new UriBuilder(this.Soap_URL);
				if (uriBuilder.Port == 6080)
				{
					result = false;
				}
				else
				{
					uriBuilder.Port = 6080;
					this.Soap_URL = uriBuilder.ToString();
					this.URL = new UriBuilder(this.URL)
					{
						Port = 6080
					}.ToString();
					if (!this.Challenge(prompt))
					{
						this.Soap_URL = soap_URL;
						this.URL = uRL;
						result = false;
					}
					else
					{
						this.folder = new AGSFolder(this.Name, this.URL, this);
						this.FoldersLoaded = false;
						result = true;
					}
				}
			}
			catch
			{
				this.Soap_URL = soap_URL;
				this.URL = uRL;
				result = false;
			}
			return result;
		}

		public bool Challenge()
		{
			return this.Challenge(true);
		}

		public bool Challenge(bool prompt)
		{
			string origRequestURI = this.Soap_URL + "?wsdl";
			string value = this.MakeRequest(origRequestURI, prompt);
			return !string.IsNullOrEmpty(value) || (this.ErrorMessage != AfaStrings.ConnectionCanceled && this.ChallengeWithPort(prompt));
		}

		public bool Refresh()
		{
			bool result;
			try
			{
				this.ErrorMessage = "";
				this.folder.Clear();
				this.Properties.Clear();
				this.PropertiesLoaded = false;
				this.FoldersLoaded = false;
				if (this.LoadConnectionProperties())
				{
					result = this.LoadChildren();
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

		public bool Equals(string other)
		{
			return this.Name == other || this.Soap_URL == other;
		}

		public string AppendToken(string serviceURL)
		{
			if (string.IsNullOrEmpty(this.Token))
			{
				return serviceURL;
			}
			return serviceURL + "?token=" + this.Token;
		}

		private bool InitializeCatalog()
		{
			ServicePointManager.ServerCertificateValidationCallback = ((object obj, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors) => true);
			bool flag = false;
			while (!flag)
			{
				try
				{
					Catalog catalog = new Catalog();
					catalog.Url = this.Soap_URL;
					if (!string.IsNullOrEmpty(this.Token))
					{
						Catalog expr_49 = catalog;
						expr_49.Url = expr_49.Url + "?token=" + this.Token;
					}
					catalog.Proxy = WebRequest.DefaultWebProxy;
					catalog.Credentials = this.Credentials;
					if (catalog.Credentials == null)
					{
						catalog.UseDefaultCredentials = true;
					}
					if (catalog.RequiresTokens())
					{
						this.TokenServicesURL = catalog.GetTokenServiceURL();
					}
					flag = true;
				}
				catch (WebException ex)
				{
					this.ErrorMessage = ex.Message;
					HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
					if (httpWebResponse != null && !this.ProcessAuthenticationError(httpWebResponse.StatusCode, ex.Message))
					{
						if (string.IsNullOrEmpty(this.ErrorMessage))
						{
							this.ErrorMessage = ex.Message;
						}
						this.ConnectionFailed = true;
						return false;
					}
				}
			}
			return true;
		}

		private string InitalizePortalToken(string baseUrl)
		{
			string origRequestURI = baseUrl + string.Format("?f=json&UserName={0}&Password={1}&client=requestip", this.UserName, this.Password);
			IDictionary<string, object> dictionary = this.MakeDictionaryRequest(origRequestURI);
			if (dictionary == null)
			{
				return null;
			}
			if (dictionary.ContainsKey("token"))
			{
				try
				{
					string text = dictionary["token"].ToString();
					this.IsPortal = true;
					string result = text;
					return result;
				}
				catch
				{
					string result = null;
					return result;
				}
			}
			return null;
		}

		private bool InitializeToken()
		{
			bool result;
			try
			{
				if (AGSConnection.bGettingToken)
				{
					result = false;
				}
				else
				{
					this.Token = "";
					if (string.IsNullOrEmpty(this.TokenServicesURL))
					{
						AGSConnection.bGettingToken = false;
						result = false;
					}
					else if (string.IsNullOrEmpty(this.UserName))
					{
						result = false;
					}
					else if (string.IsNullOrEmpty(this.Password))
					{
						result = false;
					}
					else
					{
						AGSConnection.bGettingToken = true;
						string origRequestURI = this.TokenServicesURL + string.Format("?request=getToken&username={0}&password={1}", this.UserName, this.Password);
						this.Token = this.MakeRequest(origRequestURI, false);
						AGSConnection.bGettingToken = false;
						if (!string.IsNullOrEmpty(this.ErrorMessage))
						{
							result = false;
						}
						else if (string.IsNullOrEmpty(this.Token))
						{
							result = false;
						}
						else
						{
							if (!char.IsLetterOrDigit(this.Token[0]))
							{
								this.Token = null;
								AGSConnection.bGettingToken = true;
								this.Token = this.InitalizePortalToken(this.TokenServicesURL);
								AGSConnection.bGettingToken = false;
							}
							result = true;
						}
					}
				}
			}
			catch (SystemException)
			{
				result = false;
			}
			return result;
		}

		public bool DownloadFile(string address, string fileName)
		{
			bool flag = false;
			bool result;
			while (!flag)
			{
				try
				{
					WebClient webClient = new WebClient();
					webClient.Proxy = WebRequest.DefaultWebProxy;
					webClient.Credentials = this.Credentials;
					if (webClient.Credentials == null)
					{
						webClient.UseDefaultCredentials = true;
					}
					webClient.DownloadFile(address, fileName);
					App.TempFiles.Add(fileName);
					flag = true;
					result = true;
					return result;
				}
				catch (WebException ex)
				{
					this.ErrorMessage = ex.Message;
					HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
					if (httpWebResponse == null)
					{
						ErrorReport.ShowErrorMessage(ex.Message);
						result = false;
						return result;
					}
					if (!this.ProcessAuthenticationError(httpWebResponse.StatusCode, ex.Message) && !string.IsNullOrEmpty(this.ErrorMessage))
					{
						ErrorReport.ShowErrorMessage(this.ErrorMessage);
						this.ErrorMessage = ex.Message;
						this.ConnectionFailed = true;
						result = false;
						return result;
					}
				}
			}
			try
			{
				FileAttributes attributes = File.GetAttributes(fileName);
				if (attributes == FileAttributes.Normal)
				{
					result = true;
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

		public bool CheckResultsForError(IDictionary<string, object> results, out string errString)
		{
			errString = "";
			if (results.ContainsKey("error"))
			{
				try
				{
					IDictionary<string, object> dictionary = (IDictionary<string, object>)results["error"];
					if (dictionary.ContainsKey("message"))
					{
						errString = dictionary["message"].ToString();
					}
					bool result = true;
					return result;
				}
				catch
				{
					bool result = true;
					return result;
				}
				return false;
			}
			return false;
		}

		public IDictionary<string, object> MakeDictionaryRequest(string origRequestURI)
		{
			this.TokenErrorCount = 0;
			string input = this.MakeRequest(origRequestURI);
			IDictionary<string, object> result;
			try
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				IDictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(input) as IDictionary<string, object>;
				result = dictionary;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private string ExtractErrorMessage(string errorString)
		{
			string text = errorString;
			string result;
			try
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				IDictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(errorString) as IDictionary<string, object>;
				IDictionary<string, object> dictionary2 = dictionary["error"] as IDictionary<string, object>;
				object obj;
				if (dictionary2.TryGetValue("message", out obj))
				{
					text = obj.ToString();
				}
				result = text;
			}
			catch
			{
				result = text;
			}
			return result;
		}

		private bool ExtractStatusCode(string responseString, out HttpStatusCode errorCode, out string errorMessage)
		{
			errorCode = HttpStatusCode.OK;
			errorMessage = "";
			bool result;
			try
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				IDictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(responseString) as IDictionary<string, object>;
				IDictionary<string, object> dictionary2 = dictionary["error"] as IDictionary<string, object>;
				object obj;
				if (dictionary2.TryGetValue("code", out obj))
				{
					errorCode = (HttpStatusCode)obj;
				}
				if (dictionary2.TryGetValue("message", out obj))
				{
					errorMessage = obj.ToString();
				}
				result = true;
			}
			catch (SystemException)
			{
				result = false;
			}
			return result;
		}

		private bool ProcessTokenError()
		{
			if (!this.IsTokenBasedSecurity)
			{
				return true;
			}
			if (AGSConnection.bGettingToken)
			{
				return false;
			}
			bool flag = false;
			if (this._progress != null)
			{
				this._progress.HideWindow();
			}
			while (!flag)
			{
				if (!string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Password))
				{
					if (this.InitializeToken())
					{
						if (this._progress != null)
						{
							this._progress.ShowWindow();
						}
						this.ErrorMessage = "";
						return true;
					}
					this.Password = "";
				}
				NetworkCredential networkCredential;
				if (!this.PromptForCredentials || !AFA.UI.PromptForCredentials.GetCredentials(AfaStrings.TokenAuthentication, AfaStrings.TokenCredentialRequest, this.UserName, out networkCredential))
				{
					this.Password = "";
					if (this._progress != null)
					{
						this._progress.ShowWindow();
					}
					this.ErrorMessage = AfaStrings.ConnectionCanceled;
					return false;
				}
				this.UserName = networkCredential.UserName;
				this.Password = networkCredential.Password;
				if (this.InitializeToken())
				{
					this.ErrorMessage = "";
					if (this._progress != null)
					{
						this._progress.ShowWindow();
					}
					return true;
				}
				this.Password = "";
			}
			if (this._progress != null)
			{
				this._progress.ShowWindow();
			}
			return false;
		}

		private bool ProcessAuthenticationError(HttpStatusCode code, string errorMessage)
		{
			if (this.TokenErrorCount > 4)
			{
				return false;
			}
			bool result;
			try
			{
				HttpStatusCode httpStatusCode;
				string text;
				if (code == HttpStatusCode.OK && this.ExtractStatusCode(errorMessage, out httpStatusCode, out text) && (httpStatusCode == (HttpStatusCode)499 || httpStatusCode == (HttpStatusCode)498))
				{
					code = (HttpStatusCode)499;
				}
				if (this._progress != null)
				{
					this._progress.HideWindow();
				}
				if (code == HttpStatusCode.ProxyAuthenticationRequired)
				{
					string userName = "";
					NetworkCredential credentials;
					if (!this.PromptForCredentials || !AFA.UI.PromptForCredentials.GetCredentials(AfaStrings.ProxyAuthentication, AfaStrings.ProxyCredentialRequest, userName, out credentials))
					{
						this.ErrorMessage = AfaStrings.ConnectionCanceled;
						if (this._progress != null)
						{
							this._progress.ShowWindow();
						}
						result = false;
						return result;
					}
					if (WebRequest.DefaultWebProxy == null)
					{
						WebRequest.DefaultWebProxy = new WebProxy();
					}
					WebRequest.DefaultWebProxy.Credentials = credentials;
				}
				else if (code == HttpStatusCode.Unauthorized)
				{
					string userName2 = this.UserName;
					NetworkCredential credentials2;
					if (!this.PromptForCredentials || !AFA.UI.PromptForCredentials.GetCredentials(AfaStrings.NetworkAuthentication, AfaStrings.NetworkCredentialRequest, userName2, out credentials2))
					{
						this.ErrorMessage = AfaStrings.ConnectionCanceled;
						result = false;
						return result;
					}
					this.Credentials = credentials2;
				}
				else
				{
					if (code.ToString() == "499" || code.ToString() == "498")
					{
						this.TokenErrorCount++;
						this.IsTokenBasedSecurity = true;
						this.Token = "";
						result = this.ProcessTokenError();
						return result;
					}
					this.ErrorMessage = this.ExtractErrorMessage(errorMessage);
					result = false;
					return result;
				}
				this.ErrorMessage = "";
				if (this._progress != null)
				{
					this._progress.ShowWindow();
				}
				result = true;
			}
			catch (SystemException ex)
			{
				this.ErrorMessage = ex.Message;
				result = false;
			}
			return result;
		}

		public string MakeRequest(string origRequestURI)
		{
			return this.MakeRequest(origRequestURI, true);
		}

		private string DecodeErrorResponse(string responseString)
		{
			string result;
			try
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				IDictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(responseString) as IDictionary<string, object>;
				object obj;
				if (dictionary.TryGetValue("error", out obj))
				{
					IDictionary<string, object> dictionary2 = obj as IDictionary<string, object>;
					object obj2;
					if (dictionary2.TryGetValue("message", out obj2))
					{
						result = obj2.ToString();
						return result;
					}
				}
				result = "";
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private bool DetectError(string responseString)
		{
			string value = "{\"error";
			return responseString.StartsWith(value);
		}

		private int GetErrorCode(string responseString)
		{
			int result;
			try
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				IDictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(responseString) as IDictionary<string, object>;
				if (dictionary.ContainsKey("code"))
				{
					int num = int.Parse(dictionary["code"].ToString());
					result = num;
				}
				else
				{
					if (dictionary.ContainsKey("error"))
					{
						IDictionary<string, object> dictionary2 = dictionary["error"] as IDictionary<string, object>;
						if (dictionary2.ContainsKey("code"))
						{
							int num2 = int.Parse(dictionary2["code"].ToString());
							result = num2;
							return result;
						}
					}
					result = 0;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		private bool IsProxyPresent(string origRequestURI)
		{
			if (!this.ProxyChecked)
			{
				Uri uri = new Uri(origRequestURI);
				string b = uri.ToString();
				string a = WebRequest.DefaultWebProxy.GetProxy(uri).ToString();
				this.ProxyChecked = true;
				return !(a == b);
			}
			return this.ProxyState;
		}

		public string MakeRequest(string origRequestURI, bool prompt)
		{
			WebResponse webResponse = null;
			string result;
			try
			{
				ServicePointManager.ServerCertificateValidationCallback = ((object obj, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors) => true);
				string text = origRequestURI;
				bool flag = false;
				string text2 = "";
				while (!flag)
				{
					if (!string.IsNullOrEmpty(this.Token))
					{
						text = origRequestURI + "&token=" + this.Token;
					}
					WebRequest webRequest = WebRequest.Create(text);
					if (this.IsProxyPresent(origRequestURI))
					{
						Uri proxy = WebRequest.DefaultWebProxy.GetProxy(new Uri(text));
						webRequest.Proxy = new WebProxy(proxy)
						{
							Credentials = WebRequest.DefaultWebProxy.Credentials
						};
					}
					webRequest.Credentials = this.Credentials;
					ICredentials arg_AB_0 = this.Credentials;
					try
					{
						webResponse = webRequest.GetResponse();
						HttpStatusCode httpStatusCode = HttpStatusCode.OK;
						if (httpStatusCode == HttpStatusCode.OK)
						{
							StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());
							text2 = streamReader.ReadToEnd();
							if (this.DetectError(text2))
							{
								int errorCode = this.GetErrorCode(text2);
								if (errorCode != 0)
								{
									httpStatusCode = (HttpStatusCode)errorCode;
								}
								flag = false;
								if (this.IsPortal)
								{
									this.ErrorMessage = "This connection type not supported.";
									this.ConnectionFailed = true;
									result = "";
									return result;
								}
								if (!prompt)
								{
									if (string.IsNullOrEmpty(this.ErrorMessage))
									{
										this.ErrorMessage = AfaStrings.ErrorConnectionRequiresToken;
									}
									this.ConnectionFailed = true;
									result = "";
									return result;
								}
								if (!this.ProcessAuthenticationError(httpStatusCode, text2))
								{
									if (string.IsNullOrEmpty(this.ErrorMessage))
									{
										this.ErrorMessage = AfaStrings.ErrorConnectionRequiresToken;
									}
									this.ConnectionFailed = true;
									result = "";
									return result;
								}
							}
							else
							{
								this.ErrorMessage = "";
								flag = true;
							}
						}
					}
					catch (WebException ex)
					{
						if (!string.IsNullOrEmpty(ex.Message))
						{
							this.ErrorMessage = ex.Message;
						}
						HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
						if (httpWebResponse == null)
						{
							this.ErrorMessage = ex.Message;
							this.ConnectionFailed = true;
							result = "";
							return result;
						}
						if (!prompt)
						{
							if (string.IsNullOrEmpty(this.ErrorMessage))
							{
								this.ErrorMessage = ex.Message;
							}
							this.ConnectionFailed = true;
							result = "";
							return result;
						}
						if (!this.ProcessAuthenticationError(httpWebResponse.StatusCode, ex.Message))
						{
							if (string.IsNullOrEmpty(this.ErrorMessage))
							{
								this.ErrorMessage = ex.Message;
							}
							this.ConnectionFailed = true;
							result = "";
							return result;
						}
					}
				}
				if (webResponse != null)
				{
					webResponse.Close();
				}
				this.ErrorMessage = "";
				result = text2;
			}
			catch (SystemException ex2)
			{
				this.ConnectionFailed = true;
				this.ErrorMessage = ex2.Message;
				result = "";
			}
			return result;
		}

		public bool LoadChildren()
		{
			this.ErrorMessage = "";
			bool result;
			using (this._progress = new ProgressIndicator(this))
			{
				if (this.CommandStarted != null)
				{
					CommandStartEventArgs commandStartEventArgs = new CommandStartEventArgs();
					commandStartEventArgs.WindowTitle = AfaStrings.CollectingServiceInformation;
					commandStartEventArgs.ProgressMaxValue = 0;
					commandStartEventArgs.ProgressMinValue = 0;
					commandStartEventArgs.PrograssInitValue = 0;
					this.CommandStarted(this, commandStartEventArgs);
					this.ReportExportStatus(AfaStrings.Initializing + " " + this.Name, "");
				}
				if (this.ReportCheckCancel())
				{
					this.CommandEnded(this, EventArgs.Empty);
					this.ErrorMessage = AfaStrings.CommandCancelled;
					result = false;
				}
				else
				{
					try
					{
						if (this.PopulateProperties(this.Properties))
						{
							if (this.ReportCheckCancel())
							{
								this.ErrorMessage = AfaStrings.CommandCancelled;
								result = false;
							}
							else
							{
								this.ReportExportStatus(AfaStrings.LoadingFolders);
								if (this.folder.LoadChildren())
								{
									this.FoldersLoaded = true;
								}
								if (this.ConnectionFailed || this.ReportCheckCancel())
								{
									this.FoldersLoaded = false;
									if (!this.ConnectionFailed)
									{
										this.ErrorMessage = AfaStrings.CommandCancelled;
									}
									this.CommandEnded(this, EventArgs.Empty);
									this._progress = null;
									result = false;
								}
								else
								{
									this.PropertiesLoaded = true;
									this.ConnectionFailed = false;
									this.CommandEnded(this, EventArgs.Empty);
									this._progress = null;
									result = true;
								}
							}
						}
						else
						{
							this.CommandEnded(this, EventArgs.Empty);
							this._progress = null;
							result = false;
						}
					}
					catch (SystemException ex)
					{
						this.ErrorMessage = ex.Message;
						this.PropertiesLoaded = false;
						this.FoldersLoaded = false;
						this.ConnectionFailed = true;
						if (this.CommandStarted != null)
						{
							this.CommandEnded(this, EventArgs.Empty);
						}
						this._progress = null;
						result = false;
					}
				}
			}
			return result;
		}

		public bool LoadConnectionProperties()
		{
			if (this.PropertiesLoaded)
			{
				return true;
			}
			this.ConnectionFailed = false;
			this.ErrorMessage = "";
			if (string.IsNullOrEmpty(this.URL))
			{
				this.ErrorMessage = AfaStrings.InvalidServerURL;
				return false;
			}
			string arg = Regex.Replace(this.URL, "arcgis/rest/services", "arcgis/rest/info");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("?f={0}", "json");
			string text = "";
			try
			{
				text = this.MakeRequest(arg + stringBuilder);
				if (string.IsNullOrEmpty(text))
				{
					this.ConnectionFailed = true;
					if (string.IsNullOrEmpty(this.ErrorMessage))
					{
						this.ErrorMessage = AfaStrings.UnableToQueryServer;
					}
					bool result = false;
					return result;
				}
			}
			catch (SystemException ex)
			{
				this.ConnectionFailed = true;
				this.ErrorMessage = ex.Message;
				bool result = false;
				return result;
			}
			try
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				IDictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(text) as IDictionary<string, object>;
				object obj;
				if (dictionary.TryGetValue("currentVersion", out obj))
				{
					string text2 = obj.ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						this.Version = text2;
					}
				}
				if (dictionary.ContainsKey("authInfo"))
				{
					object obj2;
					dictionary.TryGetValue("authInfo", out obj2);
					IDictionary<string, object> dictionary2 = obj2 as IDictionary<string, object>;
					if (dictionary2.ContainsKey("isTokenBasedSecurity"))
					{
						object obj3;
						dictionary2.TryGetValue("isTokenBasedSecurity", out obj3);
						if (obj3 as bool? == true)
						{
							this.IsTokenBasedSecurity = true;
						}
					}
					if (this.IsTokenBasedSecurity && dictionary2.ContainsKey("tokenServicesUrl"))
					{
						object obj4;
						dictionary2.TryGetValue("tokenServicesUrl", out obj4);
						this.TokenServicesURL = (obj4 as string);
					}
				}
				if (this.IsTokenBasedSecurity && !string.IsNullOrEmpty(this.UserName))
				{
					if (string.IsNullOrEmpty(this.UserName) || string.IsNullOrEmpty(this.Password))
					{
						NetworkCredential networkCredential;
						if (!this.PromptForCredentials || !AFA.UI.PromptForCredentials.GetCredentials(AfaStrings.TokenAuthentication, AfaStrings.TokenCredentialRequest, this.UserName, out networkCredential))
						{
							this.Password = "";
							this.ConnectionFailed = true;
							bool result = false;
							return result;
						}
						this.Password = networkCredential.Password;
						this.UserName = networkCredential.UserName;
					}
					if (string.IsNullOrEmpty(this.Password) || string.IsNullOrEmpty(this.UserName))
					{
						this.ErrorMessage = AfaStrings.UnableToGenerateToken;
						this.ConnectionFailed = true;
						bool result = false;
						return result;
					}
					if (!this.InitializeToken())
					{
						this.Password = "";
						this.ConnectionFailed = true;
						bool result = false;
						return result;
					}
				}
			}
			catch
			{
				this.IsTokenBasedSecurity = false;
			}
			if (!this.InitializeCatalog())
			{
				this.ConnectionFailed = true;
				return false;
			}
			this.ConnectionFailed = false;
			return true;
		}

		private bool PopulateProperties(IDictionary<string, string> results)
		{
			bool result;
			try
			{
				if (this.Properties == null)
				{
					this.Properties = new Dictionary<string, string>();
				}
				if (this.Properties != null)
				{
					this.Properties.Clear();
					this.Properties.Add(AfaStrings.Type, AfaStrings.ServerConnection);
					result = true;
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

		public void SaveToFile()
		{
			try
			{
				string path = AGSConnection.ConnectionFolderLocation();
				string text = this.Name;
				char[] invalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
				char[] array = invalidFileNameChars;
				for (int i = 0; i < array.Length; i++)
				{
					char oldChar = array[i];
					text = text.Replace(oldChar, '_');
				}
				string path2 = System.IO.Path.Combine(path, text + ".AGS");
				using (FileStream fileStream = new FileStream(path2, FileMode.Create))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						binaryWriter.Write(AGSConnection.cVer);
						binaryWriter.Write(this.Name);
						binaryWriter.Write(this.Soap_URL);
						binaryWriter.Write(this.URL);
						bool flag = !string.IsNullOrEmpty(this.UserName);
						binaryWriter.Write(flag);
						if (flag)
						{
							binaryWriter.Write(this.UserName);
						}
						binaryWriter.Close();
					}
				}
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorSavingConnectionFile);
			}
		}

		public void RemoveFile()
		{
			try
			{
				string path = AGSConnection.ConnectionFolderLocation();
				string path2 = System.IO.Path.Combine(path, this.Name + ".AGS");
				File.Delete(path2);
			}
			catch
			{
			}
		}

		public static AGSConnection ReadFromFile(string fileName)
		{
			AGSConnection result;
			try
			{
				using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
				{
					using (BinaryReader binaryReader = new BinaryReader(fileStream))
					{
						binaryReader.ReadInt16();
						string name = binaryReader.ReadString();
						string testURL = binaryReader.ReadString();
						AGSConnection aGSConnection = new AGSConnection(name, testURL);
						aGSConnection.URL = binaryReader.ReadString();
						bool flag = binaryReader.ReadBoolean();
						if (flag)
						{
							aGSConnection.UserName = binaryReader.ReadString();
						}
						binaryReader.Close();
						result = aGSConnection;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static string ConnectionFolderLocation()
		{
			string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			text = System.IO.Path.Combine(text, "ESRI");
			text = System.IO.Path.Combine(text, "ArcGIS for AutoCAD");
			text = System.IO.Path.Combine(text, "1.0");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			text = System.IO.Path.Combine(text, "Connections");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		private void ReadAllConnections()
		{
			string path = AGSConnection.ConnectionFolderLocation();
			using (this._progress = new ProgressIndicator(this))
			{
				if (this.CommandStarted != null)
				{
					CommandStartEventArgs commandStartEventArgs = new CommandStartEventArgs();
					commandStartEventArgs.WindowTitle = AfaStrings.InitializingConnections;
					commandStartEventArgs.ProgressMaxValue = 0;
					commandStartEventArgs.ProgressMinValue = 0;
					commandStartEventArgs.PrograssInitValue = 0;
					this.CommandStarted(this, commandStartEventArgs);
					this.ReportExportStatus(AfaStrings.InitializingConnections, "");
				}
				if (this.ReportCheckCancel())
				{
					this.CommandEnded(this, EventArgs.Empty);
				}
				else
				{
					try
					{
						string[] files = Directory.GetFiles(path, "*.ags");
						if (files.Length == 0)
						{
							AGSConnection.CreateArcGISOnlineConnectionFile();
							files = Directory.GetFiles(path, "*.ags");
						}
						string[] array = files;
						for (int i = 0; i < array.Length; i++)
						{
							string fileName = array[i];
							AGSConnection aGSConnection = AGSConnection.ReadFromFile(fileName);
							if (aGSConnection != null && !this.ContainsConnection(aGSConnection.Soap_URL, aGSConnection.UserName))
							{
								this.ReportExportStatus(AfaStrings.InitializingConnections, aGSConnection.Name);
								App.Connections.Add(aGSConnection);
							}
							if (this.ReportCheckCancel())
							{
								this.CommandEnded(this, EventArgs.Empty);
								this._progress = null;
								return;
							}
						}
						AGSConnection.bConnectionsLoaded = true;
						this.CommandEnded(this, EventArgs.Empty);
						this._progress = null;
					}
					catch
					{
						this.CommandEnded(this, EventArgs.Empty);
						this._progress = null;
					}
				}
			}
		}

		public static void LoadConnections()
		{
			Application.UseWaitCursor = true;
			AGSConnection aGSConnection = new AGSConnection();
			aGSConnection.ReadAllConnections();
			Application.UseWaitCursor = false;
		}

		public static AGSConnection GetArcGISOnlineConnection()
		{
			if (!AGSConnection.bConnectionsLoaded)
			{
				AGSConnection.LoadConnections();
			}
			foreach (AGSConnection current in App.Connections)
			{
				if (current.Name == "ArcGIS Online")
				{
					return current;
				}
			}
			string text = AGSConnection.CreateArcGISOnlineConnectionFile();
			if (!string.IsNullOrEmpty(text))
			{
				AGSConnection aGSConnection = AGSConnection.ReadFromFile(text);
				if (aGSConnection != null)
				{
					App.Connections.Add(aGSConnection);
					return aGSConnection;
				}
			}
			return null;
		}

		public static string CreateArcGISOnlineConnectionFile()
		{
			string text = "ArcGIS Online";
			string value = "http://services.arcgisonline.com/arcgis/services";
			string value2 = "http://services.arcgisonline.com/arcgis/rest/services/";
			string result;
			try
			{
				string path = AGSConnection.ConnectionFolderLocation();
				string text2 = text;
				char[] invalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
				char[] array = invalidFileNameChars;
				for (int i = 0; i < array.Length; i++)
				{
					char oldChar = array[i];
					text2 = text2.Replace(oldChar, '_');
				}
				string text3 = System.IO.Path.Combine(path, text2 + ".AGS");
				using (FileStream fileStream = new FileStream(text3, FileMode.Create))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						binaryWriter.Write(AGSConnection.cVer);
						binaryWriter.Write(text);
						binaryWriter.Write(value);
						binaryWriter.Write(value2);
						bool value3 = false;
						binaryWriter.Write(value3);
						binaryWriter.Close();
					}
				}
				result = text3;
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorSavingConnectionFile);
				result = "";
			}
			return result;
		}

		public void ReportExportError(string msg, bool fatalError)
		{
			if (this.CommandProgress != null)
			{
				CommandProgressEventArgs commandProgressEventArgs = new CommandProgressEventArgs();
				commandProgressEventArgs.ProgressTitle = msg;
				this.CommandProgress(this, commandProgressEventArgs);
			}
			if (fatalError)
			{
				this.ErrorMessage = msg;
			}
		}

		public void ReportExportStatus(string msg)
		{
			if (this.CommandProgress != null)
			{
				CommandProgressEventArgs commandProgressEventArgs = new CommandProgressEventArgs();
				commandProgressEventArgs.ProgressMessage = msg;
				this.CommandProgress(this, commandProgressEventArgs);
			}
		}

		public void ReportExportStatus(string title, string msg)
		{
			if (this.CommandProgress != null)
			{
				CommandProgressEventArgs commandProgressEventArgs = new CommandProgressEventArgs();
				commandProgressEventArgs.ProgressTitle = title;
				commandProgressEventArgs.ProgressMessage = msg;
				this.CommandProgress(this, commandProgressEventArgs);
			}
		}

		public void ReportIncrementProgress(string msg, int count)
		{
			if (this.CommandProgress != null)
			{
				CommandProgressEventArgs commandProgressEventArgs = new CommandProgressEventArgs();
				commandProgressEventArgs.ProgressMessage = msg;
				commandProgressEventArgs.ProgressValue = count;
				this.CommandProgress(this, commandProgressEventArgs);
			}
		}

		public bool ReportCheckCancel()
		{
			return this._progress != null && this._progress.UserCancelled();
		}

		public void ReportForceCancel()
		{
			if (this._progress != null)
			{
				this._progress.ForceCancel();
			}
		}
	}
}
