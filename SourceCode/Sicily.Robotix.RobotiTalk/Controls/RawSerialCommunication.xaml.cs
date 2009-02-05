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
using Sicily.Robotix.Robots;
using System.Windows.Threading;
using Sicily.Robotix.Robots.Parallax;
using Sicily.Robotix.Robots.Arduino;
using System.ComponentModel;

namespace Sicily.Robotix.MicroController.CommunicationApplication.Controls
{
	/// <summary>
	/// Interaction logic for RawCommunications.xaml
	/// </summary>
	public partial class RawSerialCommunication : UserControl
	{
		//=========================================================================
		#region -= declarations =-

		bool _updatingResponse = false;
		bool _showingError = false;
		StringBuilder _response = new StringBuilder();
		/// <summary>
		/// The maximum size of the response buffer. If the response exceeds this, it removes from the beginning.
		/// </summary>
		int _maxResponseBufferSize = 1000;

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= constructor =-

		//=========================================================================
		public RawSerialCommunication()
		{
			InitializeComponent();

			//this._robot = new BasicArduinoRobot();

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
					this._robot.SerialDataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(_robot_SerialDataReceived);
					this._robot.SerialErrorReceieved += new System.IO.Ports.SerialErrorReceivedEventHandler(_robot_SerialErrorReceieved);
					this._robot.PropertyChanged += new PropertyChangedEventHandler(_robot_PropertyChanged);
					this.txtRobotName.Text = this._robot.Configuration.DisplayName;
				}
			}
		}
		protected IRobot _robot;

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= event handlers =-

		//=========================================================================
		protected void btnClear_Click(object sender, RoutedEventArgs e)
		{ this.txtResponse.Text = ""; }
		//=========================================================================

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
		protected void btnSendInstructions_Click(object sender, RoutedEventArgs e)
		{
			//---- get the data
			//---- set our status
			//this.lblCurrentStatus.Text = "Sending Instruction to R0b0t";
			//---- send the instructions
			//this._robot.SendInstructions(this.txtInstructions.Text);
			//this._robot.SendData(new byte[] { ((byte)(int.Parse(this.txtInstructions.Text))) });

			//----
			switch (this.lstEncoding.SelectionBoxItem.ToString().ToLower())
			{
				case "ascii":
					//---- update our port status
					this.UpdatePortStatus(true);
					this._robot.SendInstructions(this.txtInstructions.Text, (bool)this.chkAppendNewline.IsChecked);
					break;
				case "byte":
					int value;
					//---- try to parse the instructions
					if (int.TryParse(this.txtInstructions.Text, out value))
					{
						//---- update our port status
						this.UpdatePortStatus(true);
						//---- send the data
						this._robot.SendData(new byte[] { ((byte)value) });
					}
					else //---- if we can't parse
					{
						//---- show an err
						MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "If you choose byte array, you must enter numbers.", "Error", MessageBoxButton.OK);
					}
					break;
				case "hex":
					
					break;
			}

		}
		//=========================================================================

		//=========================================================================
		protected void _robot_SerialDataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			//---- make sure our port is still open (in case of weird debugging instances, etc.)
			if (((IRobot)sender).PortIsOpen)
			{
				//---- get the reply from the port
				string reply = ((IRobot)sender).Port.ReadExisting(); //.Replace("\n", "\r\n");

				//---- if we're over our buffer size
				if (this._response.Length >= this._maxResponseBufferSize)
				{
					//---- remove early entries
					int lengthToRemove = this._response.Length - this._maxResponseBufferSize;
					this._response.Remove(0,lengthToRemove);
				}
				
				//---- append the reply to our buffer
				this._response.Append(reply);
				
				//---- use lamda to define a method that sets the text
				Action<string> setText = text =>
				{
					//---- update and scroll to the end
					this.txtResponse.Text = this._response.ToString();
					this.txtResponse.ScrollToEnd();
					//---- clear our update flag
					this._updatingResponse = false;
				};

				//---- if we're not currently already updating the UI
				if (!this._updatingResponse)
				{
					//---- set a flag to prevent updating and thread lock
					this._updatingResponse = true;
					//---- if we're on a different thread, invoke the method on the original thread
					Dispatcher.BeginInvoke(setText, new object[] { reply });
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
		protected void btnConfigureRobot_Click(object sender, RoutedEventArgs e)
		{

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
			//---- change button text
			this.btnOpenClosePort.Content = "Open Port";
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
