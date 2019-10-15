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
	public class IdentityXform : GeodataXform
	{
		private SpatialReference spatialReferenceField;

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
	}
}
