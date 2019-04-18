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

namespace Server_VS
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region Properties
        private DeveloperManager dev_mana;
        private MainWindow f1;
        #endregion

        public LoginWindow()
        {
            InitializeComponent();
            dev_mana = new DeveloperManager();
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

 
        private void Login_btt_Click(object sender, RoutedEventArgs e)
        {
            if (dev_mana.Check_Account(NameBox.Text, PasswordBox.Password))
            {
                MessageBox.Show("SERVER CONNECT SUCCESSFUL!");
                Show_MainForm_And_Save_ipV4();
            }
            else
            {
                MessageBox.Show("SERVER CONNECT FAILED");
            }
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (dev_mana.Check_Account(NameBox.Text, PasswordBox.Password))
                {
                    MessageBox.Show("SERVER CONNECT SUCCESSFUL!");
                    Show_MainForm_And_Save_ipV4();
                }
                else
                {
                    MessageBox.Show("SERVER CONNECT FAILED");
                    LoginWindow newLogin = new LoginWindow();
                    newLogin.Show();
                    this.Close();
                    
                }
            }
        }

        Ip_saver ipAddress = new Ip_saver();
        private void Show_MainForm_And_Save_ipV4()
        {
            ipAddress.Save_IPV4(IpBox.Text);
            f1 = new MainWindow();
            f1.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        #endregion

    }
}
