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
	public class GraphicFeatureLayer : DataObjectTable
	{
		private string geometryFieldNameField;

		private esriGeometryType geometryTypeField;

		private FeatureLayerDrawingDescription layerDrawingDescriptionField;

		private double maxScaleField;

		private double minScaleField;

		private Envelope spatialExtentField;

		private SpatialReference spatialReferenceField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string GeometryFieldName
		{
			get
			{
				return this.geometryFieldNameField;
			}
			set
			{
				this.geometryFieldNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriGeometryType GeometryType
		{
			get
			{
				return this.geometryTypeField;
			}
			set
			{
				this.geometryTypeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public FeatureLayerDrawingDescription LayerDrawingDescription
		{
			get
			{
				return this.layerDrawingDescriptionField;
			}
			set
			{
				this.layerDrawingDescriptionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MaxScale
		{
			get
			{
				return this.maxScaleField;
			}
			set
			{
				this.maxScaleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MinScale
		{
			get
			{
				return this.minScaleField;
			}
			set
			{
				this.minScaleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Envelope SpatialExtent
		{
			get
			{
				return this.spatialExtentField;
			}
			set
			{
				this.spatialExtentField = value;
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
	}
}
