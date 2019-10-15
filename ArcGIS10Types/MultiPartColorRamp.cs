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
	public class MultiPartColorRamp : ColorRamp
	{
		private int numColorRampsField;

		private bool numColorRampsFieldSpecified;

		private ColorRamp[] colorRampsField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int NumColorRamps
		{
			get
			{
				return this.numColorRampsField;
			}
			set
			{
				this.numColorRampsField = value;
			}
		}

		[XmlIgnore]
		public bool NumColorRampsSpecified
		{
			get
			{
				return this.numColorRampsFieldSpecified;
			}
			set
			{
				this.numColorRampsFieldSpecified = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public ColorRamp[] ColorRamps
		{
			get
			{
				return this.colorRampsField;
			}
			set
			{
				this.colorRampsField = value;
			}
		}
	}
}
