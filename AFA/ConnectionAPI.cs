using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AFA
{
	public class ConnectionAPI
	{
		[LispFunction("esri_proxy_initialize")]
		public object ESRI_InitializeProxy(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() != 2)
				{
					result = null;
				}
				else
				{
					TypedValue typedValue = array[0];
					TypedValue typedValue2 = array[1];
					if (typedValue.TypeCode != 5005 || typedValue2.TypeCode != 5005)
					{
						result = null;
					}
					else
					{
						if (WebRequest.DefaultWebProxy == null)
						{
							WebRequest.DefaultWebProxy = new WebProxy();
						}
						WebRequest.DefaultWebProxy.Credentials = new NetworkCredential(typedValue.Value.ToString(), typedValue2.Value.ToString());
						result = LspUtil.LispTrue;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_connection_names")]
		public object ESRI_GetConnections(ResultBuffer rb)
		{
			object result;
			try
			{
				if (!AGSConnection.bConnectionsLoaded)
				{
					AGSConnection.LoadConnections();
				}
				if (App.Connections.Count > 0)
				{
					ResultBuffer resultBuffer = new ResultBuffer();
					foreach (AGSConnection current in App.Connections)
					{
						resultBuffer.Add(new TypedValue(5005, current.Name));
					}
					result = resultBuffer;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_connection_get")]
		public object ESRI_GetConnection(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() < 1)
				{
					result = null;
				}
				else
				{
					TypedValue typedValue = array[0];
					if (typedValue.TypeCode != 5005)
					{
						result = null;
					}
					else
					{
						string b = typedValue.Value.ToString();
						if (!AGSConnection.bConnectionsLoaded)
						{
							AGSConnection.LoadConnections();
						}
						if (App.Connections.Count > 0)
						{
							foreach (AGSConnection current in App.Connections)
							{
								if (string.Equals(current.Name, b, StringComparison.CurrentCultureIgnoreCase))
								{
									List<TypedValue> list = new List<TypedValue>();
									list.Add(new TypedValue(5016, null));
									LspUtil.AppendDottedPair(ref list, 5005, "NAME", 5005, current.Name);
									LspUtil.AppendDottedPair(ref list, 5005, "URL", 5005, current.URL);
									if (!string.IsNullOrEmpty(current.Version))
									{
										LspUtil.AppendDottedPair(ref list, 5005, "VERSION", 5005, current.Version.ToString());
									}
									if (!string.IsNullOrEmpty(current.UserName))
									{
										LspUtil.AppendDottedPair(ref list, 5005, "USER", 5005, current.UserName);
									}
									list.Add(new TypedValue(5017, null));
									ResultBuffer resultBuffer = new ResultBuffer(list.ToArray());
									result = resultBuffer;
									return result;
								}
							}
							result = null;
						}
						else
						{
							result = null;
						}
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_connection_update")]
		public object ESRI_UpdateConnection(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() < 1)
				{
					result = null;
				}
				else
				{
					string text = null;
					string text2 = null;
					string text3 = null;
					string text4 = null;
					string text5 = null;
					string text6 = null;
					object obj = null;
					object obj2 = null;
					TypedValue[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						TypedValue typedValue = array2[i];
						if (typedValue.TypeCode== 5016)
						{
							obj = null;
							obj2 = null;
						}
						else if (typedValue.TypeCode == 5017)
						{
							obj = null;
							obj2 = null;
						}
						else if (typedValue.TypeCode == 5018)
						{
							if (obj != null && obj2 != null)
							{
								string a = obj.ToString();
								if (string.Equals(a, "Name", StringComparison.CurrentCultureIgnoreCase))
								{
									text = obj2.ToString();
								}
								else if (string.Equals(a, "URL", StringComparison.CurrentCultureIgnoreCase))
								{
									text2 = obj2.ToString();
								}
								else if (string.Equals(a, "tokenUser", StringComparison.CurrentCultureIgnoreCase))
								{
									text3 = obj2.ToString();
								}
								else if (string.Equals(a, "tokenPassword", StringComparison.CurrentCultureIgnoreCase))
								{
									text4 = obj2.ToString();
								}
								else if (string.Equals(a, "ServerUser", StringComparison.CurrentCultureIgnoreCase))
								{
									text5 = obj2.ToString();
								}
								else if (string.Equals(a, "ServerPassword", StringComparison.CurrentCultureIgnoreCase))
								{
									text6 = obj2.ToString();
								}
							}
						}
						else if (obj == null)
						{
							obj = typedValue;
						}
						else
						{
							obj2 = typedValue;
						}
					}
					if (string.IsNullOrEmpty(text))
					{
						result = null;
					}
					else
					{
						if (!AGSConnection.bConnectionsLoaded)
						{
							AGSConnection.LoadConnections();
						}
						if (App.Connections.Count > 0)
						{
							foreach (AGSConnection current in App.Connections)
							{
								if (string.Equals(current.Name, text, StringComparison.CurrentCultureIgnoreCase))
								{
									if (!string.IsNullOrEmpty(text3))
									{
										current.UserName = text3;
									}
									if (!string.IsNullOrEmpty(text4))
									{
										current.Password = text4;
									}
									if (!string.IsNullOrEmpty(text5))
									{
										if (string.IsNullOrEmpty(text6))
										{
											text6 = "";
										}
										NetworkCredential credentials = new NetworkCredential(text5, text6);
										current.Credentials = credentials;
									}
									if (!string.IsNullOrEmpty(text2) && Uri.IsWellFormedUriString(text2, UriKind.Absolute))
									{
										current.SetURL(text2);
									}
									if (!current.LoadConnectionProperties())
									{
										result = null;
										return result;
									}
									current.SaveToFile();
									ResultBuffer resultBuffer = new ResultBuffer();
									resultBuffer.Add(new TypedValue(5005, text));
									result = this.ESRI_GetConnection(resultBuffer);
									return result;
								}
							}
						}
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_connection_add")]
		public object ESRI_AddConnection(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() < 2)
				{
					result = null;
				}
				else
				{
					string text = null;
					string text2 = null;
					string text3 = null;
					string text4 = null;
					string text5 = null;
					string text6 = null;
					bool promptForCredentials = true;
					object obj = null;
					object obj2 = null;
					TypedValue[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						TypedValue typedValue = array2[i];
						if (typedValue.TypeCode == 5016)
						{
							obj = null;
							obj2 = null;
						}
						else if (typedValue.TypeCode == 5017)
						{
							obj = null;
							obj2 = null;
						}
						else if (typedValue.TypeCode == 5018)
						{
							if (obj != null && obj2 != null)
							{
								TypedValue typedValue2 = (TypedValue)obj;
								TypedValue typedValue3 = (TypedValue)obj2;
								string a = typedValue2.Value.ToString();
								if (string.Equals(a, "Name", StringComparison.CurrentCultureIgnoreCase))
								{
									text = typedValue3.Value.ToString();
								}
								else if (string.Equals(a, "URL", StringComparison.CurrentCultureIgnoreCase))
								{
									text2 = typedValue3.Value.ToString();
								}
								else if (string.Equals(a, "tokenUser", StringComparison.CurrentCultureIgnoreCase))
								{
									text3 = typedValue3.Value.ToString();
								}
								else if (string.Equals(a, "tokenPassword", StringComparison.CurrentCultureIgnoreCase))
								{
									text4 = typedValue3.Value.ToString();
								}
								else if (string.Equals(a, "ServerUser", StringComparison.CurrentCultureIgnoreCase))
								{
									text5 = typedValue3.Value.ToString();
								}
								else if (string.Equals(a, "ServerPassword", StringComparison.CurrentCultureIgnoreCase))
								{
									text6 = typedValue3.Value.ToString();
								}
								else if (string.Equals(a, "PromptCredentials", StringComparison.CurrentCultureIgnoreCase))
								{
									string a2 = typedValue3.Value.ToString();
									if (string.Equals(a2, "No", StringComparison.CurrentCultureIgnoreCase))
									{
										promptForCredentials = false;
									}
								}
							}
							obj2 = (obj = null);
						}
						else if (obj == null)
						{
							obj = typedValue;
						}
						else
						{
							obj2 = typedValue;
						}
					}
					if (string.IsNullOrEmpty(text))
					{
						result = null;
					}
					else if (string.IsNullOrEmpty(text2))
					{
						result = null;
					}
					else if (!Uri.IsWellFormedUriString(text2, UriKind.Absolute))
					{
						result = null;
					}
					else
					{
						if (!AGSConnection.bConnectionsLoaded)
						{
							AGSConnection.LoadConnections();
						}
						if (!string.IsNullOrEmpty(text2) && !Uri.IsWellFormedUriString(text2, UriKind.Absolute))
						{
							result = null;
						}
						else
						{
							AGSConnection aGSConnection = new AGSConnection(text, text2);
							if (!string.IsNullOrEmpty(text3))
							{
								aGSConnection.UserName = text3;
							}
							if (!string.IsNullOrEmpty(text4))
							{
								aGSConnection.Password = text4;
							}
							if (!string.IsNullOrEmpty(text5))
							{
								if (string.IsNullOrEmpty(text6))
								{
									text6 = "";
								}
								NetworkCredential credentials = new NetworkCredential(text5, text6);
								aGSConnection.Credentials = credentials;
							}
							aGSConnection.PromptForCredentials = promptForCredentials;
							if (!aGSConnection.LoadConnectionProperties())
							{
								result = null;
							}
							else if (!aGSConnection.LoadChildren())
							{
								result = null;
							}
							else
							{
								aGSConnection.PromptForCredentials = true;
								if (aGSConnection.FoldersLoaded)
								{
									aGSConnection.SaveToFile();
									App.Connections.Add(aGSConnection);
									ResultBuffer resultBuffer = new ResultBuffer();
									resultBuffer.Add(new TypedValue(5005, text));
									result = this.ESRI_GetConnection(resultBuffer);
								}
								else
								{
									result = null;
								}
							}
						}
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_connection_test")]
		public object ESRI_TestConnection(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() < 1)
				{
					result = null;
				}
				else
				{
					TypedValue typedValue = array[0];
					if (typedValue.TypeCode != 5005)
					{
						result = null;
					}
					else
					{
						string b = typedValue.Value.ToString();
						if (!AGSConnection.bConnectionsLoaded)
						{
							AGSConnection.LoadConnections();
						}
						if (App.Connections.Count > 0)
						{
							foreach (AGSConnection current in App.Connections)
							{
								if (string.Equals(current.Name, b, StringComparison.CurrentCultureIgnoreCase))
								{
									if (current.Challenge(false))
									{
										result = LspUtil.LispTrue;
										return result;
									}
									result = null;
									return result;
								}
							}
						}
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_connection_remove")]
		public object ESRI_RemoveConnection(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() < 1)
				{
					result = null;
				}
				else
				{
					TypedValue typedValue = array[0];
					if (typedValue.TypeCode != 5005)
					{
						result = null;
					}
					else
					{
						string b = typedValue.Value.ToString();
						if (!AGSConnection.bConnectionsLoaded)
						{
							AGSConnection.LoadConnections();
						}
						if (App.Connections.Count > 0)
						{
							foreach (AGSConnection current in App.Connections)
							{
								if (string.Equals(current.Name, b, StringComparison.CurrentCultureIgnoreCase))
								{
									App.Connections.Remove(current);
									current.RemoveFile();
									result = LspUtil.LispTrue;
									return result;
								}
							}
						}
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_sendwebrequest")]
		public object ESRI_SendWebRequest(ResultBuffer rb)
		{
			object result;
			try
			{
				TypedValue[] array = rb.AsArray();
				if (array.Count<TypedValue>() < 2)
				{
					result = null;
				}
				else
				{
					string text = array[0].Value.ToString();
					string text2 = array[1].Value.ToString();
					if (string.IsNullOrEmpty(text))
					{
						result = null;
					}
					else if (string.IsNullOrEmpty(text2))
					{
						result = null;
					}
					else if (!Uri.IsWellFormedUriString(text2, UriKind.Absolute))
					{
						result = null;
					}
					else
					{
						if (!AGSConnection.bConnectionsLoaded)
						{
							AGSConnection.LoadConnections();
						}
						if (!string.IsNullOrEmpty(text2) && !Uri.IsWellFormedUriString(text2, UriKind.Absolute))
						{
							result = null;
						}
						else
						{
							AGSConnection aGSConnection = null;
							foreach (AGSConnection current in App.Connections)
							{
								if (string.Equals(current.Name, text, StringComparison.CurrentCultureIgnoreCase))
								{
									aGSConnection = current;
								}
							}
							if (aGSConnection == null)
							{
								result = null;
							}
							else if (!aGSConnection.LoadConnectionProperties())
							{
								result = null;
							}
							else
							{
								try
								{
									string text3 = aGSConnection.MakeRequest(text2);
									if (string.IsNullOrEmpty(text3))
									{
										if (!string.IsNullOrEmpty(aGSConnection.ErrorMessage))
										{
											result = aGSConnection.ErrorMessage;
										}
										else
										{
											result = null;
										}
									}
									else
									{
										result = text3;
									}
								}
								catch
								{
									if (!string.IsNullOrEmpty(aGSConnection.ErrorMessage))
									{
										result = aGSConnection.ErrorMessage;
									}
									else
									{
										result = null;
									}
								}
							}
						}
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}
}
