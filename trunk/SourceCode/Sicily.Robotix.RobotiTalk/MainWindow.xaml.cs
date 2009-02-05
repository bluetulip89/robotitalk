using System.Windows;
using System.Windows.Input;
using Sicily.Robotix.MicroController.CommunicationApplication.Controls;
using System.Reflection;
using System.Collections.Generic;
using System;
using Sicily.Robotix.MicroController.CommunicationApplication.Dialogs;

namespace Sicily.Robotix.MicroController.CommunicationApplication
{
	//=======================================================================
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//=======================================================================
		#region -= declarations =-

		//---- controls
		ManageRobots _manageRobots;
		Dictionary<string, RawSerialCommunication> _commWindows = new Dictionary<string,RawSerialCommunication>();

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= constructor =-
	
		public MainWindow()
		{
			InitializeComponent();

			//---- bind our app close command
			CommandBinding closeBinding = new CommandBinding(ApplicationCommands.Close);
			closeBinding.Executed += new ExecutedRoutedEventHandler(closeBinding_Executed);
			this.CommandBindings.Add(closeBinding);

			//---- load user settings
			this.LoadUserSettings();
			
			this.lstConfiguredRobots.ItemsSource = App.CurrentApp.ConfiguredRobots;
			this.lstBuiltInRobots.ItemsSource = App.CurrentApp.BuiltInRobots;
		}

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= event handlers =-

		//=======================================================================
		protected void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
		}
		//=======================================================================

		//=======================================================================
		protected void closeBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			this.Shutdown();
		}
		//=======================================================================

		//=======================================================================
		protected void rctBanner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) { this.DragMove(); }
		//=======================================================================

		//=======================================================================
		protected void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
		{
		}
		//=======================================================================

		//=======================================================================
		protected void btnManageInstalledRobots_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.ShowManageRobots();
		}
		//=======================================================================

		//=======================================================================
		protected void btnExit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.Shutdown();
		}
		//=======================================================================

		//=======================================================================
		protected void Window_LocationChanged(object sender, System.EventArgs e)
		{
		}
		//=======================================================================

		//=======================================================================
		protected void lstConfiguredRobots_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (this.lstConfiguredRobots.SelectedItem != null)
			{
				//---- declare vars
				RobotConfiguration selectedRobotConfiguration;
				IRobot robot = null;

				//---- clear the selection on the built in robots
				this.lstBuiltInRobots.SelectedItem = null;

				//---- get the selected robot
				selectedRobotConfiguration = this.lstConfiguredRobots.SelectedItem as RobotConfiguration;

				//---- if it doesn't have a custom UI
				if (!selectedRobotConfiguration.HasCustomUI)
				{
					//---- if it's not already running
					if (this._commWindows.ContainsKey(selectedRobotConfiguration.DisplayName))
					{
						//---- hide the other controls
						this.HideControls();
						//---- show that one
						this._commWindows[selectedRobotConfiguration.DisplayName].Visibility = Visibility;
					}
					else //---- create a new one
					{
						//---- try to instantiate the robot
						try
						{ robot = Robot.FromConfiguration(selectedRobotConfiguration); }
						catch (Exception ex)
						{
							//---- throw up our message box
							MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), ex.Message, "Robot Load ERRRRRRRRRRRR", MessageBoxButton.OK);
							//---- return out
							return;
						}
						//---- show the raw communication page
						this.ShowRawCommunication(robot);
					}
				}
				else
				{
					//TODO: shit's gotta go here
				}
			}
		}
		//=======================================================================

		//=======================================================================
		protected void lstBuiltInRobots_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (this.lstBuiltInRobots.SelectedItem != null)
			{
				//---- clear the selection on the configured in robots
				this.lstConfiguredRobots.SelectedItem = null;

				//---- show the raw communication page
				this.ShowRawCommunication(this.lstBuiltInRobots.SelectedItem as IRobot);
			}
		}
		//=======================================================================

		//=======================================================================
		protected void btnAbout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			//---- throw up our message box
			About about = new About();
			about.Owner = Window.GetWindow(this);
			about.Show();
		}
		//=======================================================================

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= protected methods =-

		//=======================================================================
		protected void LoadUserSettings()
		{
			//---- set the size of the window
			if (App.CurrentApp.WindowSize.Height != 0 && App.CurrentApp.WindowSize.Width != 0)
			{
				this.Width = App.CurrentApp.WindowSize.Width;
				this.Height = App.CurrentApp.WindowSize.Height;
			}

			//---- set the window location
			this.Top = App.CurrentApp.WindowTop;
			this.Left = App.CurrentApp.WindowLeft;
		
		}
		//=======================================================================

		//=======================================================================
		protected void Shutdown()
		{
			//---- save our settings
			this.SaveSettings();
			//---- TODO: close all ports, dispose
			Application.Current.Shutdown();
		}
		//=======================================================================

		//=======================================================================
		protected void SaveSettings()
		{
			//---- size
			App.CurrentApp.WindowSize = new Size(this.Width, this.Height);
			//---- location
			App.CurrentApp.WindowLeft = this.Left;
			App.CurrentApp.WindowTop = this.Top;
		}
		//=======================================================================

		//=======================================================================
		protected void ClearRobotSelections()
		{
			this.lstBuiltInRobots.SelectedItem = null;
			this.lstConfiguredRobots.SelectedItem = null;
		}
		//=======================================================================

		//=======================================================================
		#region -= control show / hide methods =-

		//=======================================================================
		protected void ShowManageRobots()
		{
			//---- instanitate
			this._manageRobots = new ManageRobots();

			//---- unselect the robots on the left
			this.ClearRobotSelections();

			//---- initialize
			//this._manageRobots.Initialize()

			//---- wire up events

			//---- add to the page
			this.grdMainContent.Children.Add(this._manageRobots);
		}
		//=======================================================================

		//=======================================================================
		protected void CloseManageRobots()
		{
			this.grdMainContent.Children.Remove(this._manageRobots);
		}
		//=======================================================================

		//=======================================================================
		/// <summary>
		/// If there is a window for that robot already, it shows the control. otherwise
		/// it creates a new raw communication control for that robot, adds it to the 
		/// page, and shows it.
		/// </summary>
		/// <param name="robot"></param>
		/// <param name="robotName"></param>
		protected void ShowRawCommunication(IRobot robot)
		{
			//---- hide other controls
			this.HideControls();

			//---- check to see if there is a window for that robot already
			if (this._commWindows.ContainsKey(robot.Configuration.DisplayName))
			{
				//---- show it
				this._commWindows[robot.Configuration.DisplayName].Visibility = Visibility.Visible;
			}
			//---- if there isn't a window already
			else
			{
				//---- instantiate a new one
				RawSerialCommunication rawComm = new RawSerialCommunication();

				//---- assign the robot to it
				rawComm.Robot = robot;

				//---- add it to the list of them
				this._commWindows.Add(robot.Configuration.DisplayName, rawComm);

				//---- add it to the grid
				this.grdMainContent.Children.Add(rawComm);

				//---- make sure it's visible (especially important if we've hidden it once already)
				rawComm.Visibility = Visibility.Visible;
			}

		}
		//=======================================================================

		//=======================================================================
		/// <summary>
		/// Hides all the elements in the main grid.
		/// </summary>
		protected void HideControls()
		{
			//---- loop through each element in the main content grid
			foreach (UIElement element in this.grdMainContent.Children)
			{
				//---- hide the element
				element.Visibility = Visibility.Hidden;
			}
		}
		//=======================================================================

		#endregion
		//=======================================================================

		#endregion
		//=======================================================================

	}
}
