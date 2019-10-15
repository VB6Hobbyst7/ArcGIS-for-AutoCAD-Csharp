using AFA.Resources;
using AFA.UI;
using AGOBasemap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml;

namespace AFA
{
    public static class AGS_ESRIMaps
	{
		private enum esriAGOTokenOption
		{
			esriAGOTokenOptionAGO,
			esriAGOTokenOptionMSVE
		}

		private enum esriArcGISOnlineUri
		{
			esriArcGISOnlineUriBase,
			esriArcGISOnlineUriSharing,
			esriArcGISOnlineUriUpdate,
			esriArcGISOnlineUriPing,
			esriArcGISOnlineUriSpeed,
			esriArcGISOnlineBasemapQuery
		}

		private class ImageAndIndex
		{
			public Stream image
			{
				get;
				set;
			}

			public int index
			{
				get;
				set;
			}
		}

		private static string _sAGOURL = "http://www.arcgisonline.com";

		private static string _sAGOLBasemapQuery = "";

		private static OnlineSearchResponse m_searchResult;

		private static string AGOLUrl;

		private static List<OnlineSearchItem> ESRI_Maps;

		private static NetworkCredential Credentials;

		private static bool ConnectionFailed;

		private static List<OnlineSearchItem> MapList;

		private static bool ProxyChecked = false;

		private static bool ProxyState = false;

		private static bool IsProxyPresent(string origRequestURI)
		{
			if (!AGS_ESRIMaps.ProxyChecked)
			{
				Uri uri = new Uri(origRequestURI);
				string b = uri.ToString();
				string a = WebRequest.DefaultWebProxy.GetProxy(uri).ToString();
				AGS_ESRIMaps.ProxyChecked = true;
				return !(a == b);
			}
			return AGS_ESRIMaps.ProxyState;
		}

		public static string MakeWebRequest(string origRequestURI)
		{
			string text = null;
			string result;
			try
			{
				ServicePointManager.ServerCertificateValidationCallback = ((object obj, X509Certificate x509Certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors) => true);
				bool flag = false;
				while (!flag)
				{
					WebClient webClient = new WebClient();
					webClient.Headers.Add("user-agent", "ArcGIS for AutoCAD");
					webClient.Encoding = Encoding.UTF8;
					if (AGS_ESRIMaps.IsProxyPresent(origRequestURI))
					{
						Uri proxy = WebRequest.DefaultWebProxy.GetProxy(new Uri(origRequestURI));
						webClient.Proxy = new WebProxy(proxy)
						{
							Credentials = WebRequest.DefaultWebProxy.Credentials
						};
					}
					if (AGS_ESRIMaps.Credentials == null)
					{
						webClient.Credentials = CredentialCache.DefaultCredentials;
						webClient.UseDefaultCredentials = true;
					}
					try
					{
						text = webClient.DownloadString(new Uri(origRequestURI));
						flag = true;
					}
					catch (WebException ex)
					{
						HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
						string userName = "";
						if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.ProxyAuthenticationRequired)
						{
							NetworkCredential credentials;
							if (!PromptForCredentials.GetCredentials("Proxy Authentication", "To connect to the internet, you need to provide credentials", userName, out credentials))
							{
								ErrorReport.ShowErrorMessage(httpWebResponse.StatusDescription);
								AGS_ESRIMaps.ConnectionFailed = true;
								result = "";
								return result;
							}
							if (WebRequest.DefaultWebProxy == null)
							{
								WebRequest.DefaultWebProxy = new WebProxy();
							}
							WebRequest.DefaultWebProxy.Credentials = credentials;
						}
						else if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.Unauthorized)
						{
							NetworkCredential credentials2;
							if (!PromptForCredentials.GetCredentials("Network Authentication", "Message Text", userName, out credentials2))
							{
								ErrorReport.ShowErrorMessage(httpWebResponse.StatusDescription);
								AGS_ESRIMaps.ConnectionFailed = true;
								result = "";
								return result;
							}
							AGS_ESRIMaps.Credentials = credentials2;
						}
						else
						{
							AGS_ESRIMaps.ConnectionFailed = true;
							ErrorReport.ShowErrorMessage(httpWebResponse.StatusDescription);
						}
					}
				}
				AGS_ESRIMaps.ConnectionFailed = false;
				result = text;
			}
			catch
			{
				AGS_ESRIMaps.ConnectionFailed = true;
				result = "";
			}
			return result;
		}

		public static void AddESRIMapWindow()
		{
			AGS_ESRIMaps.ESRI_Maps = null;
			AGS_ESRIMaps.ConnectionFailed = false;
			Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
			try
			{
				AGS_ESRIMaps.LoadAllMapImages();
				Mouse.OverrideCursor = null;
				try
				{
					while (AGS_ESRIMaps.ESRI_Maps == null && !AGS_ESRIMaps.ConnectionFailed)
					{
                        System.Windows.Forms.Application.DoEvents();
					}
				}
				catch
				{
					ErrorReport.ShowErrorMessage("Error while waiting for map images to load");
				}
			}
			catch (SystemException ex)
			{
				Mouse.OverrideCursor = null;
				ErrorReport.ShowErrorMessage("Error loading map images. " + ex.Message);
				AGS_ESRIMaps.ConnectionFailed = true;
				return;
			}
			Mouse.OverrideCursor = null;
			if (!AGS_ESRIMaps.ConnectionFailed)
			{
				try
				{
					if (AGS_ESRIMaps.ESRI_Maps.Count > 0)
					{
                        Autodesk.AutoCAD.ApplicationServices.Core.Application.ShowModalWindow(new SelectESRIMap(AGS_ESRIMaps.ESRI_Maps));
					}
					else
					{
						ErrorReport.ShowErrorMessage("Error loading maps from ESRI.");
					}
				}
				catch
				{
					ErrorReport.ShowErrorMessage("Error displaying ESRI Maps window.");
				}
			}
		}

		public static void LoadAllMapImages()
		{
			try
			{
				AGS_ESRIMaps.AGOLUrl = AGS_ESRIMaps.GetArcGISOnlineURL();
				string basemapGroupQuery = AGS_ESRIMaps.GetBasemapGroupQuery();
				string text = AGS_ESRIMaps.AGOLUrl + "/sharing/community/groups?f=json&q=" + basemapGroupQuery;
				string origRequestURI = text.Replace(" ", "%20");
				string text2 = AGS_ESRIMaps.MakeWebRequest(origRequestURI);
				if (!AGS_ESRIMaps.ConnectionFailed && !string.IsNullOrEmpty(text2))
				{
					AGS_ESRIMaps.ProcessBasemapWindowDownload(text2);
				}
				else
				{
					ErrorReport.ShowErrorMessage(AfaStrings.UnableToQueryArcGISOnline);
				}
			}
			catch (SystemException ex)
			{
				ErrorReport.ShowErrorMessage("Error loading map images" + ex.Message);
			}
		}

		private static void ProcessBasemapWindowDownload(string str)
		{
			try
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				OnlineSearchGroupsResponse onlineSearchGroupsResponse = javaScriptSerializer.Deserialize<OnlineSearchGroupsResponse>(str);
				if (onlineSearchGroupsResponse != null && onlineSearchGroupsResponse.Results != null && onlineSearchGroupsResponse.Results.Count == 1)
				{
					string id = onlineSearchGroupsResponse.Results[0].Id;
					string text = "";
					text = text + "group:" + id + " AND (typekeywords:\"map service\") AND (tags:\"AFA250_base\")";
					string str2 = text.Replace(" ", "%20");
					string origRequestURI = AGS_ESRIMaps.AGOLUrl + "/sharing/search?f=json&sortField=numviews&sortOrder=desc&num=50&q=" + str2;
					string text2 = AGS_ESRIMaps.MakeWebRequest(origRequestURI);
					if (!AGS_ESRIMaps.ConnectionFailed && !string.IsNullOrEmpty(text2))
					{
						AGS_ESRIMaps.BasemapWindow_ProcessFeaturedItems(text2);
					}
					else
					{
						ErrorReport.ShowErrorMessage(AfaStrings.UnableToQueryArcGISOnline);
					}
				}
			}
			catch (SystemException ex)
			{
				ErrorReport.ShowErrorMessage("Error downloading map images.  " + ex.Message);
			}
		}

		private static void BasemapWindow_ProcessFeaturedItems(string str)
		{
			try
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				OnlineSearchResponse onlineSearchResponse = javaScriptSerializer.Deserialize<OnlineSearchResponse>(str);
				if (onlineSearchResponse != null)
				{
					AGS_ESRIMaps.m_searchResult = onlineSearchResponse;
					AGS_ESRIMaps.MapList = new List<OnlineSearchItem>();
					AGS_ESRIMaps.LoadImages();
					if (AGS_ESRIMaps.MapList.Count > 0)
					{
						AGS_ESRIMaps.ESRI_Maps = AGS_ESRIMaps.MapList;
					}
					else
					{
						ErrorReport.ShowErrorMessage(AfaStrings.NoArcGISOnlineMapsFound);
						AGS_ESRIMaps.ConnectionFailed = true;
					}
				}
			}
			catch (SystemException ex)
			{
				ErrorReport.ShowErrorMessage("Error processing featured items. " + ex.Message);
			}
		}

		private static BitmapImage createBitmap(Stream stream)
		{
			BitmapImage result;
			try
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = stream;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.CreateOptions = BitmapCreateOptions.None;
				bitmapImage.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable);
				bitmapImage.EndInit();
				if (!bitmapImage.IsDownloading)
				{
					bitmapImage.Freeze();
				}
				result = bitmapImage;
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.Message);
				ErrorReport.ShowErrorMessage("Error in creating bitmap.  " + ex.Message);
				AGS_ESRIMaps.ConnectionFailed = true;
				result = null;
			}
			return result;
		}

		private static void LoadImages()
		{
			try
			{
				foreach (OnlineSearchItem current in AGS_ESRIMaps.m_searchResult.Results)
				{
					if (!(current.Type != "Map Service") && current.Thumbnail != null)
					{
						string text = string.Concat(new string[]
						{
							AGS_ESRIMaps.AGOLUrl,
							"/sharing/content/items/",
							current.Id,
							"/info/",
							current.Thumbnail
						});
						current.Thumbnail = text;
						Stream cachedURLStream = AGS_ESRIMaps.getCachedURLStream(new Uri(text));
						if (cachedURLStream != null)
						{
							current.Image = AGS_ESRIMaps.createBitmap(cachedURLStream);
						}
						if (current.Image != null)
						{
							AGS_ESRIMaps.MapList.Add(current);
						}
					}
				}
			}
			catch (SystemException ex)
			{
				ErrorReport.ShowErrorMessage("Error loading images; " + ex.Message);
				AGS_ESRIMaps.ConnectionFailed = true;
			}
		}

		private static void worker_LoadImages(object sender, DoWorkEventArgs e)
		{
			try
			{
				foreach (OnlineSearchItem current in AGS_ESRIMaps.m_searchResult.Results)
				{
					if (!(current.Type != "Map Service") && current.Thumbnail != null)
					{
						string text = string.Concat(new string[]
						{
							AGS_ESRIMaps.AGOLUrl,
							"/sharing/content/items/",
							current.Id,
							"/info/",
							current.Thumbnail
						});
						current.Thumbnail = text;
						Stream cachedURLStream = AGS_ESRIMaps.getCachedURLStream(new Uri(text));
						if (cachedURLStream != null)
						{
							current.Image = AGS_ESRIMaps.createBitmap(cachedURLStream);
							current.Image.Freeze();
						}
						if (current.Image != null)
						{
							AGS_ESRIMaps.MapList.Add(current);
						}
					}
				}
			}
			catch (SystemException ex)
			{
				ErrorReport.ShowErrorMessage("Error loading images; " + ex.Message);
				AGS_ESRIMaps.ConnectionFailed = true;
			}
		}

		private static Stream getCachedURLStream(Uri uri)
		{
			Stream result;
			try
			{
				result = AGS_ESRIMaps.getCachedURLStreamInternal(uri, 1);
			}
			catch
			{
				ErrorReport.ShowErrorMessage("Error in getting cached url stream");
				AGS_ESRIMaps.ConnectionFailed = true;
				result = null;
			}
			return result;
		}

		private static Stream getCachedURLStreamInternal(Uri uri, int attempt)
		{
			Stream result;
			try
			{
				WebClient webClient = new WebClient();
				webClient.Proxy = WebRequest.DefaultWebProxy;
				webClient.Credentials = AGS_ESRIMaps.Credentials;
				if (webClient.Credentials == null)
				{
					webClient.Credentials = CredentialCache.DefaultNetworkCredentials;
					webClient.UseDefaultCredentials = true;
				}
				if (attempt == 2)
				{
					webClient.Proxy = new WebProxy();
					webClient.UseDefaultCredentials = true;
					webClient.Proxy.Credentials = CredentialCache.DefaultCredentials;
				}
				webClient.Headers.Add("user-agent", "ArcGIS for AutoCAD");
				webClient.Encoding = Encoding.UTF8;
				webClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.Default);
				Stream stream = webClient.OpenRead(uri);
				BufferedStream bufferedStream = new BufferedStream(stream);
				result = bufferedStream;
			}
			catch
			{
				if (attempt == 1)
				{
					result = AGS_ESRIMaps.getCachedURLStreamInternal(uri, ++attempt);
				}
				else
				{
					AGS_ESRIMaps.ConnectionFailed = true;
					result = null;
				}
			}
			return result;
		}

		private static void FinishedGeneratingImages(AGS_ESRIMaps.ImageAndIndex si)
		{
			try
			{
				OnlineSearchItem onlineSearchItem = AGS_ESRIMaps.m_searchResult.Results[si.index];
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = si.image;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.CreateOptions = BitmapCreateOptions.None;
				bitmapImage.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable);
				bitmapImage.EndInit();
				if (!bitmapImage.IsDownloading)
				{
					bitmapImage.Freeze();
				}
				onlineSearchItem.TheThumbnail = bitmapImage;
			}
			catch (SystemException ex)
			{
				AGS_ESRIMaps.ConnectionFailed = true;
				Trace.WriteLine(ex.Message);
			}
		}

		private static string GetBasemapGroupQuery()
		{
			return "owner:esri%20title:ESRI%20Maps%20and%20Data";
		}

		public static string GetArcGISOnlineURL()
		{
			string text = "http://www.arcgisonline.com";
			try
			{
				AGS_ESRIMaps._sAGOURL = AGS_ESRIMaps.AGOSignOn_GetURI(AGS_ESRIMaps.esriArcGISOnlineUri.esriArcGISOnlineUriBase);
				if (!string.IsNullOrEmpty(text) && text[text.Length - 1] == '/')
				{
					text = text.Substring(0, text.Length - 1);
				}
			}
			catch (SystemException)
			{
				AGS_ESRIMaps.ConnectionFailed = true;
			}
			return text;
		}

		private static string AGOSignOn_GetURI(AGS_ESRIMaps.esriArcGISOnlineUri eUriKind)
		{
			string result;
			try
			{
				AGS_ESRIMaps.InitializeURI();
				if (eUriKind != AGS_ESRIMaps.esriArcGISOnlineUri.esriArcGISOnlineUriBase)
				{
					if (eUriKind != AGS_ESRIMaps.esriArcGISOnlineUri.esriArcGISOnlineBasemapQuery)
					{
						result = "";
					}
					else
					{
						result = AGS_ESRIMaps._sAGOLBasemapQuery;
					}
				}
				else
				{
					result = AGS_ESRIMaps._sAGOURL;
				}
			}
			catch
			{
				AGS_ESRIMaps.ConnectionFailed = true;
				throw;
			}
			return result;
		}

		private static void InitializeURI()
		{
			if (AGS_ESRIMaps._sAGOURL.Length > 0)
			{
				return;
			}
			string address = "http://www.arcgis.com/arcgisuris.xml";
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				string tempFileName = Path.GetTempFileName();
				WebClient webClient = new WebClient();
				webClient.DownloadFile(address, tempFileName);
				App.TempFiles.Add(tempFileName);
				if (File.Exists(tempFileName))
				{
					xmlDocument.Load(tempFileName);
					XmlNode xmlNode = xmlDocument.SelectSingleNode("ArcGISOnlineURIList");
					if (xmlNode != null)
					{
						XmlNode xmlNode2 = xmlNode.SelectSingleNode("Base");
						AGS_ESRIMaps._sAGOURL = xmlNode2.InnerText;
						xmlNode2 = xmlNode.SelectSingleNode("BasemapQuery");
						AGS_ESRIMaps._sAGOLBasemapQuery = xmlNode2.InnerText;
					}
				}
			}
			catch
			{
				AGS_ESRIMaps.ConnectionFailed = true;
				throw;
			}
		}
	}
}
