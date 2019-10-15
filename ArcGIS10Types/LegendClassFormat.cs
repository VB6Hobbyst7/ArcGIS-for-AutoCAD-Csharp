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
	public class LegendClassFormat
	{
		private Symbol labelSymbolField;

		private Symbol descriptionSymbolField;

		private LinePatch linePatchField;

		private AreaPatch areaPatchField;

		private double patchWidthField;

		private double patchHeightField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Symbol LabelSymbol
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
		public Symbol DescriptionSymbol
		{
			get
			{
				return this.descriptionSymbolField;
			}
			set
			{
				this.descriptionSymbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public LinePatch LinePatch
		{
			get
			{
				return this.linePatchField;
			}
			set
			{
				this.linePatchField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public AreaPatch AreaPatch
		{
			get
			{
				return this.areaPatchField;
			}
			set
			{
				this.areaPatchField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double PatchWidth
		{
			get
			{
				return this.patchWidthField;
			}
			set
			{
				this.patchWidthField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double PatchHeight
		{
			get
			{
				return this.patchHeightField;
			}
			set
			{
				this.patchHeightField = value;
			}
		}
	}
}
