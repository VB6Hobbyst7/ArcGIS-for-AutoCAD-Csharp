using System;
using System.Globalization;
using System.Windows.Data;

namespace AFA
{
	public class MosaicOperatorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo language)
		{
			string a = value as string;
			if (a == "<None>")
			{
				return "<None>";
			}
			if (a == "MT_FIRST")
			{
				return "First";
			}
			if (a == "MT_LAST")
			{
				return "Last";
			}
			if (a == "MT_MIN")
			{
				return "Min";
			}
			if (a == "MT_MAX")
			{
				return "Max";
			}
			if (a == "MT_MEAN")
			{
				return "Mean";
			}
			if (a == "MT_BLEND")
			{
				return "Blend";
			}
			return "<None>";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
		{
			string a = value as string;
			if (a == "<None>")
			{
				return "<None>";
			}
			if (a == "First")
			{
				return "MT_FIRST";
			}
			if (a == "Last")
			{
				return "MT_LAST";
			}
			if (a == "Min")
			{
				return "MT_MIN";
			}
			if (a == "Max")
			{
				return "MT_MAX";
			}
			if (a == "Mean")
			{
				return "MT_MEAN";
			}
			if (a == "Blend")
			{
				return "MT_BLEND";
			}
			return "<None>";
		}
	}
}
