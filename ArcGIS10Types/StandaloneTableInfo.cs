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
	public class StandaloneTableInfo : MapTableInfo
	{
		private int idField;

		private string nameField;

		private Fields fieldsField;

		private RelateInfo[] relateInfosField;

		private bool supportsTimeField;

		private bool supportsTimeFieldSpecified;

		private string startTimeFieldNameField;

		private string endTimeFieldNameField;

		private string timeValueFormatField;

		private string trackIDFieldNameField;

		private TimeReference timeReferenceField;

		private TimeExtent fullTimeExtentField;

		private double timeIntervalField;

		private esriTimeUnits timeIntervalUnitsField;

		private bool timeIntervalUnitsFieldSpecified;

		private bool hasAttachmentsField;

		private bool hasAttachmentsFieldSpecified;

		private string displayFieldField;

		private string descriptionField;

		private string iDFieldField;

		private bool hasSubtypeField;

		private bool hasSubtypeFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
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
		public Fields Fields
		{
			get
			{
				return this.fieldsField;
			}
			set
			{
				this.fieldsField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public RelateInfo[] RelateInfos
		{
			get
			{
				return this.relateInfosField;
			}
			set
			{
				this.relateInfosField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool SupportsTime
		{
			get
			{
				return this.supportsTimeField;
			}
			set
			{
				this.supportsTimeField = value;
			}
		}

		[XmlIgnore]
		public bool SupportsTimeSpecified
		{
			get
			{
				return this.supportsTimeFieldSpecified;
			}
			set
			{
				this.supportsTimeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string StartTimeFieldName
		{
			get
			{
				return this.startTimeFieldNameField;
			}
			set
			{
				this.startTimeFieldNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string EndTimeFieldName
		{
			get
			{
				return this.endTimeFieldNameField;
			}
			set
			{
				this.endTimeFieldNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string TimeValueFormat
		{
			get
			{
				return this.timeValueFormatField;
			}
			set
			{
				this.timeValueFormatField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string TrackIDFieldName
		{
			get
			{
				return this.trackIDFieldNameField;
			}
			set
			{
				this.trackIDFieldNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TimeReference TimeReference
		{
			get
			{
				return this.timeReferenceField;
			}
			set
			{
				this.timeReferenceField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TimeExtent FullTimeExtent
		{
			get
			{
				return this.fullTimeExtentField;
			}
			set
			{
				this.fullTimeExtentField = value;
			}
		}

		[DefaultValue(0), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double TimeInterval
		{
			get
			{
				return this.timeIntervalField;
			}
			set
			{
				this.timeIntervalField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriTimeUnits TimeIntervalUnits
		{
			get
			{
				return this.timeIntervalUnitsField;
			}
			set
			{
				this.timeIntervalUnitsField = value;
			}
		}

		[XmlIgnore]
		public bool TimeIntervalUnitsSpecified
		{
			get
			{
				return this.timeIntervalUnitsFieldSpecified;
			}
			set
			{
				this.timeIntervalUnitsFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasAttachments
		{
			get
			{
				return this.hasAttachmentsField;
			}
			set
			{
				this.hasAttachmentsField = value;
			}
		}

		[XmlIgnore]
		public bool HasAttachmentsSpecified
		{
			get
			{
				return this.hasAttachmentsFieldSpecified;
			}
			set
			{
				this.hasAttachmentsFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string DisplayField
		{
			get
			{
				return this.displayFieldField;
			}
			set
			{
				this.displayFieldField = value;
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
		public string IDField
		{
			get
			{
				return this.iDFieldField;
			}
			set
			{
				this.iDFieldField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasSubtype
		{
			get
			{
				return this.hasSubtypeField;
			}
			set
			{
				this.hasSubtypeField = value;
			}
		}

		[XmlIgnore]
		public bool HasSubtypeSpecified
		{
			get
			{
				return this.hasSubtypeFieldSpecified;
			}
			set
			{
				this.hasSubtypeFieldSpecified = value;
			}
		}

		public StandaloneTableInfo()
		{
			this.timeIntervalField = 0.0;
		}
	}
}
