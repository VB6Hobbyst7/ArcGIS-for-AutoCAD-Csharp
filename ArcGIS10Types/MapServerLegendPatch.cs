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
	public class MapServerLegendPatch
	{
		private double widthField;

		private double heightField;

		private double imageDPIField;

		private LinePatch linePatchField;

		private AreaPatch areaPatchField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Width
		{
			get
			{
				return this.widthField;
			}
			set
			{
				this.widthField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Height
		{
			get
			{
				return this.heightField;
			}
			set
			{
				this.heightField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ImageDPI
		{
			get
			{
				return this.imageDPIField;
			}
			set
			{
				this.imageDPIField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public LinePatch LinePatch
		{
			get
			{
				return this.linePatchField;
			}
			set
			{
				this.linePatchField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public AreaPatch AreaPatch
		{
			get
			{
				return this.areaPatchField;
			}
			set
			{
				this.areaPatchField = value;
			}
		}
	}
}
