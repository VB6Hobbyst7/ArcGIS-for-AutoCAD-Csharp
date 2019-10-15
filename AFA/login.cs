using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace AFA
{
	public class login : Window, IComponentConnector
	{
		internal Label ServerLabel;

		internal Label ServerName;

		internal TextBox txtUserName;

		internal PasswordBox txtPassWord;

		internal Button btnlogin;

		internal Label label1;

		internal Label label2;

		internal Button CancelButton;

		private bool _contentLoaded;

		public bool Cancelled
		{
			get;
			private set;
		}

		public login()
		{
			this.InitializeComponent();
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
		}

		private void btnlogin_Click(object sender, RoutedEventArgs e)
		{
			this.Cancelled = false;
			base.Close();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Cancelled = true;
			base.Close();
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/login.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.ServerLabel = (Label)target;
				return;
			case 2:
				this.ServerName = (Label)target;
				return;
			case 3:
				this.txtUserName = (TextBox)target;
				return;
			case 4:
				this.txtPassWord = (PasswordBox)target;
				return;
			case 5:
				this.btnlogin = (Button)target;
				this.btnlogin.Click += new RoutedEventHandler(this.btnlogin_Click);
				return;
			case 6:
				this.label1 = (Label)target;
				return;
			case 7:
				this.label2 = (Label)target;
				return;
			case 8:
				this.CancelButton = (Button)target;
				this.CancelButton.Click += new RoutedEventHandler(this.CancelButton_Click);
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
