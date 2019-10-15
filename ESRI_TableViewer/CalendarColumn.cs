using System;
using System.Windows.Forms;

namespace ESRI_TableViewer
{
	public class CalendarColumn : DataGridViewColumn
	{
		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				if (value != null && !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
				{
					throw new InvalidCastException("Must be a CalendarCell");
				}
				base.CellTemplate = value;
			}
		}

		public CalendarColumn() : base(new CalendarCell())
		{
		}
	}
}
