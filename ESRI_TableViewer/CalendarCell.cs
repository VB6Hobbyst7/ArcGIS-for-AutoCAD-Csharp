using System;
using System.Windows.Forms;

namespace ESRI_TableViewer
{
	public class CalendarCell : DataGridViewTextBoxCell
	{
		public bool isNullable = true;

		public override Type EditType
		{
			get
			{
				return typeof(CalendarEditingControl);
			}
		}

		public override Type ValueType
		{
			get
			{
				return typeof(DateTime);
			}
		}

		public override object DefaultNewRowValue
		{
			get
			{
				if (this.isNullable)
				{
					return null;
				}
				return DateTime.Now;
			}
		}

		public CalendarCell()
		{
			base.Style.Format = "d";
		}

		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			CalendarEditingControl calendarEditingControl = base.DataGridView.EditingControl as CalendarEditingControl;
			calendarEditingControl.ShowCheckBox = this.isNullable;
			if (base.Value != null && base.Value != DBNull.Value)
			{
				DateTime value;
				if (base.Value is DateTime)
				{
					calendarEditingControl.Value = (DateTime)base.Value;
				}
				else if (DateTime.TryParse(Convert.ToString(base.Value), out value))
				{
					calendarEditingControl.Value = value;
				}
				else
				{
					calendarEditingControl.Checked = false;
				}
				if (this.isNullable)
				{
					calendarEditingControl.Checked = true;
					return;
				}
			}
			else if (this.isNullable)
			{
				calendarEditingControl.Checked = false;
			}
		}
	}
}
