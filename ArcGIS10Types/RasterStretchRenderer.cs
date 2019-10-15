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
	public class RasterStretchRenderer : RasterRenderer
	{
		private string colorSchemaField;

		private int layerIndex1Field;

		private bool layerIndex1FieldSpecified;

		private string stretchTypeField;

		private double standardDeviationsField;

		private bool standardDeviationsFieldSpecified;

		private bool isInvertField;

		private bool isInvertFieldSpecified;

		private double blackValueField;

		private bool blackValueFieldSpecified;

		private ColorRamp colorRampField;

		private Color bkColorField;

		private LegendGroup legendGroupField;

		private bool displayBkValueField;

		private bool displayBkValueFieldSpecified;

		private bool initCustomMinMaxField;

		private bool initCustomMinMaxFieldSpecified;

		private bool useCustomMinMaxField;

		private bool useCustomMinMaxFieldSpecified;

		private double customMinField;

		private bool customMinFieldSpecified;

		private double customMaxField;

		private bool customMaxFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ColorSchema
		{
			get
			{
				return this.colorSchemaField;
			}
			set
			{
				this.colorSchemaField = value;
			}
		}

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
		public double BlackValue
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

		[XmlIgnore]
		public bool BlackValueSpecified
		{
			get
			{
				return this.blackValueFieldSpecified;
			}
			set
			{
				this.blackValueFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public ColorRamp ColorRamp
		{
			get
			{
				return this.colorRampField;
			}
			set
			{
				this.colorRampField = value;
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public LegendGroup LegendGroup
		{
			get
			{
				return this.legendGroupField;
			}
			set
			{
				this.legendGroupField = value;
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool InitCustomMinMax
		{
			get
			{
				return this.initCustomMinMaxField;
			}
			set
			{
				this.initCustomMinMaxField = value;
			}
		}

		[XmlIgnore]
		public bool InitCustomMinMaxSpecified
		{
			get
			{
				return this.initCustomMinMaxFieldSpecified;
			}
			set
			{
				this.initCustomMinMaxFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool UseCustomMinMax
		{
			get
			{
				return this.useCustomMinMaxField;
			}
			set
			{
				this.useCustomMinMaxField = value;
			}
		}

		[XmlIgnore]
		public bool UseCustomMinMaxSpecified
		{
			get
			{
				return this.useCustomMinMaxFieldSpecified;
			}
			set
			{
				this.useCustomMinMaxFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double CustomMin
		{
			get
			{
				return this.customMinField;
			}
			set
			{
				this.customMinField = value;
			}
		}

		[XmlIgnore]
		public bool CustomMinSpecified
		{
			get
			{
				return this.customMinFieldSpecified;
			}
			set
			{
				this.customMinFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double CustomMax
		{
			get
			{
				return this.customMaxField;
			}
			set
			{
				this.customMaxField = value;
			}
		}

		[XmlIgnore]
		public bool CustomMaxSpecified
		{
			get
			{
				return this.customMaxFieldSpecified;
			}
			set
			{
				this.customMaxFieldSpecified = value;
			}
		}
	}
}
