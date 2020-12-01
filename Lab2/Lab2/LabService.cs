using System.ServiceProcess;
using System.Threading;
using static Lab2Lib.ExtractionFunctions;

namespace Lab2
{
    public partial class LabService : ServiceBase
    {
        Logger logger;
        private string sourcePath = @"C:\Users\user\Documents\SourceDirectory";
        private string targetPath = @"C:\Users\user\Documents\TargetDirectory";
        private string archivePath = @"C:\Users\user\Documents\TargetDirectory\ArchiveDir";
        public LabService()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }
        protected override void OnStart(string[] args)
        {
            //string configPath= @"C:\Users\user\Documents\Config\appSettings.json";
            //Validator provider = new Validator(configPath);
            logger = new Logger(archivePath, sourcePath, targetPath);
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
