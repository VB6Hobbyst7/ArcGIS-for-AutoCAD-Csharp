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
	public class CenterAndSize : MapArea
	{
		private Point centerField;

		private double heightField;

		private double widthField;

		private string unitsField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Point Center
		{
			get
			{
				return this.centerField;
			}
			set
			{
				this.centerField = value;
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
		public string Units
		{
			get
			{
				return this.unitsField;
			}
			set
			{
				this.unitsField = value;
			}
		}
	}
}
