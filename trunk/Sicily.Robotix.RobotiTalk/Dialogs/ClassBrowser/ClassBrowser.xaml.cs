using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;

namespace Sicily.Robotix.MicroController.CommunicationApplication.Dialogs
{
	/// <summary>
	/// Interaction logic for ClassBrowserWindow.xaml
	/// </summary>
	public partial class ClassBrowser : SimpleDialogWindowBase
	{
		//=========================================================================
		#region -= declarations =-

		//---- internal bits
		protected ReflectedAssembly _reflectedAssembly;

		//---- events
		//public static readonly RoutedEvent CancelClickEvent = EventManager.RegisterRoutedEvent("CancelClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ClassBrowser));
		//public static readonly RoutedEvent ClassSelectedEvent = EventManager.RegisterRoutedEvent("ClassSelect", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ClassBrowser));

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= events =-

		//=========================================================================
		///// <summary>
		///// 
		///// </summary>
		//public event RoutedEventHandler CancelClick
		//{
		//    add { AddHandler(CancelClickEvent, value); }
		//    remove { RemoveHandler(CancelClickEvent, value); }
		//}
		//=========================================================================

		//=========================================================================
		///// <summary>
		///// 
		///// </summary>
		//public event RoutedEventHandler ClassSelect
		//{
		//    add { AddHandler(ClassSelectedEvent, value); }
		//    remove { RemoveHandler(ClassSelectedEvent, value); }
		//}
		//=========================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= properties =-

		//=========================================================================
		/// <summary>
		/// The Assembly to browse
		/// </summary>
		public Assembly Assembly
		{
			get { return this._assembly; }
			set
			{
				this._assembly = value;

				this.PopulateTree();
			}
		}
		protected Assembly _assembly;
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// The fully qualified class name
		/// </summary>
		public string ClassName
		{
			get
			{
				if (this.ctlAssemblyTree.SelectedItem != null)
				{
					if (this.ctlAssemblyTree.SelectedItem is ReflectedClass)
					{
						return ((ReflectedClass)this.ctlAssemblyTree.SelectedItem).FullyQualifiedName;
					}
				}
				
				return "";
			}
		}
		//=========================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= constructor =-

		//=========================================================================
		public ClassBrowser()
		{
			InitializeComponent();
		}
		//=========================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= event handlers =-

		//=========================================================================
		protected void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}
		//=========================================================================

		//=========================================================================
		protected void ctlAssemblyTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (this.ctlAssemblyTree.SelectedItem != null)
			{
				if (this.ctlAssemblyTree.SelectedItem is ReflectedClass)
				{
					this.btnOpen.IsEnabled = true;
					return;
				}
			}
			this.btnOpen.IsEnabled = false;
		}
		//=========================================================================

		//=========================================================================
		protected void btnOpen_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}
		//=========================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= protected methods =-

		//=========================================================================
		protected void PopulateTree()
		{
			//---- clear it out
			this.ctlAssemblyTree.Items.Clear();

			//---- 
			this._reflectedAssembly = new ReflectedAssembly(this._assembly);

			TreeViewItem tvItem = new TreeViewItem();
			tvItem.Header = "Assembly";
			tvItem.ItemsSource = this._reflectedAssembly.ContainedClasses;
			this.ctlAssemblyTree.Items.Add(tvItem);

			//this.ctlAssemblyTree.ItemsSource = this._reflectedAssembly.ContainedClasses;

			//this.ctlAssemblyTree.DataContext = this._reflectedAssembly;
		}
		//=========================================================================

		////=========================================================================
		//protected void RaiseCancelClickEvent()
		//{
		//    RaiseEvent(new RoutedEventArgs(CancelClickEvent, this));
		//}
		////=========================================================================

		////=========================================================================
		//protected void RaiseClassSelectEvent()
		//{
		//    RaiseEvent(new RoutedEventArgs(ClassSelectedEvent, this));
		//}
		////=========================================================================

		#endregion
		//=========================================================================
	}
}
