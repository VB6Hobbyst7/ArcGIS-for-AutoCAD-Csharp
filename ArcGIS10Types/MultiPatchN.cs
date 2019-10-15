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
	public class MultiPatchN : MultiPatch
	{
		private bool hasIDField;

		private bool hasZField;

		private bool hasMField;

		private Envelope extentField;

		private Geometry[] surfacePatchArrayField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasID
		{
			get
			{
				return this.hasIDField;
			}
			set
			{
				this.hasIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasZ
		{
			get
			{
				return this.hasZField;
			}
			set
			{
				this.hasZField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasM
		{
			get
			{
				return this.hasMField;
			}
			set
			{
				this.hasMField = value;
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

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("SurfacePatch", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Geometry[] SurfacePatchArray
		{
			get
			{
				return this.surfacePatchArrayField;
			}
			set
			{
				this.surfacePatchArrayField = value;
			}
		}
	}
}
