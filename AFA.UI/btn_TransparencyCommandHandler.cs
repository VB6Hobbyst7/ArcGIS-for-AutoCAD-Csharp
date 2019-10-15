using System;
using System.Windows.Input;

namespace AFA.UI
{
	internal class btn_TransparencyCommandHandler : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public void Execute(object param)
		{
			btnTransparencyPercent btnTransparencyPercent = param as btnTransparencyPercent;
			if (btnTransparencyPercent != null)
			{
				int percentValue = btnTransparencyPercent.PercentValue;
				MSCRasterService activeRasterService = ArcGISRibbon.GetActiveRasterService();
				if (activeRasterService != null)
				{
					activeRasterService.SetTransparency(percentValue);
				}
			}
		}

		public bool CanExecute(object param)
		{
			return true;
		}
	}
}
