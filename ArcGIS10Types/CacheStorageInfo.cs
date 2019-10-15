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
	public class CacheStorageInfo
	{
		private esriMapCacheStorageFormat storageFormatField;

		private int packetSizeField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public esriMapCacheStorageFormat StorageFormat
		{
			get
			{
				return this.storageFormatField;
			}
			set
			{
				this.storageFormatField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int PacketSize
		{
			get
			{
				return this.packetSizeField;
			}
			set
			{
				this.packetSizeField = value;
			}
		}
	}
}
