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
	public class ImageDescription
	{
		private ImageType imageTypeField;

		private ImageDisplay imageDisplayField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public ImageType ImageType
		{
			get
			{
				return this.imageTypeField;
			}
			set
			{
				this.imageTypeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public ImageDisplay ImageDisplay
		{
			get
			{
				return this.imageDisplayField;
			}
			set
			{
				this.imageDisplayField = value;
			}
		}
	}
}
