using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(SpatialFilter)), XmlInclude(typeof(TimeQueryFilter)), XmlInclude(typeof(ImageQueryFilter)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public class QueryFilter
	{
		private string subFieldsField;

		private string whereClauseField;

		private string spatialReferenceFieldNameField;

		private double resolutionField;

		private SpatialReference outputSpatialReferenceField;

		private FIDSet fIDSetField;

		private string postfixClauseField;

		private FilterDef[] filterDefsField;

		private string prefixClauseField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string SubFields
		{
			get
			{
				return this.subFieldsField;
			}
			set
			{
				this.subFieldsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string WhereClause
		{
			get
			{
				return this.whereClauseField;
			}
			set
			{
				this.whereClauseField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string SpatialReferenceFieldName
		{
			get
			{
				return this.spatialReferenceFieldNameField;
			}
			set
			{
				this.spatialReferenceFieldNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Resolution
		{
			get
			{
				return this.resolutionField;
			}
			set
			{
				this.resolutionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public SpatialReference OutputSpatialReference
		{
			get
			{
				return this.outputSpatialReferenceField;
			}
			set
			{
				this.outputSpatialReferenceField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public FIDSet FIDSet
		{
			get
			{
				return this.fIDSetField;
			}
			set
			{
				this.fIDSetField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string PostfixClause
		{
			get
			{
				return this.postfixClauseField;
			}
			set
			{
				this.postfixClauseField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public FilterDef[] FilterDefs
		{
			get
			{
				return this.filterDefsField;
			}
			set
			{
				this.filterDefsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string PrefixClause
		{
			get
			{
				return this.prefixClauseField;
			}
			set
			{
				this.prefixClauseField = value;
			}
		}
	}
}
