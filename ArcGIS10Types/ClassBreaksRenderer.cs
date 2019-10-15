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
	public class ClassBreaksRenderer : FeatureRenderer
	{
		private string fieldField;

		private double minimumValueField;

		private ClassBreakInfo[] classBreakInfosField;

		private FillSymbol backgroundSymbolField;

		private string normalizationFieldField;

		private esriNormalizationType normalizationTypeField;

		private bool normalizationTypeFieldSpecified;

		private double normalizationTotalField;

		private bool normalizationTotalFieldSpecified;

		private string rotationFieldField;

		private esriRotationType rotationTypeField;

		private bool rotationTypeFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Field
		{
			get
			{
				return this.fieldField;
			}
			set
			{
				this.fieldField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double MinimumValue
		{
			get
			{
				return this.minimumValueField;
			}
			set
			{
				this.minimumValueField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public ClassBreakInfo[] ClassBreakInfos
		{
			get
			{
				return this.classBreakInfosField;
			}
			set
			{
				this.classBreakInfosField = value;
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
		public string NormalizationField
		{
			get
			{
				return this.normalizationFieldField;
			}
			set
			{
				this.normalizationFieldField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriNormalizationType NormalizationType
		{
			get
			{
				return this.normalizationTypeField;
			}
			set
			{
				this.normalizationTypeField = value;
			}
		}

		[XmlIgnore]
		public bool NormalizationTypeSpecified
		{
			get
			{
				return this.normalizationTypeFieldSpecified;
			}
			set
			{
				this.normalizationTypeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double NormalizationTotal
		{
			get
			{
				return this.normalizationTotalField;
			}
			set
			{
				this.normalizationTotalField = value;
			}
		}

		[XmlIgnore]
		public bool NormalizationTotalSpecified
		{
			get
			{
				return this.normalizationTotalFieldSpecified;
			}
			set
			{
				this.normalizationTotalFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string RotationField
		{
			get
			{
				return this.rotationFieldField;
			}
			set
			{
				this.rotationFieldField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriRotationType RotationType
		{
			get
			{
				return this.rotationTypeField;
			}
			set
			{
				this.rotationTypeField = value;
			}
		}

		[XmlIgnore]
		public bool RotationTypeSpecified
		{
			get
			{
				return this.rotationTypeFieldSpecified;
			}
			set
			{
				this.rotationTypeFieldSpecified = value;
			}
		}
	}
}
