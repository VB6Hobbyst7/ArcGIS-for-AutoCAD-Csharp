using System;
using System.Windows.Forms;

namespace AFA
{
	public class PaletteToolStrip : ToolStrip
	{
		protected override void OnMouseDown(MouseEventArgs mea)
		{
			if (!base.DesignMode)
			{
				ToolStripItem item = base.GetItemAt(mea.X, mea.Y);
				if (item != null && (item is ToolStripButton || item is ToolStripMenuItem))
				{
					PaletteUtils.OnEditorActivated(item, delegate(object sender, EventArgs e)
					{
						item.PerformClick();
					});
					return;
				}
			}
			base.OnMouseDown(mea);
		}
	}
}
