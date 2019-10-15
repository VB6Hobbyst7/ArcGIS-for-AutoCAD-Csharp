using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(RasterStretchRenderer)), XmlInclude(typeof(RasterUniqueValueRenderer)), XmlInclude(typeof(RasterRGBRenderer)), XmlInclude(typeof(RasterClassifyRenderer)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public abstract class RasterRenderer
	{
		private bool indexedField;

		private bool indexedFieldSpecified;

		private int brightnessField;

		private bool brightnessFieldSpecified;

		private int contrastField;

		private bool contrastFieldSpecified;

		private string resamplingTypeField;

		private Color noDataColorField;

		private double[] noDataValueField;

		private int alphaBandIndexField;

		private bool alphaBandIndexFieldSpecified;

		private bool useAlphaBandField;

		private bool useAlphaBandFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Indexed
		{
			get
			{
				return this.indexedField;
			}
			set
			{
				this.indexedField = value;
			}
		}

		[XmlIgnore]
		public bool IndexedSpecified
		{
			get
			{
				return this.indexedFieldSpecified;
			}
			set
			{
				this.indexedFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int Brightness
		{
			get
			{
				return this.brightnessField;
			}
			set
			{
				this.brightnessField = value;
			}
		}

		[XmlIgnore]
		public bool BrightnessSpecified
		{
			get
			{
				return this.brightnessFieldSpecified;
			}
			set
			{
				this.brightnessFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int Contrast
		{
			get
			{
				return this.contrastField;
			}
			set
			{
				this.contrastField = value;
			}
		}

		[XmlIgnore]
		public bool ContrastSpecified
		{
			get
			{
				return this.contrastFieldSpecified;
			}
			set
			{
				this.contrastFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ResamplingType
		{
			get
			{
				return this.resamplingTypeField;
			}
			set
			{
				this.resamplingTypeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Color NoDataColor
		{
			get
			{
				return this.noDataColorField;
			}
			set
			{
				this.noDataColorField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] NoDataValue
		{
			get
			{
				return this.noDataValueField;
			}
			set
			{
				this.noDataValueField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int AlphaBandIndex
		{
			get
			{
				return this.alphaBandIndexField;
			}
			set
			{
				this.alphaBandIndexField = value;
			}
		}

		[XmlIgnore]
		public bool AlphaBandIndexSpecified
		{
			get
			{
				return this.alphaBandIndexFieldSpecified;
			}
			set
			{
				this.alphaBandIndexFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool UseAlphaBand
		{
			get
			{
				return this.useAlphaBandField;
			}
			set
			{
				this.useAlphaBandField = value;
			}
		}

		[XmlIgnore]
		public bool UseAlphaBandSpecified
		{
			get
			{
				return this.useAlphaBandFieldSpecified;
			}
			set
			{
				this.useAlphaBandFieldSpecified = value;
			}
		}
	}
}
