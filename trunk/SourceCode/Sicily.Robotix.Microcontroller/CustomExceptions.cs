using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Sicily.Robotix
{
	public class CustomAssemblyNotFound : FileNotFoundException
	{
		public CustomAssemblyNotFound() : base() { }

		public CustomAssemblyNotFound(string message) : base(message) { }

		public CustomAssemblyNotFound(string message, string filename) : base(message, filename) { }
	}

	public class RobotLoadFailedException : Exception
	{
		public RobotLoadFailedException() : base() { }

		public RobotLoadFailedException(string message) : base(message) { }

		public RobotLoadFailedException(string message, Exception innerException) : base(message, innerException) { }
	}
}
