using Autodesk.AutoCAD.Geometry;
using System;

namespace Autodesk.AutoCAD.DatabaseServices
{
	public static class ViewportExtensionMethods
	{
		public static Matrix3d GetModelToPaperTransform(this Viewport vport)
		{
			if (vport.PerspectiveOn)
			{
				throw new NotSupportedException("Perspective views not supported");
			}
			Point3d point3d = new Point3d(vport.ViewCenter.X, vport.ViewCenter.Y, 0.0);
			return Matrix3d.Displacement(new Vector3d(vport.CenterPoint.X - point3d.X, vport.CenterPoint.Y - point3d.Y, 0.0)) * Matrix3d.Scaling(vport.CustomScale, point3d) * Matrix3d.Rotation(vport.TwistAngle, Vector3d.ZAxis, Point3d.Origin) * Matrix3d.WorldToPlane(new Plane(vport.ViewTarget, vport.ViewDirection));
		}

		public static Matrix3d GetPaperToModelTransform(this Viewport vport)
		{
			return vport.GetModelToPaperTransform().Inverse();
		}

		public static Point3d PaperToModel(this Viewport vport, Point3d point)
		{
			return point.TransformBy(vport.GetModelToPaperTransform().Inverse());
		}

		public static Point3d ModelToPaper(this Viewport vport, Point3d point)
		{
			return point.TransformBy(vport.GetModelToPaperTransform());
		}
	}
}
