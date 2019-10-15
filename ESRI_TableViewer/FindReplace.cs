using AFA.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ESRI_TableViewer
{
	public class FindReplace : Form
	{
		private DataTable dataTab;

		public static bool UsedFind = false;

		public static bool UsedReplace = false;

		public static string ReplaceText;

		public static List<DataGridViewCell> desiredCells = new List<DataGridViewCell>();

		public static int currentCellIndex = 0;

		private static bool UsedFindNext = false;

		private IContainer components;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private Button btnFindFindNext;

		private CheckBox checkBox1;

		private Button btnFindCancel;

		private Button btnFindFindAll;

		private Label label1;

		private TextBox tbFindText;

		private TabPage tabPage2;

		private Button btnReplace;

		private Button btnReplaceFindNext;

		private CheckBox checkBox2;

		private Button btnReplaceCancel;

		private Button btnReplaceAll;

		private Label label3;

		private TextBox tbReplaceText;

		private Label label2;

		private TextBox tbFindForReplace;

		public bool HasHighlightedCells
		{
			get;
			set;
		}

		public List<DataGridViewCell> DesiredCellsList
		{
			get
			{
				return FindReplace.desiredCells;
			}
		}

		public bool UseFind
		{
			get
			{
				return FindReplace.UsedFind;
			}
		}

		public bool UseReplace
		{
			get
			{
				return FindReplace.UsedReplace;
			}
		}

		public FindReplace(DataTable dataTable)
		{
			try
			{
				this.InitializeComponent();
				this.updateUI();
				this.HasHighlightedCells = false;
				this.dataTab = dataTable;
				FindReplace.desiredCells.Clear();
				FindReplace.currentCellIndex = 0;
			}
			catch
			{
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private List<DataGridViewCell> FindCells(string searchText, bool partialMatch)
		{
			List<DataGridViewCell> list = new List<DataGridViewCell>();
			if (string.IsNullOrEmpty(searchText))
			{
				return list;
			}
			searchText = searchText.ToLower();
			if (!string.IsNullOrEmpty(searchText))
			{
				searchText = searchText.ToLower();
				if (!partialMatch)
				{
					for (int i = 0; i < TableView.dg.Rows.Count; i++)
					{
						for (int j = 0; j < TableView.dg.Columns.Count; j++)
						{
							if (TableView.dg.Rows[i].Cells[j].Value != null && TableView.dg.Rows[i].Cells[j].Value.ToString().ToLower().Contains(searchText))
							{
								list.Add(TableView.dg.Rows[i].Cells[j]);
							}
						}
					}
				}
				else
				{
					for (int k = 0; k < this.dataTab.Rows.Count; k++)
					{
						for (int l = 0; l < this.dataTab.Columns.Count; l++)
						{
							if (TableView.dg.Rows[k].Cells[l].Value != null && TableView.dg.Rows[k].Cells[l].Value.ToString().ToLower() == searchText)
							{
								list.Add(TableView.dg.Rows[k].Cells[l]);
							}
						}
					}
				}
			}
			return list;
		}

		private void FindAll_Click(object sender, EventArgs e)
		{
			try
			{
				FindReplace.UsedFind = true;
				FindReplace.desiredCells.Clear();
				List<DataGridViewCell> list = this.FindCells(this.tbFindText.Text, this.checkBox1.Checked);
				if (list.Count != 0)
				{
					this.HasHighlightedCells = true;
					foreach (DataGridViewCell current in list)
					{
						current.Style.BackColor = Color.Aqua;
					}
				}
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			catch
			{
			}
		}

		private static string ReplaceEx(string original, string pattern, string replacement)
		{
			int length;
			int num = length = 0;
			string text = original.ToUpper();
			string value = pattern.ToUpper();
			int val = original.Length / pattern.Length * (replacement.Length - pattern.Length);
			char[] array = new char[original.Length + Math.Max(0, val)];
			int num2;
			while ((num2 = text.IndexOf(value, num)) != -1)
			{
				for (int i = num; i < num2; i++)
				{
					array[length++] = original[i];
				}
				for (int j = 0; j < replacement.Length; j++)
				{
					array[length++] = replacement[j];
				}
				num = num2 + pattern.Length;
			}
			if (num == 0)
			{
				return original;
			}
			for (int k = num; k < original.Length; k++)
			{
				array[length++] = original[k];
			}
			return new string(array, 0, length);
		}

		private void btnReplaceAll_Click(object sender, EventArgs e)
		{
			try
			{
				FindReplace.UsedReplace = true;
				FindReplace.desiredCells.Clear();
				List<DataGridViewCell> list = this.FindCells(this.tbFindForReplace.Text, this.checkBox2.Checked);
				if (list.Count != 0)
				{
					this.HasHighlightedCells = true;
					foreach (DataGridViewCell current in list)
					{
						current.Style.BackColor = Color.Aqua;
						if (!current.ReadOnly && current.Value != null)
						{
							string original = current.Value.ToString();
							current.Value = FindReplace.ReplaceEx(original, this.tbFindForReplace.Text, this.tbReplaceText.Text);
						}
					}
				}
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			catch
			{
			}
		}

		private void FindNext1_Click(object sender, EventArgs e)
		{
			try
			{
				if (FindReplace.currentCellIndex == FindReplace.desiredCells.Count)
				{
					FindReplace.currentCellIndex = 0;
				}
				if (FindReplace.currentCellIndex == 0)
				{
					if (this.findList(this.tbFindText.Text, this.checkBox1.Checked) && FindReplace.desiredCells.Count != 0)
					{
						this.HasHighlightedCells = true;
						FindReplace.desiredCells[FindReplace.currentCellIndex].Style.BackColor = Color.Aqua;
						TableView.dg.CurrentCell = FindReplace.desiredCells[FindReplace.currentCellIndex];
						FindReplace.currentCellIndex++;
					}
				}
				else if (FindReplace.desiredCells.Count != 0 && FindReplace.currentCellIndex < FindReplace.desiredCells.Count)
				{
					this.HasHighlightedCells = true;
					FindReplace.desiredCells[FindReplace.currentCellIndex].Style.BackColor = Color.Aqua;
					TableView.dg.CurrentCell = FindReplace.desiredCells[FindReplace.currentCellIndex];
					FindReplace.currentCellIndex++;
				}
			}
			catch
			{
			}
		}

		private void FindNext2_Click(object sender, EventArgs e)
		{
			try
			{
				FindReplace.UsedFindNext = true;
				if (FindReplace.currentCellIndex == FindReplace.desiredCells.Count)
				{
					FindReplace.currentCellIndex = 0;
				}
				if (FindReplace.currentCellIndex == 0)
				{
					if (this.findList(this.tbFindForReplace.Text, this.checkBox2.Checked))
					{
						if (FindReplace.desiredCells.Count != 0)
						{
							this.HasHighlightedCells = true;
							FindReplace.desiredCells[FindReplace.currentCellIndex].Style.BackColor = Color.Aqua;
							TableView.dg.CurrentCell = FindReplace.desiredCells[FindReplace.currentCellIndex];
							this.btnReplace.Enabled = !TableView.dg.CurrentCell.ReadOnly;
							FindReplace.currentCellIndex++;
						}
						else
						{
							this.btnReplace.Enabled = false;
						}
					}
				}
				else if (FindReplace.desiredCells.Count != 0)
				{
					if (FindReplace.currentCellIndex < FindReplace.desiredCells.Count)
					{
						this.HasHighlightedCells = true;
						FindReplace.desiredCells[FindReplace.currentCellIndex].Style.BackColor = Color.Aqua;
						TableView.dg.CurrentCell = FindReplace.desiredCells[FindReplace.currentCellIndex];
						this.btnReplace.Enabled = !TableView.dg.CurrentCell.ReadOnly;
						FindReplace.currentCellIndex++;
					}
					else
					{
						FindReplace.currentCellIndex = 0;
						this.btnReplace.Enabled = false;
					}
				}
				else
				{
					this.btnReplace.Enabled = false;
				}
			}
			catch
			{
			}
		}

		private void Replace_Click(object sender, EventArgs e)
		{
			try
			{
				if (FindReplace.UsedFindNext && !TableView.dg.CurrentCell.ReadOnly && TableView.dg.CurrentCell.Value != null)
				{
					string original = TableView.dg.CurrentCell.Value.ToString();
					TableView.dg.CurrentCell.Value = FindReplace.ReplaceEx(original, this.tbFindForReplace.Text, this.tbReplaceText.Text);
					FindReplace.UsedFindNext = false;
					this.btnReplace.Enabled = false;
				}
			}
			catch
			{
			}
		}

		public bool findList(string matchText, bool matchExact)
		{
			bool result;
			try
			{
				FindReplace.desiredCells.Clear();
				FindReplace.currentCellIndex = 0;
				FindReplace.desiredCells = this.FindCells(matchText, matchExact);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		private void tbFindText_TextChanged(object sender, EventArgs e)
		{
			if (FindReplace.desiredCells != null)
			{
				FindReplace.desiredCells.Clear();
			}
			FindReplace.currentCellIndex = 0;
		}

		private void tbFindForReplace_TextChanged(object sender, EventArgs e)
		{
			if (FindReplace.desiredCells != null)
			{
				FindReplace.desiredCells.Clear();
			}
			FindReplace.currentCellIndex = 0;
			this.btnReplace.Enabled = false;
		}

		private void tbReplaceText_TextChanged(object sender, EventArgs e)
		{
			if (FindReplace.desiredCells != null)
			{
				FindReplace.desiredCells.Clear();
			}
			FindReplace.currentCellIndex = 0;
			this.btnReplace.Enabled = false;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FindReplace));
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.btnFindFindNext = new Button();
			this.checkBox1 = new CheckBox();
			this.btnFindCancel = new Button();
			this.btnFindFindAll = new Button();
			this.label1 = new Label();
			this.tbFindText = new TextBox();
			this.tabPage2 = new TabPage();
			this.btnReplace = new Button();
			this.btnReplaceFindNext = new Button();
			this.checkBox2 = new CheckBox();
			this.btnReplaceCancel = new Button();
			this.btnReplaceAll = new Button();
			this.label3 = new Label();
			this.tbReplaceText = new TextBox();
			this.label2 = new Label();
			this.tbFindForReplace = new TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			base.SuspendLayout();
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			componentResourceManager.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabPage1.Controls.Add(this.btnFindFindNext);
			this.tabPage1.Controls.Add(this.checkBox1);
			this.tabPage1.Controls.Add(this.btnFindCancel);
			this.tabPage1.Controls.Add(this.btnFindFindAll);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.tbFindText);
			componentResourceManager.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.btnFindFindNext, "btnFindFindNext");
			this.btnFindFindNext.Name = "btnFindFindNext";
			this.btnFindFindNext.UseVisualStyleBackColor = true;
			this.btnFindFindNext.Click += new EventHandler(this.FindNext1_Click);
			componentResourceManager.ApplyResources(this.checkBox1, "checkBox1");
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.btnFindCancel, "btnFindCancel");
			this.btnFindCancel.Name = "btnFindCancel";
			this.btnFindCancel.UseVisualStyleBackColor = true;
			this.btnFindCancel.Click += new EventHandler(this.button4_Click);
			componentResourceManager.ApplyResources(this.btnFindFindAll, "btnFindFindAll");
			this.btnFindFindAll.Name = "btnFindFindAll";
			this.btnFindFindAll.UseVisualStyleBackColor = true;
			this.btnFindFindAll.Click += new EventHandler(this.FindAll_Click);
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.tbFindText, "tbFindText");
			this.tbFindText.Name = "tbFindText";
			this.tbFindText.TextChanged += new EventHandler(this.tbFindText_TextChanged);
			this.tabPage2.Controls.Add(this.btnReplace);
			this.tabPage2.Controls.Add(this.btnReplaceFindNext);
			this.tabPage2.Controls.Add(this.checkBox2);
			this.tabPage2.Controls.Add(this.btnReplaceCancel);
			this.tabPage2.Controls.Add(this.btnReplaceAll);
			this.tabPage2.Controls.Add(this.label3);
			this.tabPage2.Controls.Add(this.tbReplaceText);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Controls.Add(this.tbFindForReplace);
			componentResourceManager.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.btnReplace, "btnReplace");
			this.btnReplace.Name = "btnReplace";
			this.btnReplace.UseVisualStyleBackColor = true;
			this.btnReplace.Click += new EventHandler(this.Replace_Click);
			componentResourceManager.ApplyResources(this.btnReplaceFindNext, "btnReplaceFindNext");
			this.btnReplaceFindNext.Name = "btnReplaceFindNext";
			this.btnReplaceFindNext.UseVisualStyleBackColor = true;
			this.btnReplaceFindNext.Click += new EventHandler(this.FindNext2_Click);
			componentResourceManager.ApplyResources(this.checkBox2, "checkBox2");
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.btnReplaceCancel, "btnReplaceCancel");
			this.btnReplaceCancel.Name = "btnReplaceCancel";
			this.btnReplaceCancel.UseVisualStyleBackColor = true;
			this.btnReplaceCancel.Click += new EventHandler(this.button3_Click);
			componentResourceManager.ApplyResources(this.btnReplaceAll, "btnReplaceAll");
			this.btnReplaceAll.Name = "btnReplaceAll";
			this.btnReplaceAll.UseVisualStyleBackColor = true;
			this.btnReplaceAll.Click += new EventHandler(this.btnReplaceAll_Click);
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.tbReplaceText, "tbReplaceText");
			this.tbReplaceText.Name = "tbReplaceText";
			this.tbReplaceText.TextChanged += new EventHandler(this.tbReplaceText_TextChanged);
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.tbFindForReplace, "tbFindForReplace");
			this.tbFindForReplace.Name = "tbFindForReplace";
			this.tbFindForReplace.TextChanged += new EventHandler(this.tbFindForReplace_TextChanged);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tabControl1);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.Name = "FindReplace";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			base.ResumeLayout(false);
		}

		private void updateUI()
		{
			try
			{
				this.Text = AfaStrings.FindAndReplace;
				this.tabPage1.Text = AfaStrings.Find;
				this.tabPage2.Text = AfaStrings.Replace;
				this.label1.Text = AfaStrings.FindWhat;
				this.checkBox1.Text = AfaStrings.WholeWordOnly;
				this.btnFindFindNext.Text = AfaStrings.FindNext;
				this.btnFindFindAll.Text = AfaStrings.FindAll;
				this.btnFindCancel.Text = AfaStrings.Cancel;
				this.label2.Text = AfaStrings.FindWhat;
				this.label3.Text = AfaStrings.ReplaceWith;
				this.checkBox2.Text = AfaStrings.WholeWordOnly;
				this.btnReplaceFindNext.Text = AfaStrings.FindNext;
				this.btnReplace.Text = AfaStrings.Replace;
				this.btnReplaceAll.Text = AfaStrings.ReplaceAll;
				this.btnReplaceCancel.Text = AfaStrings.Cancel;
			}
			catch
			{
			}
		}
	}
}
