using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace AFA
{
    public class ViewInfo
	{
		public Point2d CenterPoint
		{
			get;
			set;
		}

		public double Height
		{
			get;
			set;
		}

		public double Width
		{
			get;
			set;
		}

		public ViewInfo(ViewTableRecord vtr)
		{
			this.CenterPoint = vtr.CenterPoint;
			this.Height = vtr.Height;
			this.Width = vtr.Width;
		}

		public ViewInfo(Document doc, ObjectId rasterObjectId)
		{
			Extent extentFromObject = DocUtil.GetExtentFromObject(doc, rasterObjectId);
			this.Width = extentFromObject.XMax.Value - extentFromObject.XMin.Value;
			this.Height = extentFromObject.YMax.Value - extentFromObject.YMin.Value;
			this.CenterPoint = new Point2d(extentFromObject.XMin.Value + this.Width / 2.0, extentFromObject.YMin.Value + this.Height / 2.0);
		}

		public static bool Equivalent(ViewInfo a, ViewInfo b)
		{
			return a != null && b != null && (a.Height == b.Height && a.Width == b.Width && a.CenterPoint == b.CenterPoint);
		}
	}
}
