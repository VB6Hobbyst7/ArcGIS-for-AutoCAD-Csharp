using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Exception = Autodesk.AutoCAD.Runtime.Exception;

namespace AFA
{
    public class MSCImageService : MSCRasterService
	{
		protected string DictionaryName = "ESRI_IMAGESERVICES";

		public override AGSRasterService ParentService
		{
			get;
			set;
		}

		public List<MSCMapLayer> Layers
		{
			get;
			set;
		}

		public string ServiceFullName
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public string CurrentImagePath
		{
			get;
			set;
		}

		public string RestEndpoint
		{
			get
			{
				Uri uri = new Uri(Path.Combine(this.ParentService.Parent.URL, this.ParentService.FullName));
				Uri uri2 = new Uri(Path.Combine(uri.ToString(), "ImageServer"));
				return uri2.ToString();
			}
		}

		private ObjectId AcadDictionaryID
		{
			get;
			set;
		}

		public MSCImageService(MSCDataset parentDataset, AGSImageService parentMap, AGSExportOptions eo)
		{
			this.ParentService = (AGSImageService)Utility.CloneObject(parentMap);
			base.ParentDataset = parentDataset;
			try
			{
				base.ConnectionURL = this.ParentService.Parent.Soap_URL;
				base.UserName = this.ParentService.Parent.UserName;
				base.ExportOptions = eo;
				this.ServiceFullName = this.ParentService.FullName;
				base.ConnectionURL = this.ParentService.Parent.Soap_URL;
				base.UserName = this.ParentService.Parent.UserName;
				base.ConnectionName = this.ParentService.Parent.Name;
				base.ExportOptions = eo;
				base.CurrentFileName = eo.OutputFile;
				this.UpdateForm = new HiddenUpdateForm(base.ParentDataset.ParentDocument);
				base.StartReactors();
				AfaDocData.ActiveDocData.CurrentImageService = this;
			}
			catch (SystemException ex)
			{
				this.ErrorMessage = ex.Message;
			}
		}

		public MSCImageService(Transaction t, MSCDataset parent, string name, ObjectId id)
		{
			base.ParentDataset = parent;
			this.AcadDictionaryID = id;
			base.Name = name;
			this.Layers = new List<MSCMapLayer>();
			this.Read(id, t);
			this.UpdateForm = new HiddenUpdateForm(base.ParentDataset.ParentDocument);
			if (this.ReconnectService())
			{
				base.CheckForUpdates();
				base.StartReactors();
			}
		}

		public override void Write()
		{
			Database database = base.ParentDataset.ParentDocument.Database;
			try
			{
				using (base.ParentDataset.ParentDocument.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						this.Write(database, transaction);
						transaction.Commit();
					}
				}
			}
			catch
			{
				this.ErrorMessage = "Error writing image service to file.";
			}
		}

		public void Write(Database db, Transaction t)
		{
			try
			{
				DBDictionary dBDictionary = base.ParentDataset.Open(db, t, this.DictionaryName, (OpenMode)1);
				dBDictionary.DisableUndoRecording(true);
				this.WriteImageDictionary(dBDictionary, t);
			}
			catch
			{
			}
		}

		protected DBDictionary WriteImageDictionary(DBDictionary parentDict, Transaction t)
		{
			DBDictionary result;
			try
			{
				parentDict.UpgradeOpen();
				DBDictionary dBDictionary = new DBDictionary();
				this.AcadDictionaryID = parentDict.SetAt(base.Name, dBDictionary);
				dBDictionary.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(dBDictionary, true);
				DocUtil.WriteXRecord(t, dBDictionary, "AutoCADID", (DxfCode)330, base.RasterObjectId);
				DocUtil.WriteXRecord(t, dBDictionary, "ServiceFullName", (DxfCode)1, this.ServiceFullName);
				DocUtil.WriteXRecord(t, dBDictionary, "ConnectionName", (DxfCode)1, base.ConnectionName);
				DocUtil.WriteXRecord(t, dBDictionary, "ConnectionURL", (DxfCode)1, base.ConnectionURL);
				DocUtil.WriteXRecord(t, dBDictionary, "UserName", (DxfCode)1, base.UserName);
				if (base.Dynamic)
				{
					Xrecord xrecord = new Xrecord();
					xrecord.Data=(new ResultBuffer(new TypedValue[]
					{
						new TypedValue(290, true)
					}));
					dBDictionary.SetAt("Dynamic", xrecord);
					xrecord.DisableUndoRecording(true);
					t.AddNewlyCreatedDBObject(xrecord, true);
				}
				DocUtil.WriteBoundingBox(t, dBDictionary, base.BoundaryExtent);
				DocUtil.WriteXRecord(t, dBDictionary, "ImageFormat", (DxfCode)1, base.ExportOptions.Format);
				DocUtil.WriteXRecord(t, dBDictionary, "Compression", (DxfCode)1, base.ExportOptions.TransCompression);
				DocUtil.WriteXRecord(t, dBDictionary, "Quality", (DxfCode)90, base.ExportOptions.Quality);
				DocUtil.WriteXRecord(t, dBDictionary, "Interpolation", (DxfCode)1, base.ExportOptions.Interpolation);
				DocUtil.WriteXRecord(t, dBDictionary, "MosaicMethod", (DxfCode)1, base.ExportOptions.MosaicMethod);
				DocUtil.WriteXRecord(t, dBDictionary, "OrderField", (DxfCode)1, base.ExportOptions.OrderField);
				DocUtil.WriteXRecord(t, dBDictionary, "OrderBaseValue", (DxfCode)1, base.ExportOptions.OrderBaseValue);
				DocUtil.WriteXRecord(t, dBDictionary, "LockRasterID", (DxfCode)1, base.ExportOptions.LockRasterID);
				DocUtil.WriteXRecord(t, dBDictionary, "Ascending", (DxfCode)290, base.ExportOptions.Ascending);
				DocUtil.WriteXRecord(t, dBDictionary, "MosaicOperator", (DxfCode)1, base.ExportOptions.MosaicOperator);
				result = dBDictionary;
			}
			catch (Exception ex)
			{
				string arg_209_0 = ex.Message;
				result = null;
			}
			return result;
		}

		protected void Read(ObjectId id, Transaction t)
		{
			try
			{
				DBDictionary dBDictionary = (DBDictionary)t.GetObject(this.AcadDictionaryID, 0);
				base.RasterObjectId = (ObjectId)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)330, "AutoCADID");
				this.ServiceFullName = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "ServiceFullName");
				base.ConnectionName = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "ConnectionName");
				base.ConnectionURL = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "ConnectionURL");
				base.UserName = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "UserName");
				base.ExportOptions = new AGSExportOptions();
				this.InitExportOptions();
				base.ExportOptions.Format = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "ImageFormat");
				base.ExportOptions.BoundingBox = DocUtil.ReadBoundingBox(t, dBDictionary);
				base.BoundaryExtent = new Extent(base.ExportOptions.BoundingBox);
				base.ExportOptions.OutputWKT = MSCPrj.ReadWKT(base.ParentDataset.ParentDocument);
				base.Dynamic = false;
				if (dBDictionary.Contains("Dynamic"))
				{
					Xrecord xrecord = (Xrecord)t.GetObject(dBDictionary.GetAt("Dynamic"), 0);
					TypedValue[] array = xrecord.Data.AsArray();
					for (int i = 0; i < array.Length; i++)
					{
						TypedValue typedValue = array[i];
						if (typedValue.TypeCode == 290)
						{
							base.Dynamic = (0 != Convert.ToInt16(typedValue.Value));
						}
					}
				}
				base.ExportOptions.Dynamic = base.Dynamic;
				base.ExportOptions.TransCompression = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "Compression");
				base.ExportOptions.Quality = (int)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)90, "Quality");
				base.ExportOptions.Interpolation = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "Interpolation");
				base.ExportOptions.MosaicMethod = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "MosaicMethod");
				base.ExportOptions.OrderField = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "OrderField");
				base.ExportOptions.OrderBaseValue = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "OrderBaseValue");
				base.ExportOptions.LockRasterID = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "LockRasterID");
				base.ExportOptions.MosaicOperator = (string)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)1, "MosaicOperator");
				if (dBDictionary.Contains("Ascending"))
				{
					Xrecord xrecord2 = (Xrecord)t.GetObject(dBDictionary.GetAt("Ascending"), 0);
					TypedValue[] array2 = xrecord2.Data.AsArray();
					for (int j = 0; j < array2.Length; j++)
					{
						TypedValue typedValue2 = array2[j];
						if (typedValue2.TypeCode == 290)
						{
							base.ExportOptions.Ascending = (0 != Convert.ToInt16(typedValue2.Value));
						}
					}
				}
				if (this.ReconnectService())
				{
					base.RefreshConnectedService();
				}
			}
			catch
			{
			}
		}

		private void InitExportOptions()
		{
			base.ExportOptions.AcadDocument = base.ParentDataset.ParentDocument;
			base.ExportOptions.BoundingBox = new Extent(base.BoundaryExtent);
			System.Drawing.Size size = Application.ToSystemDrawingSize(base.ParentDataset.ParentDocument.Window.DeviceIndependentSize);
			base.ExportOptions.Width = size.Width;
			base.ExportOptions.Height = size.Height;
			base.ExportOptions.DPI = 96;
			base.ExportOptions.Format = "PNG24";
		}

		protected override void SetParentLayerVisibility()
		{
		}

		protected override bool ReconnectService()
		{
			bool result;
			try
			{
				if (this.ParentService != null)
				{
					if (this.ParentService.IsValid)
					{
						result = true;
					}
					else
					{
						result = this.ParentService.Challenge();
					}
				}
				else if (string.IsNullOrEmpty(base.ConnectionURL))
				{
					result = false;
				}
				else
				{
					AGSConnection connection = AGSConnection.GetConnection(base.ConnectionURL, base.ConnectionName, base.UserName);
					if (connection != null)
					{
						if (!connection.ConnectionFailed)
						{
							this.ParentService = new AGSImageService(this.ServiceFullName, connection);
							result = this.ParentService.Challenge();
						}
						else
						{
							this.ErrorMessage = connection.ErrorMessage;
							result = false;
						}
					}
					else
					{
						this.ErrorMessage = AfaStrings.ErrorInitializingImageService;
						result = false;
					}
				}
			}
			catch
			{
				this.ErrorMessage = AfaStrings.ErrorInitializingImageService;
				result = false;
			}
			return result;
		}

		public bool DeleteService()
		{
			try
			{
				base.StopReactors();
				base.StopReactors();
			}
			catch
			{
			}
			Database database = base.ParentDataset.ParentDocument.Database;
			Document parentDocument = base.ParentDataset.ParentDocument;
			Editor editor = base.ParentDataset.ParentDocument.Editor;
			bool result;
			try
			{
				using (base.ParentDataset.ParentDocument.LockDocument((DocumentLockMode)20, null, null, false))
				{
					using (Transaction transaction = parentDocument.TransactionManager.StartTransaction())
					{
						RasterImage rasterImage = (RasterImage)transaction.GetObject(base.RasterObjectId, (OpenMode)1);
						rasterImage.DisableUndoRecording(true);
						ObjectId imageDefId = rasterImage.ImageDefId;
						RasterImageDef rasterImageDef = (RasterImageDef)transaction.GetObject(imageDefId, (OpenMode)1);
						rasterImageDef.DisableUndoRecording(true);
						if (rasterImageDef.IsLoaded)
						{
							rasterImageDef.Unload(true);
						}
						try
						{
							File.Delete(rasterImageDef.SourceFileName);
							if (App.TempFiles.Contains(rasterImageDef.SourceFileName))
							{
								App.TempFiles.Remove(rasterImageDef.SourceFileName);
							}
						}
						catch
						{
						}
						if (!rasterImageDef.IsErased)
						{
							rasterImageDef.Erase();
						}
						ObjectId imageDictionary = RasterImageDef.GetImageDictionary(database);
						DBDictionary dBDictionary = (DBDictionary)transaction.GetObject(imageDictionary, (OpenMode)1);
						dBDictionary.Remove(imageDefId);
						dBDictionary.Contains(imageDefId);
						if (!rasterImage.IsErased)
						{
							rasterImage.Erase(false);
						}
						this.RemoveDictionary(database, transaction);
						parentDocument.TransactionManager.QueueForGraphicsFlush();
						parentDocument.TransactionManager.FlushGraphics();
						parentDocument.Editor.UpdateScreen();
						transaction.Commit();
						base.ParentDataset.ImageServices.Remove(base.Name);
						editor.Regen();
						MSCDataset.SetDefaultActiveRasterServices();
						result = true;
					}
				}
			}
			catch
			{
				MessageBox.Show("DEBUG:  Catch in MSCMapService.DeleteService");
				result = false;
			}
			return result;
		}

		public void RemoveDictionary()
		{
			Database database = AfaDocData.ActiveDocData.Document.Database;
			try
			{
				using (Transaction transaction = database.TransactionManager.StartTransaction())
				{
					this.RemoveDictionary(database, transaction);
					transaction.Commit();
				}
			}
			catch
			{
			}
		}

		private void RemoveDictionary(Database db, Transaction t)
		{
			try
			{
				DBDictionary dBDictionary = base.ParentDataset.Open(db, t, this.DictionaryName, (OpenMode)1);
				if (dBDictionary.Contains(this.AcadDictionaryID))
				{
					dBDictionary.Remove(this.AcadDictionaryID);
					DBObject @object = t.GetObject(this.AcadDictionaryID, (OpenMode)1);
					if (!@object.IsErased)
					{
						@object.Erase();
					}
				}
			}
			catch
			{
			}
		}
	}
}
