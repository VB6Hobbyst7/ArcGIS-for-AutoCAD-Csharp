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
	public class MapImage
	{
		private byte[] imageDataField;

		private string imageURLField;

		private Envelope extentField;

		private int[] visibleLayerIDsField;

		private double mapScaleField;

		private int imageHeightField;

		private int imageWidthField;

		private double imageDPIField;

		private string imageTypeField;

		[XmlElement(Form = XmlSchemaForm.Unqualified, DataType = "base64Binary")]
		public byte[] ImageData
		{
			get
			{
				return this.imageDataField;
			}
			set
			{
				this.imageDataField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ImageURL
		{
			get
			{
				return this.imageURLField;
			}
			set
			{
				this.imageURLField = value;
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

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] VisibleLayerIDs
		{
			get
			{
				return this.visibleLayerIDsField;
			}
			set
			{
				this.visibleLayerIDsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MapScale
		{
			get
			{
				return this.mapScaleField;
			}
			set
			{
				this.mapScaleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ImageHeight
		{
			get
			{
				return this.imageHeightField;
			}
			set
			{
				this.imageHeightField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ImageWidth
		{
			get
			{
				return this.imageWidthField;
			}
			set
			{
				this.imageWidthField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ImageDPI
		{
			get
			{
				return this.imageDPIField;
			}
			set
			{
				this.imageDPIField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ImageType
		{
			get
			{
				return this.imageTypeField;
			}
			set
			{
				this.imageTypeField = value;
			}
		}
	}
}
