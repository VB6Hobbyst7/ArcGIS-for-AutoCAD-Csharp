using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace AFA.UI
{
    public class TempAttributeViewer : Window, IComponentConnector
	{
		private MSCFeatureClass _FC;

		private System.Data.DataTable _FieldData;

		internal DataGrid dgTable;

		private bool _contentLoaded;

		public TempAttributeViewer(System.Data.DataTable table)
		{
			this._FieldData = table;
			this.InitializeComponent();
			this.dgTable.ItemsSource = this._FieldData.DefaultView;
			this.dgTable.FrozenColumnCount = this._FieldData.Columns.Count;
			this.dgTable.CanUserAddRows = false;
		}

		public TempAttributeViewer(MSCFeatureClass fc)
		{
			this._FC = fc;
			this._FieldData = fc.GetDataTable();
			this.InitializeComponent();
			this.dgTable.ItemsSource = this._FieldData.DefaultView;
			this.dgTable.FrozenColumnCount = this._FieldData.Columns.Count;
			this.dgTable.CanUserAddRows = false;
		}

		public TempAttributeViewer(MSCFeatureClass fc, ObjectIdCollection ids)
		{
			this._FC = fc;
			if (ids != null)
			{
				this._FieldData = fc.GetDataTable(ids);
			}
			else
			{
				this._FieldData = fc.GetDataTable();
			}
			this.InitializeComponent();
			this.dgTable.ItemsSource = this._FieldData.DefaultView;
			this.dgTable.FrozenColumnCount = this._FieldData.Columns.Count;
			this.dgTable.CanUserAddRows = false;
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/tempattributeviewer.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.dgTable = (DataGrid)target;
				return;
			}
			this._contentLoaded = true;
		}
	}
}
