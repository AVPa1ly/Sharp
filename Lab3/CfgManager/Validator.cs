using System;

namespace CfgManager
{
    public class Validator
    {
        public ArchiveOptions archivePath;
        public SourceOptions sourcePath;
        public TargetOptions targetPath;
        IValidate provide;
        public Validator(string configPath)
        {
            archivePath = new ArchiveOptions();
            sourcePath = new SourceOptions();
            targetPath = new TargetOptions();            
            switch (CheckExtension(configPath))//creating instanse of IValidate object depending on the extension in configPath
            {
                case ".xml":
                    provide = new EtlXmlOptions(configPath);
                    break;
                case ".json":
                    provide = new EtlJsonOptions(configPath);
                    break;                       
            }            
            archivePath.ArchivePath = provide.GetOptions<ArchiveOptions>();
            sourcePath.SourcePath = provide.GetOptions<SourceOptions>();
            targetPath.TargetPath = provide.GetOptions<TargetOptions>();
        }
        public static string CheckExtension(string path)//checks extension of cfg-file using full path
        {            
            if (path.Length < 5)
            {
                throw new InvalidOperationException("Wrong path");
            }
            if (path.EndsWith(".xml"))
            {
                return ".xml";
            }
            else if (path.EndsWith(".json"))
            {
                return ".json";
            }
            else
            {
                throw new InvalidOperationException("Wrong path");
            }
        }
    }
}
