using System;
using System.Globalization;
using System.Windows.Data;

namespace AFA
{
	[ValueConversion(typeof(MSCFeatureClass.fcTypeCode), typeof(string))]
	public class GeometryTypeToString : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string)
			{
				return value;
			}
			if (value is MSCFeatureClass.fcTypeCode || value is int)
			{
				return MSCFeatureClass.GetTypeCodeString((MSCFeatureClass.fcTypeCode)value);
			}
			return MSCFeatureClass.GetTypeCodeString(MSCFeatureClass.fcTypeCode.fcTypePoint);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
