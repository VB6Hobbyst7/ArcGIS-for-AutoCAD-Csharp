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
	public class MapServerFindResult
	{
		private string valueField;

		private int layerIDField;

		private int featureIDField;

		private string fieldNameField;

		private Geometry shapeField;

		private PropertySet propertiesField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int LayerID
		{
			get
			{
				return this.layerIDField;
			}
			set
			{
				this.layerIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int FeatureID
		{
			get
			{
				return this.featureIDField;
			}
			set
			{
				this.featureIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string FieldName
		{
			get
			{
				return this.fieldNameField;
			}
			set
			{
				this.fieldNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Geometry Shape
		{
			get
			{
				return this.shapeField;
			}
			set
			{
				this.shapeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public PropertySet Properties
		{
			get
			{
				return this.propertiesField;
			}
			set
			{
				this.propertiesField = value;
			}
		}
	}
}
