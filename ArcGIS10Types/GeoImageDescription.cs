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
	public class GeoImageDescription
	{
		private SpatialReference spatialReferenceField;

		private Envelope extentField;

		private int widthField;

		private int heightField;

		private rstPixelType pixelTypeField;

		private bool pixelTypeFieldSpecified;

		private object noDataField;

		private rstResamplingTypes interpolationField;

		private bool interpolationFieldSpecified;

		private string compressionField;

		private int compressionQualityField;

		private bool compressionQualityFieldSpecified;

		private int[] bandIDsField;

		private string mosaicPropertiesField;

		private string viewpointPropertiesField;

		private MosaicRule mosaicRuleField;

		private RenderingRule renderingRuleField;

		private bool bSQField;

		private bool bSQFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public SpatialReference SpatialReference
		{
			get
			{
				return this.spatialReferenceField;
			}
			set
			{
				this.spatialReferenceField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Envelope Extent
		{
			get
			{
				return this.extentField;
			}
			set
			{
				this.extentField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int Width
		{
			get
			{
				return this.widthField;
			}
			set
			{
				this.widthField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int Height
		{
			get
			{
				return this.heightField;
			}
			set
			{
				this.heightField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public rstPixelType PixelType
		{
			get
			{
				return this.pixelTypeField;
			}
			set
			{
				this.pixelTypeField = value;
			}
		}

		[XmlIgnore]
		public bool PixelTypeSpecified
		{
			get
			{
				return this.pixelTypeFieldSpecified;
			}
			set
			{
				this.pixelTypeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public object NoData
		{
			get
			{
				return this.noDataField;
			}
			set
			{
				this.noDataField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public rstResamplingTypes Interpolation
		{
			get
			{
				return this.interpolationField;
			}
			set
			{
				this.interpolationField = value;
			}
		}

		[XmlIgnore]
		public bool InterpolationSpecified
		{
			get
			{
				return this.interpolationFieldSpecified;
			}
			set
			{
				this.interpolationFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Compression
		{
			get
			{
				return this.compressionField;
			}
			set
			{
				this.compressionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int CompressionQuality
		{
			get
			{
				return this.compressionQualityField;
			}
			set
			{
				this.compressionQualityField = value;
			}
		}

		[XmlIgnore]
		public bool CompressionQualitySpecified
		{
			get
			{
				return this.compressionQualityFieldSpecified;
			}
			set
			{
				this.compressionQualityFieldSpecified = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] BandIDs
		{
			get
			{
				return this.bandIDsField;
			}
			set
			{
				this.bandIDsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string MosaicProperties
		{
			get
			{
				return this.mosaicPropertiesField;
			}
			set
			{
				this.mosaicPropertiesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ViewpointProperties
		{
			get
			{
				return this.viewpointPropertiesField;
			}
			set
			{
				this.viewpointPropertiesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public MosaicRule MosaicRule
		{
			get
			{
				return this.mosaicRuleField;
			}
			set
			{
				this.mosaicRuleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public RenderingRule RenderingRule
		{
			get
			{
				return this.renderingRuleField;
			}
			set
			{
				this.renderingRuleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool BSQ
		{
			get
			{
				return this.bSQField;
			}
			set
			{
				this.bSQField = value;
			}
		}

		[XmlIgnore]
		public bool BSQSpecified
		{
			get
			{
				return this.bSQFieldSpecified;
			}
			set
			{
				this.bSQFieldSpecified = value;
			}
		}
	}
}
