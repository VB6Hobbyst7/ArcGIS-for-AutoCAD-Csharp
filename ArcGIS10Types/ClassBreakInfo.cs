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
	public class ClassBreakInfo
	{
		private double classMaximumValueField;

		private string labelField;

		private string descriptionField;

		private Symbol symbolField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ClassMaximumValue
		{
			get
			{
				return this.classMaximumValueField;
			}
			set
			{
				this.classMaximumValueField = value;
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
	}
}
