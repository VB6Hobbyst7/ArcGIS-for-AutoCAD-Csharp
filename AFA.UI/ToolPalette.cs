using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows.ToolPalette;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AFA.UI
{
    public static class ToolPalette
	{
		private static string catalogGuid = "50C29E33-6DBD-496f-B376-8FEB45E29EBF";

		private static string paletteGuid = "7B88FF54-6390-43F8-96FC-C691BAE51209";

		private static string groupFileName = "ArcGISCatalogGroup.ATC";

		private static string paletteFileName = "Feature Services Palette_7B88FF54-6390-43F8-96FC-C691BAE51209.atc";

		private static string fullPaletteFileName = "";

		private static string fullGroupFileName = "";

		private static string AppFolderLocation = "";

		private static string GroupFolder = "";

		private static string PaletteFolder = "";

		private static string ImageFolder = "";

		private static bool bFoldersInitialized = false;

		private static void IncludeFolderLocation(string folderLocation)
		{
			try
			{
				Application.GetSystemVariable("_TOOLPALETTEPATH");
				string text = (string)Application.GetSystemVariable("_TOOLPALETTEPATH");
				string[] array = text.Split(new char[]
				{
					';'
				});
				bool flag = false;
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string path = array2[i];
					if (string.Compare(Path.GetFullPath(path).TrimEnd(new char[]
					{
						'\\'
					}), Path.GetFullPath(folderLocation).TrimEnd(new char[]
					{
						'\\'
					}), StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					text += string.Format(";{0}", folderLocation);
					Application.SetSystemVariable("_TOOLPALETTEPATH", text);
					Application.GetSystemVariable("TPSTATE");
				}
			}
			catch
			{
			}
		}

		private static string InitializePaletteFolders()
		{
			string result;
			try
			{
				if (!ToolPalette.bFoldersInitialized)
				{
					string text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					text = Path.Combine(text, "ESRI");
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					text = Path.Combine(text, "ArcGIS for AutoCAD");
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					text = Path.Combine(text, "1.0");
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					ToolPalette.AppFolderLocation = text;
					ToolPalette.GroupFolder = Path.Combine(text, "Tool Palettes");
					if (!Directory.Exists(ToolPalette.GroupFolder))
					{
						Directory.CreateDirectory(ToolPalette.GroupFolder);
					}
					ToolPalette.PaletteFolder = Path.Combine(ToolPalette.GroupFolder, "Palettes");
					if (!Directory.Exists(ToolPalette.PaletteFolder))
					{
						Directory.CreateDirectory(ToolPalette.PaletteFolder);
					}
					ToolPalette.ImageFolder = Path.Combine(ToolPalette.PaletteFolder, "Images");
					if (!Directory.Exists(ToolPalette.ImageFolder))
					{
						Directory.CreateDirectory(ToolPalette.ImageFolder);
					}
					ToolPalette.fullGroupFileName = Path.Combine(ToolPalette.GroupFolder, ToolPalette.groupFileName);
					ToolPalette.fullPaletteFileName = Path.Combine(ToolPalette.PaletteFolder, ToolPalette.paletteFileName);
					ToolPalette.bFoldersInitialized = true;
				}
				if (!File.Exists(ToolPalette.fullPaletteFileName))
				{
					using (FileStream fileStream = new FileStream(ToolPalette.fullPaletteFileName, FileMode.CreateNew))
					{
						using (StreamWriter streamWriter = new StreamWriter(fileStream))
						{
							streamWriter.Write(tpResources.xmlEmptyPalette);
							streamWriter.Close();
						}
					}
				}
				if (!File.Exists(ToolPalette.fullGroupFileName))
				{
					using (FileStream fileStream2 = new FileStream(ToolPalette.fullGroupFileName, FileMode.CreateNew))
					{
						using (StreamWriter streamWriter2 = new StreamWriter(fileStream2))
						{
							streamWriter2.Write(tpResources.xmlGroup);
							streamWriter2.Close();
						}
					}
				}
				result = ToolPalette.AppFolderLocation;
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private static bool GroupExists(Document doc)
		{
			bool result;
			try
			{
				Editor arg_06_0 = doc.Editor;
				ToolPaletteManager manager = ToolPaletteManager.Manager;
				Guid guid = new Guid(ToolPalette.catalogGuid);
				CatalogItem catalogItem = manager.StockToolCatalogs.Find(guid);
				if (catalogItem != null)
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

		private static void CreateGroup()
		{
			try
			{
				ToolPalette.InitializePaletteFolders();
				if (File.Exists(ToolPalette.fullGroupFileName))
				{
					File.Delete(ToolPalette.fullGroupFileName);
				}
				using (FileStream fileStream = new FileStream(ToolPalette.fullGroupFileName, FileMode.Create))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream))
					{
						streamWriter.Write(tpResources.xmlGroup);
						streamWriter.Close();
					}
				}
			}
			catch
			{
			}
		}

		private static void CreateEmptyPalette()
		{
			try
			{
				ToolPalette.InitializePaletteFolders();
				if (File.Exists(ToolPalette.fullPaletteFileName))
				{
					File.Delete(ToolPalette.fullPaletteFileName);
				}
				using (FileStream fileStream = new FileStream(ToolPalette.fullPaletteFileName, FileMode.Create))
				{
					using (StreamWriter streamWriter = new StreamWriter(fileStream))
					{
						streamWriter.Write(tpResources.xmlEmptyPalette);
						streamWriter.Close();
					}
				}
			}
			catch
			{
			}
		}

		private static void WriteTool(MSCFeatureClass fs, string layerName, StreamWriter writer, Document doc, Transaction t)
		{
			try
			{
				Database database = doc.Database;
				if (fs.GeometryType == MSCFeatureClass.fcTypeCode.fcTypePoint)
				{
					if (DocUtil.IsNewDrawing(doc))
					{
						doc.Editor.WriteMessage(AfaStrings.UnableToAddPointTool);
					}
					else
					{
						ObjectId blockDefinition = DocUtil.GetBlockDefinition(doc, layerName);
						if (blockDefinition != ObjectId.Null)
						{
							string value = ToolPalette.CreateBlockImage(t, doc, ToolPalette.ImageFolder, blockDefinition);
							writer.Write(tpResources.xmlPointToolPart1);
							writer.Write(fs.Name);
							writer.Write(tpResources.xmlPointToolPart2);
							writer.Write(value);
							writer.Write(tpResources.xmlPointToolPart3);
							writer.Write(value);
							writer.Write(tpResources.xmlPointToolPart4a);
							writer.Write(AfaStrings.CreatePointFeatures);
							writer.Write(tpResources.xmlPointToolPart4b);
							writer.Write(layerName);
							writer.Write(tpResources.xmlPointToolPart5);
							writer.Write(layerName);
							writer.Write(tpResources.xmlPointToolPart6);
							writer.Write(layerName);
							writer.Write(tpResources.xmlPointToolPart7);
							writer.Write(database.Filename);
							writer.Write(tpResources.xmlPointToolPart8);
						}
						else
						{
							writer.Write(tpResources.xmlPtToolPart1);
							writer.Write(fs.Name);
							writer.Write(tpResources.xmlPtToolPart2);
							writer.Write(layerName);
							writer.Write(tpResources.xmlPtToolPart3);
							writer.Write(layerName);
							writer.Write(tpResources.xmlPtToolPart4);
						}
					}
				}
				else if (fs.GeometryType == MSCFeatureClass.fcTypeCode.fcTypePolyline)
				{
					string value2 = string.Concat(new string[]
					{
						tpResources.xmlLineToolPart1a,
						Guid.NewGuid().ToString("B"),
						tpResources.xmlLineToolPart1b,
						fs.Name,
						tpResources.xmlLineToolPart2,
						AfaStrings.CreateLineFeatures,
						tpResources.xmlLineToolPart3,
						layerName,
						tpResources.xmlLineToolPart4,
						layerName,
						tpResources.xmlLineToolPart5
					});
					writer.Write(value2);
				}
				else if (fs.GeometryType == MSCFeatureClass.fcTypeCode.fcTypePolygon)
				{
					string value3 = string.Concat(new string[]
					{
						tpResources.xmlAreaToolPart1a,
						Guid.NewGuid().ToString("B"),
						tpResources.xmlAreaToolPart1b,
						fs.Name,
						tpResources.xmlAreaToolPart2,
						AfaStrings.CreateAreaFeatures,
						tpResources.xmlAreaToolPart3,
						layerName,
						tpResources.xmlAreaToolPart3b,
						layerName,
						tpResources.xmlAreaToolPart4
					});
					writer.Write(value3);
				}
			}
			catch
			{
			}
		}

		private static void CreatePaletteFile(Document doc, MSCDataset dataset)
		{
			try
			{
				ToolPalette.InitializePaletteFolders();
				Database database = doc.Database;
				if (!dataset.HasFeatureServicesOpenedForEditing())
				{
					ToolPalette.CreateEmptyPalette();
				}
				else
				{
					bool flag = false;
					foreach (MSCFeatureService current in dataset.FeatureServices.Values)
					{
						if (!current.QueryOnly)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						ToolPalette.CreateEmptyPalette();
					}
					else
					{
						if (File.Exists(ToolPalette.fullPaletteFileName))
						{
							File.Delete(ToolPalette.fullPaletteFileName);
						}
						using (FileStream fileStream = new FileStream(ToolPalette.fullPaletteFileName, FileMode.Create))
						{
							using (StreamWriter streamWriter = new StreamWriter(fileStream))
							{
								streamWriter.Write(tpResources.xmlPaletteBeginning);
								using (doc.LockDocument())
								{
									using (Transaction transaction = database.TransactionManager.StartTransaction())
									{
										foreach (MSCFeatureService current2 in dataset.FeatureServices.Values)
										{
											if (!current2.QueryOnly)
											{
												if (current2.SubTypes.Count == 0)
												{
													ToolPalette.WriteTool(current2, current2.LayerName, streamWriter, doc, transaction);
												}
												foreach (MSCFeatureClassSubType current3 in current2.SubTypes)
												{
													ToolPalette.WriteTool(current3, current3.CadLayerName, streamWriter, doc, transaction);
												}
											}
										}
										transaction.Commit();
									}
									streamWriter.Write(tpResources.xmlPaletteClose);
									streamWriter.Close();
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

		private static bool PaletteExists(Document doc)
		{
			bool result;
			try
			{
				Editor arg_06_0 = doc.Editor;
				ToolPaletteManager manager = ToolPaletteManager.Manager;
				Guid guid = new Guid(ToolPalette.paletteGuid);
				CatalogItem catalogItem = manager.StockToolCatalogs.Find(guid);
				if (catalogItem != null)
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

		//private static void CreateBlockIcons2(Transaction tr, Document doc, ObjectId[] blkIds)
		//{
		//	try
		//	{
		//		if (!(Application.DocumentManager.MdiActiveDocument == null))
		//		{
		//			for (int i = 0; i < blkIds.Length; i++)
		//			{
		//				ObjectId objectId = blkIds[i];
		//				BlockTableRecord blockTableRecord = (BlockTableRecord)tr.GetObject(objectId, (OpenMode)1);
		//				if (!blockTableRecord.IsAnonymous && !blockTableRecord.IsLayout&& blockTableRecord.PreviewIcon == null)
		//				{
		//					Manager graphicsManager = Application.DocumentManager.MdiActiveDocument.GraphicsManager;
		//					Device device = graphicsManager.CreateAutoCADDevice(doc.Window.Handle);
		//					device.OnSize(new Size(300, 300));
		//					graphicsManager.CreateAutoCADModel();
		//					View view = graphicsManager.CreateAutoCADView(new Line(new Point3d(0.0, 0.0, 0.0), new Point3d(300.0, 300.0, 0.0)));
		//					device.Add(view);
		//					view.ZoomExtents(new Point3d(0.0, 0.0, 0.0), new Point3d(300.0, 300.0, 0.0));
		//					Rectangle rectangle = new Rectangle(0, 0, 300, 300);
		//					Bitmap snapshot = view.GetSnapshot(rectangle);
		//					blockTableRecord.PreviewIcon=(snapshot);
		//				}
		//			}
		//		}
		//	}
		//	catch
		//	{
		//	}
		//}

		private static void CreateBlockIcons(Transaction tr, Document doc, ObjectId[] blkIds)
		{
			try
			{
				string text = "";
				for (int i = 0; i < blkIds.Length; i++)
				{
					ObjectId objectId = blkIds[i];
					BlockTableRecord blockTableRecord = (BlockTableRecord)tr.GetObject(objectId, 0);
					if (!blockTableRecord.IsAnonymous && !blockTableRecord.IsLayout && blockTableRecord.PreviewIcon == null)
					{
						if (string.IsNullOrEmpty(text))
						{
							text = blockTableRecord.Name;
						}
						else
						{
							text = text + "," + blockTableRecord.Name;
						}
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						using (doc.LockDocument())
						{
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		private static string CreateBlockImage(Transaction tr, Document doc, string imagePath, ObjectId blkId)
		{
			string text = "";
			Database arg_0C_0 = doc.Database;
			try
			{
				BlockTableRecord blockTableRecord = (BlockTableRecord)tr.GetObject(blkId, 0);
				if (blockTableRecord.IsAnonymous || blockTableRecord.IsLayout)
				{
					string result = text;
					return result;
				}
				if (blockTableRecord.PreviewIcon != null)
				{
					try
					{
						if (!Directory.Exists(imagePath))
						{
							Directory.CreateDirectory(imagePath);
						}
						text = Path.Combine(imagePath, blockTableRecord.Name + ".png");
						if (File.Exists(text))
						{
							File.Delete(text);
						}
						blockTableRecord.PreviewIcon.Save(text, ImageFormat.Png);
						string result = text;
						return result;
					}
					catch
					{
						string result = "";
						return result;
					}
				}
			}
			catch
			{
			}
			return text;
		}

		public static void GenerateBlockIcons(Document doc, MSCDataset dataset)
		{
			try
			{
				Database database = doc.Database;
				List<ObjectId> list = new List<ObjectId>();
				using (doc.LockDocument())
				{
					using (Transaction transaction = database.TransactionManager.StartTransaction())
					{
						foreach (MSCFeatureService current in dataset.FeatureServices.Values)
						{
							if (!current.QueryOnly && current.GeometryType == MSCFeatureClass.fcTypeCode.fcTypePoint)
							{
								ObjectId blockDefinition = DocUtil.GetBlockDefinition(doc, current.LayerName);
								if (!(blockDefinition == ObjectId.Null))
								{
									list.Add(blockDefinition);
									foreach (MSCFeatureClassSubType current2 in current.SubTypes)
									{
										ObjectId blockDefinition2 = DocUtil.GetBlockDefinition(doc, current2.CadLayerName);
										if (!(blockDefinition2 == ObjectId.Null))
										{
											list.Add(blockDefinition2);
										}
									}
								}
							}
						}
						if (list.Count > 0)
						{
							ToolPalette.CreateBlockIcons(transaction, doc, list.ToArray());
						}
						transaction.Commit();
					}
				}
			}
			catch
			{
			}
		}

		public static void GenerateBlockImages(Document doc, MSCDataset dataset)
		{
			try
			{
				ToolPalette.InitializePaletteFolders();
				Database database = doc.Database;
				using (doc.LockDocument())
				{
					using (Transaction transaction = database.TransactionManager.StartTransaction())
					{
						foreach (MSCFeatureService current in dataset.FeatureServices.Values)
						{
							if (!current.QueryOnly && current.GeometryType == MSCFeatureClass.fcTypeCode.fcTypePoint)
							{
								ObjectId blockDefinition = DocUtil.GetBlockDefinition(doc, current.LayerName);
								if (!(blockDefinition == ObjectId.Null))
								{
									ToolPalette.CreateBlockImage(transaction, doc, ToolPalette.ImageFolder, blockDefinition);
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

		public static bool PaletteVisible()
		{
			bool result;
			try
			{
				object systemVariable = Application.GetSystemVariable("TPSTATE");
				if ((short)systemVariable == 0)
				{
					result = false;
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

		public static void CreatePalette(Document doc, MSCDataset dataset)
		{
			ToolPaletteManager manager = ToolPaletteManager.Manager;
			try
			{
				try
				{
					if (manager.Catalogs.Count > 0)
					{
						manager.SaveCatalogs();
					}
				}
				catch
				{
				}
				ToolPalette.InitializePaletteFolders();
				ToolPalette.IncludeFolderLocation(ToolPalette.GroupFolder);
				ToolPalette.CreatePaletteFile(doc, dataset);
				if (!ToolPalette.GroupExists(doc))
				{
					ToolPalette.CreateGroup();
				}
				if (manager.Catalogs.Count > 0)
				{
					if (dataset.FeatureServices.Count != 0)
					{
						manager.LoadCatalogs();
					}
					else if (ToolPalette.PaletteVisible())
					{
						manager.LoadCatalogs();
					}
				}
			}
			catch
			{
			}
		}

		public static void CreatePaletteSchemesTest(Document doc, MSCDataset dataset)
		{
			ToolPaletteManager manager = ToolPaletteManager.Manager;
			try
			{
				try
				{
					if (manager.Catalogs.Count > 0)
					{
						manager.SaveCatalogs();
					}
				}
				catch
				{
				}
				ToolPalette.InitializePaletteFolders();
				ToolPalette.IncludeFolderLocation(ToolPalette.GroupFolder);
				ToolPalette.CreatePaletteFile(doc, dataset);
				if (!ToolPalette.GroupExists(doc))
				{
					ToolPalette.CreateGroup();
				}
				Scheme scheme = new Scheme("ArcGIS");
				scheme.LoadCatalogs();
				scheme.SaveCatalogs();
				manager.Schemes.Add("ArcGIS");
			}
			catch
			{
			}
		}

		public static void UpdatePalette(Document doc, MSCDataset dataset, bool generateIcons)
		{
			try
			{
				if (generateIcons)
				{
					ToolPalette.GenerateBlockImages(doc, dataset);
				}
				ToolPalette.CreatePalette(doc, dataset);
			}
			catch
			{
			}
		}

		public static void UpdatePalette(Document doc, MSCDataset dataset)
		{
			try
			{
				ToolPalette.CreatePalette(doc, dataset);
			}
			catch
			{
			}
		}

		public static void ShowPalette(Document doc, MSCDataset dataset)
		{
			try
			{
				ToolPalette.UpdatePalette(doc, dataset);
				string cmdString = "(command \"tpnavigate\" \"ArcGIS Feature Services\")";
				CmdLine.ExecuteQuietCommand(cmdString);
			}
			catch
			{
			}
		}
	}
}
