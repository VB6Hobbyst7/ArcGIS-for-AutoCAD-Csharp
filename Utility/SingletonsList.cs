using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace Utility
{
    public static class SingletonsList
	{
		public static Database curDb
		{
			get
			{
				if (SingletonsList.curDoc != null)
				{
					return SingletonsList.curDoc.Database;
				}
				return null;
			}
		}

		public static Document curDoc
		{
			get
			{
				return Application.DocumentManager.MdiActiveDocument;
			}
		}

		public static BlockTable curBlkTbl
		{
			get
			{
				return (BlockTable)SingletonsList.curDb.BlockTableId.GetObject((OpenMode)1);
			}
		}

		public static LayerTable curLyrTbl
		{
			get
			{
				return (LayerTable)SingletonsList.curDb.LayerTableId.GetObject((OpenMode)1);
			}
		}

		public static LinetypeTable LinetypeTable
		{
			get
			{
				return (LinetypeTable)SingletonsList.curDb.LinetypeTableId.GetObject((OpenMode)1);
			}
		}

		public static ViewTable ViewTable
		{
			get
			{
				return (ViewTable)SingletonsList.curDb.ViewTableId.GetObject((OpenMode)1);
			}
		}

		public static ViewportTable ViewportTable
		{
			get
			{
				return (ViewportTable)SingletonsList.curDb.ViewportTableId.GetObject((OpenMode)1);
			}
		}

		public static DBDictionary NamedObjectDictionary
		{
			get
			{
				return (DBDictionary)SingletonsList.curDb.NamedObjectsDictionaryId.GetObject((OpenMode)1);
			}
		}

		public static BlockTableRecord modelSpace
		{
			get
			{
				return (BlockTableRecord)SingletonsList.curBlkTbl[(BlockTableRecord.ModelSpace)].GetObject((OpenMode)1);
			}
		}

		public static Editor ed
		{
			get
			{
				if (SingletonsList.curDoc != null)
				{
					return SingletonsList.curDoc.Editor;
				}
				return null;
			}
		}

		public static Transaction StartTransaction()
		{
			return SingletonsList.curDb.TransactionManager.StartTransaction();
		}
	}
}
