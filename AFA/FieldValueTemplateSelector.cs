using AFA.Resources;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace AFA
{
    public class FieldValueTemplateSelector : DataTemplateSelector
	{
		public DataTemplate DisplayOnlyTemplate
		{
			get;
			set;
		}

		public DataTemplate PlainTemplate
		{
			get;
			set;
		}

		public DataTemplate CodedValueTemplate
		{
			get;
			set;
		}

		public DataTemplate RealValueTemplate
		{
			get;
			set;
		}

		public DataTemplate IntegerValueTemplate
		{
			get;
			set;
		}

		public DataTemplate DateValueTemplate
		{
			get;
			set;
		}

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			DataTemplate result;
			try
			{
				ContentPresenter contentPresenter = container as ContentPresenter;
				object arg_0D_0 = contentPresenter.Tag;
				DataGridCell dataGridCell = contentPresenter.Parent as DataGridCell;
				if (!dataGridCell.IsEditing)
				{
					result = this.DisplayOnlyTemplate;
				}
				else
				{
					DataRowView dataRowView = (DataRowView)dataGridCell.DataContext;
					string a = dataRowView[AfaStrings.Type].ToString();
					CadField cadField = (CadField)dataRowView["BaseField"];
					if (cadField != null && cadField.Domain != null && cadField.Domain.CodedValues.Count > 0)
					{
						result = this.CodedValueTemplate;
					}
					else if (a == "Date")
					{
						result = this.DateValueTemplate;
					}
					else if (a == "Double")
					{
						result = this.RealValueTemplate;
					}
					else if (a == "Integer")
					{
						result = this.IntegerValueTemplate;
					}
					else if (a == "Float")
					{
						result = this.RealValueTemplate;
					}
					else if (a == "Short")
					{
						result = this.IntegerValueTemplate;
					}
					else
					{
						result = this.PlainTemplate;
					}
				}
			}
			catch
			{
				result = this.PlainTemplate;
			}
			return result;
		}
	}
}
