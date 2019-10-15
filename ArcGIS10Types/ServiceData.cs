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
	public class ServiceData
	{
		private ServiceDataOptions serviceDataOptionsField;

		private byte[] embeddedDataField;

		private bool notModifiedField;

		private object objectField;

		private string responseEtagField;

		private string uRIField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public ServiceDataOptions ServiceDataOptions
		{
			get
			{
				return this.serviceDataOptionsField;
			}
			set
			{
				this.serviceDataOptionsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "base64Binary")]
		public byte[] EmbeddedData
		{
			get
			{
				return this.embeddedDataField;
			}
			set
			{
				this.embeddedDataField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool NotModified
		{
			get
			{
				return this.notModifiedField;
			}
			set
			{
				this.notModifiedField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public object Object
		{
			get
			{
				return this.objectField;
			}
			set
			{
				this.objectField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ResponseEtag
		{
			get
			{
				return this.responseEtagField;
			}
			set
			{
				this.responseEtagField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string URI
		{
			get
			{
				return this.uRIField;
			}
			set
			{
				this.uRIField = value;
			}
		}
	}
}
