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
	public class PropertyInfo
	{
		private string aliasNameField;

		private Domain domainField;

		private bool editableField;

		private bool editableFieldSpecified;

		private bool isNullableField;

		private bool isNullableFieldSpecified;

		private int lengthField;

		private string nameField;

		private esriFieldType typeField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string AliasName
		{
			get
			{
				return this.aliasNameField;
			}
			set
			{
				this.aliasNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Domain Domain
		{
			get
			{
				return this.domainField;
			}
			set
			{
				this.domainField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Editable
		{
			get
			{
				return this.editableField;
			}
			set
			{
				this.editableField = value;
			}
		}

		[XmlIgnore]
		public bool EditableSpecified
		{
			get
			{
				return this.editableFieldSpecified;
			}
			set
			{
				this.editableFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IsNullable
		{
			get
			{
				return this.isNullableField;
			}
			set
			{
				this.isNullableField = value;
			}
		}

		[XmlIgnore]
		public bool IsNullableSpecified
		{
			get
			{
				return this.isNullableFieldSpecified;
			}
			set
			{
				this.isNullableFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int Length
		{
			get
			{
				return this.lengthField;
			}
			set
			{
				this.lengthField = value;
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
		public esriFieldType Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}
	}
}
