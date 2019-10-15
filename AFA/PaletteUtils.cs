using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Runtime.InteropServices;
using System.Security;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AFA
{
    public static class PaletteUtils
	{
		private class Thunk : IDisposable
		{
			private EventHandler handler;

			public Thunk(EventHandler handler)
			{
				if (handler == null)
				{
					throw new ArgumentNullException("handler");
				}
				this.handler = handler;
				Application.Idle += new EventHandler(this.Application_Idle);
			}

			private void Application_Idle(object sender, EventArgs e)
			{
				Application.Idle -= new EventHandler(this.Application_Idle);
				PaletteUtils.ActivateEditor();
				try
				{
					this.handler(null, e);
				}
				finally
				{
					this.Dispose();
				}
			}

			~Thunk()
			{
				this.Dispose();
			}

			public void Dispose()
			{
				GC.SuppressFinalize(this);
				if (this.handler != null)
				{
					Application.Idle -= new EventHandler(this.Application_Idle);
				}
				this.handler = null;
			}
		}

		public delegate void ExecuteInDocumentContextDelegate();

		private const int WM_SETFOCUS = 7;

		private const string commandName = "DOCUMENT_COMMAND";

		private const CommandFlags CommandFlagsSilent = (CommandFlags)8388624;

		private const CommandFlags CommandFlagsSilentNoUndo = (CommandFlags)25165840;

		private static PaletteUtils.ExecuteInDocumentContextDelegate documentContextHandler;

		public static Document ActiveDocument
		{
			get
			{
				return Application.DocumentManager.MdiActiveDocument;
			}
		}

		public static void ActivateEditor()
		{
			if (PaletteUtils.ActiveDocument == null)
			{
				return;
			}
			PaletteUtils.PostMessage(PaletteUtils.ActiveDocument.Window.Handle, 7, IntPtr.Zero, IntPtr.Zero);
			System.Windows.Forms.Application.DoEvents();
		}

		public static void OnEditorActivated(object sender, EventHandler handler)
		{
			if (Application.DocumentManager.IsApplicationContext)
			{
				new PaletteUtils.Thunk(handler);
			}
		}

		public static void ExecuteInDocumentContext(PaletteUtils.ExecuteInDocumentContextDelegate handler)
		{
			if (handler == null)
			{
				return;
			}
			if (PaletteUtils.ActiveDocument == null)
			{
				return;
			}
			PaletteUtils.ActivateEditor();
			if (!Application.DocumentManager.IsApplicationContext)
			{
				handler();
				return;
			}
			if (!PaletteUtils.ActiveDocument.Editor.IsQuiescent)
			{
				throw new InvalidOperationException(AfaStrings.AutoCADIsBusy);
			}
			PaletteUtils.documentContextHandler = handler;
			PaletteUtils.ActiveDocument.SendStringToExecute("DOCUMENT_COMMAND\n", true, false, false);
			PaletteUtils.acedPostCommand("");
		}

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        //[CommandMethod]
        [CommandMethod("DOCUMENT_COMMAND", CommandFlags.NoHistory | CommandFlags.NoMultiple)]
        public static void OnCommandExecute()
		{
			if (PaletteUtils.documentContextHandler != null)
			{
				bool documentActivationEnabled = Application.DocumentManager.DocumentActivationEnabled;
				Application.DocumentManager.DocumentActivationEnabled=(false);
				try
				{
					PaletteUtils.documentContextHandler();
				}
				finally
				{
					PaletteUtils.documentContextHandler = null;
					Application.DocumentManager.DocumentActivationEnabled=(documentActivationEnabled);
				}
			}
		}

		[SuppressUnmanagedCodeSecurity]
		[DllImport("acad.exe", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?acedPostCommand@@YAHPB_W@Z")]
		public static extern int acedPostCommand(string cmd);
	}
}
