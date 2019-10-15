using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public enum esriSplitPolicyType
	{
		esriSPTGeometryRatio,
		esriSPTDuplicate,
		esriSPTDefaultValue
	}
}
