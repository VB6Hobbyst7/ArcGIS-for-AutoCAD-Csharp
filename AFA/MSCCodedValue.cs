using Autodesk.AutoCAD.DatabaseServices;

namespace AFA
{
    public class MSCCodedValue
	{
		public string DisplayName
		{
			get;
			set;
		}

		public object Value
		{
			get;
			set;
		}

		protected string SymbolName
		{
			get;
			set;
		}

		public override string ToString()
		{
			return this.DisplayName.ToString();
		}

		public MSCCodedValue(string name, CadField.CadFieldType type, object value)
		{
			this.DisplayName = name;
			this.Value = CadField.CreateTypedValue(type, value.ToString()).Value;
		}

		public MSCCodedValue(string name, object value)
		{
			this.DisplayName = name;
			this.Value = value;
		}

		public ResultBuffer CreateResultBuffer()
		{
			ResultBuffer resultBuffer = new ResultBuffer();
			resultBuffer.Add(new TypedValue(5005, this.DisplayName));
			resultBuffer.Add(CadField.CreateTypedValue(this.Value));
			return resultBuffer;
		}
	}
}
