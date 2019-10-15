using AFA.Resources;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace AFA
{
	public class NoSpatialReferenceMsg : Window, IComponentConnector
	{
		internal RichTextBox richTextBox1;

		internal Button btnAssign;

		internal Button btnSkip;

		private bool _contentLoaded;

		public NoSpatialReferenceMsg()
		{
			this.InitializeComponent();
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
		}

		private string LocatePRJDirectory()
		{
			string location = Assembly.GetExecutingAssembly().Location;
			return Path.Combine(Path.GetDirectoryName(location), "Coordinate Systems");
		}

		private void btnAssign_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = AfaStrings.OpenCoordinateSystemSource;
			openFileDialog.Filter = "PRJ|*.prj|DWG|*.dwg|DXF|*.dxf";
			openFileDialog.InitialDirectory = this.LocatePRJDirectory();
			openFileDialog.CheckFileExists = true;
			openFileDialog.CheckPathExists = true;
			bool flag = false;
			string text = "";
			do
			{
				if (openFileDialog.ShowDialog() == true)
				{
					string a = Path.GetExtension(openFileDialog.FileName).ToLower();
					try
					{
						if (!(a == ".dwg") && !(a == ".dxf") && a == ".prj")
						{
							text = File.ReadAllText(openFileDialog.FileName);
						}
					}
					catch
					{
						ErrorReport.ShowErrorMessage(AfaStrings.ErrorReadingWKTStringFrom + openFileDialog.FileName);
					}
					if (!string.IsNullOrEmpty(text))
					{
						flag = true;
						MSCPrj.AssignWKT(AfaDocData.ActiveDocData.Document, text);
					}
				}
				else
				{
					flag = true;
				}
			}
			while (!flag);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/nospatialreferencemsg.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.richTextBox1 = (RichTextBox)target;
				return;
			case 2:
				this.btnAssign = (Button)target;
				this.btnAssign.Click += new RoutedEventHandler(this.btnAssign_Click);
				return;
			case 3:
				this.btnSkip = (Button)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
