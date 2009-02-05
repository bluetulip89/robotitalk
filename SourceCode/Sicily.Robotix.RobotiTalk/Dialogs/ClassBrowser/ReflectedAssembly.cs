using System;
using System.Reflection;

namespace Sicily.Robotix.MicroController.CommunicationApplication.Dialogs
{
	//=========================================================================
	public class ReflectedAssembly
	{
		//=========================================================================
		#region -= declarations =-

		protected Assembly _wrappedAssembly;

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
			get
			{
				if (this._wrappedAssembly != null)
				{
					return this._wrappedAssembly.GetName().Name;
				}
				else { return this._name; }
			}
		}
		protected string _name = "";
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// 
		/// </summary>
		public string FullQualifiedName
		{
			get
			{
				if (this._wrappedAssembly != null)
				{ return this._wrappedAssembly.FullName; }
				else { return this._fullName; }
			}
		}
		protected string _fullName = "";
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// 
		/// </summary>
		public string LocationPath
		{
			get
			{
				if (this._wrappedAssembly != null)
				{
					return this._wrappedAssembly.Location;
				}
				else { return this._locationPath; }
			}
		}
		protected string _locationPath = "";
		//=========================================================================

		//=========================================================================
		/// <summary>
		/// 
		/// </summary>
		public ReflectedClasses ContainedClasses
		{
			get
			{
				if (this._wrappedAssembly != null)
				{
					if (!this._classesLoaded)
					{
						this._containedClasses = new ReflectedClasses(this._wrappedAssembly.GetTypes());
						this._classesLoaded = true;
					}
					return this._containedClasses;
				}
				else { return new ReflectedClasses(new Type[] { }); }
			}
		}
		protected bool _classesLoaded = false;
		protected ReflectedClasses _containedClasses;
		//=========================================================================


		//=========================================================================
		/// <summary>
		/// 
		/// </summary>
		public ReflectedAssemblies ContainedAssemblies
		{
			get
			{
				if (!this._assembliesLoaded)
				{
					this._containedAssemblies = new ReflectedAssemblies(this._wrappedAssembly.GetReferencedAssemblies());
					this._classesLoaded = true;
				}
				return this._containedAssemblies;
			}
		}
		protected bool _assembliesLoaded = false;
		protected ReflectedAssemblies _containedAssemblies;
		//=========================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= constructor =-

		public ReflectedAssembly(Assembly assembly)
		{
			this._wrappedAssembly = assembly;
		}

		public ReflectedAssembly(AssemblyName assemblyName)
		{
			this._wrappedAssembly = null;
			this._fullName = assemblyName.FullName;

		}


		#endregion
		//=========================================================================

	}
	//=========================================================================
}
