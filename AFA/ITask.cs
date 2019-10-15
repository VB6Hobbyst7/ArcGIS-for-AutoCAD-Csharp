namespace AFA
{
    public interface ITask
	{
		string TaskName
		{
			get;
		}

		string CompletionMessage
		{
			get;
		}

		object ResultData
		{
			get;
		}

		bool Successful
		{
			get;
		}

		void Execute();
	}
}
