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
	public class RPCXform : GeodataXform
	{
		private SpatialReference spatialReferenceField;

		private double constantZField;

		private double zFactorField;

		private double zOffsetField;

		private bool correctGeoidField;

		private bool requireDEMField;

		private double[] rPCField;

		private GeodataXform forwardXformField;

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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ConstantZ
		{
			get
			{
				return this.constantZField;
			}
			set
			{
				this.constantZField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ZFactor
		{
			get
			{
				return this.zFactorField;
			}
			set
			{
				this.zFactorField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ZOffset
		{
			get
			{
				return this.zOffsetField;
			}
			set
			{
				this.zOffsetField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool CorrectGeoid
		{
			get
			{
				return this.correctGeoidField;
			}
			set
			{
				this.correctGeoidField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool RequireDEM
		{
			get
			{
				return this.requireDEMField;
			}
			set
			{
				this.requireDEMField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] RPC
		{
			get
			{
				return this.rPCField;
			}
			set
			{
				this.rPCField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public GeodataXform ForwardXform
		{
			get
			{
				return this.forwardXformField;
			}
			set
			{
				this.forwardXformField = value;
			}
		}
	}
}
