using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;

namespace AFA
{
    public class MSCPrj
	{
		public string WKT
		{
			get;
			set;
		}

		public ObjectId AcadId
		{
			get;
			set;
		}

		public MSCPrj()
		{
			this.WKT = "";
			this.AcadId = ObjectId.Null;
		}

		public void Initialize(ObjectId NodID, Transaction t)
		{
			try
			{
				this.AcadId = this.FindAcadId(NodID, t);
				this.WKT = this.ReadWKT(this.AcadId, t);
			}
			catch
			{
				this.AcadId = ObjectId.Null;
				this.WKT = "";
			}
		}

		public string Read(Transaction t)
		{
			if (this.AcadId != ObjectId.Null)
			{
				return this.ReadWKT(this.AcadId, t);
			}
			return "";
		}

		private string ReadWKTFromXrecord(Xrecord rec)
		{
			ResultBuffer data = rec.Data;
			string result;
			if (data != null)
			{
				TypedValue[] array = data.AsArray();
				result = array[0].Value.ToString();
			}
			else
			{
				this.AcadId = ObjectId.Null;
				result = "";
			}
			return result;
		}

		private string ReadWKT(ObjectId id, Transaction t)
		{
			if (id.IsNull)
			{
				return "";
			}
			Xrecord rec = (Xrecord)t.GetObject(id, 0);
			return this.ReadWKTFromXrecord(rec);
		}

		private ObjectId FindAcadId(ObjectId nodID, Transaction t)
		{
			ObjectId result = ObjectId.Null;
			DBDictionary dBDictionary = (DBDictionary)t.GetObject(nodID, 0, false);
			if (dBDictionary.Contains("ESRI_PRJ"))
			{
				result = dBDictionary.GetAt("ESRI_PRJ");
			}
			return result;
		}

		public static string CurrentWKT(Document doc, string defaultWKT)
		{
			string text = MSCPrj.ReadWKT(doc);
			if (string.IsNullOrEmpty(text))
			{
				if (!string.IsNullOrEmpty(defaultWKT))
				{
					MSCPrj.AssignWKT(doc, defaultWKT);
				}
				return defaultWKT;
			}
			return text;
		}

		public static void RemoveWKT(Document doc)
		{
			try
			{
				using (doc.LockDocument((DocumentLockMode)20, null, null, false))
				{
					Database database = doc.Database;
					using (Transaction transaction = database.TransactionManager.StartTransaction())
					{
						DBDictionary dBDictionary = (DBDictionary)transaction.GetObject(database.NamedObjectsDictionaryId, (OpenMode)1, false);
						if (dBDictionary.Contains("ESRI_PRJ"))
						{
							dBDictionary.Remove("ESRI_PRJ");
						}
						transaction.Commit();
						AfaDocData.ActiveDocData.DocPRJ.WKT = null;
						AfaDocData.ActiveDocData.DocPRJ.AcadId = ObjectId.Null;
					}
				}
			}
			catch
			{
			}
		}

		public static string ReadWKT(Database db)
		{
			string text = "";
			string result;
			using (Transaction transaction = db.TransactionManager.StartTransaction())
			{
				DBDictionary dBDictionary = (DBDictionary)transaction.GetObject(db.NamedObjectsDictionaryId, 0, false);
				if (dBDictionary.Contains("ESRI_PRJ"))
				{
					dBDictionary.GetAt("ESRI_PRJ");
					Xrecord xrecord = (Xrecord)transaction.GetObject(dBDictionary.GetAt("ESRI_PRJ"), 0);
					ResultBuffer data = xrecord.Data;
					if (data != null)
					{
						TypedValue[] array = data.AsArray();
						text = array[0].Value.ToString();
					}
				}
				else
				{
					text = "";
				}
				transaction.Commit();
				result = text;
			}
			return result;
		}

		public static string ReadWKT(Document doc)
		{
			string text = "";
			string result;
			try
			{
				using (doc.LockDocument((DocumentLockMode)32, null, null, false))
				{
					Database database = doc.Database;
					using (Transaction transaction = database.TransactionManager.StartTransaction())
					{
						DBDictionary dBDictionary = (DBDictionary)transaction.GetObject(database.NamedObjectsDictionaryId, 0, false);
						if (dBDictionary.Contains("ESRI_PRJ"))
						{
							ObjectId at = dBDictionary.GetAt("ESRI_PRJ");
							Xrecord xrecord = (Xrecord)transaction.GetObject(dBDictionary.GetAt("ESRI_PRJ"), 0);
							ResultBuffer data = xrecord.Data;
							if (!(data != null))
							{
								goto IL_D5;
							}
							TypedValue[] array = data.AsArray();
							text = array[0].Value.ToString();
							try
							{
								if (AfaDocData.ActiveDocData != null)
								{
									AfaDocData.ActiveDocData.DocPRJ.WKT = text;
									AfaDocData.ActiveDocData.DocPRJ.AcadId = at;
								}
								goto IL_D5;
							}
							catch
							{
								goto IL_D5;
							}
						}
						text = "";
						IL_D5:
						transaction.Commit();
						result = text;
					}
				}
			}
			catch
			{
				result = text;
			}
			return result;
		}

		public static string AssignWKT(Document doc, string wktString)
		{
			if (string.IsNullOrEmpty(wktString))
			{
				return wktString;
			}
			if (wktString == "<Undefined>")
			{
				return wktString;
			}
			if (MSCPrj.IsWKID(wktString))
			{
				return "";
			}
			string result;
			try
			{
				using (doc.LockDocument((DocumentLockMode)20, null, null, false))
				{
					using (Transaction transaction = doc.Database.TransactionManager.StartTransaction())
					{
						Database database = doc.Database;
						Xrecord xrecord = new Xrecord();
						TypedValue typedValue = new TypedValue(1, wktString);
						xrecord.Data=(new ResultBuffer(new TypedValue[]
						{
							typedValue
						}));
						DBDictionary dBDictionary = (DBDictionary)transaction.GetObject(database.NamedObjectsDictionaryId, (OpenMode)1, false);
						ObjectId acadId = dBDictionary.SetAt("ESRI_PRJ", xrecord);
						xrecord.DisableUndoRecording(true);
						transaction.AddNewlyCreatedDBObject(xrecord, true);
						transaction.Commit();
						AfaDocData.ActiveDocData.DocPRJ.WKT = wktString;
						AfaDocData.ActiveDocData.DocPRJ.AcadId = acadId;
						foreach (MSCMapService current in AfaDocData.ActiveDocData.DocDataset.MapServices.Values)
						{
							current.RefreshConnectedService();
						}
						foreach (MSCImageService current2 in AfaDocData.ActiveDocData.DocDataset.ImageServices.Values)
						{
							current2.RefreshConnectedService();
						}
						result = wktString;
					}
				}
			}
			catch (SystemException ex)
			{
				string arg_186_0 = ex.Message;
				result = "";
			}
			catch (Autodesk.AutoCAD.Runtime.Exception ex2)
			{
				string arg_199_0 = ex2.Message;
				result = "";
			}
			return result;
		}

		private static string LocatePRJDirectory()
		{
			string location = Assembly.GetExecutingAssembly().Location;
			return Path.Combine(Path.GetDirectoryName(location), "Coordinate Systems");
		}

		public static bool IsWKID(string wkt)
		{
			bool result;
			try
			{
				Convert.ToInt32(wkt);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool AssignPRJFile(string fileName)
		{
			if (!File.Exists(fileName))
			{
				return false;
			}
			string a = Path.GetExtension(fileName).ToLower();
			string text = "";
			try
			{
				if (a == ".dwg" || a == ".dxf")
				{
					Database database = new Database(false, true);
					database.ReadDwgFile(fileName, (FileOpenMode)3, true, "");
					text = MSCPrj.ReadWKT(database);
					if (string.IsNullOrEmpty(text))
					{
						bool result = false;
						return result;
					}
				}
				else
				{
					if (!(a == ".prj"))
					{
						bool result = false;
						return result;
					}
					text = File.ReadAllText(fileName);
				}
			}
			catch
			{
				bool result = false;
				return result;
			}
			if (!string.IsNullOrEmpty(text))
			{
				string value = MSCPrj.AssignWKT(AfaDocData.ActiveDocData.Document, text);
				if (!string.IsNullOrEmpty(value))
				{
					return true;
				}
			}
			return false;
		}

		public static void AssignPRJDialog()
		{
			string value = MSCPrj.ReadWKT(AfaDocData.ActiveDocData.Document);
			if (!string.IsNullOrEmpty(value) && AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count > 0)
			{
				AfaDocData.ActiveDocData.Document.Editor.WriteMessage(AfaStrings.ChangeCoordSysErrorFeatureServicesPresent + "\n");
				return;
			}
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = AfaStrings.OpenCoordinateSystemSource;
			openFileDialog.Filter = "PRJ|*.prj|DWG|*.dwg|DXF|*.dxf";
			openFileDialog.InitialDirectory = MSCPrj.LocatePRJDirectory();
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
						if (a == ".dwg" || a == ".dxf")
						{
							Database database = new Database(false, true);
							database.ReadDwgFile(openFileDialog.FileName, (FileOpenMode)3, true, "");
							text = MSCPrj.ReadWKT(database);
							if (string.IsNullOrEmpty(text))
							{
								ErrorReport.ShowErrorMessage(AfaStrings.NoValidCoordinateSystemFoundIn + openFileDialog.FileName);
							}
						}
						else if (a == ".prj")
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
	}
}
