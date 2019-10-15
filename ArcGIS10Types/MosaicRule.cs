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
	public class MosaicRule
	{
		private esriMosaicMethod mosaicMethodField;

		private string whereClauseField;

		private string sortFieldNameField;

		private object sortValueField;

		private bool ascendingField;

		private bool ascendingFieldSpecified;

		private string lockRasterIDField;

		private Point viewpointField;

		private FIDSet fIDsField;

		private rstMosaicOperatorType mosaicOperationField;

		private bool mosaicOperationFieldSpecified;

		private TimeValue timeValueField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriMosaicMethod MosaicMethod
		{
			get
			{
				return this.mosaicMethodField;
			}
			set
			{
				this.mosaicMethodField = value;
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
		public string SortFieldName
		{
			get
			{
				return this.sortFieldNameField;
			}
			set
			{
				this.sortFieldNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public object SortValue
		{
			get
			{
				return this.sortValueField;
			}
			set
			{
				this.sortValueField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Ascending
		{
			get
			{
				return this.ascendingField;
			}
			set
			{
				this.ascendingField = value;
			}
		}

		[XmlIgnore]
		public bool AscendingSpecified
		{
			get
			{
				return this.ascendingFieldSpecified;
			}
			set
			{
				this.ascendingFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string LockRasterID
		{
			get
			{
				return this.lockRasterIDField;
			}
			set
			{
				this.lockRasterIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Point Viewpoint
		{
			get
			{
				return this.viewpointField;
			}
			set
			{
				this.viewpointField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public FIDSet FIDs
		{
			get
			{
				return this.fIDsField;
			}
			set
			{
				this.fIDsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public rstMosaicOperatorType MosaicOperation
		{
			get
			{
				return this.mosaicOperationField;
			}
			set
			{
				this.mosaicOperationField = value;
			}
		}

		[XmlIgnore]
		public bool MosaicOperationSpecified
		{
			get
			{
				return this.mosaicOperationFieldSpecified;
			}
			set
			{
				this.mosaicOperationFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TimeValue TimeValue
		{
			get
			{
				return this.timeValueField;
			}
			set
			{
				this.timeValueField = value;
			}
		}
	}
}
