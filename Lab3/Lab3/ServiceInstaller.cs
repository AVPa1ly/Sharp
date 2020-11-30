using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Lab3
{
    [RunInstaller(true)]
    public partial class ServiceInstaller : Installer
    {
        System.ServiceProcess.ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public ServiceInstaller()
        {
            InitializeComponent();
            serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "WS-Lab2";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
