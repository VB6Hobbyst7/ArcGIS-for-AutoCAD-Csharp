using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(GraphicFeatureLayer)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public class DataObjectTable
	{
		private string copyrightTextField;

		private string descriptionField;

		private string displayPropNameField;

		private string globalIDPropNameField;

		private esriServerHTMLPopupType hTMLPopupTypeField;

		private int idField;

		private string nameField;

		private string oIDPropNameField;

		private PropertyInfo[] propertyInfosField;

		private RelateInfo[] relationsField;

		private TemplateInfo[] templatesField;

		private string typeIDPropNameField;

		private DataObjectType[] typesField;

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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string CopyrightText
		{
			get
			{
				return this.copyrightTextField;
			}
			set
			{
				this.copyrightTextField = value;
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
		public string DisplayPropName
		{
			get
			{
				return this.displayPropNameField;
			}
			set
			{
				this.displayPropNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string GlobalIDPropName
		{
			get
			{
				return this.globalIDPropNameField;
			}
			set
			{
				this.globalIDPropNameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriServerHTMLPopupType HTMLPopupType
		{
			get
			{
				return this.hTMLPopupTypeField;
			}
			set
			{
				this.hTMLPopupTypeField = value;
			}
		}

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
		public string OIDPropName
		{
			get
			{
				return this.oIDPropNameField;
			}
			set
			{
				this.oIDPropNameField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public PropertyInfo[] PropertyInfos
		{
			get
			{
				return this.propertyInfosField;
			}
			set
			{
				this.propertyInfosField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public RelateInfo[] Relations
		{
			get
			{
				return this.relationsField;
			}
			set
			{
				this.relationsField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public TemplateInfo[] Templates
		{
			get
			{
				return this.templatesField;
			}
			set
			{
				this.templatesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string TypeIDPropName
		{
			get
			{
				return this.typeIDPropNameField;
			}
			set
			{
				this.typeIDPropNameField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public DataObjectType[] Types
		{
			get
			{
				return this.typesField;
			}
			set
			{
				this.typesField = value;
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

		public DataObjectTable()
		{
			this.timeIntervalField = 0.0;
		}
	}
}
