using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.User_Interface
{
    public class GetConnectionsStrings
    {
        public static string GetSConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;

            string dbname = appConfig["SDBNAME"];

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = appConfig["SUSERNAME"];
            string password = appConfig["SPASSWORD"];
            string hostname = appConfig["SHOSTNAME"];
            string port = appConfig["SPORT"];

            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }

        public static string GetSHostName()
        {
            string appHost = ConfigurationManager.AppSettings["SHOSTNAME"];
            return appHost;
        }

        public static string GetSUsername()
        {
            string appHost = ConfigurationManager.AppSettings["SUSERNAME"];
            return appHost;
        }

        public static string GetSPassword()
        {
            string appHost = ConfigurationManager.AppSettings["SPASSWORD"];
            return appHost;
        }
    }
}
