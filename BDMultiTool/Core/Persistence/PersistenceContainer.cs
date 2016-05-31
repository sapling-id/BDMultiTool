using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDMultiTool.Persistence {
    public class PersistenceContainer {
        public String key { get; set; }
        public String type { get; set; }
        public XElement content { get; set; }

        public PersistenceContainer(String key, String type, XElement content) {
            this.key = key;
            this.type = type;
            this.content = content;
        }

        public override bool Equals(object obj) {
            if(obj != null && obj.GetType() == typeof(PersistenceContainer)) {
                return ((PersistenceContainer)obj).GetHashCode() == this.GetHashCode();
            } else {
                return false;
            }
        }

        public override int GetHashCode() {
            return key.GetHashCode();
        }
    }
}
