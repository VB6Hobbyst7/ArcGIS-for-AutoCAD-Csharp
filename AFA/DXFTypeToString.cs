using System;
using System.Globalization;
using System.Windows.Data;

namespace AFA
{
	[ValueConversion(typeof(short), typeof(string))]
	public class DXFTypeToString : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is string)
			{
				int num = 0;
				if (DocUtil.StringToDXFCode((string)value, ref num))
				{
					return value;
				}
			}
			if (value is int)
			{
				return DocUtil.DXFCodeToString((int)value);
			}
			return MSCFeatureClass.GetTypeCodeString(MSCFeatureClass.fcTypeCode.fcTypePoint);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
