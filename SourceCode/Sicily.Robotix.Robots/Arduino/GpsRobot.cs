using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sicily.Gps;

namespace Sicily.Robotix.Robots.Arduino
{
	//=========================================================================
	/// <summary>
	/// A custom robot that raises GpsDataReceived events and passes out Sicily.Gps.GpsRead data
	/// </summary>
	public class GpsRobot : RobotBase
	{
		//=========================================================================
		#region -= declarations =-

		//---- internal bits
		StringBuilder _responseBuffer = new StringBuilder();
		List<string> _nmeaSentenceBuffer = new List<string>();

		//---- events
		public event EventHandler<GpsDataReceivedEventArgs> GpsDataRecieved;

		#endregion
		//=========================================================================

		//=========================================================================
		public GpsRobot()
		{
			this.SerialDataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(GpsRobot_SerialDataReceived);
		}
		//=========================================================================

		//=========================================================================
		protected void GpsRobot_SerialDataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			//---- buffer the response
			this._responseBuffer.Append(this._serialPort.ReadExisting());

			//---- the minimum sentence size is 14 characters, so if our buffer is at least that big, lets 
			// try and parse the sentences
			if (this._responseBuffer.Length > 14)
			{ this.ParseSentences(); }
		}
		//=========================================================================

		//=========================================================================
		protected void ParseSentences()
		{
			//---- declare vars
			int sentenceStartIndex = -1;
			int sentenceEndIndex = 0;
			int sentenceLength = 0;

			//---- see if we can find an entire NMEA sentence (starts with '$', ends with line feed)
			for (int i = 0; i < this._responseBuffer.Length; i++)
			{
				//---- if we found a NMEA sentence start
				if (this._responseBuffer[i] == '$')
				{ sentenceStartIndex = i; }

				//---- if we found the end of the sentence
				if (sentenceStartIndex > -1 && this._responseBuffer[i] == '\r')
				{
					//---- get the end index and compute the length
					sentenceEndIndex = i;
					sentenceLength = sentenceEndIndex - sentenceStartIndex;
					
					//---- copy the sentence 
					char[] sentenceChars = new char[sentenceLength];
					this._responseBuffer.CopyTo(sentenceStartIndex, sentenceChars, 0, sentenceLength);
					string sentence = new string(sentenceChars);

					//---- add the new sentence
					this._nmeaSentenceBuffer.Add(sentence);

					//---- if the sentence is $GPRMC
					if (sentence.StartsWith("$GPRMC"))
					{
						//---- raise the event that we've got a complete set of NMEA sentences
						this.RaiseGpsDataReceivedEventArgs();

						//---- clear the sentence buffer (cause we're starting over)
						this._nmeaSentenceBuffer.Clear();
					}
					
					//---- clear the sentence out of the main response buffer (so we don't parse it again)
					this._responseBuffer.Remove(0, sentenceEndIndex + 1);

					//---- return out of the for loop
					return;
				}
			}
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// Raises our gps data received event in a thread safe way
		/// </summary>
		public void RaiseGpsDataReceivedEventArgs()
		{
			//---- raise the event
			if (this.GpsDataRecieved != null) { this.GpsDataRecieved(this, new GpsDataReceivedEventArgs(GpsReading.Parse(this._nmeaSentenceBuffer))); }
		}
		//=========================================================================


		//=========================================================================
		/// <summary>
		/// 
		/// </summary>
		public class GpsDataReceivedEventArgs : EventArgs
		{
			public GpsReading GpsReading
			{
				get { return this._gpsReading; }
				set { this._gpsReading = value; }
			}
			protected GpsReading _gpsReading = new GpsReading();


			public GpsDataReceivedEventArgs() : base() { }

			public GpsDataReceivedEventArgs(GpsReading gpsReading)
				: base()
			{
				this._gpsReading = gpsReading;
			}
		}
		//=========================================================================

	}
	//=========================================================================


}
