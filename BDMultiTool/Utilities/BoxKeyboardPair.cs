using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMultiTool.Utilities {
    class BoxKeyboardPair {
        public String _Key { get; set; }
        public System.Windows.Forms.Keys _Value { get; set; }

        public BoxKeyboardPair(String _key, System.Windows.Forms.Keys _value) {
            _Key = _key;
            _Value = _value;
        }
    }
}
