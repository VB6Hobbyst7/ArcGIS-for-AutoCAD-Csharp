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
	public class SimpleRenderer : FeatureRenderer
	{
		private Symbol symbolField;

		private string labelField;

		private string descriptionField;

		private string rotationFieldField;

		private esriRotationType rotationTypeField;

		private bool rotationTypeFieldSpecified;

		private string transparencyFieldField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Symbol Symbol
		{
			get
			{
				return this.symbolField;
			}
			set
			{
				this.symbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Label
		{
			get
			{
				return this.labelField;
			}
			set
			{
				this.labelField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
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
