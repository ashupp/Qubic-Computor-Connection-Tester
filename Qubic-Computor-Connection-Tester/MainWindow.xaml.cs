using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleTcp;

namespace Qubic_Computor_Connection_Tester
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SimpleTcpClient _client;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _client = new SimpleTcpClient(IpPortTextBox.Text);
                _client.Settings.ConnectTimeoutMs = 2500;
                _client.Events.Connected += Connected;
                _client.Connect();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Could not connect: Check IP/Port: " + exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusTextBox.Text = "Could not connect: " + exception.Message;
            }
            
        }

        private void Connected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine("Connected: " + e.IpPort + " - " + e.Reason);
            StatusTextBox.Text = "Success: Connected. Now check Computor app for CONNECTED - Message";

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                _client.Events.Connected -= Connected;
                _client.Disconnect();
            }));
            
        }
    }
}
