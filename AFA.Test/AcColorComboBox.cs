using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace AFA.Test
{
	public class AcColorComboBox : ComboBox
	{
		public class ColorItem
		{
			private short _colorIndex;

			private Color _color;

			public short ColorIndex
			{
				get
				{
					return this._colorIndex;
				}
			}

			public Color Color
			{
				get
				{
					return this._color;
				}
			}

			public ColorItem(short colorIndex, Color color)
			{
				this._colorIndex = colorIndex;
				this._color = color;
			}

			public override string ToString()
			{
				return AcColorComboBox.ColorNameOf(this._colorIndex);
			}
		}

		public class ColorItemSorter : IComparer
		{
			public int Compare(object x, object y)
			{
				return (int)(((AcColorComboBox.ColorItem)x).ColorIndex - ((AcColorComboBox.ColorItem)y).ColorIndex);
			}
		}

		private short _colorIndex;

		private IContainer components;

		public short ColorIndex
		{
			get
			{
				return this._colorIndex;
			}
			set
			{
				this._colorIndex = value;
				foreach (AcColorComboBox.ColorItem colorItem in base.Items)
				{
					if (colorItem.ColorIndex == value)
					{
						base.SelectedItem = colorItem;
						break;
					}
				}
			}
		}

		public AcColorComboBox()
		{
			this.InitializeComponent();
			base.DrawMode = DrawMode.OwnerDrawFixed;
			base.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (e.Index >= 0)
			{
				e.DrawBackground();
				e.DrawFocusRectangle();
				Rectangle bounds = e.Bounds;
				bounds.Inflate(-1, -1);
				bounds.Width = 20;
				bounds.Offset(1, 0);
				AcColorComboBox.ColorItem colorItem = (AcColorComboBox.ColorItem)base.Items[e.Index];
				e.Graphics.FillRectangle(new SolidBrush(colorItem.Color), bounds);
				e.Graphics.DrawRectangle(new Pen(Color.Black), bounds);
				e.Graphics.DrawString(colorItem.ToString(), e.Font, new SolidBrush(e.ForeColor), (float)(e.Bounds.X + bounds.Width + 4), (float)e.Bounds.Y);
			}
		}

		protected override void OnCreateControl()
		{
			base.Items.Clear();
			for (short num = 1; num < 256; num += 1)
			{
				base.Items.Add(new AcColorComboBox.ColorItem(num, AcColorComboBox.ColorOf(num)));
			}
			base.OnCreateControl();
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			this._colorIndex = ((AcColorComboBox.ColorItem)base.Items[this.SelectedIndex]).ColorIndex;
			base.OnSelectedIndexChanged(e);
		}

		public static string ColorNameOf(short colorIndex)
		{
			switch (colorIndex)
			{
			case 1:
				return "1 - Red";
			case 2:
				return "2 - Yellow";
			case 3:
				return "3 - Green";
			case 4:
				return "4 - Cyan";
			case 5:
				return "5 - Blue";
			case 6:
				return "6 - Magenta";
			case 7:
				return "7 - White";
			case 8:
				return "8 - Grey";
			default:
				return colorIndex.ToString();
			}
		}

		public static Color ColorOf(short colorIndex)
		{
			switch (colorIndex)
			{
			case 1:
				return Color.FromArgb(255, 0, 0);
			case 2:
				return Color.FromArgb(255, 255, 0);
			case 3:
				return Color.FromArgb(0, 255, 0);
			case 4:
				return Color.FromArgb(0, 255, 255);
			case 5:
				return Color.FromArgb(0, 0, 255);
			case 6:
				return Color.FromArgb(255, 0, 255);
			case 7:
				return Color.FromArgb(255, 255, 255);
			case 8:
				return Color.FromArgb(128, 128, 128);
			case 9:
				return Color.FromArgb(192, 192, 192);
			case 10:
				return Color.FromArgb(255, 0, 0);
			case 11:
				return Color.FromArgb(255, 127, 127);
			case 12:
				return Color.FromArgb(204, 0, 0);
			case 13:
				return Color.FromArgb(204, 102, 102);
			case 14:
				return Color.FromArgb(153, 0, 0);
			case 15:
				return Color.FromArgb(153, 76, 76);
			case 16:
				return Color.FromArgb(127, 0, 0);
			case 17:
				return Color.FromArgb(127, 63, 63);
			case 18:
				return Color.FromArgb(76, 0, 0);
			case 19:
				return Color.FromArgb(76, 38, 38);
			case 20:
				return Color.FromArgb(255, 63, 0);
			case 21:
				return Color.FromArgb(255, 159, 127);
			case 22:
				return Color.FromArgb(204, 51, 0);
			case 23:
				return Color.FromArgb(204, 127, 102);
			case 24:
				return Color.FromArgb(153, 38, 0);
			case 25:
				return Color.FromArgb(153, 95, 76);
			case 26:
				return Color.FromArgb(127, 31, 0);
			case 27:
				return Color.FromArgb(127, 79, 63);
			case 28:
				return Color.FromArgb(76, 19, 0);
			case 29:
				return Color.FromArgb(76, 47, 38);
			case 30:
				return Color.FromArgb(255, 127, 0);
			case 31:
				return Color.FromArgb(255, 191, 127);
			case 32:
				return Color.FromArgb(204, 102, 0);
			case 33:
				return Color.FromArgb(204, 153, 102);
			case 34:
				return Color.FromArgb(153, 76, 0);
			case 35:
				return Color.FromArgb(153, 114, 76);
			case 36:
				return Color.FromArgb(127, 63, 0);
			case 37:
				return Color.FromArgb(127, 95, 63);
			case 38:
				return Color.FromArgb(76, 38, 0);
			case 39:
				return Color.FromArgb(76, 57, 38);
			case 40:
				return Color.FromArgb(255, 191, 0);
			case 41:
				return Color.FromArgb(255, 223, 127);
			case 42:
				return Color.FromArgb(204, 153, 0);
			case 43:
				return Color.FromArgb(204, 178, 102);
			case 44:
				return Color.FromArgb(153, 114, 0);
			case 45:
				return Color.FromArgb(153, 133, 76);
			case 46:
				return Color.FromArgb(127, 95, 0);
			case 47:
				return Color.FromArgb(127, 111, 63);
			case 48:
				return Color.FromArgb(76, 57, 0);
			case 49:
				return Color.FromArgb(76, 66, 38);
			case 50:
				return Color.FromArgb(255, 255, 0);
			case 51:
				return Color.FromArgb(255, 255, 127);
			case 52:
				return Color.FromArgb(204, 204, 0);
			case 53:
				return Color.FromArgb(204, 204, 102);
			case 54:
				return Color.FromArgb(153, 153, 0);
			case 55:
				return Color.FromArgb(153, 153, 76);
			case 56:
				return Color.FromArgb(127, 127, 0);
			case 57:
				return Color.FromArgb(127, 127, 63);
			case 58:
				return Color.FromArgb(76, 76, 0);
			case 59:
				return Color.FromArgb(76, 76, 38);
			case 60:
				return Color.FromArgb(191, 255, 0);
			case 61:
				return Color.FromArgb(223, 255, 127);
			case 62:
				return Color.FromArgb(153, 204, 0);
			case 63:
				return Color.FromArgb(178, 204, 102);
			case 64:
				return Color.FromArgb(114, 153, 0);
			case 65:
				return Color.FromArgb(133, 153, 76);
			case 66:
				return Color.FromArgb(95, 127, 0);
			case 67:
				return Color.FromArgb(111, 127, 63);
			case 68:
				return Color.FromArgb(57, 76, 0);
			case 69:
				return Color.FromArgb(66, 76, 38);
			case 70:
				return Color.FromArgb(127, 255, 0);
			case 71:
				return Color.FromArgb(191, 255, 127);
			case 72:
				return Color.FromArgb(102, 204, 0);
			case 73:
				return Color.FromArgb(153, 204, 102);
			case 74:
				return Color.FromArgb(76, 153, 0);
			case 75:
				return Color.FromArgb(114, 153, 76);
			case 76:
				return Color.FromArgb(63, 127, 0);
			case 77:
				return Color.FromArgb(95, 127, 63);
			case 78:
				return Color.FromArgb(38, 76, 0);
			case 79:
				return Color.FromArgb(57, 76, 38);
			case 80:
				return Color.FromArgb(63, 255, 0);
			case 81:
				return Color.FromArgb(159, 255, 127);
			case 82:
				return Color.FromArgb(51, 204, 0);
			case 83:
				return Color.FromArgb(127, 204, 102);
			case 84:
				return Color.FromArgb(38, 153, 0);
			case 85:
				return Color.FromArgb(95, 153, 76);
			case 86:
				return Color.FromArgb(31, 127, 0);
			case 87:
				return Color.FromArgb(79, 127, 63);
			case 88:
				return Color.FromArgb(19, 76, 0);
			case 89:
				return Color.FromArgb(47, 76, 38);
			case 90:
				return Color.FromArgb(0, 255, 0);
			case 91:
				return Color.FromArgb(127, 255, 127);
			case 92:
				return Color.FromArgb(0, 204, 0);
			case 93:
				return Color.FromArgb(102, 204, 102);
			case 94:
				return Color.FromArgb(0, 153, 0);
			case 95:
				return Color.FromArgb(76, 153, 76);
			case 96:
				return Color.FromArgb(0, 127, 0);
			case 97:
				return Color.FromArgb(63, 127, 63);
			case 98:
				return Color.FromArgb(0, 76, 0);
			case 99:
				return Color.FromArgb(38, 76, 38);
			case 100:
				return Color.FromArgb(0, 255, 63);
			case 101:
				return Color.FromArgb(127, 255, 159);
			case 102:
				return Color.FromArgb(0, 204, 51);
			case 103:
				return Color.FromArgb(102, 204, 127);
			case 104:
				return Color.FromArgb(0, 153, 38);
			case 105:
				return Color.FromArgb(76, 153, 95);
			case 106:
				return Color.FromArgb(0, 127, 31);
			case 107:
				return Color.FromArgb(63, 127, 79);
			case 108:
				return Color.FromArgb(0, 76, 19);
			case 109:
				return Color.FromArgb(38, 76, 47);
			case 110:
				return Color.FromArgb(0, 255, 127);
			case 111:
				return Color.FromArgb(127, 255, 191);
			case 112:
				return Color.FromArgb(0, 204, 102);
			case 113:
				return Color.FromArgb(102, 204, 153);
			case 114:
				return Color.FromArgb(0, 153, 76);
			case 115:
				return Color.FromArgb(76, 153, 114);
			case 116:
				return Color.FromArgb(0, 127, 63);
			case 117:
				return Color.FromArgb(63, 127, 95);
			case 118:
				return Color.FromArgb(0, 76, 38);
			case 119:
				return Color.FromArgb(38, 76, 57);
			case 120:
				return Color.FromArgb(0, 255, 191);
			case 121:
				return Color.FromArgb(127, 255, 223);
			case 122:
				return Color.FromArgb(0, 204, 153);
			case 123:
				return Color.FromArgb(102, 204, 178);
			case 124:
				return Color.FromArgb(0, 153, 114);
			case 125:
				return Color.FromArgb(76, 153, 133);
			case 126:
				return Color.FromArgb(0, 127, 95);
			case 127:
				return Color.FromArgb(63, 127, 111);
			case 128:
				return Color.FromArgb(0, 76, 57);
			case 129:
				return Color.FromArgb(38, 76, 66);
			case 130:
				return Color.FromArgb(0, 255, 255);
			case 131:
				return Color.FromArgb(127, 255, 255);
			case 132:
				return Color.FromArgb(0, 204, 204);
			case 133:
				return Color.FromArgb(102, 204, 204);
			case 134:
				return Color.FromArgb(0, 153, 153);
			case 135:
				return Color.FromArgb(76, 153, 153);
			case 136:
				return Color.FromArgb(0, 127, 127);
			case 137:
				return Color.FromArgb(63, 127, 127);
			case 138:
				return Color.FromArgb(0, 76, 76);
			case 139:
				return Color.FromArgb(38, 76, 76);
			case 140:
				return Color.FromArgb(0, 191, 255);
			case 141:
				return Color.FromArgb(127, 223, 255);
			case 142:
				return Color.FromArgb(0, 153, 204);
			case 143:
				return Color.FromArgb(102, 178, 204);
			case 144:
				return Color.FromArgb(0, 114, 153);
			case 145:
				return Color.FromArgb(76, 133, 153);
			case 146:
				return Color.FromArgb(0, 95, 127);
			case 147:
				return Color.FromArgb(63, 111, 127);
			case 148:
				return Color.FromArgb(0, 57, 76);
			case 149:
				return Color.FromArgb(38, 66, 76);
			case 150:
				return Color.FromArgb(0, 127, 255);
			case 151:
				return Color.FromArgb(127, 191, 255);
			case 152:
				return Color.FromArgb(0, 102, 204);
			case 153:
				return Color.FromArgb(102, 153, 204);
			case 154:
				return Color.FromArgb(0, 76, 153);
			case 155:
				return Color.FromArgb(76, 114, 153);
			case 156:
				return Color.FromArgb(0, 63, 127);
			case 157:
				return Color.FromArgb(63, 95, 127);
			case 158:
				return Color.FromArgb(0, 38, 76);
			case 159:
				return Color.FromArgb(38, 57, 76);
			case 160:
				return Color.FromArgb(0, 63, 255);
			case 161:
				return Color.FromArgb(127, 159, 255);
			case 162:
				return Color.FromArgb(0, 51, 204);
			case 163:
				return Color.FromArgb(102, 127, 204);
			case 164:
				return Color.FromArgb(0, 38, 153);
			case 165:
				return Color.FromArgb(76, 95, 153);
			case 166:
				return Color.FromArgb(0, 31, 127);
			case 167:
				return Color.FromArgb(63, 79, 127);
			case 168:
				return Color.FromArgb(0, 19, 76);
			case 169:
				return Color.FromArgb(38, 47, 76);
			case 170:
				return Color.FromArgb(0, 0, 255);
			case 171:
				return Color.FromArgb(127, 127, 255);
			case 172:
				return Color.FromArgb(0, 0, 204);
			case 173:
				return Color.FromArgb(102, 102, 204);
			case 174:
				return Color.FromArgb(0, 0, 153);
			case 175:
				return Color.FromArgb(76, 76, 153);
			case 176:
				return Color.FromArgb(0, 0, 127);
			case 177:
				return Color.FromArgb(63, 63, 127);
			case 178:
				return Color.FromArgb(0, 0, 76);
			case 179:
				return Color.FromArgb(38, 38, 76);
			case 180:
				return Color.FromArgb(63, 0, 255);
			case 181:
				return Color.FromArgb(159, 127, 255);
			case 182:
				return Color.FromArgb(51, 0, 204);
			case 183:
				return Color.FromArgb(127, 102, 204);
			case 184:
				return Color.FromArgb(38, 0, 153);
			case 185:
				return Color.FromArgb(95, 76, 153);
			case 186:
				return Color.FromArgb(31, 0, 127);
			case 187:
				return Color.FromArgb(79, 63, 127);
			case 188:
				return Color.FromArgb(19, 0, 76);
			case 189:
				return Color.FromArgb(47, 38, 76);
			case 190:
				return Color.FromArgb(127, 0, 255);
			case 191:
				return Color.FromArgb(191, 127, 255);
			case 192:
				return Color.FromArgb(102, 0, 204);
			case 193:
				return Color.FromArgb(153, 102, 204);
			case 194:
				return Color.FromArgb(76, 0, 153);
			case 195:
				return Color.FromArgb(114, 76, 153);
			case 196:
				return Color.FromArgb(63, 0, 127);
			case 197:
				return Color.FromArgb(95, 63, 127);
			case 198:
				return Color.FromArgb(38, 0, 76);
			case 199:
				return Color.FromArgb(57, 38, 76);
			case 200:
				return Color.FromArgb(191, 0, 255);
			case 201:
				return Color.FromArgb(223, 127, 255);
			case 202:
				return Color.FromArgb(153, 0, 204);
			case 203:
				return Color.FromArgb(178, 102, 204);
			case 204:
				return Color.FromArgb(114, 0, 153);
			case 205:
				return Color.FromArgb(133, 76, 153);
			case 206:
				return Color.FromArgb(95, 0, 127);
			case 207:
				return Color.FromArgb(111, 63, 127);
			case 208:
				return Color.FromArgb(57, 0, 76);
			case 209:
				return Color.FromArgb(66, 38, 76);
			case 210:
				return Color.FromArgb(255, 0, 255);
			case 211:
				return Color.FromArgb(255, 127, 255);
			case 212:
				return Color.FromArgb(204, 0, 204);
			case 213:
				return Color.FromArgb(204, 102, 204);
			case 214:
				return Color.FromArgb(153, 0, 153);
			case 215:
				return Color.FromArgb(153, 76, 153);
			case 216:
				return Color.FromArgb(127, 0, 127);
			case 217:
				return Color.FromArgb(127, 63, 127);
			case 218:
				return Color.FromArgb(76, 0, 76);
			case 219:
				return Color.FromArgb(76, 38, 76);
			case 220:
				return Color.FromArgb(255, 0, 191);
			case 221:
				return Color.FromArgb(255, 127, 223);
			case 222:
				return Color.FromArgb(204, 0, 153);
			case 223:
				return Color.FromArgb(204, 102, 178);
			case 224:
				return Color.FromArgb(153, 0, 114);
			case 225:
				return Color.FromArgb(153, 76, 133);
			case 226:
				return Color.FromArgb(127, 0, 95);
			case 227:
				return Color.FromArgb(127, 63, 111);
			case 228:
				return Color.FromArgb(76, 0, 57);
			case 229:
				return Color.FromArgb(76, 38, 66);
			case 230:
				return Color.FromArgb(255, 0, 127);
			case 231:
				return Color.FromArgb(255, 127, 191);
			case 232:
				return Color.FromArgb(204, 0, 102);
			case 233:
				return Color.FromArgb(204, 102, 153);
			case 234:
				return Color.FromArgb(153, 0, 76);
			case 235:
				return Color.FromArgb(153, 76, 114);
			case 236:
				return Color.FromArgb(127, 0, 63);
			case 237:
				return Color.FromArgb(127, 63, 95);
			case 238:
				return Color.FromArgb(76, 0, 38);
			case 239:
				return Color.FromArgb(76, 38, 57);
			case 240:
				return Color.FromArgb(255, 0, 63);
			case 241:
				return Color.FromArgb(255, 127, 159);
			case 242:
				return Color.FromArgb(204, 0, 51);
			case 243:
				return Color.FromArgb(204, 102, 127);
			case 244:
				return Color.FromArgb(153, 0, 38);
			case 245:
				return Color.FromArgb(153, 76, 95);
			case 246:
				return Color.FromArgb(127, 0, 31);
			case 247:
				return Color.FromArgb(127, 63, 79);
			case 248:
				return Color.FromArgb(76, 0, 19);
			case 249:
				return Color.FromArgb(76, 38, 47);
			case 250:
				return Color.FromArgb(51, 51, 51);
			case 251:
				return Color.FromArgb(91, 91, 91);
			case 252:
				return Color.FromArgb(132, 132, 132);
			case 253:
				return Color.FromArgb(173, 173, 173);
			case 254:
				return Color.FromArgb(214, 214, 214);
			case 255:
				return Color.FromArgb(255, 255, 255);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}
