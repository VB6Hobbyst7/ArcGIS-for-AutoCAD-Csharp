using System;
using System.Collections.Generic;

namespace AFA
{
	public class AsyncTaskManager
	{
		private List<ITask> _tasks;

		private AsyncTaskRunner taskRunner;

		private Dictionary<string, object> _resultData;

		public event EventHandler TasksExecutionCompleted;

		public Dictionary<string, object> ResultData
		{
			get
			{
				return this._resultData;
			}
		}

		public AsyncTaskManager()
		{
			this.InitializeTasks();
		}

		public void RunTasks()
		{
			this._resultData = new Dictionary<string, object>();
			this.taskRunner = new AsyncTaskRunner();
			this.taskRunner.TaskCompleted += new TaskCompletedEventHandler(this.taskRunner_TaskCompleted);
			this.taskRunner.AllTasksCompleted += new EventHandler(this.taskRunner_AllTasksCompleted);
			this.taskRunner.ExecuteTasks(this._tasks);
		}

		public void RunTasks(List<ITask> tasks)
		{
			this._tasks = tasks;
			this.RunTasks();
		}

		private void taskRunner_TaskCompleted(object sendr, TaskCompletedEventArgs e)
		{
		}

		private void taskRunner_AllTasksCompleted(object sender, EventArgs e)
		{
			this.taskRunner.Dispose();
			foreach (ITask current in this._tasks)
			{
				if (current.ResultData != null)
				{
					this._resultData.Add(current.TaskName, current.ResultData);
				}
			}
			if (this.TasksExecutionCompleted != null)
			{
				this.TasksExecutionCompleted(this, EventArgs.Empty);
			}
		}

		public void AddTask(ITask task)
		{
			this._tasks.Add(task);
		}

		private void InitializeTasks()
		{
			this._tasks = new List<ITask>();
		}
	}
}
