using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ArcGIS10Types
{
	[GeneratedCode("wsdl", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlInclude(typeof(ConvolutionFunction)), XmlInclude(typeof(ExtractBandFunction)), XmlInclude(typeof(GeometricFunction)), XmlInclude(typeof(GrayscaleFunction)), XmlInclude(typeof(HillshadeFunction)), XmlInclude(typeof(TrendFunction)), XmlInclude(typeof(StretchFunction)), XmlInclude(typeof(SpectralConversionFunction)), XmlInclude(typeof(SlopeFunction)), XmlInclude(typeof(ShadedReliefFunction)), XmlInclude(typeof(RasterInfoFunction)), XmlInclude(typeof(PansharpeningFunction)), XmlInclude(typeof(NDVIFunction)), XmlInclude(typeof(MaskFunction)), XmlInclude(typeof(IdentityFunction)), XmlInclude(typeof(RasterFunctionTemplate)), XmlInclude(typeof(StatisticsFunction)), XmlInclude(typeof(ConstantFunction)), XmlInclude(typeof(ComplexFunction)), XmlInclude(typeof(ColorspaceConversionFunction)), XmlInclude(typeof(ColormapToRGBFunction)), XmlInclude(typeof(ColormapFunction)), XmlInclude(typeof(ClipFunction)), XmlInclude(typeof(AspectFunction)), XmlInclude(typeof(ArithmeticFunction)), XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.0")]
	[Serializable]
	public class RasterFunction
	{
		private string nameField;

		private string descriptionField;

		private rstPixelType pixelTypeField;

		private bool pixelTypeFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public rstPixelType PixelType
		{
			get
			{
				return this.pixelTypeField;
			}
			set
			{
				this.pixelTypeField = value;
			}
		}

		[XmlIgnore]
		public bool PixelTypeSpecified
		{
			get
			{
				return this.pixelTypeFieldSpecified;
			}
			set
			{
				this.pixelTypeFieldSpecified = value;
			}
		}
	}
}
