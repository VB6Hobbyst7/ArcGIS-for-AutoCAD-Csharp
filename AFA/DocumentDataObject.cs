using AFA.Resources;
using AFA.UI;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections;
using System.IO;
using System.Windows;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AFA
{
    [Serializable]
	public abstract class DocumentDataObject
	{
		private Document m_ThisDoc;

		[NonSerialized]
		protected static Hashtable m_docMap = new Hashtable();

		protected static bool DocumentInitializing = true;

		[NonSerialized]
		private static Type instanceType = null;

		private static bool ReactorsStarted = false;

		public Document Document
		{
			get
			{
				return this.m_ThisDoc;
			}
		}

		public static DocumentDataObject ActiveDocData
		{
			get
			{
				DocumentDataObject result;
				try
				{
					if (Application.DocumentManager.MdiActiveDocument != null)
					{
						result = (DocumentDataObject)DocumentDataObject.m_docMap[Application.DocumentManager.MdiActiveDocument];
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
		}

		public static void Initialize(Type type)
		{
			try
			{
				if (!(DocumentDataObject.instanceType != null))
				{
					if (type == null)
					{
						throw new InvalidOperationException("null instance type");
					}
					DocumentDataObject.instanceType = type;
					foreach (Document doc in Application.DocumentManager)
					{
						DocumentDataObject.AddDocument(doc);
					}
					if (!DocumentDataObject.ReactorsStarted)
					{
						Application.DocumentManager.DocumentCreated+=(new DocumentCollectionEventHandler(DocumentDataObject.DocumentCreated));
                      Application.DocumentManager.DocumentBecameCurrent+=(new DocumentCollectionEventHandler(DocumentDataObject.DocumentBecameCurrent));
						Application.DocumentManager.DocumentActivated+=(new DocumentCollectionEventHandler(DocumentDataObject.DocumentActivated));
						Application.DocumentManager.DocumentToBeDestroyed+=(new DocumentCollectionEventHandler(DocumentDataObject.DocumentToBeDestroyed));
						DocumentDataObject.ReactorsStarted = true;
					}
				}
			}
			catch
			{
			}
		}

		public static void Terminate()
		{
			try
			{
				bool arg_05_0 = DocumentDataObject.ReactorsStarted;
			}
			catch
			{
			}
		}

		private static void DocumentActivated(object sender, DocumentCollectionEventArgs e)
		{
			try
			{
				if (DocumentDataObject.ActiveDocData != null)
				{
					MSCFeatureClass topActiveFeatureClass = AfaDocData.ActiveDocData.GetTopActiveFeatureClass();
					if (topActiveFeatureClass != null)
					{
						ArcGISRibbon.SetActiveFeatureClass(topActiveFeatureClass);
					}
					MSCFeatureClass activeSubtype = AfaDocData.ActiveDocData.GetActiveSubtype();
					if (activeSubtype != null)
					{
						ArcGISRibbon.SetActiveFeatureClass(activeSubtype);
					}
					if (AfaDocData.ActiveDocData.DocDataset.FeatureServices != null)
					{
						AfaDocData.ActiveDocData.DocDataset.ShowFeatureServiceLayers(true);
						if (AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count > 0)
						{
							ArcGISRibbon.EnableFeatureServiceButtons(true);
						}
						else
						{
							ArcGISRibbon.EnableFeatureServiceButtons(false);
						}
					}
					if (AfaDocData.ActiveDocData.CurrentMapService != null)
					{
						ArcGISRibbon.SetActiveRasterService(AfaDocData.ActiveDocData.CurrentMapService);
					}
					else if (AfaDocData.ActiveDocData.CurrentImageService != null)
					{
						ArcGISRibbon.SetActiveRasterService(AfaDocData.ActiveDocData.CurrentImageService);
					}
					ToolPalette.UpdatePalette(AfaDocData.ActiveDocData.Document, AfaDocData.ActiveDocData.DocDataset);
				}
			}
			catch
			{
			}
		}

		public static MessageBoxResult ShowYesNo(string message, MessageBoxImage icon)
		{
			string caption = "ArcGIS for AutoCAD";
			return MessageBox.Show(message, caption, MessageBoxButton.YesNo, icon);
		}

		private static void DocumentBecameCurrent(object sender, DocumentCollectionEventArgs e)
		{
			if (e.Document == null)
			{
				DocumentDataObject.DocumentInitializing = false;
				return;
			}
			if (DocumentDataObject.DocumentInitializing)
			{
				DocumentDataObject.DocumentInitializing = false;
				AfaDocData.DocData(e.Document);
			}
		}

		private static DocumentDataObject Create(Document doc)
		{
			if (DocumentDataObject.instanceType != null)
			{
				DocumentDataObject documentDataObject = (DocumentDataObject)Activator.CreateInstance(DocumentDataObject.instanceType, true);
				documentDataObject.m_ThisDoc = doc;
				documentDataObject.Attach(doc);
				return documentDataObject;
			}
			throw new InvalidOperationException(AfaStrings.NoInstanceClassSpecified);
		}

		public static DocumentDataObject DocData()
		{
			return DocumentDataObject.ActiveDocData;
		}

		public static DocumentDataObject DocData(Document doc)
		{
			return (DocumentDataObject)DocumentDataObject.m_docMap[doc];
		}

		private static void DocumentCreated(object sender, DocumentCollectionEventArgs e)
		{
			if (e.Document != null)
			{
				DocumentDataObject.AddDocument(e.Document);
			}
		}

		private static void DocumentToBeDestroyed(object sender, DocumentCollectionEventArgs e)
		{
			DocumentDataObject.RemoveDocument(e.Document);
		}

		private static void AddDocument(Document doc)
		{
			if (doc == null)
			{
				return;
			}
			if (!DocumentDataObject.m_docMap.ContainsKey(doc))
			{
				Database database = doc.TryGetDatabase();
				if (database != null)
				{
					DocumentDataObject.m_docMap.Add(doc, DocumentDataObject.Create(doc));
				}
			}
		}

		private static void RemoveDocument(Document doc)
		{
			try
			{
				if (doc == null)
				{
					return;
				}
				DocumentDataObject documentDataObject = DocumentDataObject.DocData(doc);
				if (documentDataObject != null)
				{
					documentDataObject.Detach();
					documentDataObject.m_ThisDoc = null;
				}
			}
			catch
			{
			}
			DocumentDataObject.m_docMap.Remove(doc);
		}

		protected virtual void Detach()
		{
		}

		protected virtual void Attach(Document doc)
		{
		}

		protected void DocTrace(string msg)
		{
			try
			{
				string arg_0B_0 = this.Document.Name;
				this.Document.Editor.WriteMessage("\n{0}[{1}]: {2}\n", new object[]
				{
					base.GetType().Name,
					Path.GetFileName(this.Document.Name),
					msg
				});
			}
			catch
			{
			}
		}
	}
}
