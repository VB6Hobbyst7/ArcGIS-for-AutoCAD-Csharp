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
	public class PointN : Point
	{
		private double xField;

		private double yField;

		private double mField;

		private bool mFieldSpecified;

		private double zField;

		private bool zFieldSpecified;

		private int idField;

		private bool idFieldSpecified;

		private SpatialReference spatialReferenceField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double X
		{
			get
			{
				return this.xField;
			}
			set
			{
				this.xField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Y
		{
			get
			{
				return this.yField;
			}
			set
			{
				this.yField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double M
		{
			get
			{
				return this.mField;
			}
			set
			{
				this.mField = value;
			}
		}

		[XmlIgnore]
		public bool MSpecified
		{
			get
			{
				return this.mFieldSpecified;
			}
			set
			{
				this.mFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double Z
		{
			get
			{
				return this.zField;
			}
			set
			{
				this.zField = value;
			}
		}

		[XmlIgnore]
		public bool ZSpecified
		{
			get
			{
				return this.zFieldSpecified;
			}
			set
			{
				this.zFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		[XmlIgnore]
		public bool IDSpecified
		{
			get
			{
				return this.idFieldSpecified;
			}
			set
			{
				this.idFieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public SpatialReference SpatialReference
		{
			get
			{
				return this.spatialReferenceField;
			}
			set
			{
				this.spatialReferenceField = value;
			}
		}
	}
}
