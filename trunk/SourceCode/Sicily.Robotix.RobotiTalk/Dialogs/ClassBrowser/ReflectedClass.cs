using System;

namespace Sicily.Robotix.MicroController.CommunicationApplication.Dialogs
{
	//=========================================================================
	public class ReflectedClass
	{
		//=========================================================================
		#region -= declarations =-

		protected Type _underlyingType;

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= properties =-

		//=========================================================================
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			get { return this._underlyingType.Name; }
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// 
		/// </summary>
		public string FullyQualifiedName
		{
			get { return this._underlyingType.Namespace + "." + this._underlyingType.Name; }
		}
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// 
		/// </summary>
		public ReflectedClasses ContainedClasses
		{
			get
			{
				if (!this._classesLoaded)
				{
					this._containedClasses = new ReflectedClasses(this._underlyingType.GetNestedTypes());
					this._classesLoaded = true;
				}
				return this._containedClasses;
			}
		}
		protected bool _classesLoaded = false;
		protected ReflectedClasses _containedClasses;
		//=========================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= constructor =-

		public ReflectedClass(Type type)
		{
			this._underlyingType = type;
		}

		#endregion
		//=========================================================================

	}
	//=========================================================================
}
