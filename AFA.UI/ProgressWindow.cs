using AFA.Resources;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AFA.UI
{
	public class ProgressWindow : Form
	{
		private IContainer components;

		private Label lblTitle;

		private Label lblMessage;

		private ProgressBar pBar;

		private Button btnCancel;

		public bool UserCancelled
		{
			get;
			set;
		}

		public string ProgressTitleMessage
		{
			get
			{
				return this.lblTitle.Text;
			}
			set
			{
				this.lblTitle.Text = value;
			}
		}

		public string ProgressDetailMessage
		{
			get
			{
				return this.lblMessage.Text;
			}
			set
			{
				this.lblMessage.Text = value;
			}
		}

		public int ProgressMinValue
		{
			get
			{
				return this.pBar.Minimum;
			}
			set
			{
				this.pBar.Minimum = value;
			}
		}

		public int ProgressMaxValue
		{
			get
			{
				return this.pBar.Maximum;
			}
			set
			{
				this.pBar.Maximum = value;
			}
		}

		public int ProgressValue
		{
			get
			{
				return this.pBar.Value;
			}
			set
			{
				this.pBar.Value = value;
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
			this.lblTitle = new Label();
			this.lblMessage = new Label();
			this.pBar = new ProgressBar();
			this.btnCancel = new Button();
			base.SuspendLayout();
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblTitle.Location = new Point(12, 13);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new Size(96, 15);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Progress Title";
			this.lblMessage.AutoSize = true;
			this.lblMessage.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblMessage.Location = new Point(12, 41);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new Size(95, 15);
			this.lblMessage.TabIndex = 1;
			this.lblMessage.Text = "Status Message";
			this.pBar.Location = new Point(12, 69);
			this.pBar.MarqueeAnimationSpeed = 30;
			this.pBar.Name = "pBar";
			this.pBar.Size = new Size(396, 23);
			this.pBar.TabIndex = 2;
			this.btnCancel.Location = new Point(333, 103);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(416, 148);
			base.ControlBox = false;
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.pBar);
			base.Controls.Add(this.lblMessage);
			base.Controls.Add(this.lblTitle);
			base.Name = "ProgressWindow";
			base.Opacity = 0.8;
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.TopMost = true;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public ProgressWindow()
		{
			this.UserCancelled = false;
			this.InitializeComponent();
			this.updateUI();
			this.pBar.Style = ProgressBarStyle.Marquee;
			this.pBar.MarqueeAnimationSpeed = 100;
		}

		private void updateUI()
		{
			try
			{
				this.btnCancel.Text = AfaStrings.Cancel;
			}
			catch
			{
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.lblMessage.Text = AfaStrings.Cancelling;
			this.UserCancelled = true;
		}
	}
}
