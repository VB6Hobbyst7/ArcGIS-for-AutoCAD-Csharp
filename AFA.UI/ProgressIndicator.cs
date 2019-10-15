using System;
using System.Diagnostics;
using Application = System.Windows.Forms.Application;

namespace AFA.UI
{
    public class ProgressIndicator : IDisposable
	{
		private ILongProcessCommand _cmdObject;

		private ProgressWindow _window;

		public ProgressIndicator(ILongProcessCommand cmdObj)
		{
			this._cmdObject = cmdObj;
			this.WireCommandEvents();
		}

		public void Dispose()
		{
			try
			{
				if (this._window != null)
				{
					this._window.Dispose();
				}
			}
			catch
			{
				Trace.WriteLine("Error encountered in ProgressWindow : Dispose");
			}
		}

		public bool UserCancelled()
		{
			bool result;
			try
			{
				if (this._window == null)
				{
					result = false;
				}
				else
				{
					Application.DoEvents();
					result = this._window.UserCancelled;
				}
			}
			catch
			{
				Trace.WriteLine("Error encountered in ProgressWindow : UserCancelled");
				result = false;
			}
			return result;
		}

		public void ForceCancel()
		{
			try
			{
				if (this._window != null)
				{
					Application.DoEvents();
					this._window.UserCancelled = true;
				}
			}
			catch
			{
				Trace.WriteLine("Error encountered in ProgressWindow : ForceCancel");
				this._window.UserCancelled = true;
			}
		}

		public void HideWindow()
		{
			if (this._window != null)
			{
				this._window.Visible = false;
			}
		}

		public void ShowWindow()
		{
			if (this._window != null)
			{
				this._window.Visible = true;
			}
		}

		private void UnWireCommandEvents()
		{
			if (this._cmdObject != null)
			{
				this._cmdObject.CommandStarted -= new CommandStartedEventHandler(this._cmdObject_CommandStarted);
				this._cmdObject.CommandEnded -= new CommandEndedEventHandler(this._cmdObject_CommandEnded);
				this._cmdObject.CommandProgress -= new CommandProgressEventHandler(this._cmdObject_CommandProgress);
				this._cmdObject.CommandUpdateProgressValues -= new CommandProgressUpdateValuesEventHandler(this._cmdObject_UpdateProgressValues);
			}
		}

		private void WireCommandEvents()
		{
			if (this._cmdObject != null)
			{
				this._cmdObject.CommandStarted += new CommandStartedEventHandler(this._cmdObject_CommandStarted);
				this._cmdObject.CommandEnded += new CommandEndedEventHandler(this._cmdObject_CommandEnded);
				this._cmdObject.CommandProgress += new CommandProgressEventHandler(this._cmdObject_CommandProgress);
				this._cmdObject.CommandUpdateProgressValues += new CommandProgressUpdateValuesEventHandler(this._cmdObject_UpdateProgressValues);
			}
		}

		private void _cmdObject_CommandStarted(object sender, CommandStartEventArgs e)
		{
			if (this._window != null)
			{
				this._window.Dispose();
			}
			this._window = new ProgressWindow();
			this._window.Text = e.WindowTitle;
			if (e.ProgressMaxValue != e.ProgressMinValue)
			{
				this._window.ProgressMinValue = e.ProgressMinValue;
				this._window.ProgressMaxValue = e.ProgressMaxValue;
				this._window.ProgressValue = e.PrograssInitValue;
			}
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(Autodesk.AutoCAD.ApplicationServices.Application.MainWindow.Handle, this._window);
		}

		private void _cmdObject_UpdateProgressValues(object sender, CommandUpdateProgressValuesEventArgs e)
		{
			this._window.ProgressMinValue = e.MinValue;
			this._window.ProgressMaxValue = e.MaxValue;
		}

		private void _cmdObject_CommandProgress(object sender, CommandProgressEventArgs e)
		{
			try
			{
				if (this._window != null)
				{
					if (!string.IsNullOrEmpty(e.ProgressTitle))
					{
						this._window.ProgressTitleMessage = e.ProgressTitle;
					}
					this._window.ProgressDetailMessage = e.ProgressMessage;
					this._window.Refresh();
					Application.DoEvents();
				}
			}
			catch
			{
				Trace.WriteLine("Error encountered in ProgressWindow: _cmdObject_CommandProgress");
			}
		}

		private void _cmdObject_CommandEnded(object sender, EventArgs e)
		{
			try
			{
				if (this._window != null)
				{
					this._window.Close();
					this.UnWireCommandEvents();
				}
			}
			catch
			{
				Trace.WriteLine("Error encountered in ProgressWindow: _cmdObject_CommandEnded");
			}
		}
	}
}
