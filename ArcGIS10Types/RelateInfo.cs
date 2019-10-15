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
	public class RelateInfo
	{
		private string nameField;

		private int relationshipIDField;

		private int relatedTableIDField;

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
		public int RelationshipID
		{
			get
			{
				return this.relationshipIDField;
			}
			set
			{
				this.relationshipIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int RelatedTableID
		{
			get
			{
				return this.relatedTableIDField;
			}
			set
			{
				this.relatedTableIDField = value;
			}
		}
	}
}
