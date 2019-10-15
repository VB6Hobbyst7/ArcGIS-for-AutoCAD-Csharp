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
	public class CmykColor : Color
	{
		private byte cyanField;

		private byte magentaField;

		private byte yellowField;

		private byte blackField;

		private bool overprintField;

		private bool isSpotField;

		private string spotDescriptionField;

		private short spotPercentField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public byte Cyan
		{
			get
			{
				return this.cyanField;
			}
			set
			{
				this.cyanField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public byte Magenta
		{
			get
			{
				return this.magentaField;
			}
			set
			{
				this.magentaField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public byte Yellow
		{
			get
			{
				return this.yellowField;
			}
			set
			{
				this.yellowField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public byte Black
		{
			get
			{
				return this.blackField;
			}
			set
			{
				this.blackField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Overprint
		{
			get
			{
				return this.overprintField;
			}
			set
			{
				this.overprintField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IsSpot
		{
			get
			{
				return this.isSpotField;
			}
			set
			{
				this.isSpotField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string SpotDescription
		{
			get
			{
				return this.spotDescriptionField;
			}
			set
			{
				this.spotDescriptionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short SpotPercent
		{
			get
			{
				return this.spotPercentField;
			}
			set
			{
				this.spotPercentField = value;
			}
		}
	}
}
