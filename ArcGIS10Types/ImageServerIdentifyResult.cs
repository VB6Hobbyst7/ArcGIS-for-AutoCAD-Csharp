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
	public class ImageServerIdentifyResult
	{
		private int oIDField;

		private string nameField;

		private string valueField;

		private Point locationField;

		private PropertySet propertiesField;

		private RecordSet catalogItemsField;

		private double[] catalogItemVisibilitiesField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int OID
		{
			get
			{
				return this.oIDField;
			}
			set
			{
				this.oIDField = value;
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
		public Point Location
		{
			get
			{
				return this.locationField;
			}
			set
			{
				this.locationField = value;
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
		public RecordSet CatalogItems
		{
			get
			{
				return this.catalogItemsField;
			}
			set
			{
				this.catalogItemsField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] CatalogItemVisibilities
		{
			get
			{
				return this.catalogItemVisibilitiesField;
			}
			set
			{
				this.catalogItemVisibilitiesField = value;
			}
		}
	}
}
