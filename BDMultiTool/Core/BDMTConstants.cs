using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMultiTool.Utilities.Core {
    class BDMTConstants {
        //general
        public const String WORKSPACE_NAME = "bdmtWorkspace";
        public const String WORKSPACE_PATH = WORKSPACE_NAME + "\\";
        public const String PERSISTENCE_FILE = "persistence.xml";

        //xml constants
        public const String PERSISTENCE_ROOT_TAG = "Persistences";
        public const String PERSISTENCE_TAG = "Persistence";
        public const String PERSISTENCE_ATTRIBUTE_ID_TAG = "ID";
        public const String PERSISTENCE_ATTRIBUTE_TYPE_TAG = "Type";
    }
}
