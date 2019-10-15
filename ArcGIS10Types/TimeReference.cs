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
	public class TimeReference
	{
		private string timeZoneNameIDField;

		private bool respectsDaylightSavingTimeField;

		private bool respectsDaylightSavingTimeFieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string TimeZoneNameID
		{
			get
			{
				return this.timeZoneNameIDField;
			}
			set
			{
				this.timeZoneNameIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool RespectsDaylightSavingTime
		{
			get
			{
				return this.respectsDaylightSavingTimeField;
			}
			set
			{
				this.respectsDaylightSavingTimeField = value;
			}
		}

		[XmlIgnore]
		public bool RespectsDaylightSavingTimeSpecified
		{
			get
			{
				return this.respectsDaylightSavingTimeFieldSpecified;
			}
			set
			{
				this.respectsDaylightSavingTimeFieldSpecified = value;
			}
		}
	}
}
