using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
using Color = Autodesk.AutoCAD.Colors.Color;
using Exception = Autodesk.AutoCAD.Runtime.Exception;
using Image = System.Drawing.Image;

namespace AFA
{
    public static class DocUtil
	{
		public static string[] CADSupportedImageFormats = new string[]
		{
			"PNG24",
			"PNG",
			"JPG",
			"TIFF",
			"BMP"
		};

		private static Random randomColorGenerator = null;

		private static short[] BrightColors = new short[]
		{
			10,
			11,
			20,
			21,
			30,
			31,
			40,
			41,
			50,
			51,
			60,
			61,
			70,
			71,
			80,
			81,
			90,
			91,
			100,
			101,
			110,
			111,
			120,
			121,
			130,
			131,
			140,
			141,
			150,
			151,
			160,
			161,
			170,
			171,
			180,
			181,
			190,
			191,
			200,
			201,
			210,
			211,
			220,
			221,
			230,
			231,
			240,
			241
		};

		public static void FixPDMode()
		{
			try
			{
				Document document = AfaDocData.ActiveDocData.Document;
				int pdmode = document.Database.Pdmode;
				double pdsize = document.Database.Pdsize;
				if (pdmode == 0 && pdsize == 0.0)
				{
					using (document.LockDocument())
					{
						document.Database.Pdsize=(-1.0);
						document.Database.Pdmode=(33);
					}
				}
			}
			catch
			{
			}
		}

		public static bool IsVersion10()
		{
			bool result;
			try
			{
				if (Application.Version.Major == 18)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static ObjectId GetBlockDefinition(Document doc, Transaction t, string blockName)
		{
			ObjectId result = ObjectId.Null;
			using (doc.LockDocument())
			{
				BlockTable blockTable = (BlockTable)t.GetObject(doc.Database.BlockTableId, (OpenMode)1);
				if (blockTable.Has(blockName))
				{
					result = blockTable[(blockName)];
				}
			}
			return result;
		}

		public static ObjectId GetBlockDefinition(Document doc, string blockName)
		{
			ObjectId result = ObjectId.Null;
			ObjectId arg_0B_0 = ObjectId.Null;
			using (doc.LockDocument())
			{
				doc.TransactionManager.EnableGraphicsFlush(true);
				var transactionManager = doc.TransactionManager;
				using (Transaction transaction = transactionManager.StartTransaction())
				{
					result = DocUtil.GetBlockDefinition(doc, transaction, blockName);
					transaction.Commit();
				}
			}
			return result;
		}

		public static BlockReference CreateSimpleBlockInsert(ObjectId blockRef, Point3d pos)
		{
			return new BlockReference(pos, blockRef);
		}

		public static DBDictionary OpenNOD(Database db, Transaction t, OpenMode mode)
		{
			return (DBDictionary)t.GetObject(db.NamedObjectsDictionaryId, mode);
		}

		public static void WriteXRecord(Transaction t, DBDictionary dict, string key, ResultBuffer rb)
		{
			try
			{
				Xrecord xrecord = new Xrecord();
				xrecord.Data=(rb);
				dict.SetAt(key, xrecord);
				xrecord.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(xrecord, true);
			}
			catch
			{
			}
		}

		public static void WriteXRecord(Transaction t, DBDictionary dict, string key, DxfCode code, object value)
		{
			try
			{
				Xrecord xrecord = new Xrecord();
				xrecord.Data=(new ResultBuffer(new TypedValue[]
				{
					new TypedValue((int)code, value)
				}));
				dict.SetAt(key, xrecord);
				xrecord.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(xrecord, true);
			}
			catch
			{
			}
		}

		public static object ReadXRecord(Transaction t, DBDictionary dict, DxfCode code, string key)
		{
			object result;
			try
			{
				Xrecord xrecord = (Xrecord)t.GetObject(dict.GetAt(key), 0);
				TypedValue typedValue = xrecord.Data.AsArray()[0];
				if (typedValue.TypeCode ==(int)code)
				{
					result = typedValue.Value;
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

		public static void WriteBoundingBox(Transaction t, DBDictionary dict, Extent bbox)
		{
			DBDictionary dBDictionary = new DBDictionary();
			dict.SetAt("BoundingBox", dBDictionary);
			dBDictionary.DisableUndoRecording(true);
			t.AddNewlyCreatedDBObject(dBDictionary, true);
			DocUtil.WriteXRecord(t, dBDictionary, "xmin", (DxfCode)40, bbox.XMin);
			DocUtil.WriteXRecord(t, dBDictionary, "xmax", (DxfCode)40, bbox.XMax);
			DocUtil.WriteXRecord(t, dBDictionary, "ymin", (DxfCode)40, bbox.YMin);
			DocUtil.WriteXRecord(t, dBDictionary, "ymax", (DxfCode)40, bbox.YMax);
			DocUtil.WriteXRecord(t, dBDictionary, "WKT", (DxfCode)1, bbox.SpatialReference);
		}

		public static Extent ReadBoundingBox(Transaction t, DBDictionary dict)
		{
			Extent result;
			try
			{
				if (dict.Contains("BoundingBox"))
				{
					DBDictionary dict2 = (DBDictionary)t.GetObject(dict.GetAt("BoundingBox"), 0);
					double xMin = (double)DocUtil.ReadXRecord(t, dict2, (DxfCode)40, "xmin");
					double xMax = (double)DocUtil.ReadXRecord(t, dict2, (DxfCode)40, "xmax");
					double yMin = (double)DocUtil.ReadXRecord(t, dict2, (DxfCode)40, "ymin");
					double yMax = (double)DocUtil.ReadXRecord(t, dict2, (DxfCode)40, "ymax");
					result = new Extent(xMin, yMin, xMax, yMax)
					{
						SpatialReference = (string)DocUtil.ReadXRecord(t, dict2, (DxfCode)1, "WKT")
					};
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

		public static bool HasLimits(Document doc)
		{
			bool result;
			try
			{
				result = doc.Database.Limcheck;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static void UpdateView(Document doc)
		{
			try
			{
				Application.UpdateScreen();
			}
			catch
			{
			}
		}

		public static Extent GetViewExtent(Document doc)
		{
			Extent result;
			try
			{
				using (doc.LockDocument((DocumentLockMode)32, null, null, false))
				{
					Editor arg_12_0 = doc.Editor;
					Extent extent = new Extent(doc);
					try
					{
						extent.SpatialReference = AfaDocData.ActiveDocData.DocPRJ.WKT;
					}
					catch
					{
						extent.SpatialReference = null;
					}
					result = extent;
				}
			}
			catch
			{
				result = new Extent(0.0, 0.0, 100.0, 100.0);
			}
			return result;
		}

		public static Extent GetLimitExtent(Document doc)
		{
			Extent result;
			try
			{
				Point2d limmin = doc.Database.Limmin;
				Point2d limmax = doc.Database.Limmax;
				Extent extent = new Extent(limmin, limmax);
				try
				{
					extent.SpatialReference = AfaDocData.ActiveDocData.DocPRJ.WKT;
				}
				catch
				{
					extent.SpatialReference = null;
				}
				result = extent;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static Extent GetDwgExtent(Document doc)
		{
			Extent result;
			try
			{
				using (doc.LockDocument((DocumentLockMode)32, null, null, false))
				{
					doc.Database.UpdateExt(false);
					Point3d extmin = doc.Database.Extmin;
					Point3d extmax = doc.Database.Extmax;
					if (extmin.X < extmax.X && extmin.Y < extmax.Y)
					{
						Extent extent = new Extent(extmin, extmax);
						try
						{
							extent.SpatialReference = AfaDocData.ActiveDocData.DocPRJ.WKT;
						}
						catch
						{
							extent.SpatialReference = null;
						}
						result = extent;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = new Extent(0.0, 0.0, 100.0, 100.0);
			}
			return result;
		}

		public static Extent GetExtentFromObject(Document doc)
		{
			PaletteUtils.ActivateEditor();
			Editor editor = doc.Editor;
			PromptEntityOptions promptEntityOptions = new PromptEntityOptions(string.Format("\n{0}", AfaStrings.SelectAnEntity));
			promptEntityOptions.AllowNone=(true);
			promptEntityOptions.RemoveAllowedClass(typeof(Point3d));
			promptEntityOptions.RemoveAllowedClass(typeof(Point2d));
			PromptEntityResult entity = editor.GetEntity(promptEntityOptions);
			if (entity.Status == (PromptStatus)(PromptStatus)5100)
			{
				DocUtil.GetExtentFromObject(doc, entity.ObjectId);
				editor.WriteMessage(AfaStrings.ExtentSetFromObject);
			}
			return null;
		}

		public static Extent GetExtentFromObject(Document doc, ObjectId id)
		{
			Extent result;
			try
			{
				using (Transaction transaction = doc.Database.TransactionManager.StartTransaction())
				{
					Entity entity = (Entity)transaction.GetObject(id, 0);
					Extents3d geometricExtents = entity.GeometricExtents;
					transaction.Commit();
					Extent extent = new Extent(geometricExtents);
					try
					{
						extent.SpatialReference = AfaDocData.ActiveDocData.DocPRJ.WKT;
					}
					catch
					{
						extent.SpatialReference = null;
					}
					result = extent;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static Extent GetExtentsFromCorners(Document doc)
		{
			PaletteUtils.ActivateEditor();
			Editor editor = doc.Editor;
			PromptPointOptions promptPointOptions = new PromptPointOptions(string.Format("\n{0}", AfaStrings.SelectFirstCorner));
			PromptPointResult point = editor.GetPoint(promptPointOptions);
			if (point.Status != (PromptStatus)5100)
			{
				return null;
			}
			PromptCornerOptions promptCornerOptions = new PromptCornerOptions(string.Format("\n{0}", AfaStrings.SelectSecondCorner), point.Value);
			PromptPointResult corner = editor.GetCorner(promptCornerOptions);
			if (corner.Status == (PromptStatus)5100)
			{
				Extent extent = new Extent(point.Value, corner.Value);
				try
				{
					extent.SpatialReference = AfaDocData.ActiveDocData.DocPRJ.WKT;
				}
				catch
				{
					extent.SpatialReference = null;
				}
				editor.WriteMessage(AfaStrings.ExtentSetFromCorners);
				return extent;
			}
			return null;
		}

		public static bool IsValidExtent(Extent ext)
		{
			if (ext == null)
			{
				return false;
			}
			double? xMax = ext.XMax;
			double? xMin = ext.XMin;
			if (xMax.GetValueOrDefault() > xMin.GetValueOrDefault() && (xMax.HasValue & xMin.HasValue))
			{
				double? yMax = ext.YMax;
				double? yMin = ext.YMin;
				return yMax.GetValueOrDefault() > yMin.GetValueOrDefault() && (yMax.HasValue & yMin.HasValue);
			}
			return false;
		}

		public static void ViewSize(ref int width, ref int height)
		{
            //try
            //{
            //	if (!(Application.DocumentManager.MdiActiveDocument == null))
            //	{
            //		PaletteUtils.ActivateEditor();
            //		Document mdiActiveDocument = Application.DocumentManager.MdiActiveDocument;
            //		System.Drawing.Size size = System.Windows.Forms.Application.ToSystemDrawingSize(mdiActiveDocument.Window.DeviceIndependentSize);
            //		width = size.Width;
            //		height = size.Height;
            //	}
            //}
            //catch (SystemException ex)
            //{
            //	string arg_50_0 = ex.Message;
            //	ErrorReport.ShowErrorMessage(AfaStrings.UnableToGetViewSize);
            //}
            try
            {
                if (Application.DocumentManager.MdiActiveDocument != null)
                {
                    PaletteUtils.ActivateEditor();
                    System.Drawing.Size size = Autodesk.AutoCAD.ApplicationServices.Application.ToSystemDrawingSize(Application.DocumentManager.MdiActiveDocument.Window.DeviceIndependentSize);
                    width = size.Width;
                    height = size.Height;
                }
            }
            catch (SystemException exception1)
            {
                string message = exception1.Message;
                ErrorReport.ShowErrorMessage(AfaStrings.UnableToGetViewSize);
            }

        }

        public static string DefaultFilePath()
		{
			string result;
			try
			{
				Document mdiActiveDocument = Application.DocumentManager.MdiActiveDocument;
				if (mdiActiveDocument == null)
				{
					result = "";
				}
				else
				{
					string name = mdiActiveDocument.Name;
					object systemVariable = Application.GetSystemVariable("DWGTITLED");
					if (Convert.ToInt16(systemVariable) == 0)
					{
						result = "";
					}
					else
					{
						result = Path.GetDirectoryName(name);
					}
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static bool getScreenRasterPoints(out Point3d basePoint, out Vector3d vWidth, out Vector3d vHeight)
		{
			Document document = AfaDocData.ActiveDocData.Document;
			Database arg_11_0 = document.Database;
			Extent extent = new Extent();
			if (DocUtil.GetActiveViewportExtents(document, out extent))
			{
				basePoint = new Point3d(extent.XMin.Value, extent.YMin.Value, 0.0);
				Point3d point3d = new Point3d(extent.XMax.Value, extent.YMin.Value, 0.0);
				Point3d point3d2 = new Point3d(extent.XMin.Value, extent.YMax.Value, 0.0);
				vWidth = basePoint.GetVectorTo(point3d);
				vHeight = basePoint.GetVectorTo(point3d2);
				return true;
			}
			basePoint = Point3d.Origin;
			vWidth = Vector3d.XAxis;
			vHeight = Vector3d.YAxis;
			return false;
		}

		public static bool UpdateRasterImage(Document doc, ObjectId rasterId, string url, Point3d basePoint, Vector3d v1, Vector3d v2)
		{
			bool result;
			try
			{
				if (doc == null)
				{
					result = false;
				}
				else if (doc.IsDisposed)
				{
					result = false;
				}
				else if (rasterId.IsEffectivelyErased)
				{
					result = false;
				}
				else
				{
					Database arg_38_0 = doc.Database;
					try
					{
						Editor arg_3F_0 = doc.Editor;
						using (doc.LockDocument((DocumentLockMode)4, null, null, false))
						{
							doc.TransactionManager.EnableGraphicsFlush(true);
							var transactionManager = doc.TransactionManager;
							using (Transaction transaction = transactionManager.StartTransaction())
							{
								RasterImage rasterImage = (RasterImage)transaction.GetObject(rasterId,(OpenMode)1);
								rasterImage.DisableUndoRecording(true);
								ObjectId imageDefId = rasterImage.ImageDefId;
								RasterImageDef rasterImageDef = (RasterImageDef)transaction.GetObject(imageDefId,(OpenMode)1);
								rasterImageDef.DisableUndoRecording(true);
								string sourceFileName = rasterImageDef.SourceFileName;
								try
								{
									rasterImageDef.Unload(true);
									rasterImageDef.SourceFileName=(url);
									rasterImageDef.Load();
									if (rasterImageDef.IsLoaded)
									{
										try
										{
											if (!string.IsNullOrEmpty(sourceFileName))
											{
												File.Delete(sourceFileName);
												if (App.TempFiles.Contains(sourceFileName))
												{
													App.TempFiles.Remove(sourceFileName);
												}
											}
										}
										catch
										{
										}
									}
									rasterImage.Orientation=(new CoordinateSystem3d(basePoint, v1, v2));
									rasterImage.Draw();
								}
								catch (Exception)
								{
								}
								try
								{
									if (App.UseBeep)
									{
										Console.Beep(392, 800);
									}
									rasterImageDef.UpdateEntities();
									doc.TransactionManager.QueueForGraphicsFlush();
									doc.TransactionManager.FlushGraphics();
									doc.Editor.UpdateScreen();
								}
								catch
								{
								}
								transaction.Commit();
							}
						}
						result = true;
					}
					catch (Exception)
					{
						result = false;
					}
					catch
					{
						result = false;
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static void UpdateView(Editor ed)
		{
			try
			{
				ViewTableRecord currentView = ed.GetCurrentView();
				ed.SetCurrentView(currentView);
				currentView.Dispose();
			}
			catch
			{
			}
		}

		public static void UpdateScreen(Editor ed)
		{
			try
			{
				ed.UpdateScreen();
			}
			catch
			{
			}
		}

		public static bool UpdateRasterImage(Document doc, ObjectId rasterId, string url)
		{
			if (doc == null)
			{
				return false;
			}
			Database arg_11_0 = doc.Database;
			bool result;
			try
			{
				using (doc.LockDocument((DocumentLockMode)4, null, null, false))
				{
					var transactionManager = doc.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						RasterImage rasterImage = (RasterImage)transaction.GetObject(rasterId,(OpenMode)1);
						rasterImage.DisableUndoRecording(true);
						ObjectId imageDefId = rasterImage.ImageDefId;
						RasterImageDef rasterImageDef = (RasterImageDef)transaction.GetObject(imageDefId,(OpenMode)1);
						rasterImageDef.DisableUndoRecording(true);
						string sourceFileName = rasterImageDef.SourceFileName;
						try
						{
							rasterImageDef.Unload(true);
							rasterImageDef.SourceFileName=(url);
							rasterImageDef.Load();
							if (rasterImageDef.IsLoaded)
							{
								try
								{
									if (!string.IsNullOrEmpty(sourceFileName))
									{
										File.Delete(sourceFileName);
										if (App.TempFiles.Contains(sourceFileName))
										{
											App.TempFiles.Remove(sourceFileName);
										}
									}
								}
								catch
								{
								}
							}
						}
						catch (Exception)
						{
						}
						try
						{
							rasterImageDef.UpdateEntities();
							rasterImage.Draw();
							doc.TransactionManager.QueueForGraphicsFlush();
							doc.TransactionManager.FlushGraphics();
							doc.Editor.UpdateScreen();
						}
						catch
						{
						}
						transaction.Commit();
					}
				}
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool GetEntityVisibility(Document doc, ObjectId entID)
		{
			Database arg_06_0 = doc.Database;
			bool result = true;
			using (doc.LockDocument((DocumentLockMode)32, null, null, false))
			{
				var transactionManager = doc.TransactionManager;
				using (Transaction transaction = transactionManager.StartTransaction())
				{
					Entity entity = (Entity)transaction.GetObject(entID, 0);
					result = entity.Visible;
					transaction.Commit();
				}
			}
			return result;
		}

		public static void SetEntityDisableUndo(Document doc, ObjectId entID, bool bDisableUndo)
		{
			try
			{
				Database arg_06_0 = doc.Database;
				using (doc.LockDocument((DocumentLockMode)4, null, null, false))
				{
					var  transactionManager = doc.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						Entity entity = (Entity)transaction.GetObject(entID,(OpenMode)1);
						entity.DisableUndoRecording(bDisableUndo);
						transaction.Commit();
					}
				}
			}
			catch
			{
			}
		}

		public static bool SetEntityVisibility(Document doc, ObjectId entID, bool visible)
		{
			bool result;
			try
			{
				Database arg_06_0 = doc.Database;
				using (doc.LockDocument((DocumentLockMode)4, null, null, false))
				{
					var  transactionManager = doc.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						Entity entity = (Entity)transaction.GetObject(entID,(OpenMode)1);
						entity.Visible=(visible);
						doc.TransactionManager.QueueForGraphicsFlush();
						doc.TransactionManager.FlushGraphics();
						doc.Editor.UpdateScreen();
						transaction.Commit();
					}
				}
				PaletteUtils.ActivateEditor();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static ObjectId DefineRasterImage(Document doc, string url, Point3d basePoint, Vector3d v1, Vector3d v2, string suggestedName, byte transparency)
		{
			ObjectId result;
			try
			{
				Image.FromFile(url);
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.InvalidImageFile);
				result = ObjectId.Null;
				return result;
			}
			Database database = doc.Database;
			Editor editor = doc.Editor;
			ObjectId objectId = ObjectId.Null;
			ObjectId arg_39_0 = ObjectId.Null;
			try
			{
				using (doc.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = doc.TransactionManager;
					doc.TransactionManager.EnableGraphicsFlush(true);
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						string.IsNullOrEmpty(suggestedName);
						ObjectId objectId2 = RasterImageDef.GetImageDictionary(database);
						if (objectId2.IsNull)
						{
							objectId2 = RasterImageDef.CreateImageDictionary(database);
						}
						RasterImageDef rasterImageDef = new RasterImageDef();
						rasterImageDef.SourceFileName=(url);
						rasterImageDef.Load();
						bool arg_A4_0 = rasterImageDef.IsLoaded;
						DBDictionary dBDictionary = (DBDictionary)transaction.GetObject(objectId2,(OpenMode)1);
						string text = RasterImageDef.SuggestName(dBDictionary, url);
						if (!string.IsNullOrEmpty(suggestedName))
						{
							text = suggestedName;
							int num = 0;
							while (dBDictionary.Contains(text))
							{
								num++;
								text = suggestedName + num;
							}
						}
						ObjectId arg_F8_0 = ObjectId.Null;
						if (dBDictionary.Contains(text))
						{
							editor.WriteMessage(AfaStrings.ImageAlreadyExits);
							result = ObjectId.Null;
							return result;
						}
						dBDictionary.SetAt(text, rasterImageDef);
						transaction.AddNewlyCreatedDBObject(rasterImageDef, true);
						dBDictionary.Contains(text);
						ObjectId layer = DocUtil.GetLayer(database, transaction, ref text);
						RasterImage rasterImage = new RasterImage();
						rasterImage.ImageDefId=(rasterImageDef.ObjectId);
						rasterImage.LayerId=(layer);
						byte b = Convert.ToByte(Math.Floor((100.0 - (double)transparency) / 100.0 * 254.0));
						Transparency transparency2 = new Transparency(b);
						rasterImage.Transparency=(transparency2);
						rasterImage.ImageTransparency=(true);
						rasterImage.Orientation=(new CoordinateSystem3d(basePoint, v1, v2));
						BlockTable blockTable = (BlockTable)transactionManager.GetObject(database.BlockTableId, 0, false);
						BlockTableRecord blockTableRecord = (BlockTableRecord)transactionManager.GetObject(blockTable[(BlockTableRecord.ModelSpace)]      ,(OpenMode)1, false);
						int num2 = 0;
						try
						{
							num2 = blockTableRecord.Cast<object>().Count<object>();
						}
						catch
						{
						}
						rasterImage.ColorIndex=(256);
						objectId = blockTableRecord.AppendEntity(rasterImage);
						transactionManager.AddNewlyCreatedDBObject(rasterImage, true);
						rasterImage.AssociateRasterDef(rasterImageDef);
						RasterImage.EnableReactors(true);
						rasterImageDef.UpdateEntities();
						DrawOrderTable drawOrderTable = (DrawOrderTable)transaction.GetObject(blockTableRecord.DrawOrderTableId,(OpenMode)1);
						ObjectIdCollection objectIdCollection = new ObjectIdCollection();
						objectIdCollection.Add(objectId);
						drawOrderTable.MoveToBottom(objectIdCollection);
						try
						{
							if (App.UseBeep)
							{
								Console.Beep(392, 800);
							}
							rasterImageDef.UpdateEntities();
							doc.TransactionManager.QueueForGraphicsFlush();
							doc.TransactionManager.FlushGraphics();
							doc.Editor.UpdateScreen();
							if (num2 == 0)
							{
								DocUtil.ZoomExtents(rasterImage.GeometricExtents.MinPoint, rasterImage.GeometricExtents.MaxPoint);
							}
						}
						catch
						{
						}
						transaction.Commit();
					}
				}
				result = objectId;
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				MessageBox.Show(AfaStrings.AutoCADErrorWhenAddingRasterImage + message);
				ObjectId arg_31F_0 = ObjectId.Null;
				result = ObjectId.Null;
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.UnexpectedErrorInAddingRasterImage);
				result = ObjectId.Null;
			}
			return result;
		}

		public static bool AttachHyperlink(Document doc, ObjectId entID, string name, string hyperlinkURL)
		{
			if (entID.IsNull)
			{
				return false;
			}
			Database arg_11_0 = doc.Database;
			bool result;
			try
			{
				using (doc.LockDocument((DocumentLockMode)4, null, null, false))
				{
					var transactionManager = doc.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						Entity entity = (Entity)transaction.GetObject(entID,(OpenMode)1);
						HyperLinkCollection hyperlinks = entity.Hyperlinks;
						HyperLink hyperLink = new HyperLink();
						hyperLink.Description=("ArcGIS Service");
						hyperLink.SubLocation=("");
						hyperLink.Name=(hyperlinkURL);
						if (!hyperlinks.Contains(hyperLink))
						{
							hyperlinks.Add(hyperLink);
						}
						transaction.Commit();
					}
				}
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static void SetEntityLayerTransparency(Document doc, ObjectId id, Transparency trpy)
		{
			using (doc.LockDocument())
			{
				doc.TransactionManager.EnableGraphicsFlush(true);
				var  transactionManager = doc.TransactionManager;
				using (Transaction transaction = transactionManager.StartTransaction())
				{
					BlockTable blockTable = (BlockTable)transaction.GetObject(doc.Database.BlockTableId, 0);
					BlockTableRecord arg_50_0 = (BlockTableRecord)transaction.GetObject(blockTable[(BlockTableRecord.ModelSpace)], 0);
					Entity entity = (Entity)transaction.GetObject(id, 0);
					LayerTableRecord layerTableRecord = (LayerTableRecord)transaction.TransactionManager.GetObject(entity.LayerId,(OpenMode)1, false);
					layerTableRecord.Transparency=(trpy);
					doc.TransactionManager.QueueForGraphicsFlush();
					doc.TransactionManager.FlushGraphics();
					doc.Editor.UpdateScreen();
					transaction.Commit();
				}
			}
		}

		public static bool SetCurrentDrawingLayer(string layerName)
		{
			bool result;
			try
			{
				Database database = Application.DocumentManager.MdiActiveDocument.Database;
				using (Application.DocumentManager.MdiActiveDocument.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						database.Clayer=(DocUtil.GetLayer(database, transaction, ref layerName));
						transaction.Commit();
					}
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static string GenerateUniqueLayerName(Database db, Transaction t, string baseName)
		{
			if (!string.IsNullOrEmpty(baseName))
			{
				baseName = DocUtil.FixLayerName(baseName);
				LayerTable layerTable = (LayerTable)t.TransactionManager.GetObject(db.LayerTableId,(OpenMode)1, false);
				new LayerTableRecord();
				string text = baseName;
				int num =1;
				while (layerTable.Has(text))
				{
					text = baseName + num;
					num++;
				}
				return text;
			}
			return baseName;
		}

		public static void SetLayerLock(Database db, Transaction t, string layerName, bool setLock)
		{
			ObjectId layer = DocUtil.GetLayer(db, t, ref layerName);
			if (layer.IsNull)
			{
				return;
			}
			LayerTableRecord layerTableRecord = (LayerTableRecord)t.GetObject(layer,(OpenMode)1, false);
			layerTableRecord.IsLocked=(setLock);
			layerTableRecord.DisableUndoRecording(setLock);
		}

		public static void HideLayer(Database db, Transaction t, string layerName, bool hide)
		{
			LayerTable layerTable = (LayerTable)t.TransactionManager.GetObject(db.LayerTableId,(OpenMode)1, false);
			if (layerTable.Has(layerName))
			{
				ObjectId objectId = layerTable[(layerName)];
				if (objectId.IsNull)
				{
					return;
				}
				LayerTableRecord layerTableRecord = (LayerTableRecord)t.TransactionManager.GetObject(objectId,(OpenMode)1, false);
				layerTableRecord.IsFrozen=(hide);
				layerTableRecord.IsOff=(hide);
				layerTableRecord.DisableUndoRecording(hide);
			}
		}

		public static ObjectId GetLayer(Database db, Transaction t, ref string layerName)
		{
			return DocUtil.GetLayer(db, t, ref layerName, null);
		}

		public static ObjectId GetLayer(Database db, Transaction t, ref string layerName, AGSColor suggestedColor)
		{
			ObjectId result = db.Clayer;
			if (!string.IsNullOrEmpty(layerName))
			{
				layerName = DocUtil.FixLayerName(layerName);
				LayerTable layerTable = (LayerTable)t.TransactionManager.GetObject(db.LayerTableId,(OpenMode)1, false);
				if (layerTable.Has(layerName))
				{
					result = layerTable[(layerName)];
				}
				else
				{
					LayerTableRecord layerTableRecord = new LayerTableRecord();
					layerTableRecord.Name=(layerName);
					if (suggestedColor != null)
					{
						layerTableRecord.Color=(Color.FromRgb(suggestedColor.Red, suggestedColor.Green, suggestedColor.Blue));
					}
					else
					{
						layerTableRecord.Color=(Color.FromColorIndex((ColorMethod)195, DocUtil.GetRandomColor()));
					}
					result = layerTable.Add(layerTableRecord);
					t.TransactionManager.AddNewlyCreatedDBObject(layerTableRecord, true);
				}
			}
			return result;
		}

		public static ObjectId GetExistingLayer(Database db, Transaction t, string layerName)
		{
			ObjectId result = ObjectId.Null;
			if (!string.IsNullOrEmpty(layerName))
			{
				LayerTable layerTable = (LayerTable)t.TransactionManager.GetObject(db.LayerTableId,(OpenMode)1, false);
				if (layerTable.Has(layerName))
				{
					result = layerTable[(layerName)];
				}
			}
			return result;
		}

		public static string RenameCADLayer(Transaction t, Database db, string originalName, string newName)
		{
			LayerTable layerTable = (LayerTable)t.TransactionManager.GetObject(db.LayerTableId,(OpenMode)1, false);
			if (layerTable.Has(originalName))
			{
				ObjectId objectId = layerTable[(originalName)];
				LayerTableRecord layerTableRecord = (LayerTableRecord)t.TransactionManager.GetObject(objectId,(OpenMode)1, false);
				layerTableRecord.Name=(newName);
				return newName;
			}
			return null;
		}

		public static string RenameCADLayer(Document doc, string originalName, string newName)
		{
			newName = DocUtil.FixLayerName(newName);
			Database database = doc.Database;
			string result;
			try
			{
				using (doc.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					database.DisableUndoRecording(true);
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						newName = DocUtil.RenameCADLayer(transaction, database, originalName, newName);
						transaction.Commit();
						database.DisableUndoRecording(false);
					}
				}
				result = newName;
			}
			catch (Exception)
			{
				database.DisableUndoRecording(false);
				result = newName;
			}
			return result;
		}

		public static void SwitchLayers(string lyrName, string newLyrName, Database db, Transaction t)
		{
			ObjectId existingLayer = DocUtil.GetExistingLayer(db, t, newLyrName);
			if (existingLayer != ObjectId.Null)
			{
				DocUtil.SwitchLayers(lyrName, existingLayer, t);
			}
		}

		public static void SwitchLayers(string lyrName, ObjectId newLyrId, Transaction t)
		{
			try
			{
				Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
				try
				{
					editor.SetImpliedSelection(new ObjectId[0]);
				}
				catch
				{
				}
				ResultBuffer resultBuffer = new ResultBuffer(new TypedValue[]
				{
					new TypedValue(8, lyrName)
				});
				SelectionFilter selectionFilter = new SelectionFilter(resultBuffer.AsArray());
				PromptSelectionResult promptSelectionResult = editor.SelectAll(selectionFilter);
				if (promptSelectionResult.Status == (PromptStatus)5100)
				{
					ObjectIdCollection objectIdCollection = new ObjectIdCollection(promptSelectionResult.Value.GetObjectIds());
					foreach (ObjectId objectId in objectIdCollection)
					{
						Entity entity = (Entity)t.GetObject(objectId,(OpenMode)1, false);
						entity.SetLayerId(newLyrId, true);
					}
				}
			}
			catch
			{
			}
		}

		public static void DeleteCADLayers(List<string> layerNames, Database db, Transaction t)
		{
			LayerTable layerTable = (LayerTable)t.TransactionManager.GetObject(db.LayerTableId,(OpenMode)1, false);
			ObjectId layerZero = db.LayerZero;
			foreach (string current in layerNames)
			{
				if (layerTable.Has(current))
				{
					ObjectId objectId = layerTable[(current)];
					if (db.Clayer == objectId)
					{
						db.Clayer=(layerZero);
					}
					DocUtil.SwitchLayers(current, layerZero, t);
					LayerTableRecord layerTableRecord = (LayerTableRecord)t.TransactionManager.GetObject(objectId,(OpenMode)1, false);
					layerTableRecord.Erase();
				}
			}
		}

		public static void DeleteCADLayers(List<string> layerNames)
		{
			try
			{
				Database database = Application.DocumentManager.MdiActiveDocument.Database;
				try
				{
					using (Application.DocumentManager.MdiActiveDocument.LockDocument((DocumentLockMode)20, null, null, false))
					{
						var  transactionManager = database.TransactionManager;
						database.DisableUndoRecording(true);
						using (Transaction transaction = transactionManager.StartTransaction())
						{
							DocUtil.DeleteCADLayers(layerNames, database, transaction);
							transaction.Commit();
						}
						database.DisableUndoRecording(false);
					}
				}
				catch
				{
					database.DisableUndoRecording(false);
				}
			}
			catch
			{
			}
		}

		public static string FixSymbolName(string name)
		{
			name = name.Replace("<", "_lt_");
			name = name.Replace(">", "_gt_");
			name = name.Replace("/", ".");
			name = name.Replace("\\", ".");
			name = name.Replace(":", ".");
			name = name.Replace("?", ".");
			name = name.Replace("*", ".");
			name = name.Replace("|", "-");
			name = name.Replace("=", "-");
			return name;
		}

		public static string FixLayerName(string name)
		{
			name = DocUtil.FixSymbolName(name);
			if (name.Length > 253)
			{
				name.Remove(253);
			}
			return name;
		}

		public static void HighlightEntities(ObjectIdCollection objIds)
		{
			Document mdiActiveDocument = Application.DocumentManager.MdiActiveDocument;
			if (mdiActiveDocument == null)
			{
				return;
			}
			Transaction transaction = mdiActiveDocument.TransactionManager.StartTransaction();
			using (transaction)
			{
				foreach (ObjectId objectId in objIds)
				{
					Entity entity = transaction.GetObject(objectId, 0) as Entity;
					if (entity != null)
					{
						entity.Highlight();
						mdiActiveDocument.TransactionManager.QueueForGraphicsFlush();
						mdiActiveDocument.TransactionManager.FlushGraphics();
						mdiActiveDocument.Editor.UpdateScreen();
					}
				}
				transaction.Commit();
			}
		}

		public static void DrawEntities(List<Entity> entList)
		{
			Database database = AfaDocData.ActiveDocData.Document.Database;
			Document document = AfaDocData.ActiveDocData.Document;
			var  transactionManager = document.TransactionManager;
			using (Transaction transaction = transactionManager.StartTransaction())
			{
				BlockTable blockTable = (BlockTable)transactionManager.GetObject(database.BlockTableId, 0, false);
				BlockTableRecord blockTableRecord = (BlockTableRecord)transactionManager.GetObject(blockTable[(BlockTableRecord.ModelSpace)],(OpenMode)1, false);
				foreach (Entity current in entList)
				{
					current.ColorIndex=(256);
					blockTableRecord.AppendEntity(current);
					transactionManager.AddNewlyCreatedDBObject(current, true);
					document.TransactionManager.QueueForGraphicsFlush();
					document.TransactionManager.FlushGraphics();
					document.Editor.UpdateScreen();
				}
				transaction.Commit();
			}
		}

		public static Dictionary<ObjectId, int> AddCadFeaturesToModelSpace(Document doc, List<CadFeature> cfList, string layerName)
		{
			Dictionary<ObjectId, int> dictionary = new Dictionary<ObjectId, int>();
			Database database = doc.Database;
			Dictionary<ObjectId, int> result;
			try
			{
				using (doc.LockDocument((DocumentLockMode)20, null, null, false))
				{
					database.DisableUndoRecording(true);
					var  transactionManager = doc.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						ObjectId layer = DocUtil.GetLayer(database, transaction, ref layerName);
						BlockTable blockTable = (BlockTable)transactionManager.GetObject(database.BlockTableId, 0, false);
						BlockTableRecord blockTableRecord = (BlockTableRecord)transactionManager.GetObject(blockTable[(BlockTableRecord.ModelSpace)],(OpenMode)1, false);
						foreach (CadFeature current in cfList)
						{
							ObjectId key = ObjectId.Null;
							ObjectId objectId = ObjectId.Null;
							Group group = new Group("ESRI Feature", true);
							if (current.EntList.Count >1)
							{
								DBDictionary dBDictionary = (DBDictionary)transactionManager.GetObject(database.GroupDictionaryId,(OpenMode)1);
								objectId = dBDictionary.SetAt("ESRI_TEMP", group);
								group.SetAnonymous();
								group.CreateExtensionDictionary();
								transactionManager.AddNewlyCreatedDBObject(group, true);
								key = objectId;
							}
							foreach (Entity current2 in current.EntList)
							{
								Entity entity = current2;
								ObjectId objectId2 = entity.ObjectId;
								if (objectId2 == ObjectId.Null)
								{
									entity.SetLayerId(layer, true);
									entity.ColorIndex=(256);
									objectId2 = blockTableRecord.AppendEntity(entity);
									transactionManager.AddNewlyCreatedDBObject(entity, true);
								}
								else
								{
									entity = (Entity)transactionManager.GetObject(objectId2,(OpenMode)1);
									entity.SetLayerId(layer, true);
									entity.ColorIndex=(256);
								}
								if (objectId != ObjectId.Null)
								{
									group.Append(objectId2);
								}
								else
								{
									key = objectId2;
								}
							}
							if (current.Fields != null)
							{
								try
								{
									DBDictionary dBDictionary2;
									if (objectId != ObjectId.Null)
									{
										dBDictionary2 = (DBDictionary)transaction.GetObject(group.ExtensionDictionary,(OpenMode)1, false);
									}
									else
									{
										current.EntList[0].CreateExtensionDictionary();
										dBDictionary2 = (DBDictionary)transaction.GetObject(current.EntList[0].ExtensionDictionary,(OpenMode)1, false);
									}
									if (dBDictionary2 != null)
									{
										DBDictionary dBDictionary3 = new DBDictionary();
										dBDictionary2.SetAt("ESRI_ATTRIBUTES", dBDictionary3);
										dBDictionary3.DisableUndoRecording(true);
										transaction.AddNewlyCreatedDBObject(dBDictionary3, true);
										CadField.AddCadAttributesToEntityDictionary(transaction, dBDictionary3, current.Fields);
									}
								}
								catch
								{
									DocUtil.ShowDebugMessage(AfaStrings.ErrorInAttachingFields);
								}
							}
							dictionary.Add(key, current.SvcOID);
							doc.TransactionManager.QueueForGraphicsFlush();
							doc.TransactionManager.FlushGraphics();
							doc.Editor.UpdateScreen();
						}
						transaction.Commit();
						database.DisableUndoRecording(false);
					}
				}
				result = dictionary;
			}
			catch (Exception ex)
			{
				database.DisableUndoRecording(false);
				ErrorReport.ShowErrorMessage(ex.Message);
				result = dictionary;
			}
			return result;
		}

		public static ObjectId AddEntityToModelSpace(Document doc, Entity ent)
		{
			Database database = doc.Database;
			ObjectId result;
			try
			{
				using (doc.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = doc.TransactionManager;
					database.DisableUndoRecording(true);
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						BlockTable blockTable = (BlockTable)transactionManager.GetObject(database.BlockTableId, 0, false);
						BlockTableRecord blockTableRecord = (BlockTableRecord)transactionManager.GetObject(blockTable[(BlockTableRecord.ModelSpace)],(OpenMode)1, false);
						ent.ColorIndex=(256);
						ObjectId objectId = blockTableRecord.AppendEntity(ent);
						transactionManager.AddNewlyCreatedDBObject(ent, true);
						doc.TransactionManager.QueueForGraphicsFlush();
						doc.TransactionManager.FlushGraphics();
						doc.Editor.UpdateScreen();
						transaction.Commit();
						database.DisableUndoRecording(false);
						result = objectId;
					}
				}
			}
			catch (Exception ex)
			{
				database.DisableUndoRecording(false);
				ErrorReport.ShowErrorMessage(ex.Message);
				result = ObjectId.Null;
			}
			return result;
		}

		public static void EraseEntities(ObjectId[] ids, Transaction t)
		{
			if (ids == null)
			{
				return;
			}
			if (ids.Length == 0)
			{
				return;
			}
			for (int i = 0; i < ids.Length; i++)
			{
				ObjectId objectId = ids[i];
				if (objectId.ObjectClass.DxfName != "GROUP")
				{
					Entity entity = (Entity)t.GetObject(objectId,(OpenMode)1);
					entity.Erase();
					entity.Dispose();
				}
			}
		}

		public static bool EraseEntities(Document doc, ObjectId[] ids)
		{
			if (ids == null)
			{
				return true;
			}
			if (ids.Length == 0)
			{
				return true;
			}
			Database database = doc.Database;
			bool result;
			try
			{
				using (doc.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = doc.TransactionManager;
					database.DisableUndoRecording(true);
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						DocUtil.EraseEntities(ids, transaction);
						doc.TransactionManager.QueueForGraphicsFlush();
						doc.TransactionManager.FlushGraphics();
						doc.Editor.UpdateScreen();
						transaction.Commit();
					}
					database.DisableUndoRecording(false);
					result = true;
				}
			}
			catch (Exception)
			{
				database.DisableUndoRecording(false);
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorDeletingEntities);
				result = false;
			}
			catch
			{
				database.DisableUndoRecording(false);
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorDeletingEntities);
				result = false;
			}
			return result;
		}

		internal static ObjectIdCollection ExpandGroupObjectIds(Document doc, ObjectIdCollection sourceIds)
		{
			if (sourceIds.Count == 0)
			{
				return sourceIds;
			}
			ObjectIdCollection objectIdCollection = new ObjectIdCollection();
			using (doc.LockDocument((DocumentLockMode)32, null, null, false))
			{
				using (Transaction transaction = doc.Database.TransactionManager.StartTransaction())
				{
					foreach (ObjectId objectId in sourceIds)
					{
						DBObject @object = transaction.GetObject(objectId, 0);
						if (@object is Entity)
						{
							objectIdCollection.Add(objectId);
						}
						else if (@object is Group)
						{
							Group group = (Group)@object;
							ObjectId[] allEntityIds = group.GetAllEntityIds();
							ObjectId[] array = allEntityIds;
							for (int i = 0; i < array.Length; i++)
							{
								ObjectId objectId2 = array[i];
								DBObject object2 = transaction.GetObject(objectId2, 0);
								if (object2 is Entity)
								{
									objectIdCollection.Add(objectId2);
								}
							}
						}
					}
					transaction.Commit();
				}
			}
			return objectIdCollection;
		}

		internal static ObjectId[] ExpandGroupObjectIds(Document doc, ObjectId[] sourceIds)
		{
			if (sourceIds == null || sourceIds.Length == 0)
			{
				return sourceIds;
			}
			List<ObjectId> list = new List<ObjectId>();
			using (doc.LockDocument((DocumentLockMode)32, null, null, false))
			{
				using (Transaction transaction = doc.Database.TransactionManager.StartTransaction())
				{
					for (int i = 0; i < sourceIds.Length; i++)
					{
						ObjectId objectId = sourceIds[i];
						DBObject @object = transaction.GetObject(objectId, 0);
						if (@object is Entity)
						{
							list.Add(objectId);
						}
						else if (@object is Group)
						{
							Group group = (Group)@object;
							ObjectId[] allEntityIds = group.GetAllEntityIds();
							ObjectId[] array = allEntityIds;
							for (int j = 0; j < array.Length; j++)
							{
								ObjectId objectId2 = array[j];
								DBObject object2 = transaction.GetObject(objectId2, 0);
								if (object2 is Entity)
								{
									list.Add(objectId2);
								}
							}
						}
					}
					transaction.Commit();
				}
			}
			return list.ToArray();
		}

		public static void EraseGroupEntities(ObjectId[] ids, Transaction t)
		{
			for (int i = 0; i < ids.Length; i++)
			{
				ObjectId objectId = ids[i];
				if (objectId.ObjectClass.DxfName == "GROUP")
				{
					Group group = (Group)t.GetObject(objectId,(OpenMode)1);
					ObjectId[] allEntityIds = group.GetAllEntityIds();
					ObjectId[] array = allEntityIds;
					for (int j = 0; j < array.Length; j++)
					{
						ObjectId objectId2 = array[j];
						DBObject @object = t.GetObject(objectId2,(OpenMode)1);
						@object.Erase();
					}
					group.Erase();
				}
			}
		}

		public static void EraseGroups(ObjectId[] ids, Transaction t)
		{
			for (int i = 0; i < ids.Length; i++)
			{
				ObjectId objectId = ids[i];
				if (objectId.ObjectClass.DxfName == "GROUP")
				{
					Group group = (Group)t.GetObject(objectId,(OpenMode)1);
					group.Erase();
					group.Dispose();
				}
			}
		}

		public static void EraseGroupEntities(Document doc, ObjectId[] ids)
		{
			if (ids == null)
			{
				return;
			}
			if (ids.Length == 0)
			{
				return;
			}
			Database database = doc.Database;
			try
			{
				using (doc.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						DocUtil.EraseGroupEntities(ids, transaction);
						transaction.Commit();
					}
				}
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorDeletingEntities);
			}
		}

		public static void EraseGroups(Document doc, ObjectId[] ids)
		{
			if (ids == null)
			{
				return;
			}
			if (ids.Length == 0)
			{
				return;
			}
			Database database = doc.Database;
			try
			{
				using (doc.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						DocUtil.EraseGroups(ids, transaction);
						transaction.Commit();
					}
				}
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorDeletingEntities);
			}
		}

		private static short GetRandomColor()
		{
			if (DocUtil.randomColorGenerator == null)
			{
				DocUtil.randomColorGenerator = new Random(DateTime.Now.Millisecond);
			}
			int num = DocUtil.randomColorGenerator.Next(0, DocUtil.BrightColors.Length - 1);
			return DocUtil.BrightColors[num];
		}

		public static void ZoomToEntity(ObjectId objId)
		{
			DocUtil.ZoomToEntity(new List<ObjectId>
			{
				objId
			}.ToArray());
		}

		public static void ZoomToEntity(ObjectId[] objIds)
		{
			if (objIds.Count<ObjectId>() == 0)
			{
				return;
			}
			try
			{
				Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
				ObjectId[] impliedSelection = DocUtil.ExpandGroupObjectIds(AfaDocData.ActiveDocData.Document, objIds);
				editor.SetImpliedSelection(impliedSelection);
				Utils.ZoomObjects(false);
				editor.Regen();
				editor.SetImpliedSelection(impliedSelection);
				AfaDocData.ActiveDocData.Document.Editor.UpdateScreen();
				AfaDocData.ActiveDocData.DocDataset.UpdateMaps();
				editor.Regen();
			}
			catch
			{
			}
		}

		public static void HighlightEntities(ObjectId[] ids)
		{
		}

		private static DBDictionary OpenDataset(Database db, Transaction t, OpenMode mode)
		{
			DBDictionary dBDictionary = DocUtil.OpenNOD(db, t, mode);
			if (dBDictionary.Contains("ESRI_FEATURES"))
			{
				return (DBDictionary)t.GetObject(dBDictionary.GetAt("ESRI_FEATURES"), mode);
			}
			DBDictionary dBDictionary2 = new DBDictionary();
			ObjectId objectId = dBDictionary.SetAt("ESRI_FEATURES", dBDictionary2);
			t.AddNewlyCreatedDBObject(dBDictionary2, true);
			return (DBDictionary)t.GetObject(objectId, mode);
		}

		private static DBDictionary OpenFeatureClass(Database db, Transaction t, string name, OpenMode mode)
		{
			DBDictionary dBDictionary = DocUtil.OpenDataset(db, t, mode);
			if (dBDictionary.Contains(name))
			{
				return (DBDictionary)t.GetObject(dBDictionary.GetAt(name), mode);
			}
			DBDictionary dBDictionary2 = new DBDictionary();
			ObjectId objectId = dBDictionary.SetAt(name, dBDictionary2);
			t.AddNewlyCreatedDBObject(dBDictionary2, true);
			return (DBDictionary)t.GetObject(objectId, mode);
		}

		public static string ListFeatureClasses()
		{
			string text = "";
			string result;
			try
			{
				Database database = Application.DocumentManager.MdiActiveDocument.Database;
				using (Application.DocumentManager.MdiActiveDocument.LockDocument((DocumentLockMode)32, null, null, false))
				{
					var transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						DBDictionary dBDictionary = DocUtil.OpenDataset(database, transaction, 0);
						using (DbDictionaryEnumerator enumerator = dBDictionary.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								DBDictionaryEntry current = enumerator.Current;
								DBDictionary dBDictionary2 = DocUtil.OpenFeatureClass(database, transaction, current.Key, 0);
								string text2 = "";
								string text3 = "";
								if (dBDictionary2.Contains("FeatureType"))
								{
									Xrecord xrecord = (Xrecord)transaction.GetObject(dBDictionary2.GetAt("FeatureType"), 0);
									ResultBuffer data = xrecord.Data;
									TypedValue[] array = data.AsArray();
									text2 = array[0].Value.ToString();
								}
								if (dBDictionary2.Contains("FeatureQuery"))
								{
									Xrecord xrecord2 = (Xrecord)transaction.GetObject(dBDictionary2.GetAt("FeatureQuery"), 0);
									ResultBuffer data2 = xrecord2.Data;
									text3 = data2.ToString();
								}
								string str = string.Concat(new string[]
								{
									current.Key,
									", ",
									AfaStrings.Type,
									" = ",
									text2,
									"\n ",
									AfaStrings.Query,
									" = ",
									text3
								});
								text += str;
								text += "\n";
								if (dBDictionary2.Contains("ESRI_Subtypes"))
								{
									DBDictionary dBDictionary3 = (DBDictionary)transaction.GetObject(dBDictionary2.GetAt("ESRI_Subtypes"), 0);
									using (DbDictionaryEnumerator enumerator2 = dBDictionary3.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											DBDictionaryEntry current2 = enumerator2.Current;
											string text4 = text;
											text = string.Concat(new string[]
											{
												text4,
												AfaStrings.Subtype,
												": ",
												current2.Key,
												"\n"
											});
										}
									}
								}
								if (dBDictionary2.Contains("ESRI_ATTRIBUTES"))
								{
									DBDictionary dBDictionary4 = (DBDictionary)transaction.GetObject(dBDictionary2.GetAt("ESRI_ATTRIBUTES"), 0);
									using (DbDictionaryEnumerator enumerator3 = dBDictionary4.GetEnumerator())
									{
										while (enumerator3.MoveNext())
										{
											DBDictionaryEntry current3 = enumerator3.Current;
											object obj = text;
											text = string.Concat(new object[]
											{
												obj,
												AfaStrings.Attribute,
												": ",
												current3.Key,
												" = ",
												current3.Value
											});
										}
									}
								}
							}
						}
						transaction.Commit();
					}
				}
				result = text;
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static string DXFCodeToString(int typeCode)
		{
			int num = typeCode;
			if (num <= 38)
			{
				switch (num)
				{
				case 0:
					return AfaStrings.EntityType;
				case 1:
				case 3:
				case 4:
				case 5:
					break;
				case 2:
					return AfaStrings.BlockName;
				case 6:
					return AfaStrings.LinetypeName;
				case 7:
					return AfaStrings.TextStyleName;
				case 8:
					return AfaStrings.Layer;
				default:
					if (num == 38)
					{
						return AfaStrings.Elevation;
					}
					break;
				}
			}
			else
			{
				if (num == 48)
				{
					return AfaStrings.LinetypeScale;
				}
				if (num == 62)
				{
					return AfaStrings.ColorNumber;
				}
			}
			return typeCode.ToString();
		}

		public static bool StringToDXFCode(string value, ref int code)
		{
			string a = value.ToLower();
			code = 0;
			if (a == AfaStrings.Layer.ToLower())
			{
				code = 8;
			}
			else if (a == AfaStrings.ColorNumber.ToLower())
			{
				code = 62;
			}
			else if (a == AfaStrings.LinetypeName.ToLower())
			{
				code = 6;
			}
			else if (a == AfaStrings.LinetypeScale.ToLower())
			{
				code = 48;
			}
			else if (a == AfaStrings.TextStyleName.ToLower())
			{
				code = 7;
			}
			else if (a == AfaStrings.BlockName.ToLower())
			{
				code = 2;
			}
			return code != 0;
		}

		public static void ShowDebugMessage(string msg)
		{
            bool flag1 = Application.DocumentManager.MdiActiveDocument == null;
        }

		public static bool TestMDIDoc()
		{
			bool result;
			try
			{
				if (Application.DocumentManager.MdiActiveDocument.IsDisposed)
				{
					result = true;
				}
				else
				{
					result = true;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool TestActiveDocData()
		{
			bool result;
			try
			{
				bool arg_19_0 = AfaDocData.ActiveDocData.DocDataset.ParentDocument.Editor.IsQuiescent;
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		public static void ZoomExtents()
		{
			try
			{
				Document document = AfaDocData.ActiveDocData.Document;
				using (document.LockDocument())
				{
					Database database = document.Database;
					Editor arg_1F_0 = document.Editor;
					document.Database.UpdateExt(true);
					Point3d extmax = database.Extmax;
					Point3d extmin = database.Extmin;
					new Extent(extmin, extmax);
					DocUtil.ZoomExtents(extmin, extmax);
				}
			}
			catch
			{
			}
		}

		public static void ZoomExtents(Point3d minPoint, Point3d maxPoint)
		{
			try
			{
				Document document = AfaDocData.ActiveDocData.Document;
				Editor editor = document.Editor;
				ViewTableRecord currentView = editor.GetCurrentView();
				double num = currentView.Width / currentView.Height;
				double num2 = maxPoint.X - minPoint.X;
				double num3 = maxPoint.Y - minPoint.Y;
				if (num2 > num3 * num)
				{
					num3 = num2 / num;
				}
				Point2d centerPoint = new Point2d((maxPoint.X + minPoint.X) / 2.0, (maxPoint.Y + minPoint.Y) / 2.0);
				currentView.Height=(num3);
				currentView.Width=(num2);
				currentView.CenterPoint=(centerPoint);
				editor.SetCurrentView(currentView);
			}
			catch
			{
			}
		}

		public static int EntityCount()
		{
			int result = 0;
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			using (document.LockDocument())
			{
				var  transactionManager = document.TransactionManager;
				using (transactionManager.StartTransaction())
				{
					BlockTable blockTable = (BlockTable)transactionManager.GetObject(database.BlockTableId, 0, false);
					BlockTableRecord source = (BlockTableRecord)transactionManager.GetObject(blockTable[(BlockTableRecord.ModelSpace)],(OpenMode)1, false);
					try
					{
						result = source.Cast<object>().Count<object>();
					}
					catch
					{
					}
				}
			}
			return result;
		}

		public static bool IsNewDrawing(Document doc)
		{
			bool result;
			try
			{
				result = (doc.Name != doc.Database.Filename);
			}
			catch
			{
				result = true;
			}
			return result;
		}

		public static void ShowBlocks(Document doc)
		{
			try
			{
				Database arg_06_0 = doc.Database;
				MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
				List<ObjectId> list = new List<ObjectId>();
				using (doc.LockDocument())
				{
					using (Transaction transaction = doc.TransactionManager.StartTransaction())
					{
						foreach (MSCFeatureService current in docDataset.FeatureServices.Values)
						{
							if (!current.QueryOnly && current.GeometryType == MSCFeatureClass.fcTypeCode.fcTypePoint)
							{
								ObjectId blockDefinition = DocUtil.GetBlockDefinition(doc, current.LayerName);
								if (!(blockDefinition == ObjectId.Null))
								{
									list.Add(blockDefinition);
									BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(blockDefinition,(OpenMode)1);
									using (BlockTableRecordEnumerator enumerator2 = blockTableRecord.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											ObjectId current2 = enumerator2.Current;
											Entity entity = (Entity)transaction.GetObject(current2,(OpenMode)1);
											if (entity != null)
											{
												entity.RecordGraphicsModified(true);
												LayerTableRecord layerTableRecord = (LayerTableRecord)transaction.GetObject(entity.LayerId,(OpenMode)1);
												layerTableRecord.IsHidden=(false);
												layerTableRecord.IsFrozen=(false);
												doc.TransactionManager.QueueForGraphicsFlush();
												doc.TransactionManager.FlushGraphics();
												doc.Editor.UpdateScreen();
											}
										}
									}
									foreach (MSCFeatureClassSubType current3 in current.SubTypes)
									{
										ObjectId blockDefinition2 = DocUtil.GetBlockDefinition(doc, current3.CadLayerName);
										if (!(blockDefinition2 == ObjectId.Null))
										{
											list.Add(blockDefinition2);
											BlockTableRecord arg_181_0 = (BlockTableRecord)transaction.GetObject(blockDefinition2,(OpenMode)1);
											using (BlockTableRecordEnumerator enumerator4 = blockTableRecord.GetEnumerator())
											{
												while (enumerator4.MoveNext())
												{
													ObjectId current4 = enumerator4.Current;
													Entity entity2 = (Entity)transaction.GetObject(current4,(OpenMode)1);
													if (entity2 != null)
													{
														entity2.RecordGraphicsModified(true);
														LayerTableRecord layerTableRecord2 = (LayerTableRecord)transaction.GetObject(entity2.LayerId,(OpenMode)1);
														layerTableRecord2.IsHidden=(false);
														layerTableRecord2.IsFrozen=(false);
														doc.TransactionManager.QueueForGraphicsFlush();
														doc.TransactionManager.FlushGraphics();
														doc.Editor.UpdateScreen();
													}
												}
											}
										}
									}
								}
							}
						}
						transaction.Commit();
					}
				}
			}
			catch
			{
			}
		}

		public static bool GetDimensionalInputEnabled()
		{
			bool result;
			try
			{
				object systemVariable = Application.GetSystemVariable("DYNMODE");
				short num = (short)systemVariable;
				if (num > 1)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static void EnableTransparency()
		{
			try
			{
				Application.SetSystemVariable("TRANSPARENCYDISPLAY", 1);
			}
			catch
			{
			}
		}

		public static Matrix3d Wcs2Dcs(AbstractViewTableRecord vtr)
		{
			Matrix3d matrix3d = Matrix3d.PlaneToWorld(vtr.ViewDirection);
			matrix3d = Matrix3d.Displacement(vtr.Target - Point3d.Origin) * matrix3d;
			return (Matrix3d.Rotation(-vtr.ViewTwist, vtr.ViewDirection, vtr.Target) * matrix3d).Inverse();
		}

		public static bool GetActiveViewportExtents(Document aDoc, out Extent ext)
		{
			Point3d point3d;
			Point3d point3d2;
			if (DocUtil.GetActiveExtents(out point3d, out point3d2))
			{
				Point3d p = new Point3d(point3d.X, point3d.Y, 0.0);
				Point3d p2 = new Point3d(point3d2.X, point3d2.Y, 0.0);
				ext = new Extent(p, p2);
				ext.SpatialReference = AfaDocData.ActiveDocData.DocPRJ.WKT;
				return true;
			}
			ext = new Extent();
			return false;
		}

		public static bool GetActiveExtents(Document doc, out Point3d minPoint, out Point3d maxPoint)
		{
			minPoint = Point3d.Origin;
			maxPoint = Point3d.Origin;
			bool result;
			try
			{
				Point3d point3d = (Point3d)Application.GetSystemVariable("VIEWCTR");
				double num = (double)Application.GetSystemVariable("VIEWSIZE");
				Point2d point2d = (Point2d)Application.GetSystemVariable("SCREENSIZE");
				Point2d point2d2 = new Point2d(point2d.X, point2d.Y);
				double num2 = 0.5 * num * (point2d2.X / point2d2.Y);
				double num3 = 0.5 * num;
				Extents2d extents2d = new Extents2d(point3d.X - num2, point3d.Y - num3, point3d.X + num2, point3d.Y + num3);
				minPoint = new Point3d(extents2d.MinPoint.X, extents2d.MinPoint.Y, 0.0);
				maxPoint = new Point3d(extents2d.MaxPoint.X, extents2d.MaxPoint.Y, 0.0);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool GetActiveExtents(out Point3d minPoint, out Point3d maxPoint)
		{
			minPoint = Point3d.Origin;
			maxPoint = Point3d.Origin;
			Document mdiActiveDocument = Application.DocumentManager.MdiActiveDocument;
			return !(mdiActiveDocument == null) && DocUtil.GetActiveExtents(mdiActiveDocument, out minPoint, out maxPoint);
		}

		public static void ZoomFromAcadDoc(Point3d pMin, Point3d pMax, Point3d pCenter, double dFactor)
		{
			Document mdiActiveDocument = Application.DocumentManager.MdiActiveDocument;
			Database database = mdiActiveDocument.Database;
			int num = Convert.ToInt32(Application.GetSystemVariable("CVPORT"));
			if (database.TileMode)
			{
				if (pMin.Equals(default(Point3d)) && pMax.Equals(default(Point3d)))
				{
					pMin = database.Extmin;
					pMax = database.Extmax;
				}
			}
			else if (num == 1)
			{
				if (pMin.Equals(default(Point3d)) && pMax.Equals(default(Point3d)))
				{
					pMin = database.Pextmin;
					pMax = database.Pextmax;
				}
			}
			else if (pMin.Equals(default(Point3d)) && pMax.Equals(default(Point3d)))
			{
				pMin = database.Extmin;
				pMax = database.Extmax;
			}
			using (Transaction transaction = mdiActiveDocument.TransactionManager.StartTransaction())
			{
				using (ViewTableRecord currentView = mdiActiveDocument.Editor.GetCurrentView())
				{
					Matrix3d matrix3d = Matrix3d.PlaneToWorld(currentView.ViewDirection);
					matrix3d = Matrix3d.Displacement(currentView.Target - Point3d.Origin) * matrix3d;
					matrix3d = Matrix3d.Rotation(-currentView.ViewTwist, currentView.ViewDirection, currentView.Target) * matrix3d;
					if (pCenter.DistanceTo(Point3d.Origin) != 0.0)
					{
						pMin = new Point3d(pCenter.X - currentView.Width / 2.0, pCenter.Y - currentView.Height / 2.0, 0.0);
						pMax = new Point3d(currentView.Width / 2.0 + pCenter.X, currentView.Height / 2.0 + pCenter.Y, 0.0);
					}
					Extents3d extents3d;
					using (Line line = new Line(pMin, pMax))
					{
						extents3d = new Extents3d(line.Bounds.Value.MinPoint, line.Bounds.Value.MaxPoint);
					}
					double num2 = currentView.Width / currentView.Height;
					matrix3d = matrix3d.Inverse();
					extents3d.TransformBy(matrix3d);
					double num3;
					double num4;
					Point2d centerPoint;
					if (pCenter.DistanceTo(Point3d.Origin) != 0.0)
					{
						num3 = currentView.Width;
						num4 = currentView.Height;
						if (dFactor == 0.0)
						{
							pCenter = pCenter.TransformBy(matrix3d);
						}
						centerPoint = new Point2d(pCenter.X, pCenter.Y);
					}
					else
					{
						num3 = extents3d.MaxPoint.X - extents3d.MinPoint.X;
						num4 = extents3d.MaxPoint.Y - extents3d.MinPoint.Y;
						centerPoint = new Point2d((extents3d.MaxPoint.X + extents3d.MinPoint.X) * 0.5, (extents3d.MaxPoint.Y + extents3d.MinPoint.Y) * 0.5);
					}
					if (num3 > num4 * num2)
					{
						num4 = num3 / num2;
					}
					if (dFactor != 0.0)
					{
						currentView.Height=(num4 * dFactor);
						currentView.Width=(num3 * dFactor);
					}
					currentView.CenterPoint=(centerPoint);
					mdiActiveDocument.Editor.SetCurrentView(currentView);
				}
				mdiActiveDocument.TransactionManager.QueueForGraphicsFlush();
				mdiActiveDocument.TransactionManager.FlushGraphics();
				mdiActiveDocument.Editor.UpdateScreen();
				transaction.Commit();
			}
		}
	}
}
