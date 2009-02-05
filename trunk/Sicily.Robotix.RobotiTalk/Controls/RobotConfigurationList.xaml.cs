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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Sicily.Robotix.MicroController.CommunicationApplication.Controls
{
	/// <summary>
	/// Interaction logic for RobotList.xaml
	/// </summary>
	public partial class RobotConfigurationList : UserControl
	{
		//=======================================================================
		#region -= declarations =-

		public event EventHandler SelectedRobotChanged;

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= properties =-

		//=======================================================================
		/// <summary>
		/// 
		/// </summary>
		public ObservableCollection<RobotConfiguration> RobotConfigurations
		{
			get { return this._robotConfigurations; }
			set
			{
				this._robotConfigurations = value;
				this.lstRobotConfigurations.ItemsSource = this._robotConfigurations;
			}
		}
		protected ObservableCollection<RobotConfiguration> _robotConfigurations = new ObservableCollection<RobotConfiguration>();
		//=======================================================================

		//=======================================================================
		/// <summary>
		/// The select Robot object
		/// </summary>
		public RobotConfiguration SelectedRobot
		{
			get { return this.lstRobotConfigurations.SelectedItem as RobotConfiguration; }
		}
		//=======================================================================

		//=======================================================================
		public int SelectedRobotIndex
		{
			get { return this.lstRobotConfigurations.SelectedIndex; }
		}
		//=======================================================================

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= constructor =-

		//=======================================================================
		public RobotConfigurationList()
		{
			InitializeComponent();
			this.lstRobotConfigurations.ItemsSource = this._robotConfigurations;
		}
		//=======================================================================

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= event handlers =-

		//=======================================================================
		protected void lstRobotConfigurations_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.SelectedRobotChanged != null) { this.SelectedRobotChanged(this, new EventArgs()); }
		}
		//=======================================================================

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= public methods =-

		//=======================================================================
		public void ClearSelection()
		{
			this.lstRobotConfigurations.SelectedItem = null;
		}
		//=======================================================================


		#endregion
		//=======================================================================
	}
}
