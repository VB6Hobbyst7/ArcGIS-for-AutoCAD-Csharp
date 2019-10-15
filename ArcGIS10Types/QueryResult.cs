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
	public class QueryResult
	{
		private byte[] mimeDataField;

		private string uRLField;

		private object objectField;

		[XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "base64Binary")]
		public byte[] MimeData
		{
			get
			{
				return this.mimeDataField;
			}
			set
			{
				this.mimeDataField = value;
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
	}
}
