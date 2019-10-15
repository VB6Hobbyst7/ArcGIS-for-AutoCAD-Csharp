using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
using SystemVariableChangedEventArgs = Autodesk.AutoCAD.ApplicationServices.SystemVariableChangedEventArgs;

namespace AFA.Test
{
    public class TestWindowAttach : Window, IComponentConnector
	{
		private bool _contentLoaded;

		public TestWindowAttach()
		{
            //this.InitializeComponent();
            //Document document = AfaDocData.ActiveDocData.Document;
            //this.SetNewSize();
            //this.AddHook();
            //Application.PreTranslateMessage+=(new PreTranslateMessageEventHandler(this.Application_PreTranslateMessage));
            //Database database = document.Database;
            //         database.SystemVariableChanged += new SystemVariableChangedEventHandler(this.db_SystemVariableChanged);
            //Editor editor = document.Editor;
            //editor.PointMonitor+=(new PointMonitorEventHandler(this.ed_PointMonitor));

            this.InitializeComponent();
            Document document = AfaDocData.ActiveDocData.Document;
            this.SetNewSize();
            this.AddHook();
            Application.PreTranslateMessage += new PreTranslateMessageEventHandler(this.Application_PreTranslateMessage);
         //   document.Database.SystemVariableChanged += new SystemVariableChangedEventHandler(this.db_SystemVariableChanged);
            document.Editor.PointMonitor += new PointMonitorEventHandler(this.ed_PointMonitor);

        }

        private void Application_PreTranslateMessage(object sender, PreTranslateMessageEventArgs e)
		{
			throw new NotImplementedException();
		}

		private void ed_PointMonitor(object sender, PointMonitorEventArgs e)
		{
			if (!e.Context.PointComputed)
			{
				return;
			}
			this.SetNewSize();
		}

		private void db_SystemVariableChanged(object sender, SystemVariableChangedEventArgs e)
		{
			this.SetNewSize();
		}

		public void SetNewSize()
		{
			Document document = AfaDocData.ActiveDocData.Document;
            Point deviceIndependentLocation = document.Window.DeviceIndependentLocation;
			Size deviceIndependentSize = document.Window.DeviceIndependentSize;
			bool flag = false;
			if (base.Width != deviceIndependentSize.Width)
			{
				base.Width = deviceIndependentSize.Width;
				base.Height = deviceIndependentSize.Height;
				flag = true;
			}
			if (base.Left != deviceIndependentLocation.X)
			{
				base.Left = deviceIndependentLocation.X;
				flag = true;
			}
			if (base.Top != deviceIndependentLocation.Y + base.Height)
			{
				base.Top = deviceIndependentLocation.Y;
				flag = true;
			}
			if (flag)
			{
				base.UpdateLayout();
				this.Refresh();
			}
		}

		private void AddHook()
		{
			Document document = AfaDocData.ActiveDocData.Document;
			IntPtr handle = document.Window.Handle;
			HwndSource.FromHwnd(handle);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/test/testwindowattach.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}
	}
}
