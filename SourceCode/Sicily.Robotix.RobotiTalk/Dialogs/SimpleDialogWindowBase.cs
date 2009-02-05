using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace Sicily.Robotix.MicroController.CommunicationApplication.Dialogs
{
	public class SimpleDialogWindowBase : Window
	{
		protected FrameworkElement _title;

		public SimpleDialogWindowBase()
		{
			this.Loaded += new RoutedEventHandler(this_Loaded);
		}

		protected void this_Loaded(object sender, RoutedEventArgs e)
        {
			this._title = (FrameworkElement)this.Template.FindName("grdWindowTitle", this);
            if (null != this._title)
            {
                this._title.MouseLeftButtonDown += new MouseButtonEventHandler(title_MouseLeftButtonDown);
            }
        }

		protected void title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
	}
}
