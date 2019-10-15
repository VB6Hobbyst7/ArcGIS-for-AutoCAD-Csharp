using AFA.Resources;
using System.ComponentModel;
using System.Windows.Forms;

namespace ESRI_TableViewer
{
    public class ValueBox : Form
	{
		private IContainer components;

		private TextBox textBox1;

		public string textBoxContents
		{
			get
			{
				return this.textBox1.Text;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ValueBox));
			this.textBox1 = new TextBox();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.textBox1, "textBox1");
			this.textBox1.Name = "textBox1";
			this.textBox1.KeyPress += new KeyPressEventHandler(this.textBox1_KeyPress);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.textBox1);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "ValueBox";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public ValueBox()
		{
			try
			{
				this.InitializeComponent();
				this.Text = AfaStrings.ValueBox;
			}
			catch
			{
			}
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}
	}
}
