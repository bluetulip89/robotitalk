using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.ComponentModel;

namespace Sicily.Robotix
{
	//=========================================================================
	/// <summary>
	/// A base class that represents a basic micro-controller interface. Provides basic
	/// connection and communication services to a serial port based micro-controller 
	/// such as the Arduino or the Parallax Basic Stamp hardware. To use, create a 
	/// class that derives from this class that has specific overrides for your 
	/// robot/micro-controller project.
	/// </summary>
	public class RobotBase : Sicily.Robotix.IRobot
	{
		//=========================================================================
		#region -= declarations =-

		//---- internal bits
		protected System.IO.Ports.SerialPort _serialPort = new SerialPort();
		protected bool _serialPortIsInitialzed;

		//---- events
		public event SerialDataReceivedEventHandler SerialDataReceived;
		public event SerialErrorReceivedEventHandler SerialErrorReceieved;
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= properties =-

		//=========================================================================
		/// <summary>
		/// Provides access to the underlying port. 
		/// </summary>
		public SerialPort Port
		{
			get
			{
				return this._serialPort;
			}
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Whether or not the port is opened.
		/// </summary>
		public bool PortIsOpen
		{
			get { return this._serialPort.IsOpen; }
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// 
		/// </summary>
		public RobotConfiguration Configuration
		{
			get { return this._configuration; }
			set
			{
				this._configuration = value;
				this.RaisePropertySettingsChangedEvent("Configuration");
				//---- make sure we close and update the underlying port
				this.HandlePortSettingsChanged(false);
			}
		}
		protected RobotConfiguration _configuration = new RobotConfiguration();
		//=========================================================================


		#endregion
		//=========================================================================

		//=========================================================================
		#region -= constructors =-

		//=========================================================================
		/// <summary>
		/// Instantiates a new RobotBase class with the following defaults:
		/// Port Name: COM1 \r\n
		/// Baud Rate: 9600 \r\n
		/// Data Bits: 8 \r\n
		/// Parity Bits: None \r\n
		/// Stop Bits: 1 \r\n
		/// Handshake: none \r\n
		/// </summary>
		public RobotBase()
		{
			//---- wire up events
			this._serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
			this._serialPort.ErrorReceived += new SerialErrorReceivedEventHandler(SerialPort_ErrorReceived);
			this._configuration.PortSettings.PropertyChanged +=new PropertyChangedEventHandler(PortSettings_PropertyChanged);
			this._configuration.PropertyChanged += new PropertyChangedEventHandler(_configuration_PropertyChanged);
			
			//---- TODO: wireup the pinchange and see if we can raise a portStatusChange event based on open/close
			// then, subscribe to it on the RobotUIHost page so we can show port open/close

		}

		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Instantiates a new RobotBase class with the passed in PortSettings.
		/// </summary>
		/// <param name="portSettings">The  PortSettings for the underlying port.</param>
		public RobotBase(PortSettings portSettings)
			: this()
		{
			this.Configuration.PortSettings = portSettings;
			//---- make the changes to the underlying port
			this.UpdateUnderlyingPortSettings();
		}
		//=========================================================================


		#endregion
		//=========================================================================


		//=========================================================================
		#region -= event handlers =-

		//=========================================================================
		/// <summary>
		/// DataReceived event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			//---- raise the event
			if (this.SerialDataReceived != null)
			{
				this.SerialDataReceived(this, e);
			}
		}
		//=========================================================================

		//=========================================================================
		protected void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
		{
			//---- raise the event
			if (this.SerialErrorReceieved != null)
			{
				this.SerialErrorReceieved(this, e);
			}
		}
		//=========================================================================

		//=========================================================================
		protected void PortSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
		}
		//=========================================================================

		//=========================================================================
		protected void _configuration_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "PortSettings")
			{
				this.HandlePortSettingsChanged(true);
			}
			this.RaisePropertySettingsChangedEvent("Configuration");
		}
		//=========================================================================


		#endregion
		//=========================================================================

		//=========================================================================
		#region -= protected methods =-

		//=========================================================================
		/// <summary>
		/// Handles when the underlying port settings change
		/// </summary>
		protected void HandlePortSettingsChanged(bool raiseEvent)
		{
			//---- if the port is open, close it
			if (this.PortIsOpen)
			{ this.Close(); }

			//---- make the changes to the underlying port
			this.UpdateUnderlyingPortSettings();

			if (raiseEvent)
			{
				//---- bubble the event up.
				this.RaisePropertySettingsChangedEvent("PortSettings");
			}
		}
		//=========================================================================

		//=========================================================================
		protected void UpdateUnderlyingPortSettings()
		{
			this._serialPort.PortName = this.Configuration.PortSettings.PortName;
			this._serialPort.BaudRate = this.Configuration.PortSettings.BaudRate;
			this._serialPort.DataBits = this.Configuration.PortSettings.DataBits;
			this._serialPort.Parity = this.Configuration.PortSettings.Parity;
			this._serialPort.StopBits = this.Configuration.PortSettings.StopBits;
			this._serialPort.Handshake = this.Configuration.PortSettings.Handshake;
		}
		//=========================================================================

		////=========================================================================
		///// <summary>
		///// Raises the PortSettingsChanged event so that consuming classes know that the underlying settings changed
		///// </summary>
		//protected void RaisePortSettingsChanged()
		//{
		//    if (this.PortSettingsChanged != null) { this.PortSettingsChanged(this, new EventArgs()); }
		//}
		////=========================================================================

		//=======================================================================
		protected void RaisePropertySettingsChangedEvent(string propertyName)
		{
			if (this.PropertyChanged != null) { this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
		}
		//=======================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= public methods =-

		//=========================================================================
		/// <summary>
		/// Opens a connection to the port specified in <see cref="SerialPortName"/>
		/// (if it is not open) and sends the <paramref name="instructionString"/> to 
		/// the device.
		/// </summary>
		/// <remarks>
		/// Does not append a newline character to the end of the string (used to tell 
		/// the chip that the string has terminated). Parallax chips expect a newline 
		/// character, so for them, use the SendInstructions method with the addNewLine 
		/// parameter.
		/// </remarks>
		/// <param name="instructions">the instructions to send.</param>
		public void SendInstructions(string instructions)
		{
			this.SendInstructions(instructions, false);
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Opens a connection to the port specified in <see cref="SerialPortName"/>
		/// (if it is not open) and sends the <paramref name="instructionString"/> to 
		/// the device.
		/// </summary>
		/// <param name="instructionString"></param>
		public void SendInstructions(string instructions, bool addNewline)
		{
			//---- if the port isn't open yet, open it.
			if (!this._serialPort.IsOpen)
			{ this._serialPort.Open(); }

			if (addNewline)
			{
				//---- send the string over the serial port
				// note: writeline adds a newline character to the end which tells 
				// the chip when the string is terminated.
				_serialPort.WriteLine(instructions);
			}
			else { _serialPort.Write(instructions); }
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Opens a connection to the port specified in <see cref="SerialPortName"/>
		/// (if it is not open) and sends the <paramref name="instructionString"/> to 
		/// the device.
		/// </summary>
		/// <remarks>
		/// Does NOT append a newline character. (Not for parallax chips)
		/// </remarks>
		public void SendData(byte[] data)
		{
			//---- if the port isn't open yet, open it.
			if (!this._serialPort.IsOpen)
			{ this._serialPort.Open(); }

			//---- send the string over the serial port
			_serialPort.Write(data, 0, data.Length);
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Opens the underlying port connection to the robot. If you are waiting 
		/// for recieved data, you must keep the port open. However, you should 
		/// close the port when you're finished. 
		/// </summary>
		public void Open()
		{
			//---- if the underlying port isn't open, open it
			if (!this._serialPort.IsOpen)
			{ this._serialPort.Open(); }
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Closes the underlying port connection to the robot.
		/// </summary>
		/// <remarks>
		/// If you are waiting for recieved data, you must keep the port open. 
		/// However, you should close the port when you're finished. 
		/// </remarks>
		public void Close()
		{
			//---- close the port if it's open
			if (this._serialPort.IsOpen)
			{ this._serialPort.Close(); }
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Closes the underlying port connection
		/// </summary>
		public void Dispose()
		{
			//---- close the port
			this.Close();
			//---- dispose the serial port
			this._serialPort.Dispose();
		}
		//=========================================================================

		#endregion
		//=========================================================================

	}
	//=========================================================================
}
