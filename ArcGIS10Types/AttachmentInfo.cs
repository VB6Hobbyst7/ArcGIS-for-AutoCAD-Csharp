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
	public class AttachmentInfo
	{
		private int attachmentIDField;

		private int parentIDField;

		private string nameField;

		private string contentTypeField;

		private int sizeField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int AttachmentID
		{
			get
			{
				return this.attachmentIDField;
			}
			set
			{
				this.attachmentIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ParentID
		{
			get
			{
				return this.parentIDField;
			}
			set
			{
				this.parentIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ContentType
		{
			get
			{
				return this.contentTypeField;
			}
			set
			{
				this.contentTypeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int Size
		{
			get
			{
				return this.sizeField;
			}
			set
			{
				this.sizeField = value;
			}
		}
	}
}
