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
	public class RasterStatistics
	{
		private double minField;

		private double maxField;

		private double meanField;

		private bool meanFieldSpecified;

		private double standardDeviationField;

		private bool standardDeviationFieldSpecified;

		private double medianField;

		private bool medianFieldSpecified;

		private double modeField;

		private bool modeFieldSpecified;

		private int skipXField;

		private bool skipXFieldSpecified;

		private int skipYField;

		private bool skipYFieldSpecified;

		private object ignoresField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Min
		{
			get
			{
				return this.minField;
			}
			set
			{
				this.minField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Max
		{
			get
			{
				return this.maxField;
			}
			set
			{
				this.maxField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Mean
		{
			get
			{
				return this.meanField;
			}
			set
			{
				this.meanField = value;
			}
		}

		[XmlIgnore]
		public bool MeanSpecified
		{
			get
			{
				return this.meanFieldSpecified;
			}
			set
			{
				this.meanFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double StandardDeviation
		{
			get
			{
				return this.standardDeviationField;
			}
			set
			{
				this.standardDeviationField = value;
			}
		}

		[XmlIgnore]
		public bool StandardDeviationSpecified
		{
			get
			{
				return this.standardDeviationFieldSpecified;
			}
			set
			{
				this.standardDeviationFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Median
		{
			get
			{
				return this.medianField;
			}
			set
			{
				this.medianField = value;
			}
		}

		[XmlIgnore]
		public bool MedianSpecified
		{
			get
			{
				return this.medianFieldSpecified;
			}
			set
			{
				this.medianFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Mode
		{
			get
			{
				return this.modeField;
			}
			set
			{
				this.modeField = value;
			}
		}

		[XmlIgnore]
		public bool ModeSpecified
		{
			get
			{
				return this.modeFieldSpecified;
			}
			set
			{
				this.modeFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int SkipX
		{
			get
			{
				return this.skipXField;
			}
			set
			{
				this.skipXField = value;
			}
		}

		[XmlIgnore]
		public bool SkipXSpecified
		{
			get
			{
				return this.skipXFieldSpecified;
			}
			set
			{
				this.skipXFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int SkipY
		{
			get
			{
				return this.skipYField;
			}
			set
			{
				this.skipYField = value;
			}
		}

		[XmlIgnore]
		public bool SkipYSpecified
		{
			get
			{
				return this.skipYFieldSpecified;
			}
			set
			{
				this.skipYFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public object Ignores
		{
			get
			{
				return this.ignoresField;
			}
			set
			{
				this.ignoresField = value;
			}
		}
	}
}
