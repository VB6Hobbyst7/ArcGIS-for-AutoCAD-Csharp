using ArcGIS10Types;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AFA
{
	public class UpdateMapImageTask : ITask
	{
		private StringBuilder _errMsg = new StringBuilder();

		private bool _successful;

		private List<string> _returnData;

		public MapServerProxy Mapservice;

		public ImageDescription Imagedescription;

		public MapDescription Mapdesc;

		public AGSGeometryServer GeometryService;

		public SpatialReference OutputSpatialReference;

		public string name;

		public HiddenUpdateForm UpdateForm;

		public string OutputFile;

		public string TaskName
		{
			get
			{
				return "Request a map from ArcGIS server";
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

		public UpdateMapImageTask()
		{
			this.Mapservice = null;
			this.Imagedescription = null;
			this.Mapdesc = null;
			this.UpdateForm = null;
			this.OutputSpatialReference = null;
			this.GeometryService = null;
		}

		~UpdateMapImageTask()
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
				if (this.Mapservice == null || this.Mapdesc == null || this.Imagedescription == null)
				{
					this._errMsg.Append("Insufficient map information to generate request");
				}
				else if (this.UpdateForm == null)
				{
					this._errMsg.Append("Update form not initialized");
				}
				else
				{
					this.UpdateForm.SetReady(false);
					MapImage mapImage = this.Mapservice.ExportMapImage(this.Mapdesc, this.Imagedescription);
					string imageURL = mapImage.ImageURL;
					if (string.IsNullOrEmpty(imageURL))
					{
						this._errMsg.Append("ErrorGeneratingImage");
					}
					else
					{
						this._errMsg.Append("Map successfully retrieved");
						this._returnData.AddRange(new string[]
						{
							mapImage.ImageURL
						});
						EnvelopeN envelopeN = mapImage.Extent as EnvelopeN;
						PointN[] array = new PointN[]
						{
							new PointN
							{
								SpatialReference = envelopeN.SpatialReference,
								X = envelopeN.XMin,
								Y = envelopeN.YMin,
								Z = 0.0
							},
							new PointN
							{
								SpatialReference = envelopeN.SpatialReference,
								X = envelopeN.XMax,
								Y = envelopeN.YMin,
								Z = 0.0
							},
							new PointN
							{
								SpatialReference = envelopeN.SpatialReference,
								X = envelopeN.XMin,
								Y = envelopeN.YMax,
								Z = 0.0
							}
						};
						try
						{
							if (!string.IsNullOrEmpty(this.OutputFile))
							{
								if (File.Exists(this.OutputFile))
								{
									File.Delete(this.OutputFile);
									if (App.TempFiles.Contains(this.OutputFile))
									{
										App.TempFiles.Remove(this.OutputFile);
									}
								}
								WebClient webClient = new WebClient();
								webClient.Proxy = this.Mapservice.Proxy;
								webClient.Credentials = this.Mapservice.Credentials;
								if (webClient.Credentials == null)
								{
									webClient.UseDefaultCredentials = true;
								}
								webClient.DownloadFile(imageURL, this.OutputFile);
								App.TempFiles.Add(this.OutputFile);
							}
						}
						catch
						{
							this.UpdateForm.SetReady(false);
							return;
						}
						this.UpdateForm.SetReady(false);
						Point3d point3d = new Point3d(array[0].X, array[0].Y, 0.0);
						Point3d point3d2 = new Point3d(array[1].X, array[1].Y, 0.0);
						Point3d point3d3 = new Point3d(array[2].X, array[2].Y, 0.0);
						this.UpdateForm.V1 = point3d2 - point3d;
						this.UpdateForm.V2 = point3d3 - point3d;
						this.UpdateForm.BasePoint = point3d;
						this.UpdateForm.ImageURL = this.OutputFile;
						this.UpdateForm.SetReady(true);
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
