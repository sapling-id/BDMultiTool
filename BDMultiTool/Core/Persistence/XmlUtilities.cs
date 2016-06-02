using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BDMultiTool.Core.Persistence {
    class XmlUtilities {

        public static void checkForPersistenceFile(String workspacePath) {
            if (!File.Exists(workspacePath + BDMTConstants.PERSISTENCE_FILE) || new FileInfo(workspacePath + BDMTConstants.PERSISTENCE_FILE).Length == 0) {
                createNewEmptyPersistenceFile(workspacePath);
            } 

        }

        private static void createNewEmptyPersistenceFile(String workspacePath) {
            XmlWriterSettings xmlWriterSettings;
            XmlWriter xmlWriter;
            xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.IndentChars = "\t";
            xmlWriter = XmlWriter.Create(workspacePath + BDMTConstants.PERSISTENCE_FILE, xmlWriterSettings);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement(BDMTConstants.PERSISTENCE_ROOT_TAG);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Dispose();
            xmlWriter.Close();
        }
    }
}
