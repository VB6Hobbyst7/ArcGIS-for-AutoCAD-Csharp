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
	public class StandaloneTableDescription : MapTableDescription
	{
		private int idField;

		private string definitionExpressionField;

		private string sourceIDField;

		private bool useTimeField;

		private bool useTimeFieldSpecified;

		private bool timeDataCumulativeField;

		private bool timeDataCumulativeFieldSpecified;

		private double timeOffsetField;

		private bool timeOffsetFieldSpecified;

		private esriTimeUnits timeOffsetUnitsField;

		private bool timeOffsetUnitsFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string DefinitionExpression
		{
			get
			{
				return this.definitionExpressionField;
			}
			set
			{
				this.definitionExpressionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string SourceID
		{
			get
			{
				return this.sourceIDField;
			}
			set
			{
				this.sourceIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool UseTime
		{
			get
			{
				return this.useTimeField;
			}
			set
			{
				this.useTimeField = value;
			}
		}

		[XmlIgnore]
		public bool UseTimeSpecified
		{
			get
			{
				return this.useTimeFieldSpecified;
			}
			set
			{
				this.useTimeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool TimeDataCumulative
		{
			get
			{
				return this.timeDataCumulativeField;
			}
			set
			{
				this.timeDataCumulativeField = value;
			}
		}

		[XmlIgnore]
		public bool TimeDataCumulativeSpecified
		{
			get
			{
				return this.timeDataCumulativeFieldSpecified;
			}
			set
			{
				this.timeDataCumulativeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double TimeOffset
		{
			get
			{
				return this.timeOffsetField;
			}
			set
			{
				this.timeOffsetField = value;
			}
		}

		[XmlIgnore]
		public bool TimeOffsetSpecified
		{
			get
			{
				return this.timeOffsetFieldSpecified;
			}
			set
			{
				this.timeOffsetFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriTimeUnits TimeOffsetUnits
		{
			get
			{
				return this.timeOffsetUnitsField;
			}
			set
			{
				this.timeOffsetUnitsField = value;
			}
		}

		[XmlIgnore]
		public bool TimeOffsetUnitsSpecified
		{
			get
			{
				return this.timeOffsetUnitsFieldSpecified;
			}
			set
			{
				this.timeOffsetUnitsFieldSpecified = value;
			}
		}
	}
}
