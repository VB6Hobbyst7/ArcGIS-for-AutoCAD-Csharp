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
	public class GroupElement : Element
	{
		private string nameField;

		private string typeField;

		private bool autoTransformField;

		private bool autoTransformFieldSpecified;

		private double referenceScaleField;

		private bool referenceScaleFieldSpecified;

		private GraphicElement[] elementsField;

		private Geometry rectangleField;

		private bool lockedField;

		private bool lockedFieldSpecified;

		private bool fixedAspectRatioField;

		private bool fixedAspectRatioFieldSpecified;

		private Border borderField;

		private Background backgroundField;

		private bool draftModeField;

		private bool draftModeFieldSpecified;

		private Shadow shadowField;

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
		public string Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool AutoTransform
		{
			get
			{
				return this.autoTransformField;
			}
			set
			{
				this.autoTransformField = value;
			}
		}

		[XmlIgnore]
		public bool AutoTransformSpecified
		{
			get
			{
				return this.autoTransformFieldSpecified;
			}
			set
			{
				this.autoTransformFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ReferenceScale
		{
			get
			{
				return this.referenceScaleField;
			}
			set
			{
				this.referenceScaleField = value;
			}
		}

		[XmlIgnore]
		public bool ReferenceScaleSpecified
		{
			get
			{
				return this.referenceScaleFieldSpecified;
			}
			set
			{
				this.referenceScaleFieldSpecified = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public GraphicElement[] Elements
		{
			get
			{
				return this.elementsField;
			}
			set
			{
				this.elementsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Geometry Rectangle
		{
			get
			{
				return this.rectangleField;
			}
			set
			{
				this.rectangleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Locked
		{
			get
			{
				return this.lockedField;
			}
			set
			{
				this.lockedField = value;
			}
		}

		[XmlIgnore]
		public bool LockedSpecified
		{
			get
			{
				return this.lockedFieldSpecified;
			}
			set
			{
				this.lockedFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool FixedAspectRatio
		{
			get
			{
				return this.fixedAspectRatioField;
			}
			set
			{
				this.fixedAspectRatioField = value;
			}
		}

		[XmlIgnore]
		public bool FixedAspectRatioSpecified
		{
			get
			{
				return this.fixedAspectRatioFieldSpecified;
			}
			set
			{
				this.fixedAspectRatioFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Border Border
		{
			get
			{
				return this.borderField;
			}
			set
			{
				this.borderField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Background Background
		{
			get
			{
				return this.backgroundField;
			}
			set
			{
				this.backgroundField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool DraftMode
		{
			get
			{
				return this.draftModeField;
			}
			set
			{
				this.draftModeField = value;
			}
		}

		[XmlIgnore]
		public bool DraftModeSpecified
		{
			get
			{
				return this.draftModeFieldSpecified;
			}
			set
			{
				this.draftModeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Shadow Shadow
		{
			get
			{
				return this.shadowField;
			}
			set
			{
				this.shadowField = value;
			}
		}
	}
}
