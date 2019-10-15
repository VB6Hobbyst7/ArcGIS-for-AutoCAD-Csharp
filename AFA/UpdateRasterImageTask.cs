using AFA.Resources;
using ArcGIS10Types;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace AFA
{
	internal class UpdateRasterImageTask : ITask
	{
		private StringBuilder _errMsg = new StringBuilder();

		private bool _successful;

		private List<string> _returnData;

		public SpatialReference OutputSpatialReference;

		public HiddenUpdateForm UpdateForm;

		public string OutputFile;

		public string RequestURL
		{
			get;
			set;
		}

		public AGSConnection Parent
		{
			get;
			set;
		}

		public string TaskName
		{
			get
			{
				return "Request an image from ArcGIS server";
			}
		}

		public string CompletionMessage
		{
			get
			{
				return this._errMsg.ToString();
			}
		}

		public bool Successful
		{
			get
			{
				return this._successful;
			}
		}

		public object ResultData
		{
			get
			{
				return this._returnData;
			}
		}

		public UpdateRasterImageTask(AGSConnection parent)
		{
			this.Parent = parent;
			this.UpdateForm = null;
			this.OutputSpatialReference = null;
		}

		~UpdateRasterImageTask()
		{
		}

		public void Execute()
		{
			this._errMsg.Length = 0;
			this._successful = true;
			this._returnData = new List<string>();
			this.DoTask();
		}

		private void DoTask()
		{
			try
			{
				IDictionary<string, object> dictionary = this.Parent.MakeDictionaryRequest(this.RequestURL);
				if (dictionary == null)
				{
					string text = this.Parent.ErrorMessage;
					if (string.IsNullOrEmpty(text))
					{
						text = AfaStrings.AnUnexpectedErrorOccured;
					}
					ErrorReport.ShowErrorMessage(text);
				}
				this.UpdateForm.SetReady(false);
				SpatialReference spatialReference = null;
				if (dictionary != null)
				{
					object obj;
					if (!dictionary.TryGetValue("href", out obj))
					{
						string text2 = AfaStrings.Error;
						if (dictionary.ContainsKey("error"))
						{
							IDictionary<string, object> dictionary2 = dictionary["error"] as IDictionary<string, object>;
							object obj2;
							if (dictionary2.ContainsKey("details") && dictionary2.TryGetValue("details", out obj2))
							{
								object[] array = obj2 as object[];
								if (array != null)
								{
									text2 = text2 + " - " + array[0].ToString();
								}
							}
						}
						Mouse.OverrideCursor = null;
						ErrorReport.ShowErrorMessage(text2);
					}
					else
					{
						string text3 = obj as string;
						if (text3.Length > 0)
						{
							decimal value = 0m;
							decimal value2 = 0m;
							decimal value3 = 0m;
							decimal value4 = 0m;
							if (dictionary.TryGetValue("extent", out obj))
							{
								IDictionary<string, object> dictionary3 = obj as IDictionary<string, object>;
								object obj3;
								if (dictionary3.TryGetValue("spatialReference", out obj3))
								{
									IDictionary<string, object> dictionary4 = obj3 as IDictionary<string, object>;
									object obj4;
									if (dictionary4.TryGetValue("wkid", out obj4))
									{
										int num = int.Parse(obj4.ToString());
										spatialReference = AGSSpatialReference.SpRefFromWKID(ref num);
									}
									else if (dictionary4.TryGetValue("wkt", out obj4))
									{
										string text4 = obj4.ToString();
										spatialReference = AGSSpatialReference.SpRefFromWKT(ref text4);
									}
								}
								if (dictionary3.TryGetValue("xmin", out obj3))
								{
									value = (decimal)obj3;
								}
								if (dictionary3.TryGetValue("xmax", out obj3))
								{
									value3 = (decimal)obj3;
								}
								if (dictionary3.TryGetValue("ymax", out obj3))
								{
									value4 = (decimal)obj3;
								}
								if (dictionary3.TryGetValue("ymin", out obj3))
								{
									value2 = (decimal)obj3;
								}
								PointN pointN = new PointN();
								if (spatialReference != null)
								{
									pointN.SpatialReference = spatialReference;
								}
								pointN.X = (double)value;
								pointN.Y = (double)value2;
								pointN.Z = 0.0;
								PointN[] array2 = new PointN[]
								{
									pointN,
									new PointN
									{
										SpatialReference = spatialReference,
										X = (double)value3,
										Y = (double)value2,
										Z = 0.0
									},
									new PointN
									{
										SpatialReference = spatialReference,
										X = (double)value,
										Y = (double)value4,
										Z = 0.0
									}
								};
								try
								{
									if (File.Exists(this.OutputFile))
									{
										File.Delete(this.OutputFile);
										if (App.TempFiles.Contains(this.OutputFile))
										{
											App.TempFiles.Remove(this.OutputFile);
										}
									}
									if (!this.Parent.DownloadFile(text3, this.OutputFile))
									{
										this.OutputFile = text3;
										if (string.IsNullOrEmpty(this.OutputFile))
										{
											ErrorReport.ShowErrorMessage(AfaStrings.ErrorCreatingOutputFile);
											return;
										}
									}
								}
								catch
								{
									this.OutputFile = text3;
									if (string.IsNullOrEmpty(this.OutputFile))
									{
										ErrorReport.ShowErrorMessage(AfaStrings.ErrorCreatingOutputFile);
										return;
									}
								}
								this.UpdateForm.SetReady(false);
								Point3d point3d = new Point3d(pointN.X, array2[0].Y, 0.0);
								Point3d point3d2 = new Point3d(array2[1].X, array2[1].Y, 0.0);
								Point3d point3d3 = new Point3d(array2[2].X, array2[2].Y, 0.0);
								this.UpdateForm.V1 = point3d2 - point3d;
								this.UpdateForm.V2 = point3d3 - point3d;
								this.UpdateForm.BasePoint = point3d;
								this.UpdateForm.ImageURL = this.OutputFile;
								this.UpdateForm.SetReady(true);
							}
						}
					}
				}
			}
			catch (SystemException ex)
			{
				this.UpdateForm.SetReady(false);
				this._errMsg.Append(ex.Message);
			}
		}
	}
}
