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
	public class AttachmentData
	{
		private byte[] dataField;

		private AttachmentInfo attachmentInfoField;

		private string uRLField;

		private esriTransportType transportTypeField;

		[XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "base64Binary")]
		public byte[] Data
		{
			get
			{
				return this.dataField;
			}
			set
			{
				this.dataField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public AttachmentInfo AttachmentInfo
		{
			get
			{
				return this.attachmentInfoField;
			}
			set
			{
				this.attachmentInfoField = value;
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
	}
}
