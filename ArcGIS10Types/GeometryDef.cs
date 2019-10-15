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
	public class GeometryDef
	{
		private int avgNumPointsField;

		private esriGeometryType geometryTypeField;

		private bool hasMField;

		private bool hasZField;

		private SpatialReference spatialReferenceField;

		private double gridSize0Field;

		private bool gridSize0FieldSpecified;

		private double gridSize1Field;

		private bool gridSize1FieldSpecified;

		private double gridSize2Field;

		private bool gridSize2FieldSpecified;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int AvgNumPoints
		{
			get
			{
				return this.avgNumPointsField;
			}
			set
			{
				this.avgNumPointsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriGeometryType GeometryType
		{
			get
			{
				return this.geometryTypeField;
			}
			set
			{
				this.geometryTypeField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasM
		{
			get
			{
				return this.hasMField;
			}
			set
			{
				this.hasMField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasZ
		{
			get
			{
				return this.hasZField;
			}
			set
			{
				this.hasZField = value;
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

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double GridSize0
		{
			get
			{
				return this.gridSize0Field;
			}
			set
			{
				this.gridSize0Field = value;
			}
		}

		[XmlIgnore]
		public bool GridSize0Specified
		{
			get
			{
				return this.gridSize0FieldSpecified;
			}
			set
			{
				this.gridSize0FieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double GridSize1
		{
			get
			{
				return this.gridSize1Field;
			}
			set
			{
				this.gridSize1Field = value;
			}
		}

		[XmlIgnore]
		public bool GridSize1Specified
		{
			get
			{
				return this.gridSize1FieldSpecified;
			}
			set
			{
				this.gridSize1FieldSpecified = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double GridSize2
		{
			get
			{
				return this.gridSize2Field;
			}
			set
			{
				this.gridSize2Field = value;
			}
		}

		[XmlIgnore]
		public bool GridSize2Specified
		{
			get
			{
				return this.gridSize2FieldSpecified;
			}
			set
			{
				this.gridSize2FieldSpecified = value;
			}
		}
	}
}
