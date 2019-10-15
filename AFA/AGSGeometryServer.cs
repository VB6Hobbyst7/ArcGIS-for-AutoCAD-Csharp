using ArcGIS10Types;
using System;
using System.Collections.Generic;
using System.Net;

namespace AFA
{
	public class AGSGeometryServer : AGSService
	{
		private Geometry_GeometryServer _GeomServer;

		private static AGSGeometryServer SampleServer;

		private static AGSConnection SampleServerConnection;

		public bool IsESRIServer
		{
			get
			{
				bool result;
				try
				{
					result = (this._GeomServer.Url == "http://sampleserver3.arcgisonline.com/ArcGIS/services/Geometry/GeometryServer");
				}
				catch
				{
					result = true;
				}
				return result;
			}
		}

		public string URL
		{
			get
			{
				return this._GeomServer.Url;
			}
			set
			{
				this._GeomServer.Url = this.URL;
			}
		}

		public static AGSGeometryServer GetSampleServer()
		{
			AGSGeometryServer result;
			try
			{
				if (AGSGeometryServer.SampleServer == null)
				{
					if (AGSGeometryServer.SampleServerConnection == null)
					{
						AGSGeometryServer.SampleServerConnection = new AGSConnection("Sample Geometry Server", "http://sampleserver3.arcgisonline.com/ArcGIS/services");
					}
					AGSGeometryServer.SampleServer = new AGSGeometryServer(AGSGeometryServer.SampleServerConnection, "http://sampleserver3.arcgisonline.com/ArcGIS/services/Geometry/GeometryServer");
				}
				result = AGSGeometryServer.SampleServer;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public AGSGeometryServer(AGSConnection parent, string soapURL) : base("Geometry", parent)
		{
			base.ErrorMessage = "";
			this._GeomServer = new Geometry_GeometryServer();
			this._GeomServer.Proxy = WebRequest.DefaultWebProxy;
			this._GeomServer.Credentials = base.Parent.Credentials;
			if (this._GeomServer.Credentials == null)
			{
				this._GeomServer.UseDefaultCredentials = true;
			}
			if (string.IsNullOrEmpty(soapURL))
			{
				throw new Exception("Invalid geometry server");
			}
			this._GeomServer.Url = soapURL;
			parent.GeometryService = this;
		}

		public override string GetWKT()
		{
			return "";
		}

		public string GetSpatialReferenceWKT(int WKID)
		{
			string result;
			try
			{
				SpatialReference spatialReference = this._GeomServer.FindSRByWKID(null, WKID, -1, true, true);
				result = spatialReference.WKT;
			}
			catch (SystemException ex)
			{
				base.ErrorMessage = ex.Message;
				result = null;
			}
			catch
			{
				base.ErrorMessage = "Unknown Error";
				result = null;
			}
			return result;
		}

		public string GetSpatialReferenceWKID(string wkt)
		{
			string result;
			try
			{
				SpatialReference spatialReference = this._GeomServer.FindSRByWKT(wkt, null, true, true);
				if (!spatialReference.WKIDSpecified)
				{
					result = null;
				}
				else if (spatialReference.WKID == 0)
				{
					result = null;
				}
				else
				{
					result = spatialReference.WKID.ToString();
				}
			}
			catch (WebException)
			{
				if (base.Parent.Challenge())
				{
					this._GeomServer.Credentials = base.Parent.Credentials;
					if (this._GeomServer.Credentials == null)
					{
						this._GeomServer.UseDefaultCredentials = true;
					}
					this._GeomServer.Proxy = WebRequest.DefaultWebProxy;
					SpatialReference spatialReference2 = this._GeomServer.FindSRByWKT(wkt, null, true, true);
					if (!spatialReference2.WKIDSpecified)
					{
						result = null;
					}
					else if (spatialReference2.WKID == 0)
					{
						result = null;
					}
					else
					{
						result = spatialReference2.WKID.ToString();
					}
				}
				else
				{
					ErrorReport.ShowErrorMessage(base.Parent.ErrorMessage);
					result = null;
				}
			}
			return result;
		}

		public EnvelopeN ProjectEnvelope(EnvelopeN env, SpatialReference inSR, SpatialReference outSR)
		{
			Geometry[] inGeometryArray = new Geometry[]
			{
				env
			};
			EnvelopeN result;
			try
			{
				Geometry[] array = this._GeomServer.Project(inSR, outSR, true, null, env, inGeometryArray);
				EnvelopeN envelopeN = array[0] as EnvelopeN;
				envelopeN.SpatialReference = outSR;
				result = envelopeN;
			}
			catch
			{
				if (base.Parent.Challenge())
				{
					this._GeomServer.Credentials = base.Parent.Credentials;
					if (this._GeomServer.Credentials == null)
					{
						this._GeomServer.UseDefaultCredentials = true;
					}
					this._GeomServer.Proxy = WebRequest.DefaultWebProxy;
					Geometry[] array2 = this._GeomServer.Project(inSR, outSR, true, null, env, inGeometryArray);
					EnvelopeN envelopeN2 = array2[0] as EnvelopeN;
					envelopeN2.SpatialReference = outSR;
					result = envelopeN2;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		public RecordSet ProjectRecordSet(Envelope env, RecordSet rs, int geomFieldIndex, SpatialReference inSR, SpatialReference outSR)
		{
			if (geomFieldIndex < 0)
			{
				return null;
			}
			if (rs == null)
			{
				return null;
			}
			if (inSR == null || outSR == null)
			{
				return rs;
			}
			if (AGSService.SpRefAreEquivalent(inSR, outSR))
			{
				return rs;
			}
			RecordSet result;
			try
			{
				List<Geometry> list = new List<Geometry>(rs.Records.Length);
				Record[] records = rs.Records;
				for (int i = 0; i < records.Length; i++)
				{
					Record record = records[i];
					Geometry geometry = record.Values[geomFieldIndex] as Geometry;
					if (geometry != null)
					{
						list.Add(geometry);
					}
				}
				try
				{
					Geometry[] inGeometryArray = list.ToArray();
					Geometry[] array = this._GeomServer.Project(inSR, outSR, true, null, env, inGeometryArray);
					int num = 0;
					Record[] records2 = rs.Records;
					for (int j = 0; j < records2.Length; j++)
					{
						Record record2 = records2[j];
						record2.Values[geomFieldIndex] = array[num++];
					}
					result = rs;
				}
				catch
				{
					if (base.Parent.Challenge())
					{
						this._GeomServer.Proxy = WebRequest.DefaultWebProxy;
						this._GeomServer.Credentials = base.Parent.Credentials;
						if (this._GeomServer.Credentials == null)
						{
							this._GeomServer.UseDefaultCredentials = true;
						}
						try
						{
							Geometry[] inGeometryArray2 = list.ToArray();
							Geometry[] array2 = this._GeomServer.Project(inSR, outSR, true, null, env, inGeometryArray2);
							int num2 = 0;
							Record[] records3 = rs.Records;
							for (int k = 0; k < records3.Length; k++)
							{
								Record record3 = records3[k];
								record3.Values[geomFieldIndex] = array2[num2++];
							}
							result = rs;
							return result;
						}
						catch
						{
							result = this.ProjectRecordSetCarefully(env, rs, geomFieldIndex, inSR, outSR);
							return result;
						}
					}
					result = null;
				}
			}
			catch (Exception ex)
			{
				string arg_190_0 = ex.Message;
				result = this.ProjectRecordSetCarefully(env, rs, geomFieldIndex, inSR, outSR);
			}
			return result;
		}

		private RecordSet ProjectRecordSetCarefully(Envelope env, RecordSet rs, int geomFieldIndex, SpatialReference inSR, SpatialReference outSR)
		{
			if (geomFieldIndex < 0)
			{
				return null;
			}
			if (rs == null)
			{
				return null;
			}
			if (inSR == null || outSR == null)
			{
				return null;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 150;
			RecordSet result;
			try
			{
				List<Geometry> list = new List<Geometry>(num3);
				Record[] records = rs.Records;
				for (int i = 0; i < records.Length; i++)
				{
					Record record = records[i];
					Geometry geometry = record.Values[geomFieldIndex] as Geometry;
					if (geometry != null)
					{
						list.Add(geometry);
						num2++;
						if (num2 == num + num3)
						{
							Geometry[] array = this._GeomServer.Project(inSR, outSR, true, null, env, list.ToArray());
							Geometry[] array2 = array;
							for (int j = 0; j < array2.Length; j++)
							{
								Geometry geometry2 = array2[j];
								rs.Records[num++].Values[geomFieldIndex] = geometry2;
							}
							list.Clear();
						}
					}
				}
				if (list.Count > 0)
				{
					Geometry[] array3 = this._GeomServer.Project(inSR, outSR, true, null, env, list.ToArray());
					Geometry[] array4 = array3;
					for (int k = 0; k < array4.Length; k++)
					{
						Geometry geometry3 = array4[k];
						rs.Records[num++].Values[geomFieldIndex] = geometry3;
					}
					list.Clear();
				}
				result = rs;
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public Geometry[] DensifyGeometry(SpatialReference inputSpRef, double segmentDeviation, Geometry[] inputGeometry)
		{
			if (inputGeometry == null)
			{
				return inputGeometry;
			}
			if (inputGeometry.Length == 0)
			{
				return inputGeometry;
			}
			if (inputGeometry[0] is PointN || inputGeometry[0] is MultipointN)
			{
				Geometry[] array = new Geometry[inputGeometry.Length];
				inputGeometry.CopyTo(array, 0);
				return array;
			}
			double maxSegmentLength = 0.0;
			bool useDeviationDensification = true;
			Geometry[] result;
			try
			{
				Geometry[] array2 = this._GeomServer.Densify(inputSpRef, inputGeometry, maxSegmentLength, useDeviationDensification, segmentDeviation);
				result = array2;
			}
			catch (SystemException ex)
			{
				if (base.Parent.Challenge())
				{
					this._GeomServer.Credentials = base.Parent.Credentials;
					if (this._GeomServer.Credentials == null)
					{
						this._GeomServer.UseDefaultCredentials = true;
					}
					this._GeomServer.Proxy = WebRequest.DefaultWebProxy;
					try
					{
						Geometry[] array3 = this._GeomServer.Densify(inputSpRef, inputGeometry, maxSegmentLength, useDeviationDensification, segmentDeviation);
						result = array3;
						return result;
					}
					catch
					{
						string arg_CA_0 = ex.Message;
						ErrorReport.ShowErrorMessage("Geometry Server.Densify:  " + ex.Message);
						result = inputGeometry;
						return result;
					}
				}
				string arg_ED_0 = ex.Message;
				ErrorReport.ShowErrorMessage("Geometry Server.Densify:  " + ex.Message);
				result = inputGeometry;
			}
			return result;
		}

		public void CancelAsync(object userState)
		{
			this._GeomServer.CancelAsync(userState);
		}
	}
}
