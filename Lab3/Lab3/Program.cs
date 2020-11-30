using System.ServiceProcess;

namespace Lab3
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new LabService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
