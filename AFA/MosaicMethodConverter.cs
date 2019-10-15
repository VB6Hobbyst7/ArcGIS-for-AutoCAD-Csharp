using System;
using System.Globalization;
using System.Windows.Data;

namespace AFA
{
	public class MosaicMethodConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo language)
		{
			string a = value as string;
			if (a == "esriMosaicNone")
			{
				return "None";
			}
			if (a == "esriMosaicCenter")
			{
				return "Closest to Center";
			}
			if (a == "esriMosaicNadir")
			{
				return "Closest to Nadir";
			}
			if (a == "esriMosaicAttribute")
			{
				return "By Attribute";
			}
			if (a == "esriMosaicLockRaster")
			{
				return "Lock Raster";
			}
			if (a == "esriMosaicNorthwest")
			{
				return "North-West";
			}
			if (a == "esriMosaicSeamline")
			{
				return "Seamline";
			}
			return "None";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
		{
			string a = value as string;
			if (a == "None")
			{
				return "esriMosaicNone";
			}
			if (a == "Closest to Center")
			{
				return "esriMosaicCenter";
			}
			if (a == "Closest to Nadir")
			{
				return "esriMosaicNadir";
			}
			if (a == "By Attribute")
			{
				return "esriMosaicAttribute";
			}
			if (a == "Lock Raster")
			{
				return "esriMosaicLockRaster";
			}
			if (a == "North-West")
			{
				return "esriMosaicNorthwest";
			}
			if (a == "Seamline")
			{
				return "esriMosaicSeamline";
			}
			return "esriMosaicNone";
		}
	}
}
