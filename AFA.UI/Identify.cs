using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using DataTable = System.Data.DataTable;

namespace AFA.UI
{
    public class Identify : Window, IComponentConnector, IStyleConnector
	{
		private class IdentifyFeatureClassItem
		{
			public string Name
			{
				get;
				set;
			}

			public FCTag Tag
			{
				get;
				set;
			}

			public ObjectId[] IDs
			{
				get;
				set;
			}

			public List<Identify.IdentifyFeatureClassItem> SubtypeItems
			{
				get;
				set;
			}

			public override string ToString()
			{
				return this.Name;
			}

			public IdentifyFeatureClassItem(string name, MSCFeatureClass fc, ObjectIdCollection ids)
			{
				this.Name = name;
				this.Tag = new FCTag(fc);
				this.IDs = fc.GetFeatureIds(ids);
				this.SubtypeItems = new List<Identify.IdentifyFeatureClassItem>();
			}

			public IdentifyFeatureClassItem(MSCFeatureClass fc)
			{
				this.Name = fc.Name;
				this.Tag = new FCTag(fc);
				this.IDs = fc.GetFeatureIds();
				this.SubtypeItems = Identify.BuildSubtypeItems(fc, this.IDs);
			}

			public IdentifyFeatureClassItem(MSCFeatureClass fc, ObjectIdCollection ids)
			{
				this.Name = fc.Name;
				this.Tag = new FCTag(fc);
				this.IDs = fc.GetFeatureIds(ids);
				this.SubtypeItems = Identify.BuildSubtypeItems(fc, this.IDs);
			}
		}

		private string name = "Key";

		private string value = "Value";

		private string fieldType = "FieldType";

		private string ro = "ReadOnly";

		private string fieldLength = "FieldLength";

		internal static string VariesValue = "*-" + AfaStrings.Varies + "-*";

		private DataTable Table;

		private ObjectIdCollection currentObjectIds;

		private ObjectIdCollection originalObjectIds;

		internal System.Windows.Controls.ComboBox FeatureClassComboBox;

		internal System.Windows.Controls.ComboBox SubtypeClassComboBox;

		internal System.Windows.Controls.DataGrid dataGrid;

		private bool _contentLoaded;

		public Identify(ObjectIdCollection ids)
		{
			this.InitializeComponent();
			this.Table = new DataTable();
			this.Table.Columns.Add(this.name);
			this.Table.Columns.Add(this.value);
			this.Table.Columns.Add(this.fieldType, typeof(CadField.CadFieldType));
			this.Table.Columns.Add(this.ro, typeof(bool));
			this.Table.Columns.Add(this.fieldLength, typeof(int));
			this.Table.Columns[0].ReadOnly = true;
			this.Table.Columns[2].ReadOnly = true;
			this.Table.Columns[3].ReadOnly = true;
			this.Table.Columns[4].ReadOnly = true;
			this.originalObjectIds = ids;
			this.PopulateFeatureClassPicker(AfaDocData.ActiveDocData.GetTopActiveFeatureClass(), AfaDocData.ActiveDocData.GetActiveSubtype(), ids);
			this.FillRows(ids);
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
		}

		private void OnClose(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		public void DragWindow(object sender, MouseButtonEventArgs args)
		{
			try
			{
				base.DragMove();
			}
			catch
			{
			}
		}

		private void SetObjectId(ObjectId id)
		{
			if (id == ObjectId.Null)
			{
				this.Table.Rows.Clear();
				return;
			}
			ObjectIdCollection objectIdCollection = new ObjectIdCollection();
			this.currentObjectIds = objectIdCollection;
			objectIdCollection.Add(id);
			this.FillRows(objectIdCollection);
		}

		private void SetObjectIds(ObjectIdCollection ids)
		{
			this.currentObjectIds = ids;
			if (ids != null)
			{
				this.FillRows(ids);
			}
		}

		public void SetFeatureClassesFromObjectIds(ObjectIdCollection ids)
		{
			this.PopulateFeatureClassPicker(AfaDocData.ActiveDocData.GetTopActiveFeatureClass(), AfaDocData.ActiveDocData.GetActiveSubtype(), ids);
		}

		public void SetFeatureClassesFromObjectId(ObjectId id)
		{
			ObjectIdCollection objectIdCollection = new ObjectIdCollection();
			this.currentObjectIds = objectIdCollection;
			objectIdCollection.Add(id);
			this.PopulateFeatureClassPicker(AfaDocData.ActiveDocData.GetTopActiveFeatureClass(), AfaDocData.ActiveDocData.GetActiveSubtype(), objectIdCollection);
		}

		private void FillRows(ObjectIdCollection idCollection)
		{
			MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
			this.Table = activeFeatureClassOrSubtype.Identify(idCollection);
			this.Table.Columns[this.name].ReadOnly = true;
			this.dataGrid.ItemsSource = this.Table.DefaultView;
			this.dataGrid.DataContext = this.Table;
		}

		private void OnSelect(object sender, RoutedEventArgs e)
		{
			DocumentCollection documentManager = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager;
			if (documentManager.MdiActiveDocument == null)
			{
				return;
			}
			Editor editor = documentManager.MdiActiveDocument.Editor;
			this.currentObjectIds = null;
			editor.SetImpliedSelection(new ObjectId[0]);
			base.Close();
			PromptSelectionOptions promptSelectionOptions = new PromptSelectionOptions();
			promptSelectionOptions.AllowDuplicates=(false);
			promptSelectionOptions.MessageForAdding=(AfaStrings.MSG_SELECT_OBJECTADDED);
			promptSelectionOptions.MessageForRemoval=(AfaStrings.MSG_SELECT_OBJECTSKIPPED);
			PromptSelectionResult selection = editor.GetSelection(promptSelectionOptions);
			if (selection.Status == (PromptStatus) (-5002))
			{
				editor.WriteMessage(AfaStrings.CommandCancelled);
				return;
			}
			if (selection.Status == (PromptStatus)5100)
			{
				SelectionSet selectionSet = selection.Value;
				if (selectionSet.Count <= 0)
				{
					editor.WriteMessage(AfaStrings.NoFeaturesFound);
					return;
				}
				ObjectId[] objectIds = selectionSet.GetObjectIds();
				if (objectIds != null)
				{
					Identify identify = new Identify(new ObjectIdCollection(selectionSet.GetObjectIds()));
					Autodesk.AutoCAD.ApplicationServices.Core.Application.ShowModalWindow(null, identify, false);
					return;
				}
			}
			else
			{
				editor.WriteMessage(AfaStrings.NoFeaturesFound);
			}
		}

		private void UpdateFromClass(Identify.IdentifyFeatureClassItem newItem)
		{
			Document document = AfaDocData.ActiveDocData.Document;
			using (document.LockDocument())
			{
				FCTag tag = newItem.Tag;
				MSCFeatureClass activeFeatureClass = AfaDocData.ActiveDocData.SetActiveFeatureClass(tag.GetFeatureClass(AfaDocData.ActiveDocData.DocDataset));
				ArcGISRibbon.SetActiveFeatureClass(activeFeatureClass);
				ObjectId[] iDs = newItem.IDs;
				Editor editor = AfaDocData.ActiveDocData.Document.Editor;
				document.Editor.SetImpliedSelection(DocUtil.ExpandGroupObjectIds(AfaDocData.ActiveDocData.Document, iDs));
				ObjectIdCollection objectIds = null;
				if (iDs != null)
				{
					objectIds = new ObjectIdCollection(iDs);
				}
				this.SetObjectIds(objectIds);
				editor.Regen();
			}
		}

		private void OnFeatureClassChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.FeatureClassComboBox.SelectedItem != null)
			{
				KeyValuePair<string, Identify.IdentifyFeatureClassItem> keyValuePair = (KeyValuePair<string, Identify.IdentifyFeatureClassItem>)this.FeatureClassComboBox.SelectedItem;
				this.UpdateFromClass(keyValuePair.Value);
				this.PopulateSubtypeClassPicker(keyValuePair.Value, null);
			}
		}

		private void OnSubytpeClassChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.SubtypeClassComboBox.SelectedItem != null)
			{
				Identify.IdentifyFeatureClassItem newItem = (Identify.IdentifyFeatureClassItem)this.SubtypeClassComboBox.SelectedItem;
				this.UpdateFromClass(newItem);
			}
		}

		private static List<Identify.IdentifyFeatureClassItem> BuildSubtypeItems(MSCFeatureClass fc, ObjectId[] ids)
		{
			List<Identify.IdentifyFeatureClassItem> list = new List<Identify.IdentifyFeatureClassItem>();
			if (ids == null)
			{
				return list;
			}
			if (ids.Length == 0)
			{
				return list;
			}
			ObjectIdCollection ids2 = new ObjectIdCollection(ids);
			if (fc.SubTypes.Count > 0)
			{
				list.Add(new Identify.IdentifyFeatureClassItem("<All Types>", fc, ids2)
				{
					IDs = ids
				});
				foreach (MSCFeatureClassSubType current in fc.SubTypes)
				{
					Identify.IdentifyFeatureClassItem identifyFeatureClassItem = new Identify.IdentifyFeatureClassItem(current, ids2);
					if (identifyFeatureClassItem.IDs.Length > 0)
					{
						list.Add(identifyFeatureClassItem);
					}
				}
			}
			return list;
		}

		private void PopulateSubtypeClassPicker(Identify.IdentifyFeatureClassItem parentClass, MSCFeatureClass currentSubtype)
		{
			if (parentClass.SubtypeItems.Count == 0)
			{
				this.SubtypeClassComboBox.ItemsSource = null;
				this.SubtypeClassComboBox.IsEnabled = false;
				return;
			}
			this.SubtypeClassComboBox.IsEnabled = true;
			this.SubtypeClassComboBox.ItemsSource = parentClass.SubtypeItems;
			Identify.IdentifyFeatureClassItem selectedItem = parentClass.SubtypeItems[0];
			if (currentSubtype != null)
			{
				foreach (Identify.IdentifyFeatureClassItem current in parentClass.SubtypeItems)
				{
					if (current.Name == currentSubtype.Name)
					{
						selectedItem = current;
						break;
					}
				}
			}
			this.SubtypeClassComboBox.SelectedItem = selectedItem;
		}

		private void PopulateFeatureClassPicker(MSCFeatureClass currentTopFC, MSCFeatureClass currentSubtype, ObjectIdCollection ids)
		{
			try
			{
				System.Windows.Forms.Application.UseWaitCursor = true;
				Dictionary<string, Identify.IdentifyFeatureClassItem> featureClasses = Identify.GetFeatureClasses(ids);
				this.FeatureClassComboBox.ItemsSource = featureClasses;
				bool flag = false;
				if (currentTopFC != null)
				{
					int num = 0;
					foreach (KeyValuePair<string, Identify.IdentifyFeatureClassItem> current in featureClasses)
					{
						if (current.Value.Name == currentTopFC.Name)
						{
							this.FeatureClassComboBox.SelectedIndex = num;
							flag = true;
							break;
						}
						num++;
					}
				}
				if (!flag)
				{
					this.FeatureClassComboBox.SelectedIndex = 0;
				}
				this.PopulateSubtypeClassPicker(((KeyValuePair<string, Identify.IdentifyFeatureClassItem>)this.FeatureClassComboBox.SelectedItem).Value, currentSubtype);
				System.Windows.Forms.Application.UseWaitCursor = false;
			}
			catch (SystemException ex)
			{
				System.Windows.Forms.Application.UseWaitCursor = false;
				string arg_BE_0 = ex.Message;
			}
		}

		private static Dictionary<string, Identify.IdentifyFeatureClassItem> GetFeatureClasses()
		{
			Dictionary<string, Identify.IdentifyFeatureClassItem> dictionary = new Dictionary<string, Identify.IdentifyFeatureClassItem>();
			foreach (MSCFeatureClass current in AfaDocData.ActiveDocData.DocDataset.FeatureClasses.Values)
			{
				new Identify.IdentifyFeatureClassItem(current);
			}
			foreach (MSCFeatureService current2 in AfaDocData.ActiveDocData.DocDataset.FeatureServices.Values)
			{
				Identify.IdentifyFeatureClassItem identifyFeatureClassItem = new Identify.IdentifyFeatureClassItem(current2);
				dictionary.Add(current2.Name, identifyFeatureClassItem);
			}
			return dictionary;
		}

		private static Dictionary<string, Identify.IdentifyFeatureClassItem> GetFeatureClasses(ObjectIdCollection ids)
		{
			if (ids == null)
			{
				return Identify.GetFeatureClasses();
			}
			if (ids.Count == 0)
			{
				return Identify.GetFeatureClasses();
			}
			Dictionary<string, Identify.IdentifyFeatureClassItem> result;
			if (ids.Count > AfaDocData.ActiveDocData.DocDataset.FeatureClasses.Count + AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count)
			{
				result = Identify.SearchByFC(ids);
			}
			else
			{
				result = Identify.SearchByID(ids);
			}
			return result;
		}

		private static Dictionary<string, Identify.IdentifyFeatureClassItem> SearchByID(ObjectIdCollection selectedIDs)
		{
			Dictionary<string, Identify.IdentifyFeatureClassItem> dictionary = new Dictionary<string, Identify.IdentifyFeatureClassItem>();
			foreach (ObjectId objectId in selectedIDs)
			{
				if (objectId != ObjectId.Null)
				{
					foreach (MSCFeatureClass current in AfaDocData.ActiveDocData.DocDataset.FeatureClasses.Values)
					{
						try
						{
							ObjectIdCollection objectIdCollection = new ObjectIdCollection(current.GetEntityIds());
							if (objectIdCollection != null && objectIdCollection.Contains(objectId) && !dictionary.Keys.Contains(current.Name))
							{
								Identify.IdentifyFeatureClassItem identifyFeatureClassItem = new Identify.IdentifyFeatureClassItem(current, selectedIDs);
								dictionary.Add(current.Name, identifyFeatureClassItem);
							}
						}
						catch
						{
						}
					}
					foreach (MSCFeatureService current2 in AfaDocData.ActiveDocData.DocDataset.FeatureServices.Values)
					{
						try
						{
							ObjectIdCollection objectIdCollection2 = new ObjectIdCollection(current2.GetEntityIds());
							if (objectIdCollection2.Contains(objectId))
							{
								string key = current2.Name;
								if (!dictionary.Keys.Contains(key))
								{
									Identify.IdentifyFeatureClassItem identifyFeatureClassItem2 = new Identify.IdentifyFeatureClassItem(current2, selectedIDs);
									dictionary.Add(key, identifyFeatureClassItem2);
								}
							}
						}
						catch
						{
						}
					}
				}
			}
			return dictionary;
		}

		private static Dictionary<string, Identify.IdentifyFeatureClassItem> SearchByFC(ObjectIdCollection selectedIDs)
		{
			Dictionary<string, Identify.IdentifyFeatureClassItem> dictionary = new Dictionary<string, Identify.IdentifyFeatureClassItem>();
			foreach (MSCFeatureClass current in AfaDocData.ActiveDocData.DocDataset.FeatureClasses.Values)
			{
				ObjectId[] entityIds = current.GetEntityIds();
				if (entityIds != null)
				{
					ObjectIdCollection objectIdCollection = new ObjectIdCollection(entityIds);
					foreach (ObjectId objectId in selectedIDs)
					{
						if (objectIdCollection.Contains(objectId))
						{
							Identify.IdentifyFeatureClassItem identifyFeatureClassItem = new Identify.IdentifyFeatureClassItem(current, selectedIDs);
							dictionary.Add(current.Name, identifyFeatureClassItem);
							break;
						}
					}
				}
			}
			foreach (MSCFeatureService current2 in AfaDocData.ActiveDocData.DocDataset.FeatureServices.Values)
			{
				ObjectId[] entityIds2 = current2.GetEntityIds();
				if (entityIds2 != null)
				{
					ObjectIdCollection objectIdCollection2 = new ObjectIdCollection(entityIds2);
					foreach (ObjectId objectId2 in selectedIDs)
					{
						if (objectIdCollection2.Contains(objectId2))
						{
							string key = current2.Name;
							dictionary.Add(key, new Identify.IdentifyFeatureClassItem(current2, selectedIDs));
							break;
						}
					}
				}
			}
			return dictionary;
		}

		private bool IsPossibleNumericInteger(string testString)
		{
            if (string.IsNullOrEmpty(testString))
            {
                return true;
            }
            testString = testString + 1;
            int result = 0;
            return int.TryParse(testString, out result);
        }

		private bool IsPossibleNumericDouble(string testString)
		{
            if (string.IsNullOrEmpty(testString))
            {
                return true;
            }
            testString = testString + 1;
            int result = 0;
            return int.TryParse(testString, out result);
        }

		private bool IsValidText(CadField.CadFieldType fType, int fieldLength, string testString)
		{
			if (string.IsNullOrEmpty(testString))
			{
				return true;
			}
			if (fType == CadField.CadFieldType.Integer)
			{
				int num = 0;
				return int.TryParse(testString, out num) || this.IsPossibleNumericInteger(testString);
			}
			if (fType == CadField.CadFieldType.Short)
			{
				short num2 = 0;
				return short.TryParse(testString, out num2) || this.IsPossibleNumericInteger(testString);
			}
			if (fType == CadField.CadFieldType.Double)
			{
				double num3 = 0.0;
				return double.TryParse(testString, out num3) || this.IsPossibleNumericDouble(testString);
			}
			return fType != CadField.CadFieldType.String || testString.Length <= fieldLength;
		}

		private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(typeof(string)))
			{
				string testString = (string)e.DataObject.GetData(typeof(string));
				System.Windows.Controls.TextBox textBox = (System.Windows.Controls.TextBox)sender;
				if (textBox.Tag == null)
				{
					return;
				}
				CadField.CadFieldType fType = (CadField.CadFieldType)textBox.Tag;
				DataRowView dataRowView = (DataRowView)((FrameworkElement)sender).DataContext;
				string s = dataRowView.Row["FieldLength"].ToString();
				int num = int.Parse(s);
				if (!this.IsValidText(fType, num, testString))
				{
					e.CancelCommand();
					return;
				}
			}
			else
			{
				e.CancelCommand();
			}
		}

		private void Default_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			try
			{
				System.Windows.Controls.TextBox textBox = (System.Windows.Controls.TextBox)sender;
				if (textBox.Tag != null)
				{
					CadField.CadFieldType fType = (CadField.CadFieldType)textBox.Tag;
					DataRowView dataRowView = (DataRowView)((FrameworkElement)sender).DataContext;
					string text = textBox.Text + e.Text;
					string s = dataRowView.Row["FieldLength"].ToString();
					int num = int.Parse(s);
					e.Handled = !this.IsValidText(fType, num, text);
					if (!e.Handled)
					{
						CadField cadField = (CadField)dataRowView.Row["BaseField"];
						FieldDomain domain = cadField.Domain;
						if (domain != null)
						{
							bool flag = domain.IsWithinRangeValue(text);
							if (!flag)
							{
								e.Handled = true;
							}
						}
					}
				}
			}
			catch
			{
				e.Handled = false;
			}
		}

		private void DateBox_LostFocus(object sender, RoutedEventArgs e)
		{
			DataRowView dataRowView = (DataRowView)((FrameworkElement)sender).DataContext;
			DatePicker datePicker = (DatePicker)sender;
			if (dataRowView == null)
			{
				return;
			}
			if (datePicker == null)
			{
				return;
			}
			if (dataRowView[this.value].ToString() != datePicker.Text)
			{
				dataRowView[this.value] = datePicker.Text;
				this.CommitEdit(dataRowView);
			}
		}

		private void TextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			DataRowView dataRowView = (DataRowView)((FrameworkElement)sender).DataContext;
			System.Windows.Controls.TextBox textBox = (System.Windows.Controls.TextBox)sender;
			if (dataRowView == null)
			{
				return;
			}
			if (textBox == null)
			{
				return;
			}
			if (dataRowView[this.value].ToString() != textBox.Text)
			{
				dataRowView[this.value] = textBox.Text;
				this.CommitEdit(dataRowView);
			}
		}

		private void CommitEdit(DataRowView currentRow)
		{
			try
			{
				System.Windows.Forms.Application.UseWaitCursor = true;
				MSCFeatureClass activeFeatureClassOrSubtype = AfaDocData.ActiveDocData.GetActiveFeatureClassOrSubtype();
				ObjectIdCollection ids = new ObjectIdCollection(activeFeatureClassOrSubtype.GetFeatureIds(this.currentObjectIds));
				Document parentDocument = activeFeatureClassOrSubtype.ParentDataset.ParentDocument;
				Database arg_35_0 = parentDocument.Database;
				using (parentDocument.LockDocument())
				{
					using (Transaction transaction = parentDocument.TransactionManager.StartTransaction())
					{
						try
						{
							activeFeatureClassOrSubtype.SetEntityFields(ids, currentRow.Row, transaction);
							parentDocument.TransactionManager.QueueForGraphicsFlush();
							parentDocument.TransactionManager.FlushGraphics();
							parentDocument.Editor.UpdateScreen();
						}
						catch
						{
						}
						finally
						{
							transaction.Commit();
						}
					}
				}
				if (object.Equals(currentRow.Row["Key"], activeFeatureClassOrSubtype.TypeField))
				{
					activeFeatureClassOrSubtype.UpdateSubtypeLayers(ids, currentRow.Row["Value"]);
				}
				System.Windows.Forms.Application.UseWaitCursor = false;
			}
			catch
			{
				System.Windows.Forms.Application.UseWaitCursor = false;
			}
		}

		private void cbDomainValues_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			DataRowView dataRowView = (DataRowView)((FrameworkElement)sender).DataContext;
			CadField cadField = dataRowView["BaseField"] as CadField;
			System.Windows.Controls.ComboBox comboBox = (System.Windows.Controls.ComboBox)sender;
			if (comboBox.SelectedItem != null)
			{
				MSCCodedValue mSCCodedValue = (MSCCodedValue)comboBox.SelectedItem;
				if (mSCCodedValue == null)
				{
					return;
				}
				if (mSCCodedValue.Value == null)
				{
					return;
				}
				MSCCodedValue mSCCodedValue2 = new MSCCodedValue("test", cadField.FieldType, dataRowView["Value"]);
				if (!mSCCodedValue2.Value.Equals(mSCCodedValue.Value))
				{
					dataRowView["Value"] = mSCCodedValue.Value;
					this.CommitEdit(dataRowView);
					if (cadField.TypeField)
					{
						this.PopulateFeatureClassPicker(AfaDocData.ActiveDocData.GetTopActiveFeatureClass(), AfaDocData.ActiveDocData.GetActiveSubtype(), this.originalObjectIds);
						this.FillRows(this.originalObjectIds);
						return;
					}
					try
					{
						ObservableCollection<MSCCodedValue> observableCollection = (ObservableCollection<MSCCodedValue>)dataRowView["CodedValues"];
						MSCCodedValue mSCCodedValue3 = observableCollection.First((MSCCodedValue x) => x.DisplayName == Identify.VariesValue);
						if (mSCCodedValue3 != null)
						{
							observableCollection.Remove(mSCCodedValue3);
						}
					}
					catch
					{
					}
				}
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
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/identify.xaml", UriKind.Relative);
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
			case 6:
				this.FeatureClassComboBox = (System.Windows.Controls.ComboBox)target;
				this.FeatureClassComboBox.SelectionChanged += new SelectionChangedEventHandler(this.OnFeatureClassChanged);
				return;
			case 7:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.OnSelect);
				return;
			case 8:
				this.SubtypeClassComboBox = (System.Windows.Controls.ComboBox)target;
				this.SubtypeClassComboBox.SelectionChanged += new SelectionChangedEventHandler(this.OnSubytpeClassChanged);
				return;
			case 9:
				this.dataGrid = (System.Windows.Controls.DataGrid)target;
				return;
			case 10:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.OnClose);
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((System.Windows.Controls.TextBox)target).LostFocus += new RoutedEventHandler(this.TextBox_LostFocus);
				((System.Windows.Controls.TextBox)target).PreviewTextInput += new TextCompositionEventHandler(this.Default_PreviewTextInput);
				((System.Windows.Controls.TextBox)target).AddHandler(System.Windows.DataObject.PastingEvent, new DataObjectPastingEventHandler(this.TextBox_Pasting));
				return;
			case 2:
				((DatePicker)target).LostFocus += new RoutedEventHandler(this.DateBox_LostFocus);
				((DatePicker)target).PreviewTextInput += new TextCompositionEventHandler(this.Default_PreviewTextInput);
				((DatePicker)target).AddHandler(System.Windows.DataObject.PastingEvent, new DataObjectPastingEventHandler(this.TextBox_Pasting));
				return;
			case 3:
				((System.Windows.Controls.ComboBox)target).SelectionChanged += new SelectionChangedEventHandler(this.cbDomainValues_SelectionChanged);
				return;
			case 4:
				((System.Windows.Controls.TextBox)target).LostFocus += new RoutedEventHandler(this.TextBox_LostFocus);
				((System.Windows.Controls.TextBox)target).PreviewTextInput += new TextCompositionEventHandler(this.Default_PreviewTextInput);
				((System.Windows.Controls.TextBox)target).AddHandler(System.Windows.DataObject.PastingEvent, new DataObjectPastingEventHandler(this.TextBox_Pasting));
				return;
			case 5:
				((System.Windows.Controls.TextBox)target).LostFocus += new RoutedEventHandler(this.TextBox_LostFocus);
				((System.Windows.Controls.TextBox)target).PreviewTextInput += new TextCompositionEventHandler(this.Default_PreviewTextInput);
				((System.Windows.Controls.TextBox)target).AddHandler(System.Windows.DataObject.PastingEvent, new DataObjectPastingEventHandler(this.TextBox_Pasting));
				return;
			default:
				return;
			}
		}
	}
}
