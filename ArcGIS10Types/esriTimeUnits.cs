using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public enum esriTimeUnits
	{
		esriTimeUnitsUnknown,
		esriTimeUnitsMilliseconds,
		esriTimeUnitsSeconds,
		esriTimeUnitsMinutes,
		esriTimeUnitsHours,
		esriTimeUnitsDays,
		esriTimeUnitsWeeks,
		esriTimeUnitsMonths,
		esriTimeUnitsYears,
		esriTimeUnitsDecades,
		esriTimeUnitsCenturies
	}
}
