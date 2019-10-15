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
	public class LabelClassDescription
	{
		private LabelPlacementDescription labelPlacementDescriptionField;

		private string labelExpressionField;

		private SimpleTextSymbol symbolField;

		private bool useCodedValueField;

		private bool useCodedValueFieldSpecified;

		private double maximumScaleField;

		private bool maximumScaleFieldSpecified;

		private double minimumScaleField;

		private bool minimumScaleFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public LabelPlacementDescription LabelPlacementDescription
		{
			get
			{
				return this.labelPlacementDescriptionField;
			}
			set
			{
				this.labelPlacementDescriptionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string LabelExpression
		{
			get
			{
				return this.labelExpressionField;
			}
			set
			{
				this.labelExpressionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public SimpleTextSymbol Symbol
		{
			get
			{
				return this.symbolField;
			}
			set
			{
				this.symbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool UseCodedValue
		{
			get
			{
				return this.useCodedValueField;
			}
			set
			{
				this.useCodedValueField = value;
			}
		}

		[XmlIgnore]
		public bool UseCodedValueSpecified
		{
			get
			{
				return this.useCodedValueFieldSpecified;
			}
			set
			{
				this.useCodedValueFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MaximumScale
		{
			get
			{
				return this.maximumScaleField;
			}
			set
			{
				this.maximumScaleField = value;
			}
		}

		[XmlIgnore]
		public bool MaximumScaleSpecified
		{
			get
			{
				return this.maximumScaleFieldSpecified;
			}
			set
			{
				this.maximumScaleFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MinimumScale
		{
			get
			{
				return this.minimumScaleField;
			}
			set
			{
				this.minimumScaleField = value;
			}
		}

		[XmlIgnore]
		public bool MinimumScaleSpecified
		{
			get
			{
				return this.minimumScaleFieldSpecified;
			}
			set
			{
				this.minimumScaleFieldSpecified = value;
			}
		}
	}
}
