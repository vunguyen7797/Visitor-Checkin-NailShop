using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
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
using TestLib;

namespace CLIENT_VS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket client;
        IPEndPoint iep;

        private const int BUFFER_SIZE = 1024;
        private const int PORT_NUMBER = 7826;

        static ASCIIEncoding encoding = new ASCIIEncoding();
        public MainWindow()
        {
            InitializeComponent();
            var dispatcher = Application.Current.MainWindow.Dispatcher; 
            dispatcher.BeginInvoke(new Action(() =>
            {
                connect();
            }));
        }

        #region method
        private void connect()
        {
            iep = new IPEndPoint(IP_Saver.Ip_Connect, PORT_NUMBER);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(iep);
            }
            catch
            {
                MessageBox.Show("CONNECTION FAILED. PLEASE CONTACT MANAGER");
                this.close();
            }
            
        }

        private void send()
        {
            SockTest b = new SockTest();
            b.name = NameBox.Text;
            b.phone = PhoneBox.Text;
            b.date = checkintime.Text;

            if (check1.IsChecked == true)
            {
                b.checkS1 = "true";
                b.tech1 = tech1.Text;
            }
            else
                b.checkS1 = "false";

            if (check2.IsChecked == true)
            {
                b.checkS2 = "true";
                b.tech2 = tech2.Text;
            }
            else
                b.checkS2 = "false";

            if (check3.IsChecked == true)
            {
                b.checkS3 = "true";
                b.tech3 = tech3.Text;
            }
            else
                b.checkS3 = "false";

            if (check4.IsChecked == true)
            {
                b.checkS4 = "true";
                b.tech4 = tech4.Text;
            }
            else
                b.checkS4 = "false";

            if (check5.IsChecked == true)
            {
                b.checkS5 = "true";
                b.tech5 = tech5.Text;
            }
            else
                b.checkS5 = "false";

            if (check6.IsChecked == true)
            {
                b.checkS6 = "true";
                b.tech6 = tech6.Text;
            }
            else
                b.checkS6 = "false";

            client.Send(SerializeData(b));
            MessageBox.Show("CHECK IN DONE. THANK YOU!");
            WelcomeWindow welcome = new WelcomeWindow();
            welcome.Show();
            this.Close();
        }

        private void close()
        {
            client.Close();
        }
     
        // combine data into one object
        public byte[] SerializeData(Object o)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, o);
            return ms.ToArray();
        }

        // extract data from one object
        public object DeserializeData(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;
            return bf1.Deserialize(ms);
        }

  
        #endregion

        #region event
        private void Send_btt_Click(object sender, RoutedEventArgs e)
        {
            send();
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                send();
        }

        private void Minimize_btt_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Maximize_btt_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
                this.WindowState = WindowState.Maximized;
            else if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
        }

        private void Exit_btt_Click(object sender, RoutedEventArgs e)
        {
            Close();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Close_btt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Handle_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if (cb.Name == "check1")
            {
                check2.IsChecked = true;
                check3.IsChecked = true;
                check4.IsChecked = true;
                check5.IsChecked = true;
                check6.IsChecked = true;
            }
        }

        private void Handle_UnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if (cb.Name == "check1")
            {
                check2.IsChecked = false;
                check3.IsChecked = false;
                check4.IsChecked = false;
                check5.IsChecked = false;
                check6.IsChecked = false;
            }
        }
        #endregion
    }
}
