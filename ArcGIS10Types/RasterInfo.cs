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
	public class RasterInfo
	{
		private Point originField;

		private int blockWidthField;

		private bool blockWidthFieldSpecified;

		private int blockHeightField;

		private bool blockHeightFieldSpecified;

		private double pixelSizeXField;

		private bool pixelSizeXFieldSpecified;

		private double pixelSizeYField;

		private bool pixelSizeYFieldSpecified;

		private GeodataXform geodataXformField;

		private Envelope extentField;

		private SpatialReference nativeSpatialReferenceField;

		private Envelope nativeExtentField;

		private int bandCountField;

		private bool bandCountFieldSpecified;

		private rstPixelType pixelTypeField;

		private bool pixelTypeFieldSpecified;

		private object[] noDataField;

		private int firstPyramidLevelField;

		private bool firstPyramidLevelFieldSpecified;

		private int maximumPyramidLevelField;

		private bool maximumPyramidLevelFieldSpecified;

		private string formatField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Point Origin
		{
			get
			{
				return this.originField;
			}
			set
			{
				this.originField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int BlockWidth
		{
			get
			{
				return this.blockWidthField;
			}
			set
			{
				this.blockWidthField = value;
			}
		}

		[XmlIgnore]
		public bool BlockWidthSpecified
		{
			get
			{
				return this.blockWidthFieldSpecified;
			}
			set
			{
				this.blockWidthFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int BlockHeight
		{
			get
			{
				return this.blockHeightField;
			}
			set
			{
				this.blockHeightField = value;
			}
		}

		[XmlIgnore]
		public bool BlockHeightSpecified
		{
			get
			{
				return this.blockHeightFieldSpecified;
			}
			set
			{
				this.blockHeightFieldSpecified = value;
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

		[XmlIgnore]
		public bool PixelSizeXSpecified
		{
			get
			{
				return this.pixelSizeXFieldSpecified;
			}
			set
			{
				this.pixelSizeXFieldSpecified = value;
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

		[XmlIgnore]
		public bool PixelSizeYSpecified
		{
			get
			{
				return this.pixelSizeYFieldSpecified;
			}
			set
			{
				this.pixelSizeYFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public GeodataXform GeodataXform
		{
			get
			{
				return this.geodataXformField;
			}
			set
			{
				this.geodataXformField = value;
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
		public SpatialReference NativeSpatialReference
		{
			get
			{
				return this.nativeSpatialReferenceField;
			}
			set
			{
				this.nativeSpatialReferenceField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Envelope NativeExtent
		{
			get
			{
				return this.nativeExtentField;
			}
			set
			{
				this.nativeExtentField = value;
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

		[XmlIgnore]
		public bool BandCountSpecified
		{
			get
			{
				return this.bandCountFieldSpecified;
			}
			set
			{
				this.bandCountFieldSpecified = value;
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
		public int FirstPyramidLevel
		{
			get
			{
				return this.firstPyramidLevelField;
			}
			set
			{
				this.firstPyramidLevelField = value;
			}
		}

		[XmlIgnore]
		public bool FirstPyramidLevelSpecified
		{
			get
			{
				return this.firstPyramidLevelFieldSpecified;
			}
			set
			{
				this.firstPyramidLevelFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int MaximumPyramidLevel
		{
			get
			{
				return this.maximumPyramidLevelField;
			}
			set
			{
				this.maximumPyramidLevelField = value;
			}
		}

		[XmlIgnore]
		public bool MaximumPyramidLevelSpecified
		{
			get
			{
				return this.maximumPyramidLevelFieldSpecified;
			}
			set
			{
				this.maximumPyramidLevelFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Format
		{
			get
			{
				return this.formatField;
			}
			set
			{
				this.formatField = value;
			}
		}
	}
}
