using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace AFA
{
	public class App : IExtensionApplication
	{
		public static List<AGSConnection> Connections
		{
			get;
			set;
		}

		public static List<AGSLocator> Locators
		{
			get;
			set;
		}

		public static AGSLocator DefaultLocator
		{
			get;
			set;
		}

		public static bool UseBeep
		{
			get;
			set;
		}

		public static List<string> TempFiles
		{
			get;
			set;
		}

		public App()
		{
			App.Connections = new List<AGSConnection>();
			App.Locators = new List<AGSLocator>();
			App.TempFiles = new List<string>();
			App.UseBeep = false;
		}

		private void CleanOutTempFiles()
		{
			try
			{
				foreach (string current in App.TempFiles)
				{
					try
					{
						if (File.Exists(current))
						{
							File.Delete(current);
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		public void Terminate()
		{
			try
			{
				this.CleanOutTempFiles();
				DocumentDataObject.Terminate();
				ArcGISRibbon.Terminate();
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
			catch
			{
			}
		}

		public void Initialize()
		{
			DocumentDataObject.Initialize(typeof(AfaDocData));
			SplashScreenForm.DisplaySplashScreenFor(3);
			AfaDocData.ActiveDocData.DocDataset.ShowFeatureServiceLayers(true);
			ArcGISRibbon.Initialize();
		}

		public static string ReadConfig(string key)
		{
			return ConfigurationManager.AppSettings.GetValues(key)[0];
		}

		public static void SetConfig(string key, string value)
		{
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			try
			{
				configuration.AppSettings.Settings[key].Value = value;
			}
			catch
			{
				configuration.AppSettings.Settings.Add(key, value);
			}
			finally
			{
				configuration.Save(ConfigurationSaveMode.Full);
				ConfigurationManager.RefreshSection("appSettings");
			}
		}
	}
}
