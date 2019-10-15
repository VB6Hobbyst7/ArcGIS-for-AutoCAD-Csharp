using AFA.Resources;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace AFA
{
    public class ServicePropertyEditor : Window, IComponentConnector
	{
		private AGSRasterService localRasterService;

		private MSCRasterService mscRasterService;

		private AGSFeatureService localFeatureService;

		private MSCFeatureService mscFeatureService;

		internal ServiceProperties spProps;

		internal StatusBar sbar;

		internal Button btnOK;

		internal Button btnCancel;

		private bool _contentLoaded;

		public ServicePropertyEditor(MSCRasterService rs)
		{
			this.mscRasterService = rs;
			AGSRasterService parentService = rs.ParentService;
			if (parentService == null)
			{
				throw new Exception("Service not connected");
			}
			this.InitializeComponent();
			this.localRasterService = (AGSRasterService)Utility.CloneObject(parentService);
			this.localRasterService.ExportOptions = (AGSExportOptions)Utility.CloneObject(rs.ExportOptions);
			this.localRasterService.ExportOptions.Transparency = rs.GetTransparency();
			this.localRasterService.ExportOptions.Dynamic = rs.Dynamic;
			this.spProps.SetService(this.localRasterService, rs.ExportOptions);
			this.spProps.ExtentOptions.Add(AfaStrings.Current, rs.BoundaryExtent);
			this.spProps.cbBoundingBoxTypes.SelectedIndex = this.spProps.ExtentOptions.Count - 1;
		}

		public ServicePropertyEditor(MSCFeatureService fs)
		{
			this.mscFeatureService = fs;
			AGSFeatureService parentService = fs.ParentService;
			if (parentService == null)
			{
				throw new Exception("Service not connected");
			}
			this.InitializeComponent();
			if (parentService == null)
			{
				return;
			}
			this.localFeatureService = (AGSFeatureService)Utility.CloneObject(parentService);
			this.localFeatureService.ExportOptions = (AGSExportOptions)Utility.CloneObject(fs.ExportOptions);
			this.localFeatureService.ExportOptions.Dynamic = !this.mscFeatureService.QueryOnly;
			AGSFeatureServiceLayer aGSFeatureServiceLayer = (AGSFeatureServiceLayer)parentService.MapLayers[fs.ServiceLayerID];
			if (aGSFeatureServiceLayer != null)
			{
				this.spProps.SetService(aGSFeatureServiceLayer, fs.ExportOptions);
			}
			else
			{
				this.spProps.SetService(this.localFeatureService, fs.ExportOptions);
			}
			this.spProps.ExtentOptions.Add(AfaStrings.Current, fs.ExportOptions.BoundingBox);
			this.spProps.cbBoundingBoxTypes.SelectedIndex = this.spProps.ExtentOptions.Count - 1;
		}

		private void btnOK_Click(object sender, RoutedEventArgs e)
		{
			if (this.localRasterService != null)
			{
				AGSExportOptions aGSExportOptions = this.InitExportOptions(this.localRasterService);
				this.mscRasterService.ExportOptions = aGSExportOptions;
				this.mscRasterService.SetTransparency((int)aGSExportOptions.Transparency);
				this.mscRasterService.BoundaryExtent = aGSExportOptions.BoundingBox;
				this.mscRasterService.Dynamic = aGSExportOptions.Dynamic;
				this.mscRasterService.Write();
				base.Close();
				this.mscRasterService.RefreshService();
			}
			if (this.localFeatureService != null)
			{
				AGSExportOptions aGSExportOptions2 = this.InitExportOptions(this.localFeatureService);
				this.mscFeatureService.ExportOptions = aGSExportOptions2;
				if (!aGSExportOptions2.Dynamic)
				{
					this.mscFeatureService.QueryOnly = true;
					this.mscFeatureService.SetLayerLock(true);
				}
				else
				{
					this.mscFeatureService.QueryOnly = false;
					this.mscFeatureService.SetLayerLock(false);
				}
				this.mscFeatureService.Write();
				base.Close();
				string cmdString = ".esri_Synchronize ";
				CmdLine.ExecuteQuietCommand(cmdString);
			}
		}

		private AGSExportOptions InitExportOptions(object srcObject)
		{
			AGSExportOptions aGSExportOptions = new AGSExportOptions();
			aGSExportOptions.AcadDocument = AfaDocData.ActiveDocData.Document;
			Extent extent = this.spProps.ctrlBoundingBoxExtent.Content as Extent;
			aGSExportOptions.BoundingBox = extent;
			aGSExportOptions.OutputWKT = AfaDocData.ActiveDocData.DocPRJ.WKT;
			aGSExportOptions.WhereClause = "";
			System.Drawing.Size size = Application.ToSystemDrawingSize(AfaDocData.ActiveDocData.Document.Window.DeviceIndependentSize);
			aGSExportOptions.Width = size.Width;
			aGSExportOptions.Height = size.Height;
			aGSExportOptions.DPI = 96;
			aGSExportOptions.Transparency = this.spProps.ExportOptions.Transparency;
			aGSExportOptions.Format = this.spProps.ExportOptions.Format;
			aGSExportOptions.Dynamic = this.spProps.ExportOptions.Dynamic;
			if (aGSExportOptions.Format == null)
			{
				aGSExportOptions.Format = "PNG24";
			}
			AGSService aGSService = srcObject as AGSService;
			AGSLayer aGSLayer = srcObject as AGSLayer;
			string text = "";
			if (aGSService != null)
			{
				text = aGSService.GetWKT();
			}
			else if (aGSLayer != null)
			{
				text = aGSLayer.GetWKT();
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "PROJCS[\"WGS_1984_Web_Mercator_Auxiliary_Sphere\",GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Mercator_Auxiliary_Sphere\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"Standard_Parallel_1\",0.0],PARAMETER[\"Auxiliary_Sphere_Type\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3857]]";
			}
			if (string.IsNullOrEmpty(extent.SpatialReference))
			{
				extent.SpatialReference = text;
			}
			aGSExportOptions.OutputWKT = MSCPrj.CurrentWKT(AfaDocData.ActiveDocData.Document, text);
			try
			{
				aGSExportOptions.Interpolation = this.spProps.ExportOptions.Interpolation;
				aGSExportOptions.Quality = this.spProps.ExportOptions.Quality;
				aGSExportOptions.TransCompression = this.spProps.ExportOptions.TransCompression;
				aGSExportOptions.MosaicMethod = this.spProps.ExportOptions.MosaicMethod;
				aGSExportOptions.OrderField = this.spProps.ExportOptions.OrderField;
				aGSExportOptions.OrderBaseValue = this.spProps.ExportOptions.OrderBaseValue;
				aGSExportOptions.MosaicOperator = this.spProps.ExportOptions.MosaicOperator;
				aGSExportOptions.LockRasterID = this.spProps.ExportOptions.LockRasterID;
				aGSExportOptions.Ascending = this.spProps.ExportOptions.Ascending;
			}
			catch
			{
				return aGSExportOptions;
			}
			return aGSExportOptions;
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/servicepropertyeditor.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.spProps = (ServiceProperties)target;
				return;
			case 2:
				this.sbar = (StatusBar)target;
				return;
			case 3:
				this.btnOK = (Button)target;
				this.btnOK.Click += new RoutedEventHandler(this.btnOK_Click);
				return;
			case 4:
				this.btnCancel = (Button)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
