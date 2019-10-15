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
	public class GeoTransformation
	{
		private string wKTField;

		private int wKIDField;

		private bool wKIDFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string WKT
		{
			get
			{
				return this.wKTField;
			}
			set
			{
				this.wKTField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int WKID
		{
			get
			{
				return this.wKIDField;
			}
			set
			{
				this.wKIDField = value;
			}
		}

		[XmlIgnore]
		public bool WKIDSpecified
		{
			get
			{
				return this.wKIDFieldSpecified;
			}
			set
			{
				this.wKIDFieldSpecified = value;
			}
		}
	}
}
