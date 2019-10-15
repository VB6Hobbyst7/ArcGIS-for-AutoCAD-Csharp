using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AFA.Test.AsynchTest
{
	public class frmGoogleData : Form
	{
		private IContainer components;

		private Label lblData;

		private Button btnClose;

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
			this.lblData = new Label();
			this.btnClose = new Button();
			base.SuspendLayout();
			this.lblData.AutoSize = true;
			this.lblData.Location = new Point(18, 30);
			this.lblData.Name = "lblData";
			this.lblData.Size = new Size(35, 13);
			this.lblData.TabIndex = 0;
			this.lblData.Text = "label1";
			this.btnClose.Location = new Point(195, 219);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new Size(70, 25);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(284, 262);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.lblData);
			base.Name = "frmGoogleData";
			this.Text = "Google Data";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public frmGoogleData()
		{
			this.InitializeComponent();
		}

		public frmGoogleData(object taskData)
		{
			this.InitializeComponent();
			this.SetData(taskData);
		}

		public void SetData(object taskData)
		{
			List<string> list = taskData as List<string>;
			if (list != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string current in list)
				{
					stringBuilder.Append(current + "\n");
				}
				this.lblData.Text = stringBuilder.ToString();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Visible = false;
		}

		private void frmGoogleData_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			base.Visible = false;
		}
	}
}
