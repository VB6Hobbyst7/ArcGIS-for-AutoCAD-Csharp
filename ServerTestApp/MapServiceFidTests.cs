using ArcGIS10Types;
using System;
using System.Collections.Generic;

namespace ServerTestApp
{
	public class MapServiceFidTests
	{
		private static MapServerInfo _mapinfo;

		private static MapServerProxy _mapservice;

		private static string name;

		private static SpatialReference completeSpRef;

		private static SpatialReference wkidSpRef;

		private static SpatialReference wktSpRef;

		private static SpatialReference alternateSpRef;

		public static SpatialReference dbgInSR;

		public static SpatialReference dbgOutSR;

		public static Geometry_GeometryServer _dbgGeomServer;

		public static Envelope dbgEnv;

		public static void Initialize()
		{
			MapServiceFidTests._mapservice = new MapServerProxy();
			MapServiceFidTests._mapservice.Url = "http://mashup/ArcGIS/services/MapServices/Austin/MapServer";
			MapServiceFidTests.name = MapServiceFidTests._mapservice.GetDefaultMapName();
			MapServiceFidTests._mapinfo = MapServiceFidTests._mapservice.GetServerInfo(MapServiceFidTests.name);
		}

		public static void QueryFeaturesTest()
		{
			MapServiceFidTests.Initialize();
			LayerDescription layerDescription = null;
			LayerDescription[] layerDescriptions = MapServiceFidTests._mapinfo.DefaultMapDescription.LayerDescriptions;
			for (int i = 0; i < layerDescriptions.Length; i++)
			{
				LayerDescription layerDescription2 = layerDescriptions[i];
				if (layerDescription2.LayerID == 13)
				{
					layerDescription = layerDescription2;
					break;
				}
			}
			string geomFieldName = MapServiceFidTests.GetGeomFieldName(MapServiceFidTests._mapinfo, layerDescription.LayerID);
			SpatialFilter spatialfilter = MapServiceFidTests.BuildSpatialFilter((EnvelopeN)MapServiceFidTests._mapinfo.Extent, geomFieldName, MapServiceFidTests._mapinfo.DefaultMapDescription.SpatialReference);
			MapServiceFidTests.completeSpRef = MapServiceFidTests._mapinfo.DefaultMapDescription.SpatialReference;
			int num = 4326;
			MapServiceFidTests.wkidSpRef = MapServiceFidTests.SpRefFromWKID(ref num);
			string wKT = MapServiceFidTests.completeSpRef.WKT;
			MapServiceFidTests.wktSpRef = MapServiceFidTests.SpRefFromWKT(ref wKT);
			string text = "PROJCS[\"WGS_1984_Web_Mercator_Auxiliary_Sphere\",GEOGCS[\"GCS_WGS_1984\",DATUM[\"D_WGS_1984\",SPHEROID[\"WGS_1984\",6378137.0,298.257223563]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Mercator_Auxiliary_Sphere\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],PARAMETER[\"Standard_Parallel_1\",0.0],PARAMETER[\"Auxiliary_Sphere_Type\",0.0],UNIT[\"Meter\",1.0],AUTHORITY[\"EPSG\",3857]]";
			MapServiceFidTests.alternateSpRef = MapServiceFidTests.SpRefFromWKT(ref text);
			MapServiceFidTests.TestAlt(layerDescription, spatialfilter);
		}

		public static void TestAlt(LayerDescription activelayerdesc, SpatialFilter spatialfilter)
		{
			MapServiceFidTests.TheTest(activelayerdesc, spatialfilter, MapServiceFidTests.completeSpRef, MapServiceFidTests.alternateSpRef);
		}

		public static void TestOne(LayerDescription activelayerdesc, SpatialFilter spatialfilter)
		{
			MapServiceFidTests.TheTest(activelayerdesc, spatialfilter, MapServiceFidTests.completeSpRef, MapServiceFidTests.completeSpRef);
		}

		public static void TestTwo(LayerDescription activelayerdesc, SpatialFilter spatialfilter)
		{
			MapServiceFidTests.TheTest(activelayerdesc, spatialfilter, MapServiceFidTests.wktSpRef, MapServiceFidTests.wktSpRef);
		}

		public static void TestThree(LayerDescription activelayerdesc, SpatialFilter spatialfilter)
		{
			MapServiceFidTests.TheTest(activelayerdesc, spatialfilter, MapServiceFidTests.wkidSpRef, MapServiceFidTests.wkidSpRef);
		}

		public static void TestFour(LayerDescription activelayerdesc, SpatialFilter spatialfilter)
		{
			MapServiceFidTests.TheTest(activelayerdesc, spatialfilter, MapServiceFidTests.completeSpRef, MapServiceFidTests.wktSpRef);
		}

		public static void TestFive(LayerDescription activelayerdesc, SpatialFilter spatialfilter)
		{
			MapServiceFidTests.TheTest(activelayerdesc, spatialfilter, MapServiceFidTests.completeSpRef, MapServiceFidTests.wkidSpRef);
		}

		public static void TestSix(LayerDescription activelayerdesc, SpatialFilter spatialfilter)
		{
			MapServiceFidTests.TheTest(activelayerdesc, spatialfilter, MapServiceFidTests.wktSpRef, MapServiceFidTests.wkidSpRef);
		}

		public static void TestSeven(LayerDescription activelayerdesc, SpatialFilter spatialfilter)
		{
			MapServiceFidTests.TheTest(activelayerdesc, spatialfilter, MapServiceFidTests.wktSpRef, MapServiceFidTests.completeSpRef);
		}

		public static void TestEight(LayerDescription activelayerdesc, SpatialFilter spatialfilter)
		{
			MapServiceFidTests.TheTest(activelayerdesc, spatialfilter, MapServiceFidTests.wkidSpRef, MapServiceFidTests.completeSpRef);
		}

		public static void TestNine(LayerDescription activelayerdesc, SpatialFilter spatialfilter)
		{
			MapServiceFidTests.TheTest(activelayerdesc, spatialfilter, MapServiceFidTests.wkidSpRef, MapServiceFidTests.wktSpRef);
		}

		private static void DumpSpatialReference(SpatialReference spRef)
		{
			string.IsNullOrEmpty(spRef.WKT);
			bool arg_12_0 = spRef.WKIDSpecified;
		}

		private static void DumpEnvelope(EnvelopeN env)
		{
			MapServiceFidTests.DumpSpatialReference(env.SpatialReference);
		}

		private static void DumpSpatialFilter(SpatialFilter sf)
		{
			EnvelopeN env = (EnvelopeN)sf.FilterGeometry;
			MapServiceFidTests.DumpEnvelope(env);
			MapServiceFidTests.DumpSpatialReference(sf.OutputSpatialReference);
			if (!string.IsNullOrEmpty(sf.WhereClause))
			{
				return;
			}
			string arg_31_0 = sf.WhereClause;
		}

		public static void TheTest(LayerDescription activelayerdesc, SpatialFilter spatialfilter, SpatialReference sp1, SpatialReference sp2)
		{
			string oIDFieldName = MapServiceFidTests.GetOIDFieldName(MapServiceFidTests._mapinfo, activelayerdesc.LayerID);
			spatialfilter.OutputSpatialReference = sp1;
			EnvelopeN envelopeN = spatialfilter.FilterGeometry as EnvelopeN;
			envelopeN.SpatialReference = sp2;
			spatialfilter.FilterGeometry = envelopeN;
			FIDSet fIDSet = MapServiceFidTests.BuildFIDSet(MapServiceFidTests.name, activelayerdesc, spatialfilter);
			QueryFilter queryFilter = MapServiceFidTests.BuildQueryFromFIDSet(fIDSet, sp1);
			queryFilter.OutputSpatialReference = sp1;
			RecordSet recordSet = MapServiceFidTests.QueryFeatures(activelayerdesc, queryFilter, false);
			RecordSet rs = MapServiceFidTests.QueryFeatures(activelayerdesc, queryFilter, true);
			int num = MapServiceFidTests.FindGeometryFieldIndex(recordSet);
			SpatialReference spatialReference = recordSet.Fields.FieldArray[num].GeometryDef.SpatialReference;
			recordSet = MapServiceFidTests.ProjectRecordSet(MapServiceFidTests._mapinfo.FullExtent, recordSet, num, spatialReference, sp2);
			rs = MapServiceFidTests.ProjectRecordSet(MapServiceFidTests._mapinfo.FullExtent, rs, num, spatialReference, sp2);
			FIDSet fIDSet2 = MapServiceFidTests.BuildDeltaFIDSet(fIDSet, recordSet, oIDFieldName);
			while (fIDSet2.FIDArray.Length > 0)
			{
				queryFilter = MapServiceFidTests.BuildQueryFromFIDSet(fIDSet2, sp1);
				queryFilter.OutputSpatialReference = sp1;
				recordSet = MapServiceFidTests.QueryFeatures(activelayerdesc, queryFilter, false);
				rs = MapServiceFidTests.QueryFeatures(activelayerdesc, queryFilter, true);
				fIDSet2 = MapServiceFidTests.BuildDeltaFIDSet(fIDSet2, recordSet, oIDFieldName);
			}
		}

		private static QueryFilter BuildQueryFromFIDSet(FIDSet fidList, SpatialReference outputSR)
		{
			return new QueryFilter
			{
				FIDSet = fidList,
				OutputSpatialReference = outputSR
			};
		}

		private static FIDSet BuildDeltaFIDSet(FIDSet masterFidList, RecordSet rs, string oidFieldName)
		{
			IList<int> collection = (IList<int>)masterFidList.FIDArray;
			FIDSet fIDSet = new FIDSet();
			if (rs.Records.Length == masterFidList.FIDArray.Length)
			{
				List<int> list = new List<int>();
				fIDSet.FIDArray = list.ToArray();
				return fIDSet;
			}
			List<int> list2 = new List<int>(collection);
			int num = -1;
			for (int i = 0; i < rs.Fields.FieldArray.Length; i++)
			{
				Field field = rs.Fields.FieldArray[i];
				if (field.Name == oidFieldName)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return null;
			}
			for (int j = 0; j < rs.Records.Length; j++)
			{
				Record record = rs.Records[j];
				int item = (int)record.Values[num];
				if (list2.Contains(item))
				{
					list2.Remove(item);
				}
			}
			fIDSet.FIDArray = list2.ToArray();
			return fIDSet;
		}

		private static FIDSet BuildFIDSet(string name, LayerDescription activelayerdesc, SpatialFilter spatialfilter)
		{
			FIDSet result;
			try
			{
				result = MapServiceFidTests._mapservice.QueryFeatureIDs2(name, activelayerdesc, spatialfilter);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static int FindGeometryFieldIndex(RecordSet recordset)
		{
			int result;
			try
			{
				int num = -1;
				Field[] fieldArray = recordset.Fields.FieldArray;
				for (int i = 0; i < fieldArray.Length; i++)
				{
					Field field = fieldArray[i];
					num++;
					if (field.Type == esriFieldType.esriFieldTypeGeometry)
					{
						result = num;
						return result;
					}
				}
				result = -1;
			}
			catch
			{
				result = -1;
			}
			return result;
		}

		public static RecordSet QueryFeatures(LayerDescription activelayerdesc, QueryFilter qf, bool useTrueCurves)
		{
			QueryResultOptions queryResultOptions = new QueryResultOptions();
			queryResultOptions.Format = esriQueryResultFormat.esriQueryResultRecordSetAsObject;
			GeometryResultOptions geometryResultOptions = new GeometryResultOptions();
			geometryResultOptions.MaximumDeviation = 0.0;
			geometryResultOptions.MaximumSegmentLength = -1.0;
			geometryResultOptions.MaximumAllowableOffset = 0.0;
			if (useTrueCurves)
			{
				geometryResultOptions.DensifyGeometries = false;
			}
			else
			{
				geometryResultOptions.DensifyGeometries = true;
				geometryResultOptions.GeneralizeGeometries = true;
			}
			activelayerdesc.LayerResultOptions = new LayerResultOptions
			{
				FormatValuesInResults = true,
				GeometryResultOptions = geometryResultOptions,
				IncludeGeometry = true,
				ReturnFieldNamesInResults = true
			};
			QueryResult queryResult = MapServiceFidTests._mapservice.QueryFeatureData2(MapServiceFidTests.name, activelayerdesc, qf, queryResultOptions);
			return (RecordSet)queryResult.Object;
		}

		public static SpatialReference SpRefFromWKID(ref int wkid)
		{
			SpatialReference result;
			try
			{
				if (!MapServiceFidTests.IsGeographicWKID(wkid))
				{
					result = new ProjectedCoordinateSystem
					{
						WKID = wkid,
						WKIDSpecified = true
					};
				}
				else
				{
					result = new GeographicCoordinateSystem
					{
						WKID = wkid,
						WKIDSpecified = true
					};
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private static bool IsGeographicWKID(int WKID)
		{
			return (WKID < 2000 || WKID > 4000) && ((WKID >= 4001 && WKID <= 4904) || ((WKID < 20002 || WKID > 32766) && ((WKID >= 37001 && WKID <= 37260) || ((WKID < 53001 || WKID > 53049) && (WKID < 54001 || WKID > 54053) && (WKID < 65061 || WKID > 65063) && (WKID < 102001 || WKID > 103971) && (WKID < 104000 || WKID > 104970 || true)))));
		}

		public static SpatialReference SpRefFromWKT(ref string wkt)
		{
			SpatialReference result;
			try
			{
				if (wkt.StartsWith("PROJCS", StringComparison.CurrentCultureIgnoreCase))
				{
					result = new ProjectedCoordinateSystem
					{
						WKT = wkt
					};
				}
				else if (wkt.StartsWith("GEOGCS", StringComparison.CurrentCultureIgnoreCase))
				{
					result = new GeographicCoordinateSystem
					{
						WKT = wkt
					};
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

		public static SpatialFilter BuildSpatialFilter(EnvelopeN env, string geomField, SpatialReference spRef)
		{
			SpatialFilter result;
			try
			{
				SpatialFilter spatialFilter = new SpatialFilter();
				env.SpatialReference = spRef;
				spatialFilter.FilterGeometry = env;
				if (!string.IsNullOrEmpty(geomField))
				{
					spatialFilter.GeometryFieldName = geomField;
				}
				spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
				if (!string.IsNullOrEmpty(env.SpatialReference.WKT))
				{
					string wKT = env.SpatialReference.WKT;
					spatialFilter.OutputSpatialReference = MapServiceFidTests.SpRefFromWKT(ref wKT);
				}
				else if (env.SpatialReference.WKIDSpecified)
				{
					int wKID = env.SpatialReference.WKID;
					spatialFilter.OutputSpatialReference = MapServiceFidTests.SpRefFromWKID(ref wKID);
				}
				result = spatialFilter;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static string GetOIDFieldName(MapServerInfo mapinfo, int layerID)
		{
			MapLayerInfo[] mapLayerInfos = mapinfo.MapLayerInfos;
			MapLayerInfo[] array = mapLayerInfos;
			for (int i = 0; i < array.Length; i++)
			{
				MapLayerInfo mapLayerInfo = array[i];
				if (layerID == mapLayerInfo.LayerID)
				{
					return mapLayerInfo.IDField;
				}
			}
			return string.Empty;
		}

		public static string GetGeomFieldName(MapServerInfo mapinfo, int layerID)
		{
			MapLayerInfo[] mapLayerInfos = mapinfo.MapLayerInfos;
			string empty = string.Empty;
			MapLayerInfo[] array = mapLayerInfos;
			for (int i = 0; i < array.Length; i++)
			{
				MapLayerInfo mapLayerInfo = array[i];
				if (layerID == mapLayerInfo.LayerID)
				{
					Field[] fieldArray = mapLayerInfo.Fields.FieldArray;
					Field[] array2 = fieldArray;
					for (int j = 0; j < array2.Length; j++)
					{
						Field field = array2[j];
						if (field.Type == esriFieldType.esriFieldTypeGeometry)
						{
							empty = field.Name;
							break;
						}
					}
				}
			}
			return empty;
		}

		public static RecordSet ProjectRecordSet(Envelope env, RecordSet rs, int geomFieldIndex, SpatialReference inSR, SpatialReference outSR)
		{
			MapServiceFidTests._dbgGeomServer = new Geometry_GeometryServer();
			MapServiceFidTests._dbgGeomServer.Url = "http://mashup/ArcGIS/services/Geometry/GeometryServer";
			MapServiceFidTests.dbgInSR = inSR;
			MapServiceFidTests.dbgOutSR = outSR;
			MapServiceFidTests.dbgEnv = env;
			if (geomFieldIndex < 0)
			{
				return null;
			}
			if (rs == null)
			{
				return null;
			}
			if (inSR == null || outSR == null)
			{
				return rs;
			}
			RecordSet result;
			try
			{
				List<Geometry> list = new List<Geometry>(rs.Records.Length);
				Record[] records = rs.Records;
				for (int i = 0; i < records.Length; i++)
				{
					Record record = records[i];
					Geometry geometry = record.Values[geomFieldIndex] as Geometry;
					if (geometry != null)
					{
						list.Add(geometry);
					}
				}
				Geometry[] inGeometryArray = list.ToArray();
				Geometry[] array = MapServiceFidTests._dbgGeomServer.Project(MapServiceFidTests.dbgInSR, MapServiceFidTests.dbgOutSR, false, null, MapServiceFidTests.dbgEnv, inGeometryArray);
				int num = 0;
				Record[] records2 = rs.Records;
				for (int j = 0; j < records2.Length; j++)
				{
					Record record2 = records2[j];
					record2.Values[geomFieldIndex] = array[num++];
				}
				result = rs;
			}
			catch (Exception ex)
			{
				string arg_F2_0 = ex.Message;
				result = rs;
			}
			return result;
		}
	}
}
