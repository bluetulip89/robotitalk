//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Collections.ObjectModel;

//namespace Sicily.Robotix.MicroController.CommunicationApplication
//{
//    //=======================================================================
//    /// <summary>
//    /// Use this rather than the Settings class directly, because this 
//    /// automatically saves the settings
//    /// </summary>
//    public class ApplicationSettings
//    {
//        //=======================================================================
//        #region -= constructor =-

//        //=======================================================================
//        /// <summary>
//        /// Singleton contructor
//        /// </summary>
//        /// <param name="currentContext"></param>
//        internal ApplicationSettings(Sicily.Robotix.MicroController.CommunicationApplication.App currentContext)
//        {
//            //---- make sure to new up our installed robots if it doesn't exist
//            if (Properties.Settings.Default.ConfiguredRobots == null)
//            { Properties.Settings.Default.ConfiguredRobots = new ObservableCollection<RobotConfiguration>(); }
			
//            //---- subscribe to the collection changed event so we can make sure to save it
//            Properties.Settings.Default.ConfiguredRobots.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(InstalledRobots_CollectionChanged);
//        }
//        //=======================================================================

//        #endregion
//        //=======================================================================

//        //=======================================================================
//        #region -= properties =-

//        //=======================================================================
//        public Size WindowSize
//        {
//            get { return Properties.Settings.Default.WindowSize; }
//            set
//            {
//                Properties.Settings.Default.WindowSize = value;
//                Properties.Settings.Default.Save();
//            }
//        }
//        //=======================================================================

//        //=======================================================================
//        public Double WindowTop
//        {
//            get { return Properties.Settings.Default.WindowTop; }
//            set
//            {
//                Properties.Settings.Default.WindowTop = value;
//                Properties.Settings.Default.Save();
//            }
//        }
//        //=======================================================================

//        //=======================================================================
//        public Double WindowLeft
//        {
//            get { return Properties.Settings.Default.WindowLeft; }
//            set
//            {
//                Properties.Settings.Default.WindowLeft = value;
//                Properties.Settings.Default.Save();
//            }
//        }
//        //=======================================================================

//        //=======================================================================
//        public ObservableCollection<RobotConfiguration> ConfiguredRobots
//        {
//            get { return Properties.Settings.Default.ConfiguredRobots; }
//            set
//            {
//                Properties.Settings.Default.ConfiguredRobots = value;
//                Properties.Settings.Default.Save();
//            }
//        }
//        //=======================================================================

//        ////=======================================================================
//        //public PortSettings BasicArduinoPortSettings
//        //{
//        //    get
//        //    {
//        //        if (Properties.Settings.Default.BasicArduinoPortSettings != null)
//        //        { return Properties.Settings.Default.BasicArduinoPortSettings; }
//        //        else { return new PortSettings(); }
//        //    }
//        //    set
//        //    {
//        //        Properties.Settings.Default.BasicArduinoPortSettings = value;
//        //        Properties.Settings.Default.Save();
//        //    }
//        //}
//        ////=======================================================================

//        ////=======================================================================
//        //public PortSettings BasicParallaxPortSettings
//        //{
//        //    get
//        //    {
//        //        if (Properties.Settings.Default.BasicParallaxPortSettings != null)
//        //        { return Properties.Settings.Default.BasicParallaxPortSettings; }
//        //        else { return new PortSettings(); }
//        //    }
//        //    set
//        //    {
//        //        Properties.Settings.Default.BasicParallaxPortSettings = value;
//        //        Properties.Settings.Default.Save();
//        //    }
//        //}
//        ////=======================================================================


//        #endregion
//        //=======================================================================

//        //=======================================================================
//        #region -= event handlers =-

//        //=======================================================================
//        protected void InstalledRobots_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
//        {
//            //---- the collection changed, so make sure we save the changes
//            Properties.Settings.Default.Save();
//        }
//        //=======================================================================

//        #endregion
//        //=======================================================================

//        //=======================================================================
//        #region -= public methods =-

//        //=======================================================================
//        public bool ConfiguredRobotExists(string robotDisplayName)
//        {
//            foreach (IRobot robot in this.ConfiguredRobots)
//            {
//                if (robot.Configuration.DisplayName == robotDisplayName) { return true; }
//            }
//            //---- if we got here, we couldn't find it
//            return false;
//        }
//        //=======================================================================

//        #endregion
//        //=======================================================================

//    }
//    //=======================================================================
//}
