using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(LinePatch)), XmlInclude(typeof(AreaPatch)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public class Patch
	{
		private string nameField;

		private bool preserveAspectRatioField;

		private bool preserveAspectRatioFieldSpecified;

		private Geometry geometryField;

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
		public bool PreserveAspectRatio
		{
			get
			{
				return this.preserveAspectRatioField;
			}
			set
			{
				this.preserveAspectRatioField = value;
			}
		}

		[XmlIgnore]
		public bool PreserveAspectRatioSpecified
		{
			get
			{
				return this.preserveAspectRatioFieldSpecified;
			}
			set
			{
				this.preserveAspectRatioFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public Geometry Geometry
		{
			get
			{
				return this.geometryField;
			}
			set
			{
				this.geometryField = value;
			}
		}
	}
}
