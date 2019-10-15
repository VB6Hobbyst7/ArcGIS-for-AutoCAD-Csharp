using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public class MapServerHyperlink
	{
		private Geometry locationField;

		private string uRLField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Geometry Location
		{
			get
			{
				return this.locationField;
			}
			set
			{
				this.locationField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string URL
		{
			get
			{
				return this.uRLField;
			}
			set
			{
				this.uRLField = value;
			}
		}
	}
}
