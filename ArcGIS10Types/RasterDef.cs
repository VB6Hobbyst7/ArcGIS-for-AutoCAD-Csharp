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
	public class RasterDef
	{
		private string descriptionField;

		private bool isByRefField;

		private bool isByRefFieldSpecified;

		private SpatialReference spatialReferenceField;

		private bool isByFunctionField;

		private bool isByFunctionFieldSpecified;

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
		public bool IsByRef
		{
			get
			{
				return this.isByRefField;
			}
			set
			{
				this.isByRefField = value;
			}
		}

		[XmlIgnore]
		public bool IsByRefSpecified
		{
			get
			{
				return this.isByRefFieldSpecified;
			}
			set
			{
				this.isByRefFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public SpatialReference SpatialReference
		{
			get
			{
				return this.spatialReferenceField;
			}
			set
			{
				this.spatialReferenceField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IsByFunction
		{
			get
			{
				return this.isByFunctionField;
			}
			set
			{
				this.isByFunctionField = value;
			}
		}

		[XmlIgnore]
		public bool IsByFunctionSpecified
		{
			get
			{
				return this.isByFunctionFieldSpecified;
			}
			set
			{
				this.isByFunctionFieldSpecified = value;
			}
		}
	}
}
