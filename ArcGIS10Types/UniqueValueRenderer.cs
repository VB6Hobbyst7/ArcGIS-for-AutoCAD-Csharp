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
	public class UniqueValueRenderer : FeatureRenderer
	{
		private string field1Field;

		private string field2Field;

		private string field3Field;

		private string fieldDelimiterField;

		private Symbol defaultSymbolField;

		private string defaultLabelField;

		private UniqueValueInfo[] uniqueValueInfosField;

		private string rotationFieldField;

		private esriRotationType rotationTypeField;

		private bool rotationTypeFieldSpecified;

		private string transparencyFieldField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Field1
		{
			get
			{
				return this.field1Field;
			}
			set
			{
				this.field1Field = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Field2
		{
			get
			{
				return this.field2Field;
			}
			set
			{
				this.field2Field = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Field3
		{
			get
			{
				return this.field3Field;
			}
			set
			{
				this.field3Field = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string FieldDelimiter
		{
			get
			{
				return this.fieldDelimiterField;
			}
			set
			{
				this.fieldDelimiterField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Symbol DefaultSymbol
		{
			get
			{
				return this.defaultSymbolField;
			}
			set
			{
				this.defaultSymbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string DefaultLabel
		{
			get
			{
				return this.defaultLabelField;
			}
			set
			{
				this.defaultLabelField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public UniqueValueInfo[] UniqueValueInfos
		{
			get
			{
				return this.uniqueValueInfosField;
			}
			set
			{
				this.uniqueValueInfosField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string RotationField
		{
			get
			{
				return this.rotationFieldField;
			}
			set
			{
				this.rotationFieldField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriRotationType RotationType
		{
			get
			{
				return this.rotationTypeField;
			}
			set
			{
				this.rotationTypeField = value;
			}
		}

		[XmlIgnore]
		public bool RotationTypeSpecified
		{
			get
			{
				return this.rotationTypeFieldSpecified;
			}
			set
			{
				this.rotationTypeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string TransparencyField
		{
			get
			{
				return this.transparencyFieldField;
			}
			set
			{
				this.transparencyFieldField = value;
			}
		}
	}
}
