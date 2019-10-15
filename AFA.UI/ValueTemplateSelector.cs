using ArcGIS10Types;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace AFA.UI
{
    public class ValueTemplateSelector : DataTemplateSelector
	{
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

		public DataTemplate DateValueTemplate
		{
			get;
			set;
		}

		public DataTemplate IntegerValueTemplate
		{
			get;
			set;
		}

		public DataTemplate RealValueTemplate
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
				DataGridCell dataGridCell = contentPresenter.Parent as DataGridCell;
				DataRowView dataRowView = (DataRowView)dataGridCell.DataContext;
				CadField cadField = (CadField)dataRowView.Row["BaseField"];
				if (cadField != null)
				{
					if (cadField.Domain != null && cadField.Domain.CodedValues.Count > 0)
					{
						result = this.CodedValueTemplate;
					}
					else
					{
						esriFieldType extendedType = (esriFieldType)cadField.ExtendedType;
						if (extendedType == esriFieldType.esriFieldTypeDate)
						{
							result = this.DateValueTemplate;
						}
						else
						{
							result = this.PlainTemplate;
						}
					}
				}
				else
				{
					result = this.PlainTemplate;
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
