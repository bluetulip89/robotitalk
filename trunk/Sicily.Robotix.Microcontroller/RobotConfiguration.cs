using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Sicily.Robotix
{
	//=======================================================================
	[Serializable]
	public class RobotConfiguration : INotifyPropertyChanged
	{

		//=======================================================================
		#region -= declarations =-

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= constructor =-

		//=======================================================================
		public RobotConfiguration() { }
		//=======================================================================

		//=======================================================================
		public RobotConfiguration(string displayName, bool hasCustomClass, bool hasCustomUI, PortSettings portSettings)
			: this()
		{
			this._displayName = displayName;
			this._hasCustomClass = hasCustomClass;
			this._hasCustomUI = hasCustomUI;
			this._portSettings = portSettings;
		}
		//=======================================================================

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= properties =-

		/// <summary>
		/// The name of the robot
		/// </summary>
		public string DisplayName
		{
			get { return this._displayName; }
			set
			{
				this._displayName = value;
				this.RaisePropertySettingsChangedEvent("DisplayName");
			}
		}
		protected string _displayName;

		/// <summary>
		/// Whether or not the robot has a custom class
		/// </summary>
		public bool HasCustomClass
		{
			get { return this._hasCustomClass; }
			set
			{
				this._hasCustomClass = value;
				this.RaisePropertySettingsChangedEvent("HasCustomClass");
			}
		}
		protected bool _hasCustomClass;

		/// <summary>
		/// Whether or not the robot has a custom user interface
		/// </summary>
		public bool HasCustomUI
		{
			get { return this._hasCustomUI; }
			set
			{
				this._hasCustomUI = value;
				this.RaisePropertySettingsChangedEvent("HasCustomUI");
			}
		}
		protected bool _hasCustomUI;

		/// <summary>
		/// The fully qualified name of the custom robot class e.g, MyRobot.CoolOnes.TheGreatestRobotEver.
		/// This class should implement IRobot.
		/// </summary>
		public string RobotClassName
		{
			get { return this._robotClassName; }
			set
			{
				this._robotClassName = value;
				this.RaisePropertySettingsChangedEvent("RobotClassName");
			}
		}
		protected string _robotClassName;

		/// <summary>
		/// The fully qualified path to the assembly that contains the robot. e.g. "c:\myRobotProjects\RobotProject1\RobotProject1.dll"
		/// </summary>
		public string RobotClassAssemblyPath
		{
			get { return this._robotClassAssemblyPath; }
			set
			{
				this._robotClassAssemblyPath = value;
				this.RaisePropertySettingsChangedEvent("RobotClassAssemblyPath");
			}
		}
		protected string _robotClassAssemblyPath;


		/// <summary>
		/// The fully qualified name of the custom UI for the robot. 
		/// </summary>
		public string UIInitialClassName
		{
			get { return this._uiInitialClassName; }
			set
			{
				this._uiInitialClassName = value;
				this.RaisePropertySettingsChangedEvent("UIInitialClassName");
			}
		}
		protected string _uiInitialClassName;

		/// <summary>
		/// The fully qualified path to the assembly that contains the UI robot. e.g. "c:\myRobotProjects\RobotProject1\RobotProject1UI.dll"
		/// </summary>
		public string UIAssemblyPath
		{
			get { return this._uiAssemblyPath; }
			set
			{
				this._uiAssemblyPath = value;
				this.RaisePropertySettingsChangedEvent("UIAssemblyPath");
			}
		}
		protected string _uiAssemblyPath;

		/// <summary>
		/// The port settings for the robot
		/// </summary>
		public PortSettings PortSettings
		{
			get { return this._portSettings; }
			set
			{
				this._portSettings = value;
				this.RaisePropertySettingsChangedEvent("PortSettings");
			}
		}
		protected PortSettings _portSettings = new PortSettings();

		#endregion
		//=======================================================================

		//=======================================================================
		#region -= protected methods =-

		//=======================================================================
		protected void RaisePropertySettingsChangedEvent(string propertyName)
		{
			if (this.PropertyChanged != null) { this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
		}
		//=======================================================================

		#endregion
		//=======================================================================

	}
}
