using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sicily.Robotix
{
	public interface IRobotUI
	{
		IRobot Robot { get; set; }
		string Title { get; }
	}
}
