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
	public class TileImageInfo
	{
		private string cacheTileFormatField;

		private int compressionQualityField;

		private string antialiasingField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string CacheTileFormat
		{
			get
			{
				return this.cacheTileFormatField;
			}
			set
			{
				this.cacheTileFormatField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int CompressionQuality
		{
			get
			{
				return this.compressionQualityField;
			}
			set
			{
				this.compressionQualityField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Antialiasing
		{
			get
			{
				return this.antialiasingField;
			}
			set
			{
				this.antialiasingField = value;
			}
		}
	}
}
