using AFA.Resources;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AFA
{
    public class SelectConnection : Window, IComponentConnector, IStyleConnector
	{
		public static bool WindowOpen;

		public static bool IsInitializing;

		internal StatusBar sbar;

		internal Button btnNext;

		internal Button btnCancel;

		internal ListBox lbConnections;

		internal ListBox lbNewConnections;

		internal ListBoxItem lbItem;

		private bool _contentLoaded;

		public SelectConnection()
		{
			SelectConnection.IsInitializing = true;
			if (!AGSConnection.bConnectionsLoaded)
			{
				AGSConnection.LoadConnections();
			}
			SelectConnection.WindowOpen = true;
			this.InitializeComponent();
			this.lbConnections.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(this.ListBox_MouseDown), true);
			SelectConnection.IsInitializing = false;
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			SelectConnection.IsInitializing = true;
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
			SelectConnection.IsInitializing = false;
		}

		private void OnWindowClosing(object sender, CancelEventArgs e)
		{
			SelectConnection.WindowOpen = false;
		}

		private void lbConnections_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.btnNext.IsEnabled = (this.lbConnections.SelectedItem != null);
			e.Handled = false;
		}

		private void lbNewConnections_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			base.Close();
			SelectConnection.WindowOpen = true;
			AddNewServer addNewServer = new AddNewServer();
			Application.ShowModalWindow(addNewServer);
		}

		private void btnNext_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			if (!this.SelectService())
			{
				Application.ShowModalWindow(new SelectConnection
				{
					lbConnections = 
					{
						ItemsSource = App.Connections
					}
				});
			}
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			SelectConnection.WindowOpen = false;
		}

		private void ListBox_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2 && this.lbConnections.SelectedItem != null)
			{
				base.Close();
				if (!this.SelectService())
				{
					Application.ShowModalWindow(new SelectConnection
					{
						lbConnections = 
						{
							ItemsSource = App.Connections
						}
					});
				}
			}
		}

		private void StartProgressBar(string msg)
		{
			this.sbar.Items.Clear();
			Label label = new Label();
			label.Content = msg;
			this.sbar.Items.Add(label);
			ProgressBar progressBar = new ProgressBar();
			progressBar.Width = base.Width - 200.0;
			if (progressBar.Width < 10.0)
			{
				progressBar.Width = 250.0;
			}
			progressBar.Height = 15.0;
			Duration duration = new Duration(TimeSpan.FromSeconds(8.0));
			DoubleAnimation doubleAnimation = new DoubleAnimation(100.0, duration);
			doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
			progressBar.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
			this.sbar.Items.Add(progressBar);
		}

		private void ClearProgressBar()
		{
			this.sbar.Items.Clear();
		}

		private bool PopulateConnection(AGSConnection connection)
		{
			if (!connection.LoadConnectionProperties())
			{
				return false;
			}
			if (!connection.FoldersLoaded)
			{
				connection.LoadChildren();
			}
			return connection.FoldersLoaded;
		}

		private bool SelectService()
		{
			bool result;
			try
			{
				this.btnCancel.IsEnabled = false;
				this.btnNext.IsEnabled = false;
				base.IsEnabled = false;
				AGSConnection aGSConnection = this.lbConnections.SelectedItem as AGSConnection;
				if (aGSConnection.ConnectionFailed)
				{
					this.RefreshConnection(aGSConnection);
				}
				if (!aGSConnection.ConnectionFailed)
				{
					if (this.PopulateConnection(aGSConnection))
					{
						base.Close();
						SelectService selectService = new SelectService(aGSConnection);
						if (selectService.IsValid)
						{
							this.ClearProgressBar();
							Application.ShowModelessWindow(selectService);
							base.Close();
							result = true;
							return result;
						}
					}
					else
					{
						this.btnCancel.IsEnabled = true;
						this.btnNext.IsEnabled = true;
						base.IsEnabled = true;
						SelectConnection.WindowOpen = false;
						string text = aGSConnection.ErrorMessage;
						if (string.IsNullOrEmpty(text))
						{
							text = "Populating Connection failed";
						}
						ErrorReport.ShowErrorMessage(text);
						aGSConnection.ErrorMessage = "";
					}
				}
				result = false;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private void mnu_Rename_Click(object sender, RoutedEventArgs e)
		{
			AGSConnection item = this.lbConnections.SelectedItem as AGSConnection;
			ListBoxItem listBoxItem = this.lbConnections.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
			if (listBoxItem != null)
			{
				listBoxItem.Background = Brushes.White;
				listBoxItem.ContentTemplate = (base.FindResource("EditConnectionTemplate") as DataTemplate);
			}
		}

		private void mnu_Delete_Click(object sender, RoutedEventArgs e)
		{
			AGSConnection aGSConnection = this.lbConnections.SelectedItem as AGSConnection;
			aGSConnection.RemoveFile();
			App.Connections.Remove(aGSConnection);
			this.lbConnections.Items.Refresh();
		}

		private bool RefreshConnection(AGSConnection connection)
		{
			Mouse.OverrideCursor = Cursors.Wait;
			if (!connection.Refresh())
			{
				Mouse.OverrideCursor = null;
				ErrorReport.ShowErrorMessage(connection.ErrorMessage);
				return false;
			}
			Mouse.OverrideCursor = null;
			bool result;
			try
			{
				if (this.lbConnections.Items != null)
				{
					this.lbConnections.Items.Refresh();
				}
				result = true;
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.UnknownError);
				result = false;
			}
			return result;
		}

		private void mnu_Refresh_Click(object sender, RoutedEventArgs e)
		{
			AGSConnection connection = this.lbConnections.SelectedItem as AGSConnection;
			this.RefreshConnection(connection);
		}

		private void TextBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			if (string.IsNullOrEmpty(textBox.Text))
			{
				return;
			}
			this.RenameItem(textBox);
		}

		private void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				TextBox textBox = sender as TextBox;
				if (string.IsNullOrEmpty(textBox.Text))
				{
					return;
				}
				this.RenameItem(textBox);
				e.Handled = true;
			}
			if (e.Key == Key.Escape)
			{
				TextBox textBox2 = sender as TextBox;
				AGSConnection aGSConnection = this.lbConnections.SelectedItem as AGSConnection;
				textBox2.Text = aGSConnection.Name;
				ListBoxItem listBoxItem = this.lbConnections.ItemContainerGenerator.ContainerFromItem(aGSConnection) as ListBoxItem;
				if (listBoxItem != null)
				{
					listBoxItem.ContentTemplate = (base.FindResource("ConnectionTemplate") as DataTemplate);
				}
				e.Handled = true;
			}
		}

		private void RenameItem(TextBox tb)
		{
			try
			{
				AGSConnection aGSConnection = this.lbConnections.SelectedItem as AGSConnection;
				if (!(tb.Name == aGSConnection.Name))
				{
					if (AGSConnection.NameAlreadyExists(tb.Text))
					{
						int num = 1;
						string text = tb.Text;
						bool flag = true;
						string text2 = text;
						while (flag && num < 500)
						{
							text2 = string.Format("{0}{1}", text, num);
							flag = AGSConnection.NameAlreadyExists(text2);
							num++;
						}
						tb.Text = text2;
					}
					aGSConnection.RemoveFile();
					aGSConnection.Name = tb.Text;
					aGSConnection.SaveToFile();
					ListBoxItem listBoxItem = this.lbConnections.ItemContainerGenerator.ContainerFromItem(aGSConnection) as ListBoxItem;
					if (listBoxItem != null)
					{
						listBoxItem.ContentTemplate = (base.FindResource("ConnectionTemplate") as DataTemplate);
					}
				}
			}
			catch
			{
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/selectconnection.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((SelectConnection)target).Closing += new CancelEventHandler(this.OnWindowClosing);
				return;
			case 2:
				((MenuItem)target).Click += new RoutedEventHandler(this.mnu_Refresh_Click);
				return;
			case 3:
				((MenuItem)target).Click += new RoutedEventHandler(this.mnu_Rename_Click);
				return;
			case 4:
				((MenuItem)target).Click += new RoutedEventHandler(this.mnu_Delete_Click);
				return;
			case 6:
				this.sbar = (StatusBar)target;
				return;
			case 7:
				this.btnNext = (Button)target;
				this.btnNext.Click += new RoutedEventHandler(this.btnNext_Click);
				return;
			case 8:
				this.btnCancel = (Button)target;
				this.btnCancel.Click += new RoutedEventHandler(this.btnCancel_Click);
				return;
			case 9:
				this.lbConnections = (ListBox)target;
				this.lbConnections.SelectionChanged += new SelectionChangedEventHandler(this.lbConnections_SelectionChanged);
				return;
			case 10:
				this.lbNewConnections = (ListBox)target;
				this.lbNewConnections.SelectionChanged += new SelectionChangedEventHandler(this.lbNewConnections_SelectionChanged);
				return;
			case 11:
				this.lbItem = (ListBoxItem)target;
				return;
			}
			this._contentLoaded = true;
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId != 5)
			{
				return;
			}
			((TextBox)target).KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
			((TextBox)target).PreviewLostKeyboardFocus += new KeyboardFocusChangedEventHandler(this.TextBox_PreviewLostKeyboardFocus);
		}
	}
}
