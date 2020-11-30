using System;
using System.IO;

namespace CfgManager
{
    //json data extraction class
    public class EtlJsonOptions : IValidate
    {
        //fields of config blocks
        private ArchiveOptions archivePath;
        private SourceOptions sourcePath;
        private TargetOptions targetPath;
        public EtlJsonOptions(string configPath)
        {
            string jsonString= File.ReadAllText(configPath);
            string bufferString;
            int length = jsonString.Length;
            //json data extraction
            for (int i = 0; i < length; i++)
            {
                bufferString = ReadWord(jsonString, ref i);
                switch (bufferString)
                {
                    case "archivePath":
                        if (jsonString[i] == ':' && jsonString[i + 2] == '{')
                        {
                            bufferString = ReadWord(jsonString, ref i);
                            switch (bufferString)
                            {
                                case "ArchivePath":
                                    if (jsonString[i] == ':')
                                    {
                                        var bufferOptions = new ArchiveOptions();
                                        bufferOptions.ArchivePath = ReadWord(jsonString, ref i);
                                        archivePath = bufferOptions;
                                    }
                                    break;
                            }
                        }
                        break;
                    case "sourcePath":
                        if (jsonString[i] == ':' && jsonString[i + 2] == '{')
                        {
                            bufferString = ReadWord(jsonString, ref i);
                            switch (bufferString)
                            {
                                case "SourcePath":
                                    if (jsonString[i] == ':')
                                    {
                                        var bufferOptions = new SourceOptions();
                                        bufferOptions.SourcePath = ReadWord(jsonString, ref i);
                                        sourcePath = bufferOptions;
                                    }
                                    break;
                            }
                        }
                        break;
                    case "targetPath":
                        if (jsonString[i] == ':' && jsonString[i + 2] == '{')
                        {
                            bufferString = ReadWord(jsonString, ref i);
                            switch (bufferString) //searching for required property and its value
                            {
                                case "TargetPath":
                                    if (jsonString[i] == ':')
                                    {
                                        var bufferOptions = new TargetOptions();
                                        bufferOptions.TargetPath = ReadWord(jsonString, ref i);
                                        targetPath = bufferOptions;
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
            //checking if all values are set
            if (archivePath.ArchivePath == null)
            {
                throw new InvalidOperationException("Archive dir: path not founded");
            }
            if (sourcePath.SourcePath == null)
            {
                throw new InvalidOperationException("Source dir: path not founded");
            }
            if (targetPath.TargetPath == null)
            {
                throw new InvalidOperationException("Target dir: path not founded");
            }
        }

        //method reading a word framed with ""
        public static string ReadWord(string jsonString, ref int readPosition)
        {
            string bufferString;
            int subStringStartPosition;
            int subStringLength = 0;
            int length = jsonString.Length;
            while (jsonString[readPosition] != '"')
            {
                if (readPosition == length - 1)
                {
                    return "";
                }
                readPosition++;
            }
            readPosition++;
            subStringStartPosition = readPosition;
            while (jsonString[readPosition] != '"')
            {
                if (readPosition == length - 1)
                {
                    return "";
                }
                readPosition++;
                subStringLength++;
            }
            readPosition++;
            bufferString = jsonString.Substring(subStringStartPosition, subStringLength);
            return bufferString;
        }

        //method for data extraction
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
