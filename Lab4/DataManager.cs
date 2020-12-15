using System;
using System.Threading;

namespace Lab4
{
    class DataManager
    {
        private DataSender dataSender;
        private SqlWatcher sqlWatcher;
        private DateTime date = DateTime.Now;
        public int Timer { get; set; }

        public DataManager(string sqlServer, string dbName, bool integratedSecurity, string serverName, string login, string pass, int timer)
        {
            dataSender = new DataSender(serverName, login, pass);
            sqlWatcher = new SqlWatcher(sqlServer, dbName, integratedSecurity);
            Timer = timer;
        }

        public void Start()
        {
            new Thread(MainFunc).Start();
        }

        void MainFunc()
        {
            while (true)
            {
                Thread.Sleep(1000);
                var obj = sqlWatcher.GetLastUserInfo(date);
                if (obj is null)
                    continue;
                for (int i = 0; i < obj.Length; i++)
                {
                    Info info = new Info(obj[i][1].ToString().Trim(), obj[i][2].ToString().Trim(), obj[i][3].ToString().Trim(), obj[i][4].ToString().Trim(), (bool)obj[i][5], (int)obj[i][6], obj[i][7].ToString().Trim(), obj[i][8].ToString().Trim(), obj[i][9].ToString().Trim(), obj[i][10].ToString().Trim());
                    XmlFormer xmlFormer = new XmlFormer();
                    string xmlText = xmlFormer.GetXml(info);
                    dataSender.Send(xmlText, xmlFormer.GetXsd(xmlText));
                    date = (DateTime)obj[i][0];
                }
            }
        }
    }
}
