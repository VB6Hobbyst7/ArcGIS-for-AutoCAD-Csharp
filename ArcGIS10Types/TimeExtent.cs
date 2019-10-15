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
	public class TimeExtent : TimeValue
	{
		private DateTime startTimeField;

		private bool startTimeFieldSpecified;

		private DateTime endTimeField;

		private bool endTimeFieldSpecified;

		private bool emptyField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public DateTime StartTime
		{
			get
			{
				return this.startTimeField;
			}
			set
			{
				this.startTimeField = value;
			}
		}

		[XmlIgnore]
		public bool StartTimeSpecified
		{
			get
			{
				return this.startTimeFieldSpecified;
			}
			set
			{
				this.startTimeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public DateTime EndTime
		{
			get
			{
				return this.endTimeField;
			}
			set
			{
				this.endTimeField = value;
			}
		}

		[XmlIgnore]
		public bool EndTimeSpecified
		{
			get
			{
				return this.endTimeFieldSpecified;
			}
			set
			{
				this.endTimeFieldSpecified = value;
			}
		}

		[DefaultValue(false), XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool Empty
		{
			get
			{
				return this.emptyField;
			}
			set
			{
				this.emptyField = value;
			}
		}

		public TimeExtent()
		{
			this.emptyField = false;
		}
	}
}
