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
	public class LayerCacheInfo
	{
		private int layerIDField;

		private bool hasCacheField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int LayerID
		{
			get
			{
				return this.layerIDField;
			}
			set
			{
				this.layerIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasCache
		{
			get
			{
				return this.hasCacheField;
			}
			set
			{
				this.hasCacheField = value;
			}
		}
	}
}
