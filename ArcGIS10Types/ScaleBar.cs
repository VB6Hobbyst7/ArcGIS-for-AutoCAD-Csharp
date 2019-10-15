using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(AlternatingScaleBar)), XmlInclude(typeof(SingleDivisionScaleBar)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public abstract class ScaleBar
	{
		private double barHeightField;

		private bool barHeightFieldSpecified;

		private double divisionField;

		private bool divisionFieldSpecified;

		private short divisionsField;

		private bool divisionsFieldSpecified;

		private short divisionsBeforeZeroField;

		private bool divisionsBeforeZeroFieldSpecified;

		private short subdivisionsField;

		private bool subdivisionsFieldSpecified;

		private esriUnits unitsField;

		private bool unitsFieldSpecified;

		private string unitLabelField;

		private esriScaleBarPos unitLabelPositionField;

		private bool unitLabelPositionFieldSpecified;

		private double unitLabelGapField;

		private bool unitLabelGapFieldSpecified;

		private TextSymbol unitLabelSymbolField;

		private esriScaleBarFrequency labelFrequencyField;

		private bool labelFrequencyFieldSpecified;

		private esriVertPosEnum labelPositionField;

		private bool labelPositionFieldSpecified;

		private double labelGapField;

		private bool labelGapFieldSpecified;

		private TextSymbol labelSymbolField;

		private NumericFormat numberFormatField;

		private esriScaleBarResizeHint resizeHintField;

		private bool resizeHintFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double BarHeight
		{
			get
			{
				return this.barHeightField;
			}
			set
			{
				this.barHeightField = value;
			}
		}

		[XmlIgnore]
		public bool BarHeightSpecified
		{
			get
			{
				return this.barHeightFieldSpecified;
			}
			set
			{
				this.barHeightFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Division
		{
			get
			{
				return this.divisionField;
			}
			set
			{
				this.divisionField = value;
			}
		}

		[XmlIgnore]
		public bool DivisionSpecified
		{
			get
			{
				return this.divisionFieldSpecified;
			}
			set
			{
				this.divisionFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short Divisions
		{
			get
			{
				return this.divisionsField;
			}
			set
			{
				this.divisionsField = value;
			}
		}

		[XmlIgnore]
		public bool DivisionsSpecified
		{
			get
			{
				return this.divisionsFieldSpecified;
			}
			set
			{
				this.divisionsFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short DivisionsBeforeZero
		{
			get
			{
				return this.divisionsBeforeZeroField;
			}
			set
			{
				this.divisionsBeforeZeroField = value;
			}
		}

		[XmlIgnore]
		public bool DivisionsBeforeZeroSpecified
		{
			get
			{
				return this.divisionsBeforeZeroFieldSpecified;
			}
			set
			{
				this.divisionsBeforeZeroFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public short Subdivisions
		{
			get
			{
				return this.subdivisionsField;
			}
			set
			{
				this.subdivisionsField = value;
			}
		}

		[XmlIgnore]
		public bool SubdivisionsSpecified
		{
			get
			{
				return this.subdivisionsFieldSpecified;
			}
			set
			{
				this.subdivisionsFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriUnits Units
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

		[XmlIgnore]
		public bool UnitsSpecified
		{
			get
			{
				return this.unitsFieldSpecified;
			}
			set
			{
				this.unitsFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string UnitLabel
		{
			get
			{
				return this.unitLabelField;
			}
			set
			{
				this.unitLabelField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriScaleBarPos UnitLabelPosition
		{
			get
			{
				return this.unitLabelPositionField;
			}
			set
			{
				this.unitLabelPositionField = value;
			}
		}

		[XmlIgnore]
		public bool UnitLabelPositionSpecified
		{
			get
			{
				return this.unitLabelPositionFieldSpecified;
			}
			set
			{
				this.unitLabelPositionFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double UnitLabelGap
		{
			get
			{
				return this.unitLabelGapField;
			}
			set
			{
				this.unitLabelGapField = value;
			}
		}

		[XmlIgnore]
		public bool UnitLabelGapSpecified
		{
			get
			{
				return this.unitLabelGapFieldSpecified;
			}
			set
			{
				this.unitLabelGapFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TextSymbol UnitLabelSymbol
		{
			get
			{
				return this.unitLabelSymbolField;
			}
			set
			{
				this.unitLabelSymbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriScaleBarFrequency LabelFrequency
		{
			get
			{
				return this.labelFrequencyField;
			}
			set
			{
				this.labelFrequencyField = value;
			}
		}

		[XmlIgnore]
		public bool LabelFrequencySpecified
		{
			get
			{
				return this.labelFrequencyFieldSpecified;
			}
			set
			{
				this.labelFrequencyFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriVertPosEnum LabelPosition
		{
			get
			{
				return this.labelPositionField;
			}
			set
			{
				this.labelPositionField = value;
			}
		}

		[XmlIgnore]
		public bool LabelPositionSpecified
		{
			get
			{
				return this.labelPositionFieldSpecified;
			}
			set
			{
				this.labelPositionFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double LabelGap
		{
			get
			{
				return this.labelGapField;
			}
			set
			{
				this.labelGapField = value;
			}
		}

		[XmlIgnore]
		public bool LabelGapSpecified
		{
			get
			{
				return this.labelGapFieldSpecified;
			}
			set
			{
				this.labelGapFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TextSymbol LabelSymbol
		{
			get
			{
				return this.labelSymbolField;
			}
			set
			{
				this.labelSymbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public NumericFormat NumberFormat
		{
			get
			{
				return this.numberFormatField;
			}
			set
			{
				this.numberFormatField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriScaleBarResizeHint ResizeHint
		{
			get
			{
				return this.resizeHintField;
			}
			set
			{
				this.resizeHintField = value;
			}
		}

		[XmlIgnore]
		public bool ResizeHintSpecified
		{
			get
			{
				return this.resizeHintFieldSpecified;
			}
			set
			{
				this.resizeHintFieldSpecified = value;
			}
		}
	}
}
