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
	public class LayerDescription : MapTableDescription
	{
		private int layerIDField;

		private bool visibleField;

		private bool showLabelsField;

		private bool scaleSymbolsField;

		private int[] selectionFeaturesField;

		private Color selectionColorField;

		private Symbol selectionSymbolField;

		private bool setSelectionSymbolField;

		private double selectionBufferDistanceField;

		private bool showSelectionBufferField;

		private string definitionExpressionField;

		private string sourceIDField;

		private FillSymbol selectionBufferSymbolField;

		private LayerResultOptions layerResultOptionsField;

		private bool useTimeField;

		private bool useTimeFieldSpecified;

		private bool timeDataCumulativeField;

		private bool timeDataCumulativeFieldSpecified;

		private double timeOffsetField;

		private bool timeOffsetFieldSpecified;

		private esriTimeUnits timeOffsetUnitsField;

		private bool timeOffsetUnitsFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int LayerID
		{
			get
			{
				return this.layerIDField;
			}
			set
			{
				this.layerIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Visible
		{
			get
			{
				return this.visibleField;
			}
			set
			{
				this.visibleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool ShowLabels
		{
			get
			{
				return this.showLabelsField;
			}
			set
			{
				this.showLabelsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool ScaleSymbols
		{
			get
			{
				return this.scaleSymbolsField;
			}
			set
			{
				this.scaleSymbolsField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] SelectionFeatures
		{
			get
			{
				return this.selectionFeaturesField;
			}
			set
			{
				this.selectionFeaturesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Color SelectionColor
		{
			get
			{
				return this.selectionColorField;
			}
			set
			{
				this.selectionColorField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Symbol SelectionSymbol
		{
			get
			{
				return this.selectionSymbolField;
			}
			set
			{
				this.selectionSymbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool SetSelectionSymbol
		{
			get
			{
				return this.setSelectionSymbolField;
			}
			set
			{
				this.setSelectionSymbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double SelectionBufferDistance
		{
			get
			{
				return this.selectionBufferDistanceField;
			}
			set
			{
				this.selectionBufferDistanceField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool ShowSelectionBuffer
		{
			get
			{
				return this.showSelectionBufferField;
			}
			set
			{
				this.showSelectionBufferField = value;
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
		public FillSymbol SelectionBufferSymbol
		{
			get
			{
				return this.selectionBufferSymbolField;
			}
			set
			{
				this.selectionBufferSymbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public LayerResultOptions LayerResultOptions
		{
			get
			{
				return this.layerResultOptionsField;
			}
			set
			{
				this.layerResultOptionsField = value;
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
