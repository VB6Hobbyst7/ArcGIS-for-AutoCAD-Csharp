using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using DataTable = System.Data.DataTable;

namespace AFA
{
    public class FeatureClassProperties : System.Windows.Window, IComponentConnector, IStyleConnector
	{
		private System.Data.DataTable _QueryData;

		private System.Data.DataTable _FieldData;

		private MSCFeatureClass _FC;

		private List<string> _mColorList;

		private static bool isUpdating;

		internal FeatureClassProperties This;

		internal System.Windows.Controls.Label lblName;

		internal System.Windows.Controls.Label lblType;

		internal System.Windows.Controls.Label lblProperty;

		internal System.Windows.Controls.ComboBox cbProperty;

		internal System.Windows.Controls.Label lblValue;

		internal System.Windows.Controls.ComboBox cbValue;

		internal System.Windows.Controls.CheckBox cbAppend;

		internal System.Windows.Controls.DataGrid dgQuery;

		internal System.Windows.Controls.Button btnPreview;

		internal System.Windows.Controls.DataGrid dgFields;

		private bool _contentLoaded;

		public DataTable QueryData
		{
			get
			{
				return this._QueryData;
			}
		}

		public DataTable FieldData
		{
			get
			{
				return this._FieldData;
			}
		}

		public Dictionary<string, int> FieldTypeChoices
		{
			get
			{
				return CadField.CadFieldStrings;
			}
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
		}

		public FeatureClassProperties(MSCFeatureService fs)
		{
			this._mColorList = this.InitColorList();
		}

		public FeatureClassProperties(MSCFeatureClass fc)
		{
			try
			{
				this._mColorList = this.InitColorList();
				this._FC = fc;
				this._QueryData = new DataTable();
				this._QueryData.Columns.Add("Type");
				this._QueryData.Columns.Add("Value");
				if (fc.Query != null)
				{
					TypedValue[] array = fc.Query.AsArray();
					TypedValue[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						TypedValue typedValue = array2[i];
						if (typedValue.TypeCode == 62)
						{
							this.BuildColorRow(typedValue.Value.ToString());
						}
						else
						{
							DataRow dataRow = this._QueryData.NewRow();
							dataRow["Type"] = new DXFCode((int)typedValue.TypeCode).CodeString;
							dataRow["Value"] = typedValue.Value;
							this._QueryData.Rows.Add(dataRow);
						}
					}
				}
				this._QueryData.RowChanged += new DataRowChangeEventHandler(this._QueryData_RowChanged);
				this._FieldData = new DataTable();
				this._FieldData.Columns.Add("Name");
				this._FieldData.Columns.Add("Type");
				this._FieldData.Columns.Add("Value");
				this._FieldData.Columns.Add("DisplayValue");
				this._FieldData.Columns.Add("Length", typeof(int));
				this._FieldData.Columns.Add("ReadOnly", typeof(bool));
				this._FieldData.Columns.Add("BaseField", typeof(CadField));
				this._FieldData.Columns.Add("CodedValues", typeof(List<MSCCodedValue>));
				this._FieldData.Columns.Add("CodedValue", typeof(MSCCodedValue));
				this._FieldData.Columns.Add("Domain", typeof(FieldDomain));
				this._FieldData.Columns.Add("CanEditType", typeof(bool));
				if (fc.Fields != null)
				{
					foreach (CadField current in fc.Fields)
					{
						if (current != null && current.Visible)
						{
							DataRow dataRow2 = this._FieldData.NewRow();
							dataRow2["Name"] = current.Name;
							dataRow2["Value"] = current.Value.Value;
							dataRow2["DisplayValue"] = dataRow2["Value"];
							if (current.Value.TypeCode == 1)
							{
								dataRow2["Length"] = current.Length;
							}
							else
							{
								dataRow2["Length"] = 0;
							}
							dataRow2["Type"] = current.GetTypeString();
							if (current.ReadOnly)
							{
								dataRow2["ReadOnly"] = true;
							}
							else
							{
								dataRow2["ReadOnly"] = false;
							}
							dataRow2["BaseField"] = current;
							this._FieldData.Rows.Add(dataRow2);
							if (current.Domain != null)
							{
								dataRow2["Domain"] = current.Domain;
							}
							dataRow2["CanEditType"] = true;
							if (current.Domain != null)
							{
								dataRow2["CanEditType"] = false;
								current.Value = current.Domain.CheckValue(current.Value.Value);
								dataRow2["CodedValues"] = current.Domain.CodedValuesDisplayList;
								dataRow2["CodedValue"] = current.Domain.GetCodedValue(current.Value.Value);
								dataRow2["DisplayValue"] = dataRow2["CodedValue"];
							}
						}
					}
				}
				this._FieldData.RowChanged += new DataRowChangeEventHandler(this._FieldData_RowChanged);
				this.InitializeComponent();
				this.lblName.Content = fc.Name;
				this.lblType.Content = MSCFeatureClass.GetTypeCodeString(fc.GeometryType);
				this.dgQuery.ItemsSource = this.QueryData.DefaultView;
				this.BuildQueryPropertyOptions(fc.GeometryType);
				this.dgFields.ItemsSource = this._FieldData.DefaultView;
				if (fc.ReadOnly || fc.IsSubType || fc.SubTypes.Count > 0)
				{
					this.dgQuery.IsEnabled = false;
					this.dgFields.IsEnabled = false;
					this.cbProperty.IsEnabled = false;
					this.cbAppend.IsEnabled = false;
					this.cbValue.IsEnabled = false;
				}
			}
			catch (Exception ex)
			{
				ErrorReport.ShowErrorMessage(ex.Message);
			}
		}

		private void BuildQueryPropertyOptions(MSCFeatureClass.fcTypeCode typeCode)
		{
			this.cbProperty.Items.Clear();
			this.cbProperty.Items.Add(AfaStrings.Layer);
			this.cbProperty.Items.Add(AfaStrings.Color);
			this.cbProperty.Items.Add(AfaStrings.Linetype);
			this.cbProperty.Items.Add(AfaStrings.Lineweight);
			if (typeCode == MSCFeatureClass.fcTypeCode.fcTypeAnnotation)
			{
				this.cbProperty.Items.Add(AfaStrings.TextStyleName);
			}
			if (typeCode == MSCFeatureClass.fcTypeCode.fcTypePoint)
			{
				this.cbProperty.Items.Add(AfaStrings.BlockName);
			}
		}

		private void _QueryData_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			try
			{
				if ((e.Action == DataRowAction.Add || e.Action == DataRowAction.Change) && e.Row["Type"].ToString() == "Color" && !FeatureClassProperties.isUpdating && e.Row["Value"].GetType() != typeof(short))
				{
					short num = DXFCode.TranslateColorString(e.Row["Value"].ToString());
					FeatureClassProperties.isUpdating = true;
					e.Row["Value"] = num;
					FeatureClassProperties.isUpdating = false;
				}
			}
			catch (Exception)
			{
			}
		}

		private void _FieldData_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			try
			{
				if ((e.Action == DataRowAction.Add || e.Action == DataRowAction.Change) && e.Row["Type"].ToString() == "String")
				{
					string s = e.Row["Length"].ToString();
					short num;
					if (!short.TryParse(s, out num))
					{
						e.Row["Length"] = "255";
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void OnClickCancel(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private ResultBuffer BuildQueryResultBuffer()
		{
			ResultBuffer resultBuffer = new ResultBuffer();
			foreach (DataRow dataRow in this._QueryData.Rows)
			{
				DXFCode dXFCode = new DXFCode(dataRow["Type"].ToString());
				object obj = dataRow["Value"];
				if (dXFCode.Code == 62)
				{
					obj = DXFCode.TranslateColorString(obj.ToString());
				}
				if (DXFCode.IsValidTypedValue(dXFCode.Code, obj.ToString()))
				{
					TypedValue typedValue = DXFCode.CreateTypedValue(dXFCode.Code, obj.ToString());
					resultBuffer.Add(typedValue);
				}
			}
			return resultBuffer;
		}

		private void OnClickOK(object sender, RoutedEventArgs e)
		{
			if (null == Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument)
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorWritingFeatureClass);
				return;
			}
			ResultBuffer resultBuffer = this.BuildQueryResultBuffer();
			if (resultBuffer.AsArray().Count<TypedValue>() == 0)
			{
				this._FC.Query = new ResultBuffer(new TypedValue[]
				{
					new TypedValue(8, "*")
				});
			}
			else
			{
				this._FC.Query = resultBuffer;
			}
			List<CadField> list = new List<CadField>();
			foreach (DataRow dataRow in this._FieldData.Rows)
			{
				CadField cadField = new CadField();
				cadField.Name = CadField.FixFieldName(dataRow["Name"].ToString());
				CadField.CadFieldType code = CadField.TypeCodeFromString(dataRow["Type"].ToString());
				cadField.ExtendedType = CadField.ExtendedTypeFromString(dataRow["Type"].ToString());
				cadField.Value = CadField.CreateTypedValue(code, dataRow["Value"].ToString());
				if (dataRow["Domain"] is FieldDomain)
				{
					cadField.Domain = (FieldDomain)dataRow["Domain"];
				}
				else
				{
					cadField.Domain = null;
				}
				CadField.CadFieldType arg_142_0 = cadField.FieldType;
				if (cadField.Value.TypeCode == 1)
				{
					string text = dataRow["Length"].ToString();
					if (!string.IsNullOrEmpty(text))
					{
						short length;
						if (short.TryParse(text, out length))
						{
							cadField.Length = length;
						}
						else
						{
							cadField.Length = 254;
						}
					}
					else
					{
						cadField.Length = 254;
					}
				}
				list.Add(cadField);
			}
			this._FC.Fields = list;
			Database database = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Database;
			try
			{
				using (Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.LockDocument())
				{
					var transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						this._FC.Write(database, transaction);
						transaction.Commit();
					}
				}
				base.Close();
			}
			catch
			{
				ErrorReport.ShowErrorMessage(AfaStrings.ErrorWritingFeatureClass);
			}
		}

		private void OnClickNewQuery(object sender, RoutedEventArgs e)
		{
			System.Windows.MessageBox.Show("Not Implemented:  This will invoke a new dialog to define a new query ala Quick Select");
		}

		private void cbProperty_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string text = (string)this.cbProperty.SelectedValue;
			if (text == null)
			{
				return;
			}
			if (text == AfaStrings.Layer)
			{
				this.cbValue.ItemsSource = this.BuildLayerList();
				return;
			}
			if (text == AfaStrings.Color)
			{
				this.cbValue.ItemsSource = this.BuildColorList();
				return;
			}
			if (text == AfaStrings.Linetype)
			{
				this.cbValue.ItemsSource = this.BuildLinetypeList();
				return;
			}
			if (text == AfaStrings.Lineweight)
			{
				this.cbValue.ItemsSource = this.BuildLineweightList();
				return;
			}
			if (text == AfaStrings.TextStyleName)
			{
				this.cbValue.ItemsSource = this.BuildTextStyleList();
				return;
			}
			if (text == AfaStrings.BlockName)
			{
				this.cbValue.ItemsSource = this.BuildBlockNameList();
				return;
			}
			this.cbValue.ItemsSource = null;
		}

		private List<string> BuildLayerList()
		{
			List<string> list = new List<string>();
			list.Add("*");
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			try
			{
				using (document.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						LayerTable layerTable = (LayerTable)transaction.TransactionManager.GetObject(database.LayerTableId, 0, false);
						using (SymbolTableEnumerator enumerator = layerTable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ObjectId current = enumerator.Current;
								LayerTableRecord layerTableRecord = (LayerTableRecord)transaction.TransactionManager.GetObject(current, 0, false);
								if (!layerTableRecord.IsHidden)
								{
									list.Add(layerTableRecord.Name);
								}
							}
						}
						transaction.Commit();
					}
				}
			}
			catch
			{
			}
			list.Add("Select from entity...");
			return list;
		}

		private List<string> InitColorList()
		{
			return new List<string>
			{
				AfaStrings.AnyValue,
				"ByLayer",
				"Red",
				"Yellow",
				"Green",
				"Cyan",
				"Blue",
				"Magenta",
				"White",
				"Select Color...",
				"Select from entity..."
			};
		}

		private List<string> BuildColorList()
		{
			return this._mColorList;
		}

		private List<string> BuildLinetypeList()
		{
			List<string> list = new List<string>();
			list.Add(AfaStrings.AnyValue);
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			try
			{
				using (document.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						LinetypeTable linetypeTable = (LinetypeTable)transaction.TransactionManager.GetObject(database.LinetypeTableId, 0, false);
						using (SymbolTableEnumerator enumerator = linetypeTable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ObjectId current = enumerator.Current;
								LinetypeTableRecord linetypeTableRecord = (LinetypeTableRecord)transaction.TransactionManager.GetObject(current, 0, false);
								if (!linetypeTableRecord.IsErased)
								{
									list.Add(linetypeTableRecord.Name);
								}
							}
						}
						transaction.Commit();
					}
				}
			}
			catch
			{
			}
			list.Add("Select from entity...");
			return list;
		}

		private List<string> BuildLineweightList()
		{
			return new List<string>
			{
				AfaStrings.AnyValue,
				"ByLayer",
				"ByBlock",
				"Default",
				"0.00",
				"0.05",
				"0.09",
				"0.13",
				"0.15",
				"0.18",
				"0.20",
				"0.25",
				"0.30",
				"0.35",
				"0.40",
				"0.50",
				"0.53",
				"0.60",
				"0.70",
				"0.80",
				"0.90",
				"1.00",
				"1.06",
				"1.20",
				"1.40",
				"1.58",
				"2.00",
				"2.11",
				"Select from entity..."
			};
		}

		private List<string> BuildTextStyleList()
		{
			List<string> list = new List<string>();
			list.Add(AfaStrings.AnyValue);
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			try
			{
				using (document.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						TextStyleTable textStyleTable = (TextStyleTable)transaction.TransactionManager.GetObject(database.TextStyleTableId, 0, false);
						using (SymbolTableEnumerator enumerator = textStyleTable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ObjectId current = enumerator.Current;
								TextStyleTableRecord textStyleTableRecord = (TextStyleTableRecord)transaction.TransactionManager.GetObject(current, 0, false);
								if (!textStyleTableRecord.IsErased)
								{
									list.Add(textStyleTableRecord.Name);
								}
							}
						}
						transaction.Commit();
					}
				}
			}
			catch
			{
			}
			list.Add("Select from entity...");
			return list;
		}

		private List<string> BuildBlockNameList()
		{
			List<string> list = new List<string>();
			list.Add(AfaStrings.AnyValue);
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			try
			{
				using (document.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						BlockTable blockTable = (BlockTable)transaction.TransactionManager.GetObject(database.BlockTableId, 0, false);
						using (SymbolTableEnumerator enumerator = blockTable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ObjectId current = enumerator.Current;
								BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.TransactionManager.GetObject(current, 0, false);
								if (!blockTableRecord.IsErased && !blockTableRecord.IsAnonymous && !blockTableRecord.Name.StartsWith("*"))
								{
									list.Add(blockTableRecord.Name);
								}
							}
						}
						transaction.Commit();
					}
				}
			}
			catch
			{
			}
			list.Add("Select from entity...");
			return list;
		}

		private void cbValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string text = (string)this.cbProperty.SelectedValue;
			string text2 = (string)this.cbValue.SelectedItem;
			if (text2 == null)
			{
				return;
			}
			if (text2 == "Select from entity...")
			{
				this.SelectValueFromEntity(text);
				return;
			}
			if (text == AfaStrings.Layer)
			{
				this.BuildLayerRow(text2);
				return;
			}
			if (text == AfaStrings.Color)
			{
				this.BuildColorRow(text2);
				return;
			}
			if (text == AfaStrings.Linetype)
			{
				this.BuildLinetypeRow(text2);
				return;
			}
			if (text == AfaStrings.Lineweight)
			{
				this.BuildLineweightRow(text2);
				return;
			}
			if (text == AfaStrings.TextStyleName)
			{
				this.BuildTextStyleRow(text2);
				return;
			}
			if (text == AfaStrings.BlockName)
			{
				this.BuildBlockNameRow(text2);
			}
		}

		private bool QueryDataContainsRow(string typeString, out int index)
		{
			index = 0;
			bool result;
			try
			{
				for (int i = 0; i < this._QueryData.Rows.Count; i++)
				{
					DataRow dataRow = this._QueryData.Rows[i];
					if (dataRow["Type"].ToString() == typeString)
					{
						index = i;
						result = true;
						return result;
					}
				}
				result = false;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private void BuildLayerRow(string selectedValue)
		{
			if (selectedValue == "256")
			{
				selectedValue = "ByLayer";
			}
			int index = 0;
			if (!this.QueryDataContainsRow(AfaStrings.Layer, out index))
			{
				DataRow dataRow = this.QueryData.NewRow();
				dataRow["Value"] = selectedValue;
				dataRow["Type"] = AfaStrings.Layer;
				this.QueryData.Rows.Add(dataRow);
				return;
			}
			DataRow dataRow2 = this.QueryData.Rows[index];
			string text = dataRow2["Value"].ToString();
			if (!(this.cbAppend.IsChecked == true))
			{
				this.QueryData.Rows.RemoveAt(index);
				DataRow dataRow3 = this.QueryData.NewRow();
				dataRow3["Value"] = selectedValue;
				dataRow3["Type"] = AfaStrings.Layer;
				this.QueryData.Rows.Add(dataRow3);
				return;
			}
			if (string.IsNullOrEmpty(text))
			{
				dataRow2["Value"] = text;
				return;
			}
			if (this.IsInStringList(text, "*"))
			{
				dataRow2["Value"] = "*";
				return;
			}
			if (!this.IsInStringList(text, selectedValue))
			{
				dataRow2["Value"] = text + "," + selectedValue;
			}
		}

		private void BuildColorRow(string selectedValue)
		{
			string value = "";
			if (selectedValue == AfaStrings.AnyValue)
			{
				value = "";
			}
			else if (selectedValue == "Select Color...")
			{
               var colorDialog = new Autodesk.AutoCAD.Windows.ColorDialog();
				DialogResult dialogResult = colorDialog.ShowDialog();
				if (dialogResult == System.Windows.Forms.DialogResult.OK)
				{
					string text = DXFCode.TranstateColorIndexToString(colorDialog.Color.ColorIndex);
					if (!this._mColorList.Contains(text))
					{
						this._mColorList.Add(text);
						this.cbValue.ItemsSource = this._mColorList;
					}
					this.cbValue.SelectedValue = text;
					value = text;
				}
			}
			else
			{
				short colorIndex = DXFCode.TranslateColorString(selectedValue);
				value = DXFCode.TranstateColorIndexToString(colorIndex);
			}
			int index;
			if (this.QueryDataContainsRow(AfaStrings.Color, out index))
			{
				this.QueryData.Rows.RemoveAt(index);
			}
			if (!string.IsNullOrEmpty(value))
			{
				DataRow dataRow = this.QueryData.NewRow();
				dataRow["Value"] = value;
				dataRow["Type"] = AfaStrings.Color;
				this.QueryData.Rows.Add(dataRow);
			}
		}

		private void BuildLinetypeRow(string selectedValue)
		{
			string value = "";
			int index = -1;
			DataRow dataRow = null;
			if (this.QueryDataContainsRow(AfaStrings.Linetype, out index))
			{
				dataRow = this._QueryData.Rows[index];
			}
			if (selectedValue == AfaStrings.AnyValue)
			{
				value = "";
			}
			else if (this.cbAppend.IsChecked == true)
			{
				string text = dataRow["Value"].ToString();
				if (!string.IsNullOrEmpty(text))
				{
					if (!this.IsInStringList(text, selectedValue))
					{
						value = text + "," + selectedValue;
					}
					else
					{
						value = text;
					}
				}
			}
			else
			{
				value = selectedValue;
			}
			if (dataRow != null)
			{
				dataRow["Value"] = value;
				return;
			}
			DataRow dataRow2 = this._QueryData.NewRow();
			dataRow2["Type"] = AfaStrings.Linetype;
			dataRow2["Value"] = value;
			this._QueryData.Rows.Add(dataRow2);
		}

		private void BuildLineweightRow(string selectedValue)
		{
			string value;
			if (selectedValue == AfaStrings.AnyValue)
			{
				value = "";
			}
			else
			{
				value = selectedValue;
			}
			int index;
			if (this.QueryDataContainsRow(AfaStrings.Lineweight, out index))
			{
				this.QueryData.Rows.RemoveAt(index);
			}
			if (!string.IsNullOrEmpty(value))
			{
				DataRow dataRow = this.QueryData.NewRow();
				dataRow["Value"] = value;
				dataRow["Type"] = AfaStrings.Lineweight;
				this.QueryData.Rows.Add(dataRow);
			}
		}

		private void BuildTextStyleRow(string selectedValue)
		{
			string value = "";
			int index = -1;
			DataRow dataRow = null;
			if (this.QueryDataContainsRow(AfaStrings.TextStyle, out index))
			{
				dataRow = this._QueryData.Rows[index];
			}
			if (selectedValue == AfaStrings.AnyValue)
			{
				value = "";
			}
			else if (this.cbAppend.IsChecked == true)
			{
				string text = dataRow["Value"].ToString();
				if (!string.IsNullOrEmpty(text))
				{
					if (!this.IsInStringList(text, selectedValue))
					{
						value = text + "," + selectedValue;
					}
					else
					{
						value = text;
					}
				}
			}
			else
			{
				value = selectedValue;
			}
			if (dataRow != null)
			{
				dataRow["Value"] = value;
				return;
			}
			DataRow dataRow2 = this._QueryData.NewRow();
			dataRow2["Type"] = AfaStrings.TextStyle;
			dataRow2["Value"] = value;
			this._QueryData.Rows.Add(dataRow2);
		}

		private string BuildBlockNameRow(string selectedValue)
		{
			string value = "";
			int index = -1;
			DataRow dataRow = null;
			if (this.QueryDataContainsRow(AfaStrings.BlockName, out index))
			{
				dataRow = this._QueryData.Rows[index];
			}
			if (selectedValue == AfaStrings.AnyValue)
			{
				value = "";
			}
			else if (this.cbAppend.IsChecked == true)
			{
				string text = dataRow["Value"].ToString();
				if (!string.IsNullOrEmpty(text))
				{
					if (!this.IsInStringList(text, selectedValue))
					{
						value = text + "," + selectedValue;
					}
					else
					{
						value = text;
					}
				}
			}
			else
			{
				value = selectedValue;
			}
			if (dataRow != null)
			{
				dataRow["Value"] = value;
			}
			else
			{
				DataRow dataRow2 = this._QueryData.NewRow();
				dataRow2["Type"] = AfaStrings.BlockName;
				dataRow2["Value"] = value;
				this._QueryData.Rows.Add(dataRow2);
			}
			return selectedValue;
		}

		private bool IsInStringList(string stringList, string testString)
		{
			if (!stringList.Contains(testString))
			{
				return false;
			}
			string[] array = stringList.Split(new char[]
			{
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				string a = text.Trim();
				if (a == testString)
				{
					return true;
				}
			}
			return false;
		}

		private void SelectValueFromEntity(string propertyType)
		{
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			Editor editor = document.Editor;
			try
			{
				using (document.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
					using (Transaction transaction = transactionManager.StartTransaction())
					{
						PromptEntityOptions promptEntityOptions = new PromptEntityOptions("\nPick a sample entity: ");
						promptEntityOptions.SetRejectMessage("Invalid pick: must be AutoCAD entity.");
						promptEntityOptions.AddAllowedClass(typeof(Entity), false);
						PromptEntityResult entity = editor.GetEntity(promptEntityOptions);
						if (entity.Status== (PromptStatus)5100)
						{
							Entity entity2 = (Entity)transaction.GetObject(entity.ObjectId, 0);
							if (propertyType == "Layer")
							{
								this.cbValue.SelectedValue = entity2.Layer;
							}
							else if (propertyType == "Color")
							{
								this.cbValue.SelectedValue = DXFCode.TranstateColorIndexToString(entity2.Color.ColorIndex);
							}
							else if (propertyType == "Linetype")
							{
								this.cbValue.SelectedValue = entity2.Linetype;
							}
							else if (propertyType == "Lineweight")
							{
								this.cbValue.SelectedValue = entity2.LineWeight.ToString();
							}
							else if (propertyType == "Text Style")
							{
								if (entity2 is DBText)
								{
									DBText dBText = (DBText)entity2;
									string textStyleName = dBText.TextStyleName;
									this.cbValue.SelectedValue = textStyleName;
								}
								else if (entity2 is MText)
								{
									MText mText = (MText)entity2;
									string textStyleName2 = mText.TextStyleName;
									this.cbValue.SelectedValue = textStyleName2;
								}
							}
							else if (propertyType == "Block Name" && entity2 is BlockReference)
							{
								BlockReference blockReference = (BlockReference)entity2;
								this.cbValue.SelectedValue = blockReference.BlockName;
							}
						}
						transaction.Commit();
					}
				}
			}
			catch
			{
			}
		}

		private void btnPreview_Click(object sender, RoutedEventArgs e)
		{
			Document document = AfaDocData.ActiveDocData.Document;
			Editor editor = document.Editor;
			ResultBuffer resultBuffer = this.BuildQueryResultBuffer();
			try
			{
				if (resultBuffer.AsArray().Count<TypedValue>() == 0)
				{
					resultBuffer = new ResultBuffer(new TypedValue[]
					{
						new TypedValue(8, "*")
					});
				}
			}
			catch
			{
				resultBuffer = new ResultBuffer(new TypedValue[]
				{
					new TypedValue(8, "*")
				});
			}
			ResultBuffer resultBuffer2 = MSCFeatureClass.BuildTypeQuery(this._FC.GeometryType, resultBuffer.AsArray());
			SelectionFilter selectionFilter = new SelectionFilter(resultBuffer2.AsArray());
			PromptSelectionResult promptSelectionResult = editor.SelectAll(selectionFilter);
			if (promptSelectionResult.Status == (PromptStatus)5100 && promptSelectionResult.Value.Count > 0)
			{
				ObjectId[] objectIds = promptSelectionResult.Value.GetObjectIds();
				editor.WriteMessage(string.Format("\n{0} entities found", objectIds.Length));
				editor.SetImpliedSelection(DocUtil.ExpandGroupObjectIds(document, objectIds));
				editor.UpdateScreen();
				return;
			}
			editor.WriteMessage(string.Format("\n{0}", AfaStrings.noEntitiesQuery));
		}

		private void Length_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			int num = 0;
			e.Handled = true;
			if (int.TryParse(e.Text, out num))
			{
				e.Handled = false;
			}
		}

		private void cbDomainValues_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			DataRowView dataRowView = (DataRowView)((FrameworkElement)sender).DataContext;
			System.Windows.Controls.ComboBox comboBox = (System.Windows.Controls.ComboBox)sender;
			if (comboBox.SelectedItem != null)
			{
				MSCCodedValue mSCCodedValue = (MSCCodedValue)comboBox.SelectedItem;
				if (dataRowView["Value"].ToString() != mSCCodedValue.Value.ToString())
				{
					dataRowView["Value"] = mSCCodedValue.Value.ToString();
				}
			}
		}

		private void dgInitializeNewItem(object sender, InitializingNewItemEventArgs e)
		{
			DataRowView dataRowView = (DataRowView)e.NewItem;
			dataRowView.Row["Type"] = "Integer";
		}

		private void DataGrid_GotFocus(object sender, RoutedEventArgs e)
		{
		}

		private void cb_FieldTypeChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				DataRowView dataRowView = (DataRowView)((FrameworkElement)sender).DataContext;
				if (dataRowView != null)
				{
					System.Windows.Controls.ComboBox comboBox = (System.Windows.Controls.ComboBox)sender;
					if (comboBox.SelectedItem != null)
					{
						string text = comboBox.SelectedItem.ToString();
						if (dataRowView["Type"].ToString() != text)
						{
							if (text == "String")
							{
								dataRowView["Length"] = 255;
							}
							else if (text == "Date")
							{
								dataRowView["Length"] = 36;
							}
							else
							{
								dataRowView["Length"] = 0;
							}
							dataRowView["Value"] = "";
						}
					}
				}
			}
			catch
			{
			}
		}

		private void UpdateDisplayValueFromDefault(DataRowView rowView)
		{
			if (rowView == null)
			{
				return;
			}
			rowView["DisplayValue"] = rowView["Value"];
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			DataRowView rowView = (DataRowView)((FrameworkElement)sender).DataContext;
			this.UpdateDisplayValueFromDefault(rowView);
		}

		private void dptDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			DataRowView rowView = (DataRowView)((FrameworkElement)sender).DataContext;
			this.UpdateDisplayValueFromDefault(rowView);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/featureclassproperties.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.This = (FeatureClassProperties)target;
				return;
			case 7:
				this.lblName = (System.Windows.Controls.Label)target;
				return;
			case 8:
				this.lblType = (System.Windows.Controls.Label)target;
				return;
			case 9:
				this.lblProperty = (System.Windows.Controls.Label)target;
				return;
			case 10:
				this.cbProperty = (System.Windows.Controls.ComboBox)target;
				this.cbProperty.SelectionChanged += new SelectionChangedEventHandler(this.cbProperty_SelectionChanged);
				return;
			case 11:
				this.lblValue = (System.Windows.Controls.Label)target;
				return;
			case 12:
				this.cbValue = (System.Windows.Controls.ComboBox)target;
				this.cbValue.SelectionChanged += new SelectionChangedEventHandler(this.cbValue_SelectionChanged);
				return;
			case 13:
				this.cbAppend = (System.Windows.Controls.CheckBox)target;
				return;
			case 14:
				this.dgQuery = (System.Windows.Controls.DataGrid)target;
				return;
			case 15:
				this.btnPreview = (System.Windows.Controls.Button)target;
				this.btnPreview.Click += new RoutedEventHandler(this.btnPreview_Click);
				return;
			case 16:
				this.dgFields = (System.Windows.Controls.DataGrid)target;
				this.dgFields.GotFocus += new RoutedEventHandler(this.DataGrid_GotFocus);
				this.dgFields.InitializingNewItem += new InitializingNewItemEventHandler(this.dgInitializeNewItem);
				return;
			case 19:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.OnClickOK);
				return;
			case 20:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.OnClickCancel);
				return;
			}
			this._contentLoaded = true;
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 2:
				((System.Windows.Controls.TextBox)target).TextChanged += new TextChangedEventHandler(this.TextBox_TextChanged);
				return;
			case 3:
				((DatePicker)target).SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(this.dptDatePicker_SelectedDateChanged);
				return;
			case 4:
				((System.Windows.Controls.ComboBox)target).SelectionChanged += new SelectionChangedEventHandler(this.cbDomainValues_SelectionChanged);
				return;
			case 5:
				((System.Windows.Controls.TextBox)target).TextChanged += new TextChangedEventHandler(this.TextBox_TextChanged);
				return;
			case 6:
				((System.Windows.Controls.TextBox)target).TextChanged += new TextChangedEventHandler(this.TextBox_TextChanged);
				return;
			default:
				switch (connectionId)
				{
				case 17:
					((System.Windows.Controls.ComboBox)target).SelectionChanged += new SelectionChangedEventHandler(this.cb_FieldTypeChanged);
					return;
				case 18:
					((System.Windows.Controls.TextBox)target).PreviewTextInput += new TextCompositionEventHandler(this.Length_PreviewTextInput);
					return;
				default:
					return;
				}
				break;
			}
		}
	}
}
