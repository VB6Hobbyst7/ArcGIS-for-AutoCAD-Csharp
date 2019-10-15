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
	public class ParagraphTextElement : GraphicElement
	{
		private string nameField;

		private string typeField;

		private bool autoTransformField;

		private bool autoTransformFieldSpecified;

		private double referenceScaleField;

		private bool referenceScaleFieldSpecified;

		private string textField;

		private double scaleField;

		private bool scaleFieldSpecified;

		private Symbol symbolField;

		private Geometry textGeometryField;

		private bool lockedField;

		private bool lockedFieldSpecified;

		private Border frameBorderField;

		private Background frameBackgroundField;

		private Shadow frameShadowField;

		private double columnGapField;

		private bool columnGapFieldSpecified;

		private int columnCountField;

		private bool columnCountFieldSpecified;

		private double marginField;

		private bool marginFieldSpecified;

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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Text
		{
			get
			{
				return this.textField;
			}
			set
			{
				this.textField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Scale
		{
			get
			{
				return this.scaleField;
			}
			set
			{
				this.scaleField = value;
			}
		}

		[XmlIgnore]
		public bool ScaleSpecified
		{
			get
			{
				return this.scaleFieldSpecified;
			}
			set
			{
				this.scaleFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Symbol Symbol
		{
			get
			{
				return this.symbolField;
			}
			set
			{
				this.symbolField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Geometry TextGeometry
		{
			get
			{
				return this.textGeometryField;
			}
			set
			{
				this.textGeometryField = value;
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
		public Border FrameBorder
		{
			get
			{
				return this.frameBorderField;
			}
			set
			{
				this.frameBorderField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Background FrameBackground
		{
			get
			{
				return this.frameBackgroundField;
			}
			set
			{
				this.frameBackgroundField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Shadow FrameShadow
		{
			get
			{
				return this.frameShadowField;
			}
			set
			{
				this.frameShadowField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ColumnGap
		{
			get
			{
				return this.columnGapField;
			}
			set
			{
				this.columnGapField = value;
			}
		}

		[XmlIgnore]
		public bool ColumnGapSpecified
		{
			get
			{
				return this.columnGapFieldSpecified;
			}
			set
			{
				this.columnGapFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ColumnCount
		{
			get
			{
				return this.columnCountField;
			}
			set
			{
				this.columnCountField = value;
			}
		}

		[XmlIgnore]
		public bool ColumnCountSpecified
		{
			get
			{
				return this.columnCountFieldSpecified;
			}
			set
			{
				this.columnCountFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Margin
		{
			get
			{
				return this.marginField;
			}
			set
			{
				this.marginField = value;
			}
		}

		[XmlIgnore]
		public bool MarginSpecified
		{
			get
			{
				return this.marginFieldSpecified;
			}
			set
			{
				this.marginFieldSpecified = value;
			}
		}
	}
}
