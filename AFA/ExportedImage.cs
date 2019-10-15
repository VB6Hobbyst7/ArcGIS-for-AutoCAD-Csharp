using System;
using System.Collections.Generic;

namespace AFA
{
	public class ExportedImage
	{
		public IDictionary<string, object> Properties;

		public string Path
		{
			get;
			set;
		}

		public Uri URI
		{
			get
			{
				return new Uri(this.Path);
			}
		}

		public AGSService Parent
		{
			get;
			set;
		}

		public ExportedImage(AGSService map, string path)
		{
			this.Properties = new Dictionary<string, object>();
			this.Path = path;
			this.Parent = map;
		}
	}
}
