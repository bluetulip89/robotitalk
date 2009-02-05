using System;
using System.Reflection;

namespace Sicily.Robotix.MicroController
{
	//=======================================================================
	public static class AssemblyManager
	{
		//=======================================================================
		public static bool TryLoadAssembly(string assemblyPath, out string message, out Assembly assembly)
		{
			assembly = null;

			try
			{
				//this._assembly = Assembly.ReflectionOnlyLoadFrom(assemblyPath);
				assembly = Assembly.LoadFrom(assemblyPath);
				message = "Assembly loaded successfully";
				return true;
			}

				//Display error if unable to create/load the assembly. 
			catch (System.IO.FileNotFoundException)
			{
				message = "Assemly not found.";
				return false;
			}
			catch (TypeLoadException)
			{
				message = "Error loading types, may not be a valid .net assemly.";
				return false;
			}
			catch (Exception ex)
			{
				message = ("Assembly load error: " + ex.Message);
				return false;
			}
		}
		//=======================================================================

		//=======================================================================
		public static bool TryLoadClass(Assembly assembly, string className, out string message, out object instance)
		{
			instance = null;

			if (assembly == null)
			{
				message = "Assembly must not be null";
				return false;
			}

			try
			{
				instance = assembly.CreateInstance(className);
				message = "Class loaded successfully";
				return true;
			}
			catch (MissingMethodException)
			{
				message = "Error loading class, constructor not found.";
				return false;
			}
			catch (Exception ex)
			{
				message = ("Class load error: " + ex.Message);
				return false;
			}
		}
		//=======================================================================
	}
	//=======================================================================
}
