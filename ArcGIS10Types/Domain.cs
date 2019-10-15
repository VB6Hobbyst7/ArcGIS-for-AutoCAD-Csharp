using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(RangeDomain)), XmlInclude(typeof(CodedValueDomain)), XmlInclude(typeof(BitMaskCodedValueDomain)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public abstract class Domain
	{
		private string domainNameField;

		private esriFieldType fieldTypeField;

		private esriMergePolicyType mergePolicyField;

		private esriSplitPolicyType splitPolicyField;

		private string descriptionField;

		private string ownerField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string DomainName
		{
			get
			{
				return this.domainNameField;
			}
			set
			{
				this.domainNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriFieldType FieldType
		{
			get
			{
				return this.fieldTypeField;
			}
			set
			{
				this.fieldTypeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriMergePolicyType MergePolicy
		{
			get
			{
				return this.mergePolicyField;
			}
			set
			{
				this.mergePolicyField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriSplitPolicyType SplitPolicy
		{
			get
			{
				return this.splitPolicyField;
			}
			set
			{
				this.splitPolicyField = value;
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
		public string Owner
		{
			get
			{
				return this.ownerField;
			}
			set
			{
				this.ownerField = value;
			}
		}
	}
}
