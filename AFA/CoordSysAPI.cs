using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace AFA
{
    public class CoordSysAPI
	{
		[LispFunction("esri_coordsys_get")]
		public object ESRI_CoordSys_Get(ResultBuffer rb)
		{
			object result;
			try
			{
				Document document = AfaDocData.ActiveDocData.Document;
				string text = MSCPrj.ReadWKT(document);
				if (!string.IsNullOrEmpty(text))
				{
					result = text;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_coordsys_set")]
		public object ESRI_CoordSys_Assign(ResultBuffer rb)
		{
			object result;
			try
			{
				string argument = LspUtil.GetArgument(rb, 0, null);
				if (string.IsNullOrEmpty(argument))
				{
					result = null;
				}
				else
				{
					Document arg_1F_0 = AfaDocData.ActiveDocData.Document;
					if (AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count > 0)
					{
						result = null;
					}
					else if (MSCPrj.AssignPRJFile(argument))
					{
						result = true;
					}
					else
					{
						result = false;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[LispFunction("esri_coordsys_remove")]
		public object ESRI_CoordSys_Remove(ResultBuffer rb)
		{
			object result;
			try
			{
				Document document = AfaDocData.ActiveDocData.Document;
				int num = AfaDocData.ActiveDocData.DocDataset.MapServices.Count + AfaDocData.ActiveDocData.DocDataset.ImageServices.Count + AfaDocData.ActiveDocData.DocDataset.FeatureServices.Count;
				if (num > 0)
				{
					result = null;
				}
				else
				{
					string value = MSCPrj.ReadWKT(document);
					if (string.IsNullOrEmpty(value))
					{
						result = null;
					}
					else
					{
						MSCPrj.RemoveWKT(document);
						result = LspUtil.LispTrue;
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}
}
