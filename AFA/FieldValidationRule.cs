using AFA.Resources;
using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace AFA
{
	public class FieldValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			BindingGroup bindingGroup = (BindingGroup)value;
			StringBuilder stringBuilder = null;
			foreach (object current in bindingGroup.Items)
			{
				DataRowView dataRowView = current as DataRowView;
				if (dataRowView != null)
				{
					try
					{
						DataRow row = dataRowView.Row;
						DataTable table = row.Table;
						string text = dataRowView.Row["Name"].ToString();
						if (!CadField.IsValidFieldName(text))
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append(AfaStrings.InvalidFieldName);
							ValidationResult result = new ValidationResult(false, stringBuilder.ToString());
							return result;
						}
						foreach (DataRow dataRow in table.Rows)
						{
							if (dataRow != row && dataRow["Name"].ToString().Equals(text, StringComparison.CurrentCultureIgnoreCase))
							{
								stringBuilder = new StringBuilder();
								stringBuilder.Append(AfaStrings.DuplicateFieldName);
								ValidationResult result = new ValidationResult(false, stringBuilder.ToString());
								return result;
							}
						}
						string text2 = row["Value"].ToString();
						string text3 = row["Type"].ToString();
						if (string.IsNullOrEmpty(text3))
						{
							row["Type"] = "String";
						}
						CadField.GetTypeCode(text3);
						CadField.CadFieldType cadFieldType = CadField.FieldTypeCode(row["Type"].ToString());
						if (!string.IsNullOrEmpty(text2) && !CadField.IsValidTypedValue(cadFieldType, text2))
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append(AfaStrings.ErrorValueNotEqualType);
							ValidationResult result = new ValidationResult(false, stringBuilder.ToString());
							return result;
						}
						string s = row["Length"].ToString();
						int num = 0;
						if (cadFieldType == CadField.CadFieldType.String)
						{
							if (int.TryParse(s, out num))
							{
								if (num < 0)
								{
									stringBuilder = new StringBuilder();
									stringBuilder.Append(AfaStrings.InvalidLengthValue);
									ValidationResult result = new ValidationResult(false, stringBuilder.ToString());
									return result;
								}
								if (num == 0)
								{
									row["Length"] = "255";
									dataRowView["Length"] = "255";
								}
							}
							else
							{
								row["Length"] = "255";
								dataRowView["Length"] = "255";
							}
						}
						else
						{
							if (string.IsNullOrEmpty(row["Value"].ToString()))
							{
								row["Value"] = "0";
								dataRowView["Value"] = "0";
							}
							row["Length"] = "0";
							dataRowView["Length"] = "255";
						}
						if (cadFieldType == CadField.CadFieldType.String && num > 0 && text2.Length > num)
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append(AfaStrings.ValueExceedsFieldLength);
							ValidationResult result = new ValidationResult(false, stringBuilder.ToString());
							return result;
						}
					}
					catch
					{
						stringBuilder = new StringBuilder();
						stringBuilder.Append(AfaStrings.ErrorInDXFCode);
						ValidationResult result = new ValidationResult(false, stringBuilder.ToString());
						return result;
					}
					if (!string.IsNullOrEmpty(dataRowView.Row.RowError))
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						stringBuilder.Append(((stringBuilder.Length != 0) ? ", " : "") + dataRowView.Row.RowError);
					}
				}
			}
			if (stringBuilder != null)
			{
				return new ValidationResult(false, stringBuilder.ToString());
			}
			return ValidationResult.ValidResult;
		}
	}
}
