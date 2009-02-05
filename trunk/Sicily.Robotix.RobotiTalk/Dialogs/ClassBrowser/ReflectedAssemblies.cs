using System.Collections.Generic;
using System.Reflection;

namespace Sicily.Robotix.MicroController.CommunicationApplication.Dialogs
{

	//=========================================================================
	/// <summary>
	/// 
	/// </summary>
	public class ReflectedAssemblies : List<ReflectedAssembly>
	{
		//=========================================================================
		/// <summary>
		/// 
		/// </summary>
		/// <param name="types"></param>
		public ReflectedAssemblies(AssemblyName[] assemblies)
		{
			foreach (AssemblyName assembly in assemblies)
			{
				this.Add(new ReflectedAssembly(assembly));
			}
		}
		//=========================================================================

	}
	//=========================================================================
}
