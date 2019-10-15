namespace AFA.UI
{
    public interface ILongProcessCommand
	{
		event CommandStartedEventHandler CommandStarted;

		event CommandEndedEventHandler CommandEnded;

		event CommandProgressEventHandler CommandProgress;

		event CommandProgressUpdateValuesEventHandler CommandUpdateProgressValues;
	}
}
