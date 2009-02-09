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
using Sicily.Gps;
using System.ComponentModel;

namespace Sicily.Robotix.Robots.Arduino
{
	/// <summary>
	/// Interaction logic for GpsRobotUI.xaml
	/// </summary>
	public partial class GpsRobotUI : UserControl, IRobotUI
	{
		//=========================================================================
		#region -= declarations =-

		GpsReading _gpsReading;

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= properties =-

		/// <summary>
		/// Must be of type GpsRobot
		/// </summary>
		public IRobot Robot
		{
			get { return this._robot; }
			set
			{
				if (!(value is GpsRobot))
				{ throw new InvalidCastException("Robot must be of type GpsRobot"); }

				this._robot = value as GpsRobot;

				//---- wire up our events
				this._robot.GpsDataRecieved += new EventHandler<GpsRobot.GpsDataReceivedEventArgs>(_robot_GpsDataRecieved);
			}
		}
		protected GpsRobot _robot;

		public string Title
		{
			get { return "GPS Robot Interface"; }
		}

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= constructor =-

		public GpsRobotUI()
		{
			InitializeComponent();
		}

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= event handlers =-

		//=========================================================================
		protected void _robot_GpsDataRecieved(object sender, GpsRobot.GpsDataReceivedEventArgs e)
		{
			//---- save our gps reading
			this._gpsReading = e.GpsReading;
		
			//---- use lamda to define a delegate to update the UI
			Action updateUI = () => { this.UpdateUI(); };

			//---- invoke the updateui on the main thread
			Dispatcher.BeginInvoke(updateUI, new object[] { });
		}
		//=========================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= protected methods =-

		//=========================================================================
		protected void UpdateUI()
		{
			this.txtLatitude.Text = this._gpsReading.Summary.Position.Latitude.ToString();
			this.txtLongitude.Text = this._gpsReading.Summary.Position.Longitude.ToString();
			this.txtGroundSpeed.Text = this._gpsReading.Summary.GroundSpeed.ToString();
			this.txtHeading.Text = this._gpsReading.Summary.Heading.ToString();
			this.PopulateSatellites();
		}
		//=========================================================================

		//=========================================================================
		protected void PopulateSatellites()
		{
			//---- clear our sats in view 
			this.txtSatellitesInView.Text = "";

			foreach (GsvData gsv in this._gpsReading.SatellitesInView)
			{
				foreach (Satellite sat in gsv.Satellites)
				{
					this.txtSatellitesInView.Text += "ID: [" + sat.ID.ToString() + "] strength: [" + sat.SignalStrength + "]\r\n";
				}

				this.txtNumberOfSatellitesInView.Text = gsv.SatellitesInView.ToString();
			}

		}
		//=========================================================================

		#endregion
		//=========================================================================
	}
}
