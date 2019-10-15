using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(TimeQueryFilter)), XmlInclude(typeof(ImageQueryFilter)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public class SpatialFilter : QueryFilter
	{
		private esriSearchOrder searchOrderField;

		private esriSpatialRelEnum spatialRelField;

		private string spatialRelDescriptionField;

		private Geometry filterGeometryField;

		private string geometryFieldNameField;

		private bool filterOwnsGeometryField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriSearchOrder SearchOrder
		{
			get
			{
				return this.searchOrderField;
			}
			set
			{
				this.searchOrderField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriSpatialRelEnum SpatialRel
		{
			get
			{
				return this.spatialRelField;
			}
			set
			{
				this.spatialRelField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string SpatialRelDescription
		{
			get
			{
				return this.spatialRelDescriptionField;
			}
			set
			{
				this.spatialRelDescriptionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Geometry FilterGeometry
		{
			get
			{
				return this.filterGeometryField;
			}
			set
			{
				this.filterGeometryField = value;
			}
		}

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
		public bool FilterOwnsGeometry
		{
			get
			{
				return this.filterOwnsGeometryField;
			}
			set
			{
				this.filterOwnsGeometryField = value;
			}
		}
	}
}
