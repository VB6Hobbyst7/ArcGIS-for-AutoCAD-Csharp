using System.Collections.Generic;

namespace AFA
{
    public interface IAGSLayer
	{
		string Name
		{
			get;
			set;
		}

		int Id
		{
			get;
			set;
		}

		IDictionary<string, object> Properties
		{
			get;
			set;
		}

		IAGSService Service
		{
			get;
			set;
		}

		int parentLayerId
		{
			get;
			set;
		}

		IDictionary<string, AGSField> Fields
		{
			get;
			set;
		}

		SubTypeDictionary Subtypes
		{
			get;
			set;
		}

		AGSRenderer Renderer
		{
			get;
			set;
		}

		string DisplayField
		{
			get;
			set;
		}

		string IDField
		{
			get;
			set;
		}

		string GeomField
		{
			get;
			set;
		}

		string GeometryType
		{
			get;
			set;
		}

		bool IsFeatureLayer
		{
			get;
			set;
		}

		bool IsGroupLayer
		{
			get;
		}

		IDictionary<string, object> ExportProperties
		{
			get;
			set;
		}
	}
}
