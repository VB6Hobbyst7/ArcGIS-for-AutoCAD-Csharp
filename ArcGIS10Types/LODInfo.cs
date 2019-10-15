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
	public class LODInfo
	{
		private int levelIDField;

		private double scaleField;

		private double resolutionField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int LevelID
		{
			get
			{
				return this.levelIDField;
			}
			set
			{
				this.levelIDField = value;
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
		public double Resolution
		{
			get
			{
				return this.resolutionField;
			}
			set
			{
				this.resolutionField = value;
			}
		}
	}
}
