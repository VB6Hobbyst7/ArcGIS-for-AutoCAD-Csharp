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
	public class ImageServerDownloadResult
	{
		private int[] rasterIDsField;

		private string uRIField;

		private double fileSizeField;

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] RasterIDs
		{
			get
			{
				return this.rasterIDsField;
			}
			set
			{
				this.rasterIDsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string URI
		{
			get
			{
				return this.uRIField;
			}
			set
			{
				this.uRIField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double FileSize
		{
			get
			{
				return this.fileSizeField;
			}
			set
			{
				this.fileSizeField = value;
			}
		}
	}
}
