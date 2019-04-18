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
using System.Windows.Shapes;
using TestLib;

namespace Server_VS
{
    /// <summary>
    /// Interaction logic for DashBoardWindow.xaml
    /// </summary>  
    public partial class MainWindow : Window
    {
        private const int BUFFER_SIZE = 1024;
        private const int PORT_NUMBER = 7826;
        Socket server;
        IPEndPoint iep;

        static ASCIIEncoding encoding = new ASCIIEncoding();
       

        // Extract data from an object
        public object DeserializeData(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;
            return bf1.Deserialize(ms);
        }

        public MainWindow()
        {
            InitializeComponent();

            var dispatcher = Application.Current.MainWindow.Dispatcher;
            // Or use this.Dispatcher if this method is in a window class.
            dispatcher.BeginInvoke(new Action(() =>
            {
                connect();
            })); 

        }

        List<Socket> clientList;
        private void connect()
        {
            clientList = new List<Socket>();
            iep = new IPEndPoint(Ip_saver.ip_connect, PORT_NUMBER);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(iep);

            Thread listen = new Thread(() =>
           {
               try
               {
                   while (true)
                   {
                       server.Listen(100);
                       Socket client = server.Accept();
                       clientList.Add(client);
                       Thread receive = new Thread(Receive);
                       receive.IsBackground = true;
                       receive.Start(client);
                   }
               }
               catch
               {
                   iep = new IPEndPoint(IPAddress.Any, PORT_NUMBER);
                   server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

               }
           });
            listen.IsBackground = true;
            listen.Start();
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

        private void Close_btt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Shown(object sender, EventArgs e)
        { 
        }

        private void Receive(object obj)
        {
            Socket client = obj as Socket;
            try
            {
              while (true)
             {
                    byte[] data = new byte[BUFFER_SIZE];
                    client.Receive(data);
                    this.Dispatcher.Invoke(() =>
                    {
                        SockTest b = (SockTest)DeserializeData(data);
                        NameBox.Text = b.name;
                        PhoneBox.Text = b.phone;
                        DateBox.Text = b.date;
                        TechBox.Text = " ";
                       
                        if (b.checkS1 == "true")
                        {
                            TechBox.Text = b.tech1;
                        }
                        else
                        {
                            if (b.checkS2 == "true")
                                TechBox.Text += b.tech2 + ", ";
                            if (b.checkS3 == "true")
                                TechBox.Text += b.tech3 + ", ";
                            if (b.checkS4 == "true")
                                TechBox.Text += b.tech4 + ", ";
                            if (b.checkS5 == "true")
                                TechBox.Text += b.tech5 + ", ";
                            if (b.checkS6 == "true")
                                TechBox.Text += b.tech6;
                        }

                        this.ListView.Items.Add(new MyItem { FullName = NameBox.Text, PhoneNumber = PhoneBox.Text, CheckinTime = DateBox.Text, TechChoices = TechBox.Text });
                    });
              }
           }
           catch
            {
               clientList.Remove(client);
                client.Close();
           }
        } 
    }
}
