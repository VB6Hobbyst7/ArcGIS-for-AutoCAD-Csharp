using AFA;
using AFA.Resources;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Utility;
using Application = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;
using Exception = Autodesk.AutoCAD.Runtime.Exception;

namespace SelectByAttributes
{
    public class SelectByAttributesForm : Form
	{
		public static List<ObjectId> oidsList = new List<ObjectId>();

		private static List<string> typeOfAttribute = new List<string>();

		private DataTable dataTable = new DataTable();

		private DataRow[] foundRows;

		private IContainer components;

		private Label featureClassLabel;

		private ComboBox featureClassComboBox;

		private Label methodLabel;

		private ComboBox methodsComboBox;

		private ListBox featureClassFieldsListBox;

		private Button equalButton;

		private Button notEqualButton;

		private Button likeButton;

		private Button notButton;

		private Button orButton;

		private Button lessThanEqualButton;

		private Button lessThanButton;

		private Button andButton;

		private Button greaterThanEqualButton;

		private Button greaterThanButton;

		private Button parenthesesButton;

		private Button IsButton;

		private Button uniqueValuesButton;

		private Button percentButton;

		private ListBox uniqueValuesListBox;

		private TextBox gotoTextBox;

		private Label gotoLabel;

		private Label sampleSQLLabel;

		private TextBox sqlQueryEntryTextBox;

		private Button verifyButton;

		private Button helpButton;

		private Button applyButton;

		private Button closeButton;

		public SelectByAttributesForm()
		{
			this.InitializeComponent();
			this.disableUI();
		}

		public SelectByAttributesForm(bool launchedByTableViewer, MSCFeatureClass selectedFeatureClass)
		{
			if (selectedFeatureClass == null)
			{
				MessageBox.Show("Select a feature class first");
				return;
			}
			this.InitializeComponent();
			this.updateMethodsComboBox();
			this.updateFeatureClassComboBox(selectedFeatureClass);
			this.verifyButton.Enabled = false;
			this.applyButton.Enabled = false;
		}

		private void disableUI()
		{
			this.featureClassComboBox.Enabled = false;
			this.methodsComboBox.Enabled = false;
			this.verifyButton.Enabled = false;
			this.applyButton.Enabled = false;
			this.sqlQueryEntryTextBox.Enabled = false;
			this.featureClassFieldsListBox.Enabled = false;
			this.uniqueValuesButton.Enabled = false;
			this.uniqueValuesListBox.Enabled = false;
			this.equalButton.Enabled = false;
			this.notEqualButton.Enabled = false;
			this.likeButton.Enabled = false;
			this.greaterThanButton.Enabled = false;
			this.greaterThanEqualButton.Enabled = false;
			this.andButton.Enabled = false;
			this.lessThanButton.Enabled = false;
			this.lessThanEqualButton.Enabled = false;
			this.orButton.Enabled = false;
			this.percentButton.Enabled = false;
			this.parenthesesButton.Enabled = false;
			this.notButton.Enabled = false;
			this.IsButton.Enabled = false;
			this.gotoTextBox.Enabled = false;
			this.featureClassFieldsListBox.BackColor = this.BackColor;
			this.uniqueValuesListBox.BackColor = this.BackColor;
			this.gotoTextBox.BackColor = this.BackColor;
			this.sqlQueryEntryTextBox.BackColor = this.BackColor;
		}

		private void updateMethodsComboBox()
		{
			this.methodsComboBox.Items.Add(AfaStrings.createNewSelection);
			this.methodsComboBox.Items.Add(AfaStrings.addToCurrentSelection);
			this.methodsComboBox.Items.Add(AfaStrings.removeFCurrentSelection);
			this.methodsComboBox.Items.Add(AfaStrings.selectFCurrentSelection);
			this.methodsComboBox.SelectedIndex = 0;
		}

		private void updateFeatureClassComboBox(MSCFeatureClass selectedFeatureClass)
		{
			if (selectedFeatureClass == null)
			{
				return;
			}
			try
			{
				foreach (MSCFeatureClass current in AfaDocData.ActiveDocData.DocDataset.FeatureClasses.Values)
				{
					this.featureClassComboBox.Items.Add(current);
					if (current.SubTypes.Count > 0)
					{
						foreach (MSCFeatureClass current2 in current.SubTypes)
						{
							this.featureClassComboBox.Items.Add(current2);
						}
					}
				}
				foreach (MSCFeatureService current3 in AfaDocData.ActiveDocData.DocDataset.FeatureServices.Values)
				{
					this.featureClassComboBox.Items.Add(current3);
					if (current3.SubTypes.Count > 0)
					{
						foreach (MSCFeatureClass current4 in current3.SubTypes)
						{
							this.featureClassComboBox.Items.Add(current4);
						}
					}
				}
				if (selectedFeatureClass != null)
				{
					this.featureClassComboBox.SelectedItem = selectedFeatureClass;
				}
				else
				{
					this.featureClassComboBox.SelectedIndex = 0;
				}
			}
			catch (Exception ex)
			{
				if (SingletonsList.ed != null)
				{
					SingletonsList.ed.WriteMessage("\n" + AfaStrings.Error + " " + ex.Message);
				}
			}
		}

		private void updateDataTable(MSCFeatureClass fc)
		{
			this.dataTable = fc.GetDataTable();
		}

		private void featureClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.featureClassFieldsListBox.Items.Clear();
				this.uniqueValuesListBox.Items.Clear();
				MSCFeatureClass mSCFeatureClass = (MSCFeatureClass)this.featureClassComboBox.SelectedItem;
				foreach (CadField current in mSCFeatureClass.Fields)
				{
					this.featureClassFieldsListBox.Items.Add(current.Name);
				}
				AfaDocData.ActiveDocData.SetActiveFeatureClass(mSCFeatureClass);
				ArcGISRibbon.SetActiveFeatureClass(mSCFeatureClass);
				this.updateDataTable((MSCFeatureClass)this.featureClassComboBox.SelectedItem);
				this.uniqueValuesButton.Enabled = false;
				this.uniqueValuesListBox.Items.Clear();
				this.uniqueValuesListBox.Enabled = false;
				this.uniqueValuesListBox.BackColor = this.BackColor;
				this.gotoTextBox.Enabled = false;
				this.gotoTextBox.BackColor = this.BackColor;
			}
			catch (Exception ex)
			{
				if (SingletonsList.ed != null)
				{
					SingletonsList.ed.WriteMessage("\n" + AfaStrings.Error + " " + ex.Message);
				}
			}
			catch (SystemException ex2)
			{
				if (SingletonsList.ed != null)
				{
					SingletonsList.ed.WriteMessage("\n" + AfaStrings.Error + " " + ex2.Message);
				}
			}
		}

		public ObjectId[] featureClassChecker(MSCFeatureClass fc)
		{
			ObjectId[] result;
			try
			{
				ResultBuffer fCQuery = fc.FCQuery;
				SelectionFilter selectionFilter = new SelectionFilter(fCQuery.AsArray());
				PromptSelectionResult promptSelectionResult = SingletonsList.ed.SelectAll(selectionFilter);
				if (promptSelectionResult.Status == (PromptStatus)5100 && promptSelectionResult.Value.Count > 0)
				{
					ObjectId[] objectIds = promptSelectionResult.Value.GetObjectIds();
					if (objectIds.Length != 0)
					{
						result = objectIds;
						return result;
					}
				}
				result = null;
			}
			catch (Exception ex)
			{
				if (SingletonsList.ed != null)
				{
					SingletonsList.ed.WriteMessage("\n" + AfaStrings.Error + " " + ex.Message);
				}
				result = null;
			}
			return result;
		}

		private void uniqueValuesButton_Click(object sender, EventArgs e)
		{
			try
			{
				this.uniqueValuesListBox.Items.Clear();
				this.uniqueValuesListBox.Enabled = true;
				this.uniqueValuesListBox.BackColor = Color.White;
				this.gotoTextBox.Enabled = true;
				this.gotoTextBox.BackColor = Color.White;
				if (this.featureClassFieldsListBox.SelectedItems.Count == 1)
				{
					string columnName = this.featureClassFieldsListBox.SelectedItem.ToString();
					List<string> list = new List<string>();
					int num = this.dataTable.Columns.IndexOf(columnName);
					if (num != -1)
					{
						for (int i = 0; i < this.dataTable.Rows.Count; i++)
						{
							if (!string.IsNullOrEmpty(this.dataTable.Rows[i][num].ToString()) && !list.Contains(this.dataTable.Rows[i][num].ToString()))
							{
								list.Add(this.dataTable.Rows[i][num].ToString());
							}
						}
						if (list != null)
						{
							list.Sort();
							for (int i = 0; i < list.Count; i++)
							{
								if (!string.IsNullOrEmpty(list[i]))
								{
									this.uniqueValuesListBox.Items.Add(list[i]);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (SingletonsList.ed != null)
				{
					SingletonsList.ed.WriteMessage("\n" + AfaStrings.Error + " " + ex.Message);
				}
			}
		}

		private void equalButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" = ");
		}

		private void notEqualButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" <> ");
		}

		private void likeButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" LIKE ");
		}

		private void greaterThanButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" > ");
		}

		private void greaterThanEqualButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" >= ");
		}

		private void andButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" AND ");
		}

		private void lessThanButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" < ");
		}

		private void lessThanEqualButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" <= ");
		}

		private void orButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" OR ");
		}

		private void percentButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" % ");
		}

		private void parenthesesButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" () ");
		}

		private void notButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" NOT ");
		}

		private void IsButton_Click(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(" IS ");
		}

		private void verifyButton_Click(object sender, EventArgs e)
		{
			try
			{
				this.dataTable.Select(this.sqlQueryEntryTextBox.Text);
				MessageBox.Show(AfaStrings.expressionVerify);
			}
			catch (SyntaxErrorException ex)
			{
				MessageBox.Show(AfaStrings.expressionVerifyError + "\n" + ex.Message);
			}
			catch
			{
				MessageBox.Show(AfaStrings.expressionVerifyError);
			}
		}

		private void helpButton_Click(object sender, EventArgs e)
		{
			try
			{
				string url = ArcGISRibbon.BuildHelpPath().ToString();
				Help.ShowHelp(this, url, HelpNavigator.TopicId);
			}
			catch
			{
			}
		}

		private void applyButton_Click(object sender, EventArgs e)
		{
            //try
            //{
            //	if (!string.IsNullOrEmpty(this.sqlQueryEntryTextBox.Text))
            //	{
            //		try
            //		{
            //			this.foundRows = this.dataTable.Select(this.sqlQueryEntryTextBox.Text);
            //		}
            //		catch (SyntaxErrorException ex)
            //		{
            //			MessageBox.Show(ex.Message);
            //			return;
            //		}
            //		catch
            //		{
            //			MessageBox.Show(AfaStrings.queryError);
            //			return;
            //		}
            //		if (this.foundRows.Length == 0)
            //		{
            //			MessageBox.Show(AfaStrings.noEntitiesQuery);
            //		}
            //		else
            //		{
            //			switch (this.methodsComboBox.SelectedIndex)
            //			{
            //			case 0:
            //				IL_94:
            //				SelectByAttributesForm.oidsList.Clear();
            //				for (int i = 0; i < this.foundRows.Length; i++)
            //				{
            //					ObjectId item = new ObjectId((IntPtr)this.foundRows[i].ItemArray[0]);
            //					if (!item.IsNull)
            //					{
            //						SelectByAttributesForm.oidsList.Add(item);
            //					}
            //				}
            //				goto IL_25E;
            //			case 1:
            //				for (int i = 0; i < this.foundRows.Length; i++)
            //				{
            //					ObjectId item2 = new ObjectId((IntPtr)this.foundRows[i].ItemArray[0]);
            //					if (!SelectByAttributesForm.oidsList.Contains(item2) && !item2.IsNull)
            //					{
            //						SelectByAttributesForm.oidsList.Add(item2);
            //					}
            //				}
            //				goto IL_25E;
            //			case 2:
            //				for (int i = 0; i < this.foundRows.Length; i++)
            //				{
            //					if (SelectByAttributesForm.oidsList.Count != 0)
            //					{
            //						try
            //						{
            //							ObjectId item3 = new ObjectId((IntPtr)this.foundRows[i].ItemArray[0]);
            //							if (!item3.IsNull && SelectByAttributesForm.oidsList.Contains(item3))
            //							{
            //								SelectByAttributesForm.oidsList.RemoveAt(SelectByAttributesForm.oidsList.IndexOf(item3));
            //							}
            //						}
            //						catch (InvalidCastException ex2)
            //						{
            //							MessageBox.Show(ex2.Message);
            //							return;
            //						}
            //					}
            //				}
            //				goto IL_25E;
            //			case 3:
            //			{
            //				List<ObjectId> list = new List<ObjectId>();
            //				if (SelectByAttributesForm.oidsList.Count != 0)
            //				{
            //					for (int i = 0; i < SelectByAttributesForm.oidsList.Count; i++)
            //					{
            //						list.Add(SelectByAttributesForm.oidsList[i]);
            //					}
            //					SelectByAttributesForm.oidsList.Clear();
            //					for (int i = 0; i < this.foundRows.Length; i++)
            //					{
            //						try
            //						{
            //							ObjectId item4 = (ObjectId)this.foundRows[i].ItemArray[0];
            //							if (!item4.IsNull && list.Contains(item4))
            //							{
            //								SelectByAttributesForm.oidsList.Add(item4);
            //							}
            //						}
            //						catch (InvalidCastException ex3)
            //						{
            //							MessageBox.Show(ex3.Message);
            //							return;
            //						}
            //					}
            //					goto IL_25E;
            //				}
            //				goto IL_25E;
            //			}
            //			}
            //			goto IL_94;
            //			IL_25E:
            //			if (SelectByAttributesForm.oidsList.Count != 0)
            //			{
            //				this.zoomToEntities();
            //			}
            //		}
            //	}
            //}
            //catch (Exception ex4)
            //{
            //	if (SingletonsList.ed != null)
            //	{
            //		SingletonsList.ed.WriteMessage("\n" + AfaStrings.Error + " " + ex4.Message);
            //	}
            //}
            try
            {
                if (!string.IsNullOrEmpty(this.sqlQueryEntryTextBox.Text))
                {
                    try
                    {
                        this.foundRows = this.dataTable.Select(this.sqlQueryEntryTextBox.Text);
                    }
                    catch (SyntaxErrorException exception1)
                    {
                        MessageBox.Show(exception1.Message);
                        return;
                    }
                    catch
                    {
                        MessageBox.Show(AfaStrings.queryError);
                        return;
                    }
                    if (this.foundRows.Length != 0)
                    {
                        int num;
                        switch (this.methodsComboBox.SelectedIndex)
                        {
                            case 1:
                                num = 0;
                                while (num < this.foundRows.Length)
                                {
                                    ObjectId item = new ObjectId((IntPtr)this.foundRows[num].ItemArray[0]);
                                    if (!oidsList.Contains(item) && !item.IsNull)
                                    {
                                        oidsList.Add(item);
                                    }
                                    num++;
                                }
                                break;

                            case 2:
                                num = 0;
                                while (true)
                                {
                                    if (num >= this.foundRows.Length)
                                    {
                                        break;
                                    }
                                    if (oidsList.Count != 0)
                                    {
                                        try
                                        {
                                            ObjectId item = new ObjectId((IntPtr)this.foundRows[num].ItemArray[0]);
                                            if (!item.IsNull && oidsList.Contains(item))
                                            {
                                                oidsList.RemoveAt(oidsList.IndexOf(item));
                                            }
                                        }
                                        catch (InvalidCastException exception5)
                                        {
                                            MessageBox.Show(exception5.Message);
                                            return;
                                        }
                                    }
                                    num++;
                                }
                                break;

                            case 3:
                                {
                                    List<ObjectId> list = new List<ObjectId>();
                                    if (oidsList.Count != 0)
                                    {
                                        num = 0;
                                        while (true)
                                        {
                                            if (num < oidsList.Count)
                                            {
                                                list.Add(oidsList[num]);
                                                num++;
                                                continue;
                                            }
                                            oidsList.Clear();
                                            num = 0;
                                            while (true)
                                            {
                                                if (num >= this.foundRows.Length)
                                                {
                                                    break;
                                                }
                                                try
                                                {
                                                    ObjectId item = (ObjectId)this.foundRows[num].ItemArray[0];
                                                    if (!item.IsNull && list.Contains(item))
                                                    {
                                                        oidsList.Add(item);
                                                    }
                                                }
                                                catch (InvalidCastException exception6)
                                                {
                                                    MessageBox.Show(exception6.Message);
                                                    return;
                                                }
                                                num++;
                                            }
                                            break;
                                        }
                                    }
                                    break;
                                }
                            default:
                                oidsList.Clear();
                                for (num = 0; num < this.foundRows.Length; num++)
                                {
                                    ObjectId item = new ObjectId((IntPtr)this.foundRows[num].ItemArray[0]);
                                    if (!item.IsNull)
                                    {
                                        oidsList.Add(item);
                                    }
                                }
                                break;
                        }
                        if (oidsList.Count != 0)
                        {
                            this.zoomToEntities();
                        }
                    }
                    else
                    {
                        MessageBox.Show(AfaStrings.noEntitiesQuery);
                    }
                }
            }
            catch (Exception exception4)
            {
                if (SingletonsList.ed != null)
                {
                    SingletonsList.ed.WriteMessage("\n" + AfaStrings.Error + " " + exception4.Message);
                }
            }
        }

		private List<ObjectId> GetSubEntities(List<ObjectId> initialList, Transaction t)
		{
			List<ObjectId> list = new List<ObjectId>();
			foreach (ObjectId current in initialList)
			{
				list.Add(current);
				if (current.ObjectClass.DxfName== "GROUP")
				{
					Group group = (Group)t.GetObject(current, 0);
					ObjectId[] allEntityIds = group.GetAllEntityIds();
					ObjectId[] array = allEntityIds;
					for (int i = 0; i < array.Length; i++)
					{
						ObjectId item = array[i];
						list.Add(item);
					}
				}
			}
			return list;
		}

		private void zoomToEntities()
		{
			try
			{
				Document document = AfaDocData.ActiveDocData.Document;
				Application.UseWaitCursor = true;
				document.Editor.SetImpliedSelection(DocUtil.ExpandGroupObjectIds(document, SelectByAttributesForm.oidsList.ToArray()));
				Utils.ZoomObjects(false);
				Application.UseWaitCursor = false;
				document.TransactionManager.QueueForGraphicsFlush();
				document.TransactionManager.FlushGraphics();
				document.Editor.UpdateScreen();
				AfaDocData.ActiveDocData.DocDataset.UpdateMaps();
			}
			catch (Exception ex)
			{
				if (SingletonsList.ed != null)
				{
					SingletonsList.ed.WriteMessage("\n" + AfaStrings.Error + " " + ex.Message);
				}
			}
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			base.Close();
			base.DialogResult = DialogResult.OK;
		}

		private void featureClassFieldsListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.uniqueValuesButton.Enabled = true;
			this.uniqueValuesListBox.Items.Clear();
			this.uniqueValuesListBox.Enabled = false;
			this.uniqueValuesListBox.BackColor = this.BackColor;
			this.gotoTextBox.Enabled = false;
			this.gotoTextBox.BackColor = this.BackColor;
		}

		private void featureClassFieldsListBox_DoubleClick(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText(this.featureClassFieldsListBox.SelectedItem.ToString() + " ");
		}

		private void uniqueFieldsListBox_DoubleClick(object sender, EventArgs e)
		{
			this.sqlQueryEntryTextBox.AppendText("'" + this.uniqueValuesListBox.SelectedItem.ToString() + "'");
		}

		private void sqlQueryEntryTextBox_TextChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.sqlQueryEntryTextBox.Text))
			{
				this.applyButton.Enabled = false;
				this.verifyButton.Enabled = false;
				return;
			}
			this.applyButton.Enabled = true;
			this.verifyButton.Enabled = true;
		}

		private void gotoTextBox_TextChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < this.uniqueValuesListBox.Items.Count; i++)
			{
				if (this.uniqueValuesListBox.Items[i].ToString().StartsWith(this.gotoTextBox.Text))
				{
					this.uniqueValuesListBox.SelectedIndex = i;
					return;
				}
			}
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SelectByAttributesForm));
			this.featureClassLabel = new Label();
			this.featureClassComboBox = new ComboBox();
			this.methodLabel = new Label();
			this.methodsComboBox = new ComboBox();
			this.featureClassFieldsListBox = new ListBox();
			this.equalButton = new Button();
			this.notEqualButton = new Button();
			this.likeButton = new Button();
			this.notButton = new Button();
			this.orButton = new Button();
			this.lessThanEqualButton = new Button();
			this.lessThanButton = new Button();
			this.andButton = new Button();
			this.greaterThanEqualButton = new Button();
			this.greaterThanButton = new Button();
			this.parenthesesButton = new Button();
			this.IsButton = new Button();
			this.uniqueValuesButton = new Button();
			this.percentButton = new Button();
			this.uniqueValuesListBox = new ListBox();
			this.gotoTextBox = new TextBox();
			this.gotoLabel = new Label();
			this.sampleSQLLabel = new Label();
			this.sqlQueryEntryTextBox = new TextBox();
			this.verifyButton = new Button();
			this.helpButton = new Button();
			this.applyButton = new Button();
			this.closeButton = new Button();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.featureClassLabel, "featureClassLabel");
			this.featureClassLabel.Name = "featureClassLabel";
			this.featureClassComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.featureClassComboBox.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.featureClassComboBox, "featureClassComboBox");
			this.featureClassComboBox.Name = "featureClassComboBox";
			this.featureClassComboBox.SelectedIndexChanged += new EventHandler(this.featureClassComboBox_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.methodLabel, "methodLabel");
			this.methodLabel.Name = "methodLabel";
			this.methodsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.methodsComboBox.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.methodsComboBox, "methodsComboBox");
			this.methodsComboBox.Name = "methodsComboBox";
			this.featureClassFieldsListBox.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.featureClassFieldsListBox, "featureClassFieldsListBox");
			this.featureClassFieldsListBox.Name = "featureClassFieldsListBox";
			this.featureClassFieldsListBox.SelectedIndexChanged += new EventHandler(this.featureClassFieldsListBox_SelectedIndexChanged);
			this.featureClassFieldsListBox.DoubleClick += new EventHandler(this.featureClassFieldsListBox_DoubleClick);
			componentResourceManager.ApplyResources(this.equalButton, "equalButton");
			this.equalButton.Name = "equalButton";
			this.equalButton.UseVisualStyleBackColor = true;
			this.equalButton.Click += new EventHandler(this.equalButton_Click);
			componentResourceManager.ApplyResources(this.notEqualButton, "notEqualButton");
			this.notEqualButton.Name = "notEqualButton";
			this.notEqualButton.UseVisualStyleBackColor = true;
			this.notEqualButton.Click += new EventHandler(this.notEqualButton_Click);
			componentResourceManager.ApplyResources(this.likeButton, "likeButton");
			this.likeButton.Name = "likeButton";
			this.likeButton.UseVisualStyleBackColor = true;
			this.likeButton.Click += new EventHandler(this.likeButton_Click);
			componentResourceManager.ApplyResources(this.notButton, "notButton");
			this.notButton.Name = "notButton";
			this.notButton.UseVisualStyleBackColor = true;
			this.notButton.Click += new EventHandler(this.notButton_Click);
			componentResourceManager.ApplyResources(this.orButton, "orButton");
			this.orButton.Name = "orButton";
			this.orButton.UseVisualStyleBackColor = true;
			this.orButton.Click += new EventHandler(this.orButton_Click);
			componentResourceManager.ApplyResources(this.lessThanEqualButton, "lessThanEqualButton");
			this.lessThanEqualButton.Name = "lessThanEqualButton";
			this.lessThanEqualButton.UseVisualStyleBackColor = true;
			this.lessThanEqualButton.Click += new EventHandler(this.lessThanEqualButton_Click);
			componentResourceManager.ApplyResources(this.lessThanButton, "lessThanButton");
			this.lessThanButton.Name = "lessThanButton";
			this.lessThanButton.UseVisualStyleBackColor = true;
			this.lessThanButton.Click += new EventHandler(this.lessThanButton_Click);
			componentResourceManager.ApplyResources(this.andButton, "andButton");
			this.andButton.Name = "andButton";
			this.andButton.UseVisualStyleBackColor = true;
			this.andButton.Click += new EventHandler(this.andButton_Click);
			componentResourceManager.ApplyResources(this.greaterThanEqualButton, "greaterThanEqualButton");
			this.greaterThanEqualButton.Name = "greaterThanEqualButton";
			this.greaterThanEqualButton.UseVisualStyleBackColor = true;
			this.greaterThanEqualButton.Click += new EventHandler(this.greaterThanEqualButton_Click);
			componentResourceManager.ApplyResources(this.greaterThanButton, "greaterThanButton");
			this.greaterThanButton.Name = "greaterThanButton";
			this.greaterThanButton.UseVisualStyleBackColor = true;
			this.greaterThanButton.Click += new EventHandler(this.greaterThanButton_Click);
			componentResourceManager.ApplyResources(this.parenthesesButton, "parenthesesButton");
			this.parenthesesButton.Name = "parenthesesButton";
			this.parenthesesButton.UseVisualStyleBackColor = true;
			this.parenthesesButton.Click += new EventHandler(this.parenthesesButton_Click);
			componentResourceManager.ApplyResources(this.IsButton, "IsButton");
			this.IsButton.Name = "IsButton";
			this.IsButton.UseVisualStyleBackColor = true;
			this.IsButton.Click += new EventHandler(this.IsButton_Click);
			componentResourceManager.ApplyResources(this.uniqueValuesButton, "uniqueValuesButton");
			this.uniqueValuesButton.Name = "uniqueValuesButton";
			this.uniqueValuesButton.UseVisualStyleBackColor = true;
			this.uniqueValuesButton.Click += new EventHandler(this.uniqueValuesButton_Click);
			componentResourceManager.ApplyResources(this.percentButton, "percentButton");
			this.percentButton.Name = "percentButton";
			this.percentButton.UseVisualStyleBackColor = true;
			this.percentButton.Click += new EventHandler(this.percentButton_Click);
			this.uniqueValuesListBox.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.uniqueValuesListBox, "uniqueValuesListBox");
			this.uniqueValuesListBox.Name = "uniqueValuesListBox";
			this.uniqueValuesListBox.DoubleClick += new EventHandler(this.uniqueFieldsListBox_DoubleClick);
			componentResourceManager.ApplyResources(this.gotoTextBox, "gotoTextBox");
			this.gotoTextBox.Name = "gotoTextBox";
			this.gotoTextBox.TextChanged += new EventHandler(this.gotoTextBox_TextChanged);
			componentResourceManager.ApplyResources(this.gotoLabel, "gotoLabel");
			this.gotoLabel.MinimumSize = new Size(107, 13);
			this.gotoLabel.Name = "gotoLabel";
			componentResourceManager.ApplyResources(this.sampleSQLLabel, "sampleSQLLabel");
			this.sampleSQLLabel.Name = "sampleSQLLabel";
			componentResourceManager.ApplyResources(this.sqlQueryEntryTextBox, "sqlQueryEntryTextBox");
			this.sqlQueryEntryTextBox.Name = "sqlQueryEntryTextBox";
			this.sqlQueryEntryTextBox.TextChanged += new EventHandler(this.sqlQueryEntryTextBox_TextChanged);
			componentResourceManager.ApplyResources(this.verifyButton, "verifyButton");
			this.verifyButton.Name = "verifyButton";
			this.verifyButton.UseVisualStyleBackColor = true;
			this.verifyButton.Click += new EventHandler(this.verifyButton_Click);
			componentResourceManager.ApplyResources(this.helpButton, "helpButton");
			this.helpButton.Name = "helpButton";
			this.helpButton.UseVisualStyleBackColor = true;
			this.helpButton.Click += new EventHandler(this.helpButton_Click);
			componentResourceManager.ApplyResources(this.applyButton, "applyButton");
			this.applyButton.Name = "applyButton";
			this.applyButton.UseVisualStyleBackColor = true;
			this.applyButton.Click += new EventHandler(this.applyButton_Click);
			componentResourceManager.ApplyResources(this.closeButton, "closeButton");
			this.closeButton.Name = "closeButton";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new EventHandler(this.closeButton_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.closeButton);
			base.Controls.Add(this.applyButton);
			base.Controls.Add(this.helpButton);
			base.Controls.Add(this.verifyButton);
			base.Controls.Add(this.sqlQueryEntryTextBox);
			base.Controls.Add(this.sampleSQLLabel);
			base.Controls.Add(this.gotoLabel);
			base.Controls.Add(this.gotoTextBox);
			base.Controls.Add(this.uniqueValuesListBox);
			base.Controls.Add(this.percentButton);
			base.Controls.Add(this.uniqueValuesButton);
			base.Controls.Add(this.IsButton);
			base.Controls.Add(this.parenthesesButton);
			base.Controls.Add(this.greaterThanButton);
			base.Controls.Add(this.greaterThanEqualButton);
			base.Controls.Add(this.andButton);
			base.Controls.Add(this.lessThanButton);
			base.Controls.Add(this.lessThanEqualButton);
			base.Controls.Add(this.orButton);
			base.Controls.Add(this.notButton);
			base.Controls.Add(this.likeButton);
			base.Controls.Add(this.notEqualButton);
			base.Controls.Add(this.equalButton);
			base.Controls.Add(this.featureClassFieldsListBox);
			base.Controls.Add(this.methodsComboBox);
			base.Controls.Add(this.methodLabel);
			base.Controls.Add(this.featureClassComboBox);
			base.Controls.Add(this.featureClassLabel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SelectByAttributesForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
