using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;

namespace AFA
{
	public class AsyncTaskRunner : IDisposable
	{
		private int _currentTaskCount;

		private List<ITask> _tasks;

		private BackgroundWorker _backWorker;

		private Timer _timer;

		public event EventHandler AllTasksCompleted;

		public event TaskCompletedEventHandler TaskCompleted;

		public void ExecuteTasks(List<ITask> tasks)
		{
			this.CreateNotifyIcon();
			this._tasks = tasks;
			if (this._tasks.Count == 0)
			{
				return;
			}
			this._currentTaskCount = 0;
			this.RunTask(this._tasks[this._currentTaskCount]);
		}

		private void RunTask(ITask task)
		{
			try
			{
				this.OnTaskStarted(task);
				this._backWorker = new BackgroundWorker();
				this._backWorker.DoWork += new DoWorkEventHandler(this.backWorker_DoWork);
				this._backWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backWorker_RunWorkerCompleted);
				this._backWorker.RunWorkerAsync(task);
			}
			catch
			{
			}
		}

		private void backWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (!e.Cancelled)
			{
				ITask task = e.Result as ITask;
				this.OnTaskComleted(task);
				if (this.TaskCompleted != null)
				{
					this.TaskCompleted(this, new TaskCompletedEventArgs(task.TaskName, task.ResultData));
				}
			}
			this._backWorker.DoWork -= new DoWorkEventHandler(this.backWorker_DoWork);
			this._backWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(this.backWorker_RunWorkerCompleted);
			this._backWorker.Dispose();
			this._backWorker = null;
		}

		private void backWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			ITask task = e.Argument as ITask;
			task.Execute();
			e.Result = task;
		}

		private void OnTaskStarted(ITask task)
		{
			this.ShowNotifyIconBalloon(task.TaskName, "Start " + task.TaskName + "...", 2000, true);
		}

		private void OnTaskComleted(ITask task)
		{
			this._currentTaskCount++;
			this.ShowNotifyIconBalloon(task.TaskName, task.CompletionMessage, 2000, task.Successful);
			this._timer = new Timer();
			this._timer.Elapsed += new ElapsedEventHandler(this.timer_Elapsed);
			this._timer.Interval = 2000.0;
			this._timer.Start();
		}

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			this._timer.Stop();
			this._timer.Dispose();
			if (this._currentTaskCount < this._tasks.Count)
			{
				this.RunTask(this._tasks[this._currentTaskCount]);
				return;
			}
			if (this.AllTasksCompleted != null)
			{
				this.AllTasksCompleted(this, EventArgs.Empty);
			}
		}

		private void CreateNotifyIcon()
		{
		}

		private void ShowNotifyIconBalloon(string title, string text, int timeout, bool successful)
		{
		}

		public void Dispose()
		{
		}
	}
}
