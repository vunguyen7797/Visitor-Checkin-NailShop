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
using System.Windows.Shapes;

namespace CLIENT_VS
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region Properties
        private MainWindow mainForm;

        #endregion

        public LoginWindow()
        {
            InitializeComponent();
        }

        #region events
        private void Exit_btt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Close_btt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Maximize_btt_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
                this.WindowState = WindowState.Maximized;
            else if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
        }

        private void Minimize_btt_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    
        IP_Saver ipAddress = new IP_Saver();
        private void login_btt_Click(object sender, RoutedEventArgs e)
        {
            ipAddress.Save_IPV4(IpBox.Text);
            WelcomeWindow welcome = new WelcomeWindow();
            welcome.Show();
            this.Close();
        }

        // Press Enter to login
        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (ipAddress.Save_IPV4(IpBox.Text) == true)
                {
                    WelcomeWindow welcome = new WelcomeWindow();
                    welcome.Show();
                    this.Close();
                }
                else
                    this.Close();
                
            }

        }
        
        // move the window
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        #endregion

    }
}
