using AFA.Resources;
using ArcGIS10Types;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AFA
{
    public class GIS2CAD
	{
		public static List<Entity> ToEntity(Geometry obj, Geometry denseObj, double defaultElevation, ObjectId blockDefId)
		{
			List<Entity> list = null;
			List<Entity> result;
			try
			{
				if (obj == null)
				{
					result = null;
				}
				else
				{
					PolygonN polygonN = obj as PolygonN;
					PolylineN polylineN = obj as PolylineN;
					PointN pointN = obj as PointN;
					MultipointN multipointN = obj as MultipointN;
					if (polygonN != null)
					{
						list = GIS2CAD.ToEntity(polygonN, denseObj as PolygonN, defaultElevation);
					}
					else if (polylineN != null)
					{
						list = GIS2CAD.ToEntity(polylineN, denseObj as PolylineN, defaultElevation);
					}
					else if (pointN != null)
					{
						list = GIS2CAD.ToEntity(pointN, defaultElevation, blockDefId);
					}
					else if (multipointN != null)
					{
						list = GIS2CAD.ToEntity(multipointN, defaultElevation);
					}
					else
					{
						DocUtil.ShowDebugMessage(AfaStrings.EncounteredUnknownObjectType);
					}
					if (list == null)
					{
						result = list;
					}
					else
					{
						result = list;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static List<Entity> ToEntity(PolygonN gisGeom, PolygonN densifiedGeometry, double defaultElevation)
		{
			List<Entity> list = new List<Entity>();
			int num = 0;
			Ring[] ringArray = gisGeom.RingArray;
			for (int i = 0; i < ringArray.Length; i++)
			{
				Ring ring = ringArray[i];
				Point[] pointArray = densifiedGeometry.RingArray[num].PointArray;
				num++;
				Entity entity = GIS2CAD.DrawPart(ring.SegmentArray, ring.PointArray, pointArray, true, gisGeom.HasZ, defaultElevation);
				if (entity != null)
				{
					list.Add(entity);
				}
			}
			if (list.Count > 0)
			{
				return list;
			}
			return null;
		}

		private static List<Entity> ToEntity(PolylineN gisGeom, PolylineN densifiedGeometry, double defaultElevation)
		{
			List<Entity> list = new List<Entity>();
			int num = 0;
			Path[] pathArray = gisGeom.PathArray;
			for (int i = 0; i < pathArray.Length; i++)
			{
				Path path = pathArray[i];
				Point[] pointArray = densifiedGeometry.PathArray[num].PointArray;
				num++;
				Entity entity = GIS2CAD.DrawPart(path.SegmentArray, path.PointArray, pointArray, false, gisGeom.HasZ, defaultElevation);
				if (entity != null)
				{
					list.Add(entity);
				}
			}
			if (list.Count > 0)
			{
				return list;
			}
			return null;
		}

		private static List<Entity> ToEntity(PointN gisGeom, double defaultElevation, ObjectId blockDefID)
		{
			List<Entity> list = new List<Entity>();
			double num = gisGeom.Z;
			if (!gisGeom.ZSpecified)
			{
				num = defaultElevation;
			}
			Point3d point3d = new Point3d(gisGeom.X, gisGeom.Y, num);
			if (blockDefID == ObjectId.Null)
			{
				DBPoint item = new DBPoint(point3d);
				list.Add(item);
			}
			else
			{
				BlockReference item2 = new BlockReference(point3d, blockDefID);
				list.Add(item2);
			}
			return list;
		}

		private static List<Entity> ToEntity(MultipointN gisGeom, double defaultElevation)
		{
			List<Entity> list = new List<Entity>();
			Point[] pointArray = gisGeom.PointArray;
			for (int i = 0; i < pointArray.Length; i++)
			{
				Point point = pointArray[i];
				PointN pointN = point as PointN;
				DBPoint item = new DBPoint(new Point3d(pointN.X, pointN.Y, pointN.Z));
				list.Add(item);
			}
			return list;
		}

		private static double CalculateMidPointDistance(Point2d fromPoint, Point2d endPoint, Point2d centerPoint, double theta, bool IsCounterClockwise)
		{
			double num = Math.Tan(theta / 4.0);
			if (!IsCounterClockwise)
			{
				num *= -1.0;
			}
			CircularArc2d circularArc2d = new CircularArc2d(fromPoint, endPoint, num, false);
			return circularArc2d.Radius;
		}

		private static double CalcThetaFromVectors(Point2d fromPoint, Point2d endPoint, Point2d centerPoint, bool isCCW)
		{
			Vector2d vectorTo = centerPoint.GetVectorTo(fromPoint);
			Vector2d vectorTo2 = centerPoint.GetVectorTo(endPoint);
			double angleTo;
			if (isCCW)
			{
				angleTo = vectorTo2.GetAngleTo(vectorTo);
			}
			else
			{
				angleTo = vectorTo.GetAngleTo(vectorTo2);
			}
			Vector2d vectorTo3 = centerPoint.GetVectorTo(fromPoint);
			Vector2d vectorTo4 = centerPoint.GetVectorTo(endPoint);
			Vector3d vector3d = new Vector3d(vectorTo3.X, vectorTo3.Y, 0.0);
			Vector3d vector3d2 = new Vector3d(vectorTo4.X, vectorTo4.Y, 0.0);
			Vector3d vector3d3 = new Vector3d(0.0, 0.0, 1.0);
			if (isCCW)
			{
				angleTo = vector3d.GetAngleTo(vector3d2, vector3d3);
			}
			else
			{
				angleTo = vector3d2.GetAngleTo(vector3d, vector3d3);
			}
			return angleTo;
		}

		private static Entity DrawPart(Segment[] segs, Point[] points, Point[] densifiedPoints, bool closePart, bool hasZ, double defaultElevation)
		{
			double num = 0.0;
			if (segs != null)
			{
				if (segs.Length == 1)
				{
					CircularArc circularArc = segs[0] as CircularArc;
					EllipticArc ellipticArc = segs[0] as EllipticArc;
					BezierCurve bezierCurve = segs[0] as BezierCurve;
                    ArcGIS10Types.Line line = segs[0] as ArcGIS10Types.Line;
					if (circularArc != null)
					{
						if (((PointN)circularArc.FromPoint).X == ((PointN)circularArc.ToPoint).X && ((PointN)circularArc.FromPoint).Y == ((PointN)circularArc.ToPoint).Y)
						{
							return GIS2CAD.DrawCircle(circularArc, defaultElevation);
						}
						return GIS2CAD.DrawCircularArc(circularArc, defaultElevation, densifiedPoints);
					}
					else if (ellipticArc != null)
					{
						if (!ellipticArc.IsCounterClockwise)
						{
							return AGSEllipticalArc.ToCadSpline(ellipticArc, defaultElevation);
						}
						return AGSEllipticalArc.ToCadEllipse(ellipticArc, defaultElevation);
					}
					else
					{
						if (line != null)
						{
							return GIS2CAD.DrawPolyline(densifiedPoints, false);
						}
						if (bezierCurve != null)
						{
							return GIS2CAD.DrawPolyline(densifiedPoints, closePart);
						}
					}
				}
				else if (segs.Length > 1)
				{
					PointN pointN = segs[0].FromPoint as PointN;
					num = pointN.Z;
					if (num == 0.0)
					{
						num = defaultElevation;
					}
					if (GIS2CAD.CanBeDrawnAsPolyline(segs))
					{
                       var  polyline = new Autodesk.AutoCAD.DatabaseServices.Polyline();
						polyline.ColorIndex=(256);
						int num2 = 0;
						PointN pointN2 = (PointN)segs[0].ToPoint;
						for (int i = 0; i < segs.Length; i++)
						{
							Segment segment = segs[i];
							CircularArc circularArc2 = segment as CircularArc;
							var  line2 = segment as ArcGIS10Types.Line;
							if (line2 != null)
							{
								PointN pointN3 = (PointN)line2.FromPoint;
								polyline.AddVertexAt(num2++, new Point2d(pointN3.X, pointN3.Y), 0.0, -1.0, -1.0);
								pointN2 = (PointN)line2.ToPoint;
							}
							else if (circularArc2 != null)
							{
								PointN pointN4 = (PointN)circularArc2.CenterPoint;
								PointN pointN5 = (PointN)circularArc2.FromPoint;
								PointN pointN6 = (PointN)circularArc2.ToPoint;
								new Point2d(pointN5.X - pointN4.X, pointN5.Y - pointN4.Y);
								new Point2d(pointN6.X - pointN4.X, pointN6.Y - pointN4.Y);
								Point2d point2d = new Point2d(pointN5.X, pointN5.Y);
								Point2d centerPoint = new Point2d(pointN4.X, pointN4.Y);
								Point2d point2d2 = new Point2d(pointN6.X, pointN6.Y);
								double num3 = Math.Abs(centerPoint.GetDistanceTo(point2d));
								double num4 = Math.Abs(point2d.GetDistanceTo(point2d2));
								double num5 = num3;
								double num6 = num3;
								double d = (num5 * num5 + num6 * num6 - num4 * num4) / (2.0 * num5 * num6);
								double num7 = Math.Acos(d);
								num7 = GIS2CAD.CalcThetaFromVectors(point2d, point2d2, centerPoint, circularArc2.IsCounterClockwise);
								double num8 = Math.Tan(num7 / 4.0);
								if (!circularArc2.IsCounterClockwise)
								{
									num8 *= -1.0;
								}
								polyline.AddVertexAt(num2++, point2d, num8, -1.0, -1.0);
								pointN2 = pointN6;
							}
						}
						polyline.AddVertexAt(num2, new Point2d(pointN2.X, pointN2.Y), 0.0, -1.0, -1.0);
						if (closePart)
						{
							polyline.Closed=(true);
						}
						return polyline;
					}
					return GIS2CAD.Draw3dPline(densifiedPoints, closePart);
				}
			}
			else if (points != null)
			{
				if (points.Length == 2)
				{
					var  line3 = new Autodesk.AutoCAD.DatabaseServices.Line();
					line3.ColorIndex=(256);
					GIS2CAD.AdjustZ(ref points, defaultElevation);
					Point3d startPoint = GIS2CAD.ToCadPoint3d((PointN)points[0]);
					Point3d endPoint = GIS2CAD.ToCadPoint3d((PointN)points[1]);
					line3.StartPoint=(startPoint);
					line3.EndPoint=(endPoint);
					return line3;
				}
				if (points.Length > 0)
				{
					if (!GIS2CAD.IsPlanar(points))
					{
						try
						{
							Document document = AfaDocData.ActiveDocData.Document;
							var  database = document.Database;
							var  transactionManager = document.TransactionManager;
							using (document.LockDocument())
							{
								using (Transaction transaction = transactionManager.StartTransaction())
								{
									BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, 0);
									BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(blockTable[(BlockTableRecord.ModelSpace)], (OpenMode)1);
									Polyline3d polyline3d = new Polyline3d();
									polyline3d.ColorIndex=(256);
									blockTableRecord.AppendEntity(polyline3d);
									transaction.AddNewlyCreatedDBObject(polyline3d, true);
									Point[] array = points;
									for (int j = 0; j < array.Length; j++)
									{
										PointN srcPt = (PointN)array[j];
										PolylineVertex3d polylineVertex3d = new PolylineVertex3d(GIS2CAD.ToCadPoint3d(srcPt));
										polyline3d.AppendVertex(polylineVertex3d);
										transaction.AddNewlyCreatedDBObject(polylineVertex3d, true);
									}
									if (closePart)
									{
										polyline3d.Closed=(true);
									}
									document.TransactionManager.QueueForGraphicsFlush();
									document.TransactionManager.FlushGraphics();
									document.Editor.UpdateScreen();
									transaction.Commit();
									return polyline3d;
								}
							}
						}
						catch (System.Exception ex)
						{
							string arg_526_0 = ex.Message;
						}
					}
                    var  polyline2 = new Autodesk.AutoCAD.DatabaseServices.Polyline();
					polyline2.ColorIndex=(256);
					polyline2.Elevation=(num);
					num = ((PointN)points[0]).Z;
					if (num == 0.0)
					{
						num = defaultElevation;
					}
					int num9 = 0;
					Point[] array2 = points;
					for (int k = 0; k < array2.Length; k++)
					{
						PointN pointN7 = (PointN)array2[k];
						polyline2.AddVertexAt(num9++, new Point2d(pointN7.X, pointN7.Y), 0.0, -1.0, -1.0);
					}
					if (closePart)
					{
						polyline2.Closed=(true);
					}
					return polyline2;
				}
			}
			return null;
		}

		private static bool CanBeDrawnAsPolyline(Segment[] segs)
		{
			Segment segment = segs[0];
			PointN pointN = (PointN)segment.FromPoint;
			PointN pointN2 = (PointN)segment.ToPoint;
			if (pointN.Z != pointN2.Z)
			{
				return false;
			}
			double z = pointN2.Z;
			int i = 0;
			while (i < segs.Length)
			{
				Segment segment2 = segs[i];
				EllipticArc ellipticArc = segment2 as EllipticArc;
				BezierCurve bezierCurve = segment2 as BezierCurve;
				bool result;
				if (bezierCurve != null)
				{
					result = false;
				}
				else if (ellipticArc != null)
				{
					result = false;
				}
				else
				{
					pointN = (PointN)segment2.FromPoint;
					pointN2 = (PointN)segment2.ToPoint;
					if (pointN.Z != z)
					{
						result = false;
					}
					else
					{
						if (pointN2.Z == z)
						{
							i++;
							continue;
						}
						result = false;
					}
				}
				return result;
			}
			return true;
		}

		private static Entity DrawSpline(BezierCurve curve, double defaultElevation)
		{
			Entity result;
			try
			{
				List<Point3d> list = new List<Point3d>();
				Point[] controlPointArray = curve.ControlPointArray;
				for (int i = 0; i < controlPointArray.Length; i++)
				{
					Point point = controlPointArray[i];
					list.Add(GIS2CAD.ToCadPoint3d((PointN)point));
				}
				int count = list.Count;
				double num = 1.0 / (double)count;
				List<double> list2 = new List<double>();
				for (int j = 0; j < count; j++)
				{
					list2.Add((double)j * num);
				}
				Spline spline = new Spline(curve.Degree, false, false, false, new Point3dCollection(list.ToArray()), new DoubleCollection(list2.ToArray()), null, 0.0, 0.0);
				result = spline;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Entity DrawCircle(CircularArc arc, double defaultElevation)
		{
			PointN pointN = arc.FromPoint as PointN;
			PointN pointN2 = arc.CenterPoint as PointN;
			double num = Math.Sqrt((pointN.X - pointN2.X) * (pointN.X - pointN2.X) + (pointN.Y - pointN2.Y) * (pointN.Y - pointN2.Y));
			Point3d point3d = new Point3d(pointN2.X, pointN2.Y, pointN2.Z);
			Entity entity = new Circle(point3d, Vector3d.ZAxis, num);
			entity.ColorIndex=(256);
			return entity;
		}

		private static Arc CreateFromCircularArc(CircularArc3d circArc)
		{
			Point3d center = circArc.Center;
			Vector3d normal = circArc.Normal;
			Vector3d referenceVector = circArc.ReferenceVector;
			Plane plane = new Plane(center, normal);
			double num = referenceVector.AngleOnPlane(plane);
			return new Arc(center, normal, circArc.Radius, circArc.StartAngle+ num, circArc.EndAngle + num);
		}

		private static Entity DrawCircularArc(CircularArc arc, double defaultElevation, Point[] densifiedPoints)
		{
			PointN pointN = arc.FromPoint as PointN;
			PointN pointN2 = arc.ToPoint as PointN;
			PointN pointN3 = arc.CenterPoint as PointN;
			Point3d point3d = new Point3d(pointN.X, pointN.Y, pointN.Z);
			Point3d point3d2 = new Point3d(pointN3.X, pointN3.Y, pointN3.Z);
			Point3d point3d3 = new Point3d(pointN2.X, pointN2.Y, pointN2.Z);
			Point2d centerPoint = new Point2d(pointN3.X, pointN3.Y);
			Point2d point2d = new Point2d(pointN.X, pointN.Y);
			Math.Abs(centerPoint.GetDistanceTo(point2d));
			CircularArc3d circArc;
			if (densifiedPoints != null)
			{
				int num = densifiedPoints.Length / 2;
				PointN pointN4 = (PointN)densifiedPoints[num];
				if (arc.IsCounterClockwise)
				{
					PointN arg_CC_0 = (PointN)densifiedPoints[0];
					PointN arg_D9_0 = (PointN)densifiedPoints[densifiedPoints.Length - 1];
				}
				else
				{
					PointN arg_E4_0 = (PointN)densifiedPoints[0];
					PointN arg_F1_0 = (PointN)densifiedPoints[densifiedPoints.Length - 1];
				}
				Point3d point3d4 = new Point3d(pointN4.X, pointN4.Y, pointN4.Z);
				circArc = new CircularArc3d(point3d, point3d4, point3d3);
			}
			else
			{
				Point2d point2d2 = new Point2d(point3d.X, point3d.Y);
				Point2d point2d3 = new Point2d(point3d3.X, point3d3.Y);
				double num2 = GIS2CAD.CalcThetaFromVectors(point2d2, point2d3, centerPoint, arc.IsCounterClockwise);
				double num3 = Math.Tan(num2 / 4.0);
				if (!arc.IsCounterClockwise)
				{
					num3 *= -1.0;
				}
				CircularArc2d circularArc2d = new CircularArc2d(point2d2, point2d3, num3, false);
				Point2d[] samplePoints = circularArc2d.GetSamplePoints(3);
				Point3d point3d5 = new Point3d(samplePoints[1].X, samplePoints[1].Y, point3d2.Z);
				circArc = new CircularArc3d(point3d, point3d5, point3d3);
			}
			new Point3d(pointN3.X, pointN3.Y, pointN3.Z);
			Arc arc2 = GIS2CAD.CreateFromCircularArc(circArc);
			arc2.ColorIndex=(256);
			return arc2;
		}

		private static Entity DrawPolyline(Point[] points, bool closePart)
		{
			var  polyline = new Autodesk.AutoCAD.DatabaseServices.Polyline();
			int num = 0;
			for (int i = 0; i < points.Length; i++)
			{
				Point point = points[i];
				PointN srcPt = (PointN)point;
				polyline.AddVertexAt(num++, GIS2CAD.ToCadPoint2d(srcPt), 0.0, 0.0, 0.0);
			}
			return polyline;
		}

		private static Entity Draw3dPline(Point[] points, bool closePart)
		{
			Document document = AfaDocData.ActiveDocData.Document;
			Database database = document.Database;
			var  transactionManager = document.TransactionManager;
			Polyline3d polyline3d = new Polyline3d();
			using (document.LockDocument())
			{
				using (Transaction transaction = transactionManager.StartTransaction())
				{
					BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, 0);
					BlockTableRecord blockTableRecord = (BlockTableRecord)transaction.GetObject(blockTable[(BlockTableRecord.ModelSpace)], (OpenMode)1);
					polyline3d.ColorIndex=(256);
					blockTableRecord.AppendEntity(polyline3d);
					transaction.AddNewlyCreatedDBObject(polyline3d, true);
					int num = 0;
					if (points != null && points.Length > 0)
					{
						for (int i = 0; i < points.Length; i++)
						{
							Point point = points[i];
							PointN pointN = (PointN)point;
							PolylineVertex3d polylineVertex3d = new PolylineVertex3d(new Point3d(pointN.X, pointN.Y, pointN.Z));
							polyline3d.AppendVertex(polylineVertex3d);
							num++;
						}
					}
					if (num == 0)
					{
						return null;
					}
					if (closePart)
					{
						polyline3d.Closed=(true);
					}
					polyline3d.ColorIndex=(256);
					document.TransactionManager.QueueForGraphicsFlush();
					document.TransactionManager.FlushGraphics();
					document.Editor.UpdateScreen();
					transaction.Commit();
				}
			}
			return polyline3d;
		}

		public static GraphicFeature ToGraphicFeature(Group grp, List<CadField> fields, SpatialReference spRef)
		{
			return null;
		}

		public static GraphicFeature ToGraphicFeature(Entity ent, List<CadField> fields)
		{
			if (ent is Autodesk.AutoCAD.DatabaseServices.Polyline)
			{
				return GIS2CAD.ToGraphicFeature((Autodesk.AutoCAD.DatabaseServices.Polyline)ent, fields);
			}
			return null;
		}

		private static PointN ToPointN(Point3d cadPt, SpatialReference spRef)
		{
			PointN result;
			try
			{
				result = new PointN
				{
					SpatialReference = spRef,
					MSpecified = false,
					IDSpecified = false,
					X = cadPt.X,
					Y = cadPt.Y,
					Z = cadPt.Z,
					ZSpecified = true
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Path ToPath(Autodesk.AutoCAD.DatabaseServices.Polyline pline, SpatialReference spRef)
		{
			Path path = new Path();
			if (pline.IsOnlyLines)
			{
				List<Point> list = new List<Point>();
				for (int i = 0; i < pline.NumberOfVertices; i++)
				{
					Point3d point3dAt = pline.GetPoint3dAt(i);
					PointN pointN = GIS2CAD.ToPointN(point3dAt, spRef);
					if (pointN != null)
					{
						list.Add(pointN);
					}
				}
				path.PointArray = list.ToArray();
				return path;
			}
			return null;
		}

		public static Point2d ToCadPoint2d(PointN srcPt)
		{
			return new Point2d(srcPt.X, srcPt.Y);
		}

		public static Point3d ToCadPoint3d(PointN srcPt)
		{
			return new Point3d(srcPt.X, srcPt.Y, srcPt.Z);
		}

		private static Vector2d ToCadVector2d(PointN srcPt)
		{
			return new Vector2d(srcPt.X, srcPt.Y);
		}

		public static Vector3d ToCadVector3d(PointN srcPt)
		{
			return new Vector3d(srcPt.X, srcPt.Y, srcPt.Z);
		}

		public static Vector2d ToCadVector2d(PointN basePt, PointN destPt)
		{
			Point2d point2d = GIS2CAD.ToCadPoint2d(basePt);
			Point2d point2d2 = GIS2CAD.ToCadPoint2d(destPt);
			return point2d.GetVectorTo(point2d2);
		}

		public static Vector3d ToCadVector3d(PointN basePt, PointN destPt)
		{
			Point3d point3d = GIS2CAD.ToCadPoint3d(basePt);
			Point3d point3d2 = GIS2CAD.ToCadPoint3d(destPt);
			return point3d.GetVectorTo(point3d2);
		}

		public static bool IsPlanar(Point[] plainPoints)
		{
			double z = ((PointN)plainPoints[0]).Z;
			for (int i = 0; i < plainPoints.Length; i++)
			{
				Point point = plainPoints[i];
				PointN pointN = (PointN)point;
				if (pointN.Z != z)
				{
					return false;
				}
			}
			return true;
		}

		public static void AdjustZ(ref Point[] plainPoints, double defaultElevation)
		{
			if (defaultElevation == 0.0)
			{
				return;
			}
			PointN[] array = (PointN[])plainPoints;
			double z = array[0].Z;
			PointN[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				PointN pointN = array2[i];
				if (pointN.Z == z)
				{
					if (pointN.Z == 0.0)
					{
						i++;
						continue;
					}
				}
				return;
			}
			PointN[] array3 = array;
			for (int j = 0; j < array3.Length; j++)
			{
				PointN pointN2 = array3[j];
				pointN2.Z = defaultElevation;
			}
		}
	}
}
