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
	public class ServerSymbolOutputOptions
	{
		private esriServerPictureOutputType pictureOutputTypeField;

		private bool pictureOutputTypeFieldSpecified;

		private bool convertLabelExpressionsField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriServerPictureOutputType PictureOutputType
		{
			get
			{
				return this.pictureOutputTypeField;
			}
			set
			{
				this.pictureOutputTypeField = value;
			}
		}

		[XmlIgnore]
		public bool PictureOutputTypeSpecified
		{
			get
			{
				return this.pictureOutputTypeFieldSpecified;
			}
			set
			{
				this.pictureOutputTypeFieldSpecified = value;
			}
		}

		[DefaultValue(true), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool ConvertLabelExpressions
		{
			get
			{
				return this.convertLabelExpressionsField;
			}
			set
			{
				this.convertLabelExpressionsField = value;
			}
		}

		public ServerSymbolOutputOptions()
		{
			this.convertLabelExpressionsField = true;
		}
	}
}
