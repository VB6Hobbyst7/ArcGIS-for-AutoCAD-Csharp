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
	public class MapLayerInfo : MapTableInfo
	{
		private int layerIDField;

		private string nameField;

		private string descriptionField;

		private string layerTypeField;

		private string sourceDescriptionField;

		private bool hasLabelsField;

		private bool canSelectField;

		private bool canScaleSymbolsField;

		private double minScaleField;

		private double maxScaleField;

		private Envelope extentField;

		private bool hasHyperlinksField;

		private bool hasAttributesField;

		private bool canIdentifyField;

		private bool canFindField;

		private bool isFeatureLayerField;

		private Fields fieldsField;

		private string displayFieldField;

		private string iDFieldField;

		private bool isCompositeField;

		private int[] subLayerIDsField;

		private int parentLayerIDField;

		private string[] fieldAliasesField;

		private string copyrightTextField;

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

		private esriServerHTMLPopupType hTMLPopupTypeField;

		private bool hTMLPopupTypeFieldSpecified;

		private bool hasLayerDrawingDescriptionField;

		private bool hasLayerDrawingDescriptionFieldSpecified;

		private bool hasSubtypeField;

		private bool hasSubtypeFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int LayerID
		{
			get
			{
				return this.layerIDField;
			}
			set
			{
				this.layerIDField = value;
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
		public string LayerType
		{
			get
			{
				return this.layerTypeField;
			}
			set
			{
				this.layerTypeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string SourceDescription
		{
			get
			{
				return this.sourceDescriptionField;
			}
			set
			{
				this.sourceDescriptionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasLabels
		{
			get
			{
				return this.hasLabelsField;
			}
			set
			{
				this.hasLabelsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool CanSelect
		{
			get
			{
				return this.canSelectField;
			}
			set
			{
				this.canSelectField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool CanScaleSymbols
		{
			get
			{
				return this.canScaleSymbolsField;
			}
			set
			{
				this.canScaleSymbolsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MinScale
		{
			get
			{
				return this.minScaleField;
			}
			set
			{
				this.minScaleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MaxScale
		{
			get
			{
				return this.maxScaleField;
			}
			set
			{
				this.maxScaleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Envelope Extent
		{
			get
			{
				return this.extentField;
			}
			set
			{
				this.extentField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasHyperlinks
		{
			get
			{
				return this.hasHyperlinksField;
			}
			set
			{
				this.hasHyperlinksField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasAttributes
		{
			get
			{
				return this.hasAttributesField;
			}
			set
			{
				this.hasAttributesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool CanIdentify
		{
			get
			{
				return this.canIdentifyField;
			}
			set
			{
				this.canIdentifyField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool CanFind
		{
			get
			{
				return this.canFindField;
			}
			set
			{
				this.canFindField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IsFeatureLayer
		{
			get
			{
				return this.isFeatureLayerField;
			}
			set
			{
				this.isFeatureLayerField = value;
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
		public bool IsComposite
		{
			get
			{
				return this.isCompositeField;
			}
			set
			{
				this.isCompositeField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] SubLayerIDs
		{
			get
			{
				return this.subLayerIDsField;
			}
			set
			{
				this.subLayerIDsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ParentLayerID
		{
			get
			{
				return this.parentLayerIDField;
			}
			set
			{
				this.parentLayerIDField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public string[] FieldAliases
		{
			get
			{
				return this.fieldAliasesField;
			}
			set
			{
				this.fieldAliasesField = value;
			}
		}

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

		[XmlIgnore]
		public bool HTMLPopupTypeSpecified
		{
			get
			{
				return this.hTMLPopupTypeFieldSpecified;
			}
			set
			{
				this.hTMLPopupTypeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasLayerDrawingDescription
		{
			get
			{
				return this.hasLayerDrawingDescriptionField;
			}
			set
			{
				this.hasLayerDrawingDescriptionField = value;
			}
		}

		[XmlIgnore]
		public bool HasLayerDrawingDescriptionSpecified
		{
			get
			{
				return this.hasLayerDrawingDescriptionFieldSpecified;
			}
			set
			{
				this.hasLayerDrawingDescriptionFieldSpecified = value;
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

		public MapLayerInfo()
		{
			this.timeIntervalField = 0.0;
		}
	}
}
