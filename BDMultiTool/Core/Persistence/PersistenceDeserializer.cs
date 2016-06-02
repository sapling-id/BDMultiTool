using BDMultiTool.Core.Persistence;
using BDMultiTool.Utilities.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BDMultiTool.Persistence {
    class PersistenceDeserializer {
        private String workspacePath;
        private XDocument persistenceDocument;
        private XElement rootElement;


        public PersistenceDeserializer(String workspacePath) {
            this.workspacePath = workspacePath;

            initialize();
        }

        private void initialize() {
            XmlUtilities.checkForPersistenceFile(workspacePath);

            persistenceDocument = XDocument.Load(workspacePath + BDMTConstants.PERSISTENCE_FILE);
            rootElement = persistenceDocument.Element(BDMTConstants.PERSISTENCE_ROOT_TAG);
        }

        public PersistenceContainer loadContainerByKey(String key) {
            XElement searchedPersistence = null;

            IEnumerable<XElement> tempDebug = rootElement.Elements(BDMTConstants.PERSISTENCE_TAG);
            IEnumerable<XElement> searchedPersistences = rootElement.Elements(BDMTConstants.PERSISTENCE_TAG)
                               .Where(persistence => persistence.Attribute(BDMTConstants.PERSISTENCE_ATTRIBUTE_ID_TAG).Value == key);

            if(searchedPersistences != null && searchedPersistences.Count() > 0) {
                searchedPersistence = searchedPersistences.First();
            }


            if (searchedPersistence != null) {
                return new PersistenceContainer(key, searchedPersistence.Attribute(BDMTConstants.PERSISTENCE_ATTRIBUTE_TYPE_TAG).Value, searchedPersistence);
            } else {
                return null;
            }
        }

        public PersistenceContainer[] loadAllContainersByType(String type) {
            List<PersistenceContainer> temporaryPersistencesList = new List<PersistenceContainer>();
            IEnumerable<XElement> searchedPersistences = rootElement.Elements(BDMTConstants.PERSISTENCE_TAG)
                                                                    .Where(persistence => persistence.Attribute(BDMTConstants.PERSISTENCE_ATTRIBUTE_TYPE_TAG).Value == type);
            if(searchedPersistences != null) {
                foreach (XElement currentElement in searchedPersistences) {
                    temporaryPersistencesList.Add(new PersistenceContainer(currentElement.Attribute(BDMTConstants.PERSISTENCE_ATTRIBUTE_ID_TAG).Value,
                                                                           currentElement.Attribute(BDMTConstants.PERSISTENCE_ATTRIBUTE_TYPE_TAG).Value,
                                                                           currentElement));
                }
            }

            return temporaryPersistencesList.ToArray();
        }

    }
}
