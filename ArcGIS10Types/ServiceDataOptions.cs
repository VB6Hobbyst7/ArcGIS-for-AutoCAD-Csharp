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
	public class ServiceDataOptions
	{
		private string etagField;

		private string formatField;

		private PropertySet propertiesField;

		private esriTransportType transportTypeField;

		private bool transportTypeFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Etag
		{
			get
			{
				return this.etagField;
			}
			set
			{
				this.etagField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Format
		{
			get
			{
				return this.formatField;
			}
			set
			{
				this.formatField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public PropertySet Properties
		{
			get
			{
				return this.propertiesField;
			}
			set
			{
				this.propertiesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriTransportType TransportType
		{
			get
			{
				return this.transportTypeField;
			}
			set
			{
				this.transportTypeField = value;
			}
		}

		[XmlIgnore]
		public bool TransportTypeSpecified
		{
			get
			{
				return this.transportTypeFieldSpecified;
			}
			set
			{
				this.transportTypeFieldSpecified = value;
			}
		}
	}
}
