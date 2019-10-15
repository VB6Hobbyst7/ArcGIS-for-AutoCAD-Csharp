using AFA.Resources;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace AFA
{

    public class AddNewServer : Window, IComponentConnector
	{
		public bool Succeeded;

		public bool CancelPressed;

		internal Button btnNext;

		internal TextBox tbServerName;

		internal TextBox tbServerURL;

		internal TextBox tbUserName;

		internal PasswordBox tbPassword;

		private bool _contentLoaded;

		public AddNewServer()
		{
			this.InitializeComponent();
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			this.Succeeded = false;
			SelectConnection.WindowOpen = false;
			this.CancelPressed = true;
		}

		private void Next_Click(object sender, RoutedEventArgs e)
		{
			base.Visibility = Visibility.Collapsed;
			this.tbServerName.Text = AGSConnection.FixName(this.tbServerName.Text);
			AGSConnection aGSConnection = null;
			try
			{
				aGSConnection = new AGSConnection(this.tbServerName.Text, this.tbServerURL.Text);
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorConnectingToServer);
				base.Visibility = Visibility.Visible;
				this.Succeeded = false;
			}
			if (aGSConnection != null && !aGSConnection.ConnectionFailed)
			{
				aGSConnection.UserName = this.tbUserName.Text;
				aGSConnection.Password = this.tbPassword.Password;
				Mouse.OverrideCursor = Cursors.Wait;
				if (!aGSConnection.LoadConnectionProperties())
				{
					Mouse.OverrideCursor = null;
					ErrorReport.ShowErrorMessage(aGSConnection.ErrorMessage);
					base.Visibility = Visibility.Visible;
					this.Succeeded = false;
					return;
				}
				Mouse.OverrideCursor = null;
				base.Close();
				Mouse.OverrideCursor = Cursors.Wait;
				if (!aGSConnection.LoadChildren())
				{
					Mouse.OverrideCursor = null;
					string text = aGSConnection.ErrorMessage;
					if (string.IsNullOrEmpty(text))
					{
						text = AfaStrings.ErrorConnectingToServer;
					}
					ErrorReport.ShowErrorMessage(text);
					return;
				}
				Mouse.OverrideCursor = null;
				if (aGSConnection.FoldersLoaded && !aGSConnection.ConnectionFailed)
				{
					this.Succeeded = true;
					aGSConnection.SaveToFile();
					App.Connections.Add(aGSConnection);
					SelectService selectService = new SelectService(aGSConnection);
					if (selectService.IsValid)
					{
                        Autodesk.AutoCAD.ApplicationServices.Core. Application.ShowModalWindow(selectService);
						if (selectService.DialogResult.HasValue)
						{
							bool arg_165_0 = selectService.DialogResult.Value;
						}
					}
				}
			}
		}

		private string ReplaceSpecialCharacters(string inputString)
		{
			string text = inputString.Trim();
			text = text.Replace(":[; \\/:*?\"<>|&']", "_");
			return text.Replace(":", "_");
		}

		private void tbServerName_LostFocus(object sender, RoutedEventArgs e)
		{
			if (this.tbServerName.Text.Length != 0 && this.tbServerURL.Text.Length == 0)
			{
				string text = "http://" + this.tbServerName.Text + "/arcgis/services";
				string text2 = this.ReplaceSpecialCharacters(this.tbServerName.Text);
				if (Uri.IsWellFormedUriString(text, UriKind.Absolute))
				{
					this.tbServerURL.Text = text;
				}
				this.tbServerName.Text = text2;
			}
			try
			{
				if (AGSConnection.NameAlreadyExists(this.tbServerName.Text))
				{
					int num = 1;
					string text3 = this.tbServerName.Text;
					string text4 = text3;
					bool flag = true;
					while (flag && num < 500)
					{
						text4 = string.Format("{0}{1}", text3, num);
						flag = AGSConnection.NameAlreadyExists(text4);
						num++;
					}
					this.tbServerName.Text = text4;
				}
			}
			catch
			{
			}
		}

		private void tbServerURL_LostFocus(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(this.tbServerName.Text.Trim()))
			{
				this.tbServerName.Text = this.BuildServerName(this.tbServerURL.Text);
			}
		}

		private void tbServerURL_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.tbServerURL.Text = this.tbServerURL.Text.Trim();
			string text = this.tbServerURL.Text;
			this.btnNext.IsEnabled = false;
			if (Uri.IsWellFormedUriString(text, UriKind.Absolute) && this.NameIsValid(this.tbServerName.Text))
			{
				this.btnNext.IsEnabled = true;
			}
		}

		private void tbServerName_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.tbServerURL.Text = this.tbServerURL.Text.Trim();
			string text = this.tbServerURL.Text;
			this.btnNext.IsEnabled = false;
			if (Uri.IsWellFormedUriString(text, UriKind.Absolute) && this.NameIsValid(this.tbServerName.Text))
			{
				this.btnNext.IsEnabled = true;
			}
		}

		private string BuildServerName(string uriString)
		{
			string text = "";
			if (Uri.IsWellFormedUriString(uriString, UriKind.Absolute))
			{
				Uri uri = new Uri(uriString);
				text = this.ReplaceSpecialCharacters(uri.Host.Trim());
				int num = 1;
				string text2 = text;
				while (AddNewServer.NameAlreadyExists(text2))
				{
					text2 = text + num.ToString();
					num++;
				}
				return text2;
			}
			return text;
		}

		private bool NameIsValid(string proposedName)
		{
			proposedName = proposedName.Trim();
			return !string.IsNullOrEmpty(proposedName) && !AddNewServer.NameAlreadyExists(proposedName);
		}

		public static bool NameAlreadyExists(string testName)
		{
			testName = testName.ToUpper();
			foreach (AGSConnection current in App.Connections)
			{
				if (current.Name.ToUpper() == testName)
				{
					return true;
				}
			}
			return false;
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			SelectConnection.WindowOpen = false;
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/addnewserver.xaml", UriKind.Relative);
            System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((AddNewServer)target).Closing += new CancelEventHandler(this.Window_Closing);
				return;
			case 2:
				this.btnNext = (Button)target;
				this.btnNext.Click += new RoutedEventHandler(this.Next_Click);
				return;
			case 3:
				((Button)target).Click += new RoutedEventHandler(this.Cancel_Click);
				return;
			case 4:
				this.tbServerName = (TextBox)target;
				this.tbServerName.TextChanged += new TextChangedEventHandler(this.tbServerName_TextChanged);
				this.tbServerName.LostFocus += new RoutedEventHandler(this.tbServerName_LostFocus);
				return;
			case 5:
				this.tbServerURL = (TextBox)target;
				this.tbServerURL.TextChanged += new TextChangedEventHandler(this.tbServerURL_TextChanged);
				this.tbServerURL.LostFocus += new RoutedEventHandler(this.tbServerURL_LostFocus);
				return;
			case 6:
				this.tbUserName = (TextBox)target;
				return;
			case 7:
				this.tbPassword = (PasswordBox)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
