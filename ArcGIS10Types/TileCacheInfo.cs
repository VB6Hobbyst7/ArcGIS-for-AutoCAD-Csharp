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
	public class TileCacheInfo
	{
		private SpatialReference spatialReferenceField;

		private Point tileOriginField;

		private int tileColsField;

		private int tileRowsField;

		private int dPIField;

		private LODInfo[] lODInfosField;

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
		public Point TileOrigin
		{
			get
			{
				return this.tileOriginField;
			}
			set
			{
				this.tileOriginField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int TileCols
		{
			get
			{
				return this.tileColsField;
			}
			set
			{
				this.tileColsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int TileRows
		{
			get
			{
				return this.tileRowsField;
			}
			set
			{
				this.tileRowsField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int DPI
		{
			get
			{
				return this.dPIField;
			}
			set
			{
				this.dPIField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public LODInfo[] LODInfos
		{
			get
			{
				return this.lODInfosField;
			}
			set
			{
				this.lODInfosField = value;
			}
		}
	}
}
