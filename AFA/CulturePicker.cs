using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace AFA
{
	public class CulturePicker : Form
	{
		private IContainer components;

		private Button btnDefault;

		private Button btnEnglish;

		private Button btnFrench;

		private Button btnGerman;

		public CultureInfo SelectedCultureInfo
		{
			get;
			private set;
		}

		public CulturePicker()
		{
			this.InitializeComponent();
		}

		private void btnDefault_Click(object sender, EventArgs e)
		{
			this.SelectedCultureInfo = Thread.CurrentThread.CurrentCulture;
			base.Close();
		}

		private void btnEnglish_Click(object sender, EventArgs e)
		{
			this.SelectedCultureInfo = new CultureInfo("en-US");
			base.Close();
		}

		private void btnFrench_Click(object sender, EventArgs e)
		{
			this.SelectedCultureInfo = new CultureInfo("fr-FR");
			base.Close();
		}

		private void btnGerman_Click(object sender, EventArgs e)
		{
			this.SelectedCultureInfo = new CultureInfo("de-DE");
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
			this.btnDefault = new Button();
			this.btnEnglish = new Button();
			this.btnFrench = new Button();
			this.btnGerman = new Button();
			base.SuspendLayout();
			this.btnDefault.Location = new Point(99, 46);
			this.btnDefault.Name = "btnDefault";
			this.btnDefault.Size = new Size(110, 24);
			this.btnDefault.TabIndex = 0;
			this.btnDefault.Text = "Default";
			this.btnDefault.UseVisualStyleBackColor = true;
			this.btnDefault.Click += new EventHandler(this.btnDefault_Click);
			this.btnEnglish.Location = new Point(99, 90);
			this.btnEnglish.Name = "btnEnglish";
			this.btnEnglish.Size = new Size(110, 24);
			this.btnEnglish.TabIndex = 1;
			this.btnEnglish.Text = "US English";
			this.btnEnglish.UseVisualStyleBackColor = true;
			this.btnEnglish.Click += new EventHandler(this.btnEnglish_Click);
			this.btnFrench.Location = new Point(99, 178);
			this.btnFrench.Name = "btnFrench";
			this.btnFrench.Size = new Size(110, 24);
			this.btnFrench.TabIndex = 4;
			this.btnFrench.Text = "French";
			this.btnFrench.UseVisualStyleBackColor = true;
			this.btnFrench.Click += new EventHandler(this.btnFrench_Click);
			this.btnGerman.Location = new Point(99, 134);
			this.btnGerman.Name = "btnGerman";
			this.btnGerman.Size = new Size(110, 24);
			this.btnGerman.TabIndex = 6;
			this.btnGerman.Text = "German";
			this.btnGerman.UseVisualStyleBackColor = true;
			this.btnGerman.Click += new EventHandler(this.btnGerman_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(284, 229);
			base.Controls.Add(this.btnGerman);
			base.Controls.Add(this.btnFrench);
			base.Controls.Add(this.btnEnglish);
			base.Controls.Add(this.btnDefault);
			base.Name = "CulturePicker";
			this.Text = "Testing:  Select Culture";
			base.ResumeLayout(false);
		}
	}
}
