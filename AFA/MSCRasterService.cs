using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace AFA
{
    public abstract class MSCRasterService
	{
		protected HiddenUpdateForm UpdateForm;

		public string Name
		{
			get;
			set;
		}

		public abstract AGSRasterService ParentService
		{
			get;
			set;
		}

		public MSCDataset ParentDataset
		{
			get;
			set;
		}

		public bool Dynamic
		{
			get
			{
				return this.ExportOptions.Dynamic;
			}
			set
			{
				this.ExportOptions.Dynamic = value;
			}
		}

		public Extent BoundaryExtent
		{
			get;
			set;
		}

		protected Extent CurrentExtent
		{
			get;
			set;
		}

		public ObjectId RasterObjectId
		{
			get;
			set;
		}

		public bool Visible
		{
			get
			{
				return this.IsVisible();
			}
			set
			{
				this.SetVisbility(value);
			}
		}

		private Extent DrawnViewInfo
		{
			get;
			set;
		}

		protected Extent OutstandingRequestInfo
		{
			get;
			set;
		}

		public string CurrentFileName
		{
			get;
			set;
		}

		protected ExportedImageEventArgs PendingImage
		{
			get;
			set;
		}

		protected AGSExportOptions LastRequested
		{
			get;
			set;
		}

		public AGSExportOptions ExportOptions
		{
			get;
			set;
		}

		public string ConnectionURL
		{
			get;
			set;
		}

		protected string UserName
		{
			get;
			set;
		}

		protected string ConnectionName
		{
			get;
			set;
		}

		protected bool ParentServiceConnected
		{
			get
			{
				return this.ParentService != null && this.ParentService.Parent != null && !this.ParentService.Parent.ConnectionFailed;
			}
		}

		protected int PendingRequestCount
		{
			get;
			set;
		}

		protected bool CancelRequests
		{
			get;
			set;
		}

		protected bool RequestUpdateNow()
		{
			if (!this.ParentServiceConnected)
			{
				return false;
			}
			Extent extent = new Extent(this.ParentDataset.ParentDocument);
			Extent extent2 = Extent.Intersect(extent, this.BoundaryExtent);
			if (!extent2.IsValid())
			{
				return false;
			}
			this.RequestNewRasterNow(extent2);
			this.OutstandingRequestInfo = extent;
			return true;
		}

		protected abstract bool ReconnectService();

		protected abstract void SetParentLayerVisibility();

		protected bool RequestUpdate(Extent viewExtent)
		{
			return this.RequestUpdate(viewExtent, false);
		}

		private static void taskManager_TasksExecutionCompleted(object sender, EventArgs e)
		{
		}

		public void RefreshConnectedService()
		{
			if (this.ParentServiceConnected)
			{
				this.RefreshService();
			}
		}

		public void RefreshService()
		{
			if (!this.ParentServiceConnected)
			{
				AGSConnection.ReestablishConnection(this.ConnectionURL, this.ConnectionName, this.UserName);
			}
			if (!this.ReconnectService())
			{
				Mouse.OverrideCursor = null;
				ErrorReport.ShowErrorMessage(AfaStrings.UnableToConnectService + this.Name);
				return;
			}
			if (!this.ParentDataset.ParentDocument.IsActive)
			{
				return;
			}
			Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
			Editor editor = this.ParentDataset.ParentDocument.Editor;
			if (!this.RequestUpdateNow())
			{
				editor.WriteMessage(AfaStrings.InvalidExtent);
			}
			if (!string.IsNullOrEmpty(this.ParentService.ErrorMessage))
			{
				editor.WriteMessage(this.ParentService.ErrorMessage);
			}
			this.CancelRequests = false;
			Mouse.OverrideCursor = null;
		}

		public void RequestNewRaster(Extent newExtent)
		{
			AGSExportOptions localEO = null;
			try
			{
				if (this.CancelRequests)
				{
					return;
				}
				if (!this.ReconnectService())
				{
					ErrorReport.ShowErrorMessage(AfaStrings.UnableToConnectService + this.Name);
					return;
				}
				localEO = (AGSExportOptions)Utility.CloneObject(this.ExportOptions);
				localEO.BoundingBox = newExtent;
				System.Drawing.Size size = Autodesk.AutoCAD.ApplicationServices.Application.ToSystemDrawingSize(this.ParentDataset.ParentDocument.Window.DeviceIndependentSize);
				localEO.Width = size.Width;
				localEO.Height = size.Height;
				this.SetParentLayerVisibility();
				localEO.OutputFile = Utility.TemporaryFilePath();
				newExtent.SpatialReference = localEO.OutputWKT;
			}
			catch
			{
			}
			try
			{
				this.UpdateForm._rasterID = this.RasterObjectId;
				ITask exportTask = this.ParentService.GetExportTask(localEO, this.UpdateForm);
				AsyncTaskManager asyncTaskManager = new AsyncTaskManager();
				asyncTaskManager.TasksExecutionCompleted += new EventHandler(MSCRasterService.taskManager_TasksExecutionCompleted);
				asyncTaskManager.AddTask(exportTask);
				this.PendingRequestCount++;
				if (App.UseBeep)
				{
					Console.Beep(196, 800);
				}
				asyncTaskManager.RunTasks();
				int times = 0;
				int maxTimes = 200000;
				Timer timer = new Timer
				{
					Interval = 10,
					Enabled = true
				};
				timer.Tick += delegate(object s, EventArgs e)
				{
					if (times++ >= maxTimes)
					{
						timer.Stop();
						timer.Dispose();
						timer = null;
						times = 0;
						this.PendingRequestCount--;
						return;
					}
					if (!this.UpdateForm.IsReady())
					{
						return;
					}
					if (!this.CancelRequests && this.PendingRequestCount < 4)
					{
						if (this.UpdateForm.UpdateRasterNow())
						{
							this.CurrentExtent = localEO.BoundingBox;
							this.CurrentFileName = localEO.OutputFile;
						}
						this.OutstandingRequestInfo = null;
						this.PendingRequestCount--;
						timer.Stop();
						timer.Dispose();
						timer = null;
						return;
					}
					this.OutstandingRequestInfo = null;
					this.PendingRequestCount--;
					timer.Stop();
					timer.Dispose();
					timer = null;
				};
				this.UpdateForm.SetReady(false);
			}
			catch (Exception)
			{
			}
		}

		protected bool RequestUpdate(Extent viewExtent, bool forceRefresh)
		{
			if (!this.ParentServiceConnected)
			{
				return false;
			}
			Extent extent = Extent.Intersect(viewExtent, this.BoundaryExtent);
			if (!extent.IsValid())
			{
				return false;
			}
			if (string.IsNullOrEmpty(extent.SpatialReference))
			{
				extent.SpatialReference = this.BoundaryExtent.SpatialReference;
			}
			if (!forceRefresh && this.CurrentExtent != null && this.CurrentExtent.Equals(extent))
			{
				return false;
			}
			if (!this.ParentDataset.ParentDocument.Editor.IsQuiescent&& DocUtil.GetDimensionalInputEnabled())
			{
				return false;
			}
			this.RequestNewRaster(extent);
			this.OutstandingRequestInfo = viewExtent;
			return true;
		}

		public void RequestNewRaster()
		{
			Editor arg_10_0 = this.ParentDataset.ParentDocument.Editor;
			Extent extent = new Extent(this.ParentDataset.ParentDocument);
			this.RequestNewRaster(extent);
			this.OutstandingRequestInfo = extent;
		}

		public void RequestNewRasterNow(Extent newExtent)
		{
			this.CancelRequests = true;
			AGSExportOptions aGSExportOptions = (AGSExportOptions)Utility.CloneObject(this.ExportOptions);
			aGSExportOptions.BoundingBox = newExtent;
			System.Drawing.Size size = Autodesk.AutoCAD.ApplicationServices.Application.ToSystemDrawingSize(this.ParentDataset.ParentDocument.Window.DeviceIndependentSize);
			this.ExportOptions.Width = size.Width;
			this.ExportOptions.Height = size.Height;
			aGSExportOptions.OutputFile = Utility.TemporaryFilePath();
			try
			{
				string text = MSCPrj.ReadWKT(this.ParentDataset.ParentDocument);
				if (!string.IsNullOrEmpty(text))
				{
					aGSExportOptions.OutputWKT = text;
				}
			}
			catch
			{
			}
			newExtent.SpatialReference = aGSExportOptions.OutputWKT;
			this.SetParentLayerVisibility();
			this.PendingImage = null;
			this.ParentService.ExportMapNow(aGSExportOptions, new ExportImageEventHandler(this.UpdateExistingRaster), this.LastRequested);
			this.LastRequested = (AGSExportOptions)Utility.CloneObject(aGSExportOptions);
			this.CancelRequests = false;
		}

		public virtual void Write()
		{
			throw new NotImplementedException();
		}

		protected void UpdateExistingRaster(object sender, ExportedImageEventArgs e)
		{
			if (this.PendingImage != null)
			{
				return;
			}
			this.PendingImage = e;
			new Extent(e.Points);
			Point3d point3d = new Point3d(this.PendingImage.Points[0].X, this.PendingImage.Points[0].Y, 0.0);
			Point3d point3d2 = new Point3d(this.PendingImage.Points[1].X, this.PendingImage.Points[1].Y, 0.0);
			Point3d point3d3 = new Point3d(this.PendingImage.Points[2].X, this.PendingImage.Points[2].Y, 0.0);
			Vector3d v = point3d2 - point3d;
			Vector3d v2 = point3d3 - point3d;
			DocUtil.UpdateRasterImage(this.ParentDataset.ParentDocument, this.RasterObjectId, this.PendingImage.ExportOptions.OutputFile, point3d, v, v2);
			this.CurrentFileName = this.PendingImage.ExportOptions.OutputFile;
			this.PendingImage = null;
		}

		public void UpdateToCurrentView()
		{
			this.DrawnViewInfo = DocUtil.GetExtentFromObject(this.ParentDataset.ParentDocument, this.RasterObjectId);
			this.UpdateIfNeeded();
		}

		protected void UpdateIfNeeded()
		{
			try
			{
				Editor arg_10_0 = this.ParentDataset.ParentDocument.Editor;
				if (this.ParentDataset.ParentDocument.IsActive)
				{
					if (!this.ParentDataset.ParentDocument.IsDisposed)
					{
						if (this.Dynamic)
						{
							if (this.DrawnViewInfo != null)
							{
								Extent extent = new Extent();
								if (DocUtil.GetActiveViewportExtents(this.ParentDataset.ParentDocument, out extent))
								{
									if (!extent.Equals(this.DrawnViewInfo))
									{
										if (this.OutstandingRequestInfo == null || !this.OutstandingRequestInfo.Equals(extent))
										{
											if (this.RequestUpdate(extent))
											{
												this.OutstandingRequestInfo = extent;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		public void CheckForUpdates()
		{
			if (this.ParentService == null)
			{
				return;
			}
			if (this.ParentService.Parent == null)
			{
				return;
			}
			if (!this.ParentServiceConnected)
			{
				return;
			}
			if (this.Dynamic)
			{
				this.UpdateIfNeeded();
			}
		}

		public string SuggestExportName()
		{
			string text = this.CurrentFileName;
			string result;
			try
			{
				string formatFileExtension = MSCRasterService.GetFormatFileExtension(this.ExportOptions.Format);
				text = this.Name;
				text = Path.ChangeExtension(text, formatFileExtension);
				result = text;
			}
			catch
			{
				result = text;
			}
			return result;
		}

		public static string GetFormatFileExtension(string SelectedImageFormat)
		{
			if (string.IsNullOrEmpty(SelectedImageFormat))
			{
				return null;
			}
			if (SelectedImageFormat == "PNG24")
			{
				return "PNG";
			}
			return SelectedImageFormat;
		}

		public bool CreateExportImage(string outputFileName)
		{
			bool result;
			try
			{
				this.StopReactors();
				if (!File.Exists(this.CurrentFileName))
				{
					this.RequestNewRasterNow(this.DrawnViewInfo);
				}
				File.Copy(this.CurrentFileName, outputFileName, true);
				DocUtil.UpdateRasterImage(this.ParentDataset.ParentDocument, this.RasterObjectId, outputFileName);
				this.RasterObjectId = ObjectId.Null;
				if (base.GetType() == typeof(MSCMapService))
				{
					AfaDocData.ActiveDocData.DocDataset.MapServices.Remove(this.Name);
					MSCMapService mSCMapService = (MSCMapService)this;
					mSCMapService.RemoveDictionary();
					this.ParentDataset.MapServices.Remove(this.Name);
					MSCDataset.SetDefaultActiveRasterServices();
				}
				if (base.GetType() == typeof(MSCImageService))
				{
					AfaDocData.ActiveDocData.DocDataset.ImageServices.Remove(this.Name);
					MSCImageService mSCImageService = (MSCImageService)this;
					mSCImageService.RemoveDictionary();
					this.ParentDataset.ImageServices.Remove(this.Name);
					MSCDataset.SetDefaultActiveRasterServices();
				}
				result = true;
			}
			catch (SystemException)
			{
				result = false;
			}
			return result;
		}

		public byte GetTransparency()
		{
			byte result = 0;
			Document parentDocument = this.ParentDataset.ParentDocument;
			using (parentDocument.LockDocument())
			{
				var transactionManager = parentDocument.TransactionManager;
				using (Transaction transaction = transactionManager.StartTransaction())
				{
					BlockTable blockTable = (BlockTable)transaction.GetObject(parentDocument.Database.BlockTableId, 0);
					BlockTableRecord arg_57_0 = (BlockTableRecord)transaction.GetObject(blockTable[(BlockTableRecord.ModelSpace)], 0);
					RasterImage rasterImage = (RasterImage)transaction.GetObject(this.RasterObjectId, 0);
					Transparency transparency = rasterImage.Transparency;
					if (transparency.IsByAlpha)
					{
                        byte alpha = transparency.Alpha;
						double d = 100.0 - 100.0 * ((double)alpha / 254.0);
						result = Convert.ToByte(Math.Floor(d));
					}
					transaction.Commit();
				}
			}
			return result;
		}

		public void SetTransparency(int newValue)
		{
			try
			{
				this.CheckForUpdates();
				DocUtil.EnableTransparency();
				Document parentDocument = this.ParentDataset.ParentDocument;
				byte b = Convert.ToByte(Math.Floor((100.0 - (double)newValue) / 100.0 * 254.0));
				Transparency transparency = new Transparency(b);
				using (parentDocument.LockDocument())
				{
					parentDocument.TransactionManager.EnableGraphicsFlush(true);
					var transactionManager = parentDocument.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						BlockTable blockTable = (BlockTable)transaction.GetObject(parentDocument.Database.BlockTableId, 0);
						BlockTableRecord arg_A1_0 = (BlockTableRecord)transaction.GetObject(blockTable[(BlockTableRecord.ModelSpace)], (OpenMode)1);
						RasterImage rasterImage = (RasterImage)transaction.GetObject(this.RasterObjectId, (OpenMode)1);
						rasterImage.Transparency=(transparency);
						rasterImage.Draw();
						parentDocument.TransactionManager.QueueForGraphicsFlush();
						parentDocument.TransactionManager.FlushGraphics();
						parentDocument.Editor.UpdateScreen();
						transaction.Commit();
					}
				}
			}
			catch
			{
			}
		}

		public void SendBehind()
		{
			Document parentDocument = this.ParentDataset.ParentDocument;
			using (parentDocument.LockDocument())
			{
				var transactionManager = parentDocument.TransactionManager;
				using (Transaction transaction = transactionManager.StartTransaction())
				{
					BlockTable blockTable = (BlockTable)transaction.GetObject(parentDocument.Database.BlockTableId, 0);
					BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(blockTable[(BlockTableRecord.ModelSpace)], (OpenMode)1);
					DrawOrderTable drawOrderTable = (DrawOrderTable)transaction.GetObject(blockTableRecord.DrawOrderTableId, (OpenMode)1);
					ObjectIdCollection objectIdCollection = new ObjectIdCollection();
					objectIdCollection.Add(this.RasterObjectId);
					drawOrderTable.MoveToBottom(objectIdCollection);
					parentDocument.TransactionManager.QueueForGraphicsFlush();
					parentDocument.TransactionManager.FlushGraphics();
					parentDocument.Editor.UpdateScreen();
					transaction.Commit();
				}
			}
			PaletteUtils.ActivateEditor();
		}

		public bool UpdateExtentLimit(Extent ext)
		{
			if (ext.IsValid() && ext.SpatialReference != null)
			{
				this.BoundaryExtent = ext;
				return true;
			}
			return false;
		}

		public bool UpdateExtentFromCurrentView()
		{
			Editor arg_10_0 = this.ParentDataset.ParentDocument.Editor;
			Extent ext;
			return DocUtil.GetActiveViewportExtents(this.ParentDataset.ParentDocument, out ext) && this.UpdateExtentLimit(ext);
		}

		public bool GetVisibility()
		{
			bool result;
			try
			{
				result = DocUtil.GetEntityVisibility(this.ParentDataset.ParentDocument, this.RasterObjectId);
			}
			catch
			{
				result = true;
			}
			return result;
		}

		public bool SetVisbility(bool visible)
		{
			return DocUtil.SetEntityVisibility(this.ParentDataset.ParentDocument, this.RasterObjectId, visible);
		}

		public void ToggleVisiblity()
		{
			bool flag = DocUtil.GetEntityVisibility(this.ParentDataset.ParentDocument, this.RasterObjectId);
			flag = !flag;
			DocUtil.SetEntityVisibility(this.ParentDataset.ParentDocument, this.RasterObjectId, flag);
		}

		public bool IsVisible()
		{
			return this.GetVisibility();
		}

		public Extents3d GetAutoCADExtents()
		{
			Point3d point3d = new Point3d(this.BoundaryExtent.XMin.Value, this.BoundaryExtent.YMin.Value, 0.0);
			Point3d point3d2 = new Point3d(this.BoundaryExtent.XMax.Value, this.BoundaryExtent.YMax.Value, 0.0);
			Extents3d result = new Extents3d(point3d, point3d2);
			return result;
		}

		public void ZoomExtents()
		{
			this.CheckForUpdates();
			string a = MSCPrj.ReadWKT(this.ParentDataset.ParentDocument);
			if (a != this.BoundaryExtent.SpatialReference)
			{
				ErrorReport.ShowErrorMessage(AfaStrings.CoordinateSystemsDontMatch);
				return;
			}
			Point3d minPoint = new Point3d(this.BoundaryExtent.XMin.Value, this.BoundaryExtent.YMin.Value, 0.0);
			Point3d maxPoint = new Point3d(this.BoundaryExtent.XMax.Value, this.BoundaryExtent.YMax.Value, 0.0);
			DocUtil.ZoomExtents(minPoint, maxPoint);
			this.CheckForUpdates();
		}

		protected void StartReactors()
		{
			Editor editor = this.ParentDataset.ParentDocument.Editor;
			Document parentDocument = this.ParentDataset.ParentDocument;
			try
			{
				this.DrawnViewInfo = new Extent(parentDocument);
			}
			catch
			{
				this.DrawnViewInfo = this.ExportOptions.BoundingBox;
			}
			editor.EnteringQuiescentState+=(new EventHandler(this.ed_EnteringQuiescentState));
			editor.PointMonitor+=(new PointMonitorEventHandler(this.ed_PointMonitor));
			parentDocument.CommandEnded+=(new CommandEventHandler(this.doc_CommandEnded));
			Database database = this.ParentDataset.ParentDocument.Database;
			database.ObjectErased+=(new ObjectErasedEventHandler(this.db_ObjectErased));
		}

		private void doc_CommandEnded(object sender, CommandEventArgs e)
		{
			try
			{
				this.CheckForUpdates();
				this.CheckForUpdates();
			}
			catch
			{
			}
		}

		public void StopReactors()
		{
			try
			{
				Editor editor = this.ParentDataset.ParentDocument.Editor;
				editor.EnteringQuiescentState-=(new EventHandler(this.ed_EnteringQuiescentState));
				editor.PointMonitor-=(new PointMonitorEventHandler(this.ed_PointMonitor));
				Database database = this.ParentDataset.ParentDocument.Database;
				database.ObjectErased-=(new ObjectErasedEventHandler(this.db_ObjectErased));
			}
			catch
			{
			}
		}

		private void db_ObjectErased(object sender, ObjectErasedEventArgs e)
		{
			try
			{
				if (e.DBObject.Id == this.RasterObjectId)
				{
					if (base.GetType() == typeof(MSCMapService))
					{
						MSCMapService mSCMapService = this as MSCMapService;
						mSCMapService.DeleteService();
					}
					else if (base.GetType() == typeof(MSCImageService))
					{
						MSCImageService mSCImageService = this as MSCImageService;
						mSCImageService.DeleteService();
					}
				}
				this.CheckForUpdates();
			}
			catch
			{
				System.Windows.MessageBox.Show("DEBUG:  Catch in MSCRasterService.db_ObjectErased");
			}
		}

		private void ed_PointMonitor(object sender, PointMonitorEventArgs e)
		{
			if (!e.Context.PointComputed)
			{
				return;
			}
			this.CheckForUpdates();
			this.CheckForUpdates();
		}

		private void ed_EnteringQuiescentState(object sender, EventArgs e)
		{
			Editor arg_10_0 = this.ParentDataset.ParentDocument.Editor;
			this.CheckForUpdates();
			this.CheckForUpdates();
		}
	}
}
