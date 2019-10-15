using ArcGIS10Types;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AFA
{
    public abstract class AGSService : AGSObject, IAGSService, IAGSObject
	{
		public StringList SupportedImageTypes
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public AGSService()
		{
		}

		public AGSService(string serviceFullName, AGSConnection parent)
		{
			base.Parent = parent;
			base.FullName = serviceFullName;
			base.Name = System.IO.Path.GetFileName(base.FullName);
			base.Type = AGSType.AGSService;
			base.Properties = new Dictionary<string, object>();
			base.Properties.Add("Name", base.FullName);
			base.Properties.Add("Server", base.Parent.URL);
		}

		public bool Challenge()
		{
			string origRequestURI = base.Parent.URL + "?wsdl";
			string value = base.Parent.MakeRequest(origRequestURI);
			return !string.IsNullOrEmpty(value);
		}

		private static int GetWKIDFromWKT(string baseWKT)
		{
			string pattern = "(.+)(AUTHORITY\\[)(.+)(,)(?<wkid>\\d+)(.+)";
			Regex regex = new Regex(pattern);
			Match match = regex.Match(baseWKT);
			if (match.Success)
			{
				return int.Parse(regex.Match(baseWKT).Result("${wkid}"));
			}
			return 0;
		}

		private static bool SpRefCompareAuthority(string aWKT, string bWKT)
		{
			int wKIDFromWKT = AGSService.GetWKIDFromWKT(aWKT);
			if (wKIDFromWKT == 0)
			{
				return false;
			}
			int wKIDFromWKT2 = AGSService.GetWKIDFromWKT(bWKT);
			return wKIDFromWKT2 != 0 && AGSService.SpWKIDAreEquivalent(wKIDFromWKT, wKIDFromWKT2);
		}

		public static bool SpWKIDAreEquivalent(int a, int b)
		{
			return a == b || (a == 102100 && b == 3857) || (b == 102100 && a == 3857);
		}

		public static bool SpRefAreEquivalent(SpatialReference a, SpatialReference b)
		{
			if (a == null && b == null)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.WKIDSpecified && b.WKIDSpecified)
			{
				return AGSService.SpWKIDAreEquivalent(a.WKID, b.WKID);
			}
			return !string.IsNullOrEmpty(a.WKT) && !string.IsNullOrEmpty(b.WKT) && (a.WKT == b.WKT || AGSService.SpRefCompareAuthority(a.WKT, b.WKT));
		}

		public static EnvelopeN InitializeEnvelope(EnvelopeN baseEnv, Extent bbox)
		{
			EnvelopeN result;
			try
			{
				EnvelopeN envelopeN;
				if (baseEnv == null)
				{
					envelopeN = baseEnv;
				}
				else
				{
					envelopeN = new EnvelopeN();
				}
				envelopeN.XMin = bbox.XMin.Value;
				envelopeN.XMax = bbox.XMax.Value;
				envelopeN.YMin = bbox.YMin.Value;
				envelopeN.YMax = bbox.YMax.Value;
				envelopeN.SpatialReference = AGSSpatialReference.SpRefFromString(bbox.SpatialReference);
				result = envelopeN;
			}
			catch
			{
				result = baseEnv;
			}
			return result;
		}

		public static SpatialFilter BuildSpatialFilter(EnvelopeN env, string geomField)
		{
			SpatialFilter result;
			try
			{
				SpatialFilter spatialFilter = new SpatialFilter();
				spatialFilter.FilterGeometry = env;
				if (!string.IsNullOrEmpty(geomField))
				{
					spatialFilter.GeometryFieldName = geomField;
				}
				spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
				result = spatialFilter;
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}
}
