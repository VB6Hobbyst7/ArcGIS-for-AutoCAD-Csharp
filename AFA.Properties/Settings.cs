using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AFA.Properties
{
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"), CompilerGenerated]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		[DefaultSettingValue("600"), UserScopedSetting, DebuggerNonUserCode]
		public double ConnectionHeight
		{
			get
			{
				return (double)this["ConnectionHeight"];
			}
			set
			{
				this["ConnectionHeight"] = value;
			}
		}

		[DefaultSettingValue("400"), UserScopedSetting, DebuggerNonUserCode]
		public double ConnectionWidth
		{
			get
			{
				return (double)this["ConnectionWidth"];
			}
			set
			{
				this["ConnectionWidth"] = value;
			}
		}

		[DefaultSettingValue("20"), UserScopedSetting, DebuggerNonUserCode]
		public double ConnectionTop
		{
			get
			{
				return (double)this["ConnectionTop"];
			}
			set
			{
				this["ConnectionTop"] = value;
			}
		}

		[DefaultSettingValue("20"), UserScopedSetting, DebuggerNonUserCode]
		public double ConnectionLeft
		{
			get
			{
				return (double)this["ConnectionLeft"];
			}
			set
			{
				this["ConnectionLeft"] = value;
			}
		}

		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public double ConnectionState
		{
			get
			{
				return (double)this["ConnectionState"];
			}
			set
			{
				this["ConnectionState"] = value;
			}
		}
	}
}
