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
	public class ImageServiceInfo
	{
		private string nameField;

		private string descriptionField;

		private Envelope extentField;

		private double pixelSizeXField;

		private double pixelSizeYField;

		private int bandCountField;

		private rstPixelType pixelTypeField;

		private object[] noDataField;

		private double minPixelSizeField;

		private double maxPixelSizeField;

		private string copyrightTextField;

		private esriImageServiceDataType serviceDataTypeField;

		private double[] minValuesField;

		private double[] maxValuesField;

		private double[] meanValuesField;

		private double[] stdvValuesField;

		private string servicePropertiesField;

		private int maxNColsField;

		private int maxNRowsField;

		private esriImageServiceSourceType serviceSourceTypeField;

		private bool serviceSourceTypeFieldSpecified;

		private string allowedFieldsField;

		private string allowedCompressionsField;

		private string allowedMosaicMethodsField;

		private string allowedItemMetadataField;

		private int maxRecordCountField;

		private bool maxRecordCountFieldSpecified;

		private int maxDownloadImageCountField;

		private bool maxDownloadImageCountFieldSpecified;

		private int maxMosaicImageCountField;

		private bool maxMosaicImageCountFieldSpecified;

		private string defaultCompressionField;

		private int defaultCompressionQualityField;

		private bool defaultCompressionQualityFieldSpecified;

		private rstResamplingTypes defaultResamplingMethodField;

		private bool defaultResamplingMethodFieldSpecified;

		private esriMosaicMethod defaultMosaicMethodField;

		private bool defaultMosaicMethodFieldSpecified;

		private bool supportBSQField;

		private bool supportBSQFieldSpecified;

		private bool supportsTimeField;

		private bool supportsTimeFieldSpecified;

		private string startTimeFieldNameField;

		private string endTimeFieldNameField;

		private string timeValueFormatField;

		private TimeReference timeReferenceField;

		private TimeExtent timeExtentField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
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
		public double PixelSizeX
		{
			get
			{
				return this.pixelSizeXField;
			}
			set
			{
				this.pixelSizeXField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double PixelSizeY
		{
			get
			{
				return this.pixelSizeYField;
			}
			set
			{
				this.pixelSizeYField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int BandCount
		{
			get
			{
				return this.bandCountField;
			}
			set
			{
				this.bandCountField = value;
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

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("AnyType", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public object[] NoData
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
		public double MinPixelSize
		{
			get
			{
				return this.minPixelSizeField;
			}
			set
			{
				this.minPixelSizeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MaxPixelSize
		{
			get
			{
				return this.maxPixelSizeField;
			}
			set
			{
				this.maxPixelSizeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string CopyrightText
		{
			get
			{
				return this.copyrightTextField;
			}
			set
			{
				this.copyrightTextField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriImageServiceDataType ServiceDataType
		{
			get
			{
				return this.serviceDataTypeField;
			}
			set
			{
				this.serviceDataTypeField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] MinValues
		{
			get
			{
				return this.minValuesField;
			}
			set
			{
				this.minValuesField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] MaxValues
		{
			get
			{
				return this.maxValuesField;
			}
			set
			{
				this.maxValuesField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] MeanValues
		{
			get
			{
				return this.meanValuesField;
			}
			set
			{
				this.meanValuesField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] StdvValues
		{
			get
			{
				return this.stdvValuesField;
			}
			set
			{
				this.stdvValuesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ServiceProperties
		{
			get
			{
				return this.servicePropertiesField;
			}
			set
			{
				this.servicePropertiesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int MaxNCols
		{
			get
			{
				return this.maxNColsField;
			}
			set
			{
				this.maxNColsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int MaxNRows
		{
			get
			{
				return this.maxNRowsField;
			}
			set
			{
				this.maxNRowsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriImageServiceSourceType ServiceSourceType
		{
			get
			{
				return this.serviceSourceTypeField;
			}
			set
			{
				this.serviceSourceTypeField = value;
			}
		}

		[XmlIgnore]
		public bool ServiceSourceTypeSpecified
		{
			get
			{
				return this.serviceSourceTypeFieldSpecified;
			}
			set
			{
				this.serviceSourceTypeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string AllowedFields
		{
			get
			{
				return this.allowedFieldsField;
			}
			set
			{
				this.allowedFieldsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string AllowedCompressions
		{
			get
			{
				return this.allowedCompressionsField;
			}
			set
			{
				this.allowedCompressionsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string AllowedMosaicMethods
		{
			get
			{
				return this.allowedMosaicMethodsField;
			}
			set
			{
				this.allowedMosaicMethodsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string AllowedItemMetadata
		{
			get
			{
				return this.allowedItemMetadataField;
			}
			set
			{
				this.allowedItemMetadataField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int MaxRecordCount
		{
			get
			{
				return this.maxRecordCountField;
			}
			set
			{
				this.maxRecordCountField = value;
			}
		}

		[XmlIgnore]
		public bool MaxRecordCountSpecified
		{
			get
			{
				return this.maxRecordCountFieldSpecified;
			}
			set
			{
				this.maxRecordCountFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int MaxDownloadImageCount
		{
			get
			{
				return this.maxDownloadImageCountField;
			}
			set
			{
				this.maxDownloadImageCountField = value;
			}
		}

		[XmlIgnore]
		public bool MaxDownloadImageCountSpecified
		{
			get
			{
				return this.maxDownloadImageCountFieldSpecified;
			}
			set
			{
				this.maxDownloadImageCountFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int MaxMosaicImageCount
		{
			get
			{
				return this.maxMosaicImageCountField;
			}
			set
			{
				this.maxMosaicImageCountField = value;
			}
		}

		[XmlIgnore]
		public bool MaxMosaicImageCountSpecified
		{
			get
			{
				return this.maxMosaicImageCountFieldSpecified;
			}
			set
			{
				this.maxMosaicImageCountFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string DefaultCompression
		{
			get
			{
				return this.defaultCompressionField;
			}
			set
			{
				this.defaultCompressionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int DefaultCompressionQuality
		{
			get
			{
				return this.defaultCompressionQualityField;
			}
			set
			{
				this.defaultCompressionQualityField = value;
			}
		}

		[XmlIgnore]
		public bool DefaultCompressionQualitySpecified
		{
			get
			{
				return this.defaultCompressionQualityFieldSpecified;
			}
			set
			{
				this.defaultCompressionQualityFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public rstResamplingTypes DefaultResamplingMethod
		{
			get
			{
				return this.defaultResamplingMethodField;
			}
			set
			{
				this.defaultResamplingMethodField = value;
			}
		}

		[XmlIgnore]
		public bool DefaultResamplingMethodSpecified
		{
			get
			{
				return this.defaultResamplingMethodFieldSpecified;
			}
			set
			{
				this.defaultResamplingMethodFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriMosaicMethod DefaultMosaicMethod
		{
			get
			{
				return this.defaultMosaicMethodField;
			}
			set
			{
				this.defaultMosaicMethodField = value;
			}
		}

		[XmlIgnore]
		public bool DefaultMosaicMethodSpecified
		{
			get
			{
				return this.defaultMosaicMethodFieldSpecified;
			}
			set
			{
				this.defaultMosaicMethodFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool SupportBSQ
		{
			get
			{
				return this.supportBSQField;
			}
			set
			{
				this.supportBSQField = value;
			}
		}

		[XmlIgnore]
		public bool SupportBSQSpecified
		{
			get
			{
				return this.supportBSQFieldSpecified;
			}
			set
			{
				this.supportBSQFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool SupportsTime
		{
			get
			{
				return this.supportsTimeField;
			}
			set
			{
				this.supportsTimeField = value;
			}
		}

		[XmlIgnore]
		public bool SupportsTimeSpecified
		{
			get
			{
				return this.supportsTimeFieldSpecified;
			}
			set
			{
				this.supportsTimeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string StartTimeFieldName
		{
			get
			{
				return this.startTimeFieldNameField;
			}
			set
			{
				this.startTimeFieldNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string EndTimeFieldName
		{
			get
			{
				return this.endTimeFieldNameField;
			}
			set
			{
				this.endTimeFieldNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string TimeValueFormat
		{
			get
			{
				return this.timeValueFormatField;
			}
			set
			{
				this.timeValueFormatField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TimeReference TimeReference
		{
			get
			{
				return this.timeReferenceField;
			}
			set
			{
				this.timeReferenceField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TimeExtent TimeExtent
		{
			get
			{
				return this.timeExtentField;
			}
			set
			{
				this.timeExtentField = value;
			}
		}
	}
}
