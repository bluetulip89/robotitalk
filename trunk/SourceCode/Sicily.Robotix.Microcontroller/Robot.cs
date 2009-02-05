using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sicily.Robotix.MicroController;
using System.Reflection;

namespace Sicily.Robotix
{
	//=========================================================================
	public static class Robot
	{
		//=========================================================================
		/// <summary>
		/// Creates a new IRobot from the configuration specified.
		/// </summary>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IRobot FromConfiguration(RobotConfiguration configuration)
		{
			//---- declare vars
			IRobot robot = new RobotBase();
			string loadMessage;

			//---- if it has a custom class
			if (configuration.HasCustomClass)
			{
				//---- check to see if the assembly exists
				if (System.IO.File.Exists(configuration.RobotClassAssemblyPath))
				{
					//---- declare vars
					Assembly assembly;
					object robotClass;

					//---- try to load the assembly
					if (AssemblyManager.TryLoadAssembly(configuration.RobotClassAssemblyPath, out loadMessage, out assembly))
					{
						//---- try and load the class
						if (AssemblyManager.TryLoadClass(assembly, configuration.RobotClassName, out loadMessage, out robotClass))
						{
							//---- check to see if it implements IRobot
							if (robotClass is IRobot)
							{
								//---- cast
								robot = robotClass as IRobot;
							}
							else //---- throw an exception
							{ throw new RobotLoadFailedException("Robot load failed, custom class does not implement IRobot"); }
						}
						else //---- throw an exception
						{ throw new RobotLoadFailedException("Robot load failed, custom class could not be loaded.", new Exception(loadMessage)); }
					}
					else //---- throw an exception
					{ throw new RobotLoadFailedException("Robot load failed, custom class assembly could not be loaded.", new Exception(loadMessage)); }
				}
				else //---- throw an exception
				{ throw new RobotLoadFailedException("Robot load failed, custom class assembly not found."); }
			}

			//---- if it has a custom UI
			if (configuration.HasCustomUI)
			{
				//---- check to see if the assembly exists
				if (System.IO.File.Exists(configuration.UIAssemblyPath))
				{
					//---- declare vars
					Assembly assembly;
					object uiClass;

					//---- try to load the assembly
					if (AssemblyManager.TryLoadAssembly(configuration.UIAssemblyPath, out loadMessage, out assembly))
					{
						//---- try and load the class
						if (AssemblyManager.TryLoadClass(assembly, configuration.UIInitialClassName, out loadMessage, out uiClass))
						{
							//---- check to see if it's a UIElement
							if (uiClass is System.Windows.UIElement) { }
							else //---- throw an exception
							{ throw new RobotLoadFailedException("Robot load failed, custom UI class must derive from UIElement"); }
						}
						else //---- throw an exception
						{ throw new RobotLoadFailedException("Robot load failed, custom UI class could not be loaded.", new Exception(loadMessage)); }
					}
					else //---- throw an exception
					{ throw new RobotLoadFailedException("Robot load failed, custom UI class assembly could not be loaded.", new Exception(loadMessage)); }
				}
				else //---- throw an exception
				{ throw new RobotLoadFailedException("Robot load failed, custom UI class assembly not found."); }
			}

			//---- copy the configuration over
			robot.Configuration = configuration;

			//---- if we got here, all is well
			return robot;
		}
		//=========================================================================
	}
}
