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
	public class PresetColorRamp : ColorRamp
	{
		private int numColorsField;

		private bool numColorsFieldSpecified;

		private int presetSizeField;

		private bool presetSizeFieldSpecified;

		private Color[] colorsField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int NumColors
		{
			get
			{
				return this.numColorsField;
			}
			set
			{
				this.numColorsField = value;
			}
		}

		[XmlIgnore]
		public bool NumColorsSpecified
		{
			get
			{
				return this.numColorsFieldSpecified;
			}
			set
			{
				this.numColorsFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int PresetSize
		{
			get
			{
				return this.presetSizeField;
			}
			set
			{
				this.presetSizeField = value;
			}
		}

		[XmlIgnore]
		public bool PresetSizeSpecified
		{
			get
			{
				return this.presetSizeFieldSpecified;
			}
			set
			{
				this.presetSizeFieldSpecified = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Color[] Colors
		{
			get
			{
				return this.colorsField;
			}
			set
			{
				this.colorsField = value;
			}
		}
	}
}
