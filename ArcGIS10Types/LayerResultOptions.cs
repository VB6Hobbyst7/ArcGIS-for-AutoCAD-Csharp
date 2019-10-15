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
	public class LayerResultOptions
	{
		private bool includeGeometryField;

		private GeometryResultOptions geometryResultOptionsField;

		private bool returnFieldNamesInResultsField;

		private bool formatValuesInResultsField;

		[DefaultValue(true), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool IncludeGeometry
		{
			get
			{
				return this.includeGeometryField;
			}
			set
			{
				this.includeGeometryField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public GeometryResultOptions GeometryResultOptions
		{
			get
			{
				return this.geometryResultOptionsField;
			}
			set
			{
				this.geometryResultOptionsField = value;
			}
		}

		[DefaultValue(false), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool ReturnFieldNamesInResults
		{
			get
			{
				return this.returnFieldNamesInResultsField;
			}
			set
			{
				this.returnFieldNamesInResultsField = value;
			}
		}

		[DefaultValue(true), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool FormatValuesInResults
		{
			get
			{
				return this.formatValuesInResultsField;
			}
			set
			{
				this.formatValuesInResultsField = value;
			}
		}

		public LayerResultOptions()
		{
			this.includeGeometryField = true;
			this.returnFieldNamesInResultsField = false;
			this.formatValuesInResultsField = true;
		}
	}
}
