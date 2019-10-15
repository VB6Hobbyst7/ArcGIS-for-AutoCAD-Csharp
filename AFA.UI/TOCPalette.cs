using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Windows;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AFA.UI
{
    public class TOCPalette
	{
		private static PaletteSet _ps;

		private static bool _showTOC;

		private static TOCControl _toc;

		public static void ShowTOC()
		{
			if (TOCPalette._ps == null)
			{
				TOCPalette._ps = new PaletteSet(AfaStrings.GISContents, new Guid("C7FE86F9-5BCC-4764-AE32-E54587302339"));
				TOCPalette._ps.Size=(new Size(300, 400));
				TOCPalette._ps.Text=(AfaStrings.GISContents);
				TOCPalette._ps.DockEnabled=(DockSides)(20480);
				TOCPalette._toc = new TOCControl();
				ElementHost elementHost = new ElementHost();
				elementHost.AutoSize = true;
				elementHost.Dock = DockStyle.Fill;
				elementHost.Child = TOCPalette._toc;
				TOCPalette._ps.Add(AfaStrings.GISContents, elementHost);
				Application.DocumentManager.DocumentCreated+=(new DocumentCollectionEventHandler(TOCPalette.DocumentManager_DocumentCreated));
				Application.DocumentManager.DocumentDestroyed+=(new DocumentDestroyedEventHandler(TOCPalette.DocumentManager_DocumentDestroyed));
				Application.DocumentManager.DocumentActivated+=(new DocumentCollectionEventHandler(TOCPalette.DocumentManager_DocumentActivated));
				TOCPalette._ps.StateChanged+=(new PaletteSetStateEventHandler(TOCPalette._ps_StateChanged));
				TOCPalette._ps.PaletteActivated+=(new PaletteActivatedEventHandler(TOCPalette._ps_PaletteActivated));
				TOCPalette._ps.PaletteSetDestroy+=(new PaletteSetDestroyEventHandler(TOCPalette._ps_PaletteSetDestroy));
			}
			TOCPalette._ps.KeepFocus=(true);
			TOCPalette._ps.Visible=(true);
			TOCPalette._showTOC = true;
		}

		private static void _ps_PaletteSetDestroy(object sender, EventArgs e)
		{
		}

		private static void _ps_PaletteActivated(object sender, PaletteActivatedEventArgs e)
		{
		}

		private static void _ps_StateChanged(object sender, PaletteSetStateEventArgs e)
		{
            //if (e.NewState == null)
            //{
            //	TOCPalette._showTOC = false;
            //	return;
            //}
            //if (e.NewState == 1)
            //{
            //	TOCPalette._showTOC = true;
            //}
            if (e.NewState == ((StateEventIndex)((int)StateEventIndex.Hide)))
            {
                _showTOC = false;
            }
            else if (e.NewState == ((StateEventIndex)((int)StateEventIndex.Show)))
            {
                _showTOC = true;
            }
        }

		private static void DocumentManager_DocumentDestroyed(object sender, DocumentDestroyedEventArgs e)
		{
			if (TOCPalette._ps != null && Application.DocumentManager.Count == 1)
			{
				bool showTOC = TOCPalette._showTOC;
				TOCPalette._ps.Visible=(false);
				TOCPalette._showTOC = showTOC;
			}
		}

		private static void DocumentManager_DocumentCreated(object sender, DocumentCollectionEventArgs e)
		{
			if (TOCPalette._ps != null && TOCPalette._showTOC)
			{
				TOCPalette._ps.Visible=(true);
			}
		}

		private static void DocumentManager_DocumentActivated(object sender, DocumentCollectionEventArgs e)
		{
			if (TOCPalette._ps != null && TOCPalette._showTOC)
			{
				TOCPalette._ps.Visible=(true);
			}
		}
	}
}
