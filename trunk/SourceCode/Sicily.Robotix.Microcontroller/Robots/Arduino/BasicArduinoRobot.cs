using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sicily.Robotix.Robots.Arduino
{
	//=========================================================================
	public class BasicArduinoRobot : RobotBase
	{
		//=========================================================================
		/// <summary>
		/// Instantiates a new BasicRobot class with the following defaults:
		/// Port Name: COM1 \r\n
		/// Baud Rate: 9600 \r\n
		/// Data Bits: 8 \r\n
		/// Parity Bits: None \r\n
		/// Stop Bits: 1 \r\n
		/// Handshake: none \r\n
		/// </summary>
		public BasicArduinoRobot() : base()
		{
			this.CommonInit();
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Instantiates a new BasicRobot class with the passed in PortSettings.
		/// </summary>
		/// <param name="portSettings">The  PortSettings for the underlying port.</param>
		public BasicArduinoRobot(PortSettings portSettings)
			: base(portSettings)
		{
			this.CommonInit();
		}
		//=========================================================================

		//=========================================================================
		protected void CommonInit()
		{
			base.Configuration.DisplayName = "BASIC ARDUINO";
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Opens a connection to the port specified in <see cref="SerialPortName"/>
		/// (if it is not open) and sends the <paramref name="instructionString"/> to 
		/// the device.
		/// </summary>
		/// <remarks>
		/// Does not append a newline character to the end of the string.
		/// </remarks>
		/// <param name="instructions">the instructions to send.</param>
		public void SendInsructions(string instructions)
		{
			base.SendInstructions(instructions, false);
		}
		//=========================================================================

	}
	//=========================================================================
}
