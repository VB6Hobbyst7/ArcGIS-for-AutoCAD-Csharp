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
	public class PolylineN : Polyline
	{
		private bool hasIDField;

		private bool hasZField;

		private bool hasMField;

		private Envelope extentField;

		private Path[] pathArrayField;

		private SpatialReference spatialReferenceField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public bool HasID
		{
			get
			{
				return this.hasIDField;
			}
			set
			{
				this.hasIDField = value;
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
		public Envelope Extent
		{
			get
			{
				return this.extentField;
			}
			set
			{
				this.extentField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public Path[] PathArray
		{
			get
			{
				return this.pathArrayField;
			}
			set
			{
				this.pathArrayField = value;
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
