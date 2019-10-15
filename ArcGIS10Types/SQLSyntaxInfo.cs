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
	public class SQLSyntaxInfo
	{
		private PropertySet functionNamesField;

		private PropertySet specialCharactersField;

		private string[] supportedPredicatesField;

		private string[] supportedClausesField;

		private bool identifierCaseField;

		private bool delimitedIdentifierCaseField;

		private bool stringComparisonCaseField;

		private string[] keywordsField;

		private string invalidCharactersField;

		private string invalidStartingCharactersField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public PropertySet FunctionNames
		{
			get
			{
				return this.functionNamesField;
			}
			set
			{
				this.functionNamesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public PropertySet SpecialCharacters
		{
			get
			{
				return this.specialCharactersField;
			}
			set
			{
				this.specialCharactersField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public string[] SupportedPredicates
		{
			get
			{
				return this.supportedPredicatesField;
			}
			set
			{
				this.supportedPredicatesField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public string[] SupportedClauses
		{
			get
			{
				return this.supportedClausesField;
			}
			set
			{
				this.supportedClausesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IdentifierCase
		{
			get
			{
				return this.identifierCaseField;
			}
			set
			{
				this.identifierCaseField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool DelimitedIdentifierCase
		{
			get
			{
				return this.delimitedIdentifierCaseField;
			}
			set
			{
				this.delimitedIdentifierCaseField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool StringComparisonCase
		{
			get
			{
				return this.stringComparisonCaseField;
			}
			set
			{
				this.stringComparisonCaseField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public string[] Keywords
		{
			get
			{
				return this.keywordsField;
			}
			set
			{
				this.keywordsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string InvalidCharacters
		{
			get
			{
				return this.invalidCharactersField;
			}
			set
			{
				this.invalidCharactersField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string InvalidStartingCharacters
		{
			get
			{
				return this.invalidStartingCharactersField;
			}
			set
			{
				this.invalidStartingCharactersField = value;
			}
		}
	}
}
