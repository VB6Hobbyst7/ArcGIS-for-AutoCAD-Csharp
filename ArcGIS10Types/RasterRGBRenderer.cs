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
	public class RasterRGBRenderer : RasterRenderer
	{
		private int layerIndex1Field;

		private bool layerIndex1FieldSpecified;

		private int layerIndex2Field;

		private bool layerIndex2FieldSpecified;

		private int layerIndex3Field;

		private bool layerIndex3FieldSpecified;

		private byte useRGBBandField;

		private bool useRGBBandFieldSpecified;

		private string stretchTypeField;

		private double standardDeviationsField;

		private bool standardDeviationsFieldSpecified;

		private bool isInvertField;

		private bool isInvertFieldSpecified;

		private bool displayBkValueField;

		private bool displayBkValueFieldSpecified;

		private double[] blackValueField;

		private bool isLegendExpandField;

		private bool isLegendExpandFieldSpecified;

		private Color bkColorField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int LayerIndex1
		{
			get
			{
				return this.layerIndex1Field;
			}
			set
			{
				this.layerIndex1Field = value;
			}
		}

		[XmlIgnore]
		public bool LayerIndex1Specified
		{
			get
			{
				return this.layerIndex1FieldSpecified;
			}
			set
			{
				this.layerIndex1FieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int LayerIndex2
		{
			get
			{
				return this.layerIndex2Field;
			}
			set
			{
				this.layerIndex2Field = value;
			}
		}

		[XmlIgnore]
		public bool LayerIndex2Specified
		{
			get
			{
				return this.layerIndex2FieldSpecified;
			}
			set
			{
				this.layerIndex2FieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int LayerIndex3
		{
			get
			{
				return this.layerIndex3Field;
			}
			set
			{
				this.layerIndex3Field = value;
			}
		}

		[XmlIgnore]
		public bool LayerIndex3Specified
		{
			get
			{
				return this.layerIndex3FieldSpecified;
			}
			set
			{
				this.layerIndex3FieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public byte UseRGBBand
		{
			get
			{
				return this.useRGBBandField;
			}
			set
			{
				this.useRGBBandField = value;
			}
		}

		[XmlIgnore]
		public bool UseRGBBandSpecified
		{
			get
			{
				return this.useRGBBandFieldSpecified;
			}
			set
			{
				this.useRGBBandFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string StretchType
		{
			get
			{
				return this.stretchTypeField;
			}
			set
			{
				this.stretchTypeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double StandardDeviations
		{
			get
			{
				return this.standardDeviationsField;
			}
			set
			{
				this.standardDeviationsField = value;
			}
		}

		[XmlIgnore]
		public bool StandardDeviationsSpecified
		{
			get
			{
				return this.standardDeviationsFieldSpecified;
			}
			set
			{
				this.standardDeviationsFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IsInvert
		{
			get
			{
				return this.isInvertField;
			}
			set
			{
				this.isInvertField = value;
			}
		}

		[XmlIgnore]
		public bool IsInvertSpecified
		{
			get
			{
				return this.isInvertFieldSpecified;
			}
			set
			{
				this.isInvertFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool DisplayBkValue
		{
			get
			{
				return this.displayBkValueField;
			}
			set
			{
				this.displayBkValueField = value;
			}
		}

		[XmlIgnore]
		public bool DisplayBkValueSpecified
		{
			get
			{
				return this.displayBkValueFieldSpecified;
			}
			set
			{
				this.displayBkValueFieldSpecified = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] BlackValue
		{
			get
			{
				return this.blackValueField;
			}
			set
			{
				this.blackValueField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IsLegendExpand
		{
			get
			{
				return this.isLegendExpandField;
			}
			set
			{
				this.isLegendExpandField = value;
			}
		}

		[XmlIgnore]
		public bool IsLegendExpandSpecified
		{
			get
			{
				return this.isLegendExpandFieldSpecified;
			}
			set
			{
				this.isLegendExpandFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Color BkColor
		{
			get
			{
				return this.bkColorField;
			}
			set
			{
				this.bkColorField = value;
			}
		}
	}
}
