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
	public class CacheDescriptionInfo
	{
		private TileCacheInfo tileCacheInfoField;

		private TileImageInfo tileImageInfoField;

		private ArrayOfLayerCacheInfo layerCacheInfosField;

		private CacheControlInfo cacheControlInfoField;

		private esriCachedMapServiceType serviceTypeField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TileCacheInfo TileCacheInfo
		{
			get
			{
				return this.tileCacheInfoField;
			}
			set
			{
				this.tileCacheInfoField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TileImageInfo TileImageInfo
		{
			get
			{
				return this.tileImageInfoField;
			}
			set
			{
				this.tileImageInfoField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public ArrayOfLayerCacheInfo LayerCacheInfos
		{
			get
			{
				return this.layerCacheInfosField;
			}
			set
			{
				this.layerCacheInfosField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public CacheControlInfo CacheControlInfo
		{
			get
			{
				return this.cacheControlInfoField;
			}
			set
			{
				this.cacheControlInfoField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriCachedMapServiceType ServiceType
		{
			get
			{
				return this.serviceTypeField;
			}
			set
			{
				this.serviceTypeField = value;
			}
		}
	}
}
