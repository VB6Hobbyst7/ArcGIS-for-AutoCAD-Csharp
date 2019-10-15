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
	public class MapServerInfo
	{
		private string nameField;

		private string descriptionField;

		private Envelope fullExtentField;

		private Envelope extentField;

		private SpatialReference spatialReferenceField;

		private MapLayerInfo[] mapLayerInfosField;

		private Color backgroundColorField;

		private MapServerBookmark[] bookmarksField;

		private MapDescription defaultMapDescriptionField;

		private esriUnits unitsField;

		private esriImageReturnType supportedImageReturnTypesField;

		private FillSymbol backgroundSymbolField;

		private string copyrightTextField;

		private StandaloneTableInfo[] standaloneTableInfosField;

		private StandaloneTableDescription[] standaloneTableDescriptionsField;

		private TimeExtent fullTimeExtentField;

		private double defaultTimeStepIntervalField;

		private bool defaultTimeStepIntervalFieldSpecified;

		private esriTimeUnits defaultTimeStepIntervalUnitsField;

		private bool defaultTimeStepIntervalUnitsFieldSpecified;

		private double defaultTimeWindowField;

		private bool defaultTimeWindowFieldSpecified;

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
		public Envelope FullExtent
		{
			get
			{
				return this.fullExtentField;
			}
			set
			{
				this.fullExtentField = value;
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

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public MapLayerInfo[] MapLayerInfos
		{
			get
			{
				return this.mapLayerInfosField;
			}
			set
			{
				this.mapLayerInfosField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Color BackgroundColor
		{
			get
			{
				return this.backgroundColorField;
			}
			set
			{
				this.backgroundColorField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public MapServerBookmark[] Bookmarks
		{
			get
			{
				return this.bookmarksField;
			}
			set
			{
				this.bookmarksField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public MapDescription DefaultMapDescription
		{
			get
			{
				return this.defaultMapDescriptionField;
			}
			set
			{
				this.defaultMapDescriptionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriUnits Units
		{
			get
			{
				return this.unitsField;
			}
			set
			{
				this.unitsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriImageReturnType SupportedImageReturnTypes
		{
			get
			{
				return this.supportedImageReturnTypesField;
			}
			set
			{
				this.supportedImageReturnTypesField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public FillSymbol BackgroundSymbol
		{
			get
			{
				return this.backgroundSymbolField;
			}
			set
			{
				this.backgroundSymbolField = value;
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
		public StandaloneTableInfo[] StandaloneTableInfos
		{
			get
			{
				return this.standaloneTableInfosField;
			}
			set
			{
				this.standaloneTableInfosField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public StandaloneTableDescription[] StandaloneTableDescriptions
		{
			get
			{
				return this.standaloneTableDescriptionsField;
			}
			set
			{
				this.standaloneTableDescriptionsField = value;
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double DefaultTimeStepInterval
		{
			get
			{
				return this.defaultTimeStepIntervalField;
			}
			set
			{
				this.defaultTimeStepIntervalField = value;
			}
		}

		[XmlIgnore]
		public bool DefaultTimeStepIntervalSpecified
		{
			get
			{
				return this.defaultTimeStepIntervalFieldSpecified;
			}
			set
			{
				this.defaultTimeStepIntervalFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriTimeUnits DefaultTimeStepIntervalUnits
		{
			get
			{
				return this.defaultTimeStepIntervalUnitsField;
			}
			set
			{
				this.defaultTimeStepIntervalUnitsField = value;
			}
		}

		[XmlIgnore]
		public bool DefaultTimeStepIntervalUnitsSpecified
		{
			get
			{
				return this.defaultTimeStepIntervalUnitsFieldSpecified;
			}
			set
			{
				this.defaultTimeStepIntervalUnitsFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double DefaultTimeWindow
		{
			get
			{
				return this.defaultTimeWindowField;
			}
			set
			{
				this.defaultTimeWindowField = value;
			}
		}

		[XmlIgnore]
		public bool DefaultTimeWindowSpecified
		{
			get
			{
				return this.defaultTimeWindowFieldSpecified;
			}
			set
			{
				this.defaultTimeWindowFieldSpecified = value;
			}
		}
	}
}
