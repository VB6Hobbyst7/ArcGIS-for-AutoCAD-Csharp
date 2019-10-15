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
	public class MapServerIdentifyResult
	{
		private int layerIDField;

		private string nameField;

		private PropertySet propertiesField;

		private Geometry shapeField;

		private MapServerRelationship[] relationshipsField;

		private string hTMLPopupField;

		private int featureIDField;

		private bool featureIDFieldSpecified;

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

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public MapServerRelationship[] Relationships
		{
			get
			{
				return this.relationshipsField;
			}
			set
			{
				this.relationshipsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string HTMLPopup
		{
			get
			{
				return this.hTMLPopupField;
			}
			set
			{
				this.hTMLPopupField = value;
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

		[XmlIgnore]
		public bool FeatureIDSpecified
		{
			get
			{
				return this.featureIDFieldSpecified;
			}
			set
			{
				this.featureIDFieldSpecified = value;
			}
		}
	}
}
