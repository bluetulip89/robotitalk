using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;

namespace Sicily.Robotix.MicroController.CommunicationApplication.Controls
{
	/// <summary>
	/// Interaction logic for PortSettingsConfiguration.xaml
	/// </summary>
	public partial class PortSettingsConfiguration : UserControl
	{
		//=========================================================================
		#region -= declarations =-

		//public static readonly RoutedEvent SaveSettingsClickEvent = EventManager.RegisterRoutedEvent("SaveSettingsClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PortSettingsConfiguration));

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= constructor / intializer =-

		public PortSettingsConfiguration()
		{
			InitializeComponent();
			this.PopulatePage();
		}

		public void Initialize()
		{

		}


		#endregion
		//=========================================================================

		//=========================================================================
		#region -= events =-

		///// <summary>
		///// 
		///// </summary>
		//public event RoutedEventHandler SaveSettingsClick
		//{
		//    add { AddHandler(SaveSettingsClickEvent, value); }
		//    remove { RemoveHandler(SaveSettingsClickEvent, value); }
		//}


		#endregion
		//=========================================================================

		//=========================================================================
		#region -= properties =-

		/// <summary>
		/// 
		/// </summary>
		public PortSettings PortSettings
		{
			get
			{
				//return this._portSettings.Clone() as PortSettings;
				return this.GetChanges();
			}
			set
			{
				this._portSettings = value.Clone() as PortSettings;
				//this._portSettings = value;
				if (this._portSettings != null)
				{
					this.SetValuesFromSettings();
				}
			}
		}
		protected PortSettings _portSettings;


		#endregion
		//=========================================================================

		//=========================================================================
		#region -= page population methods =-

		//=========================================================================
		protected void PopulatePage()
		{
			this.PopulateSerialPortList();
			this.PopulateBaudRate();
			this.PopulateHandshake();
			this.PopulateParity();
			this.PopulateStopBits();
		}
		//=========================================================================

		//=========================================================================
		protected void SetValuesFromSettings()
		{
			this.txtDataBits.Text = this._portSettings.DataBits.ToString();
			this.SelectItem(this.lstSerialPorts, this._portSettings.PortName);
			this.SelectBaudRate(this._portSettings.BaudRate);
			this.lstParity.SelectedItem = this._portSettings.Parity;
			this.lstStopBits.SelectedItem = this._portSettings.StopBits;
			this.lstHandshake.SelectedItem = this._portSettings.Handshake;
		}
		//=========================================================================

		//=========================================================================
		protected void PopulateSerialPortList()
		{
			//---- enumerate the ports
			string[] ports = System.IO.Ports.SerialPort.GetPortNames();
			this.lstSerialPorts.ItemsSource = ports;
		}
		//=========================================================================

		//=========================================================================
		protected void PopulateBaudRate()
		{

			string[] baudRates = new string[(Enum.GetNames(typeof(BaudRate))).Length + 1];
			(Enum.GetNames(typeof(BaudRate))).CopyTo(baudRates, 0);
			baudRates[baudRates.Length - 1] = "Other";

			this.lstBaudRates.ItemsSource = baudRates;
		}
		//=========================================================================

		//=========================================================================
		protected void PopulateParity()
		{
			this.lstParity.ItemsSource = Enum.GetValues(typeof(Parity));
		}
		//=========================================================================

		//=========================================================================
		protected void PopulateStopBits()
		{
			this.lstStopBits.ItemsSource = Enum.GetValues(typeof(StopBits));
		}
		//=========================================================================

		//=========================================================================
		protected void PopulateHandshake()
		{
			this.lstHandshake.ItemsSource = Enum.GetValues(typeof(Handshake));
		}
		//=========================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= protected methods =-

		//=========================================================================
		/// <summary>
		/// Selects an item in a combo box if it's text matches the passed in itemValue;
		/// </summary>
		/// <param name="comboBox"></param>
		/// <param name="itemValue"></param>
		protected void SelectItem(ComboBox comboBox, string itemValue)
		{
			foreach (Object item in comboBox.Items)
			{
				if (item != null)
				{
					if (item.ToString() == itemValue)
					{
						comboBox.SelectedItem = item;
						return;
					}
				}
			}
		}
		//=========================================================================


		//=========================================================================
		/// <summary>
		/// 
		/// </summary>
		protected void SelectBaudRate(int baudRate)
		{
			foreach (Object item in this.lstBaudRates.Items)
			{
				if (item != null)
				{
					if (((int)(Enum.Parse(typeof(BaudRate), item.ToString()))) == baudRate)
					{
						this.lstBaudRates.SelectedItem = item;
						return;
					}
				}
			}
			//---- if we got here, it must be other
			this.lstBaudRates.SelectedItem = this.lstBaudRates.Items[this.lstBaudRates.Items.Count - 1];
			this.txtOtherBaudRate.Text = baudRate.ToString();
		}
		//=========================================================================

		//=========================================================================
		protected PortSettings GetChanges()
		{
			//---- declare vars
			PortSettings portSettings = new PortSettings();

			portSettings.PortName = this.lstSerialPorts.SelectedValue.ToString();
			portSettings.Parity = (Parity)this.lstParity.SelectedItem;
			portSettings.StopBits = (StopBits)this.lstStopBits.SelectedItem;
			portSettings.Handshake = (Handshake)this.lstHandshake.SelectedItem;

			if (this.lstBaudRates.SelectedValue == "Other")
			{
				int baudRate;
				if (int.TryParse(this.txtOtherBaudRate.Text, out baudRate))
				{ portSettings.BaudRate = baudRate; }
				else { throw new Exception("Invalid Baud Rate."); }
			}
			else { portSettings.BaudRate = (int)(Enum.Parse(typeof(BaudRate), this.lstBaudRates.SelectedItem.ToString())); }

			int dataBits;
			if (int.TryParse(this.txtDataBits.Text, out dataBits))
			{ portSettings.DataBits = dataBits; }
			else { throw new Exception("Invalid Data Bits Value."); }

			return portSettings;

		}
		//=========================================================================

		//=========================================================================
		//protected void RaiseSaveSettingsClickEvent()
		//{
		//    RaiseEvent(new RoutedEventArgs(SaveSettingsClickEvent, this));
		//}
		//=========================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= event handlers =-

		//=========================================================================
		private void btnDataBitsUp_Click(object sender, RoutedEventArgs e)
		{
			int dataBits;

			if (int.TryParse(this.txtDataBits.Text, out dataBits))
			{
				dataBits++;
				this.txtDataBits.Text = dataBits.ToString();
			}
		}
		//=========================================================================

		//=========================================================================
		private void btnDataBitsDown_Click(object sender, RoutedEventArgs e)
		{
			int dataBits;

			if (int.TryParse(this.txtDataBits.Text, out dataBits))
			{
				if (dataBits > 0)
				{
					dataBits--;
					this.txtDataBits.Text = dataBits.ToString();
				}
			}
		}
		//=========================================================================

		//=========================================================================
		private void btnRefreshSerialPorts_Click(object sender, RoutedEventArgs e)
		{
			this.PopulateSerialPortList();
		}
		//=========================================================================

		//=========================================================================
		private void lstBaudRates_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.lstBaudRates.SelectedItem == "Other")
			{ this.pnlOtherBaudRate.Visibility = Visibility.Visible; }
			else
			{ this.pnlOtherBaudRate.Visibility = Visibility.Collapsed; }

			if (this.lstBaudRates.SelectedItem == "Other")
			{
				this._portSettings.BaudRate = 0;
			}
			else
			{
				if (this._portSettings.BaudRate != (int)Enum.Parse(typeof(BaudRate), this.lstBaudRates.SelectedItem.ToString()))
				{ this._portSettings.BaudRate = (int)Enum.Parse(typeof(BaudRate), this.lstBaudRates.SelectedItem.ToString()); }
			}

		}
		//=========================================================================

		//=========================================================================
		private void lstParity_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this._portSettings.Parity != (Parity)this.lstParity.SelectedItem)
			{ this._portSettings.Parity = (Parity)this.lstParity.SelectedItem; }
		}
		//=========================================================================

		//=========================================================================
		private void lstStopBits_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this._portSettings.StopBits != (StopBits)this.lstStopBits.SelectedItem)
			{ this._portSettings.StopBits = (StopBits)this.lstStopBits.SelectedItem; }
		}
		//=========================================================================

		//=========================================================================
		private void lstHandshake_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this._portSettings.Handshake != (Handshake)this.lstHandshake.SelectedItem)
			{ this._portSettings.Handshake = (Handshake)this.lstHandshake.SelectedItem; }
		}
		//=========================================================================

		//=========================================================================
		private void txtDataBits_TextChanged(object sender, TextChangedEventArgs e)
		{
			int dataBits;
			if (int.TryParse(this.txtDataBits.Text, out dataBits))
			{
				if (dataBits != this._portSettings.DataBits)
				{ this._portSettings.DataBits = dataBits; }
			}
			else { throw new Exception("Invalid Data Bits Value."); }
			//--- todo: validate
		}
		//=========================================================================

		//=========================================================================
		private void txtOtherBaudRate_LostFocus(object sender, RoutedEventArgs e)
		{
			int baudRate;
			if (int.TryParse(this.txtOtherBaudRate.Text, out baudRate))
			{
				if (baudRate != this._portSettings.BaudRate)
				{ this._portSettings.BaudRate = baudRate; }
			}
			else { throw new Exception("Invalid Baud Rate."); }
			//--- TODO: preview text input, only allow numbers
		}
		//=========================================================================

		//=========================================================================
		private void lstSerialPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.lstSerialPorts.SelectedValue.ToString() != this._portSettings.PortName)
			{ this._portSettings.PortName = this.lstSerialPorts.SelectedValue.ToString(); }
		}
		//=========================================================================

		#endregion
		//=========================================================================

		//=========================================================================
		#region -= public methods =-

		//=========================================================================
		public void ResetToDefaults()
		{
			this._portSettings = new PortSettings();
			this.SetValuesFromSettings();
		}
		//=========================================================================

		//=======================================================================
		public bool PageIsValid()
		{
			//---- validate the baud rates
			if (this.lstBaudRates.SelectedItem.ToString() == "Other")
			{
				int baudRate;
				if (int.TryParse(this.txtOtherBaudRate.Text, out baudRate))
				{
					if (baudRate <= 0)
					{
						Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "Baud rate must be greater than zero.", "Baud Rate Error", MessageBoxButton.OK);
						return false;
					}
				}
				else
				{
					Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "Baud rate must be specified.", "Baud Rate Error", MessageBoxButton.OK);
					return false;
				}
			}
			//---- validate the data bits
			int dataBits;
			if (int.TryParse(this.txtDataBits.Text, out dataBits))
			{
				if (dataBits <= 0)
				{
					Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "Data bits must be greater than zero.", "Data Bits Error", MessageBoxButton.OK);
					return false;
				}
			}
			else
			{
				Sicily.Robotix.MicroController.CommunicationApplication.Dialogs.MessageBox.Show(Window.GetWindow(this), "Data bits rate must be specified.", "Data Bits Error", MessageBoxButton.OK);
				return false;
			}


			//---- if we got here, everything is hunky dori
			return true;
		}
		//=======================================================================

		////=========================================================================
		///// <summary>
		///// doing it this way because if there is a reference to a port settings on a robot, 
		///// we don't want to tightly bind it and wait for changes on the page because it will 
		///// close the port. so instead, we let the consumer have control of when to get 
		///// the updated port settings
		///// </summary>
		///// <returns></returns>
		//public PortSettings GetUpdatedPortSettings()
		//{
		//    PortSettings portSettings = new PortSettings();

		//    portSettings.PortName = this.lstSerialPorts.SelectedValue.ToString();

		//    if (this.lstBaudRates.SelectedValue == "Other")
		//    {
		//        int baudRate;
		//        if (int.TryParse(this.txtOtherBaudRate.Text, out baudRate))
		//        { portSettings.BaudRate = baudRate; }
		//        else { throw new Exception("Invalid Baud Rate."); }
		//    }
		//    else { portSettings.BaudRate = (int)(BaudRate)this.lstBaudRates.SelectedItem; }

		//    int dataBits;
		//    if (int.TryParse(this.txtDataBits.Text, out dataBits))
		//    { portSettings.DataBits = dataBits; }
		//    else { throw new Exception("Invalid Data Bits Value."); }

		//    portSettings.Parity = (Parity)this.lstParity.SelectedItem;
		//    portSettings.StopBits = (StopBits)this.lstStopBits.SelectedItem;
		//    portSettings.Handshake = (Handshake)this.lstHandshake.SelectedItem;

		//    return portSettings;
		//}
		////=========================================================================

		#endregion
		//=========================================================================
	}
}
