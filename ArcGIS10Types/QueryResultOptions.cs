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
	public class QueryResultOptions
	{
		private esriQueryResultFormat formatField;

		private PropertySet formatPropertiesField;

		private GeoTransformation geoTransformationField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriQueryResultFormat Format
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public PropertySet FormatProperties
		{
			get
			{
				return this.formatPropertiesField;
			}
			set
			{
				this.formatPropertiesField = value;
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
	}
}
