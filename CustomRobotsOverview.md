# Introduction #

RobotiTalk supports custom robots and custom robot user interfaces. If you want to build a custom robot, but don't need specify a custom UI, RobotiTalk will automatically use the built in Raw Serial Communicator.

In order to add a custom robot to RobotiTalk, your robot class must implement IRobot.

In order to add a custom robot UI to RobotiTalk, your robot UI class must derive from UIElement and implement IRobotUI.

To add custom robots, click the "Manage Robots" link at the bottom of RobotiTalk and then click "new robot."


# Sample Robots #
For more information on sample robots included with RobotiTalk, check out the SampleRobots page.

# Building Your Own #
Fore more information on how to build your own custom robots, check out the BuildingCustomRobots and [BuildingCustomRobotUIs](BuildingCustomRobotUIs.md) page.