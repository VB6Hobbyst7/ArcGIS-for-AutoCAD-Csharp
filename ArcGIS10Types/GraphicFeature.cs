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
	public class GraphicFeature : DataObject
	{
		private Geometry geometryField;

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
