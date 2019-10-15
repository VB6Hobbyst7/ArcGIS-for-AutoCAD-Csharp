using ArcGIS10Types;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using Image = System.Drawing.Image;

namespace AFA.Test
{
	internal class TiledRaster
	{
		public class ViewInfo
		{
			public Point2d CenterPoint
			{
				get;
				set;
			}

			public double Height
			{
				get;
				set;
			}

			public double Width
			{
				get;
				set;
			}

			public ViewInfo(ViewTableRecord vtr)
			{
				this.CenterPoint = vtr.CenterPoint;
				this.Height = vtr.Height;
				this.Width = vtr.Width;
			}

			public static bool Equivalent(TiledRaster.ViewInfo a, TiledRaster.ViewInfo b)
			{
				return a != null && b != null && (a.Height == b.Height && a.Width == b.Width && a.CenterPoint == b.CenterPoint);
			}
		}

		public class RasterInfo
		{
			public TiledRaster.ViewInfo View
			{
				get;
				set;
			}

			public string url
			{
				get;
				set;
			}

			public EnvelopeN ActualPosition
			{
				get;
				set;
			}

			public RasterInfo(string imageURL, TiledRaster.ViewInfo v, EnvelopeN env)
			{
				this.View = v;
				this.url = imageURL;
				this.ActualPosition = (EnvelopeN)Utility.CloneObject(env);
			}
		}

		private MapServerInfo _mapinfo;

		private MapServerProxy _mapservice;

		public Document ParentDoc
		{
			get;
			set;
		}

		public ObjectId AcadID
		{
			get;
			set;
		}

		public List<ObjectId> RasterIds
		{
			get;
			set;
		}

		public TiledRaster(Document doc)
		{
			this.ParentDoc = doc;
			this.RasterIds = new List<ObjectId>();
			this._mapservice = new MapServerProxy();
			this._mapservice.Url = "http://mashup/ArcGIS/services/MapServices/CorvallisCompact/MapServer";
			this._mapinfo = this._mapservice.GetServerInfo(this._mapservice.GetDefaultMapName());
			this.SampleGetTileInfo();
		}

		private ImageDescription CreateImageDescription()
		{
			ImageType imageType = new ImageType();
			imageType.ImageFormat = esriImageFormat.esriImagePNG;
			imageType.ImageReturnType = esriImageReturnType.esriImageReturnURL;
			ImageDisplay imageDisplay = new ImageDisplay();
			return new ImageDescription
			{
				ImageDisplay = imageDisplay,
				ImageType = imageType
			};
		}

		private void SampleGetTileInfo()
		{
			string defaultMapName = this._mapservice.GetDefaultMapName();
			this._mapservice.GetServerInfo(defaultMapName);
			MapDescription defaultMapDescription = this._mapinfo.DefaultMapDescription;
			string virtualCacheDirectory = this._mapservice.GetVirtualCacheDirectory(defaultMapName, -1);
			int num = 400;
			int height = 400;
			EnvelopeN envelopeN = (EnvelopeN)defaultMapDescription.MapArea.Extent;
			double num2 = Math.Abs(envelopeN.XMax - envelopeN.XMin) / (double)num;
			Bitmap bitmap = new Bitmap(num, height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.FillRectangle(new SolidBrush(System.Drawing.Color.LightGray), 0, 0, num, height);
			if (this._mapservice.HasSingleFusedMapCache(defaultMapName))
			{
				TileCacheInfo tileCacheInfo = this._mapservice.GetTileCacheInfo(defaultMapName);
				LODInfo[] lODInfos = tileCacheInfo.LODInfos;
				double num3 = 0.0;
				int level = 0;
				LODInfo[] array = lODInfos;
				for (int i = 0; i < array.Length; i++)
				{
					LODInfo lODInfo = array[i];
					double resolution = lODInfo.Resolution;
					num3 = resolution;
					level = lODInfo.LevelID;
					if (num2 >= resolution)
					{
						break;
					}
				}
				double xMin = envelopeN.XMin;
				double yMin = envelopeN.YMin;
				double xMax = envelopeN.XMax;
				double yMax = envelopeN.YMax;
				double x = ((PointN)tileCacheInfo.TileOrigin).X;
				double y = ((PointN)tileCacheInfo.TileOrigin).Y;
				double d = (xMin - x) / ((double)tileCacheInfo.TileCols * num3);
				double d2 = (y - yMax) / ((double)tileCacheInfo.TileRows * num3);
				double d3 = (xMax - x) / ((double)tileCacheInfo.TileCols * num3);
				double d4 = (y - yMin) / ((double)tileCacheInfo.TileRows * num3);
				int num4 = (int)Math.Floor(d);
				int num5 = (int)Math.Floor(d2);
				int num6 = (int)Math.Floor(d3);
				int num7 = (int)Math.Floor(d4);
				double num8 = x + (double)num4 * ((double)tileCacheInfo.TileCols * num3);
				double num9 = y - (double)num5 * ((double)tileCacheInfo.TileRows * num3);
				double num10 = Math.Abs(xMin - num8);
				double num11 = Math.Abs(yMax - num9);
				int num12 = (int)(num10 / num3);
				int num13 = (int)(num11 / num3);
				TileImageInfo tileImageInfo = this._mapservice.GetTileImageInfo(this._mapservice.GetDefaultMapName());
				int num14 = 0;
				for (int j = num5; j <= num7; j++)
				{
					int num15 = 0;
					for (int k = num4; k <= num6; k++)
					{
						byte[] array2 = null;
						try
						{
							array2 = this._mapservice.GetMapTile(defaultMapName, level, j, k, tileImageInfo.CacheTileFormat);
							string text = "png";
							string requestUriString = string.Concat(new string[]
							{
								virtualCacheDirectory,
								"/L",
								level.ToString().PadLeft(2, '0'),
								"/R",
								j.ToString("x").PadLeft(8, '0'),
								"/C",
								k.ToString("x").PadLeft(8, '0'),
								text
							});
							HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
							HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
							Stream responseStream = httpWebResponse.GetResponseStream();
							MemoryStream memoryStream = new MemoryStream();
							int num16;
							while ((num16 = responseStream.ReadByte()) != -1)
							{
								memoryStream.WriteByte((byte)num16);
							}
							array2 = memoryStream.ToArray();
						}
						catch
						{
						}
						if (array2 != null)
						{
							using (MemoryStream memoryStream2 = new MemoryStream(array2, 0, array2.Length))
							{
								memoryStream2.Write(array2, 0, array2.Length);
								Image image = Image.FromStream(memoryStream2, true);
								graphics.DrawImage(image, tileCacheInfo.TileCols * num15 - num12, tileCacheInfo.TileRows * num14 - num13, tileCacheInfo.TileCols, tileCacheInfo.TileRows);
							}
						}
						num15++;
					}
					num14++;
				}
			}
			string filename = Utility.TemporaryFilePath() + ".bmp";
			bitmap.Save(filename, ImageFormat.Bmp);
			Bitmap bitmap2 = new Bitmap(num, height);
			Graphics graphics2 = Graphics.FromImage(bitmap2);
			graphics2.FillRectangle(new SolidBrush(System.Drawing.Color.LightGray), 0, 0, num, height);
			graphics2.DrawImage(bitmap, 0, 0, num, height);
			string filename2 = Utility.TemporaryFilePath() + ".bmp";
			bitmap2.Save(filename2, ImageFormat.Bmp);
		}
	}
}
