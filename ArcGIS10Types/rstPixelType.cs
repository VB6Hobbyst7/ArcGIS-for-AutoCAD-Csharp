using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public enum rstPixelType
	{
		U1,
		U2,
		U4,
		U8,
		S8,
		U16,
		S16,
		U32,
		S32,
		F32,
		F64,
		C64,
		C128,
		UNKNOWN
	}
}
