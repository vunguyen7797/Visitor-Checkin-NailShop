using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CLIENT_VS
{
    [Serializable]
    public class IP_Saver
    {
        #region Properties

        public static IPAddress ip_connect; // store the ip address in correct format
        public static String ip_string;     // store the input ip address 
        public static IPAddress Ip_Connect
        {
            get
            {
                return ip_connect;
            }

            set
            {
                ip_connect = value;
            }
        }
        #endregion

        public bool Save_IPV4(string ipV4)
        {
            try
            {
                ip_string = ipV4;   // assign input to its storage
                Ip_Connect = IPAddress.Parse(ipV4); // convert string to format IP address
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("IP ADDRESS IS IN WRONG FORMAT ! : " + ex.Message);
                return false;
            }
        }

    }
}
