using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(PictureMarkerSymbol)), XmlInclude(typeof(CharacterMarkerSymbol)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public abstract class CartographicMarkerSymbol : MarkerSymbol
	{
		private double xScaleField;

		private double yScaleField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double XScale
		{
			get
			{
				return this.xScaleField;
			}
			set
			{
				this.xScaleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double YScale
		{
			get
			{
				return this.yScaleField;
			}
			set
			{
				this.yScaleField = value;
			}
		}

		public CartographicMarkerSymbol()
		{
			this.xScaleField = 1.0;
			this.yScaleField = 1.0;
		}
	}
}
