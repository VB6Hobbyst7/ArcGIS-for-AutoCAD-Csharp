using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(MaskFunctionArguments)), XmlInclude(typeof(HillshadeFunctionArguments)), XmlInclude(typeof(GrayscaleFunctionArguments)), XmlInclude(typeof(TrendFunctionArguments)), XmlInclude(typeof(StretchFunctionArguments)), XmlInclude(typeof(StatisticsFunctionArguments)), XmlInclude(typeof(SpectralConversionFunctionArguments)), XmlInclude(typeof(SlopeFunctionArguments)), XmlInclude(typeof(ShadedReliefFunctionArguments)), XmlInclude(typeof(RasterInfoFunctionArguments)), XmlInclude(typeof(PansharpeningFunctionArguments)), XmlInclude(typeof(NDVIFunctionArguments)), XmlInclude(typeof(GeometricFunctionArguments)), XmlInclude(typeof(ExtractBandFunctionArguments)), XmlInclude(typeof(ConvolutionFunctionArguments)), XmlInclude(typeof(ConstantFunctionArguments)), XmlInclude(typeof(ColorspaceConversionFunctionArguments)), XmlInclude(typeof(ColormapFunctionArguments)), XmlInclude(typeof(ClipFunctionArguments)), XmlInclude(typeof(ArithmeticFunctionArguments)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public class RasterFunctionArguments
	{
		private string[] namesField;

		private object[] valuesField;

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public string[] Names
		{
			get
			{
				return this.namesField;
			}
			set
			{
				this.namesField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("AnyType", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public object[] Values
		{
			get
			{
				return this.valuesField;
			}
			set
			{
				this.valuesField = value;
			}
		}
	}
}
