using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AFA
{
    public class SplashScreen : Form
	{
		private IContainer components;

		public Label errMsg;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SplashScreen));
			this.errMsg = new Label();
			base.SuspendLayout();
			this.errMsg.AutoSize = true;
			this.errMsg.Location = new Point(28, 120);
			this.errMsg.Name = "errMsg";
			this.errMsg.Size = new Size(168, 13);
			this.errMsg.TabIndex = 2;
			this.errMsg.Text = "Error loading ArcGIS for AutoCAD.";
			this.errMsg.UseWaitCursor = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = AFA.Properties.Resources.AutoCAD300_Splash_456x270;
			this.BackgroundImageLayout = ImageLayout.Stretch;
			base.ClientSize = new Size(458, 270);
			base.Controls.Add(this.errMsg);
			this.Cursor = Cursors.WaitCursor;
			base.FormBorderStyle = FormBorderStyle.None;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SplashScreen";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "About";
			base.UseWaitCursor = true;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public SplashScreen()
		{
			this.InitializeComponent();
		}
	}
}
