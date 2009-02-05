using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sicily.Robotix.Robots.Arduino
{
	//=========================================================================
	public class LEDCounter01 : Sicily.Robotix.Robots.Arduino.BasicArduinoRobot
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
		public LEDCounter01() : base()
		{
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Instantiates a new BasicRobot class with the passed in PortSettings.
		/// </summary>
		/// <param name="portSettings">The  PortSettings for the underlying port.</param>
		public LEDCounter01(PortSettings portSettings)
			: base(portSettings)
		{
		}
		//=========================================================================


	}
	//=========================================================================
}
