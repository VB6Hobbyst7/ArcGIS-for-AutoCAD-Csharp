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
	public class MapDescription
	{
		private string nameField;

		private MapArea mapAreaField;

		private LayerDescription[] layerDescriptionsField;

		private double rotationField;

		private SpatialReference spatialReferenceField;

		private Color transparentColorField;

		private Color selectionColorField;

		private FillSymbol backgroundSymbolField;

		private GraphicElement[] customGraphicsField;

		private GeoTransformation geoTransformationField;

		private TimeReference timeReferenceField;

		private TimeValue timeValueField;

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
		public MapArea MapArea
		{
			get
			{
				return this.mapAreaField;
			}
			set
			{
				this.mapAreaField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public LayerDescription[] LayerDescriptions
		{
			get
			{
				return this.layerDescriptionsField;
			}
			set
			{
				this.layerDescriptionsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Rotation
		{
			get
			{
				return this.rotationField;
			}
			set
			{
				this.rotationField = value;
			}
		}

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
		public Color TransparentColor
		{
			get
			{
				return this.transparentColorField;
			}
			set
			{
				this.transparentColorField = value;
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
		public FillSymbol BackgroundSymbol
		{
			get
			{
				return this.backgroundSymbolField;
			}
			set
			{
				this.backgroundSymbolField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public GraphicElement[] CustomGraphics
		{
			get
			{
				return this.customGraphicsField;
			}
			set
			{
				this.customGraphicsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public GeoTransformation GeoTransformation
		{
			get
			{
				return this.geoTransformationField;
			}
			set
			{
				this.geoTransformationField = value;
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
		public TimeValue TimeValue
		{
			get
			{
				return this.timeValueField;
			}
			set
			{
				this.timeValueField = value;
			}
		}
	}
}
