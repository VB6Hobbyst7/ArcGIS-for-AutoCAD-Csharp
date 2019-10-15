using ArcGIS10Types;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;

namespace AFA
{
	public static class AGSEllipticalArc
	{
		private static void TransformToEllipseStd(ref PointN inOutPoint, PointN center, double rotation)
		{
			double num = Math.Cos(rotation);
			double num2 = Math.Sin(rotation);
			inOutPoint.X -= center.X;
			inOutPoint.Y -= center.Y;
			double y = inOutPoint.Y;
			inOutPoint.Y *= num;
			inOutPoint.Y -= inOutPoint.X * num2;
			inOutPoint.X *= num;
			inOutPoint.X += y * num2;
		}

		private static void TransformToEllipseStd(ref EllipticArc arc)
		{
			PointN pointN = (PointN)arc.CenterPoint;
			if (double.IsNaN(pointN.Z))
			{
				pointN.Z = 0.0;
			}
			PointN fromPoint = (PointN)arc.FromPoint;
			PointN toPoint = (PointN)arc.ToPoint;
			AGSEllipticalArc.TransformToEllipseStd(ref fromPoint, pointN, arc.Rotation);
			AGSEllipticalArc.TransformToEllipseStd(ref toPoint, pointN, arc.Rotation);
			arc.FromPoint = fromPoint;
			arc.ToPoint = toPoint;
		}

		public static Ellipse ToCadEllipse(EllipticArc arc, double defaultElevation)
		{
			EllipticArc ellipticArc = (EllipticArc)Utility.CloneObject(arc);
			PointN pointN = (PointN)ellipticArc.CenterPoint;
			if (double.IsNaN(pointN.Z))
			{
				pointN.Z = 0.0;
			}
			if (!pointN.ZSpecified)
			{
				pointN.Z = defaultElevation;
				((PointN)ellipticArc.FromPoint).Z = defaultElevation;
				((PointN)ellipticArc.ToPoint).Z = defaultElevation;
			}
			Point3d point3d = GIS2CAD.ToCadPoint3d((PointN)ellipticArc.FromPoint);
			Point3d point3d2 = GIS2CAD.ToCadPoint3d((PointN)ellipticArc.ToPoint);
			if (!ellipticArc.EllipseStd)
			{
				AGSEllipticalArc.TransformToEllipseStd(ref ellipticArc);
			}
			PointN pointN2 = (PointN)ellipticArc.FromPoint;
			PointN pointN3 = (PointN)ellipticArc.ToPoint;
			double arg_B7_0 = pointN2.X;
			double arg_BF_0 = pointN2.Y;
			double num = ellipticArc.MinorMajorRatio * ellipticArc.MinorMajorRatio;
			double x = pointN2.X;
			double y = pointN2.Y;
			double x2 = pointN3.X;
			double y2 = pointN3.Y;
			double num2 = Math.Sqrt(num * x * x + y * y);
			double num3 = Math.Sqrt(num * x2 * x2 + y2 * y2);
			double num4 = 0.5 * (num2 + num3);
			double num5 = num4 / ellipticArc.MinorMajorRatio;
			Vector3d vector3d = new Vector3d(num5, 0.0, 0.0);
			Vector3d vector3d2 = new Vector3d(0.0, num4, 0.0);
			Vector3d vector3d3 = vector3d.RotateBy(ellipticArc.Rotation, Vector3d.ZAxis);
			Vector3d vector3d4 = vector3d2.RotateBy(ellipticArc.Rotation, Vector3d.ZAxis);
			Point3d point3d3 = GIS2CAD.ToCadPoint3d(pointN);
			EllipticalArc3d ellipticalArc3d = new EllipticalArc3d(point3d3, vector3d3, vector3d4, num5, num4);
			double num6 = ellipticalArc3d.GetParameterOf(point3d);
			double num7 = ellipticalArc3d.GetParameterOf(point3d2);
			if (!ellipticArc.IsCounterClockwise)
			{
				double num8 = num6;
				num6 = num7;
				num7 = num8;
			}
			return new Ellipse(point3d3, Vector3d.ZAxis, vector3d3, ellipticArc.MinorMajorRatio, num6, num7);
		}

		private static Point3dCollection SamplePoints(EllipticalArc3d arc, bool isCounterClockwise)
		{
			double num = arc.GetLength(arc.StartAngle, arc.EndAngle, 0.0) / 1000.0;
			PointOnCurve3d[] samplePoints = arc.GetSamplePoints(arc.StartAngle ,arc.EndAngle, num);
			Point3dCollection point3dCollection = new Point3dCollection();
			if (isCounterClockwise)
			{
				PointOnCurve3d[] array = samplePoints;
				for (int i = 0; i < array.Length; i++)
				{
					PointOnCurve3d pointOnCurve3d = array[i];
					point3dCollection.Add(pointOnCurve3d.GetPoint());
				}
			}
			else
			{
				int num2 = samplePoints.Length;
				for (int j = num2 - 1; j >= 0; j--)
				{
					point3dCollection.Add(samplePoints[j].GetPoint());
				}
			}
			return point3dCollection;
		}

		private static Spline BuildSpline(EllipticalArc3d arc, bool isCounterClockwise)
		{
			Point3dCollection point3dCollection = AGSEllipticalArc.SamplePoints(arc, isCounterClockwise);
			return new Spline(point3dCollection, 2, 0.0);
		}

		public static Spline ToCadSpline(EllipticArc inArc, double defaultElevation)
		{
			Ellipse ellipse = AGSEllipticalArc.ToCadEllipse(inArc, defaultElevation);
			Point3d center = ellipse.Center;
			Vector3d majorAxis = ellipse.MajorAxis;
			Vector3d minorAxis = ellipse.MinorAxis;
			double majorRadius = ellipse.MajorRadius;
			double minorRadius = ellipse.MinorRadius;
			double parameterAtAngle = ellipse.GetParameterAtAngle(ellipse.StartAngle);
			double parameterAtAngle2 = ellipse.GetParameterAtAngle(ellipse.EndAngle);
			EllipticalArc3d arc = new EllipticalArc3d(center, majorAxis, minorAxis, majorRadius, minorRadius, parameterAtAngle, parameterAtAngle2);
			return AGSEllipticalArc.BuildSpline(arc, inArc.IsCounterClockwise);
		}

		public static Point3dCollection SamplePoints(EllipticArc inArc, double defaultElevation)
		{
			Ellipse ellipse = AGSEllipticalArc.ToCadEllipse(inArc, defaultElevation);
			Point3d center = ellipse.Center;
			Vector3d majorAxis = ellipse.MajorAxis;
			Vector3d minorAxis = ellipse.MinorAxis;
			double majorRadius = ellipse.MajorRadius;
			double minorRadius = ellipse.MinorRadius;
			double parameterAtAngle = ellipse.GetParameterAtAngle(ellipse.StartAngle);
			double parameterAtAngle2 = ellipse.GetParameterAtAngle(ellipse.EndAngle);
			EllipticalArc3d arc = new EllipticalArc3d(center, majorAxis, minorAxis, majorRadius, minorRadius, parameterAtAngle, parameterAtAngle2);
			return AGSEllipticalArc.SamplePoints(arc, inArc.IsCounterClockwise);
		}
	}
}
