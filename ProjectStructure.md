# Introduction #
The project is a Visual Studio 2008 project. The solution file contains several projects:
**Sicily.Robotix.MicroController** Sicily.Robotix.RobotiTalk
**Sicily.Robotix.Robots** Sicily.WPF.Validators

## Sicily.Robotix.MicroController ##
The main base classes. Includes IRobot, RobotBase, RobotConfiguration, etc. If you're creating your own IRobot class, you'll need to reference this assembly/project.

## Sicily.Robotix.RobotiTalk ##
The actual WPF application. Contains all the windows, controls, etc.

## Sicily.Robotix.Robots ##
Contains the built-in robots that derive from IRobot.

## Sicily.WPF.Validators ##
Contains WPF validators. Will probably be deleted, since I'm not using them.