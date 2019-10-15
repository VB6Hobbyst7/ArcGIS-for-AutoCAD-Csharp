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
	public class RasterClassifyRenderer : RasterRenderer
	{
		private string classFieldField;

		private string normFieldField;

		private bool classificationComponentField;

		private bool classificationComponentFieldSpecified;

		private string guidField;

		private string colorSchemaField;

		private int legendGroupsCountField;

		private bool legendGroupsCountFieldSpecified;

		private LegendGroup[] legendGroupsField;

		private int breakSizeField;

		private bool breakSizeFieldSpecified;

		private double[] arrayOfBreakField;

		private bool ascendingField;

		private bool ascendingFieldSpecified;

		private NumericFormat numberFormatField;

		private bool showClassGapsField;

		private bool showClassGapsFieldSpecified;

		private double deviationIntervalField;

		private bool deviationIntervalFieldSpecified;

		private object exlusionValuesField;

		private object exclusionRangesField;

		private bool exclusionShowClassField;

		private bool exclusionShowClassFieldSpecified;

		private LegendClass exclusionLegendClassField;

		private RasterUniqueValues uniqueValuesField;

		private bool useHillShaderField;

		private bool useHillShaderFieldSpecified;

		private double zScaleField;

		private bool zScaleFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ClassField
		{
			get
			{
				return this.classFieldField;
			}
			set
			{
				this.classFieldField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string NormField
		{
			get
			{
				return this.normFieldField;
			}
			set
			{
				this.normFieldField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool ClassificationComponent
		{
			get
			{
				return this.classificationComponentField;
			}
			set
			{
				this.classificationComponentField = value;
			}
		}

		[XmlIgnore]
		public bool ClassificationComponentSpecified
		{
			get
			{
				return this.classificationComponentFieldSpecified;
			}
			set
			{
				this.classificationComponentFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Guid
		{
			get
			{
				return this.guidField;
			}
			set
			{
				this.guidField = value;
			}
		}

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
		public int LegendGroupsCount
		{
			get
			{
				return this.legendGroupsCountField;
			}
			set
			{
				this.legendGroupsCountField = value;
			}
		}

		[XmlIgnore]
		public bool LegendGroupsCountSpecified
		{
			get
			{
				return this.legendGroupsCountFieldSpecified;
			}
			set
			{
				this.legendGroupsCountFieldSpecified = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public LegendGroup[] LegendGroups
		{
			get
			{
				return this.legendGroupsField;
			}
			set
			{
				this.legendGroupsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int BreakSize
		{
			get
			{
				return this.breakSizeField;
			}
			set
			{
				this.breakSizeField = value;
			}
		}

		[XmlIgnore]
		public bool BreakSizeSpecified
		{
			get
			{
				return this.breakSizeFieldSpecified;
			}
			set
			{
				this.breakSizeFieldSpecified = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] ArrayOfBreak
		{
			get
			{
				return this.arrayOfBreakField;
			}
			set
			{
				this.arrayOfBreakField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Ascending
		{
			get
			{
				return this.ascendingField;
			}
			set
			{
				this.ascendingField = value;
			}
		}

		[XmlIgnore]
		public bool AscendingSpecified
		{
			get
			{
				return this.ascendingFieldSpecified;
			}
			set
			{
				this.ascendingFieldSpecified = value;
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
		public bool ShowClassGaps
		{
			get
			{
				return this.showClassGapsField;
			}
			set
			{
				this.showClassGapsField = value;
			}
		}

		[XmlIgnore]
		public bool ShowClassGapsSpecified
		{
			get
			{
				return this.showClassGapsFieldSpecified;
			}
			set
			{
				this.showClassGapsFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double DeviationInterval
		{
			get
			{
				return this.deviationIntervalField;
			}
			set
			{
				this.deviationIntervalField = value;
			}
		}

		[XmlIgnore]
		public bool DeviationIntervalSpecified
		{
			get
			{
				return this.deviationIntervalFieldSpecified;
			}
			set
			{
				this.deviationIntervalFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public object ExlusionValues
		{
			get
			{
				return this.exlusionValuesField;
			}
			set
			{
				this.exlusionValuesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public object ExclusionRanges
		{
			get
			{
				return this.exclusionRangesField;
			}
			set
			{
				this.exclusionRangesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool ExclusionShowClass
		{
			get
			{
				return this.exclusionShowClassField;
			}
			set
			{
				this.exclusionShowClassField = value;
			}
		}

		[XmlIgnore]
		public bool ExclusionShowClassSpecified
		{
			get
			{
				return this.exclusionShowClassFieldSpecified;
			}
			set
			{
				this.exclusionShowClassFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public LegendClass ExclusionLegendClass
		{
			get
			{
				return this.exclusionLegendClassField;
			}
			set
			{
				this.exclusionLegendClassField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public RasterUniqueValues UniqueValues
		{
			get
			{
				return this.uniqueValuesField;
			}
			set
			{
				this.uniqueValuesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool UseHillShader
		{
			get
			{
				return this.useHillShaderField;
			}
			set
			{
				this.useHillShaderField = value;
			}
		}

		[XmlIgnore]
		public bool UseHillShaderSpecified
		{
			get
			{
				return this.useHillShaderFieldSpecified;
			}
			set
			{
				this.useHillShaderFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ZScale
		{
			get
			{
				return this.zScaleField;
			}
			set
			{
				this.zScaleField = value;
			}
		}

		[XmlIgnore]
		public bool ZScaleSpecified
		{
			get
			{
				return this.zScaleFieldSpecified;
			}
			set
			{
				this.zScaleFieldSpecified = value;
			}
		}
	}
}
