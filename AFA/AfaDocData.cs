using AFA.Resources;
using AFA.UI;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System.Linq;

namespace AFA
{
    public class AfaDocData : DocumentDataObject
	{
		private MSCFeatureClass CurrentFC;

		private MSCFeatureClass CurrentFCSubtype;

		public new static AfaDocData ActiveDocData
		{
			get
			{
				return DocumentDataObject.ActiveDocData as AfaDocData;
			}
		}

		public MSCPrj DocPRJ
		{
			get;
			set;
		}

		public MSCDataset DocDataset
		{
			get;
			set;
		}

		public MSCMapService CurrentMapService
		{
			get;
			set;
		}

		public MSCImageService CurrentImageService
		{
			get;
			set;
		}

		public ObjectId NOD
		{
			get;
			set;
		}

		public bool FeatureServiceLayersVisible
		{
			get;
			set;
		}

		protected override void Attach(Document doc)
		{
			this.NOD = doc.Database.NamedObjectsDictionaryId;
			Database arg_17_0 = doc.Database;
			try
			{
				using (Transaction transaction = doc.TransactionManager.StartTransaction())
				{
					this.DocPRJ = new MSCPrj();
					this.DocPRJ.Initialize(this.NOD, transaction);
					this.DocDataset = new MSCDataset();
					this.DocDataset.Initialize(doc, transaction);
					if (this.DocDataset.FeatureClasses.Count > 0)
					{
						this.CurrentFC = this.DocDataset.FeatureClasses.ElementAt(0).Value;
						ArcGISRibbon.EnableFeatureServiceButtons(false);
					}
					else if (this.DocDataset.FeatureServices.Count > 0)
					{
						this.CurrentFC = this.DocDataset.FeatureServices.ElementAt(0).Value;
						ArcGISRibbon.EnableFeatureServiceButtons(true);
					}
					transaction.Commit();
					if (this.DocDataset.HasFeatureServicesOpenedForEditing())
					{
						ToolPalette.ShowPalette(doc, this.DocDataset);
					}
					else
					{
						ToolPalette.UpdatePalette(doc, this.DocDataset, false);
					}
				}
			}
			catch
			{
			}
		}

		protected override void Detach()
		{
			if (this.DocDataset != null)
			{
				this.DocDataset.Uninitialize();
				this.CurrentFC = null;
				this.NOD = ObjectId.Null;
				this.DocPRJ = null;
			}
		}

		public static AfaDocData DocData(Document doc)
		{
			return DocumentDataObject.DocData(doc) as AfaDocData;
		}

		public static void Initialize()
		{
			DocumentDataObject.Initialize(typeof(AfaDocData));
		}

		public MSCFeatureClass GetActiveFeatureClassOrSubtype()
		{
			if (this.CurrentFCSubtype != null)
			{
				return this.CurrentFCSubtype;
			}
			return this.CurrentFC;
		}

		public MSCFeatureService GetActiveFeatureService()
		{
			if (this.CurrentFC is MSCFeatureService)
			{
				return (MSCFeatureService)this.CurrentFC;
			}
			return null;
		}

		public MSCFeatureClass GetTopActiveFeatureClass()
		{
			return this.CurrentFC;
		}

		public MSCFeatureClass GetActiveSubtype()
		{
			return this.CurrentFCSubtype;
		}

		public void ClearActiveSubtype()
		{
			this.CurrentFCSubtype = null;
		}

		public void ClearActiveFeatureClass()
		{
			this.CurrentFCSubtype = null;
			this.CurrentFC = null;
		}

		public MSCFeatureClass SetActiveFeatureClass(MSCFeatureClass fc)
		{
			if (fc.ParentFC != null)
			{
				this.CurrentFC = fc.ParentFC;
				this.CurrentFCSubtype = fc;
			}
			else
			{
				this.CurrentFC = fc;
				this.CurrentFCSubtype = null;
			}
			return fc;
		}

		public void Refresh()
		{
			this.ClearActiveFeatureClass();
			this.CurrentImageService = null;
			this.CurrentMapService = null;
			this.DocPRJ = null;
			Document document = base.Document;
			Database arg_28_0 = document.Database;
			try
			{
				using (Transaction transaction = document.TransactionManager.StartTransaction())
				{
					this.DocPRJ = new MSCPrj();
					this.DocPRJ.Initialize(this.NOD, transaction);
					this.DocDataset.Initialize(document, transaction);
					MSCDataset.SetDefaultActiveFeatureClass();
					MSCDataset.SetDefaultActiveRasterServices();
					document.TransactionManager.QueueForGraphicsFlush();
					document.TransactionManager.FlushGraphics();
					document.Editor.UpdateScreen();
					transaction.Commit();
					if (AfaDocData.ActiveDocData.DocDataset.HasFeatureServicesOpenedForEditing())
					{
						CmdLine.ExecuteQuietCommand("ESRI_UpdatePalette");
					}
				}
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorInitializingGISDataset);
			}
		}
	}
}
