using System;
using System.ComponentModel;
using System.Net;

namespace ArcGISExtensions
{
	public static class Extensions
	{
		public static void DownloadStringAsync2(this WebClient request, Uri address, DownloadStringCompletedEventHandler2 callback)
		{
			request.DownloadStringAsync2(address, callback, null);
		}

		public static void DownloadStringAsync2(this WebClient request, Uri address, DownloadStringCompletedEventHandler2 callback, object userToken)
		{
			request.DownloadStringAsync2Internal(address, callback, userToken, 1);
		}

		private static void DownloadStringAsync2Internal(this WebClient request, Uri address, DownloadStringCompletedEventHandler2 callback, object userToken, int attempt)
		{
			if (attempt == 1)
			{
				request.DownloadStringCompleted += delegate(object sender, DownloadStringCompletedEventArgs e)
				{
					if (e.Error != null && attempt == 1)
					{
						try
						{
							WebException ex = (WebException)e.Error;
							HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
							if (httpWebResponse.StatusCode == HttpStatusCode.ProxyAuthenticationRequired)
							{
								request.Proxy = new WebProxy();
								request.UseDefaultCredentials = true;
								request.Proxy.Credentials = CredentialCache.DefaultCredentials;
								request.DownloadStringAsync2Internal(address, callback, userToken, ++attempt);
								return;
							}
						}
						catch
						{
						}
					}
					CallState callState = e.UserState as CallState;
					DownloadStringCompletedEventHandler2 downloadStringCompletedEventHandler = callState.Callback as DownloadStringCompletedEventHandler2;
					DownloadStringCompletedEventArgs2 e2 = new DownloadStringCompletedEventArgs2(e.Result, e.Error, e.Cancelled, callState.Data);
					downloadStringCompletedEventHandler(callState.Data, e2);
				};
			}
			request.DownloadStringAsync(address, new CallState
			{
				Callback = callback,
				Data = userToken
			});
		}

		public static void DownloadFileAsync2(this WebClient request, Uri address, string fileName, AsyncCompletedEventHandler callback)
		{
			request.DownloadFileAsync2(address, fileName, null, callback);
		}

		public static void DownloadFileAsync2(this WebClient request, Uri address, string fileName, object userToken, AsyncCompletedEventHandler callback)
		{
			request.DownloadFileAsync2Internal(address, fileName, userToken, callback, 1);
		}

		private static void DownloadFileAsync2Internal(this WebClient request, Uri address, string fileName, object userToken, AsyncCompletedEventHandler callback, int attempt)
		{
			if (attempt == 1)
			{
				request.DownloadFileCompleted += delegate(object sender, AsyncCompletedEventArgs e)
				{
					if (e.Error != null && attempt == 1)
					{
						try
						{
							WebException ex = (WebException)e.Error;
							HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
							if (httpWebResponse.StatusCode == HttpStatusCode.ProxyAuthenticationRequired)
							{
								request.Proxy = new WebProxy();
								request.UseDefaultCredentials = true;
								request.Proxy.Credentials = CredentialCache.DefaultCredentials;
								request.DownloadFileAsync2Internal(address, fileName, userToken, callback, ++attempt);
								return;
							}
						}
						catch
						{
						}
					}
					CallState callState = e.UserState as CallState;
					AsyncCompletedEventHandler asyncCompletedEventHandler = callState.Callback as AsyncCompletedEventHandler;
					AsyncCompletedEventArgs e2 = new AsyncCompletedEventArgs(e.Error, e.Cancelled, callState.Data);
					asyncCompletedEventHandler(callState.Data, e2);
				};
			}
			request.DownloadFileAsync(address, fileName, new CallState
			{
				Callback = callback,
				Data = userToken
			});
		}
	}
}
