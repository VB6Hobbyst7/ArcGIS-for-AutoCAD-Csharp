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
	public class MapServerLegendInfo
	{
		private int layerIDField;

		private string nameField;

		private MapServerLegendGroup[] legendGroupsField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public int LayerID
		{
			get
			{
				return this.layerIDField;
			}
			set
			{
				this.layerIDField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public MapServerLegendGroup[] LegendGroups
		{
			get
			{
				return this.legendGroupsField;
			}
			set
			{
				this.legendGroupsField = value;
			}
		}
	}
}
