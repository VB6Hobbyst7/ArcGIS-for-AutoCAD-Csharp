using AFA;
using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Internal;
using SelectByAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DataColumn = System.Data.DataColumn;
using DataTable = System.Data.DataTable;
using Exception = Autodesk.AutoCAD.Runtime.Exception;

namespace ESRI_TableViewer
{
    public class TableView : Form
	{
		public class NumericTextBox : ToolStripTextBox
		{
			private bool allowSpace;

			protected override void OnKeyPress(KeyPressEventArgs e)
			{
				base.OnKeyPress(e);
				NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
				string numberDecimalSeparator = numberFormat.NumberDecimalSeparator;
				string numberGroupSeparator = numberFormat.NumberGroupSeparator;
				string negativeSign = numberFormat.NegativeSign;
				string text = e.KeyChar.ToString();
				if (char.IsDigit(e.KeyChar))
				{
					return;
				}
				if (!text.Equals(numberDecimalSeparator) && !text.Equals(numberGroupSeparator))
				{
					if (text.Equals(negativeSign))
					{
						return;
					}
					if (e.KeyChar == '\b')
					{
						return;
					}
					if (this.allowSpace && e.KeyChar == ' ')
					{
						return;
					}
					e.Handled = true;
				}
			}
		}

		private static DataGridView d;

		private ObjectId[] LimitedIds;

		private bool Initializing;

		private Dictionary<string, MSCFeatureClass> items;

		private static bool isSettingValue = false;

		private object lastDirtyCell;

		private static bool watching = false;

		private IContainer components;

		private MenuStrip menuStrip1;

		private MenuStrip menuStrip2;

		private DataGridView dataGridView1;

		private ToolStripMenuItem toolStripMenuItem1;

		private ToolStripMenuItem findReplaceToolStripMenuItem;

		private ToolStripMenuItem selectByAttributesToolStripMenuItem;

		private ToolStripMenuItem switchSelectionToolStripMenuItem;

		private ToolStripMenuItem selectAllToolStripMenuItem;

		private ToolStripMenuItem zoomToolStripMenuItem;

		private ToolStripMenuItem clearHighlightingToolStripMenuItem;

		private ToolStripMenuItem featureClassToolStripMenuItem;

		private ToolStripComboBox featureClassListComboBox1;

		private ToolStripMenuItem moveToBeginningOfTableToolStripMenuItem;

		private ToolStripMenuItem moveToThePreviousRecordToolStripMenuItem;

		private ToolStripMenuItem moveToTheNextRecordToolStripMenuItem;

		private ToolStripMenuItem moveToTheEndOfTableToolStripMenuItem;

		private ToolStripSeparator t2;

		private ToolStripMenuItem showAllRecordsToolStripMenuItem;

		private ToolStripMenuItem showSelectedRecordsToolStripMenuItem;

		private ToolStripSeparator t3;

		private ToolStripTextBox toolStriplabel;

		private ToolStripSeparator toolStripSeparator1;

		private TableView.NumericTextBox toolStripTextBox1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem cutToolStripMenuItem;

		private ToolStripMenuItem copyToolStripMenuItem;

		private ToolStripMenuItem pasteToolStripMenuItem;

		private ToolStripMenuItem deleteToolStripMenuItem;

		private ContextMenuStrip contextMenuStrip2;

		private ToolStripMenuItem zoomToToolStripMenuItem;

		private ToolStripMenuItem selectUnselectToolStripMenuItem;

		private ToolStripMenuItem copySelectedToolStripMenuItem;

		private ToolStripMenuItem deleteToolStripMenuItem1;

		private ToolStripMenuItem clearSelectionToolStripMenuItem;

		private ToolStripMenuItem subtypeToolStripMenuItem;

		private ToolStripComboBox subtypeListComboBox;

		public static DataGridView dg
		{
			get
			{
				return TableView.d;
			}
		}

		public TableView(MSCFeatureClass currentFC, ObjectId[] limitedIds)
		{
			try
			{
				this.Initializing = true;
				this.LimitedIds = limitedIds;
				this.InitializeComponent();
				this.fillRows(currentFC);
				this.updateUI();
				this.clearHighlightingToolStripMenuItem.Enabled = false;
				if (TableView.d.Width + 10 < base.Width)
				{
					base.Width = TableView.d.Width + 10;
				}
				this.PopulateFeatureClassCombo(AfaDocData.ActiveDocData.GetTopActiveFeatureClass(), AfaDocData.ActiveDocData.GetActiveSubtype());
				this.startWatchers();
				base.FormClosing += new FormClosingEventHandler(this.TableView_FormClosing);
				base.MinimizeBox = false;
				this.dataGridView1.MouseUp += new MouseEventHandler(this.dataGridView1_MouseUp);
				this.dataGridView1.CurrentCellChanged += new EventHandler(this.dataGridView1_CurrentCellChanged);
				this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
				this.dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
				this.dataGridView1.KeyDown += new KeyEventHandler(this.dataGridView1_KeyDown);
				this.Initializing = false;
				TableView.d = this.dataGridView1;
			}
			catch
			{
				this.Initializing = false;
			}
		}

		public TableView(MSCFeatureClass currentFC)
		{
			try
			{
				this.Initializing = true;
				try
				{
					this.InitializeComponent();
				}
				catch (SystemException ex)
				{
					ErrorReport.ShowErrorMessage("Error in Initializing Table View: \n" + ex.Message);
					return;
				}
				this.fillRows(currentFC);
				this.updateUI();
				this.clearHighlightingToolStripMenuItem.Enabled = false;
				if (TableView.d.Width + 10 < base.Width)
				{
					base.Width = TableView.d.Width + 10;
				}
				this.PopulateFeatureClassCombo(AfaDocData.ActiveDocData.GetTopActiveFeatureClass(), AfaDocData.ActiveDocData.GetActiveSubtype());
				this.startWatchers();
				this.dataGridView1.MouseUp += new MouseEventHandler(this.dataGridView1_MouseUp);
				this.dataGridView1.CurrentCellChanged += new EventHandler(this.dataGridView1_CurrentCellChanged);
				this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
				this.dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
				this.dataGridView1.KeyDown += new KeyEventHandler(this.dataGridView1_KeyDown);
				this.Initializing = false;
				TableView.d = this.dataGridView1;
			}
			catch
			{
				this.Initializing = false;
			}
		}

		private void TableView_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.lastDirtyCell != null)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)this.lastDirtyCell;
				CadField cadField = null;
				string text = null;
				try
				{
					text = dataGridViewCell.EditedFormattedValue.ToString();
					string columnName = this.dataGridView1.Columns[dataGridViewCell.ColumnIndex].Name;
					MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
					cadField = activeFeatureClassOrSubtype.Fields.Find((CadField f) => f.Name == columnName);
				}
				catch
				{
					return;
				}
				if (cadField != null)
				{
					ObjectId id = new ObjectId((IntPtr)this.dataGridView1.Rows[dataGridViewCell.RowIndex].Cells[0].Value);
					DataGridViewRow arg_C5_0 = this.dataGridView1.Rows[dataGridViewCell.RowIndex];
					if (cadField.Domain != null)
					{
						if (cadField.Domain.CodedValues != null)
						{
							object obj = dataGridViewCell.ParseFormattedValue(dataGridViewCell.EditedFormattedValue, dataGridViewCell.Style, null, null);
							if (obj != null)
							{
								this.WriteField(obj.ToString(), id, cadField);
							}
						}
					}
					else if (CadField.IsValidTypedValue(cadField.FieldType, text))
					{
						this.WriteField(text, id, cadField);
					}
					bool arg_127_0 = cadField.TypeField;
					return;
				}
				return;
			}
		}

		~TableView()
		{
			try
			{
				this.Uninitialize();
			}
			catch
			{
			}
		}

		public void Uninitialize()
		{
			try
			{
				if (this.items != null)
				{
					this.items.Clear();
				}
				TableView.d = null;
				this.stopWatchers();
				this.stopWatchers();
				this.featureClassListComboBox1.ComboBox.DataBindings.Clear();
			}
			catch
			{
			}
		}

		private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			string value = e.FormattedValue.ToString();
			string columnName = this.dataGridView1.Columns[e.ColumnIndex].Name;
			MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
			ObjectId id = new ObjectId((IntPtr)this.dataGridView1.Rows[e.RowIndex].Cells[0].Value);
			List<CadField> entityFields = activeFeatureClassOrSubtype.GetEntityFields(id);
			CadField cadField = entityFields.Find((CadField f) => f.Name == columnName);
			if (cadField == null)
			{
				cadField = activeFeatureClassOrSubtype.Fields.Find((CadField f) => f.Name == columnName);
			}
			if (cadField == null)
			{
				return;
			}
			if (cadField.Domain != null)
			{
				if (cadField.Domain.HasCodedValues())
				{
					return;
				}
				if (cadField.Domain.IsWithinRangeValue(e.FormattedValue))
				{
					return;
				}
				this.dataGridView1.Rows[e.RowIndex].ErrorText = AfaStrings.ValueOutsideOfRange + " (" + columnName + ")";
				e.Cancel = true;
			}
			if (!CadField.IsValidTypedValue(cadField.FieldType, value))
			{
				this.dataGridView1.Rows[e.RowIndex].ErrorText = AfaStrings.ValueNotValidForType + " (" + columnName + ")";
				e.Cancel = true;
			}
		}

		private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			this.dataGridView1.Rows[e.RowIndex].ErrorText = string.Empty;
		}

		private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			if (this.dataGridView1.IsCurrentCellDirty)
			{
				this.lastDirtyCell = this.dataGridView1.CurrentCell;
			}
		}

		private object WriteField(object newValue, ObjectId id, CadField cf)
		{
			object result = null;
			object obj = null;
			try
			{
				obj = cf.Value.Value;
			}
			catch
			{
			}
			if (string.IsNullOrEmpty(newValue.ToString()))
			{
				newValue = obj;
			}
			TypedValue arg_2F_0 = cf.Value;
			if (newValue == obj)
			{
				CadField.RemoveCadAttribute(id, cf);
				TableView.isSettingValue = true;
				this.dataGridView1.CurrentCell.Value = obj;
				result = obj;
				TableView.isSettingValue = false;
			}
			else if (CadField.IsValidTypedValue(cf.FieldType, newValue.ToString()))
			{
				CadField cadField = new CadField(cf);
				if (cf.Length > 0)
				{
					string text = newValue.ToString();
					if (text.Length > (int)cf.Length)
					{
						text = text.Remove((int)cf.Length);
						newValue = text;
					}
				}
				cadField.Value = CadField.CreateTypedValue(cf.FieldType, newValue.ToString());
				if (cadField.TypeField)
				{
					MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
					activeFeatureClassOrSubtype.UpdateSubtypeLayer(id, cadField.Value.Value);
				}
				CadField.AddCadAttributeToEntity(id, cadField);
				result = cadField.Value.Value;
			}
			return result;
		}

		private string WriteField(string newValueString, ObjectId id, CadField cf)
		{
			string text = "";
			string result = text;
			try
			{
				text = cf.Value.Value.ToString();
			}
			catch
			{
			}
			if (string.IsNullOrEmpty(newValueString))
			{
				CadField.RemoveCadAttribute(id, cf);
				TableView.isSettingValue = true;
				this.dataGridView1.CurrentCell.Value = text;
				result = text;
				TableView.isSettingValue = false;
			}
			else
			{
				TypedValue arg_5B_0 = cf.Value;
				if (newValueString == text)
				{
					CadField.RemoveCadAttribute(id, cf);
					TableView.isSettingValue = true;
					this.dataGridView1.CurrentCell.Value = text;
					result = text;
					TableView.isSettingValue = false;
				}
				else if (CadField.IsValidTypedValue(cf.FieldType, newValueString))
				{
					CadField cadField = new CadField(cf);
					if (cf.Length > 0 && newValueString.Length > (int)cf.Length)
					{
						newValueString = newValueString.Remove((int)cf.Length);
					}
					cadField.Value = CadField.CreateTypedValue(cf.FieldType, newValueString);
					if (cadField.TypeField)
					{
						MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
						activeFeatureClassOrSubtype.UpdateSubtypeLayer(id, cadField.Value.Value);
					}
					CadField.AddCadAttributeToEntity(id, cadField);
					result = cadField.Value.Value.ToString();
				}
			}
			return result;
		}

		private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			this.lastDirtyCell = null;
			if (TableView.isSettingValue)
			{
				return;
			}
			try
			{
				object newValue = null;
				try
				{
					newValue = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
				}
				catch (SystemException)
				{
				}
				string columnName = this.dataGridView1.Columns[e.ColumnIndex].Name;
				MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
				CadField cadField = activeFeatureClassOrSubtype.Fields.Find((CadField f) => f.Name == columnName);
				if (cadField != null)
				{
					ObjectId id = new ObjectId((IntPtr)this.dataGridView1.Rows[e.RowIndex].Cells[0].Value);
					DataTable dataTable = (DataTable)this.dataGridView1.DataSource;
					DataRow dataRow = dataTable.Rows.Find(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value);
					dataRow[columnName] = this.WriteField(newValue, id, cadField);
				}
			}
			catch
			{
			}
		}

		private void startWatchers()
		{
			if (!TableView.watching)
			{
				this.dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
				this.dataGridView1.SelectionChanged += new EventHandler(this.dataGridView1_SelectionChanged);
				this.dataGridView1.CurrentCellDirtyStateChanged += new EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
				TableView.watching = true;
			}
		}

		private void stopWatchers()
		{
			if (TableView.watching)
			{
				this.dataGridView1.CellValueChanged -= new DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
				this.dataGridView1.SelectionChanged -= new EventHandler(this.dataGridView1_SelectionChanged);
				TableView.watching = false;
			}
		}

		public void fillRows(MSCFeatureClass currentFC)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				DataTable dataTable;
				if (this.LimitedIds != null)
				{
					dataTable = currentFC.GetDataTable(new ObjectIdCollection(this.LimitedIds));
				}
				else
				{
					dataTable = currentFC.GetDataTable();
				}
				this.stopWatchers();
				this.dataGridView1.AutoGenerateColumns = false;
				this.dataGridView1.DataSource = dataTable;
				this.dataGridView1.Columns.Clear();
				foreach (DataColumn dataColumn in dataTable.Columns)
				{
					DataGridViewColumn dataGridViewColumn;
					if (dataColumn.ExtendedProperties.ContainsKey("Domain"))
					{
						FieldDomain fieldDomain = (FieldDomain)dataColumn.ExtendedProperties["Domain"];
						if (fieldDomain.HasCodedValues())
						{
							List<MSCCodedValue> codedValuesDisplayList = fieldDomain.CodedValuesDisplayList;
							DataGridViewComboBoxColumn dataGridViewComboBoxColumn = new DataGridViewComboBoxColumn();
							dataGridViewComboBoxColumn.DefaultCellStyle = this.dataGridView1.DefaultCellStyle;
							dataGridViewComboBoxColumn.DefaultCellStyle.BackColor = this.dataGridView1.DefaultCellStyle.BackColor;
							dataGridViewComboBoxColumn.DataSource = codedValuesDisplayList;
							dataGridViewComboBoxColumn.DisplayMember = "DisplayName";
							dataGridViewComboBoxColumn.ValueType = fieldDomain.GetFieldType();
							dataGridViewComboBoxColumn.ValueMember = "Value";
							dataGridViewComboBoxColumn.Name = dataColumn.ColumnName;
							dataGridViewComboBoxColumn.DataPropertyName = dataColumn.ColumnName;
							dataGridViewColumn = dataGridViewComboBoxColumn;
							dataGridViewColumn.ValueType = dataGridViewComboBoxColumn.ValueType;
						}
						else
						{
							dataGridViewColumn = new DataGridViewTextBoxColumn();
							dataGridViewColumn.DataPropertyName = dataColumn.ColumnName;
							dataGridViewColumn.Name = dataColumn.ColumnName;
						}
					}
					else if (dataColumn.ExtendedProperties.ContainsKey("DateField"))
					{
						dataGridViewColumn = new CalendarColumn();
						dataGridViewColumn.DataPropertyName = dataColumn.ColumnName;
						dataGridViewColumn.Name = dataColumn.ColumnName;
					}
					else
					{
						dataGridViewColumn = new DataGridViewTextBoxColumn();
						dataGridViewColumn.DataPropertyName = dataColumn.ColumnName;
						dataGridViewColumn.Name = dataColumn.ColumnName;
					}
					if (dataGridViewColumn != null)
					{
						this.dataGridView1.Columns.Add(dataGridViewColumn);
					}
				}
				string arg_208_0 = this.dataGridView1.Columns[1].Name;
				string arg_21F_0 = this.dataGridView1.Columns[1].DataPropertyName;
				dataTable.PrimaryKey = new DataColumn[]
				{
					dataTable.Columns[0]
				};
				try
				{
					this.toolStripTextBox1.Text = "1";
					this.toolStriplabel.ReadOnly = false;
					this.toolStriplabel.Text = string.Format(AfaStrings.XofYSelected, 0, dataTable.Rows.Count);
					this.toolStriplabel.ReadOnly = true;
					this.toolStripTextBox1.Text = "1";
				}
				catch
				{
				}
				this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
				this.dataGridView1.AutoResizeColumns();
				TableView.d = this.dataGridView1;
				this.startWatchers();
				this.Cursor = Cursors.Default;
			}
			catch
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void findReplaceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FindReplace findReplace = new FindReplace((DataTable)this.dataGridView1.DataSource);
			this.clearHighlightingToolStripMenuItem.Enabled = false;
			for (int i = 0; i < this.dataGridView1.RowCount; i++)
			{
				for (int j = 0; j < this.dataGridView1.ColumnCount; j++)
				{
					this.dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
				}
			}
			Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessDialog(Autodesk.AutoCAD.ApplicationServices.Core.Application.MainWindow.Handle, findReplace, false);
			findReplace.FormClosed += new FormClosedEventHandler(this.fr_FormClosed);
		}

		private void fr_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.clearHighlightingToolStripMenuItem.Enabled = false;
			FindReplace findReplace = (FindReplace)sender;
			if (findReplace != null)
			{
				this.clearHighlightingToolStripMenuItem.Enabled = findReplace.HasHighlightedCells;
			}
		}

		private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (null == Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument)
			{
				return;
			}
			Editor editor = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Editor;
			try
			{
				Document document = AfaDocData.ActiveDocData.Document;
				using (Transaction transaction = document.TransactionManager.StartTransaction())
				{
					DataGridViewSelectedRowCollection selectedRows = this.dataGridView1.SelectedRows;
					List<ObjectId> list = new List<ObjectId>();
					for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
					{
						if (selectedRows.Contains(this.dataGridView1.Rows[i]))
						{
							ObjectId objectId = new ObjectId((IntPtr)this.dataGridView1.Rows[i].Cells[0].Value);
							if (objectId.ObjectClass.DxfName != "GROUP")
							{
								list.Add(objectId);
							}
							else
							{
								Group group = (Group)transaction.GetObject(objectId, 0);
								ObjectId[] allEntityIds = group.GetAllEntityIds();
								ObjectId[] array = allEntityIds;
								for (int j = 0; j < array.Length; j++)
								{
									ObjectId item = array[j];
									list.Add(item);
								}
							}
						}
					}
					document.TransactionManager.QueueForGraphicsFlush();
					document.TransactionManager.FlushGraphics();
					document.Editor.UpdateScreen();
					transaction.Commit();
					System.Windows.Forms.Application.UseWaitCursor = true;
					if (list.Count > 0)
					{
						document.Editor.SetImpliedSelection(DocUtil.ExpandGroupObjectIds(document, list.ToArray()));
						Utils.ZoomObjects(false);
					}
					System.Windows.Forms.Application.UseWaitCursor = false;
					DocUtil.UpdateView(editor);
					AfaDocData.ActiveDocData.DocDataset.UpdateMaps();
				}
			}
			catch
			{
			}
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (null == Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument)
			{
				return;
			}
			Editor editor = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Editor;
			try
			{
				using (Transaction transaction = AfaDocData.ActiveDocData.Document.Database.TransactionManager.StartTransaction())
				{
					this.dataGridView1.SelectAll();
					ObjectId[] array = new ObjectId[this.dataGridView1.Rows.Count];
					for (int i = 0; i < array.Length; i++)
					{
						if (this.dataGridView1.Rows[i].Cells[0].Value != null)
						{
							array[i] = new ObjectId((IntPtr)this.dataGridView1.Rows[i].Cells[0].Value);
						}
					}
					System.Windows.Forms.Application.UseWaitCursor = true;
					editor.SetImpliedSelection(DocUtil.ExpandGroupObjectIds(AfaDocData.ActiveDocData.Document, array));
					System.Windows.Forms.Application.UseWaitCursor = false;
					this.zoomToolStripMenuItem.Enabled = false;
					DocUtil.UpdateView(editor);
					transaction.Commit();
				}
				Utils.ZoomObjects(false);
			}
			catch
			{
			}
		}

		private void switchSelectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridView1.SelectedRows.Count == 0)
				{
					this.dataGridView1.SelectAll();
				}
				else if (this.dataGridView1.SelectedRows.Count == this.dataGridView1.Rows.Count)
				{
					this.dataGridView1.ClearSelection();
				}
				else
				{
					DataGridViewSelectedRowCollection selectedRows = this.dataGridView1.SelectedRows;
					for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
					{
						if (!selectedRows.Contains(this.dataGridView1.Rows[i]))
						{
							this.dataGridView1.Rows[i].Selected = true;
						}
						else
						{
							this.dataGridView1.Rows[i].Selected = false;
						}
					}
				}
			}
			catch
			{
			}
		}

		private void dataGridView1_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridView1.SelectedRows.Count == 0)
				{
					this.zoomToolStripMenuItem.Enabled = false;
					this.clearSelectionToolStripMenuItem.Enabled = false;
					if (this.dataGridView1.DataSource != null)
					{
						DataTable dataTable = (DataTable)this.dataGridView1.DataSource;
						this.toolStriplabel.Text = string.Format(AfaStrings.XofYSelected, 0, dataTable.Rows.Count);
					}
					int num = this.dataGridView1.CurrentCellAddress.Y + 1;
					this.toolStripTextBox1.Text = num.ToString();
				}
				else
				{
					this.zoomToolStripMenuItem.Enabled = true;
					this.clearSelectionToolStripMenuItem.Enabled = true;
					if (this.dataGridView1.DataSource != null)
					{
						DataTable dataTable2 = (DataTable)this.dataGridView1.DataSource;
						this.toolStriplabel.Text = string.Format(AfaStrings.XofYSelected, this.dataGridView1.SelectedRows.Count, dataTable2.Rows.Count);
					}
					int num2 = this.dataGridView1.CurrentCellAddress.Y + 1;
					this.toolStripTextBox1.Text = num2.ToString();
				}
			}
			catch
			{
				this.zoomToolStripMenuItem.Enabled = false;
				this.clearSelectionToolStripMenuItem.Enabled = false;
			}
		}

		private void moveToBeginningOfTableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.Rows.Count != 0)
			{
				int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
				if (rowIndex != 0)
				{
					this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[this.dataGridView1.CurrentCell.ColumnIndex];
					this.dataGridView1.CurrentCell.Selected = true;
					this.toolStripTextBox1.Text = (this.dataGridView1.CurrentCell.RowIndex + 1).ToString();
				}
			}
		}

		private void moveToThePreviousRecordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.Rows.Count != 0)
			{
				int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
				int columnIndex = this.dataGridView1.CurrentCell.ColumnIndex;
				if (rowIndex > 0)
				{
					this.dataGridView1.CurrentCell = this.dataGridView1.Rows[rowIndex - 1].Cells[columnIndex];
					this.dataGridView1.CurrentCell.Selected = true;
					this.toolStripTextBox1.Text = (this.dataGridView1.CurrentCell.RowIndex + 1).ToString();
				}
			}
		}

		private void moveToTheNextRecordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.Rows.Count != 0 && this.dataGridView1.CurrentCellAddress.X + 1 != this.dataGridView1.Rows.Count)
			{
				int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
				int columnIndex = this.dataGridView1.CurrentCell.ColumnIndex;
				if (rowIndex <= this.dataGridView1.RowCount - 2)
				{
					this.dataGridView1.CurrentCell = this.dataGridView1.Rows[rowIndex + 1].Cells[columnIndex];
					this.dataGridView1.CurrentCell.Selected = true;
					this.toolStripTextBox1.Text = (this.dataGridView1.CurrentCell.RowIndex + 1).ToString();
				}
			}
		}

		private void moveToTheEndOfTableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.Rows.Count != 0)
			{
				int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
				int columnIndex = this.dataGridView1.CurrentCell.ColumnIndex;
				if (rowIndex < this.dataGridView1.RowCount)
				{
					this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1].Cells[columnIndex];
					this.dataGridView1.CurrentCell.Selected = true;
					this.toolStripTextBox1.Text = (this.dataGridView1.CurrentCell.RowIndex + 1).ToString();
				}
			}
		}

		private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				if (e.KeyChar == '\r')
				{
					int columnIndex = this.dataGridView1.CurrentCell.ColumnIndex;
					int num = int.Parse(this.toolStripTextBox1.Text);
					if (num >= 1 && num <= this.dataGridView1.RowCount)
					{
						this.dataGridView1.CurrentCell = this.dataGridView1.Rows[num - 1].Cells[columnIndex];
						this.dataGridView1.CurrentCell.Selected = true;
						this.toolStripTextBox1.Text = num.ToString();
					}
				}
			}
			catch
			{
			}
		}

		private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
		{
			try
			{
				int num = this.dataGridView1.CurrentCellAddress.Y + 1;
				this.toolStripTextBox1.Text = num.ToString();
			}
			catch
			{
			}
		}

		private void clearSelectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < this.dataGridView1.RowCount; i++)
				{
					if (this.dataGridView1.Rows[i].Selected)
					{
						this.dataGridView1.Rows[i].Selected = false;
					}
				}
			}
			catch
			{
			}
		}

		private void clearHighlightingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.dataGridView1.RowCount; i++)
			{
				for (int j = 0; j < this.dataGridView1.ColumnCount; j++)
				{
					this.dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
				}
			}
			this.clearHighlightingToolStripMenuItem.Enabled = false;
		}

		private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				DataGridView.HitTestInfo hitTestInfo = this.dataGridView1.HitTest(e.X, e.Y);
				if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
				{
					this.dataGridView1.Rows[hitTestInfo.RowIndex].Cells[hitTestInfo.ColumnIndex].Selected = true;
					this.dataGridView1.CurrentCell = this.dataGridView1.Rows[hitTestInfo.RowIndex].Cells[hitTestInfo.ColumnIndex];
					if (this.dataGridView1.CurrentCell.ColumnIndex == 0)
					{
						this.copyToolStripMenuItem.Enabled = true;
						this.cutToolStripMenuItem.Enabled = false;
						this.pasteToolStripMenuItem.Enabled = false;
						this.deleteToolStripMenuItem.Enabled = false;
					}
					else
					{
						this.cutToolStripMenuItem.Enabled = true;
						string text = System.Windows.Forms.Clipboard.GetText();
						if (string.IsNullOrEmpty(text))
						{
							this.copyToolStripMenuItem.Enabled = false;
							this.pasteToolStripMenuItem.Enabled = false;
						}
						else
						{
							this.copyToolStripMenuItem.Enabled = true;
							this.pasteToolStripMenuItem.Enabled = true;
						}
						this.deleteToolStripMenuItem.Enabled = true;
						if (this.dataGridView1.CurrentCell.Value == null)
						{
							this.cutToolStripMenuItem.Enabled = false;
							this.copyToolStripMenuItem.Enabled = false;
							this.deleteToolStripMenuItem.Enabled = false;
						}
						if (this.dataGridView1.CurrentCell.ReadOnly)
						{
							this.cutToolStripMenuItem.Enabled = false;
							this.pasteToolStripMenuItem.Enabled = false;
							this.deleteToolStripMenuItem.Enabled = false;
						}
					}
					try
					{
						if (string.IsNullOrEmpty(this.dataGridView1.CurrentRow.Cells[0].Value.ToString()))
						{
							this.cutToolStripMenuItem.Enabled = false;
							this.pasteToolStripMenuItem.Enabled = false;
							this.deleteToolStripMenuItem.Enabled = false;
							this.copyToolStripMenuItem.Enabled = false;
						}
					}
					catch
					{
						this.cutToolStripMenuItem.Enabled = false;
						this.pasteToolStripMenuItem.Enabled = false;
						this.deleteToolStripMenuItem.Enabled = false;
						this.copyToolStripMenuItem.Enabled = false;
					}
					this.contextMenuStrip1.Show(this.dataGridView1, new System.Drawing.Point(e.X, e.Y));
					return;
				}
				if (hitTestInfo.Type == DataGridViewHitTestType.RowHeader)
				{
					try
					{
						this.dataGridView1.Rows[hitTestInfo.RowIndex].Cells[0].Selected = true;
						this.dataGridView1.CurrentCell = this.dataGridView1.Rows[hitTestInfo.RowIndex].Cells[0];
						this.zoomToToolStripMenuItem.Enabled = true;
						this.selectUnselectToolStripMenuItem.Enabled = true;
						this.copySelectedToolStripMenuItem.Enabled = true;
						this.deleteToolStripMenuItem1.Enabled = true;
						if (string.IsNullOrEmpty(this.dataGridView1.CurrentRow.Cells[0].Value.ToString()))
						{
							this.zoomToToolStripMenuItem.Enabled = false;
							this.selectUnselectToolStripMenuItem.Enabled = false;
							this.copySelectedToolStripMenuItem.Enabled = false;
							this.deleteToolStripMenuItem1.Enabled = false;
						}
					}
					catch
					{
						this.zoomToToolStripMenuItem.Enabled = false;
						this.selectUnselectToolStripMenuItem.Enabled = false;
						this.copySelectedToolStripMenuItem.Enabled = false;
						this.deleteToolStripMenuItem1.Enabled = false;
					}
					this.contextMenuStrip2.Show(this.dataGridView1, new System.Drawing.Point(e.X, e.Y));
				}
			}
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Windows.Forms.Clipboard.Clear();
				if (this.dataGridView1.CurrentCell.Value != null)
				{
					string value = this.dataGridView1.CurrentCell.Value.ToString();
					if (!string.IsNullOrEmpty(value))
					{
						System.Windows.Forms.Clipboard.SetText(this.dataGridView1.CurrentCell.Value.ToString());
					}
					string columnName = this.dataGridView1.Columns[this.dataGridView1.CurrentCell.ColumnIndex].Name;
					MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
					CadField cadField = activeFeatureClassOrSubtype.Fields.Find((CadField f) => f.Name == columnName);
					if (cadField != null)
					{
						ObjectId id = new ObjectId((IntPtr)this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value);
						CadField.RemoveCadAttribute(id, cadField);
						string value2 = "";
						try
						{
							value2 = cadField.Value.Value.ToString();
						}
						catch
						{
						}
						this.dataGridView1.CurrentCell.Value = value2;
					}
				}
			}
			catch
			{
			}
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				System.Windows.Forms.Clipboard.Clear();
				System.Windows.Forms.Clipboard.SetText(this.dataGridView1.CurrentCell.Value.ToString());
			}
			catch
			{
			}
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = System.Windows.Forms.Clipboard.GetText();
				string columnName = this.dataGridView1.Columns[this.dataGridView1.CurrentCell.ColumnIndex].Name;
				MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
				CadField cadField = activeFeatureClassOrSubtype.Fields.Find((CadField f) => f.Name == columnName);
				if (cadField != null)
				{
					new ObjectId((IntPtr)this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value);
					if (!CadField.IsValidTypedValue(cadField.FieldType, text))
					{
						this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].ErrorText = AfaStrings.ValueNotValidForType + " (" + columnName + ")";
					}
					else
					{
						this.dataGridView1.CurrentCell.Value = text;
					}
				}
			}
			catch (SystemException)
			{
				this.dataGridView1.CurrentCell.ErrorText = AfaStrings.ValueNotValidForType;
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string columnName = this.dataGridView1.Columns[this.dataGridView1.CurrentCell.ColumnIndex].Name;
			MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
			CadField cadField = activeFeatureClassOrSubtype.Fields.Find((CadField f) => f.Name == columnName);
			if (cadField == null)
			{
				return;
			}
			ObjectId id = new ObjectId((IntPtr)this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value);
			CadField.RemoveCadAttribute(id, cadField);
			string value = "";
			try
			{
				value = cadField.Value.Value.ToString();
			}
			catch
			{
			}
			this.dataGridView1.CurrentCell.Value = value;
		}

		private void zoomToToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (null == Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument)
			{
				return;
			}
			Editor editor = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Editor;
			Document document = AfaDocData.ActiveDocData.Document;
			try
			{
				using (Transaction transaction = document.Database.TransactionManager.StartTransaction())
				{
					List<ObjectId> list = new List<ObjectId>();
					ObjectId objectId = new ObjectId((IntPtr)this.dataGridView1.CurrentRow.Cells[0].Value);
					if (objectId.ObjectClass.DxfName != "GROUP")
					{
						list.Add(objectId);
					}
					else
					{
						Group group = (Group)transaction.GetObject(objectId, 0);
						ObjectId[] allEntityIds = group.GetAllEntityIds();
						ObjectId[] array = allEntityIds;
						for (int i = 0; i < array.Length; i++)
						{
							ObjectId item = array[i];
							list.Add(item);
						}
					}
					transaction.Commit();
					System.Windows.Forms.Application.UseWaitCursor = true;
					if (list.Count > 0)
					{
						editor.SetImpliedSelection(DocUtil.ExpandGroupObjectIds(AfaDocData.ActiveDocData.Document, list.ToArray()));
						Utils.ZoomObjects(false);
					}
					System.Windows.Forms.Application.UseWaitCursor = false;
					DocUtil.UpdateView(editor);
					editor.Regen();
				}
				AfaDocData.ActiveDocData.DocDataset.UpdateMaps();
			}
			catch
			{
			}
		}

		private void selectUnselectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.CurrentRow.Selected)
			{
				this.dataGridView1.CurrentRow.Selected = false;
				return;
			}
			this.dataGridView1.CurrentRow.Selected = true;
		}

		private void copySelectedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = "";
			if (this.dataGridView1.CurrentRow.Index != this.dataGridView1.Rows.Count)
			{
				for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
				{
					text = text + this.dataGridView1.CurrentRow.Cells[i].Value.ToString() + " ";
				}
				System.Windows.Forms.Clipboard.SetText(text);
			}
		}

		private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (null == Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument)
			{
				return;
			}
			Editor arg_22_0 = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Editor;
			try
			{
				this.DeleteRows();
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
		}

		private void showAllRecordsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.moveToBeginningOfTableToolStripMenuItem.Enabled = true;
			this.moveToTheEndOfTableToolStripMenuItem.Enabled = true;
			this.moveToTheNextRecordToolStripMenuItem.Enabled = true;
			this.moveToThePreviousRecordToolStripMenuItem.Enabled = true;
			try
			{
				for (int i = 0; i < this.dataGridView1.RowCount; i++)
				{
					this.dataGridView1.Rows[i].Visible = true;
				}
			}
			catch
			{
			}
		}

		private void showSelectedRecordsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.moveToBeginningOfTableToolStripMenuItem.Enabled = false;
			this.moveToTheEndOfTableToolStripMenuItem.Enabled = false;
			this.moveToTheNextRecordToolStripMenuItem.Enabled = false;
			this.moveToThePreviousRecordToolStripMenuItem.Enabled = false;
			try
			{
				for (int i = 0; i < this.dataGridView1.RowCount; i++)
				{
					if (!this.dataGridView1.Rows[i].Selected)
					{
						this.dataGridView1.Rows[i].Visible = false;
					}
				}
			}
			catch
			{
			}
		}

		private void PopulateFeatureClassCombo(MSCFeatureClass currentFC, MSCFeatureClass currentSubtype)
		{
			List<FCTag> list = new List<FCTag>();
			FCTag selectedItem = null;
			foreach (MSCFeatureClass current in AfaDocData.ActiveDocData.DocDataset.FeatureClasses.Values)
			{
				FCTag fCTag = new FCTag(current);
				list.Add(fCTag);
				if (current == currentFC)
				{
					selectedItem = fCTag;
				}
			}
			foreach (MSCFeatureService current2 in AfaDocData.ActiveDocData.DocDataset.FeatureServices.Values)
			{
				FCTag fCTag2 = new FCTag(current2);
				if (current2 == currentFC)
				{
					selectedItem = fCTag2;
				}
				list.Add(fCTag2);
			}
			this.featureClassListComboBox1.ComboBox.DataSource = new BindingSource(list, null);
			if (currentFC != null)
			{
				this.featureClassListComboBox1.ComboBox.SelectedItem = selectedItem;
				this.RepopulateSubtypeCombo(currentFC, currentSubtype);
			}
		}

		private void featureClassListComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.Initializing)
			{
				return;
			}
			this.stopWatchers();
			this.stopWatchers();
			FCTag fCTag = (FCTag)this.featureClassListComboBox1.SelectedItem;
			MSCFeatureClass featureClass = fCTag.GetFeatureClass(AfaDocData.ActiveDocData.DocDataset);
			if (AfaDocData.ActiveDocData.GetTopActiveFeatureClass() == featureClass)
			{
				return;
			}
			this.RepopulateSubtypeCombo(featureClass, null);
			AfaDocData.ActiveDocData.SetActiveFeatureClass(featureClass);
			ArcGISRibbon.SetActiveFeatureClass(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
			this.dataGridView1.DataSource = null;
			this.fillRows(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
			this.startWatchers();
		}

		private void subtypeListComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.Initializing)
			{
				return;
			}
			this.stopWatchers();
			this.stopWatchers();
			MSCFeatureClass mSCFeatureClass = AfaDocData.ActiveDocData.GetTopActiveFeatureClass();
			FCTag fCTag = (FCTag)this.subtypeListComboBox.SelectedItem;
			if (fCTag.fcName != "All Types")
			{
				mSCFeatureClass = fCTag.GetFeatureClass(AfaDocData.ActiveDocData.DocDataset);
			}
			if (AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype() == mSCFeatureClass)
			{
				return;
			}
			AfaDocData.ActiveDocData.SetActiveFeatureClass(mSCFeatureClass);
			ArcGISRibbon.SetActiveFeatureClass(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
			this.dataGridView1.DataSource = null;
			this.fillRows(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
			this.startWatchers();
		}

		private void RepopulateSubtypeCombo(MSCFeatureClass currentFC, MSCFeatureClass currentSubtype)
		{
			List<FCTag> list = new List<FCTag>();
			FCTag fCTag = null;
			if (currentFC.SubTypes.Count > 0)
			{
				this.subtypeListComboBox.ComboBox.Enabled = true;
				FCTag fCTag2 = new FCTag(currentFC);
				fCTag2.fcName = "All Types";
				list.Add(fCTag2);
				fCTag = fCTag2;
				foreach (MSCFeatureClassSubType current in currentFC.SubTypes)
				{
					FCTag fCTag3 = new FCTag(current);
					if (current == currentSubtype)
					{
						fCTag = fCTag3;
					}
					list.Add(fCTag3);
				}
			}
			this.subtypeListComboBox.ComboBox.DataSource = new BindingSource(list, null);
			if (fCTag != null)
			{
				this.subtypeListComboBox.ComboBox.SelectedItem = fCTag;
			}
			if (list.Count == 0)
			{
				this.subtypeListComboBox.Enabled = false;
			}
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.stopWatchers();
			this.dataGridView1.Columns.Clear();
			this.dataGridView1.Rows.Clear();
			this.featureClassListComboBox1.SelectedItem.ToString();
			this.fillRows(AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
		}

		private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.dataGridView1.SelectedCells.Count > 0)
			{
				if (e.KeyCode == Keys.F2)
				{
					int columnIndex = this.dataGridView1.SelectedCells[0].ColumnIndex;
					if (columnIndex == 0)
					{
						return;
					}
					for (int i = 1; i < this.dataGridView1.SelectedCells.Count; i++)
					{
						if (columnIndex != this.dataGridView1.SelectedCells[i].ColumnIndex)
						{
							return;
						}
					}
					this.dataGridView1.BeginEdit(false);
					ValueBox valueBox = new ValueBox();
					if (valueBox.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(valueBox.textBoxContents))
					{
						for (int j = 0; j < this.dataGridView1.SelectedCells.Count; j++)
						{
							this.dataGridView1.SelectedCells[j].Value = valueBox.textBoxContents;
						}
						return;
					}
				}
				else if (e.KeyCode == Keys.Delete)
				{
					if (this.dataGridView1.SelectedRows.Count != 0)
					{
						try
						{
							this.DeleteRows();
							return;
						}
						catch (Exception ex)
						{
							System.Windows.Forms.MessageBox.Show(ex.Message);
							return;
						}
					}
					for (int k = 0; k < this.dataGridView1.SelectedCells.Count; k++)
					{
						this.dataGridView1.SelectedCells[k].Value = null;
					}
				}
			}
		}

		private void DeleteRows()
		{
			if (null == Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument)
			{
				return;
			}
			List<ObjectId> list = new List<ObjectId>();
			List<DataRowView> list2 = new List<DataRowView>();
			foreach (DataGridViewRow dataGridViewRow in this.dataGridView1.SelectedRows)
			{
				DataRowView dataRowView = (DataRowView)dataGridViewRow.DataBoundItem;
				ObjectId item = new ObjectId((IntPtr)dataRowView.Row.ItemArray[0]);
				list.Add(item);
				list2.Add(dataRowView);
			}
			if (list.Count > 0)
			{
				ObjectId[] array = list.ToArray();
				if (array != null)
				{
					DocUtil.EraseEntities(AfaDocData.ActiveDocData.Document, array);
				}
				DocUtil.EraseGroupEntities(AfaDocData.ActiveDocData.Document, array);
			}
			foreach (DataRowView current in list2)
			{
				current.Row.Delete();
			}
			Editor editor = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Editor;
			editor.UpdateScreen();
		}

		private void selectByAttributesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (null == Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument)
			{
				return;
			}
			base.Hide();
			Editor editor = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Editor;
			SelectByAttributesForm selectByAttributesForm = new SelectByAttributesForm(false, AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype());
			if (selectByAttributesForm != null)
			{
				Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(Autodesk.AutoCAD.ApplicationServices.Core.Application.MainWindow.Handle, selectByAttributesForm, true);
			}
			try
			{
				PromptSelectionResult promptSelectionResult = editor.SelectImplied();
				try
				{
					if (promptSelectionResult.Status == (PromptStatus)5100)
					{
						SelectionSet value = promptSelectionResult.Value;
						ObjectId[] objectIds = value.GetObjectIds();
						if (objectIds != null)
						{
							if (objectIds.Length > 0)
							{
								for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
								{
									ObjectId objectId = new ObjectId((IntPtr)this.dataGridView1.Rows[i].Cells[0].Value);
									for (int j = 0; j < objectIds.Length; j++)
									{
										if (objectIds[j] == objectId)
										{
											this.dataGridView1.Rows[i].Selected = true;
										}
									}
								}
							}
							else
							{
								editor.WriteMessage(AfaStrings.NoFeaturesFound);
							}
						}
						else
						{
							editor.WriteMessage(AfaStrings.NoFeaturesFound);
						}
					}
					else
					{
						editor.WriteMessage(AfaStrings.NoFeaturesFound);
					}
				}
				catch (Exception ex)
				{
					System.Windows.MessageBox.Show(ex.Message);
				}
			}
			catch
			{
			}
			finally
			{
				base.Show();
			}
		}

		private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.ThrowException = false;
		}

		private void featureClassListComboBox1_DropDown(object sender, EventArgs e)
		{
			this.AdjustWidthComboBox_DropDown(sender, e);
		}

		private void subtypeListComboBox_DropDown(object sender, EventArgs e)
		{
			this.AdjustWidthComboBox_DropDown(sender, e);
		}

		private void AdjustWidthComboBox_DropDown(object sender, EventArgs e)
		{
			ToolStripComboBox toolStripComboBox = (ToolStripComboBox)sender;
			int num = toolStripComboBox.DropDownWidth;
			Graphics graphics = toolStripComboBox.ComboBox.CreateGraphics();
            System.Drawing.Font font = toolStripComboBox.Font;
			int num2 = (toolStripComboBox.Items.Count > toolStripComboBox.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;
			foreach (FCTag fCTag in toolStripComboBox.ComboBox.Items)
			{
				int num3 = (int)graphics.MeasureString(fCTag.fcName, font).Width + num2;
				if (num < num3)
				{
					num = num3;
				}
			}
			toolStripComboBox.DropDownWidth = num;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TableView));
			this.menuStrip1 = new MenuStrip();
			this.toolStripMenuItem1 = new ToolStripMenuItem();
			this.findReplaceToolStripMenuItem = new ToolStripMenuItem();
			this.selectByAttributesToolStripMenuItem = new ToolStripMenuItem();
			this.switchSelectionToolStripMenuItem = new ToolStripMenuItem();
			this.selectAllToolStripMenuItem = new ToolStripMenuItem();
			this.clearSelectionToolStripMenuItem = new ToolStripMenuItem();
			this.zoomToolStripMenuItem = new ToolStripMenuItem();
			this.clearHighlightingToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.toolStripTextBox1 = new TableView.NumericTextBox();
			this.featureClassToolStripMenuItem = new ToolStripMenuItem();
			this.featureClassListComboBox1 = new ToolStripComboBox();
			this.subtypeToolStripMenuItem = new ToolStripMenuItem();
			this.subtypeListComboBox = new ToolStripComboBox();
			this.menuStrip2 = new MenuStrip();
			this.moveToBeginningOfTableToolStripMenuItem = new ToolStripMenuItem();
			this.moveToThePreviousRecordToolStripMenuItem = new ToolStripMenuItem();
			this.moveToTheNextRecordToolStripMenuItem = new ToolStripMenuItem();
			this.moveToTheEndOfTableToolStripMenuItem = new ToolStripMenuItem();
			this.t2 = new ToolStripSeparator();
			this.showAllRecordsToolStripMenuItem = new ToolStripMenuItem();
			this.showSelectedRecordsToolStripMenuItem = new ToolStripMenuItem();
			this.t3 = new ToolStripSeparator();
			this.toolStriplabel = new ToolStripTextBox();
			this.dataGridView1 = new DataGridView();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.cutToolStripMenuItem = new ToolStripMenuItem();
			this.copyToolStripMenuItem = new ToolStripMenuItem();
			this.pasteToolStripMenuItem = new ToolStripMenuItem();
			this.deleteToolStripMenuItem = new ToolStripMenuItem();
			this.contextMenuStrip2 = new ContextMenuStrip(this.components);
			this.zoomToToolStripMenuItem = new ToolStripMenuItem();
			this.selectUnselectToolStripMenuItem = new ToolStripMenuItem();
			this.copySelectedToolStripMenuItem = new ToolStripMenuItem();
			this.deleteToolStripMenuItem1 = new ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.menuStrip2.SuspendLayout();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			base.SuspendLayout();
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripMenuItem1,
				this.clearSelectionToolStripMenuItem,
				this.zoomToolStripMenuItem,
				this.clearHighlightingToolStripMenuItem,
				this.toolStripSeparator1,
				this.featureClassToolStripMenuItem,
				this.featureClassListComboBox1,
				this.subtypeToolStripMenuItem,
				this.subtypeListComboBox
			});
			componentResourceManager.ApplyResources(this.menuStrip1, "menuStrip1");
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.ShowItemToolTips = true;
			this.toolStripMenuItem1.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.findReplaceToolStripMenuItem,
				this.selectByAttributesToolStripMenuItem,
				this.switchSelectionToolStripMenuItem,
				this.selectAllToolStripMenuItem
			});
			this.toolStripMenuItem1.Image = ImageResources.TableOpen16;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			componentResourceManager.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			this.findReplaceToolStripMenuItem.Image = ImageResources.Find16;
			this.findReplaceToolStripMenuItem.Name = "findReplaceToolStripMenuItem";
			componentResourceManager.ApplyResources(this.findReplaceToolStripMenuItem, "findReplaceToolStripMenuItem");
			this.findReplaceToolStripMenuItem.Click += new EventHandler(this.findReplaceToolStripMenuItem_Click);
			this.selectByAttributesToolStripMenuItem.Image = ImageResources.SelectbyAttributes16;
			this.selectByAttributesToolStripMenuItem.Name = "selectByAttributesToolStripMenuItem";
			componentResourceManager.ApplyResources(this.selectByAttributesToolStripMenuItem, "selectByAttributesToolStripMenuItem");
			this.selectByAttributesToolStripMenuItem.Click += new EventHandler(this.selectByAttributesToolStripMenuItem_Click);
			this.switchSelectionToolStripMenuItem.Image = ImageResources.SwitchSelection16;
			this.switchSelectionToolStripMenuItem.Name = "switchSelectionToolStripMenuItem";
			componentResourceManager.ApplyResources(this.switchSelectionToolStripMenuItem, "switchSelectionToolStripMenuItem");
			this.switchSelectionToolStripMenuItem.Click += new EventHandler(this.switchSelectionToolStripMenuItem_Click);
			this.selectAllToolStripMenuItem.Image = ImageResources.SelectAll16;
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			componentResourceManager.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
			this.selectAllToolStripMenuItem.Click += new EventHandler(this.selectAllToolStripMenuItem_Click);
			this.clearSelectionToolStripMenuItem.AutoToolTip = true;
			this.clearSelectionToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.clearSelectionToolStripMenuItem.Image = ImageResources.SelectionClearSelected16;
			componentResourceManager.ApplyResources(this.clearSelectionToolStripMenuItem, "clearSelectionToolStripMenuItem");
			this.clearSelectionToolStripMenuItem.Name = "clearSelectionToolStripMenuItem";
			this.clearSelectionToolStripMenuItem.Click += new EventHandler(this.clearSelectionToolStripMenuItem_Click);
			this.zoomToolStripMenuItem.AutoToolTip = true;
			this.zoomToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			componentResourceManager.ApplyResources(this.zoomToolStripMenuItem, "zoomToolStripMenuItem");
			this.zoomToolStripMenuItem.Image = ImageResources.ZoomToSelection16;
			this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
			this.zoomToolStripMenuItem.ToolTipText = AfaStrings.ZoomToSelectedFeatures;
			this.zoomToolStripMenuItem.Click += new EventHandler(this.zoomToolStripMenuItem_Click);
			this.clearHighlightingToolStripMenuItem.AutoToolTip = true;
			this.clearHighlightingToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.clearHighlightingToolStripMenuItem.Image = ImageResources.GenericEraser16;
			this.clearHighlightingToolStripMenuItem.Name = "clearHighlightingToolStripMenuItem";
			componentResourceManager.ApplyResources(this.clearHighlightingToolStripMenuItem, "clearHighlightingToolStripMenuItem");
			this.clearHighlightingToolStripMenuItem.Click += new EventHandler(this.clearHighlightingToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			componentResourceManager.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			this.featureClassToolStripMenuItem.Name = "featureClassToolStripMenuItem";
			componentResourceManager.ApplyResources(this.featureClassToolStripMenuItem, "featureClassToolStripMenuItem");
			this.featureClassListComboBox1.AutoToolTip = true;
			this.featureClassListComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
			this.featureClassListComboBox1.DropDownWidth = 300;
			this.featureClassListComboBox1.Name = "featureClassListComboBox1";
			componentResourceManager.ApplyResources(this.featureClassListComboBox1, "featureClassListComboBox1");
			this.featureClassListComboBox1.DropDown += new EventHandler(this.featureClassListComboBox1_DropDown);
			this.featureClassListComboBox1.SelectedIndexChanged += new EventHandler(this.featureClassListComboBox1_SelectedIndexChanged);
			this.subtypeToolStripMenuItem.Name = "subtypeToolStripMenuItem";
			componentResourceManager.ApplyResources(this.subtypeToolStripMenuItem, "subtypeToolStripMenuItem");
			this.subtypeListComboBox.Name = "subtypeListComboBox";
			componentResourceManager.ApplyResources(this.subtypeListComboBox, "subtypeListComboBox");
			this.subtypeListComboBox.DropDown += new EventHandler(this.subtypeListComboBox_DropDown);
			this.subtypeListComboBox.SelectedIndexChanged += new EventHandler(this.subtypeListComboBox_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.menuStrip2, "menuStrip2");
			this.menuStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.moveToBeginningOfTableToolStripMenuItem,
				this.moveToThePreviousRecordToolStripMenuItem,
				this.moveToTheNextRecordToolStripMenuItem,
				this.moveToTheEndOfTableToolStripMenuItem,
				this.t2,
				this.showAllRecordsToolStripMenuItem,
				this.showSelectedRecordsToolStripMenuItem,
				this.t3,
				this.toolStriplabel
			});
			this.menuStrip2.Name = "menuStrip2";
			this.menuStrip2.ShowItemToolTips = true;
			this.moveToBeginningOfTableToolStripMenuItem.AutoToolTip = true;
			this.moveToBeginningOfTableToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.moveToBeginningOfTableToolStripMenuItem.Image = ImageResources.GenericBlackArrowStart16;
			this.moveToBeginningOfTableToolStripMenuItem.Name = "moveToBeginningOfTableToolStripMenuItem";
			componentResourceManager.ApplyResources(this.moveToBeginningOfTableToolStripMenuItem, "moveToBeginningOfTableToolStripMenuItem");
			this.moveToBeginningOfTableToolStripMenuItem.Click += new EventHandler(this.moveToBeginningOfTableToolStripMenuItem_Click);
			this.moveToThePreviousRecordToolStripMenuItem.AutoToolTip = true;
			this.moveToThePreviousRecordToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.moveToThePreviousRecordToolStripMenuItem.Image = ImageResources.GenericBlackArrowLeft16;
			this.moveToThePreviousRecordToolStripMenuItem.Name = "moveToThePreviousRecordToolStripMenuItem";
			componentResourceManager.ApplyResources(this.moveToThePreviousRecordToolStripMenuItem, "moveToThePreviousRecordToolStripMenuItem");
			this.moveToThePreviousRecordToolStripMenuItem.Click += new EventHandler(this.moveToThePreviousRecordToolStripMenuItem_Click);
			this.moveToTheNextRecordToolStripMenuItem.AutoToolTip = true;
			this.moveToTheNextRecordToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.moveToTheNextRecordToolStripMenuItem.Image = ImageResources.GenericBlackArrowRight16;
			this.moveToTheNextRecordToolStripMenuItem.Name = "moveToTheNextRecordToolStripMenuItem";
			componentResourceManager.ApplyResources(this.moveToTheNextRecordToolStripMenuItem, "moveToTheNextRecordToolStripMenuItem");
			this.moveToTheNextRecordToolStripMenuItem.Click += new EventHandler(this.moveToTheNextRecordToolStripMenuItem_Click);
			this.moveToTheEndOfTableToolStripMenuItem.AutoToolTip = true;
			this.moveToTheEndOfTableToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.moveToTheEndOfTableToolStripMenuItem.Image = ImageResources.GenericBlackArrowEnd16;
			this.moveToTheEndOfTableToolStripMenuItem.Name = "moveToTheEndOfTableToolStripMenuItem";
			componentResourceManager.ApplyResources(this.moveToTheEndOfTableToolStripMenuItem, "moveToTheEndOfTableToolStripMenuItem");
			this.moveToTheEndOfTableToolStripMenuItem.Click += new EventHandler(this.moveToTheEndOfTableToolStripMenuItem_Click);
			this.t2.Name = "t2";
			componentResourceManager.ApplyResources(this.t2, "t2");
			this.showAllRecordsToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.showAllRecordsToolStripMenuItem.Image = ImageResources.TableShowAllRecords16;
			this.showAllRecordsToolStripMenuItem.Name = "showAllRecordsToolStripMenuItem";
			componentResourceManager.ApplyResources(this.showAllRecordsToolStripMenuItem, "showAllRecordsToolStripMenuItem");
			this.showAllRecordsToolStripMenuItem.ToolTipText = AfaStrings.ShowAll;
			this.showAllRecordsToolStripMenuItem.Click += new EventHandler(this.showAllRecordsToolStripMenuItem_Click);
			this.showSelectedRecordsToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.showSelectedRecordsToolStripMenuItem.Image = ImageResources.TableShowSelectedRecords16;
			this.showSelectedRecordsToolStripMenuItem.Name = "showSelectedRecordsToolStripMenuItem";
			componentResourceManager.ApplyResources(this.showSelectedRecordsToolStripMenuItem, "showSelectedRecordsToolStripMenuItem");
			this.showSelectedRecordsToolStripMenuItem.ToolTipText = AfaStrings.ShowSelected;
			this.showSelectedRecordsToolStripMenuItem.Click += new EventHandler(this.showSelectedRecordsToolStripMenuItem_Click);
			this.t3.Name = "t3";
			componentResourceManager.ApplyResources(this.t3, "t3");
			this.toolStriplabel.BackColor = System.Drawing.SystemColors.Control;
			this.toolStriplabel.Name = "toolStriplabel";
			this.toolStriplabel.ReadOnly = true;
			componentResourceManager.ApplyResources(this.toolStriplabel, "toolStriplabel");
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView1.BorderStyle = BorderStyle.None;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			componentResourceManager.ApplyResources(this.dataGridView1, "dataGridView1");
			this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
			this.dataGridView1.MinimumSize = new System.Drawing.Size(20, 20);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
			this.dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
			this.dataGridView1.CurrentCellDirtyStateChanged += new EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
			this.dataGridView1.DataError += new DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.cutToolStripMenuItem,
				this.copyToolStripMenuItem,
				this.pasteToolStripMenuItem,
				this.deleteToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.cutToolStripMenuItem.Image = ImageResources.EditCut16;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			componentResourceManager.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
			this.cutToolStripMenuItem.Click += new EventHandler(this.cutToolStripMenuItem_Click);
			this.copyToolStripMenuItem.Image = ImageResources.EditCopy16;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			componentResourceManager.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
			this.copyToolStripMenuItem.Click += new EventHandler(this.copyToolStripMenuItem_Click);
			this.pasteToolStripMenuItem.Image = ImageResources.EditPaste16;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			componentResourceManager.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
			this.pasteToolStripMenuItem.Click += new EventHandler(this.pasteToolStripMenuItem_Click);
			this.deleteToolStripMenuItem.Image = ImageResources.GenericDeleteRed16;
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			componentResourceManager.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
			this.deleteToolStripMenuItem.Click += new EventHandler(this.deleteToolStripMenuItem_Click);
			this.contextMenuStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.zoomToToolStripMenuItem,
				this.selectUnselectToolStripMenuItem,
				this.copySelectedToolStripMenuItem,
				this.deleteToolStripMenuItem1
			});
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			componentResourceManager.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
			this.zoomToToolStripMenuItem.Image = ImageResources.ZoomInTool16;
			this.zoomToToolStripMenuItem.Name = "zoomToToolStripMenuItem";
			componentResourceManager.ApplyResources(this.zoomToToolStripMenuItem, "zoomToToolStripMenuItem");
			this.zoomToToolStripMenuItem.Click += new EventHandler(this.zoomToToolStripMenuItem_Click);
			this.selectUnselectToolStripMenuItem.Image = ImageResources.SelectionSelectUnselect16;
			this.selectUnselectToolStripMenuItem.Name = "selectUnselectToolStripMenuItem";
			componentResourceManager.ApplyResources(this.selectUnselectToolStripMenuItem, "selectUnselectToolStripMenuItem");
			this.selectUnselectToolStripMenuItem.Click += new EventHandler(this.selectUnselectToolStripMenuItem_Click);
			this.copySelectedToolStripMenuItem.Image = ImageResources.SelectionCopySelectedRecords16;
			this.copySelectedToolStripMenuItem.Name = "copySelectedToolStripMenuItem";
			componentResourceManager.ApplyResources(this.copySelectedToolStripMenuItem, "copySelectedToolStripMenuItem");
			this.copySelectedToolStripMenuItem.Click += new EventHandler(this.copySelectedToolStripMenuItem_Click);
			this.deleteToolStripMenuItem1.Image = ImageResources.GenericDeleteRed16;
			this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
			componentResourceManager.ApplyResources(this.deleteToolStripMenuItem1, "deleteToolStripMenuItem1");
			this.deleteToolStripMenuItem1.Click += new EventHandler(this.deleteToolStripMenuItem1_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.dataGridView1);
			base.Controls.Add(this.menuStrip1);
			base.Controls.Add(this.menuStrip2);
			base.MainMenuStrip = this.menuStrip1;
			base.MinimizeBox = false;
			base.Name = "TableView";
			base.FormClosing += new FormClosingEventHandler(this.TableView_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.menuStrip2.ResumeLayout(false);
			this.menuStrip2.PerformLayout();
			((ISupportInitialize)this.dataGridView1).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.contextMenuStrip2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void updateUI()
		{
			try
			{
				this.Text = AfaStrings.Table;
				this.findReplaceToolStripMenuItem.Text = AfaStrings.FindAndReplace;
				this.selectAllToolStripMenuItem.Text = AfaStrings.SelectAll;
				this.switchSelectionToolStripMenuItem.Text = AfaStrings.SwitchSelection;
				this.selectByAttributesToolStripMenuItem.Text = AfaStrings.SelectByAttributes;
				this.clearHighlightingToolStripMenuItem.Text = AfaStrings.ClearHighlighting;
				this.clearHighlightingToolStripMenuItem.ToolTipText = AfaStrings.ClearHighlighting;
				this.zoomToolStripMenuItem.Text = AfaStrings.ZoomToSelectedFeatures;
				this.zoomToolStripMenuItem.ToolTipText = AfaStrings.ZoomToSelectedFeatures;
				this.clearHighlightingToolStripMenuItem.Text = AfaStrings.ClearHighlighting;
				this.clearHighlightingToolStripMenuItem.ToolTipText = AfaStrings.ClearHighlighting;
				this.featureClassToolStripMenuItem.Text = AfaStrings.FeatureClass;
				this.cutToolStripMenuItem.Text = AfaStrings.Cut;
				this.copyToolStripMenuItem.Text = AfaStrings.Copy;
				this.pasteToolStripMenuItem.Text = AfaStrings.Paste;
				this.deleteToolStripMenuItem.Text = AfaStrings.Delete;
				this.zoomToToolStripMenuItem.Text = AfaStrings.ZoomToSelectedFeatures;
				this.selectUnselectToolStripMenuItem.Text = AfaStrings.SelectUn;
				this.copySelectedToolStripMenuItem.Text = AfaStrings.CopySelected;
				this.deleteToolStripMenuItem1.Text = AfaStrings.Delete;
				this.moveToBeginningOfTableToolStripMenuItem.Text = AfaStrings.MoveBeginning;
				this.moveToThePreviousRecordToolStripMenuItem.Text = AfaStrings.MovePrevious;
				this.moveToTheNextRecordToolStripMenuItem.Text = AfaStrings.MoveNext;
				this.moveToTheEndOfTableToolStripMenuItem.Text = AfaStrings.MoveEnd;
				this.moveToBeginningOfTableToolStripMenuItem.ToolTipText = AfaStrings.MoveBeginning;
				this.moveToThePreviousRecordToolStripMenuItem.ToolTipText = AfaStrings.MovePrevious;
				this.moveToTheNextRecordToolStripMenuItem.ToolTipText = AfaStrings.MoveNext;
				this.moveToTheEndOfTableToolStripMenuItem.ToolTipText = AfaStrings.MoveEnd;
				this.showAllRecordsToolStripMenuItem.Text = AfaStrings.ShowAll;
				this.showAllRecordsToolStripMenuItem.ToolTipText = AfaStrings.ShowAll;
				this.showSelectedRecordsToolStripMenuItem.Text = AfaStrings.ShowSelected;
				this.showSelectedRecordsToolStripMenuItem.ToolTipText = AfaStrings.ShowSelected;
			}
			catch
			{
			}
		}
	}
}
