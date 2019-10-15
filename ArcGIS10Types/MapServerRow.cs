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
	public class MapServerRow
	{
		private string nameField;

		private PropertySet propertiesField;

		private MapServerRelationship[] relationshipsField;

		private int featureIDField;

		private bool featureIDFieldSpecified;

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
