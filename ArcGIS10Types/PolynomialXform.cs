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
	public class PolynomialXform : GeodataXform
	{
		private SpatialReference spatialReferenceField;

		private int polynomialOrderField;

		private bool polynomialOrderFieldSpecified;

		private double[] sourceGCPsField;

		private double[] targetGCPsField;

		private double[] coeffXField;

		private double[] coeffYField;

		private double[] inverseCoeffXField;

		private double[] inverseCoeffYField;

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
		public int PolynomialOrder
		{
			get
			{
				return this.polynomialOrderField;
			}
			set
			{
				this.polynomialOrderField = value;
			}
		}

		[XmlIgnore]
		public bool PolynomialOrderSpecified
		{
			get
			{
				return this.polynomialOrderFieldSpecified;
			}
			set
			{
				this.polynomialOrderFieldSpecified = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] SourceGCPs
		{
			get
			{
				return this.sourceGCPsField;
			}
			set
			{
				this.sourceGCPsField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] TargetGCPs
		{
			get
			{
				return this.targetGCPsField;
			}
			set
			{
				this.targetGCPsField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] CoeffX
		{
			get
			{
				return this.coeffXField;
			}
			set
			{
				this.coeffXField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] CoeffY
		{
			get
			{
				return this.coeffYField;
			}
			set
			{
				this.coeffYField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] InverseCoeffX
		{
			get
			{
				return this.inverseCoeffXField;
			}
			set
			{
				this.inverseCoeffXField = value;
			}
		}

		[XmlArray(Form = XmlSchemaForm.Unqualified), XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
		public double[] InverseCoeffY
		{
			get
			{
				return this.inverseCoeffYField;
			}
			set
			{
				this.inverseCoeffYField = value;
			}
		}
	}
}
