using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4
{
    class Exmaple
    {
        static void Main()
        {
            string configPath = @"C:\Users\user\Documents\Config\appSettings.json";
            Validator provider = new Validator(configPath);
            DataManager dataManager = new DataManager(provider.sqlInfo.SqlServer, provider.sqlInfo.DbName, provider.sqlInfo.Security, provider.serverInfo.ServerName, provider.serverInfo.Login, provider.serverInfo.Password, provider.serverInfo.ThreadBlock);
            dataManager.Start();
        }
    }
}
