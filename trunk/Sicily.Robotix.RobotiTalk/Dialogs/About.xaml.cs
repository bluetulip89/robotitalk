using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sicily.Robotix.MicroController.CommunicationApplication.Dialogs
{
	/// <summary>
	/// Interaction logic for About.xaml
	/// </summary>
	public partial class About : SimpleDialogWindowBase
	{
		public About()
		{
			InitializeComponent();
		}

		private void btnOk_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
