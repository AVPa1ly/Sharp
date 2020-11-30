using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace CfgManager
{

    //xml data extraction class
    public class EtlXmlOptions : IValidate
    {
        //fields of config blocks
        private ArchiveOptions archivePath;
        private SourceOptions sourcePath;
        private TargetOptions targetPath;
        public EtlXmlOptions(string configPath)
        {
            archivePath = new ArchiveOptions();
            sourcePath = new SourceOptions();
            targetPath = new TargetOptions();
            if (!CheckXml(configPath)) //validation check
            {
                throw new InvalidOperationException("Something went wrong with xml-file");
            }
            //xml data extraction
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(configPath);
            XmlElement xRoot = xmlDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                if (xnode.Attributes.Count > 0)
                {
                    if (xnode.Attributes.GetNamedItem("name").Value == "Lab3")
                    {
                        foreach (XmlNode childnode in xnode.ChildNodes) //all child nodes of Lab3 element
                        {
                            if (childnode.Name == "archivePath")
                            {
                                archivePath.ArchivePath = childnode.InnerText;
                            }
                            if (childnode.Name == "sourcePath")
                            {
                                sourcePath.SourcePath = childnode.InnerText;
                            }
                            if (childnode.Name == "targetPath")
                            {
                                targetPath.TargetPath = childnode.InnerText;
                            }
                        }
                    }
                }
            }
            //check all fields are set
            if (archivePath.ArchivePath == null)
            {
                throw new InvalidOperationException("Archive dir: path not found");
            }
            if (sourcePath.SourcePath == null)
            {
                throw new InvalidOperationException("Source dir: path not found");
            }
            if (targetPath.TargetPath == null)
            {
                throw new InvalidOperationException("Target dir: path not found");
            }
        }

        //check xml validity using xsd
        public static bool CheckXml(string configPath)
        {
            bool error = false;
            string xsdPath = @"C:\Users\user\Documents\Config\config.xsd";
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", xsdPath);
            XDocument xDocument = XDocument.Load(configPath);
            xDocument.Validate(schemas, (o, e) =>
            {
                error = true;
            });
            if (error)
            {
                return false;
            }
            else return true;
        }

        //data extraction method
        public string GetOptions<T>()
        {
            if (archivePath is T)
            {
                return archivePath.ArchivePath;
            }
            else if (sourcePath is T)
            {
                return sourcePath.SourcePath;
            }
            else if (targetPath is T)
            {
                return targetPath.TargetPath;
            }
            else
            {
                throw new InvalidOperationException("Wrong type");
            }
        }
    }
}
