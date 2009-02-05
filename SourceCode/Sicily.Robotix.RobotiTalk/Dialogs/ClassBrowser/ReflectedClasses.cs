using System;
using System.Collections.Generic;

namespace Sicily.Robotix.MicroController.CommunicationApplication.Dialogs
{
	//=========================================================================
	/// <summary>
	/// 
	/// </summary>
	public class ReflectedClasses : List<ReflectedClass>
	{
		//=========================================================================
		/// <summary>
		/// 
		/// </summary>
		/// <param name="types"></param>
		public ReflectedClasses(Type[] types)
		{
			foreach (Type type in types)
			{
				this.Add(new ReflectedClass(type));
			}
		}
		//=========================================================================

	}
	//=========================================================================
}
