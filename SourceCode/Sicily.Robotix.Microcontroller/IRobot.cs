using System;
using System.ComponentModel;
namespace Sicily.Robotix
{
	public interface IRobot : IDisposable, INotifyPropertyChanged
	{
		//=======================================================================
		#region -= properties =-
	
		/// <summary>
		/// The underlying serial port
		/// </summary>
		System.IO.Ports.SerialPort Port { get; }

		/// <summary>
		/// Whether or not the underlying port is open
		/// </summary>
		bool PortIsOpen { get; }

		RobotConfiguration Configuration { get; set; }

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= events =-
		
		//event EventHandler PortSettingsChanged;
		
		event System.IO.Ports.SerialDataReceivedEventHandler SerialDataReceived;
		
		event System.IO.Ports.SerialErrorReceivedEventHandler SerialErrorReceieved;

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= methods =-

		void Close();

		void Open();
		
		void SendData(byte[] data);

		void SendInstructions(string instructions);

		void SendInstructions(string instructions, bool addNewline);

		#endregion
		//=======================================================================


	}
}
