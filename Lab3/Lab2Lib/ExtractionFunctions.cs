using System;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace Lab2Lib
{
    public class ExtractionFunctions
    {
        public class Logger
        {
            FileSystemWatcher watcher;
            private bool enabled = true;
            private string sourceDirPath;
            private string targetDirPath;
            private string archiveDirPath;
            public Logger(string archive,string source,string target)
            {
                sourceDirPath = source;
                targetDirPath = target;
                archiveDirPath = archive;
                watcher = new FileSystemWatcher(sourceDirPath);
                watcher.Filter = "*.txt";
                watcher.Created += Watcher_Created;
            }

            public void Start()
            {
                watcher.EnableRaisingEvents = true;
                while (enabled)
                {
                    Thread.Sleep(1000);
                }
            }
            public void Stop()
            {
                watcher.EnableRaisingEvents = false;
                enabled = false;
            }
            //file creation
            private void Watcher_Created(object sender, FileSystemEventArgs e)
            {
                string archivePath = Archive(archiveDirPath, e.FullPath, e.Name);
                Dearchive(targetDirPath, archivePath, e.Name);
            }
        }
        public static string Archive(string archivePath, string filePath, string fileName)
        {
            string content;
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                content = streamReader.ReadToEnd();
            }
            string archiveName = Path.ChangeExtension(fileName,".gz");
            archivePath = Path.Combine(archivePath, archiveName);
            using (FileStream targetStream = File.Create(archivePath))
            {
                using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                {
                    using (StreamWriter streamWriter = new StreamWriter(compressionStream))
                    {
                        streamWriter.Write(Encrypt(content));
                    }
                }
            }
            return archivePath;
        }
        public static void Dearchive(string targetPath, string archivePath, string fileName)
        {
            string content = "";
            targetPath = Path.Combine(targetPath, fileName);
            using (FileStream sourceStream = new FileStream(archivePath, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(targetPath))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                    }
                }
            }

            using (StreamReader streamReader = new StreamReader(targetPath))
            {
                content = streamReader.ReadToEnd();
            }

            using (StreamWriter streamWriter = new StreamWriter(targetPath))
            {
                streamWriter.Write(Decrypt(content));
            }
        }
        private static string Encrypt(string str)
        {
            string enryptedString = "";
            for (int i = 0, len = str.Length; i < len; i++)
            {
                enryptedString += Convert.ToChar(str[i] + 1);
            }
            return enryptedString;
        }
        private static string Decrypt(string str)
        {
            string decryptedString = "";
            for (int i = 0, len = str.Length; i < len; i++)
            {
                decryptedString += Convert.ToChar(str[i] - 1);
            }
            return decryptedString;
        }
    }
}