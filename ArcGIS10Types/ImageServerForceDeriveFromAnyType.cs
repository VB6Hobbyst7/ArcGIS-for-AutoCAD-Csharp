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
	public class ImageServerForceDeriveFromAnyType
	{
		private RasterFunctionTemplate rasterFunctionTemplateField;

		private RasterFunctionVariable rasterFunctionVariableField;

		private RasterStatistics rasterStatisticsField;

		private RasterHistogram rasterHistogramField;

		private RasterColormap rasterColormapField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public RasterFunctionTemplate RasterFunctionTemplate
		{
			get
			{
				return this.rasterFunctionTemplateField;
			}
			set
			{
				this.rasterFunctionTemplateField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public RasterFunctionVariable RasterFunctionVariable
		{
			get
			{
				return this.rasterFunctionVariableField;
			}
			set
			{
				this.rasterFunctionVariableField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public RasterStatistics RasterStatistics
		{
			get
			{
				return this.rasterStatisticsField;
			}
			set
			{
				this.rasterStatisticsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public RasterHistogram RasterHistogram
		{
			get
			{
				return this.rasterHistogramField;
			}
			set
			{
				this.rasterHistogramField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public RasterColormap RasterColormap
		{
			get
			{
				return this.rasterColormapField;
			}
			set
			{
				this.rasterColormapField = value;
			}
		}
	}
}
