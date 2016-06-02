using BDMultiTool.Core;
using BDMultiTool.Core.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BDMultiTool.Persistence {
    class PersistenceSerializer {
        private String workspacePath;
        private XDocument persistenceDocument;
        private XElement rootElement;


        public PersistenceSerializer(String workspacePath) {
            this.workspacePath = workspacePath;

            initialize();
        }

        private void initialize() {
            XmlUtilities.checkForPersistenceFile(workspacePath);

            persistenceDocument = XDocument.Load(workspacePath + BDMTConstants.PERSISTENCE_FILE);
            rootElement = persistenceDocument.Element(BDMTConstants.PERSISTENCE_ROOT_TAG);
        }

        public void deleteByKey(String key) {
            IEnumerable<XElement> searchedPersistences = rootElement.Elements(BDMTConstants.PERSISTENCE_TAG)
                               .Where(persistence => persistence.Attribute(BDMTConstants.PERSISTENCE_ATTRIBUTE_ID_TAG).Value == key);

            if (searchedPersistences != null && searchedPersistences.Count() > 0) {
                searchedPersistences.First().Remove();
                save();
            }

        }

        public void updateEntry(PersistenceContainer currentPersistencePair) {
            XElement searchedPersistence = null;
            IEnumerable<XElement> searchedPersistences = rootElement.Elements(BDMTConstants.PERSISTENCE_TAG)
                               .Where(persistence => persistence.Attribute(BDMTConstants.PERSISTENCE_ATTRIBUTE_ID_TAG).Value == currentPersistencePair.key);

            if (searchedPersistences != null && searchedPersistences.Count() > 0) {
                searchedPersistence = searchedPersistences.First();
            }



            if (searchedPersistence != null) {
                searchedPersistence.RemoveNodes();
                searchedPersistence.Add(currentPersistencePair.content.Elements());
            } else {
                XElement newPersistenceElement = new XElement(BDMTConstants.PERSISTENCE_TAG, currentPersistencePair.content.Elements());
                newPersistenceElement.Add(new XAttribute(BDMTConstants.PERSISTENCE_ATTRIBUTE_ID_TAG, currentPersistencePair.key));
                newPersistenceElement.Add(new XAttribute(BDMTConstants.PERSISTENCE_ATTRIBUTE_TYPE_TAG, currentPersistencePair.type));

                rootElement.Add(newPersistenceElement);
            }
        }

        public void save() {
            persistenceDocument.Save(workspacePath + BDMTConstants.PERSISTENCE_FILE);
        }
    }
}
