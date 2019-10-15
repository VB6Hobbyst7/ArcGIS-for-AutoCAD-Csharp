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
	public class RasterUniqueValueRenderer : RasterRenderer
	{
		private string valueFieldField;

		private string classFieldField;

		private string colorSchemaField;

		private bool useDefaultSymbolField;

		private bool useDefaultSymbolFieldSpecified;

		private Symbol defaultSymbolField;

		private string defaultLabelField;

		private int legendGroupsCountField;

		private bool legendGroupsCountFieldSpecified;

		private LegendGroup[] legendGroupsField;

		private int classValuesCountField;

		private bool classValuesCountFieldSpecified;

		private int[] classesInLegendField;

		private int[] classesInLegendSizeField;

		private object[] uniqueValueVariantsField;

		private bool globalField;

		private bool globalFieldSpecified;

		private RasterUniqueValues uniqueValuesField;

		private ColorRamp colorRampField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ValueField
		{
			get
			{
				return this.valueFieldField;
			}
			set
			{
				this.valueFieldField = value;
			}
		}

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
		public bool UseDefaultSymbol
		{
			get
			{
				return this.useDefaultSymbolField;
			}
			set
			{
				this.useDefaultSymbolField = value;
			}
		}

		[XmlIgnore]
		public bool UseDefaultSymbolSpecified
		{
			get
			{
				return this.useDefaultSymbolFieldSpecified;
			}
			set
			{
				this.useDefaultSymbolFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Symbol DefaultSymbol
		{
			get
			{
				return this.defaultSymbolField;
			}
			set
			{
				this.defaultSymbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string DefaultLabel
		{
			get
			{
				return this.defaultLabelField;
			}
			set
			{
				this.defaultLabelField = value;
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
		public int ClassValuesCount
		{
			get
			{
				return this.classValuesCountField;
			}
			set
			{
				this.classValuesCountField = value;
			}
		}

		[XmlIgnore]
		public bool ClassValuesCountSpecified
		{
			get
			{
				return this.classValuesCountFieldSpecified;
			}
			set
			{
				this.classValuesCountFieldSpecified = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] ClassesInLegend
		{
			get
			{
				return this.classesInLegendField;
			}
			set
			{
				this.classesInLegendField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] ClassesInLegendSize
		{
			get
			{
				return this.classesInLegendSizeField;
			}
			set
			{
				this.classesInLegendSizeField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Value", Form = XmlSchemaForm.Unqualified)]
		public object[] UniqueValueVariants
		{
			get
			{
				return this.uniqueValueVariantsField;
			}
			set
			{
				this.uniqueValueVariantsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Global
		{
			get
			{
				return this.globalField;
			}
			set
			{
				this.globalField = value;
			}
		}

		[XmlIgnore]
		public bool GlobalSpecified
		{
			get
			{
				return this.globalFieldSpecified;
			}
			set
			{
				this.globalFieldSpecified = value;
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
	}
}
