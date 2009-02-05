using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Sicily.Robotix.MicroController.CommunicationApplication.Dialogs;

namespace Sicily.Robotix.MicroController.CommunicationApplication.Controls
{
	/// <summary>
	/// Interaction logic for ManageRobots.xaml
	/// </summary>
	public partial class ManageRobots : UserControl
	{
		//=======================================================================
		#region -= declarations =-

		protected RobotConfiguration _selectedRobotConfiguration;
		protected bool _selectedRobotConfigurationIsBuiltIn;
		protected PageMode _pageMode;

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= constructor =-

		//=======================================================================
		public ManageRobots()
		{
			InitializeComponent();
		}
		//=======================================================================

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= properties =-



		#endregion
		//=======================================================================

		//=======================================================================
		#region -= event handlers =-

		//=======================================================================
		protected void ManageRobots_Loaded(object sender, RoutedEventArgs e)
		{
			//---- populate the lists of robot configurations
			this.PopulateRobotLists();
		}
		//=======================================================================

		//=======================================================================
		protected void lstBuiltInRobotConfigurations_SelectedRobotChanged(object sender, EventArgs e)
		{
			if (this.lstBuiltInRobotConfigurations.SelectedRobot != null)
			{
				this._selectedRobotConfiguration = this.lstBuiltInRobotConfigurations.SelectedRobot;
				this.lstConfiguredRobots.ClearSelection();
				this._selectedRobotConfigurationIsBuiltIn = true;
				this._pageMode = PageMode.Edit;
				this.PopulateRobotDetails();
				this.btnRemoveRobot.IsEnabled = false;
			}
		}
		//=======================================================================

		//=======================================================================
		protected void lstConfiguredRobots_SelectedRobotChanged(object sender, EventArgs e)
		{
			if (this.lstConfiguredRobots.SelectedRobot != null)
			{
				this._selectedRobotConfiguration = this.lstConfiguredRobots.SelectedRobot;
				this.lstBuiltInRobotConfigurations.ClearSelection();
				this._selectedRobotConfigurationIsBuiltIn = false;
				this._pageMode = PageMode.Edit;
				this.PopulateRobotDetails();
				this.btnRemoveRobot.IsEnabled = true;
			}
			else
			{
				this.btnRemoveRobot.IsEnabled = false;
			}
		}
		//=======================================================================

		//=======================================================================
		private void btnUpdateRobot_Click(object sender, RoutedEventArgs e)
		{
			this.AddUpdateRobot();
		}
		//=======================================================================

		//=======================================================================
		private void btnAddNewRobot_Click(object sender, RoutedEventArgs e)
		{
			this.ResetRobotDetails();
			this.ShowRobotDetailsSection();
			this._selectedRobotConfiguration = new RobotConfiguration();
			this._pageMode = PageMode.Add;
		}
		//=======================================================================

		//=======================================================================
		private void btnBrowseClassPath_Click(object sender, RoutedEventArgs e)
		{
			//---- create a file dialogue
			Microsoft.Win32.OpenFileDialog fileDialogue = new Microsoft.Win32.OpenFileDialog();
			//---- set it to filter only .dll files
			fileDialogue.Filter = "Assemblies and Executables (*.dll, *.exe)|*.dll;*.exe"; 
			//---- if the user selected a file
			if ((bool)fileDialogue.ShowDialog())
			{
				//---- put the name and path of the file in the text box
				this.txtClassAssemblyPath.Text = fileDialogue.FileName;
			}
		}
		//=======================================================================

		//=======================================================================
		private void btnBrowseClassName_Click(object sender, RoutedEventArgs e)
		{
			//---- declare vars
			string loadMessage;
			ClassBrowser classBrowser;
			Assembly assembly;

			if (AssemblyManager.TryLoadAssembly(this.txtClassAssemblyPath.Text, out loadMessage, out assembly))
			{
				//---- instiantiate
				classBrowser = new ClassBrowser();

				//---- set data
				classBrowser.Assembly = assembly;
				classBrowser.Owner = Window.GetWindow(this);
				if ((bool)classBrowser.ShowDialog())
				{
					this.txtClassName.Text = classBrowser.ClassName;
				}
			}
			else
			{
				//---- throw a kick ass message box
				MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), loadMessage, "Error Loading Assembly", MessageBoxButton.OK);
			}
		}
		//=======================================================================

		//=======================================================================
		private void btnBrowseUIPath_Click(object sender, RoutedEventArgs e)
		{
			//---- create a file dialogue
			Microsoft.Win32.OpenFileDialog fileDialogue = new Microsoft.Win32.OpenFileDialog();
			//---- set it to filter only .dll files
			fileDialogue.Filter = "Assemblies and Executables (*.dll, *.exe)|*.dll;*.exe";
			//---- if the user selected a file
			if ((bool)fileDialogue.ShowDialog())
			{
				//---- put the name and path of the file in the text box
				this.txtUIAssemblyPath.Text = fileDialogue.FileName;
			}

		}
		//=======================================================================

		//=======================================================================
		private void btnBrowseUIClassName_Click(object sender, RoutedEventArgs e)
		{
			//---- declare vars
			string loadMessage;
			ClassBrowser classBrowser;
			Assembly assembly;

			if (AssemblyManager.TryLoadAssembly(this.txtUIAssemblyPath.Text, out loadMessage, out assembly))
			{
				//---- instiantiate
				classBrowser = new ClassBrowser();

				//---- set data
				classBrowser.Assembly = assembly;
				classBrowser.Owner = Window.GetWindow(this);
				if ((bool)classBrowser.ShowDialog())
				{
					this.txtUIClassName.Text = classBrowser.ClassName;
				}
			}
			else
			{
				//---- throw a kick ass message box
				MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), loadMessage, "Error Loading Assembly", MessageBoxButton.OK);
			}
		}
		//=======================================================================

		//=======================================================================
		private void chkHasCustomClass_Checked(object sender, RoutedEventArgs e)
		{
			this.grdCustomClassSettings.Visibility = Visibility.Visible;
		}
		//=======================================================================

		//=======================================================================
		private void chkHasCustomUI_Checked(object sender, RoutedEventArgs e)
		{
			this.grdCustomUISettings.Visibility = Visibility.Visible;
		}
		//=======================================================================

		//=======================================================================
		private void chkHasCustomClass_Unchecked(object sender, RoutedEventArgs e)
		{
			this.grdCustomClassSettings.Visibility = Visibility.Collapsed;
		}
		//=======================================================================

		//=======================================================================
		private void chkHasCustomUI_Unchecked(object sender, RoutedEventArgs e)
		{
			this.grdCustomUISettings.Visibility = Visibility.Collapsed;
		}
		//=======================================================================

		//=======================================================================
		protected void btnRemoveRobot_Click(object sender, RoutedEventArgs e)
		{
			//---- remove the robot
			App.CurrentApp.ConfiguredRobots.Remove(this._selectedRobotConfiguration);
			//---- 
			this.HideRobotDetailsSection();
		}
		//=======================================================================

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= protected methods =-

		//=======================================================================
		protected void PopulateRobotLists()
		{
			//---- bind the configured robots
			this.lstConfiguredRobots.RobotConfigurations = App.CurrentApp.ConfiguredRobots;

			//---- loop through the built-in robots and add their configurations
			foreach(IRobot robot in App.CurrentApp.BuiltInRobots)
			{
				this.lstBuiltInRobotConfigurations.RobotConfigurations.Add(robot.Configuration);
			}
		}
		//=======================================================================

		//=======================================================================
		protected void PopulateRobotDetails()
		{
			//---- show the details pane
			this.grdRobotDetails.Visibility = Visibility.Visible;

			//---- if it's a built in robot, only the port settings are editable, so hide the other
			if (this._selectedRobotConfigurationIsBuiltIn)
			{ this.pnlCustomRobotSettings.Visibility = Visibility.Collapsed; }
			else //---- if it's a custom robot, set visibility
			{
				//---- show the settings
				this.pnlCustomRobotSettings.Visibility = Visibility.Visible; 
				
				//---- set the display name
				this.txtDisplayName.Text = this._selectedRobotConfiguration.DisplayName;

				//---- set custom class stuff, and do the visibility
				this.chkHasCustomClass.IsChecked = this._selectedRobotConfiguration.HasCustomClass;
				if (this._selectedRobotConfiguration.HasCustomClass)
				{
					this.grdCustomClassSettings.Visibility = Visibility.Visible;
					this.txtClassAssemblyPath.Text = this._selectedRobotConfiguration.RobotClassAssemblyPath;
					this.txtClassName.Text = this._selectedRobotConfiguration.RobotClassName;
				}
				else { this.grdCustomClassSettings.Visibility = Visibility.Collapsed; }

				//---- set the custom UI stuff, and visibility
				this.chkHasCustomUI.IsChecked = this._selectedRobotConfiguration.HasCustomUI;
				if (this._selectedRobotConfiguration.HasCustomUI)
				{
					this.grdCustomUISettings.Visibility = Visibility.Visible;
					this.txtUIAssemblyPath.Text = this._selectedRobotConfiguration.UIAssemblyPath;
					this.txtUIClassName.Text = this._selectedRobotConfiguration.UIInitialClassName;
				}
				else { this.grdCustomUISettings.Visibility = Visibility.Collapsed; }
			}

			//---- populate the port settings
			this.ctlPortSettings.PortSettings = this._selectedRobotConfiguration.PortSettings;
		}
		//=======================================================================

		//=======================================================================
		protected void AddUpdateRobot()
		{
			if (this.PageIsValid())
			{
				//---- copy the port settings over
				this._selectedRobotConfiguration.PortSettings = this.ctlPortSettings.PortSettings;

				//---- if it is built in
				if (this._selectedRobotConfigurationIsBuiltIn)
				{
					App.CurrentApp.SaveSettings();
				}
				//---- if it's not a built in robot, we have to save more data
				else 
				{
					this._selectedRobotConfiguration.DisplayName = this.txtDisplayName.Text;
					this._selectedRobotConfiguration.HasCustomClass = (bool)this.chkHasCustomClass.IsChecked;
					this._selectedRobotConfiguration.HasCustomUI = (bool)this.chkHasCustomUI.IsChecked;

					if (this._selectedRobotConfiguration.HasCustomClass)
					{
						this._selectedRobotConfiguration.RobotClassAssemblyPath = this.txtClassAssemblyPath.Text;
						this._selectedRobotConfiguration.RobotClassName = this.txtClassName.Text;
					}
					if (this._selectedRobotConfiguration.HasCustomUI)
					{
						this._selectedRobotConfiguration.UIAssemblyPath = this.txtUIAssemblyPath.Text;
						this._selectedRobotConfiguration.UIInitialClassName = this.txtUIClassName.Text;
					}

					//---- if we're in add mode, add the robot
					if (this._pageMode == PageMode.Add)
					{
						App.CurrentApp.ConfiguredRobots.Add(this._selectedRobotConfiguration);
					}
					//---- otherwise, update it
					else
					{
						//---- agh, this doesn't raise the change event, so we have to manually save
						App.CurrentApp.ConfiguredRobots[this.lstConfiguredRobots.SelectedRobotIndex] = this._selectedRobotConfiguration;
						App.CurrentApp.SaveSettings();
						//App.CurrentApp.ConfiguredRobots.Remove(this._selectedRobotConfiguration);
						//App.CurrentApp.ConfiguredRobots.Add(this._selectedRobotConfiguration);
					}
				}

				//----
				this.ResetRobotDetails();
				this.HideRobotDetailsSection();
				this.lstBuiltInRobotConfigurations.ClearSelection();
				this.lstConfiguredRobots.ClearSelection();
			}
		}
		//=======================================================================

		//=======================================================================
		protected bool PageIsValid()
		{
			//---- if it's a built in robot
			if (!this._selectedRobotConfigurationIsBuiltIn)
			{
				//---- check display name
				if(string.IsNullOrEmpty(this.txtDisplayName.Text))
				{
					//---- throw an error
					MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "Display Name is required.", "Error", MessageBoxButton.OK);
					return false;
				}

				//---- make sure there isn't a robot with that name already
				if (this._selectedRobotConfiguration.DisplayName != this.txtDisplayName.Text && App.CurrentApp.ConfiguredRobotExists(this.txtDisplayName.Text))
				{
					//---- throw an error
					MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "A robot with that name already exists.", "Error", MessageBoxButton.OK);
					return false;
				}

				//---- if it's supposed to have a custom class
				if ((bool)this.chkHasCustomClass.IsChecked)
				{
					//---- make sure it has an assembly specified
					if (string.IsNullOrEmpty(this.txtClassAssemblyPath.Text))
					{
						//---- show the error
						MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "If a custom class is checked, you must specify the assembly in which the class resides.", "Error", MessageBoxButton.OK);
						return false;
					}

					//---- make sure it has a class specified
					if (string.IsNullOrEmpty(this.txtClassName.Text))
					{
						//---- show the error
						MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "If a custom class is checked, you must specify the class in the assembly.", "Error", MessageBoxButton.OK);
						return false;
					}

					//---- make sure it has a proper assembly
					string loadMessage; Assembly assembly;
					if (!AssemblyManager.TryLoadAssembly(this.txtClassAssemblyPath.Text, out loadMessage, out assembly))
					{
						//---- show the error
						MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), loadMessage, "Error", MessageBoxButton.OK);
						return false;
					}
					else
					{
						//---- declare vars
						object customClass;

						//---- make sure we can instance the class
						try
						{
							//---- instance the class
							customClass = assembly.CreateInstance(this.txtClassName.Text);

							//---- check to make sure it implements IRobot
							if (!(customClass is IRobot))
							{
								//---- show the error
								MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "The custom robot class must implement IRobot.", "Error", MessageBoxButton.OK);
								return false;
							}
						}
						catch (Exception e)
						{
							//---- show the error
							MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "The custom robot class could not be instantiated: " + e.Message, "Error", MessageBoxButton.OK);
							return false;
						}
					}
				}


				//---- if it's supposed to have a custom UI class
				if ((bool)this.chkHasCustomUI.IsChecked)
				{
					//---- make sure it has an assembly specified
					if (string.IsNullOrEmpty(this.txtUIAssemblyPath.Text))
					{
						//---- show the error
						MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "If a custom UI class is checked, you must specify the assembly in which the class resides.", "Error", MessageBoxButton.OK);
						return false;
					}

					//---- make sure it has a class specified
					if (string.IsNullOrEmpty(this.txtUIClassName.Text))
					{
						//---- show the error
						MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "If a custom UI class is checked, you must specify the class in the assembly.", "Error", MessageBoxButton.OK);
						return false;
					}

					//---- make sure it has a proper assembly
					string loadMessage; Assembly assembly;
					if (!AssemblyManager.TryLoadAssembly(this.txtUIAssemblyPath.Text, out loadMessage, out assembly))
					{
						//---- show the error
						MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), loadMessage, "Error", MessageBoxButton.OK);
						return false;
					}
					else
					{
						//---- declare vars
						object customClass;

						//---- make sure we can instance the class
						try
						{
							//---- instance the class
							customClass = assembly.CreateInstance(this.txtClassName.Text);

							//---- check to make sure it implements IRobotUI
							if (!(customClass is UIElement))
							{
								//---- show the error
								MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "The custom robot UI class must implement IRobotUI.", "Error", MessageBoxButton.OK);
								return false;
							}
						}
						catch (Exception e)
						{
							//---- show the error
							MessageBoxResult result = Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "The custom robot UI class could not be instantiated: " + e.Message, "Error", MessageBoxButton.OK);
							return false;
						}
					}
				}

			}

			//---- validate the port settings too
			if (!this.ctlPortSettings.PageIsValid())
			{ return false; }

			//---- if we got here, everything is hunky dori
			return true;
		}
		//=======================================================================

		//=======================================================================
		protected void ResetRobotDetails()
		{
			this.txtDisplayName.Text = "";
			this.chkHasCustomClass.IsChecked = false;
			this.chkHasCustomClass.IsChecked = false;
			this.txtClassAssemblyPath.Text = "";
			this.txtClassName.Text = "";
			this.txtUIAssemblyPath.Text = "";
			this.txtUIClassName.Text = "";
			this.ctlPortSettings.ResetToDefaults();
		}
		//=======================================================================

		//=======================================================================
		protected void ShowRobotDetailsSection()
		{
			this.grdRobotDetails.Visibility = Visibility.Visible;
			this.pnlCustomRobotSettings.Visibility = Visibility.Visible;
			this.grdCustomClassSettings.Visibility = Visibility.Collapsed;
			this.grdCustomUISettings.Visibility = Visibility.Collapsed;
		}
		//=======================================================================

		//=======================================================================
		protected void HideRobotDetailsSection()
		{
			this.grdRobotDetails.Visibility = Visibility.Hidden;
			this.pnlCustomRobotSettings.Visibility = Visibility.Collapsed;
			this.grdCustomClassSettings.Visibility = Visibility.Collapsed;
			this.grdCustomUISettings.Visibility = Visibility.Collapsed;
		}
		//=======================================================================

		#endregion
		//=======================================================================


		protected enum PageMode
		{
			Add,
			Edit
		}
	}
	//=======================================================================
}
