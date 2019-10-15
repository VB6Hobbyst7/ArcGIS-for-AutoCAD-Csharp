using System;
using System.Windows;
using System.Windows.Threading;

namespace AFA
{
	public static class ExtensionMethods
	{
		private static Action EmptyDelegate = delegate
		{
		};

		public static void Refresh(this UIElement uiElement)
		{
			try
			{
				uiElement.Dispatcher.Invoke(DispatcherPriority.Render, ExtensionMethods.EmptyDelegate);
			}
			catch
			{
			}
		}
	}
}
