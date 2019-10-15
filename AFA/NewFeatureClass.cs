using AFA.Resources;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AFA
{
    public class NewFeatureClass : Window, IComponentConnector
	{
		public static string[] m_typeStrings = new string[]
		{
			"Point",
			"Polyline",
			"Polygon",
			"Annotation",
			"MultiPatch"
		};

		internal NewFeatureClass This;

		internal TextBox tbName;

		internal ComboBox cbType;

		internal Label lblErrorMessage;

		private bool _contentLoaded;

		public NewFeatureClass()
		{
			this.InitializeComponent();
			this.cbType.ItemsSource = new List<string>(NewFeatureClass.m_typeStrings);
			this.cbType.SelectedIndex = 0;
			this.lblErrorMessage.Content = "";
			this.tbName.Focus();
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
		}

		private void OnClickCancel(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void OnClickNext(object sender, RoutedEventArgs e)
		{
			this.lblErrorMessage.Content = "";
			if (string.IsNullOrEmpty(this.tbName.Text))
			{
				this.lblErrorMessage.Content = AfaStrings.NameCannotBeBlank;
				return;
			}
			string text = this.tbName.Text;
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
			}
			if (string.IsNullOrEmpty(text))
			{
				this.lblErrorMessage.Content = AfaStrings.NameCannotBeBlank;
				return;
			}
			text = NewFeatureClass.FixFeatureClassName(text);
			MSCDataset docDataset = AfaDocData.ActiveDocData.DocDataset;
			MSCFeatureClass mSCFeatureClass = new MSCFeatureClass(docDataset);
			this.tbName.Text = text;
			mSCFeatureClass.Name = text;
			mSCFeatureClass.SetGeometryType(this.cbType.SelectedValue.ToString());
			mSCFeatureClass.Query = new ResultBuffer(new TypedValue[]
			{
				new TypedValue(8, "*")
			});
			mSCFeatureClass.Write(AfaDocData.ActiveDocData.Document);
			docDataset.FeatureClasses.Add(mSCFeatureClass.Name, mSCFeatureClass);
			docDataset.FeatureClassViewList.Add(new FCView(mSCFeatureClass));
			base.Close();
			FeatureClassProperties featureClassProperties = new FeatureClassProperties(mSCFeatureClass);
			try
			{
				Application.ShowModalWindow(featureClassProperties);
			}
			catch (Exception)
			{
			}
			AfaDocData.ActiveDocData.SetActiveFeatureClass(mSCFeatureClass);
			ArcGISRibbon.SetActiveFeatureClass(mSCFeatureClass);
		}

		public static string FixFeatureClassName(string name)
		{
			name = name.Replace("<", "_lt_");
			name = name.Replace(">", "_gt_");
			name = name.Replace("/", ".");
			name = name.Replace("\\", ".");
			name = name.Replace(":", ".");
			name = name.Replace("?", ".");
			name = name.Replace("*", ".");
			name = name.Replace("|", "-");
			name = name.Replace("=", "-");
			name = name.Replace(" ", "_");
			if (!char.IsLetter(name[0]))
			{
				name = "a" + name;
			}
			if (name.Length > 160)
			{
				name = name.Substring(0, 159);
			}
			return MSCDataset.GenerateUniqueName(name);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/ArcGISForAutoCAD;component/ui/newfeatureclass.xaml", UriKind.Relative);
            System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.This = (NewFeatureClass)target;
				return;
			case 2:
				this.tbName = (TextBox)target;
				return;
			case 3:
				this.cbType = (ComboBox)target;
				return;
			case 4:
				this.lblErrorMessage = (Label)target;
				return;
			case 5:
				((Button)target).Click += new RoutedEventHandler(this.OnClickNext);
				return;
			case 6:
				((Button)target).Click += new RoutedEventHandler(this.OnClickCancel);
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
