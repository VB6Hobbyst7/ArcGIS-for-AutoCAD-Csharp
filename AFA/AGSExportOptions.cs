using ArcGIS10Types;
using Autodesk.AutoCAD.ApplicationServices;
using System.Collections.Generic;

namespace AFA
{
    public class AGSExportOptions
	{
		public Extent BoundingBox
		{
			get;
			set;
		}

		public int Width
		{
			get;
			set;
		}

		public int Height
		{
			get;
			set;
		}

		public AGSSpatialReference SR
		{
			get;
			set;
		}

		public bool Dynamic
		{
			get;
			set;
		}

		public string OutputWKT
		{
			get;
			set;
		}

		public Document AcadDocument
		{
			get;
			set;
		}

		public string Format
		{
			get;
			set;
		}

		public int DPI
		{
			get;
			set;
		}

		public byte Transparency
		{
			get;
			set;
		}

		public string[] SupportedFormats
		{
			get;
			set;
		}

		public List<string> LayerList
		{
			get;
			set;
		}

		public string WhereClause
		{
			get;
			set;
		}

		public string PixelType
		{
			get;
			set;
		}

		public int NoDataValue
		{
			get;
			set;
		}

		public string Interpolation
		{
			get;
			set;
		}

		public string TransCompression
		{
			get;
			set;
		}

		public string MosaicMethod
		{
			get;
			set;
		}

		public string OrderField
		{
			get;
			set;
		}

		public string OrderBaseValue
		{
			get;
			set;
		}

		public string LockRasterID
		{
			get;
			set;
		}

		public bool Ascending
		{
			get;
			set;
		}

		public string MosaicOperator
		{
			get;
			set;
		}

		public int Quality
		{
			get;
			set;
		}

		public string BandIds
		{
			get;
			set;
		}

		public string MosaicRule
		{
			get;
			set;
		}

		public string RenderingRule
		{
			get;
			set;
		}

		public esriImageFormat EsriFormat
		{
			get
			{
				return this.ConvertImageFormat(this.Format);
			}
		}

		public string OutputFile
		{
			get;
			set;
		}

		private esriImageFormat ConvertImageFormat(string ImageFormat)
		{
			if (ImageFormat == "PNG")
			{
				return esriImageFormat.esriImagePNG;
			}
			if (ImageFormat == "PNG24")
			{
				return esriImageFormat.esriImagePNG24;
			}
			if (ImageFormat == "AI")
			{
				return esriImageFormat.esriImageAI;
			}
			if (ImageFormat == "BMP")
			{
				return esriImageFormat.esriImageBMP;
			}
			if (ImageFormat == "DIB")
			{
				return esriImageFormat.esriImageDIB;
			}
			if (ImageFormat == "EMF")
			{
				return esriImageFormat.esriImageEMF;
			}
			if (ImageFormat == "GIF")
			{
				return esriImageFormat.esriImageGIF;
			}
			if (ImageFormat == "JPG")
			{
				return esriImageFormat.esriImageJPG;
			}
			if (ImageFormat == "JPGPNG")
			{
				return esriImageFormat.esriImageJPGPNG;
			}
			if (ImageFormat == "PDF")
			{
				return esriImageFormat.esriImagePDF;
			}
			if (ImageFormat == "PNG32")
			{
				return esriImageFormat.esriImagePNG32;
			}
			if (ImageFormat == "PS")
			{
				return esriImageFormat.esriImagePS;
			}
			if (ImageFormat == "SVG")
			{
				return esriImageFormat.esriImageSVG;
			}
			if (ImageFormat == "TIFF")
			{
				return esriImageFormat.esriImageTIFF;
			}
			return esriImageFormat.esriImagePNG;
		}
	}
}
