using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Lab4
{
    public class XmlFormer
    {
        public XmlFormer()
        {

        }

        public string GetXml(object obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            string result = string.Empty;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, obj);
                memoryStream.Position = 0;
                result = new StreamReader(memoryStream).ReadToEnd();
            }
            return result;
        }

        public string GetXsd(string xmlString)
        {
            XmlReader reader = XmlReader.Create(new StringReader(xmlString));
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            XmlSchemaInference schema = new XmlSchemaInference();
            schemaSet = schema.InferSchema(reader);
            string result = "";
            foreach (XmlSchema s in schemaSet.Schemas())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    s.Write(memoryStream);
                    memoryStream.Position = 0;
                    result = new StreamReader(memoryStream).ReadToEnd();
                }
            }
            return result;
        }
    }
}
