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
	public class ImageType
	{
		private esriImageFormat imageFormatField;

		private esriImageReturnType imageReturnTypeField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriImageFormat ImageFormat
		{
			get
			{
				return this.imageFormatField;
			}
			set
			{
				this.imageFormatField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriImageReturnType ImageReturnType
		{
			get
			{
				return this.imageReturnTypeField;
			}
			set
			{
				this.imageReturnTypeField = value;
			}
		}
	}
}
