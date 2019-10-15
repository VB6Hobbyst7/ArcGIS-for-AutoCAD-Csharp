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
	public class FeatureExtent : MapArea
	{
		private double defaultScaleField;

		private double expandRatioField;

		private int[] featureIDsField;

		private int layerIDField;

		private string mapNameField;

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double DefaultScale
		{
			get
			{
				return this.defaultScaleField;
			}
			set
			{
				this.defaultScaleField = value;
			}
		}

		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public double ExpandRatio
		{
			get
			{
				return this.expandRatioField;
			}
			set
			{
				this.expandRatioField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public int[] FeatureIDs
		{
			get
			{
				return this.featureIDsField;
			}
			set
			{
				this.featureIDsField = value;
			}
		}

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
		public string MapName
		{
			get
			{
				return this.mapNameField;
			}
			set
			{
				this.mapNameField = value;
			}
		}

		public FeatureExtent()
		{
			this.expandRatioField = 1.0;
		}
	}
}
