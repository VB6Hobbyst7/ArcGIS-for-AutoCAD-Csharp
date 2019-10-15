using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace AFA.Test.TestFieldGrid
{
	public class TestFieldGrid : Window, IComponentConnector, IStyleConnector
	{
		private DataTable _FieldData;

		internal DataGrid dgFields;

		private bool _contentLoaded;

		public TestFieldGrid()
		{
			this.InitializeDataTable();
			this.InitializeComponent();
			this.dgFields.ItemsSource = this._FieldData.DefaultView;
		}

		private void InitializeDataTable()
		{
			this._FieldData = new DataTable();
			this._FieldData.Columns.Add("Name");
			this._FieldData.Columns.Add("FieldType");
			this._FieldData.Columns.Add("Value");
			this._FieldData.Columns.Add("Length", typeof(int));
			this._FieldData.Columns.Add("ReadOnly", typeof(bool));
		}

		private void Length_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			int num = 0;
			e.Handled = true;
			if (int.TryParse(e.Text, out num))
			{
				e.Handled = false;
			}
		}

		private void Default_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			try
			{
				DataRowView dataRowView = (DataRowView)((FrameworkElement)sender).DataContext;
				string text = dataRowView.Row["FieldType"].ToString();
				e.Handled = true;
				if (string.IsNullOrEmpty(text))
				{
					e.Handled = false;
				}
				else if (text == "Integer" || text == "Short")
				{
					int num = 0;
					if (int.TryParse(e.Text, out num))
					{
						e.Handled = false;
					}
				}
				else if (text == "Double")
				{
					double num2 = 0.0;
					if (double.TryParse(e.Text, out num2))
					{
						e.Handled = false;
					}
				}
				else if (text == "String")
				{
					string s = dataRowView.Row["Length"].ToString();
					if (string.IsNullOrEmpty(text))
					{
						e.Handled = false;
					}
					else
					{
						int num3 = int.Parse(s);
						TextBox textBox = (TextBox)e.Source;
						if (textBox.Text.Length < num3)
						{
							e.Handled = false;
						}
						else
						{
							e.Handled = true;
						}
					}
				}
			}
			catch
			{
				e.Handled = false;
			}
		}

		private void DataGrid_GotFocus(object sender, RoutedEventArgs e)
		{
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/test/testfieldgrid/testfieldgrid.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.dgFields = (DataGrid)target;
				this.dgFields.GotFocus += new RoutedEventHandler(this.DataGrid_GotFocus);
				return;
			}
			this._contentLoaded = true;
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 2:
				((TextBox)target).PreviewTextInput += new TextCompositionEventHandler(this.Default_PreviewTextInput);
				return;
			case 3:
				((TextBox)target).PreviewTextInput += new TextCompositionEventHandler(this.Length_PreviewTextInput);
				return;
			default:
				return;
			}
		}
	}
}
