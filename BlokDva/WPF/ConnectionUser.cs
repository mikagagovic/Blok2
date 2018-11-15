using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public class ConnectionUser
    {
        private static ConnectionUser connUser;
        public User CurrentUser { get; set; }

        public ConnectionUser()
        {
        }

        public static ConnectionUser Instance()
        {
            if (connUser == null)
            {
                connUser = new ConnectionUser();
            }

            return connUser;
        }
    }
}
