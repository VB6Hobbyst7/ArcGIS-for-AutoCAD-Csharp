using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;
using ColorDialog = Autodesk.AutoCAD.Windows.ColorDialog;

namespace AFA.Test
{
    public class BuildQuery : System.Windows.Window, IComponentConnector
	{
		private List<string> mColorList;

		internal System.Windows.Controls.ComboBox cbProperty;

		internal System.Windows.Controls.Label label1;

		internal System.Windows.Controls.Label label2;

		internal System.Windows.Controls.ComboBox cbValue;

		internal System.Windows.Controls.Button btnPreview;

		internal System.Windows.Controls.Button btnOk;

		internal System.Windows.Controls.Button btnCancel;

		internal System.Windows.Controls.Button btnClear;

		internal System.Windows.Controls.Label label3;

		internal System.Windows.Controls.CheckBox cbAppend;

		internal System.Windows.Controls.TextBox tbLayer;

		internal System.Windows.Controls.Label label4;

		internal System.Windows.Controls.TextBox tbColor;

		internal System.Windows.Controls.Label label5;

		internal System.Windows.Controls.TextBox tbLinetype;

		internal System.Windows.Controls.Label label6;

		internal System.Windows.Controls.TextBox tbLineweight;

		internal System.Windows.Controls.Label label7;

		internal System.Windows.Controls.TextBox tbTextStyle;

		internal System.Windows.Controls.Label label8;

		internal System.Windows.Controls.TextBox tbBlockName;

		private bool _contentLoaded;

		public BuildQuery()
		{
			this.InitializeComponent();
			this.mColorList = this.InitColorList();
		}

		private void cbProperty_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBoxItem comboBoxItem = (ComboBoxItem)this.cbProperty.SelectedItem;
			if (comboBoxItem == null)
			{
				return;
			}
			if (comboBoxItem.Content.ToString() == "Layer")
			{
				this.cbValue.ItemsSource = this.BuildLayerList();
				return;
			}
			if (comboBoxItem.Content.ToString() == "Color")
			{
				this.cbValue.ItemsSource = this.BuildColorList();
				return;
			}
			if (comboBoxItem.Content.ToString() == "Linetype")
			{
				this.cbValue.ItemsSource = this.BuildLinetypeList();
				return;
			}
			if (comboBoxItem.Content.ToString() == "Lineweight")
			{
				this.cbValue.ItemsSource = this.BuildLineweightList();
				return;
			}
			if (comboBoxItem.Content.ToString() == "Text Style")
			{
				this.cbValue.ItemsSource = this.BuildTextStyleList();
				return;
			}
			if (comboBoxItem.Content.ToString() == "Block Name")
			{
				this.cbValue.ItemsSource = this.BuildBlockNameList();
			}
		}

		private void cbValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBoxItem comboBoxItem = (ComboBoxItem)this.cbProperty.SelectedItem;
			string text = (string)this.cbValue.SelectedItem;
			if (text == null)
			{
				return;
			}
			if (text == "Select from entity...")
			{
				this.SelectValueFromEntity(comboBoxItem.Content.ToString());
				return;
			}
			if (comboBoxItem == null)
			{
				return;
			}
			if (comboBoxItem.Content.ToString() == "Layer")
			{
				return;
			}
			if (comboBoxItem.Content.ToString() == "Color")
			{
				return;
			}
			if (comboBoxItem.Content.ToString() == "Linetype")
			{
				return;
			}
			if (comboBoxItem.Content.ToString() == "Lineweight")
			{
				return;
			}
			if (comboBoxItem.Content.ToString() == "Text Style")
			{
				return;
			}
			if (comboBoxItem.Content.ToString() == "Block Name")
			{
				this.tbBlockName.Text = this.BuildBlockNameText(text);
			}
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
				"Any Color",
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
			return this.mColorList;
		}

		private List<string> BuildLinetypeList()
		{
			List<string> list = new List<string>();
			list.Add("Any Linetype");
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
				"Any Lineweight",
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
			list.Add("Any Text Style");
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			try
			{
				using (document.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var transactionManager = database.TransactionManager;
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
			list.Add("Any Block Name");
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			try
			{
				using (document.LockDocument((DocumentLockMode)20, null, null, false))
				{
					var  transactionManager = database.TransactionManager;
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
						if (entity.Status == (PromptStatus)5100)
						{
							Entity entity2 = (Entity)transaction.GetObject(entity.ObjectId, 0);
							if (propertyType == "Layer")
							{
								this.cbValue.SelectedValue = entity2.Layer;
							}
							else if (propertyType == "Color")
							{
								if (!string.IsNullOrEmpty(entity2.Color.ColorName))
								{
									this.cbValue.SelectedValue = entity2.Color.ColorName;
								}
								else
								{
									string text = entity2.Color.ColorIndex.ToString();
									if (!this.mColorList.Contains(text))
									{
										this.mColorList.Add(text);
										this.cbValue.ItemsSource = null;
										this.cbValue.ItemsSource = this.mColorList;
										this.cbValue.SelectedValue = text;
									}
								}
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

		private string BuildLayerText(string selectedValue)
		{
			if (selectedValue == "*")
			{
				return selectedValue;
			}
			if (selectedValue == "256")
			{
				selectedValue = "ByLayer";
			}
			if (!(this.cbAppend.IsChecked == true))
			{
				return selectedValue;
			}
			if (string.IsNullOrEmpty(this.tbLayer.Text))
			{
				return selectedValue;
			}
			if (this.IsInStringList(this.tbLayer.Text, "*"))
			{
				return this.tbLayer.Text;
			}
			if (!this.IsInStringList(this.tbLayer.Text, selectedValue))
			{
				return this.tbLayer.Text + "," + selectedValue;
			}
			return this.tbLayer.Text;
		}

		private string BuildColorText(string selectedValue)
		{
			if (selectedValue == "Any Color")
			{
				return "";
			}
			if (!(selectedValue == "Select Color..."))
			{
				return selectedValue;
			}
			ColorDialog colorDialog = new ColorDialog();
			DialogResult dialogResult = colorDialog.ShowDialog();
			if (dialogResult == System.Windows.Forms.DialogResult.OK)
			{
				string text = colorDialog.Color.ColorIndex.ToString();
				if (!this.mColorList.Contains(text))
				{
					this.mColorList.Add(text);
					this.cbValue.ItemsSource = this.mColorList;
				}
				this.cbValue.SelectedValue = text;
				return text;
			}
			return "";
		}

		private string BuildLinetypeText(string selectedValue)
		{
			if (selectedValue == "Any Linetype")
			{
				return "";
			}
			if (!(this.cbAppend.IsChecked == true))
			{
				return selectedValue;
			}
			if (string.IsNullOrEmpty(this.tbLinetype.Text))
			{
				return selectedValue;
			}
			if (!this.IsInStringList(this.tbLinetype.Text, selectedValue))
			{
				return this.tbLinetype.Text + "," + selectedValue;
			}
			return this.tbLinetype.Text;
		}

		private string BuildLineweightText(string selectedValue)
		{
			if (selectedValue == "Any Lineweight")
			{
				return "";
			}
			return selectedValue;
		}

		private string BuildTextStyleText(string selectedValue)
		{
			if (selectedValue == "Any Text Style")
			{
				return "";
			}
			if (!(this.cbAppend.IsChecked == true))
			{
				return selectedValue;
			}
			if (string.IsNullOrEmpty(this.tbTextStyle.Text))
			{
				return selectedValue;
			}
			if (!this.IsInStringList(this.tbTextStyle.Text, selectedValue))
			{
				return this.tbTextStyle.Text + "," + selectedValue;
			}
			return this.tbTextStyle.Text;
		}

		private string BuildBlockNameText(string selectedValue)
		{
			if (selectedValue == "Any Block Name")
			{
				return "";
			}
			if (!(this.cbAppend.IsChecked == true))
			{
				return selectedValue;
			}
			if (string.IsNullOrEmpty(this.tbBlockName.Text))
			{
				return selectedValue;
			}
			if (!this.IsInStringList(this.tbBlockName.Text, selectedValue))
			{
				return this.tbBlockName.Text + "," + selectedValue;
			}
			return this.tbBlockName.Text;
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

		private ResultBuffer BuildSelectionFilter()
		{
			ResultBuffer result;
			try
			{
				ResultBuffer resultBuffer = new ResultBuffer(new TypedValue[]
				{
					new TypedValue(-4, "<and")
				});
				if (string.IsNullOrEmpty(this.tbLayer.Text))
				{
					this.tbLayer.Text = "*";
				}
				resultBuffer.Add(new TypedValue(8, this.tbLayer.Text));
				if (!string.IsNullOrEmpty(this.tbColor.Text))
				{
					resultBuffer.Add(this.GetColorTypedValue());
				}
				if (!string.IsNullOrEmpty(this.tbLinetype.Text))
				{
					resultBuffer.Add(this.GetLinetypeTypedValue());
				}
				if (!string.IsNullOrEmpty(this.tbLineweight.Text))
				{
					resultBuffer.Add(this.GetLineweightTypedValue());
				}
				if (!string.IsNullOrEmpty(this.tbTextStyle.Text))
				{
					resultBuffer.Add(this.GetTextStyleTypedValue());
				}
				if (!string.IsNullOrEmpty(this.tbBlockName.Text))
				{
					resultBuffer.Add(this.GetBlockNameTypedValue());
				}
				resultBuffer.Add(new TypedValue(-4, "and>"));
				result = resultBuffer;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private TypedValue GetColorTypedValue()
		{
			if (this.tbColor.Text == "Red")
			{
				return new TypedValue(62, 1);
			}
			if (this.tbColor.Text == "Yellow")
			{
				return new TypedValue(62, 2);
			}
			if (this.tbColor.Text == "Green")
			{
				return new TypedValue(62, 3);
			}
			if (this.tbColor.Text == "Cyan")
			{
				return new TypedValue(62, 4);
			}
			if (this.tbColor.Text == "Blue")
			{
				return new TypedValue(62, 5);
			}
			if (this.tbColor.Text == "Magenta")
			{
				return new TypedValue(62, 6);
			}
			if (this.tbColor.Text == "White")
			{
				return new TypedValue(62, 7);
			}
			if (this.tbColor.Text == "ByLayer")
			{
				return new TypedValue(62, 256);
			}
			if (this.tbColor.Text == "ByBlock")
			{
				return new TypedValue(62, 0);
			}
			short num;
			if (short.TryParse(this.tbColor.Text, out num))
			{
				return new TypedValue(62, num);
			}
			return new TypedValue(62, 256);
		}

		private TypedValue GetLinetypeTypedValue()
		{
			return new TypedValue(6, this.tbLinetype.Text);
		}

		private TypedValue GetLineweightTypedValue()
		{
			return new TypedValue(370, Convert.ToInt16(100.0 * double.Parse(this.tbLineweight.Text)));
		}

		private TypedValue GetTextStyleTypedValue()
		{
			return new TypedValue(7, this.tbTextStyle.Text);
		}

		private TypedValue GetBlockNameTypedValue()
		{
			return new TypedValue(2, this.tbBlockName.Text);
		}

		private void btnPreview_Click(object sender, RoutedEventArgs e)
		{
			Document document = AfaDocData.ActiveDocData.Document;
			Editor editor = document.Editor;
			ResultBuffer resultBuffer = this.BuildSelectionFilter();
			SelectionFilter selectionFilter = new SelectionFilter(resultBuffer.AsArray());
			PromptSelectionResult promptSelectionResult = editor.SelectAll(selectionFilter);
			if (promptSelectionResult.Status == (PromptStatus)5100 && promptSelectionResult.Value.Count > 0)
			{
				ObjectId[] objectIds = promptSelectionResult.Value.GetObjectIds();
				editor.WriteMessage(string.Format("\n{0} entities found", objectIds.Length));
				editor.SetImpliedSelection(DocUtil.ExpandGroupObjectIds(document, objectIds));
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/test/buildquery.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.cbProperty = (System.Windows.Controls.ComboBox)target;
				this.cbProperty.SelectionChanged += new SelectionChangedEventHandler(this.cbProperty_SelectionChanged);
				return;
			case 2:
				this.label1 = (System.Windows.Controls.Label)target;
				return;
			case 3:
				this.label2 = (System.Windows.Controls.Label)target;
				return;
			case 4:
				this.cbValue = (System.Windows.Controls.ComboBox)target;
				this.cbValue.SelectionChanged += new SelectionChangedEventHandler(this.cbValue_SelectionChanged);
				return;
			case 5:
				this.btnPreview = (System.Windows.Controls.Button)target;
				this.btnPreview.Click += new RoutedEventHandler(this.btnPreview_Click);
				return;
			case 6:
				this.btnOk = (System.Windows.Controls.Button)target;
				return;
			case 7:
				this.btnCancel = (System.Windows.Controls.Button)target;
				return;
			case 8:
				this.btnClear = (System.Windows.Controls.Button)target;
				return;
			case 9:
				this.label3 = (System.Windows.Controls.Label)target;
				return;
			case 10:
				this.cbAppend = (System.Windows.Controls.CheckBox)target;
				return;
			case 11:
				this.tbLayer = (System.Windows.Controls.TextBox)target;
				return;
			case 12:
				this.label4 = (System.Windows.Controls.Label)target;
				return;
			case 13:
				this.tbColor = (System.Windows.Controls.TextBox)target;
				return;
			case 14:
				this.label5 = (System.Windows.Controls.Label)target;
				return;
			case 15:
				this.tbLinetype = (System.Windows.Controls.TextBox)target;
				return;
			case 16:
				this.label6 = (System.Windows.Controls.Label)target;
				return;
			case 17:
				this.tbLineweight = (System.Windows.Controls.TextBox)target;
				return;
			case 18:
				this.label7 = (System.Windows.Controls.Label)target;
				return;
			case 19:
				this.tbTextStyle = (System.Windows.Controls.TextBox)target;
				return;
			case 20:
				this.label8 = (System.Windows.Controls.Label)target;
				return;
			case 21:
				this.tbBlockName = (System.Windows.Controls.TextBox)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
