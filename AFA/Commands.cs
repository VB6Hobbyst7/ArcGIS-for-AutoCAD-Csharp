using AFA.Resources;
using AFA.UI;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using ESRI_TableViewer;
using SelectByAttributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
using Cursors = System.Windows.Input.Cursors;
using Exception = System.Exception;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace AFA
{
    public static class Commands
    {
        [CommandMethod("ESRI_AboutArcGIS", CommandFlags.NoUndoMarker)]
        public static void AboutArcGIS()
        {
            Application.ShowModelessWindow(new About());
        }




        [CommandMethod("ESRI_AddESRIMap", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void AddESRIMap()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                if (SelectConnection.WindowOpen)
                {
                    Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(AfaStrings.ConnectionWindowOpen);
                }
                else
                {
                    if (string.IsNullOrEmpty(MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument)))
                    {
                        Application.ShowModalWindow(new NoSpatialReferenceMsg());
                        string str = MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument);
                    }
                    AGS_ESRIMaps.AddESRIMapWindow();
                }
            }
        }

        [CommandMethod("ESRI_AddService", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void AddService()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                if (SelectConnection.WindowOpen)
                {
                    Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(AfaStrings.ConnectionWindowOpen);
                }
                else
                {
                    if (string.IsNullOrEmpty(MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument)))
                    {
                        Application.ShowModalWindow(new NoSpatialReferenceMsg());
                    }
                    SelectConnection formToShow = new SelectConnection
                    {
                        lbConnections = { ItemsSource = App.Connections }
                    };
                    Application.ShowModalWindow(formToShow);
                }
            }
        }


        [CommandMethod("esri_AddMapServiceByURL", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void AddURLMap()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                string str = MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument);
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                PromptStringOptions options = new PromptStringOptions("\nURL: ")
                {
                    AllowSpaces = false
                };
                PromptResult result = editor.GetString(options);
                if (!string.IsNullOrEmpty(result.StringResult))
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    AGSMapService service = AGSMapService.BuildMapServiceFromURL(null, result.StringResult);
                    if (service == null)
                    {
                        ErrorReport.ShowErrorMessage(AfaStrings.ErrorAddingService);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            MSCPrj.AssignWKT(AfaDocData.ActiveDocData.Document, service.GetWKT());
                        }
                        if (!service.AddService())
                        {
                            string errorAddingService = AfaStrings.ErrorAddingService;
                            if (!string.IsNullOrEmpty(service.ErrorMessage))
                            {
                                editor.WriteMessage(errorAddingService + "  " + service.ErrorMessage);
                            }
                        }
                    }
                    Mouse.OverrideCursor = null;
                }
            }
        }

        [CommandMethod("esri_AddImageServiceByURL", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void AddURLImage()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                string str = MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument);
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                PromptStringOptions options = new PromptStringOptions("\nURL: ")
                {
                    AllowSpaces = false
                };
                PromptResult result = editor.GetString(options);
                if (!string.IsNullOrEmpty(result.StringResult))
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    AGSImageService service = AGSImageService.BuildImageServiceFromURL(null, result.StringResult);
                    if (service == null)
                    {
                        ErrorReport.ShowErrorMessage(AfaStrings.ErrorAddingService);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            MSCPrj.AssignWKT(AfaDocData.ActiveDocData.Document, service.GetWKT());
                        }
                        if (!service.AddService(service.ExportOptions))
                        {
                            string errorAddingService = AfaStrings.ErrorAddingService;
                            if (!string.IsNullOrEmpty(service.ErrorMessage))
                            {
                                editor.WriteMessage(errorAddingService + "  " + service.ErrorMessage);
                            }
                        }
                        Mouse.OverrideCursor = null;
                    }
                    Mouse.OverrideCursor = null;
                }
            }
        }

        [CommandMethod("esri_AddFeatureServiceByURL", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void AddURLFeatureService()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                string str = MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument);
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                PromptStringOptions options = new PromptStringOptions("\nURL: ")
                {
                    AllowSpaces = false
                };
                PromptResult result = editor.GetString(options);
                if (!string.IsNullOrEmpty(result.StringResult))
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    AGSFeatureService service = AGSFeatureService.BuildFeatureServiceFromURL(null, result.StringResult);
                    if (service == null)
                    {
                        editor.WriteMessage(AfaStrings.ErrorAddingService);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(str))
                        {
                            MSCPrj.AssignWKT(AfaDocData.ActiveDocData.Document, service.GetWKT());
                        }
                        if (!service.AddService())
                        {
                            string errorAddingService = AfaStrings.ErrorAddingService;
                            if (!string.IsNullOrEmpty(service.ErrorMessage))
                            {
                                editor.WriteMessage(errorAddingService + "  " + service.ErrorMessage);
                            }
                        }
                    }
                    Mouse.OverrideCursor = null;
                }
            }
        }

        [CommandMethod("ESRI_AssignCS", CommandFlags.NoUndoMarker)]
        public static void AssignCS()
        {
            Editor editor = AfaDocData.ActiveDocData.Document.Editor;
            MSCPrj.AssignPRJDialog();
            if (!string.IsNullOrEmpty(AfaDocData.ActiveDocData.DocPRJ.WKT))
            {
                editor.WriteMessage(AfaDocData.ActiveDocData.DocPRJ.WKT + "\n");
            }
            else
            {
                editor.WriteMessage(AfaStrings.NoValidCoordinateSystemFound + "\n");
            }
        }
        [CommandMethod("ESRI_ClearSelection", CommandFlags.UsePickSet)]
        public static void CommandClearSelection()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Application.DocumentManager.MdiActiveDocument.Editor.SetImpliedSelection(new ObjectId[0]);
            }
        }

        [CommandMethod("ESRI_SelectFeatures", CommandFlags.NoUndoMarker)]
        public static void CommandSelectAll()
        {
            MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
            if (activeFeatureClassOrSubtype == null)
            {
                AfaDocData.ActiveDocData.Document.Editor.WriteMessage(AfaStrings.NoActiveFeatureClassFound);
            }
            else
            {
                activeFeatureClassOrSubtype.Select();
            }
        }
        [CommandMethod("ESRI_SelectFeatureObjects")]
        public static void CommandSelectObjects()
        {
            PromptSelectionResult selection;
            MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
            Editor ed = AfaDocData.ActiveDocData.Document.Editor;
            PromptSelectionOptions options = new PromptSelectionOptions
            {
                AllowDuplicates = false,
                MessageForAdding = AfaStrings.MSG_SELECT_OBJECTADDED,
                MessageForRemoval = AfaStrings.MSG_SELECT_OBJECTSKIPPED
            };
            if (activeFeatureClassOrSubtype != null)
            {
                selection = ed.GetSelection(options, activeFeatureClassOrSubtype.GetSelectionFilter());
            }
            else
            {
                selection = ed.GetSelection(options);
            }
            if (selection.Status == PromptStatus.OK)
            {
                CmdLine.DisplayCountMessage(ed, selection.Value.Count);
                ed.SetImpliedSelection(selection.Value);
            }
        }

        [CommandMethod("ESRI_ZoomExtents", CommandFlags.Transparent)]
        public static void CommandZoomExtents()
        {
            MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
            Document document = AfaDocData.ActiveDocData.Document;
            Database database = document.Database;
            Editor editor = document.Editor;
            document.Database.UpdateExt(true);
            Extent a = new Extent(database.Extmin, database.Extmax);
            if (AfaDocData.ActiveDocData.DocPRJ != null)
            {
                a.SpatialReference = AfaDocData.ActiveDocData.DocPRJ.WKT;
            }
            if (docDataset.MapServices.Count > 0)
            {
                foreach (MSCMapService service in docDataset.MapServices.Values)
                {
                    Extent boundaryExtent = service.BoundaryExtent;
                    if (a.SpatialReference != boundaryExtent.SpatialReference)
                    {
                        editor.WriteMessage(AfaStrings.CoordinateSystemsDontMatch);
                        continue;
                    }
                    a = Extent.Union(a, boundaryExtent);
                }
            }
            if (docDataset.ImageServices.Count > 0)
            {
                foreach (MSCImageService service2 in docDataset.ImageServices.Values)
                {
                    Extent boundaryExtent = service2.BoundaryExtent;
                    a = Extent.Union(a, boundaryExtent);
                }
            }
            Point3d minPoint = new Point3d(a.XMin.Value, a.YMin.Value, 0.0);
            Point3d maxPoint = new Point3d(a.XMax.Value, a.YMax.Value, 0.0);
            DocUtil.ZoomExtents(minPoint, maxPoint);
            docDataset.UpdateMaps();
            AfaDocData.ActiveDocData.Document.Editor.Regen();
        }

        [CommandMethod("ESRI_ZoomFeatures", CommandFlags.Redraw)]
        public static void CommandZoomFeatures()
        {
            Editor editor = AfaDocData.ActiveDocData.Document.Editor;
            MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
            if (activeFeatureClassOrSubtype == null)
            {
                editor.WriteMessage(AfaStrings.NoActiveFeatureClassFound);
            }
            else
            {
                int num = activeFeatureClassOrSubtype.ZoomFeatures();
                editor.WriteMessage("\n" + AfaStrings.MSG_ZOOMFEATURESFOUND + string.Format(":  {0}", num));
            }
        }

        [CommandMethod("ESRI_ZoomSelected", CommandFlags.Redraw)]
        public static void CommandZoomSelected()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
                PromptSelectionResult result = ed.SelectImplied();
                try
                {
                    if (result.Status == PromptStatus.Cancel)
                    {
                        ed.WriteMessage(AfaStrings.CommandCancelled);
                    }
                    else if (result.Status == PromptStatus.OK)
                    {
                        ObjectId[] objectIds = result.Value.GetObjectIds();
                        if ((objectIds != null) && (objectIds.Length > 0))
                        {
                            DocUtil.ZoomToEntity(objectIds);
                        }
                        CmdLine.DisplayCountMessage(ed, 0);
                    }
                }
                catch
                {
                    ed.WriteMessage(AfaStrings.Error + " ZoomSelected");
                }
            }
        }

        [CommandMethod("ESRI_DeleteFeatureClass", CommandFlags.NoUndoMarker | CommandFlags.UsePickSet)]
        public static void DeleteActiveFeatureClass()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                try
                {
                    MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
                    if (activeFeatureClassOrSubtype == null)
                    {
                        editor.WriteMessage("\n" + AfaStrings.NoCurrentFeatureClassFound);
                    }
                    else
                    {
                        MSCFeatureClass parentFC = activeFeatureClassOrSubtype.ParentFC;
                        MSCDataset parentDataset = activeFeatureClassOrSubtype.ParentDataset;
                        if ((parentFC != null) && (activeFeatureClassOrSubtype is MSCFeatureClassSubType))
                        {
                            parentFC.SubTypes.Remove((MSCFeatureClassSubType)activeFeatureClassOrSubtype);
                            AfaDocData.ActiveDocData.ClearActiveSubtype();
                            parentFC.Write(AfaDocData.ActiveDocData.Document);
                        }
                        else if (parentDataset != null)
                        {
                            parentDataset.RemoveFeatureClass(activeFeatureClassOrSubtype);
                            MSCDataset.SetDefaultActiveFeatureClass();
                        }
                    }
                }
                catch
                {
                }
            }
        }

        [CommandMethod("ESRI_DeleteAllFeatureServices", CommandFlags.NoUndoMarker | CommandFlags.UsePickSet)]
        public static void DeleteAllFeatureServices()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                try
                {
                    MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
                    if (docDataset.FeatureServices.Count == 0)
                    {
                        editor.WriteMessage(AfaStrings.NoFeatureServicesFound);
                    }
                    else
                    {
                        List<string> list = new List<string>();
                        foreach (MSCFeatureService service in docDataset.FeatureServices.Values)
                        {
                            string name = service.Name;
                            if (service.DeleteService())
                            {
                                list.Add(name);
                            }
                        }
                        foreach (string str2 in list)
                        {
                            docDataset.FeatureServices.Remove(str2);
                        }
                        MSCDataset.SetDefaultActiveFeatureClass();
                        ArcGISRibbon.EnableFeatureServiceButtons(false);
                    }
                }
                catch
                {
                }
            }
        }

        [CommandMethod("esri_RemoveCS", CommandFlags.NoUndoMarker)]
        public static void DeleteCS()
        {
            Document doc = AfaDocData.ActiveDocData.Document;
            Editor editor = doc.Editor;
            if (((AfaDocData.ActiveDocData.DocDataset.MapServices.Count + AfaDocData.ActiveDocData.DocDataset.ImageServices.Count) + AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count) > 0)
            {
                editor.WriteMessage(AfaStrings.DeleteCoordSysErrorServicesPresent);
            }
            else if (string.IsNullOrEmpty(MSCPrj.ReadWKT(doc)))
            {
                editor.WriteMessage(AfaStrings.NoValidCoordinateSystemFound);
            }
            else
            {
                MSCPrj.RemoveWKT(doc);
                editor.WriteMessage(AfaStrings.CoordinateSystemRemoved);
            }
        }

        [CommandMethod("ESRI_DeleteFeatureService", CommandFlags.NoUndoMarker | CommandFlags.UsePickSet)]
        public static void DeleteFeatureService()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                try
                {
                    MSCFeatureService activeFeatureService = AfaDocData.ActiveDocData.GetActiveFeatureService();
                    if (activeFeatureService == null)
                    {
                        editor.WriteMessage("\n" + AfaStrings.NoCurrentFeatureServiceFound);
                    }
                    else
                    {
                        string name = activeFeatureService.Name;
                        bool queryOnly = activeFeatureService.QueryOnly;
                        if (activeFeatureService.DeleteService())
                        {
                            AfaDocData.ActiveDocData.DocDataset.FeatureServices.Remove(name);
                            MSCDataset.SetDefaultActiveFeatureClass();
                            activeFeatureService = null;
                            if (AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count == 0)
                            {
                                ArcGISRibbon.EnableFeatureServiceButtons(false);
                            }
                            if (!queryOnly)
                            {
                                ToolPalette.UpdatePalette(AfaDocData.ActiveDocData.DocDataset.ParentDocument, AfaDocData.ActiveDocData.DocDataset, false);
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }

        [CommandMethod("esri_DiscardAllEdits", CommandFlags.NoUndoMarker | CommandFlags.UsePickSet)]
        public static void DiscardAllFeatureServiceEdits()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                try
                {
                    if (AfaDocData.ActiveDocData.DocDataset.FeatureServices == null)
                    {
                        editor.WriteMessage("\n" + AfaStrings.NoFeatureServicesFound);
                    }
                    else if (AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count == 0)
                    {
                        editor.WriteMessage("\n" + AfaStrings.NoFeatureServicesFound);
                    }
                    else
                    {
                        string str = "";
                        foreach (MSCFeatureService service in AfaDocData.ActiveDocData.DocDataset.FeatureServices.Values)
                        {
                            string resultString = "";
                            service.Refresh(ref resultString);
                            if (!string.IsNullOrEmpty(resultString))
                            {
                                str = str + resultString + "\r\n";
                            }
                            if ((!service.QueryOnly && !service.HasCommitErrors) && service.ParentService.IsValid)
                            {
                                service.ParentDataset.RefreshAssociatedMaps(service.ParentService.ServiceURL);
                            }
                        }
                        if (!string.IsNullOrEmpty(str))
                        {
                            MessageBoxEx.Show(new WindowWrapper(Application.MainWindow.Handle), str, AfaStrings.RefreshResults, 0x2710);
                        }
                    }
                }
                catch
                {
                    editor.WriteMessage("\n" + AfaStrings.Error);
                }
            }
        }


        [CommandMethod("esri_DiscardEdits", CommandFlags.NoUndoMarker | CommandFlags.UsePickSet)]
        public static void DiscardFeatureServiceEdits()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                try
                {
                    MSCFeatureService activeFeatureService = AfaDocData.ActiveDocData.GetActiveFeatureService();
                    if (activeFeatureService == null)
                    {
                        editor.WriteMessage("\n" + AfaStrings.NoCurrentFeatureServiceFound);
                    }
                    else
                    {
                        string resultString = "";
                        activeFeatureService.Refresh(ref resultString);
                        if (!string.IsNullOrEmpty(resultString))
                        {
                            MessageBoxEx.Show(new WindowWrapper(Application.MainWindow.Handle), resultString, AfaStrings.RefreshResults, 0x2710);
                        }
                    }
                }
                catch
                {
                }
            }
        }
        [CommandMethod("ESRI_DisconnectFeatureService", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void DisconnectFeatureService()
        {
            MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
            Editor editor = docDataset.ParentDocument.Editor;
            MSCFeatureService activeFeatureService = AfaDocData.ActiveDocData.GetActiveFeatureService();
            if (activeFeatureService == null)
            {
                editor.WriteMessage("\n" + AfaStrings.NoCurrentFeatureServiceFound);
            }
            else
            {
                string name = activeFeatureService.Name;
                bool queryOnly = activeFeatureService.QueryOnly;
                MSCFeatureClass fc = activeFeatureService.Disconnect();
                if (fc != null)
                {
                    docDataset.FeatureServices.Remove(name);
                    ArcGISRibbon.SetActiveFeatureClass(fc);
                }
                if (!queryOnly)
                {
                    ToolPalette.UpdatePalette(docDataset.ParentDocument, docDataset, false);
                }
            }
        }

        [CommandMethod("esri_FeatureClassProperties", CommandFlags.Redraw | CommandFlags.UsePickSet)]
        public static void EditFeatureServiceProperties()
        {
            Editor editor = AfaDocData.ActiveDocData.DocDataset.ParentDocument.Editor;
            MSCFeatureService activeFeatureService = AfaDocData.ActiveDocData.GetActiveFeatureService();
            if (activeFeatureService != null)
            {
                if (!activeFeatureService.ReconnectFeatureService())
                {
                    ErrorReport.ShowErrorMessage(AfaStrings.UnableToConnectFeatureService + activeFeatureService.Name);
                }
                else
                {
                    Application.ShowModalWindow(new ServicePropertyEditor(activeFeatureService));
                }
            }
            else
            {
                MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
                if (activeFeatureClassOrSubtype == null)
                {
                    editor.WriteMessage("\n" + AfaStrings.NoCurrentFeatureClassFound);
                }
                else
                {
                    Application.ShowModalWindow(new FeatureClassProperties(activeFeatureClassOrSubtype));
                }
            }
        }

        [CommandMethod("esri_MapServiceProperties", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void EditServiceProperties()
        {
            Editor editor = AfaDocData.ActiveDocData.DocDataset.ParentDocument.Editor;
            MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
            if (activeRasterService == null)
            {
                editor.WriteMessage(AfaStrings.NoCurrentMapServiceFound);
            }
            else if (activeRasterService.ParentService == null)
            {
                editor.WriteMessage(AfaStrings.ParentServiceNotConnected);
            }
            else
            {
                Application.ShowModalWindow(new ServicePropertyEditor(activeRasterService));
            }
        }

        [CommandMethod("ESRI_ExportMap", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void ExportMap()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
                if (activeRasterService == null)
                {
                    editor.WriteMessage(AfaStrings.NoCurrentMapServiceFound);
                }
                else if (activeRasterService.ParentService == null)
                {
                    editor.WriteMessage(AfaStrings.ParentServiceNotConnected);
                }
                else
                {
                    AGSRasterService parentService = activeRasterService.ParentService;
                    if ((parentService != null) && !parentService.SupportsDisconnect())
                    {
                        editor.WriteMessage(AfaStrings.ErrorMapServiceCannotDisconnect);
                    }
                    else
                    {
                        string path = activeRasterService.SuggestExportName();
                        SaveFileDialog dialog = new SaveFileDialog
                        {
                            Title = AfaStrings.SaveLocalImageFile
                        };
                        string extension = Path.GetExtension(path);
                        dialog.DefaultExt = extension;
                        dialog.FileName = path;
                        dialog.Filter = string.Format("{0} {1}|*.{0}", extension, AfaStrings.Files);
                        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                        dialog.CheckFileExists = false;
                        dialog.OverwritePrompt = true;
                        dialog.CheckPathExists = true;
                        bool? nullable = dialog.ShowDialog();
                        while (true)
                        {
                            bool? nullable2 = nullable;
                            if (!(nullable2.Value && (nullable2 != null)) || !File.Exists(dialog.FileName))
                            {
                                bool? nullable3 = nullable;
                                if (nullable3.Value && (nullable3 != null))
                                {
                                    if (!activeRasterService.CreateExportImage(dialog.FileName))
                                    {
                                        editor.WriteMessage(AfaStrings.ExportFailed);
                                        return;
                                    }
                                    activeRasterService = null;
                                    editor.WriteMessage(AfaStrings.ExportCreated);
                                }
                                return;
                            }
                            try
                            {
                                File.Delete(dialog.FileName);
                            }
                            catch (Exception exception1)
                            {
                                ErrorReport.ShowErrorMessage(exception1.Message);
                                nullable = dialog.ShowDialog();
                            }
                        }
                    }
                }
            }
        }

        [CommandMethod("ESRI_GeneratePalette")]
        public static void GeneratePalette()
        {
            MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
            ToolPalette.CreatePalette(AfaDocData.ActiveDocData.Document, docDataset);
        }

        [CommandMethod("ESRI_Locate", CommandFlags.Transparent)]
        public static void GeoLocate()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                if (string.IsNullOrEmpty(MSCPrj.ReadWKT(Application.DocumentManager.MdiActiveDocument)))
                {
                    Application.ShowModalWindow(new NoSpatialReferenceMsg());
                }
                Editor editor = AfaDocData.ActiveDocData.Document.Editor;
                if (App.Locators.Count == 0)
                {
                    AGSLocator.LoadLocators();
                }
                if (App.Locators.Count == 0)
                {
                    editor.WriteMessage("\n" + AfaStrings.NoValidLocatorsFound);
                }
                else
                {
                    Application.ShowModalWindow(new Locate());
                    PaletteUtils.ActivateEditor();
                }
            }
        }

        [CommandMethod("esri_Attributes", CommandFlags.Redraw)]
        public static void Identify()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                PromptSelectionOptions options = new PromptSelectionOptions
                {
                    AllowDuplicates = false,
                    MessageForAdding = AfaStrings.MSG_SELECT_OBJECTADDED,
                    MessageForRemoval = AfaStrings.MSG_SELECT_OBJECTSKIPPED
                };
                PromptSelectionResult selection = editor.GetSelection(options);
                if (selection.Status == PromptStatus.Cancel)
                {
                    editor.WriteMessage(AfaStrings.CommandCancelled);
                }
                else if (selection.Status != PromptStatus.OK)
                {
                    editor.WriteMessage(AfaStrings.NoFeaturesFound);
                }
                else
                {
                    SelectionSet set = selection.Value;
                    if (set.Count == 0)
                    {
                        editor.WriteMessage(AfaStrings.NoFeaturesFound);
                    }
                    else
                    {
                        Identify formToShow = new Identify(new ObjectIdCollection(set.GetObjectIds()));
                        Application.ShowModalWindow((Window)null, formToShow, false);
                    }
                }
            }
        }

        [CommandMethod("ESRI_IdentifyMap")]
        public static void IdentifyMap()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Document mdiActiveDocument = Application.DocumentManager.MdiActiveDocument;
                Editor editor = mdiActiveDocument.Editor;
                MSCMapService currentMapService = AfaDocData.ActiveDocData.CurrentMapService;
                if (currentMapService == null)
                {
                    editor.WriteMessage(AfaStrings.NoCurrentMapServiceFound);
                }
                else if (!((AGSMapService)currentMapService.ParentService).SupportsQuery)
                {
                    editor.WriteMessage(AfaStrings.QueryNotSupported);
                }
                else
                {
                    PromptPointResult point = editor.GetPoint(new PromptPointOptions(AfaStrings.MSG_SELECT_FIRSTCORNER));
                    if (point.Status != PromptStatus.OK)
                    {
                        editor.WriteMessage(AfaStrings.MSG_SELECTCORNER_ERROR);
                    }
                    else
                    {
                        PromptPointResult corner = editor.GetCorner(new PromptCornerOptions(AfaStrings.MSG_SELECT_SECONDCORNER, point.Value));
                        if (corner.Status != PromptStatus.OK)
                        {
                            editor.WriteMessage(AfaStrings.MSG_SELECTCORNER_ERROR);
                        }
                        else
                        {
                            Extent ext = new Extent(point.Value, corner.Value)
                            {
                                SpatialReference = MSCPrj.ReadWKT(mdiActiveDocument)
                            };
                            MapTableViewer formToShow = new MapTableViewer(currentMapService, ext);
                            if (formToShow.LayerCount() > 0)
                            {
                                Application.ShowModalWindow((Window)null, formToShow, true);
                            }
                            else
                            {
                                editor.WriteMessage(AfaStrings.NoIdentifyResultsFound);
                            }
                        }
                    }
                }
            }
        }


        [CommandMethod("ESRI_ImportSchema", CommandFlags.NoUndoMarker)]
        public static void ImportSchema()
        {
            MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
            Editor editor = docDataset.ParentDocument.Editor;
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = AfaStrings.OpenImportSchemaSource,
                Filter = "DWG|*.dwg|DWT|*.dwt",
                CheckFileExists = true,
                CheckPathExists = true
            };
            bool? nullable2 = dialog.ShowDialog();
            if (nullable2.Value && (nullable2 != null))
            {
                Path.GetExtension(dialog.FileName).ToLower();
                try
                {
                    Database sourceDB = new Database(false, true);
                    sourceDB.ReadDwgFile(dialog.FileName, FileOpenMode.OpenForReadAndWriteNoShare, true, "");
                    int num = AfaDocData.ActiveDocData.DocDataset.Import(sourceDB, docDataset.ParentDocument.Database);
                    editor.WriteMessage(string.Format(AfaStrings.ImportCount, num));
                }
                catch
                {
                    ErrorReport.ShowErrorMessage(AfaStrings.ErrorOpeningSourceDrawing + "(" + dialog.FileName + ")");
                }
            }
        }

        [CommandMethod("ESRI_ListCS", CommandFlags.NoUndoMarker)]
        public static void ListCS()
        {
            Document doc = AfaDocData.ActiveDocData.Document;
            Editor editor = doc.Editor;
            string str = MSCPrj.ReadWKT(doc);
            if (!string.IsNullOrEmpty(str))
            {
                editor.WriteMessage(str + "\n");
            }
            else
            {
                editor.WriteMessage(AfaStrings.NoValidCoordinateSystemFound);
            }
        }

        [CommandMethod("ESRI_NavigatePalette")]
        public static void NavigatePalette()
        {
            CmdLine.ExecuteQuietCommand("(command \"tpnavigate\" \"ArcGIS Feature Services\")");
        }


        [CommandMethod("ESRI_NewFeatureClass", CommandFlags.Redraw | CommandFlags.UsePickSet)]
        public static void NewFeatureClass()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                try
                {
                    Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                    Application.ShowModalWindow(new NewFeatureClass());
                }
                catch (Exception exception1)
                {
                    System.Windows.MessageBox.Show(exception1.Message);
                }
            }
        }

        [CommandMethod("ESRI_RefreshAllMaps", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void RefreshAllMaps()
        {
            MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
            Document document = AfaDocData.ActiveDocData.Document;
            Database database = document.Database;
            Editor editor = document.Editor;
            docDataset.RefreshAllMaps();
            AfaDocData.ActiveDocData.Document.Editor.Regen();
        }

        [CommandMethod("ESRI_RefreshDataset", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void RefreshDataset()
        {
            AfaDocData.ActiveDocData.Refresh();
        }

        [CommandMethod("ESRI_RefreshMap", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void RefreshMap()
        {
            MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
            Document document = AfaDocData.ActiveDocData.Document;
            Database database = document.Database;
            Editor editor = document.Editor;
            MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
            if (activeRasterService == null)
            {
                editor.WriteMessage(AfaStrings.NoCurrentMapServiceFound);
            }
            else
            {
                activeRasterService.RefreshService();
                activeRasterService.CheckForUpdates();
            }
        }

        [CommandMethod("ESRI_SelectByAttribute", CommandFlags.NoUndoMarker)]
        public static void SBAFunc()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                SelectByAttributesForm form;
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                try
                {
                    PromptSelectionResult result = editor.SelectAll();
                    if (result.Status == PromptStatus.Error)
                    {
                        editor.WriteMessage("\n" + AfaStrings.ZeroFound);
                    }
                    else if (result.Status == PromptStatus.Cancel)
                    {
                        editor.WriteMessage(AfaStrings.CommandCancelled);
                    }
                    else
                    {
                        int count = result.Value.Count;
                        form = new SelectByAttributesForm(false, AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
                        if (form != null)
                        {
                            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Application.MainWindow.Handle, form, true);
                        }
                    }
                }
                catch (Exception exception)
                {
                    form = new SelectByAttributesForm();
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Application.MainWindow.Handle, form, true);
                    editor.WriteMessage("\n" + AfaStrings.Error + " " + exception.Message);
                }
            }
        }

        [CommandMethod("ESRI_SetCurrentDrawingLayer", CommandFlags.UsePickSet)]
        public static void SetCurrentDrawingLayer()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                try
                {
                    MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
                    if (activeFeatureClassOrSubtype != null)
                    {
                        if (!activeFeatureClassOrSubtype.IsSingleLayerQuery())
                        {
                            editor.WriteMessage(AfaStrings.FCNotSingleLayerQuery);
                        }
                        else
                        {
                            string singleLayerQueryLayerName = activeFeatureClassOrSubtype.GetSingleLayerQueryLayerName();
                            if (string.IsNullOrEmpty(singleLayerQueryLayerName))
                            {
                                editor.WriteMessage(AfaStrings.FCLayerQueryNotValid);
                            }
                            else if (DocUtil.SetCurrentDrawingLayer(singleLayerQueryLayerName))
                            {
                                editor.WriteMessage(AfaStrings.CurrentLayerSetTo + " " + singleLayerQueryLayerName);
                            }
                        }
                    }
                }
                catch
                {
                    editor.WriteMessage(AfaStrings.Error);
                }
            }
        }


        [CommandMethod("ESRI_SetCurrentFeatureServiceLimit", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void SetCurrentFeatureServiceLimit()
        {
            MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
            Editor editor = AfaDocData.ActiveDocData.DocDataset.ParentDocument.Editor;
            MSCFeatureService activeFeatureService = AfaDocData.ActiveDocData.GetActiveFeatureService();
            if (activeFeatureService == null)
            {
                editor.WriteMessage(AfaStrings.NoCurrentFeatureServiceFound);
            }
            else if (!activeFeatureService.UpdateExtentFromCurrentView())
            {
                editor.WriteMessage(AfaStrings.ExtentUpdateFailed);
            }
            else
            {
                activeFeatureService.Write();
                editor.WriteMessage(AfaStrings.ExtentUpdated);
                CmdLine.ExecuteQuietCommand(".esri_Synchronize ");
            }
        }

        [CommandMethod("ESRI_SetCurrentMapLimit", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void SetCurrentMapLimit()
        {
            Editor editor = AfaDocData.ActiveDocData.DocDataset.ParentDocument.Editor;
            MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
            if (activeRasterService == null)
            {
                editor.WriteMessage(AfaStrings.NoCurrentMapServiceFound);
            }
            else if (activeRasterService.ParentService == null)
            {
                editor.WriteMessage(AfaStrings.ParentServiceNotConnected);
            }
            else if (!activeRasterService.UpdateExtentFromCurrentView())
            {
                editor.WriteMessage(AfaStrings.ExtentUpdateFailed);
            }
            else
            {
                activeRasterService.Write();
                editor.WriteMessage(AfaStrings.ExtentUpdated);
            }
        }

        [CommandMethod("ESRI_SetFeatureServiceLimit", CommandFlags.NoUndoMarker | CommandFlags.NoPaperSpace)]
        public static void SetFeatureServiceLimit()
        {
            MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
            Editor editor = AfaDocData.ActiveDocData.DocDataset.ParentDocument.Editor;
            if (docDataset.FeatureServices.Count == 0)
            {
                editor.WriteMessage(AfaStrings.NoFeatureServicesFound);
            }
            else
            {
                int num = 0;
                int num2 = 0;
                foreach (MSCFeatureService service in docDataset.FeatureServices.Values)
                {
                    if (!service.UpdateExtentFromCurrentView())
                    {
                        editor.WriteMessage(AfaStrings.ExtentUpdateFailed + " - " + service.Name);
                        num2++;
                        continue;
                    }
                    service.Write();
                    editor.WriteMessage(AfaStrings.ExtentUpdated + " - " + service.Name);
                    num++;
                }
                if ((num > 0) && (num2 == 0))
                {
                    CmdLine.ExecuteQuietCommand(".esri_SynchronizeAll ");
                }
            }
        }

        [CommandMethod("esri_OpenAttributeTable", CommandFlags.NoUndoMarker)]
        public static void ShowFeatureClassTable()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                try
                {
                    try
                    {
                        int count = editor.SelectAll().Value.Count;
                        if ((AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count + AfaDocData.ActiveDocData.DocDataset.FeatureClasses.Count) == 0)
                        {
                            editor.WriteMessage("\n" + AfaStrings.NoFeatureClassesFound);
                        }
                        else
                        {
                            MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
                            if (activeFeatureClassOrSubtype == null)
                            {
                                editor.WriteMessage("\n" + AfaStrings.NoCurrentFeatureClassFound);
                            }
                            else
                            {
                                TableView formToShow = new TableView(activeFeatureClassOrSubtype);
                                Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog((IWin32Window)null, formToShow, true);
                                formToShow.Uninitialize();
                                formToShow.Dispose();
                            }
                        }
                    }
                    catch
                    {
                        editor.WriteMessage("\nError");
                    }
                }
                catch
                {
                    editor.WriteMessage("\n" + AfaStrings.NoFeaturesFound);
                }
            }
        }
        [CommandMethod("esri_TableSelected", CommandFlags.UsePickSet)]
        public static void ShowFeatureClassTableSelected()
        {
            try
            {
                Editor editor = AfaDocData.ActiveDocData.DocDataset.ParentDocument.Editor;
                MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
                if (activeFeatureClassOrSubtype != null)
                {
                    PromptSelectionResult result = editor.SelectImplied();
                    try
                    {
                        if (result.Status != PromptStatus.OK)
                        {
                            editor.WriteMessage(AfaStrings.NoFeaturesFound);
                        }
                        else
                        {
                            ObjectId[] objectIds = result.Value.GetObjectIds();
                            if (objectIds != null)
                            {
                                if (objectIds.Length <= 0)
                                {
                                    editor.WriteMessage(AfaStrings.NoFeaturesFound);
                                }
                                else
                                {
                                    TableView formToShow = new TableView(activeFeatureClassOrSubtype, objectIds);
                                    Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Application.MainWindow.Handle, formToShow, true);
                                    return;
                                }
                            }
                            editor.WriteMessage(AfaStrings.NoFeaturesFound);
                        }
                    }
                    catch (Exception exception1)
                    {
                        MessageBox.Show(exception1.Message);
                    }
                }
                else
                {
                    editor.WriteMessage("\n" + AfaStrings.NoCurrentFeatureClassFound);
                }
            }
            catch
            {
            }
        }

        [CommandMethod("ESRI_ArcGISHelp")]
        public static void ShowHelp()
        {
            Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
            try
            {
                string str = ArcGISRibbon.BuildHelpPath().ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    Process.Start(str);
                }
                else
                {
                    editor.WriteMessage(AfaStrings.UnableToFindHelp);
                }
            }
            catch
            {
                editor.WriteMessage(AfaStrings.UnableToFindHelp);
            }
        }

        [CommandMethod("ESRI_ShowPalette")]
        public static void ShowPalette()
        {
            CmdLine.ExecuteQuietCommand("ESRI_GeneratePalette");
            CmdLine.ExecuteQuietCommand("ESRI_NavigatePalette");
        }

        [CommandMethod("ESRI_ShowTOC", CommandFlags.Modal)]
        public static void ShowTOC()
        {
            TOCPalette.ShowTOC();
        }

        [CommandMethod("esri_SynchronizeAll", CommandFlags.NoUndoMarker)]
        public static void UpdateAllFeatureServices()
        {
            Editor editor = AfaDocData.ActiveDocData.DocDataset.ParentDocument.Editor;
            try
            {
                if (AfaDocData.ActiveDocData.DocDataset.FeatureServices == null)
                {
                    editor.WriteMessage("\n" + AfaStrings.NoFeatureServicesFound);
                }
                if (AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count == 0)
                {
                    editor.WriteMessage("\n" + AfaStrings.NoFeatureServicesFound);
                }
                if (!AfaDocData.ActiveDocData.DocDataset.HasFeatureServicesOpenedForEditing() || AfaDocData.ActiveDocData.DocDataset.CommitAllFeatureServices())
                {
                    bool flag = false;
                    foreach (MSCFeatureService service in AfaDocData.ActiveDocData.DocDataset.FeatureServices.Values)
                    {
                        if (service.HasCommitErrors)
                        {
                            flag = true;
                        }
                    }
                    if (!flag || MessageUtil.ShowYesNo(AfaStrings.CommitErrorQuestion))
                    {
                        AfaDocData.ActiveDocData.DocDataset.RefreshAllFeatureServices();
                    }
                    else
                    {
                        editor.WriteMessage("\n" + AfaStrings.CommandCancelled);
                    }
                }
            }
            catch
            {
            }
        }

        [CommandMethod("esri_Synchronize", CommandFlags.NoUndoMarker)]
        public static void UpdateFeatureService()
        {
            if (null != Application.DocumentManager.MdiActiveDocument)
            {
                Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;
                try
                {
                    MSCFeatureService activeFeatureService = AfaDocData.ActiveDocData.GetActiveFeatureService();
                    if (activeFeatureService == null)
                    {
                        editor.WriteMessage("\n" + AfaStrings.NoCurrentFeatureServiceFound);
                    }
                    else if (activeFeatureService.ReconnectFeatureService())
                    {
                        string rString = "";
                        if (!activeFeatureService.QueryOnly && activeFeatureService.PrepareToCommit(ref rString))
                        {
                            if (MessageUtil.ShowYesNo(string.Format("{0}\n{1}", rString, AfaStrings.ProceedQuestion)))
                            {
                                rString = "";
                                activeFeatureService.CommitEdits(ref rString);
                                if (activeFeatureService.HasCommitErrors && !MessageUtil.ShowYesNo(AfaStrings.CommitErrorQuestion))
                                {
                                    editor.WriteMessage("\n" + AfaStrings.CommandCancelled);
                                    return;
                                }
                            }
                            else
                            {
                                editor.WriteMessage("\n" + AfaStrings.CommandCancelled);
                                return;
                            }
                        }
                        if (!string.IsNullOrEmpty(rString))
                        {
                            rString = rString + "\r\n";
                        }
                        activeFeatureService.Refresh(ref rString);
                        if (!string.IsNullOrEmpty(rString))
                        {
                            MessageBoxEx.Show(new WindowWrapper(Application.MainWindow.Handle), rString, AfaStrings.SynchronizeResults, 0x2710);
                        }
                        if ((!activeFeatureService.QueryOnly && !activeFeatureService.HasCommitErrors) && activeFeatureService.ParentService.IsValid)
                        {
                            activeFeatureService.ParentDataset.RefreshAssociatedMaps(activeFeatureService.ParentService.ServiceURL);
                        }
                    }
                    else
                    {
                        ErrorReport.ShowErrorMessage(AfaStrings.UnableToConnectFeatureService + activeFeatureService.Name);
                    }
                }
                catch
                {
                }
            }
        }
        [CommandMethod("ESRI_UpdatePalette")]
        public static void UpdatePalette()
        {
            Document document = AfaDocData.ActiveDocData.Document;
            bool flag = ToolPalette.PaletteVisible();
            CmdLine.ExecuteQuietCommand("ESRI_GeneratePalette");
            if (!flag)
            {
                CmdLine.ExecuteQuietCommand("ESRI_NavigatePalette");
            }
        }


    }
}
