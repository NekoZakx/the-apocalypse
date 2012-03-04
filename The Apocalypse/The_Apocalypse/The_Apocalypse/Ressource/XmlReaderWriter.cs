using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace The_Apocalypse
{
    class XmlReaderWriter
    {
        XmlTextReader textReader;
        XmlTextWriter textWriter;

        public string ReadNextTextNode()
        {
            do
            {
                textReader.Read();
            } while (textReader.NodeType != XmlNodeType.Text);
                
            return textReader.Value.ToString();
        }

        public void OpenWrite(string filename)
        {
            if (textReader == null)
            {
                textWriter = new XmlTextWriter(filename, null);
                textWriter.WriteStartDocument();
            }
            else
                throw new Exception("Impossible d'écrire lorsque le fichier est ouvert en lecture");
            return;
        }
        public void OpenRead(string filename)
        {
            if(textWriter == null)
                textReader = new XmlTextReader(filename);
            else
                throw new Exception("Impossible de lire lorsque le fichier est ouvert en écriture");
        }

        public void WriteNextTextNode(string nodeName,string value)
        {
            textWriter.WriteStartElement(nodeName, "");
            textWriter.WriteString(value);
            textWriter.WriteEndElement();
        }
        public void WriteCategory(string categoryName)
        {
            textWriter.WriteStartElement(categoryName, "");
        }
        public void WriteEndCategory()
        {
            textWriter.WriteEndElement();
        }

        public void ReadClose()
        {
            textReader.Close();
        }
        public void WriteClose()
        {
            textWriter.WriteEndDocument();
            textWriter.Close();
        }

        public string FindReadNode(string NodeName)
        {
            textReader = new XmlTextReader(textReader.BaseURI);
            while ((textReader.NodeType != XmlNodeType.Element || textReader.Name != NodeName) && textReader.Read());
            if (!textReader.Read())
                throw new Exception("L'élément recherché est introuvable");

            return textReader.Value.ToString();
        }
    }
}
