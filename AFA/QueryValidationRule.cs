using AFA.Resources;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace AFA
{
    public class QueryValidationRule : ValidationRule
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
						string value2 = "";
						if (!DXFCode.IsValid(dataRowView.Row["Type"].ToString(), ref value2))
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append(value2);
							ValidationResult result = new ValidationResult(false, stringBuilder.ToString());
							return result;
						}
						string text = dataRowView.Row["Type"].ToString();
						foreach (DataRow dataRow in table.Rows)
						{
							if (dataRow != row && dataRow["Type"].ToString() == text)
							{
								stringBuilder = new StringBuilder();
								stringBuilder.Append(AfaStrings.DuplicateTypeCode);
								ValidationResult result = new ValidationResult(false, stringBuilder.ToString());
								return result;
							}
						}
						DXFCode dXFCode = new DXFCode(text);
						object obj = dataRowView.Row["Value"];
						if (dXFCode.Code == 62)
						{
							obj = DXFCode.TranslateColorString(obj.ToString());
						}
						if (!DXFCode.IsValidTypedValue(dXFCode.Code, obj.ToString()))
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append(AfaStrings.CodeDoesNotMatchType);
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
