using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AFA
{
    public class HiddenUpdateForm : Form
	{
		private Document _doc;

		public ObjectId _rasterID;

		private IContainer components;

		private Label lblReadyForUpdate;

		public Point3d BasePoint
		{
			get;
			set;
		}

		public Vector3d V1
		{
			get;
			set;
		}

		public Vector3d V2
		{
			get;
			set;
		}

		public string ImageURL
		{
			get;
			set;
		}

		public HiddenUpdateForm(Document doc)
		{
			this.InitializeComponent();
			this._doc = doc;
		}

		public bool IsReady()
		{
			return this.lblReadyForUpdate.Text == "ready";
		}

		public void SetReady(bool newValue)
		{
			if (newValue)
			{
				this.lblReadyForUpdate.Text = "ready";
				return;
			}
			this.lblReadyForUpdate.Text = "not ready";
		}

		public bool UpdateRasterNow()
		{
			bool result;
			try
			{
				bool flag = DocUtil.UpdateRasterImage(this._doc, this._rasterID, this.ImageURL, this.BasePoint, this.V1, this.V2);
				if (flag)
				{
					this.lblReadyForUpdate.Text = "not ready";
				}
				result = flag;
			}
			catch (SystemException ex)
			{
				ErrorReport.ShowErrorMessage("Catch in UpdateRasterNow: " + ex.Message);
				result = false;
			}
			return result;
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
            this.lblReadyForUpdate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblReadyForUpdate
            // 
            this.lblReadyForUpdate.AutoSize = true;
            this.lblReadyForUpdate.Location = new System.Drawing.Point(27, 161);
            this.lblReadyForUpdate.Name = "lblReadyForUpdate";
            this.lblReadyForUpdate.Size = new System.Drawing.Size(59, 12);
            this.lblReadyForUpdate.TabIndex = 4;
            this.lblReadyForUpdate.Text = "not ready";
            // 
            // HiddenUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 204);
            this.Controls.Add(this.lblReadyForUpdate);
            this.Name = "HiddenUpdateForm";
            this.Text = "HiddenUpdateForm";
            this.Load += new System.EventHandler(this.HiddenUpdateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void HiddenUpdateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
