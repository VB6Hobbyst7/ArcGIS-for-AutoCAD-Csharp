using ArcGIS10Types;
using System;

namespace AFA
{
	public class AGSSpatialReference
	{
		public string Name
		{
			get;
			set;
		}

		public string WKT
		{
			get;
			set;
		}

		public AGSSpatialReference(AGSSpatialReference src)
		{
			if (!string.IsNullOrEmpty(src.Name))
			{
				this.Name = string.Copy(src.Name);
			}
			if (!string.IsNullOrEmpty(src.WKT))
			{
				this.WKT = string.Copy(src.WKT);
			}
		}

		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.WKT))
			{
				return this.WKT;
			}
			return "<Unknown>";
		}

		public static SpatialReference SpRefFromString(string str)
		{
			SpatialReference result;
			try
			{
				SpatialReference spatialReference;
				if (AGSSpatialReference.IsInteger(str))
				{
					int num = int.Parse(str);
					spatialReference = AGSSpatialReference.SpRefFromWKID(ref num);
				}
				else
				{
					string text = str;
					spatialReference = AGSSpatialReference.SpRefFromWKT(ref text);
				}
				result = spatialReference;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static SpatialReference SpRefFromWKID(ref int wkid)
		{
			SpatialReference result;
			try
			{
				if (!AGSSpatialReference.IsGeographicWKID(wkid))
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

		private static bool IsGeographicWKID(int WKID)
		{
			return (WKID < 2000 || WKID > 4000) && ((WKID >= 4001 && WKID <= 4904) || ((WKID < 20002 || WKID > 32766) && ((WKID >= 37001 && WKID <= 37260) || ((WKID < 53001 || WKID > 53049) && (WKID < 54001 || WKID > 54053) && (WKID < 65061 || WKID > 65063) && (WKID < 102001 || WKID > 103971) && (WKID < 104000 || WKID > 104970 || true)))));
		}

		public static string GetSpRefString(SpatialReference spRef)
		{
			if (spRef == null)
			{
				return "";
			}
			string result;
			try
			{
				if (!string.IsNullOrEmpty(spRef.WKT))
				{
					result = spRef.WKT;
				}
				else if (spRef.WKIDSpecified && spRef.WKID != 0)
				{
					result = spRef.WKID.ToString();
				}
				else
				{
					result = "";
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static string GetWKTFrom(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			string result;
			try
			{
				string text = "";
				IAGSObject iAGSObject = obj as IAGSObject;
				if (iAGSObject != null)
				{
					if (iAGSObject.Properties.ContainsKey("Full Extent"))
					{
						Extent extent = iAGSObject.Properties["Full Extent"] as Extent;
						text = extent.SpatialReference;
					}
					else if (iAGSObject.Properties.ContainsKey("Initial Extent"))
					{
						Extent extent2 = iAGSObject.Properties["Initial Extent"] as Extent;
						text = extent2.SpatialReference;
					}
					else if (iAGSObject.Properties.ContainsKey("WKT"))
					{
						text = iAGSObject.Properties["WKT"].ToString();
					}
					else if (iAGSObject.Properties.ContainsKey("WKID"))
					{
						text = iAGSObject.Properties["WKID"].ToString();
					}
					else if (iAGSObject.Properties.ContainsKey("WKID"))
					{
						text = iAGSObject.Properties["WKID"].ToString();
					}
				}
				SpatialReference spatialReference = obj as SpatialReference;
				if (spatialReference != null)
				{
					result = AGSSpatialReference.GetSpRefString(spatialReference);
				}
				else
				{
					Extent extent3 = obj as Extent;
					if (extent3 != null)
					{
						result = extent3.SpatialReference;
					}
					else
					{
						result = text;
					}
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		private static bool IsInteger(string theValue)
		{
			bool result;
			try
			{
				if (!string.IsNullOrEmpty(theValue))
				{
					int num;
					if (int.TryParse(theValue, out num))
					{
						result = true;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}
	}
}
