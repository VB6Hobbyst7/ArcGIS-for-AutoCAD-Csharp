using AFA.Resources;
using ArcGIS10Types;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;

namespace AFA
{
	public class Extent
	{
		public double? XMin
		{
			get;
			set;
		}

		public double? XMax
		{
			get;
			set;
		}

		public double? YMin
		{
			get;
			set;
		}

		public double? YMax
		{
			get;
			set;
		}

		public string SpatialReference
		{
			get;
			set;
		}

		public Point2d MinPoint
		{
			get
			{
				Point2d result = new Point2d(this.XMin.Value, this.YMin.Value);
				return result;
			}
		}

		public Point2d MaxPoint
		{
			get
			{
				Point2d result = new Point2d(this.XMax.Value, this.YMax.Value);
				return result;
			}
		}

		public Extent(Extent src)
		{
			this.XMin = src.XMin;
			this.XMax = src.XMax;
			this.YMin = src.YMin;
			this.YMax = src.YMax;
			if (src.SpatialReference != null)
			{
				this.SpatialReference = string.Copy(src.SpatialReference);
			}
		}

		public Extent()
		{
			this.XMin = (this.XMax = (this.YMin = (this.YMax = new double?(0.0))));
			this.SpatialReference = "";
		}

		public Extent(double xMin, double yMin, double xMax, double yMax)
		{
			this.XMin = new double?(xMin);
			this.YMin = new double?(yMin);
			this.XMax = new double?(xMax);
			this.YMax = new double?(yMax);
		}

		public Extent(Point3d p1, Point3d p2)
		{
			this.XMin = new double?(Math.Min(p1.X, p2.X));
			this.YMin = new double?(Math.Min(p1.Y, p2.Y));
			this.XMax = new double?(Math.Max(p1.X, p2.X));
			this.YMax = new double?(Math.Max(p1.Y, p2.Y));
		}

		public Extent(Point2d p1, Point2d p2)
		{
			this.XMin = new double?(Math.Min(p1.X, p2.X));
			this.YMin = new double?(Math.Min(p1.Y, p2.Y));
			this.XMax = new double?(Math.Max(p1.X, p2.X));
			this.YMax = new double?(Math.Max(p1.Y, p2.Y));
		}

		public Extent(Extents3d ext)
		{
			this.XMin = new double?(Math.Min(ext.MinPoint.X, ext.MaxPoint.X));
			this.YMin = new double?(Math.Min(ext.MinPoint.Y, ext.MaxPoint.Y));
			this.XMax = new double?(Math.Max(ext.MinPoint.X, ext.MaxPoint.X));
			this.YMax = new double?(Math.Max(ext.MinPoint.Y, ext.MaxPoint.Y));
		}

		public Extent(Document doc)
		{
			Point3d point3d;
			Point3d point3d2;
			if (DocUtil.GetActiveExtents(doc, out point3d, out point3d2))
			{
				Point3d point3d3 = new Point3d(point3d.X, point3d.Y, 0.0);
				Point3d point3d4 = new Point3d(point3d2.X, point3d2.Y, 0.0);
				this.XMin = new double?(point3d3.X);
				this.YMin = new double?(point3d3.Y);
				this.XMax = new double?(point3d4.X);
				this.YMax = new double?(point3d4.Y);
				try
				{
					this.SpatialReference = AfaDocData.ActiveDocData.DocPRJ.WKT;
					return;
				}
				catch
				{
					try
					{
						this.SpatialReference = MSCPrj.ReadWKT(doc);
					}
					catch
					{
						this.SpatialReference = "";
					}
					return;
				}
			}
			this.XMin = (this.XMax = (this.YMin = (this.YMax = new double?(0.0))));
			this.SpatialReference = "";
		}

		public Extent(IDictionary<string, object> dict)
		{
			try
			{
				this.XMin = (this.XMax = (this.YMin = (this.YMax = new double?(0.0))));
				this.SpatialReference = "";
				if (dict != null)
				{
					object obj;
					double value;
					if (dict.TryGetValue("xmin", out obj) && obj != null && double.TryParse(obj.ToString(), out value))
					{
						this.XMin = new double?(value);
					}
					double value2;
					if (dict.TryGetValue("xmax", out obj) && obj != null && double.TryParse(obj.ToString(), out value2))
					{
						this.XMax = new double?(value2);
					}
					double value3;
					if (dict.TryGetValue("ymin", out obj) && obj != null && double.TryParse(obj.ToString(), out value3))
					{
						this.YMin = new double?(value3);
					}
					double value4;
					if (dict.TryGetValue("ymax", out obj) && obj != null && double.TryParse(obj.ToString(), out value4))
					{
						this.YMax = new double?(value4);
					}
					if (dict.TryGetValue("spatialReference", out obj))
					{
						IDictionary<string, object> dictionary = obj as IDictionary<string, object>;
						if (dictionary != null)
						{
							object obj2;
							if (dictionary.TryGetValue("wkt", out obj2))
							{
								this.SpatialReference = obj2.ToString();
							}
							else if (dictionary.TryGetValue("wkid", out obj2))
							{
								this.SpatialReference = obj2.ToString();
							}
						}
					}
					if (!this.XMin.HasValue || !this.XMax.HasValue || !this.YMin.HasValue || !this.YMax.HasValue)
					{
						this.XMin = (this.XMax = (this.YMin = (this.YMax = new double?(0.0))));
					}
				}
			}
			catch
			{
				this.XMin = (this.XMax = (this.YMin = (this.YMax = new double?(0.0))));
			}
		}

		public Extent(Point2d center, double h, double w)
		{
			this.XMin = new double?(center.X - w / 2.0);
			this.XMax = new double?(center.X + w / 2.0);
			this.YMin = new double?(center.Y - h / 2.0);
			this.YMax = new double?(center.Y + h / 2.0);
		}

		public Extent(PointN[] points)
		{
			try
			{
				this.XMin = new double?(Math.Min(points[0].X, points[1].X));
				this.XMin = new double?(Math.Min(this.XMin.Value, points[2].X));
				this.YMin = new double?(Math.Min(points[0].Y, points[1].Y));
				this.YMin = new double?(Math.Min(this.YMin.Value, points[2].Y));
				this.XMax = new double?(Math.Max(points[0].X, points[1].X));
				this.XMax = new double?(Math.Max(this.XMax.Value, points[2].X));
				this.YMax = new double?(Math.Max(points[0].Y, points[1].Y));
				this.YMax = new double?(Math.Max(this.YMax.Value, points[2].Y));
				if (points[0].SpatialReference != null)
				{
					this.SpatialReference = AGSSpatialReference.GetSpRefString(points[0].SpatialReference);
				}
			}
			catch
			{
			}
		}

		public void SetWKTFrom(object obj)
		{
			this.SpatialReference = AGSSpatialReference.GetWKTFrom(obj);
		}

		public void SetWKTFrom(string wktString)
		{
			this.SpatialReference = wktString;
		}

		public bool IsValid()
		{
			double? xMin = this.XMin;
			double? xMax = this.XMax;
			if (xMin.GetValueOrDefault() != xMax.GetValueOrDefault() || xMin.HasValue != xMax.HasValue)
			{
				double? yMin = this.YMin;
				double? yMax = this.YMax;
				if (yMin.GetValueOrDefault() != yMax.GetValueOrDefault() || yMin.HasValue != yMax.HasValue)
				{
					return this.XMin.HasValue && this.YMin.HasValue && this.XMax.HasValue && this.YMax.HasValue;
				}
			}
			return false;
		}

		public override string ToString()
		{
			double? xMin = this.XMin;
			double? yMin = this.YMin;
			if (xMin.GetValueOrDefault() != yMin.GetValueOrDefault() || xMin.HasValue != yMin.HasValue)
			{
				return string.Format("({0},{1}); ({2},{3})\n{4}", new object[]
				{
					this.XMin,
					this.XMax,
					this.YMin,
					this.YMax,
					this.SpatialReference
				});
			}
			return AfaStrings.UndefinedExtent;
		}

		public override bool Equals(object obj)
		{
			if (obj is Extent)
			{
				Extent extent = (Extent)obj;
				double? xMax = extent.XMax;
				double? xMax2 = this.XMax;
				if (xMax.GetValueOrDefault() == xMax2.GetValueOrDefault() && xMax.HasValue == xMax2.HasValue)
				{
					double? xMin = extent.XMin;
					double? xMin2 = this.XMin;
					if (xMin.GetValueOrDefault() == xMin2.GetValueOrDefault() && xMin.HasValue == xMin2.HasValue)
					{
						double? yMax = extent.YMax;
						double? yMax2 = this.YMax;
						if (yMax.GetValueOrDefault() == yMax2.GetValueOrDefault() && yMax.HasValue == yMax2.HasValue)
						{
							double? yMin = extent.YMin;
							double? yMin2 = this.YMin;
							if (yMin.GetValueOrDefault() == yMin2.GetValueOrDefault() && yMin.HasValue == yMin2.HasValue && extent.SpatialReference == this.SpatialReference)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			return base.Equals(obj);
		}

		public static Extent Union(Extent a, Extent b)
		{
			if (a.SpatialReference != b.SpatialReference)
			{
				return a;
			}
			return new Extent(a)
			{
				XMin = new double?(Math.Min(a.XMin.Value, b.XMin.Value)),
				YMin = new double?(Math.Min(a.YMin.Value, b.YMin.Value)),
				XMax = new double?(Math.Max(a.XMax.Value, b.XMax.Value)),
				YMax = new double?(Math.Max(a.YMax.Value, b.YMax.Value))
			};
		}

		public static Extent Intersect(Extent a, Extent b)
		{
			double? xMin = a.XMin;
			double? xMax = b.XMax;
			if (xMin.GetValueOrDefault() < xMax.GetValueOrDefault() && (xMin.HasValue & xMax.HasValue))
			{
				double? xMax2 = a.XMax;
				double? xMin2 = b.XMin;
				if (xMax2.GetValueOrDefault() > xMin2.GetValueOrDefault() && (xMax2.HasValue & xMin2.HasValue))
				{
					double? yMin = a.YMin;
					double? yMax = b.YMax;
					if (yMin.GetValueOrDefault() < yMax.GetValueOrDefault() && (yMin.HasValue & yMax.HasValue))
					{
						double? yMax2 = a.YMax;
						double? yMin2 = b.YMin;
						if (yMax2.GetValueOrDefault() > yMin2.GetValueOrDefault() && (yMax2.HasValue & yMin2.HasValue))
						{
							return new Extent(a)
							{
								XMin = new double?(Math.Max(a.XMin.Value, b.XMin.Value)),
								YMin = new double?(Math.Max(a.YMin.Value, b.YMin.Value)),
								XMax = new double?(Math.Min(a.XMax.Value, b.XMax.Value)),
								YMax = new double?(Math.Min(a.YMax.Value, b.YMax.Value))
							};
						}
					}
				}
			}
			return new Extent();
		}
	}
}
