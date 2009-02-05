using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Sicily.Robotix.Robots.Arduino;
using Sicily.Robotix.Robots.Parallax;
using System.Collections.ObjectModel;

namespace Sicily.Robotix.MicroController.CommunicationApplication
{
	//=======================================================================
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		//=======================================================================
		#region -= declarations =-


		#endregion
		//=======================================================================

		//=======================================================================
		#region -= constructor =-

		public App()
		{
			InitializeComponent();
			
			//---- subscribe to the collection changed event so we can make sure to save it
			this.ConfiguredRobots.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.ConfiguredRobots_CollectionChanged);
			this.BuiltInRobots.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.BuiltInRobots_CollectionChanged);

			//---- if there are no robots in there, make sure to add the default ones
			if (CommunicationApplication.Properties.Settings.Default.BuiltInRobots.Count == 0)
			{ this.LoadBuiltInRobots(); }

		}

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= properties =-

		//=======================================================================
		/// <summary>
		/// Provides strongly typed access to the app
		/// </summary>
		public static CommunicationApplication.App CurrentApp
		{
			get { return App.Current as CommunicationApplication.App; }
		}
		//=======================================================================

		//=======================================================================
		public Size WindowSize
		{
			get { return CommunicationApplication.Properties.Settings.Default.WindowSize; }
			set
			{
				CommunicationApplication.Properties.Settings.Default.WindowSize = value;
				CommunicationApplication.Properties.Settings.Default.Save();
			}
		}
		//=======================================================================

		//=======================================================================
		public Double WindowTop
		{
			get { return CommunicationApplication.Properties.Settings.Default.WindowTop; }
			set
			{
				CommunicationApplication.Properties.Settings.Default.WindowTop = value;
				CommunicationApplication.Properties.Settings.Default.Save();
			}
		}
		//=======================================================================

		//=======================================================================
		public Double WindowLeft
		{
			get { return CommunicationApplication.Properties.Settings.Default.WindowLeft; }
			set
			{
				CommunicationApplication.Properties.Settings.Default.WindowLeft = value;
				CommunicationApplication.Properties.Settings.Default.Save();
			}
		}
		//=======================================================================

		//=======================================================================
		public ObservableCollection<IRobot> BuiltInRobots
		{
			get
			{
				//---- make sure to new up our built robots collection if it doesn't exist
				if (CommunicationApplication.Properties.Settings.Default.BuiltInRobots == null)
				{ CommunicationApplication.Properties.Settings.Default.BuiltInRobots = new ObservableCollection<IRobot>(); }

				//---- return the collection
				return CommunicationApplication.Properties.Settings.Default.BuiltInRobots;
			}
			set
			{
				CommunicationApplication.Properties.Settings.Default.BuiltInRobots = value;
				CommunicationApplication.Properties.Settings.Default.Save();
			}
		}
		//=======================================================================

		//=======================================================================
		public ObservableCollection<RobotConfiguration> ConfiguredRobots
		{
			get
			{
				//---- make sure to new up our installed robots if it doesn't exist
				if (CommunicationApplication.Properties.Settings.Default.ConfiguredRobots == null)
				{ CommunicationApplication.Properties.Settings.Default.ConfiguredRobots = new ObservableCollection<RobotConfiguration>(); }
				//---- return the collection
				return CommunicationApplication.Properties.Settings.Default.ConfiguredRobots;
			}
			set
			{
				CommunicationApplication.Properties.Settings.Default.ConfiguredRobots = value;
				CommunicationApplication.Properties.Settings.Default.Save();
			}
		}
		//=======================================================================


		#endregion
		//=======================================================================

		//=======================================================================
		#region -= protected methods =-

		//=======================================================================
		protected void LoadBuiltInRobots()
		{
			////---- add the built in buggers
			//CommunicationApplication.Properties.Settings.Default.BuiltInRobots.Add(new BasicArduinoRobot());
			//CommunicationApplication.Properties.Settings.Default.BuiltInRobots.Add(new BasicParallaxRobot());
			
			////---- save (so we don't have to load them again)
			//CommunicationApplication.Properties.Settings.Default.Save();
			this.BuiltInRobots.Add(new BasicArduinoRobot());
			this.BuiltInRobots.Add(new BasicParallaxRobot());
		}
		//=======================================================================

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= public methods =-

		//=======================================================================
		public bool ConfiguredRobotExists(string robotDisplayName)
		{
			foreach (RobotConfiguration robotConfig in this.ConfiguredRobots)
			{
				if (robotConfig.DisplayName == robotDisplayName) { return true; }
			}
			//---- if we got here, we couldn't find it
			return false;
		}
		//=======================================================================

		//=======================================================================
		public void SaveSettings()
		{
			CommunicationApplication.Properties.Settings.Default.Save();
		}
		//=======================================================================

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= event handlers =-

		//=======================================================================
		protected void ConfiguredRobots_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			//---- the collection changed, so make sure we save the changes
			CommunicationApplication.Properties.Settings.Default.Save();
		}
		//=======================================================================

		//=======================================================================
		protected void BuiltInRobots_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			//---- the collection changed, so make sure we save the changes
			CommunicationApplication.Properties.Settings.Default.Save();
		}
		//=======================================================================

		#endregion
		//=======================================================================

	}
	//=======================================================================
}
