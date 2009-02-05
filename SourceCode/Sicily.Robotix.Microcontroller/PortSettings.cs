using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.ComponentModel;

namespace Sicily.Robotix
{
	//=======================================================================
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class PortSettings : ICloneable, INotifyPropertyChanged
	{
		//=======================================================================
		#region -= declarations =-

		//public event EventHandler PortSettingsChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= constructors =-

		//=======================================================================
		/// <summary>
		/// Instantiates a new PortSettings class with the following defaults:
		/// Port Name: COM1,
		/// Baud Rate: 9600,
		/// Data Bits: 8,
		/// Parity Bits: None,
		/// Stop Bits: 1,
		/// Handshake: none,
		/// </summary>
		public PortSettings()
		{ }
		//=======================================================================

		//=======================================================================
		/// <summary>
		/// Instantiates a new RobotBase class with the following defaults:
		/// Baud Rate: 9600,
		/// Data Bits: 8,
		/// Parity Bits: None,
		/// Stop Bits: 1,
		/// Handshake: none,
		/// </summary>
		/// <param name="portName">The name of the port, such as "COM1".</param>
		public PortSettings(string portName)
			: this()
		{
			this._portName = portName;
		}
		//=======================================================================


		//=========================================================================
		/// <summary>
		/// Instantiates a new RobotBase class with the following defaults:
		/// Data Bits: 8,
		/// Parity Bits: None,
		/// Stop Bits: 1,
		/// Handshake: none,
		/// </summary>
		/// <param name="baudRate">The speed of the port. See the BaudRate enumeration for common speeds.</param>
		/// <param name="portName">The name of the port, such as "COM1".</param>
		public PortSettings(string portName, int baudRate)
			: this(portName)
		{
			//---- set the baud rate (the rate that data is sent and received in bits/sec)
			this._baudRate = baudRate;
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Instantiates a new RobotBase class.
		/// </summary>
		/// <param name="baudRate">The speed of the port. See the BaudRate enumeration for common speeds.</param>
		/// <param name="portName">The name of the port, such as "COM1".</param>
		/// <param name="dataBits">The number of bits in each frame of data. Typically 8.</param>
		/// <param name="stopBits">The number of bits used to seperate the data into frames.</param>
		/// <param name="parity">The type of parity bit used to detect errors in the frames. Even, Odd, or None.</param>
		/// <param name="handshake">The type of handshake used.</param>
		public PortSettings(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits, Handshake handshake)
			: this(portName, baudRate)
		{
			//---- set number of bits in each frame of data
			this._dataBits = dataBits;

			//---- parity bit for error detection purposes: even, odd, or none
			this._parity = parity;

			//---- set stop bit to one (used to separate the data into frames)
			this._stopBits = stopBits;

			//---- no handshaking used
			this._handshake = handshake; 
		}
		//=========================================================================


		#endregion
		//=======================================================================

		//=======================================================================
		#region -= properties =-

		//=======================================================================
		/// <summary>
		/// The name of the serial port in which to connect to the micro-controller.
		/// Default is the first port available, or "COM1" if it can't find any.
		/// </summary>
		public string PortName
		{
			get
			{
				if (string.IsNullOrEmpty(this._portName))
				{
					try
					{
						string[] ports = System.IO.Ports.SerialPort.GetPortNames();
						if (ports.Length > 0)
						{ return ports[0]; }
						else { return "COM1"; }
					}
					catch (Exception e)
					{
						return "COM1";
					}
				}
				else { return this._portName; }
			}
			set
			{
				if (this._portName != value)
				{
					this._portName = value;
					this.RaisePropertySettingsChangedEvent("PortName");
				}
			}
		}
		protected string _portName;
		//=======================================================================

		//=======================================================================
		/// <summary>
		/// The speed of the port. See the BaudRate enumeration for common speeds.
		/// Default is 9600.
		/// </summary>
		public int BaudRate
		{
			get { return this._baudRate; }
			set 
			{
				if (this._baudRate != value)
				{
					this._baudRate = value;
					this.RaisePropertySettingsChangedEvent("BaudRate");
				}
			
			}
		}
		protected int _baudRate = 9600;
		//=======================================================================

		//=======================================================================
		/// <summary>
		/// The number of bits in each frame of data. Typically 8.
		/// Default is 8.
		/// </summary>
		public int DataBits
		{
			get { return this._dataBits; }
			set
			{
				if (this._dataBits != value)
				{
					this._dataBits = value;
					this.RaisePropertySettingsChangedEvent("DataBits");
				}
			}
		}
		protected int _dataBits = 8;
		//=======================================================================

		//=======================================================================
		/// <summary>
		/// The type of parity bit used to detect errors in the frames. Even, Odd, or None.
		/// Default is Parity.None.
		/// </summary>
		public System.IO.Ports.Parity Parity
		{
			get { return this._parity; }
			set
			{
				if (this._parity != value)
				{
					this._parity = value;
					this.RaisePropertySettingsChangedEvent("Parity");
				}
			}
		}
		protected Parity _parity = Parity.None;
		//=======================================================================

		//=======================================================================
		/// <summary>
		/// The number of bits used to seperate the data into frames.
		/// Default is StopBits.One.
		/// </summary>
		public System.IO.Ports.StopBits StopBits
		{
			get { return this._stopBits; }
			set
			{
				if (this._stopBits != value)
				{
					this._stopBits = value;
					this.RaisePropertySettingsChangedEvent("StopBits");
				}
			}
		}
		protected StopBits _stopBits = StopBits.One;
		//=======================================================================

		//=======================================================================
		/// <summary>
		/// The type of handshake used.
		/// Default is Handshake.None;
		/// </summary>
		public System.IO.Ports.Handshake Handshake
		{
			get { return this._handshake; }
			set
			{
				if (this._handshake != value)
				{
					this._handshake = value;
					this.RaisePropertySettingsChangedEvent("Handshake");
				}
			}
		}
		protected Handshake _handshake = Handshake.None;
		//=======================================================================

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= protected methods =-

		////=======================================================================
		//protected void RaisePortSettingsChangedEvent()
		//{
		//    if (this.PortSettingsChanged != null) { this.PortSettingsChanged(this, new EventArgs()); }
		//}
		////=======================================================================

		//=======================================================================
		protected void RaisePropertySettingsChangedEvent(string propertyName)
		{
			if (this.PropertyChanged != null) { this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
		}
		//=======================================================================


		#endregion
		//=======================================================================


		#region ICloneable Members

		public object Clone()
		{
			//return this.MemberwiseClone();

			PortSettings portSettings = new PortSettings();

			portSettings.BaudRate = this.BaudRate;
			portSettings.DataBits = this.DataBits;
			portSettings.Handshake = this.Handshake;
			portSettings.Parity = this.Parity;
			portSettings.PortName = this.PortName;
			portSettings.StopBits = this.StopBits;

			return portSettings as object;
		}

		#endregion

		#region INotifyPropertyChanged Members


		#endregion
	}
	//=======================================================================
}
