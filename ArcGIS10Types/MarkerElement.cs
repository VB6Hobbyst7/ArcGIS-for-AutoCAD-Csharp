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
	public class MarkerElement : GraphicElement
	{
		private string nameField;

		private string typeField;

		private bool autoTransformField;

		private bool autoTransformFieldSpecified;

		private double referenceScaleField;

		private bool referenceScaleFieldSpecified;

		private Symbol symbolField;

		private Point pointField;

		private bool lockedField;

		private bool lockedFieldSpecified;

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
		public Point Point
		{
			get
			{
				return this.pointField;
			}
			set
			{
				this.pointField = value;
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
	}
}
