using ArcGIS10Types;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AFA
{
	public class CAD2GIS
	{
		public static Geometry ToServerPline(Entity ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			if (ent is Autodesk.AutoCAD.DatabaseServices.Polyline)
			{
				return CAD2GIS.ToServerPline((Autodesk.AutoCAD.DatabaseServices.Polyline)ent, spRef, HasZ, HasM);
			}
			if (ent is Autodesk.AutoCAD.DatabaseServices.Line)
			{
				return CAD2GIS.ToServerPline((Autodesk.AutoCAD.DatabaseServices.Line)ent, spRef, HasZ, HasM);
			}
			if (ent is Circle)
			{
				return CAD2GIS.ToServerPline((Circle)ent, spRef, HasZ, HasM);
			}
			if (ent is Arc)
			{
				return CAD2GIS.ToServerPline((Arc)ent, spRef, HasZ, HasM);
			}
			if (ent is Ellipse)
			{
				return CAD2GIS.ToServerPline((Ellipse)ent, spRef, HasZ, HasM);
			}
			if (ent is Mline)
			{
				return CAD2GIS.ToServerPline((Mline)ent, spRef, HasZ, HasM);
			}
			if (ent is Polyline3d)
			{
				return CAD2GIS.ToServerPline((Polyline3d)ent, spRef, HasZ, HasM);
			}
			if (ent is Face)
			{
				return CAD2GIS.ToServerPline((Face)ent, spRef, HasZ, HasM);
			}
			if (ent is Autodesk.AutoCAD.DatabaseServices.Curve)
			{
				return CAD2GIS.ToServerPline((Autodesk.AutoCAD.DatabaseServices.Curve)ent, spRef, HasZ, HasM);
			}
			return null;
		}

		public static Geometry ToServerPolygon(Entity ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			if (ent is Autodesk.AutoCAD.DatabaseServices.Polyline)
			{
				return CAD2GIS.ToServerPolygon((Autodesk.AutoCAD.DatabaseServices.Polyline)ent, spRef, HasZ, HasM);
			}
			if (ent is Circle)
			{
				return CAD2GIS.ToServerPolygon((Circle)ent, spRef, HasZ, HasM);
			}
			if (ent is Ellipse)
			{
				return CAD2GIS.ToServerPolygon((Ellipse)ent, spRef, HasZ, HasM);
			}
			if (ent is Mline)
			{
				return CAD2GIS.ToServerPolygon((Mline)ent, spRef, HasZ, HasM);
			}
			if (ent is Polyline3d)
			{
				return CAD2GIS.ToServerPolygon((Polyline3d)ent, spRef, HasZ, HasM);
			}
			if (ent is Face)
			{
				return CAD2GIS.ToServerPolygon((Face)ent, spRef, HasZ, HasM);
			}
			if (ent is Autodesk.AutoCAD.DatabaseServices.Curve)
			{
				return CAD2GIS.ToServerPolygon((Autodesk.AutoCAD.DatabaseServices.Curve)ent, spRef, HasZ, HasM);
			}
			return null;
		}

		public static Geometry ToServerPoint(Entity ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			if (ent is DBPoint)
			{
				DBPoint pt = ent as DBPoint;
				return CAD2GIS.ToServerPoint(pt, spRef, HasZ, HasM);
			}
			if (ent is BlockReference)
			{
				return CAD2GIS.ToServerPoint((BlockReference)ent, spRef, HasZ, HasM);
			}
			return null;
		}

		public static Geometry ToServerMultiPoint(Entity ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			if (ent is DBPoint)
			{
				return CAD2GIS.ToServerMultiPoint((DBPoint)ent, spRef, HasZ, HasM);
			}
			if (ent is BlockReference)
			{
				return CAD2GIS.ToServerMultiPoint((BlockReference)ent, spRef, HasZ, HasM);
			}
			return null;
		}

		private static object EncodeString(object inputValue)
		{
			if (inputValue == null)
			{
				return inputValue;
			}
			if (!(inputValue is string))
			{
				return inputValue;
			}
			string text = inputValue as string;
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			string text2 = text;
			if (text2.Contains("<") && text2.Contains(">"))
			{
				text2 = text2.Replace('<', '[');
				text2 = text2.Replace('>', ']');
			}
			return text2;
		}

		private static DataObject UpdateProperties(Transaction t, DataObject d, MSCFeatureClass fc, DBObject srcObject)
		{
			List<CadField> entityFields = fc.GetEntityFields(srcObject.Id, t);
			PropertySetProperty[] propertyArray = d.Properties.PropertyArray;
			for (int i = 0; i < propertyArray.Length; i++)
			{
				PropertySetProperty propertySetProperty = propertyArray[i];
				CadField cadField = null;
				foreach (CadField current in entityFields)
				{
					if (current.Name == propertySetProperty.Key)
					{
						cadField = current;
						break;
					}
				}
				if (cadField != null)
				{
					if (propertySetProperty.Value == null)
					{
						propertySetProperty.Value = cadField.GetExtendedValue();
					}
					else if (propertySetProperty.Value.ToString() != cadField.Value.Value.ToString())
					{
						propertySetProperty.Value = cadField.GetExtendedValue();
					}
					if (cadField.FieldType == CadField.CadFieldType.String)
					{
						propertySetProperty.Value = CAD2GIS.EncodeString(propertySetProperty.Value);
					}
					if (cadField.Domain != null && cadField.Domain.CodedValues != null && !cadField.Domain.IsValidCodedValue(propertySetProperty.Value))
					{
						propertySetProperty.Value = null;
					}
				}
			}
			return d;
		}

		public static DataObject UpdateGraphicFeature(Transaction t, DataObject prototype, DBObject srcObject, SpatialReference spRef, MSCFeatureClass fc, bool hasZ, bool hasM)
		{
			if (!(prototype is GraphicFeature))
			{
				return prototype;
			}
			DataObject dataObject = Utility.DeepClone<DataObject>(prototype);
			if (dataObject == null)
			{
				return prototype;
			}
			GraphicFeature graphicFeature = (GraphicFeature)dataObject;
			MSCFeatureClass.fcTypeCode geometryType = fc.GeometryType;
			if (srcObject is Entity)
			{
				Entity ent = (Entity)srcObject;
				Geometry geometry;
				if (geometryType == MSCFeatureClass.fcTypeCode.fcTypePolyline)
				{
					geometry = CAD2GIS.ToServerPline(ent, spRef, hasZ, hasM);
				}
				else if (geometryType == MSCFeatureClass.fcTypeCode.fcTypePolygon)
				{
					geometry = CAD2GIS.ToServerPolygon(ent, spRef, hasZ, hasM);
				}
				else
				{
					if (geometryType != MSCFeatureClass.fcTypeCode.fcTypePoint)
					{
						return dataObject;
					}
					geometry = CAD2GIS.ToServerPoint(ent, spRef, hasZ, hasM);
				}
				if (geometry == null)
				{
					return null;
				}
				graphicFeature.Geometry = geometry;
			}
			else if (srcObject is Group)
			{
				Group group = (Group)srcObject;
				Geometry geometry2;
				if (geometryType == MSCFeatureClass.fcTypeCode.fcTypePolyline)
				{
					geometry2 = CAD2GIS.ToServerPline(t, group, spRef, hasZ, hasM);
				}
				else
				{
					if (geometryType != MSCFeatureClass.fcTypeCode.fcTypePolygon)
					{
						return dataObject;
					}
					geometry2 = CAD2GIS.ToServerPolygon(t, group, spRef, hasZ, hasM);
				}
				if (geometry2 == null)
				{
					return null;
				}
				graphicFeature.Geometry = geometry2;
			}
			return CAD2GIS.UpdateProperties(t, dataObject, fc, srcObject);
		}

		private static Geometry ToServerPolygon(Transaction t, Group group, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				PolygonN polygonN = new PolygonN();
				polygonN.SpatialReference = spRef;
				polygonN.HasM = HasM;
				polygonN.HasZ = HasZ;
				List<Ring> list = new List<Ring>();
				ObjectId[] allEntityIds = group.GetAllEntityIds();
				ObjectId[] array = allEntityIds;
				for (int i = 0; i < array.Length; i++)
				{
					ObjectId objectId = array[i];
					Ring ring = null;
					DBObject @object = t.GetObject(objectId, 0);
					if (@object is Autodesk.AutoCAD.DatabaseServices.Polyline)
					{
						ring = CAD2GIS.ToRing((Autodesk.AutoCAD.DatabaseServices.Polyline)@object, spRef, HasZ, HasM);
					}
					else if (@object is Autodesk.AutoCAD.DatabaseServices.Curve)
					{
						ring = CAD2GIS.ToRing((Autodesk.AutoCAD.DatabaseServices.Curve)@object, spRef, HasZ, HasM);
					}
					if (ring != null)
					{
						list.Add(ring);
					}
				}
				if (list.Count == 0)
				{
					result = null;
				}
				else
				{
					polygonN.RingArray = list.ToArray();
					result = polygonN;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPline(Transaction t, Group group, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				PolylineN polylineN = new PolylineN();
				polylineN.SpatialReference = spRef;
				polylineN.HasM = HasM;
				polylineN.HasZ = HasZ;
				List<Path> list = new List<Path>();
				ObjectId[] allEntityIds = group.GetAllEntityIds();
				ObjectId[] array = allEntityIds;
				for (int i = 0; i < array.Length; i++)
				{
					ObjectId objectId = array[i];
					Path path = null;
					DBObject @object = t.GetObject(objectId, 0);
					if (@object is Autodesk.AutoCAD.DatabaseServices.Polyline)
					{
						path = CAD2GIS.ToPath((Autodesk.AutoCAD.DatabaseServices.Polyline)@object, spRef, HasZ, HasM);
					}
					else if (@object is Autodesk.AutoCAD.DatabaseServices.Curve)
					{
						path = CAD2GIS.ToPath((Autodesk.AutoCAD.DatabaseServices.Curve)@object, spRef, HasZ, HasM, false);
					}
					if (path != null)
					{
						list.Add(path);
					}
				}
				if (list.Count == 0)
				{
					result = null;
				}
				else
				{
					polylineN.PathArray = list.ToArray();
					result = polylineN;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static Geometry ToServerPoint(Transaction t, Group grp, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				ObjectId[] allEntityIds = grp.GetAllEntityIds();
				MultipointN multipointN = new MultipointN();
				multipointN.HasM = HasM;
				multipointN.HasZ = HasZ;
				List<PointN> list = new List<PointN>();
				ObjectId[] array = allEntityIds;
				for (int i = 0; i < array.Length; i++)
				{
					ObjectId objectId = array[i];
					PointN pointN = null;
					DBObject @object = t.GetObject(objectId, 0);
					if (@object is DBPoint)
					{
						pointN = CAD2GIS.ToServerPoint((DBPoint)@object, spRef, HasZ, HasM);
					}
					else if (@object is BlockReference)
					{
						pointN = CAD2GIS.ToServerPoint((BlockReference)@object, spRef, HasZ, HasM);
					}
					if (pointN != null)
					{
						list.Add(pointN);
					}
				}
				if (list.Count == 0)
				{
					result = null;
				}
				else
				{
					multipointN.PointArray = list.ToArray();
					result = multipointN;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static Geometry ToServerMultiPoint(Transaction t, Group grp, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				ObjectId[] allEntityIds = grp.GetAllEntityIds();
				MultipointN multipointN = new MultipointN();
				multipointN.HasM = HasM;
				multipointN.HasZ = HasZ;
				List<PointN> list = new List<PointN>();
				ObjectId[] array = allEntityIds;
				for (int i = 0; i < array.Length; i++)
				{
					ObjectId objectId = array[i];
					PointN pointN = null;
					DBObject @object = t.GetObject(objectId, 0);
					if (@object is DBPoint)
					{
						pointN = CAD2GIS.ToServerPoint((DBPoint)@object, spRef, HasZ, HasM);
					}
					else if (@object is BlockReference)
					{
						pointN = CAD2GIS.ToServerPoint((BlockReference)@object, spRef, HasZ, HasM);
					}
					if (pointN != null)
					{
						list.Add(pointN);
					}
				}
				if (list.Count == 0)
				{
					result = null;
				}
				else
				{
					multipointN.PointArray = list.ToArray();
					result = multipointN;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Path ToPath(Autodesk.AutoCAD.DatabaseServices.Polyline pline, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Path result;
			try
			{
				Path path = new Path();
				if (pline.IsOnlyLines)
				{
					List<Point> list = new List<Point>();
					for (int i = 0; i < pline.NumberOfVertices; i++)
					{
						Point3d point3dAt = pline.GetPoint3dAt(i);
						PointN pointN = CAD2GIS.ToPointN(point3dAt, spRef, HasZ, HasM);
						if (pointN != null)
						{
							list.Add(pointN);
						}
					}
					if (pline.Closed && list[0] != list[list.Count - 1])
					{
						list.Add(list[0]);
					}
					path.PointArray = list.ToArray();
				}
				else
				{
					int num = pline.NumberOfVertices - 1;
					List<Segment> list2 = new List<Segment>();
					for (int j = 0; j < num; j++)
					{
						SegmentType segmentType = pline.GetSegmentType(j);
						if (segmentType == (SegmentType)0)
						{
							LineSegment3d lineSegmentAt = pline.GetLineSegmentAt(j);
							list2.Add(CAD2GIS.ToSegment(lineSegmentAt, spRef, HasZ, HasM));
						}
						else if (segmentType == (SegmentType)1)
						{
							CircularArc2d arcSegment2dAt = pline.GetArcSegment2dAt(j);
							bool isClockWise = arcSegment2dAt.IsClockWise;
							CircularArc3d arcSegmentAt = pline.GetArcSegmentAt(j);
							list2.Add(CAD2GIS.ToSegment(arcSegmentAt, isClockWise, spRef, HasZ, HasM));
						}
					}
					if (pline.Closed)
					{
						try
						{
							SegmentType segmentType2 = pline.GetSegmentType(num);
							if (segmentType2 == (SegmentType)0)
							{
								LineSegment3d lineSegmentAt2 = pline.GetLineSegmentAt(num);
								list2.Add(CAD2GIS.ToSegment(lineSegmentAt2, spRef, HasZ, HasM));
							}
							else if (segmentType2 == (SegmentType)1)
							{
								CircularArc2d arcSegment2dAt2 = pline.GetArcSegment2dAt(num);
								bool isClockWise2 = arcSegment2dAt2.IsClockWise;
								CircularArc3d arcSegmentAt2 = pline.GetArcSegmentAt(num);
								list2.Add(CAD2GIS.ToSegment(arcSegmentAt2, isClockWise2, spRef, HasZ, HasM));
							}
						}
						catch
						{
							PointN pointN2 = (PointN)list2[0].FromPoint;
							PointN pointN3 = (PointN)list2[list2.Count - 1].ToPoint;
							if (pointN2 != pointN3)
							{
								list2.Add(CAD2GIS.ToSegment(pointN3, pointN2));
							}
						}
					}
					path.SegmentArray = list2.ToArray();
				}
				result = path;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Segment ToSegment(PointN startPoint, PointN endPoint)
		{
			return new ArcGIS10Types.Line
            {
				FromPoint = startPoint,
				ToPoint = endPoint
			};
		}

		private static Segment ToSegment(LineSegment3d line, SpatialReference spRef, bool HasZ, bool HasM)
		{
			PointN fromPoint = CAD2GIS.ToPointN(line.StartPoint, spRef, HasZ, HasM);
			PointN toPoint = CAD2GIS.ToPointN(line.EndPoint, spRef, HasZ, HasM);
			return new ArcGIS10Types.Line
            {
				FromPoint = fromPoint,
				ToPoint = toPoint
			};
		}

		private static Segment ToSegment(CircularArc3d arc, bool isCW, SpatialReference spRef, bool HasZ, bool HasM)
		{
			PointN centerPoint = CAD2GIS.ToPointN(arc.Center, spRef, HasZ, HasM);
			double arg_16_0 = arc.Radius;
			PointN fromPoint = CAD2GIS.ToPointN(arc.StartPoint, spRef, HasZ, HasM);
			PointN toPoint = CAD2GIS.ToPointN(arc.EndPoint, spRef, HasZ, HasM);
			return new CircularArc
			{
				FromPoint = fromPoint,
				ToPoint = toPoint,
				CenterPoint = centerPoint,
				IsCounterClockwise = !isCW
			};
		}

		private static Ring ToRing(Autodesk.AutoCAD.DatabaseServices.Polyline pline, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Ring result;
			try
			{
				Ring ring = new Ring();
				if (pline.IsOnlyLines)
				{
					List<Point> list = new List<Point>();
					for (int i = 0; i < pline.NumberOfVertices; i++)
					{
						Point3d point3dAt = pline.GetPoint3dAt(i);
						PointN pointN = CAD2GIS.ToPointN(point3dAt, spRef, HasZ, HasM);
						if (pointN != null)
						{
							list.Add(pointN);
						}
					}
					if (list[0] != list[list.Count - 1])
					{
						list.Add(list[0]);
					}
					ring.PointArray = list.ToArray();
					result = ring;
				}
				else
				{
					int num = pline.NumberOfVertices - 1;
					List<Segment> list2 = new List<Segment>();
					for (int j = 0; j < num; j++)
					{
						SegmentType segmentType = pline.GetSegmentType(j);
						if (segmentType == (SegmentType)0)
						{
							LineSegment3d lineSegmentAt = pline.GetLineSegmentAt(j);
							list2.Add(CAD2GIS.ToSegment(lineSegmentAt, spRef, HasZ, HasM));
						}
						else if (segmentType == (SegmentType)1)
						{
							CircularArc2d arcSegment2dAt = pline.GetArcSegment2dAt(j);
							bool isClockWise = arcSegment2dAt.IsClockWise;
							CircularArc3d arcSegmentAt = pline.GetArcSegmentAt(j);
							list2.Add(CAD2GIS.ToSegment(arcSegmentAt, isClockWise, spRef, HasZ, HasM));
						}
					}
					try
					{
						SegmentType segmentType2 = pline.GetSegmentType(num);
						if (segmentType2 == (SegmentType)0)
						{
							LineSegment3d lineSegmentAt2 = pline.GetLineSegmentAt(num);
							list2.Add(CAD2GIS.ToSegment(lineSegmentAt2, spRef, HasZ, HasM));
						}
						else if (segmentType2 == (SegmentType)1)
						{
							CircularArc2d arcSegment2dAt2 = pline.GetArcSegment2dAt(num);
							bool isClockWise2 = arcSegment2dAt2.IsClockWise;
							CircularArc3d arcSegmentAt2 = pline.GetArcSegmentAt(num);
							list2.Add(CAD2GIS.ToSegment(arcSegmentAt2, isClockWise2, spRef, HasZ, HasM));
						}
					}
					catch
					{
						PointN pointN2 = (PointN)list2[0].FromPoint;
						PointN pointN3 = (PointN)list2[list2.Count - 1].ToPoint;
						if (pointN2 != pointN3)
						{
							list2.Add(CAD2GIS.ToSegment(pointN3, pointN2));
						}
					}
					ring.SegmentArray = list2.ToArray();
					result = ring;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPline(Autodesk.AutoCAD.DatabaseServices.Polyline pline, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				if (pline.Length == 0.0)
				{
					result = null;
				}
				else
				{
					PolylineN polylineN = new PolylineN();
					polylineN.SpatialReference = spRef;
					polylineN.HasM = HasM;
					polylineN.HasZ = HasZ;
					polylineN.PathArray = new Path[1];
					Path path = CAD2GIS.ToPath(pline, spRef, HasZ, HasM);
					if (path != null)
					{
						polylineN.PathArray[0] = path;
						result = polylineN;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPolygon(Autodesk.AutoCAD.DatabaseServices.Polyline pline, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				if (pline.Length == 0.0)
				{
					result = null;
				}
				else
				{
					PolygonN polygonN = new PolygonN();
					polygonN.SpatialReference = spRef;
					polygonN.HasM = HasM;
					polygonN.HasZ = HasZ;
					polygonN.RingArray = new Ring[1];
					Ring ring = CAD2GIS.ToRing(pline, spRef, HasZ, HasM);
					if (ring != null)
					{
						polygonN.RingArray[0] = ring;
						result = polygonN;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPline(Autodesk.AutoCAD.DatabaseServices.Line ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				if (ent.Length == 0.0)
				{
					result = null;
				}
				else
				{
					PolylineN polylineN = new PolylineN();
					polylineN.SpatialReference = spRef;
					polylineN.HasM = HasM;
					polylineN.HasZ = HasZ;
					polylineN.PathArray = new Path[1];
					Path path = CAD2GIS.ToPath(ent, spRef, HasZ, HasM);
					if (path != null)
					{
						polylineN.PathArray[0] = path;
						result = polylineN;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Path ToPath(Autodesk.AutoCAD.DatabaseServices.Line line, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Path result;
			try
			{
				result = new Path
				{
					PointArray = new List<Point>
					{
						CAD2GIS.ToPointN(line.StartPoint, spRef, HasZ, HasM),
						CAD2GIS.ToPointN(line.EndPoint, spRef, HasZ, HasM)
					}.ToArray()
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPline(Arc ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				if (ent.Length== 0.0)
				{
					result = null;
				}
				else
				{
					PolylineN polylineN = new PolylineN();
					polylineN.SpatialReference = spRef;
					polylineN.HasM = HasM;
					polylineN.HasZ = HasZ;
					polylineN.PathArray = new Path[1];
					Path path = CAD2GIS.ToPath(ent, spRef, HasZ, HasM);
					if (path != null)
					{
						polylineN.PathArray[0] = path;
						result = polylineN;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Path ToPath(Arc circle, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Path result;
			try
			{
				Point3d pointAtParameter = circle.GetPointAtParameter(circle.StartParam);
				Point3d pointAtParameter2 = circle.GetPointAtParameter(circle.EndParam);
				CircularArc circularArc = new CircularArc();
				circularArc.CenterPoint = CAD2GIS.ToPointN(circle.Center, spRef, HasZ, HasM);
				circularArc.FromAngle = 0.0;
				circularArc.ToAngle = 6.2831853071795862;
				circularArc.FromPoint = CAD2GIS.ToPointN(pointAtParameter, spRef, HasZ, HasM);
				circularArc.ToPoint = CAD2GIS.ToPointN(pointAtParameter2, spRef, HasZ, HasM);
				circularArc.IsMinor = false;
				circularArc.IsCounterClockwise = true;
				if (circle.Normal.Z < 0.0)
				{
					circularArc.IsCounterClockwise = false;
				}
				result = new Path
				{
					SegmentArray = new Segment[]
					{
						circularArc
					}
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPolygon(Circle ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				if (ent.Circumference == 0.0)
				{
					result = null;
				}
				else
				{
					PolygonN polygonN = new PolygonN();
					polygonN.SpatialReference = spRef;
					polygonN.HasM = HasM;
					polygonN.HasZ = HasZ;
					polygonN.RingArray = new Ring[1];
					Ring ring = CAD2GIS.ToRing(ent, spRef, HasZ, HasM);
					if (ring != null)
					{
						polygonN.RingArray[0] = ring;
						result = polygonN;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPline(Circle ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				if (ent.Circumference == 0.0)
				{
					result = null;
				}
				else if (ent.Area == 0.0)
				{
					result = null;
				}
				else
				{
					PolylineN polylineN = new PolylineN();
					polylineN.SpatialReference = spRef;
					polylineN.HasM = HasM;
					polylineN.HasZ = HasZ;
					polylineN.PathArray = new Path[1];
					Path path = CAD2GIS.ToPath(ent, spRef, HasZ, HasM);
					if (path != null)
					{
						polylineN.PathArray[0] = path;
						result = polylineN;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Path ToPath(Circle circle, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Path result;
			try
			{
				Point3d pointAtParameter = circle.GetPointAtParameter(circle.StartParam);
				Point3d pointAtParameter2 = circle.GetPointAtParameter(circle.EndParam);
				CircularArc circularArc = new CircularArc();
				circularArc.CenterPoint = CAD2GIS.ToPointN(circle.Center, spRef, HasZ, HasM);
				circularArc.FromAngle = 0.0;
				circularArc.ToAngle = 6.2831853071795862;
				circularArc.FromPoint = CAD2GIS.ToPointN(pointAtParameter, spRef, HasZ, HasM);
				circularArc.ToPoint = CAD2GIS.ToPointN(pointAtParameter2, spRef, HasZ, HasM);
				circularArc.IsMinor = false;
				result = new Path
				{
					SegmentArray = new Segment[]
					{
						circularArc
					}
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Ring ToRing(Circle circle, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Ring result;
			try
			{
				Point3d pointAtParameter = circle.GetPointAtParameter(circle.StartParam);
				Point3d pointAtParameter2 = circle.GetPointAtParameter(circle.EndParam);
				CircularArc circularArc = new CircularArc();
				circularArc.CenterPoint = CAD2GIS.ToPointN(circle.Center, spRef, HasZ, HasM);
				circularArc.FromAngle = 0.0;
				circularArc.ToAngle = 6.2831853071795862;
				circularArc.FromPoint = CAD2GIS.ToPointN(pointAtParameter, spRef, HasZ, HasM);
				circularArc.ToPoint = CAD2GIS.ToPointN(pointAtParameter2, spRef, HasZ, HasM);
				circularArc.IsMinor = false;
				result = new Ring
				{
					SegmentArray = new Segment[]
					{
						circularArc
					}
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPolygon(Ellipse ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				if (ent.Area == 0.0)
				{
					result = null;
				}
				else
				{
					PolygonN polygonN = new PolygonN();
					polygonN.SpatialReference = spRef;
					polygonN.HasM = HasM;
					polygonN.HasZ = HasZ;
					polygonN.RingArray = new Ring[1];
					Ring ring = CAD2GIS.ToRing(ent, spRef, HasZ, HasM);
					if (ring != null)
					{
						polygonN.RingArray[0] = ring;
						result = polygonN;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPline(Ellipse ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				if (ent.Area == 0.0)
				{
					result = null;
				}
				else if (ent.StartPoint != ent.EndPoint)
				{
					result = CAD2GIS.ToServerPline(ent, spRef, HasZ, HasM);
				}
				else
				{
					PolylineN polylineN = new PolylineN();
					polylineN.SpatialReference = spRef;
					polylineN.HasM = HasM;
					polylineN.HasZ = HasZ;
					polylineN.PathArray = new Path[1];
					Path path = CAD2GIS.ToPath(ent, spRef, HasZ, HasM);
					if (path != null)
					{
						polylineN.PathArray[0] = path;
						result = polylineN;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Path ToPath(Ellipse ellipse, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Path result;
			try
			{
				EllipticArc ellipticArc = new EllipticArc();
				ellipticArc.CenterPoint = CAD2GIS.ToPointN(ellipse.Center, spRef, HasZ, HasM);
				ellipticArc.EllipseStd = false;
				ellipticArc.IsCounterClockwise = false;
				ellipticArc.IsMinor = false;
				Vector3d arg_34_0 = Vector3d.XAxis;
				double radiusRatio = ellipse.RadiusRatio;
				double arg_4B_0 = ellipse.MajorAxis.Length;
				double arg_5B_0 = ellipse.MinorAxis.Length;
				ellipticArc.MinorMajorRatio = radiusRatio;
				Point3d point3d = ellipse.Center + ellipse.MajorAxis;
				Point3d point3d2 = ellipse.Center - ellipse.MajorAxis;
                Autodesk.AutoCAD.DatabaseServices.Line line = new Autodesk.AutoCAD.DatabaseServices.Line(point3d2, point3d);
				ellipticArc.Rotation = line.Angle;
				Point3d startPoint = ellipse.StartPoint;
				Point3d endPoint = ellipse.EndPoint;
				ellipticArc.FromPoint = CAD2GIS.ToPointN(startPoint, spRef, HasZ, HasM);
				ellipticArc.ToPoint = CAD2GIS.ToPointN(endPoint, spRef, HasZ, HasM);
				result = new Path
				{
					SegmentArray = new Segment[]
					{
						ellipticArc
					}
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Ring ToRing(Ellipse ellipse, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Ring result;
			try
			{
				EllipticArc ellipticArc = new EllipticArc();
				ellipticArc.CenterPoint = CAD2GIS.ToPointN(ellipse.Center, spRef, HasZ, HasM);
				ellipticArc.EllipseStd = false;
				ellipticArc.IsCounterClockwise = true;
				ellipticArc.IsMinor = false;
				Vector3d arg_34_0 = Vector3d.XAxis;
				double radiusRatio = ellipse.RadiusRatio;
				double arg_4B_0 = ellipse.MajorAxis.Length;
				double arg_5B_0 = ellipse.MinorAxis.Length;
				ellipticArc.MinorMajorRatio = radiusRatio;
				Point3d point3d = ellipse.Center + ellipse.MajorAxis;
				PointN pointN = CAD2GIS.ToPointN(point3d, spRef, HasZ, HasM);
				ellipticArc.FromPoint = pointN;
				ellipticArc.ToPoint = pointN;
				Point3d point3d2 = ellipse.Center - ellipse.MajorAxis;
                Autodesk.AutoCAD.DatabaseServices.Line line = new Autodesk.AutoCAD.DatabaseServices.Line(point3d2, point3d);
				ellipticArc.Rotation = line.Angle;
				result = new Ring
				{
					SegmentArray = new Segment[]
					{
						ellipticArc
					}
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPolygon(Mline ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				PolygonN polygonN = new PolygonN();
				polygonN.SpatialReference = spRef;
				polygonN.HasM = HasM;
				polygonN.HasZ = HasZ;
				polygonN.RingArray = new Ring[1];
				Ring ring = CAD2GIS.ToRing(ent, spRef, HasZ, HasM);
				if (ring != null)
				{
					polygonN.RingArray[0] = ring;
					result = polygonN;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPline(Mline ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				PolylineN polylineN = new PolylineN();
				polylineN.SpatialReference = spRef;
				polylineN.HasM = HasM;
				polylineN.HasZ = HasZ;
				polylineN.PathArray = new Path[1];
				Path path = CAD2GIS.ToPath(ent, spRef, HasZ, HasM);
				if (path != null)
				{
					polylineN.PathArray[0] = path;
					result = polylineN;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Path ToPath(Mline ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Path result;
			try
			{
				new PolylineN();
				int numberOfVertices = ent.NumberOfVertices;
				List<Point> list = new List<Point>();
				for (int i = 0; i < numberOfVertices; i++)
				{
					Point3d cadPt = ent.VertexAt(i);
					list.Add(CAD2GIS.ToPointN(cadPt, spRef, HasZ, HasM));
				}
				if (ent.IsClosed && list.Count > 2 && list[0] != list[list.Count - 1])
				{
					list.Add(list[0]);
				}
				result = new Path
				{
					PointArray = list.ToArray()
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Ring ToRing(Mline ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Ring result;
			try
			{
				new PolylineN();
				int numberOfVertices = ent.NumberOfVertices;
				List<Point> list = new List<Point>();
				for (int i = 0; i < numberOfVertices; i++)
				{
					Point3d cadPt = ent.VertexAt(i);
					list.Add(CAD2GIS.ToPointN(cadPt, spRef, HasZ, HasM));
				}
				if (ent.IsClosed && list.Count > 2 && list[0] != list[list.Count - 1])
				{
					list.Add(list[0]);
				}
				result = new Ring
				{
					PointArray = list.ToArray()
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPolygon(Polyline3d ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				if (ent.Length == 0.0)
				{
					result = null;
				}
				else
				{
					PolygonN polygonN = new PolygonN();
					polygonN.SpatialReference = spRef;
					polygonN.HasM = HasM;
					polygonN.HasZ = HasZ;
					polygonN.RingArray = new Ring[1];
					Ring ring = CAD2GIS.ToRing(ent, spRef, HasZ, HasM);
					if (ring != null)
					{
						polygonN.RingArray[0] = ring;
						result = polygonN;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPline(Polyline3d ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				if (ent.Length == 0.0)
				{
					result = null;
				}
				else
				{
					PolylineN polylineN = new PolylineN();
					polylineN.SpatialReference = spRef;
					polylineN.HasM = HasM;
					polylineN.HasZ = HasZ;
					polylineN.PathArray = new Path[1];
					Path path = CAD2GIS.ToPath(ent, spRef, HasZ, HasM);
					if (path != null)
					{
						polylineN.PathArray[0] = path;
						result = polylineN;
					}
					else
					{
						result = null;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Path ToPath(Polyline3d ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
            //Path result;
            //try
            //{
            //	List<Point> list = new List<Point>();
            //	new PolylineN();
            //	Document document = AfaDocData.ActiveDocData.Document;
            //	Database database = document.Database;
            //             Autodesk.AutoCAD.ApplicationServices.TransactionManager transactionManager = document.TransactionManager;
            //	using (Transaction transaction = transactionManager.StartTransaction())
            //	{
            //		BlockTable blockTable = (BlockTable)transaction.GetObject(database.BlockTableId, 0);
            //		BlockTableRecord arg_5B_0 = (BlockTableRecord)transaction.GetObject(blockTable.Item(BlockTableRecord.ModelSpace), 0);
            //		foreach (ObjectId objectId in ent)
            //		{
            //			try
            //			{
            //				PolylineVertex3d polylineVertex3d = (PolylineVertex3d)transaction.GetObject(objectId, 0);
            //				if (polylineVertex3d.get_VertexType() != 1)
            //				{
            //					Point3d position = polylineVertex3d.get_Position();
            //					list.Add(CAD2GIS.ToPointN(position, spRef, HasZ, HasM));
            //				}
            //			}
            //			catch
            //			{
            //			}
            //		}
            //		transaction.Commit();
            //	}
            //	if (ent.get_Closed() && list.Count > 2 && list[0] != list[list.Count - 1])
            //	{
            //		list.Add(list[0]);
            //	}
            //	result = new Path
            //	{
            //		PointArray = list.ToArray()
            //	};
            //}
            //catch
            //{
            //	result = null;
            //}
            //return result;
            try
            {
                List<Point> list = new List<Point>();
                PolylineN en1 = new PolylineN();
                Document document = AfaDocData.ActiveDocData.Document;
                Database database = document.Database;
                using (Transaction transaction = document.TransactionManager.StartTransaction())
                {
                    BlockTableRecord record1 = (BlockTableRecord)transaction.GetObject(((BlockTable)transaction.GetObject(database.BlockTableId, 0))[BlockTableRecord.ModelSpace], OpenMode.ForRead);
                    foreach (ObjectId id in ent)
                    {
                        try
                        {
                            PolylineVertex3d vertexd = (PolylineVertex3d)transaction.GetObject(id, OpenMode.ForRead);
                            if (vertexd.VertexType == Vertex3dType.ControlVertex)
                            {
                                continue;
                            }
                            list.Add(ToPointN(vertexd.Position, spRef, HasZ, HasM));
                        }
                        catch
                        {
                        }
                    }
                    transaction.Commit();
                }
                if ((ent.Closed && (list.Count > 2)) && (list[0] != list[list.Count - 1]))
                {
                    list.Add(list[0]);
                }
                return new Path { PointArray = list.ToArray() };
            }
            catch
            {
                return null;
            }
        }

		private static Ring ToRing(Polyline3d ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Ring result;
			try
			{
				List<Point> list = new List<Point>();
				new PolylineN();
				Document document = AfaDocData.ActiveDocData.Document;
				Database arg_1D_0 = document.Database;
                Autodesk.AutoCAD.ApplicationServices.TransactionManager transactionManager = document.TransactionManager;
				using (Transaction transaction = transactionManager.StartTransaction())
				{
					foreach (ObjectId objectId in ent)
					{
						PolylineVertex3d polylineVertex3d = (PolylineVertex3d)transaction.GetObject(objectId, 0);
						if (polylineVertex3d.VertexType !=(Vertex3dType)1)
						{
							Point3d position = polylineVertex3d.Position;
							list.Add(CAD2GIS.ToPointN(position, spRef, HasZ, HasM));
						}
					}
					transaction.Commit();
				}
				if (ent.Closed && list.Count > 2 && list[0] != list[list.Count - 1])
				{
					list.Add(list[0]);
				}
				result = new Ring
				{
					PointArray = list.ToArray()
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPolygon(Face ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				PolygonN polygonN = new PolygonN();
				polygonN.SpatialReference = spRef;
				polygonN.HasM = HasM;
				polygonN.HasZ = HasZ;
				polygonN.RingArray = new Ring[1];
				Ring ring = CAD2GIS.ToRing(ent, spRef, HasZ, HasM);
				if (ring != null)
				{
					polygonN.RingArray[0] = ring;
					result = polygonN;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPline(Face ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				PolylineN polylineN = new PolylineN();
				polylineN.SpatialReference = spRef;
				polylineN.HasM = HasM;
				polylineN.HasZ = HasZ;
				polylineN.PathArray = new Path[1];
				Path path = CAD2GIS.ToPath(ent, spRef, HasZ, HasM);
				if (path != null)
				{
					polylineN.PathArray[0] = path;
					result = polylineN;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Path ToPath(Face ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Path result;
			try
			{
				List<Point> list = new List<Point>();
				new PolylineN();
				Point3d vertexAt = ent.GetVertexAt(0);
				list.Add(CAD2GIS.ToPointN(vertexAt, spRef, HasZ, HasM));
				Point3d vertexAt2 = ent.GetVertexAt(1);
				list.Add(CAD2GIS.ToPointN(vertexAt2, spRef, HasZ, HasM));
				Point3d vertexAt3 = ent.GetVertexAt(2);
				list.Add(CAD2GIS.ToPointN(vertexAt3, spRef, HasZ, HasM));
				try
				{
					Point3d vertexAt4 = ent.GetVertexAt(3);
					list.Add(CAD2GIS.ToPointN(vertexAt4, spRef, HasZ, HasM));
				}
				catch
				{
				}
				if (list.Count > 2 && list[0] != list[list.Count - 1])
				{
					list.Add(list[0]);
				}
				result = new Path
				{
					PointArray = list.ToArray()
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Ring ToRing(Face ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Ring result;
			try
			{
				List<Point> list = new List<Point>();
				new PolylineN();
				Point3d vertexAt = ent.GetVertexAt(0);
				list.Add(CAD2GIS.ToPointN(vertexAt, spRef, HasZ, HasM));
				Point3d vertexAt2 = ent.GetVertexAt(1);
				list.Add(CAD2GIS.ToPointN(vertexAt2, spRef, HasZ, HasM));
				Point3d vertexAt3 = ent.GetVertexAt(2);
				list.Add(CAD2GIS.ToPointN(vertexAt3, spRef, HasZ, HasM));
				try
				{
					Point3d vertexAt4 = ent.GetVertexAt(3);
					list.Add(CAD2GIS.ToPointN(vertexAt4, spRef, HasZ, HasM));
				}
				catch
				{
				}
				if (list.Count > 2 && list[0] != list[list.Count - 1])
				{
					list.Add(list[0]);
				}
				result = new Ring
				{
					PointArray = list.ToArray()
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPolygon(Spline ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				PolygonN polygonN = new PolygonN();
				polygonN.SpatialReference = spRef;
				polygonN.HasM = HasM;
				polygonN.HasZ = HasZ;
				polygonN.RingArray = new Ring[1];
				Ring ring = CAD2GIS.ToRing(ent, spRef, HasZ, HasM);
				if (ring != null)
				{
					polygonN.RingArray[0] = ring;
					result = polygonN;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPline(Spline ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				PolylineN polylineN = new PolylineN();
				polylineN.SpatialReference = spRef;
				polylineN.HasM = HasM;
				polylineN.HasZ = HasZ;
				polylineN.PathArray = new Path[1];
				Path path = CAD2GIS.ToPath(ent, spRef, HasZ, HasM);
				if (path != null)
				{
					polylineN.PathArray[0] = path;
					result = polylineN;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Path ToPath(Spline ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Path result;
			try
			{
				BezierCurve bezierCurve = new BezierCurve();
				bezierCurve.FromPoint = CAD2GIS.ToPointN(ent.StartPoint, spRef, HasZ, HasM);
				bezierCurve.ToPoint = CAD2GIS.ToPointN(ent.EndPoint, spRef, HasZ, HasM);
				Document document = AfaDocData.ActiveDocData.Document;
				Database arg_3F_0 = document.Database;
				NurbsData nurbsData = ent.NurbsData;
				bezierCurve.Degree = nurbsData.Degree;
				Point3dCollection controlPoints = nurbsData.GetControlPoints();
				List<PointN> list = new List<PointN>();
				foreach (Point3d cadPt in controlPoints)
				{
					list.Add(CAD2GIS.ToPointN(cadPt, spRef, HasZ, HasM));
				}
				bezierCurve.ControlPointArray = list.ToArray();
				List<Segment> list2 = new List<Segment>();
				list2.Add(bezierCurve);
				result = new Path
				{
					SegmentArray = list2.ToArray()
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Ring ToRing(Spline ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Ring result;
			try
			{
				BezierCurve bezierCurve = new BezierCurve();
				Document document = AfaDocData.ActiveDocData.Document;
				Database arg_17_0 = document.Database;
				NurbsData nurbsData = ent.NurbsData;
				bezierCurve.Degree = nurbsData.Degree;
				Point3dCollection controlPoints = nurbsData.GetControlPoints();
				List<PointN> list = new List<PointN>();
				foreach (Point3d cadPt in controlPoints)
				{
					list.Add(CAD2GIS.ToPointN(cadPt, spRef, HasZ, HasM));
				}
				bezierCurve.ControlPointArray = list.ToArray();
				List<Segment> list2 = new List<Segment>();
				list2.Add(bezierCurve);
				result = new Ring
				{
					SegmentArray = list2.ToArray()
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static PointN ToServerPoint(DBPoint pt, SpatialReference spRef, bool HasZ, bool HasM)
		{
			PointN result;
			try
			{
				PointN pointN = CAD2GIS.ToPointN(pt.Position, spRef, HasZ, HasM);
				result = pointN;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static MultipointN ToServerMultiPoint(DBPoint pt, SpatialReference spRef, bool HasZ, bool HasM)
		{
			MultipointN multipointN = new MultipointN();
			multipointN.HasZ = HasZ;
			multipointN.PointArray = new PointN[1];
			multipointN.PointArray[0] = CAD2GIS.ToServerPoint(pt, spRef, HasZ, HasM);
			return multipointN;
		}

		private static PointN ToServerPoint(BlockReference blk, SpatialReference spRef, bool HasZ, bool HasM)
		{
			PointN result;
			try
			{
				PointN pointN = CAD2GIS.ToPointN(blk.Position, spRef, HasZ, HasM);
				result = pointN;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static MultipointN ToServerMultiPoint(BlockReference blk, SpatialReference spRef, bool HasZ, bool HasM)
		{
			MultipointN result;
			try
			{
				MultipointN multipointN = new MultipointN();
				multipointN.HasZ = HasZ;
				multipointN.PointArray = new PointN[1];
				multipointN.PointArray[0] = CAD2GIS.ToServerPoint(blk, spRef, HasZ, HasM);
				result = multipointN;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPline(Autodesk.AutoCAD.DatabaseServices.Curve ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				PolylineN polylineN = new PolylineN();
				polylineN.SpatialReference = spRef;
				polylineN.HasM = HasM;
				polylineN.HasZ = HasZ;
				polylineN.PathArray = new Path[1];
				Path path = CAD2GIS.ToPath(ent, spRef, HasZ, HasM, false);
				if (path != null)
				{
					polylineN.PathArray[0] = path;
					result = polylineN;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Geometry ToServerPolygon(Autodesk.AutoCAD.DatabaseServices.Curve ent, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Geometry result;
			try
			{
				PolygonN polygonN = new PolygonN();
				polygonN.SpatialReference = spRef;
				polygonN.HasM = HasM;
				polygonN.HasZ = HasZ;
				polygonN.RingArray = new Ring[1];
				Ring ring = CAD2GIS.ToRing(ent, spRef, HasZ, HasM);
				if (ring != null)
				{
					polygonN.RingArray[0] = ring;
					result = polygonN;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Path ToPath(Autodesk.AutoCAD.DatabaseServices.Curve curve, SpatialReference spRef, bool HasZ, bool HasM, bool closed)
		{
			Path result;
			try
			{
				Path path = new Path();
				List<Point> pointsOnCurve = CAD2GIS.GetPointsOnCurve(curve, spRef, HasZ, HasM, closed);
				path.PointArray = pointsOnCurve.ToArray();
				result = path;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static Ring ToRing(Autodesk.AutoCAD.DatabaseServices.Curve curve, SpatialReference spRef, bool HasZ, bool HasM)
		{
			Ring result;
			try
			{
				Ring ring = new Ring();
				List<Point> pointsOnCurve = CAD2GIS.GetPointsOnCurve(curve, spRef, HasZ, HasM, true);
				ring.PointArray = pointsOnCurve.ToArray();
				result = ring;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static PointN ToPointN(Point3d cadPt, SpatialReference spRef, bool HasZ, bool HasM)
		{
			PointN result;
			try
			{
				result = new PointN
				{
					SpatialReference = spRef,
					X = cadPt.X,
					Y = cadPt.Y,
					Z = cadPt.Z,
					ZSpecified = HasZ,
					MSpecified = HasM
				};
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static List<Point> GetPointsOnCurve2(Autodesk.AutoCAD.DatabaseServices.Curve curve, SpatialReference spRef, bool HasZ, bool HasM, bool closed)
		{
			double num = Math.Abs(curve.EndParam - curve.StartParam) / 100.0;
			curve.GetDistanceAtParameter(curve.StartParam);
			curve.GetDistanceAtParameter(curve.EndParam);
			double arg_3D_0 = curve.StartParam;
			double arg_44_0 = curve.EndParam;
			List<Point3d> list = new List<Point3d>();
			double num2 = curve.StartParam;
			Point3d point3d = curve.GetPointAtParameter(num2);
			num2 += num;
			Point3d point3d2 = curve.GetPointAtParameter(num2);
			num2 += num;
			Point3d pointAtParameter = curve.GetPointAtParameter(num2);
			num2 += num;
			list.Add(point3d);
			int num3 = 0;
			do
			{
				LineSegment3d lineSegment3d = new LineSegment3d(point3d, pointAtParameter);
				if (!lineSegment3d.IsOn(point3d2))
				{
					list.Add(point3d2);
					point3d = point3d2;
				}
				else
				{
					num3++;
				}
				point3d2 = pointAtParameter;
				pointAtParameter = curve.GetPointAtParameter(num2);
				num2 += num;
			}
			while (num2 < curve.EndParam);
			list.Add(curve.EndPoint);
			if (list.Count > 1 && closed && list[0] != list[list.Count - 1])
			{
				list.Add(list[0]);
			}
			List<Point> list2 = new List<Point>();
			foreach (Point3d current in list)
			{
				list2.Add(CAD2GIS.ToPointN(current, spRef, HasZ, HasM));
			}
			return list2;
		}

		private static List<Point> GetPointsOnCurve(Autodesk.AutoCAD.DatabaseServices.Curve curve, SpatialReference spRef, bool HasZ, bool HasM, bool closed)
		{
			double num = Math.Abs(curve.EndParam - curve.StartParam) / 100.0;
			curve.GetDistanceAtParameter(curve.StartParam);
			curve.GetDistanceAtParameter(curve.EndParam);
			double arg_3D_0 = curve.StartParam;
			double arg_44_0 = curve.EndParam;
			List<Point> list = new List<Point>();
			for (double num2 = curve.StartParam; num2 < curve.EndParam; num2 += num)
			{
				try
				{
					Point3d pointAtParameter = curve.GetPointAtParameter(num2);
					PointN item = CAD2GIS.ToPointN(pointAtParameter, spRef, HasZ, HasM);
					list.Add(item);
				}
				catch
				{
				}
			}
			if (list.Count > 1 && closed && list[0] != list[list.Count - 1])
			{
				list.Add(list[0]);
			}
			return list;
		}
	}
}
