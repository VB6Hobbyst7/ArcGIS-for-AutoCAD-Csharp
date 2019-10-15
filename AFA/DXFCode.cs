using AFA.Resources;
using Autodesk.AutoCAD.DatabaseServices;
using System.ComponentModel;

namespace AFA
{
    public class DXFCode : INotifyPropertyChanged
	{
		private int _code;

		public event PropertyChangedEventHandler PropertyChanged;

		public int Code
		{
			get
			{
				return this._code;
			}
			set
			{
				this._code = value;
				if (this.PropertyChanged != null)
				{
					this.PropertyChanged(this, new PropertyChangedEventArgs("Code"));
					this.PropertyChanged(this, new PropertyChangedEventArgs("CodeString"));
				}
			}
		}

		public string CodeString
		{
			get
			{
				int code = this._code;
				switch (code)
				{
				case 2:
					return AfaStrings.BlockName;
				case 3:
				case 4:
				case 5:
					break;
				case 6:
					return AfaStrings.Linetype;
				case 7:
					return AfaStrings.TextStyleName;
				case 8:
					return AfaStrings.Layer;
				default:
					if (code == 62)
					{
						return AfaStrings.Color;
					}
					if (code == 370)
					{
						return AfaStrings.Lineweight;
					}
					break;
				}
				return this._code.ToString();
			}
			set
			{
				if (value.ToLower() == AfaStrings.Layer.ToLower())
				{
					this.Code = 8;
					return;
				}
				if (value.ToLower() == AfaStrings.Color.ToLower())
				{
					this.Code = 62;
					return;
				}
				if (value.ToLower() == AfaStrings.Lineweight.ToLower())
				{
					this.Code = 370;
					return;
				}
				if (value.ToLower() == AfaStrings.Linetype.ToLower())
				{
					this.Code = 6;
					return;
				}
				if (value.ToLower() == AfaStrings.BlockName.ToLower())
				{
					this.Code = 2;
					return;
				}
				if (value.ToLower() == AfaStrings.TextStyleName.ToLower())
				{
					this.Code = 7;
					return;
				}
				try
				{
					this.Code = int.Parse(value);
				}
				catch
				{
					this.Code = 999;
				}
			}
		}

		public DXFCode(int value)
		{
			this.Code = value;
		}

		public DXFCode(string value)
		{
			this.CodeString = value;
		}

		public static bool IsValid(string value, ref string errMessage)
		{
			bool result;
			try
			{
				DXFCode dXFCode = new DXFCode(value);
				if (dXFCode.Code < -5 || dXFCode.Code > 1071)
				{
					errMessage = AfaStrings.DXFCodeError;
					result = false;
				}
				else
				{
					result = true;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool IsValidTypedValue(int code, string value)
		{
			bool result;
			try
			{
				if (code >= -5 && code <= 9)
				{
					result = true;
				}
				else if (code > 9 && code <= 58)
				{
					double num;
					result = double.TryParse(value, out num);
				}
				else if (code >= 60 && code <= 99)
				{
					int num2;
					result = int.TryParse(value, out num2);
				}
				else if (code >= 100 && code <= 105)
				{
					result = true;
				}
				else if (code >= 210 && code <= 230)
				{
					double num3;
					result = double.TryParse(value, out num3);
				}
				else if (code >= 280 && code <= 289)
				{
					short num4;
					result = short.TryParse(value, out num4);
				}
				else if (code >= 290 && code <= 299)
				{
					bool flag;
					result = bool.TryParse(value, out flag);
				}
				else if (code >= 300 && code <= 309)
				{
					result = true;
				}
				else if (code >= 310 && code <= 319)
				{
					result = true;
				}
				else if (code >= 320 && code <= 329)
				{
					result = true;
				}
				else if (code >= 330 && code <= 339)
				{
					result = true;
				}
				else if (code >= 340 && code <= 349)
				{
					result = true;
				}
				else if (code >= 350 && code <= 359)
				{
					result = true;
				}
				else if (code >= 360 && code <= 369)
				{
					result = true;
				}
				else if (code >= 370 && code <= 379)
				{
					short num5;
					result = short.TryParse(value, out num5);
				}
				else if (code >= 380 && code <= 389)
				{
					short num6;
					result = short.TryParse(value, out num6);
				}
				else if (code >= 390 && code <= 399)
				{
					result = true;
				}
				else if (code >= 400 && code <= 409)
				{
					short num7;
					result = short.TryParse(value, out num7);
				}
				else if (code >= 410 && code <= 319)
				{
					result = true;
				}
				else if (code >= 999 && code <= 1005)
				{
					result = true;
				}
				else if (code >= 1010 && code <= 1042)
				{
					double num8;
					result = double.TryParse(value, out num8);
				}
				else if (code == 1070)
				{
					short num9;
					result = short.TryParse(value, out num9);
				}
				else if (code == 1071)
				{
					int num10;
					result = int.TryParse(value, out num10);
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static TypedValue CreateTypedValue(int code, string value)
		{
			if (!DXFCode.IsValidTypedValue(code, value))
			{
				return new TypedValue(3, value);
			}
			TypedValue result;
			try
			{
				if (code >= -5 && code <= 9)
				{
					result = new TypedValue(code, value);
				}
				else if (code > 9 && code <= 58)
				{
					result = new TypedValue(code, double.Parse(value));
				}
				else if (code >= 60 && code <= 99)
				{
					result = new TypedValue(code, int.Parse(value));
				}
				else if (code >= 100 && code <= 105)
				{
					result = new TypedValue(code, value);
				}
				else if (code >= 210 && code <= 230)
				{
					result = new TypedValue(code, double.Parse(value));
				}
				else if (code >= 280 && code <= 289)
				{
					result = new TypedValue(code, short.Parse(value));
				}
				else if (code >= 290 && code <= 299)
				{
					result = new TypedValue(code, bool.Parse(value));
				}
				else if (code >= 300 && code <= 309)
				{
					result = new TypedValue(code, value);
				}
				else if (code >= 310 && code <= 319)
				{
					result = new TypedValue(code, value);
				}
				else if (code >= 320 && code <= 329)
				{
					result = new TypedValue(code, value);
				}
				else if (code >= 330 && code <= 339)
				{
					result = new TypedValue(code, value);
				}
				else if (code >= 340 && code <= 349)
				{
					result = new TypedValue(code, value);
				}
				else if (code >= 350 && code <= 359)
				{
					result = new TypedValue(code, value);
				}
				else if (code >= 360 && code <= 369)
				{
					result = new TypedValue(code, value);
				}
				else if (code >= 370 && code <= 379)
				{
					result = new TypedValue(code, short.Parse(value));
				}
				else if (code >= 380 && code <= 389)
				{
					result = new TypedValue(code, short.Parse(value));
				}
				else if (code >= 390 && code <= 399)
				{
					result = new TypedValue(code, value);
				}
				else if (code >= 400 && code <= 409)
				{
					result = new TypedValue(code, short.Parse(value));
				}
				else if (code >= 410 && code <= 319)
				{
					result = new TypedValue(code, value);
				}
				else if (code >= 999 && code <= 1005)
				{
					result = new TypedValue(code, value);
				}
				else if (code >= 1010 && code <= 1042)
				{
					result = new TypedValue(code, double.Parse(value));
				}
				else if (code == 1070)
				{
					result = new TypedValue(code, short.Parse(value));
				}
				else if (code == 1071)
				{
					result = new TypedValue(code, int.Parse(value));
				}
				else
				{
					result = new TypedValue(3, value);
				}
			}
			catch
			{
				result = new TypedValue(3, value);
			}
			return result;
		}

		public static string TranstateColorIndexToString(short colorIndex)
		{
			if (colorIndex == 256)
			{
				return "ByLayer";
			}
			if (colorIndex == 1)
			{
				return "Red";
			}
			if (colorIndex == 2)
			{
				return "Yellow";
			}
			if (colorIndex == 3)
			{
				return "Green";
			}
			if (colorIndex == 4)
			{
				return "Cyan";
			}
			if (colorIndex == 5)
			{
				return "Blue";
			}
			if (colorIndex == 6)
			{
				return "Magenta";
			}
			if (colorIndex == 7)
			{
				return "White";
			}
			return colorIndex.ToString();
		}

		public static short TranslateColorString(string testString)
		{
			string text = testString.ToLower();
			if (text == "bylayer")
			{
				return 256;
			}
			if (text == "red")
			{
				return 1;
			}
			if (text == "yellow")
			{
				return 2;
			}
			if (text == "green")
			{
				return 3;
			}
			if (text == "cyan")
			{
				return 4;
			}
			if (text == "blue")
			{
				return 5;
			}
			if (text == "magenta")
			{
				return 6;
			}
			if (text == "white")
			{
				return 7;
			}
			return short.Parse(text);
		}
	}
}
