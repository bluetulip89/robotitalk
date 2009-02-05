using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sicily.Robotix.Robots.Parallax
{
	//=========================================================================
	public class BasicParallaxRobot : RobotBase
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
		public BasicParallaxRobot() : base()
		{
			this.CommonInit();
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Instantiates a new BasicRobot class with the passed in PortSettings.
		/// </summary>
		/// <param name="portSettings">The  PortSettings for the underlying port.</param>
		public BasicParallaxRobot(PortSettings portSettings)
			: base(portSettings)
		{
			this.CommonInit();
		}
		//=========================================================================

		//=========================================================================
		protected void CommonInit()
		{
			base.Configuration.DisplayName = "BASIC PARALLAX";
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Opens a connection to the port specified in <see cref="SerialPortName"/>
		/// (if it is not open) and sends the <paramref name="instructionString"/> to 
		/// the device.
		/// </summary>
		/// <remarks>
		/// Appends a newline character to the end of the string (used to tell 
		/// the chip that the string has terminated). Parallax chips expect a newline 
		/// character.
		/// </remarks>
		/// <param name="instructions">the instructions to send.</param>
		public void SendInsructions(string instructions)
		{
			base.SendInstructions(instructions, true);
		}
		//=========================================================================

	}
	//=========================================================================
}
