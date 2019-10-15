using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AGOBasemap
{
    [DataContract]
	public class OnlineSearchItem : INotifyPropertyChanged
	{
		private ImageSource _thumbnail;

		public event PropertyChangedEventHandler PropertyChanged;

		public string Id
		{
			get;
			set;
		}

		public string Item
		{
			get;
			set;
		}

		public string ItemType
		{
			get;
			set;
		}

		public string Owner
		{
			get;
			set;
		}

		public string Uploaded
		{
			get;
			set;
		}

		public string Guid
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public string Type
		{
			get;
			set;
		}

		public List<string> TypeKeywords
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public List<string> Tags
		{
			get;
			set;
		}

		public string Snippet
		{
			get;
			set;
		}

		public string Thumbnail
		{
			get;
			set;
		}

		public double[][] Extent
		{
			get;
			set;
		}

		public string LastModified
		{
			get;
			set;
		}

		public string SpatialReference
		{
			get;
			set;
		}

		public string LicenseInfo
		{
			get;
			set;
		}

		public string Access
		{
			get;
			set;
		}

		public string Size
		{
			get;
			set;
		}

		public BitmapImage Image
		{
			get;
			set;
		}

		public string TagList
		{
			get
			{
				string text = "";
				foreach (string current in this.Tags)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += current;
				}
				return text;
			}
		}

		public ImageSource TheThumbnail
		{
			get
			{
				return this._thumbnail;
			}
			set
			{
				this._thumbnail = value;
				this.NotifyPropertyChanged("TheThumbnail");
			}
		}

		private void NotifyPropertyChanged(string propName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}
	}
}
