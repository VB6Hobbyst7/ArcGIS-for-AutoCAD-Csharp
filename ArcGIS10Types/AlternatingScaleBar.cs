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
	public class AlternatingScaleBar : ScaleBar
	{
		private FillSymbol fillSymbol1Field;

		private FillSymbol fillSymbol2Field;

		private LineSymbol divisionMarkSymbolField;

		private LineSymbol subdivisionMarkSymbolField;

		private double divisionMarkHeightField;

		private bool divisionMarkHeightFieldSpecified;

		private double subdivisionMarkHeightField;

		private bool subdivisionMarkHeightFieldSpecified;

		private esriVertPosEnum markPositionField;

		private bool markPositionFieldSpecified;

		private esriScaleBarFrequency markFrequencyField;

		private bool markFrequencyFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public FillSymbol FillSymbol1
		{
			get
			{
				return this.fillSymbol1Field;
			}
			set
			{
				this.fillSymbol1Field = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public FillSymbol FillSymbol2
		{
			get
			{
				return this.fillSymbol2Field;
			}
			set
			{
				this.fillSymbol2Field = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public LineSymbol DivisionMarkSymbol
		{
			get
			{
				return this.divisionMarkSymbolField;
			}
			set
			{
				this.divisionMarkSymbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public LineSymbol SubdivisionMarkSymbol
		{
			get
			{
				return this.subdivisionMarkSymbolField;
			}
			set
			{
				this.subdivisionMarkSymbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double DivisionMarkHeight
		{
			get
			{
				return this.divisionMarkHeightField;
			}
			set
			{
				this.divisionMarkHeightField = value;
			}
		}

		[XmlIgnore]
		public bool DivisionMarkHeightSpecified
		{
			get
			{
				return this.divisionMarkHeightFieldSpecified;
			}
			set
			{
				this.divisionMarkHeightFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double SubdivisionMarkHeight
		{
			get
			{
				return this.subdivisionMarkHeightField;
			}
			set
			{
				this.subdivisionMarkHeightField = value;
			}
		}

		[XmlIgnore]
		public bool SubdivisionMarkHeightSpecified
		{
			get
			{
				return this.subdivisionMarkHeightFieldSpecified;
			}
			set
			{
				this.subdivisionMarkHeightFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriVertPosEnum MarkPosition
		{
			get
			{
				return this.markPositionField;
			}
			set
			{
				this.markPositionField = value;
			}
		}

		[XmlIgnore]
		public bool MarkPositionSpecified
		{
			get
			{
				return this.markPositionFieldSpecified;
			}
			set
			{
				this.markPositionFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriScaleBarFrequency MarkFrequency
		{
			get
			{
				return this.markFrequencyField;
			}
			set
			{
				this.markFrequencyField = value;
			}
		}

		[XmlIgnore]
		public bool MarkFrequencySpecified
		{
			get
			{
				return this.markFrequencyFieldSpecified;
			}
			set
			{
				this.markFrequencyFieldSpecified = value;
			}
		}
	}
}
