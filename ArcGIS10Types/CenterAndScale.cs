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
	public class CenterAndScale : MapArea
	{
		private Point centerField;

		private double scaleField;

		private double dPIField;

		private bool dPIFieldSpecified;

		private int devBottomField;

		private bool devBottomFieldSpecified;

		private int devLeftField;

		private bool devLeftFieldSpecified;

		private int devTopField;

		private bool devTopFieldSpecified;

		private int devRightField;

		private bool devRightFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Point Center
		{
			get
			{
				return this.centerField;
			}
			set
			{
				this.centerField = value;
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double DPI
		{
			get
			{
				return this.dPIField;
			}
			set
			{
				this.dPIField = value;
			}
		}

		[XmlIgnore]
		public bool DPISpecified
		{
			get
			{
				return this.dPIFieldSpecified;
			}
			set
			{
				this.dPIFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int DevBottom
		{
			get
			{
				return this.devBottomField;
			}
			set
			{
				this.devBottomField = value;
			}
		}

		[XmlIgnore]
		public bool DevBottomSpecified
		{
			get
			{
				return this.devBottomFieldSpecified;
			}
			set
			{
				this.devBottomFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int DevLeft
		{
			get
			{
				return this.devLeftField;
			}
			set
			{
				this.devLeftField = value;
			}
		}

		[XmlIgnore]
		public bool DevLeftSpecified
		{
			get
			{
				return this.devLeftFieldSpecified;
			}
			set
			{
				this.devLeftFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int DevTop
		{
			get
			{
				return this.devTopField;
			}
			set
			{
				this.devTopField = value;
			}
		}

		[XmlIgnore]
		public bool DevTopSpecified
		{
			get
			{
				return this.devTopFieldSpecified;
			}
			set
			{
				this.devTopFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int DevRight
		{
			get
			{
				return this.devRightField;
			}
			set
			{
				this.devRightField = value;
			}
		}

		[XmlIgnore]
		public bool DevRightSpecified
		{
			get
			{
				return this.devRightFieldSpecified;
			}
			set
			{
				this.devRightFieldSpecified = value;
			}
		}
	}
}
