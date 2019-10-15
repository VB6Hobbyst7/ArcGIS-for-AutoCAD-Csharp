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
	public class RelateDescription
	{
		private int relationshipIDField;

		private string relatedTableDefinitionExpressionField;

		private string relatedTableFieldsField;

		private SpatialReference outputSpatialReferenceField;

		private GeoTransformation geoTransformationField;

		private bool includeGeometryField;

		private GeometryResultOptions geometryResultOptionsField;

		private esriRelateResultFormat resultFormatField;

		private TimeReference outputTimeReferenceField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int RelationshipID
		{
			get
			{
				return this.relationshipIDField;
			}
			set
			{
				this.relationshipIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string RelatedTableDefinitionExpression
		{
			get
			{
				return this.relatedTableDefinitionExpressionField;
			}
			set
			{
				this.relatedTableDefinitionExpressionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string RelatedTableFields
		{
			get
			{
				return this.relatedTableFieldsField;
			}
			set
			{
				this.relatedTableFieldsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public SpatialReference OutputSpatialReference
		{
			get
			{
				return this.outputSpatialReferenceField;
			}
			set
			{
				this.outputSpatialReferenceField = value;
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

		[DefaultValue(true), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IncludeGeometry
		{
			get
			{
				return this.includeGeometryField;
			}
			set
			{
				this.includeGeometryField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public GeometryResultOptions GeometryResultOptions
		{
			get
			{
				return this.geometryResultOptionsField;
			}
			set
			{
				this.geometryResultOptionsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriRelateResultFormat ResultFormat
		{
			get
			{
				return this.resultFormatField;
			}
			set
			{
				this.resultFormatField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TimeReference OutputTimeReference
		{
			get
			{
				return this.outputTimeReferenceField;
			}
			set
			{
				this.outputTimeReferenceField = value;
			}
		}

		public RelateDescription()
		{
			this.includeGeometryField = true;
		}
	}
}
