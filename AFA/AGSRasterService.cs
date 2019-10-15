using System;
using System.Collections.Generic;
using System.IO;

namespace AFA
{
	public abstract class AGSRasterService : AGSService
	{
		public AGSRasterService()
		{
		}

		public virtual bool SupportsDisconnect()
		{
			return true;
		}

		public AGSRasterService(string serviceFullName, AGSConnection parent)
		{
			base.Parent = parent;
			base.FullName = serviceFullName;
			base.Name = Path.GetFileName(base.FullName);
			base.Type = AGSType.AGSService;
			base.Properties = new Dictionary<string, object>();
			base.Properties.Add("Name", base.FullName);
			base.Properties.Add("Server", base.Parent.URL);
		}

		public virtual bool ExportMapNow(AGSExportOptions eo, ExportImageEventHandler action, AGSExportOptions cancelOptions)
		{
			throw new NotImplementedException();
		}

		public virtual ITask GetExportTask(AGSExportOptions eo, HiddenUpdateForm form)
		{
			throw new NotImplementedException();
		}
	}
}
