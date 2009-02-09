using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Sicily.Robotix.MicroController.CommunicationApplication.Controls
{
	//=========================================================================
	/// <summary>
	/// Hosts the Robot UI. handles port opening/closing/errors
	/// </summary>
	public partial class RobotUIHost : UserControl
	{
		//=========================================================================
		#region -= declarations =-

		bool _showingError = false;

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= constructor =-

		//=========================================================================
		public RobotUIHost()
		{
			InitializeComponent();
		}
		//=========================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= properties =-

		public IRobot Robot
		{
			get { return this._robot; }
			set
			{
				this._robot = value;
				if (value != null)
				{
					//---- wire up handlers
					this._robot.SerialErrorReceieved += new System.IO.Ports.SerialErrorReceivedEventHandler(_robot_SerialErrorReceieved);
					this._robot.PropertyChanged += new PropertyChangedEventHandler(_robot_PropertyChanged);
					
					//---- set the robot name
					this.txtRobotName.Text = this._robot.Configuration.DisplayName;

					//---- if we have the UI, set the robot on it
					if (this._robotUI != null)
					{ (this._robotUI as IRobotUI).Robot = this.Robot; }
				}
			}
		}
		protected IRobot _robot;


		public UIElement RobotUI
		{
			get { return this._robotUI; }
			set
			{
				this._robotUI = value;

				if (this._robotUI != null)
				{
					//---- if we have the robot, set the UI on it
					if (this._robot != null)
					{ (this._robotUI as IRobotUI).Robot = this.Robot; }

					//---- add the robot ui to the page
					this.grdMainRobotUI.Children.Add(this._robotUI);
					//---- set the title
					this.txtTitle.Text = (this._robotUI as IRobotUI).Title;
				}
			}
		}
		protected UIElement _robotUI;


		#endregion
		//=========================================================================

		//=========================================================================
		#region -= event handlers =-

		//=========================================================================
		protected void btnOpenClosePort_Click(object sender, RoutedEventArgs e)
		{
			//---- if the port is open, close it and update the button and status
			if (this._robot.PortIsOpen)
			{
				this._robot.Close();
				this.UpdatePortStatus(false);
			}
			//---- if the port isn't open, open it and update the interface
			else
			{
				try
				{
					this._robot.Open();
					this.UpdatePortStatus(true);
				}
				catch (Exception ex)
				{
					Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "Could not open port. Check to see if a robot with that port already has it open. Message: " + ex.Message, "Open Port Error", MessageBoxButton.OK);
				}
			}
		}
		//=========================================================================

		//=========================================================================
		protected void _robot_SerialErrorReceieved(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
		{
			//---- use lamda to define a method that sets the text
			Action showError = () =>
			{
				this.UpdatePortStatus(false);
				MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "serial error has occured", "Serial Error", MessageBoxButton.OK);
				this._showingError = false;

			};

			//---- if the error isn't up already
			if (!this._showingError)
			{
				//----
				this._showingError = true;
				//---- close the port
				this._robot.Port.Close();

				//---- if we're on a different thread, invoke the method on the original thread
				Dispatcher.BeginInvoke(showError, new object[] { });
			}
		}
		//=========================================================================

		//=========================================================================
		protected void _robot_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "PortSettings")
			{
				this.HandlePortSettingsChange();
			}
		}
		//=========================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= protected methods =-

		//=========================================================================
		public void HandlePortSettingsChange()
		{
			this.UpdatePortStatus(this.Robot.Port.IsOpen);
		}
		//=========================================================================

		//=========================================================================
		protected void UpdatePortStatus(bool portIsOpen)
		{
			if (portIsOpen)
			{
				this.btnOpenClosePort.Content = "Close Port";
				this.txtPortStatus.Text = "port opened";
				this.imgPortClosed.Visibility = Visibility.Collapsed;
				this.imgPortOpen.Visibility = Visibility.Visible;
			}
			else
			{
				this.btnOpenClosePort.Content = "Open Port";
				this.txtPortStatus.Text = "port closed";
				this.imgPortClosed.Visibility = Visibility.Visible;
				this.imgPortOpen.Visibility = Visibility.Collapsed;
			}
		}
		//=========================================================================

		#endregion
		//=========================================================================

	}
}
