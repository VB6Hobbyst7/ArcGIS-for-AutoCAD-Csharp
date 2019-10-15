using System;
using System.Windows.Forms;

namespace ESRI_TableViewer
{
	internal class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
	{
		private DataGridView dataGridView;

		private bool valueChanged;

		private int rowIndex;

		public object EditingControlFormattedValue
		{
			get
			{
				if (base.ShowCheckBox && !base.Checked)
				{
					return string.Empty;
				}
				if (base.Format == DateTimePickerFormat.Custom)
				{
					return base.Value.ToString();
				}
				return base.Value.ToShortDateString();
			}
			set
			{
				string text = value as string;
				if (!string.IsNullOrEmpty(text))
				{
					base.Value = DateTime.Parse(text);
					return;
				}
				if (base.ShowCheckBox)
				{
					base.Checked = false;
				}
			}
		}

		public int EditingControlRowIndex
		{
			get
			{
				return this.rowIndex;
			}
			set
			{
				this.rowIndex = value;
			}
		}

		public bool RepositionEditingControlOnValueChange
		{
			get
			{
				return false;
			}
		}

		public DataGridView EditingControlDataGridView
		{
			get
			{
				return this.dataGridView;
			}
			set
			{
				this.dataGridView = value;
			}
		}

		public bool EditingControlValueChanged
		{
			get
			{
				return this.valueChanged;
			}
			set
			{
				this.valueChanged = value;
			}
		}

		public Cursor EditingPanelCursor
		{
			get
			{
				return base.Cursor;
			}
		}

		public CalendarEditingControl()
		{
			base.Format = DateTimePickerFormat.Short;
		}

		public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return this.EditingControlFormattedValue;
		}

		public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
			this.Font = dataGridViewCellStyle.Font;
			base.CalendarForeColor = dataGridViewCellStyle.ForeColor;
			base.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
		}

		public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
		{
			switch (key & Keys.KeyCode)
			{
			case Keys.Prior:
			case Keys.Next:
			case Keys.End:
			case Keys.Home:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				return true;
			default:
				return !dataGridViewWantsInputKey;
			}
		}

		public void PrepareEditingControlForEdit(bool selectAll)
		{
		}

		protected override void OnValueChanged(EventArgs eventargs)
		{
			this.valueChanged = true;
			this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
			base.OnValueChanged(eventargs);
		}
	}
}
