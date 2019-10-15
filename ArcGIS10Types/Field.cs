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
	public class Field
	{
		private string nameField;

		private esriFieldType typeField;

		private bool isNullableField;

		private int lengthField;

		private int precisionField;

		private int scaleField;

		private bool requiredField;

		private bool requiredFieldSpecified;

		private bool editableField;

		private bool domainFixedField;

		private bool domainFixedFieldSpecified;

		private GeometryDef geometryDefField;

		private string aliasNameField;

		private string modelNameField;

		private object defaultValueField;

		private Domain domainField;

		private RasterDef rasterDefField;

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
		public int Precision
		{
			get
			{
				return this.precisionField;
			}
			set
			{
				this.precisionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int Scale
		{
			get
			{
				return this.scaleField;
			}
			set
			{
				this.scaleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Required
		{
			get
			{
				return this.requiredField;
			}
			set
			{
				this.requiredField = value;
			}
		}

		[XmlIgnore]
		public bool RequiredSpecified
		{
			get
			{
				return this.requiredFieldSpecified;
			}
			set
			{
				this.requiredFieldSpecified = value;
			}
		}

		[DefaultValue(true), XmlElement(Form = XmlSchemaForm.Unqualified)]
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool DomainFixed
		{
			get
			{
				return this.domainFixedField;
			}
			set
			{
				this.domainFixedField = value;
			}
		}

		[XmlIgnore]
		public bool DomainFixedSpecified
		{
			get
			{
				return this.domainFixedFieldSpecified;
			}
			set
			{
				this.domainFixedFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public GeometryDef GeometryDef
		{
			get
			{
				return this.geometryDefField;
			}
			set
			{
				this.geometryDefField = value;
			}
		}

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
		public string ModelName
		{
			get
			{
				return this.modelNameField;
			}
			set
			{
				this.modelNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public object DefaultValue
		{
			get
			{
				return this.defaultValueField;
			}
			set
			{
				this.defaultValueField = value;
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
		public RasterDef RasterDef
		{
			get
			{
				return this.rasterDefField;
			}
			set
			{
				this.rasterDefField = value;
			}
		}

		public Field()
		{
			this.editableField = true;
		}
	}
}
