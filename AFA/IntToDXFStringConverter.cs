using System;
using System.Globalization;
using System.Windows.Data;

namespace AFA
{
	public class IntToDXFStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
            try
            {
                bool flag1 = value.GetType() == typeof(string);
                if (!DBNull.Value.Equals(value) && (value != null))
                {
                    value.ToString();
                    DXFCode code = new DXFCode(int.Parse(value.ToString()));
                    return code.CodeString;
                }
                return "";
            }
            catch
            {
                return value;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null)
			{
				try
				{
					value.ToString();
					int value2 = int.Parse(value.ToString());
					DXFCode dXFCode = new DXFCode(value2);
					object result = dXFCode.CodeString;
					return result;
				}
				catch
				{
					object result = value;
					return result;
				}
			}
			return "";
		}
	}
}
