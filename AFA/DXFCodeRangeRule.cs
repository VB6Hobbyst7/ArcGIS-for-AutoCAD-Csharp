using AFA.Resources;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace AFA
{
	public class DXFCodeRangeRule : ValidationRule
	{
		private int _min;

		private int _max;

		public int Min
		{
			get
			{
				return this._min;
			}
			set
			{
				this._min = value;
			}
		}

		public int Max
		{
			get
			{
				return this._max;
			}
			set
			{
				this._max = value;
			}
		}

		public DXFCodeRangeRule()
		{
			this._min = -5;
			this._max = 1071;
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			int num = 0;
			try
			{
				if (((string)value).Length > 0 && !DocUtil.StringToDXFCode((string)value, ref num))
				{
					num = int.Parse((string)value);
				}
			}
			catch (SystemException ex)
			{
				ValidationResult result = new ValidationResult(false, AfaStrings.IllegalCharactersOr + ex.Message);
				return result;
			}
			if (num < this.Min || num > this.Max)
			{
				return new ValidationResult(false, AfaStrings.InvalidDXFCode);
			}
			return new ValidationResult(true, null);
		}
	}
}
