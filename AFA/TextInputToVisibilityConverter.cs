using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AFA
{
	public class TextInputToVisibilityConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values[0] is bool && values[1] is bool)
			{
				bool flag = !(bool)values[0];
				if ((bool)values[1] || flag)
				{
					return Visibility.Collapsed;
				}
			}
			return Visibility.Visible;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
