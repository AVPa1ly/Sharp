using System.ServiceProcess;
using System.Threading;
using static Lab2Lib.ExtractionFunctions;
using CfgManager;

namespace Lab3
{
    public partial class LabService : ServiceBase
    {
        Logger logger;
        public LabService()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }
        protected override void OnStart(string[] args)
        {
            string configPath= @"C:\Users\user\Documents\Config\appSettings.json";
            Validator provider = new Validator(configPath);
            logger = new Logger(provider.archivePath.ArchivePath, provider.sourcePath.SourcePath, provider.targetPath.TargetPath);//config data loading
            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            loggerThread.Start();
        }
        protected override void OnStop()
        {
            logger.Stop();
            Thread.Sleep(1000);
        }
    }    
}
