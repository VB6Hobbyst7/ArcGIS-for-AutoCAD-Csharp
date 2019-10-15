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
	public class PolygonLabelPlacementDescription : LabelPlacementDescription
	{
		private esriServerPolygonLabelPlacementType typeField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriServerPolygonLabelPlacementType Type
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
	}
}
