using BDMultiTool.Utilities.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDMultiTool.Persistence {
    class PersistenceUnit {
        private const int BUFFER_MAX_SIZE = 10;
        private Dictionary<String, PersistenceContainer> persistenceBuffer;
        private PersistenceSerializer serializer;
        private PersistenceDeserializer deserializer;

        public PersistenceUnit() {
            persistenceBuffer = new Dictionary<String, PersistenceContainer>();
            serializer = new PersistenceSerializer(BDMTConstants.WORKSPACE_PATH);
            deserializer = new PersistenceDeserializer(BDMTConstants.WORKSPACE_PATH);
        }

        public PersistenceContainer loadContainerByKey(String key) {
            return deserializer.loadContainerByKey(key.GetHashCode().ToString("X"));
        }

        public void deleteByKy(String key) {
            serializer.deleteByKey(key.GetHashCode().ToString("X"));
        }

        public void addToPersistenceBuffer(PersistenceContainer currentPersistencePair) {
            if(persistenceBuffer.ContainsKey(currentPersistencePair.key)) {
                persistenceBuffer[currentPersistencePair.key] = currentPersistencePair;
            } else {
                persistenceBuffer.Add(currentPersistencePair.key, currentPersistencePair);
            }

            checkForPersist();
        }

        private void checkForPersist() {
            if(persistenceBuffer.Count >= BUFFER_MAX_SIZE) {
                persist();
            }
        }

        public PersistenceContainer[] loadContainersByType(String type) {
            return deserializer.loadAllContainersByType(type);
        }

        public void persist() {
            if(persistenceBuffer.Count > 0) {
                Debug.WriteLine("\n\nStart saving...");
                foreach (PersistenceContainer currentContainer in persistenceBuffer.Values) {
                    serializer.updateEntry(currentContainer);
                }
                persistenceBuffer.Clear();
                serializer.save();
                Debug.WriteLine("Saved!");
            }
        }

        public static PersistenceContainer createPersistenceContainer(String key, String type, String[][] content) {
            XElement currentInnerRoot = new XElement(BDMTConstants.PERSISTENCE_TAG);
            foreach(String[] currentElement in content) {
                currentInnerRoot.Add(new XElement(currentElement[0], currentElement[1]));
            }
            return new PersistenceContainer(key.GetHashCode().ToString("X"), type, currentInnerRoot);
        }
    }
}
