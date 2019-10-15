using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using DataTable = System.Data.DataTable;

namespace AFA
{
    public class MSCMapLayer
	{
		public string Name
		{
			get;
			set;
		}

		public int ID
		{
			get;
			set;
		}

		public int ParentLayerID
		{
			get;
			set;
		}

		public MSCMapService ParentMap
		{
			get;
			set;
		}

		public bool Visible
		{
			get;
			set;
		}

		public List<MSCMapLayer> Layers
		{
			get;
			set;
		}

		public List<int> ChildLayerIDs
		{
			get;
			set;
		}

		public DataTable IdentifyResults
		{
			get;
			set;
		}

		public MSCMapLayer()
		{
			this.Layers = new List<MSCMapLayer>();
			this.ChildLayerIDs = new List<int>();
		}

		public MSCMapLayer(Transaction t, string name, ObjectId lyrDictionaryID)
		{
			this.Name = name;
			this.Layers = new List<MSCMapLayer>();
			this.ChildLayerIDs = new List<int>();
			this.ReadMapLayer(t, lyrDictionaryID);
		}

		public void ReadMapLayer(Transaction t, ObjectId lyrDictionaryID)
		{
			DBDictionary dBDictionary = (DBDictionary)t.GetObject(lyrDictionaryID, 0);
			this.ID = (int)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)90, "ID");
			this.ParentLayerID = (int)DocUtil.ReadXRecord(t, dBDictionary, (DxfCode)90, "ParentID");
			this.Visible = false;
			if (dBDictionary.Contains("MapLayerName"))
			{
				Xrecord xrecord = (Xrecord)t.GetObject(dBDictionary.GetAt("MapLayerName"), 0);
				TypedValue[] array = xrecord.Data.AsArray();
				for (int i = 0; i < array.Length; i++)
				{
					TypedValue typedValue = array[i];
					if (typedValue.TypeCode == 1)
					{
						this.Name = typedValue.Value.ToString();
						break;
					}
				}
			}
			if (dBDictionary.Contains("Visible"))
			{
				Xrecord xrecord2 = (Xrecord)t.GetObject(dBDictionary.GetAt("Visible"), 0);
				TypedValue[] array2 = xrecord2.Data.AsArray();
				for (int j = 0; j < array2.Length; j++)
				{
					TypedValue typedValue2 = array2[j];
					if (typedValue2.TypeCode == 290)
					{
						this.Visible = (0 != Convert.ToInt16(typedValue2.Value));
						break;
					}
				}
			}
			this.Layers = new List<MSCMapLayer>();
			if (dBDictionary.Contains("ChildLayers"))
			{
				DBDictionary dBDictionary2 = (DBDictionary)t.GetObject(dBDictionary.GetAt("ChildLayers"), 0);
				using (DbDictionaryEnumerator enumerator = dBDictionary2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DBDictionaryEntry current = enumerator.Current;
						MSCMapLayer item = new MSCMapLayer(t, current.Key, current.Value);
						this.Layers.Add(item);
					}
				}
			}
		}

		private string FixName(string inputString)
		{
			string text = inputString.Trim();
			text = text.Replace(":[; \\/:*?\"<>|&']", "-");
			return text.Replace(":", "-");
		}

		public DBDictionary WriteMapLayer(DBDictionary parentDict, Transaction t)
		{
			DBDictionary dBDictionary = new DBDictionary();
			try
			{
				parentDict.SetAt(this.FixName(this.Name), dBDictionary);
				dBDictionary.DisableUndoRecording(true);
				t.AddNewlyCreatedDBObject(dBDictionary, true);
				DocUtil.WriteXRecord(t, dBDictionary, "MapLayerName", (DxfCode)1, this.Name);
				DocUtil.WriteXRecord(t, dBDictionary, "ID", (DxfCode)90, this.ID);
				DocUtil.WriteXRecord(t, dBDictionary, "ParentID", (DxfCode)90, this.ParentLayerID);
				if (this.Visible)
				{
					Xrecord xrecord = new Xrecord();
					xrecord.Data=(new ResultBuffer(new TypedValue[]
					{
						new TypedValue(290, true)
					}));
					dBDictionary.SetAt("Visible", xrecord);
					xrecord.DisableUndoRecording(true);
					t.AddNewlyCreatedDBObject(xrecord, true);
				}
			}
			catch
			{
			}
			try
			{
				if (this.Layers != null && this.Layers.Count > 0)
				{
					DBDictionary dBDictionary2 = new DBDictionary();
					dBDictionary.SetAt("ChildLayers", dBDictionary2);
					t.AddNewlyCreatedDBObject(dBDictionary2, true);
					foreach (MSCMapLayer current in this.Layers)
					{
						current.WriteMapLayer(dBDictionary2, t);
					}
				}
			}
			catch
			{
			}
			return dBDictionary;
		}
	}
}
