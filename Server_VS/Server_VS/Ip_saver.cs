using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Server_VS
{
    [Serializable]
    public class Ip_saver
    {
        #region Properties

        public static IPAddress ip_connect;
        public static String ip_string;
        public static IPAddress Ip_connect
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
                ip_string = ipV4;
                Ip_connect = IPAddress.Parse(ipV4);
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
