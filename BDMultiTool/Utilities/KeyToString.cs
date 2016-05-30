using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDMultiTool.Utilities {
    class KeyToString {

        private KeyToString() {

        }

        public static String convert(Keys key) {
            String returnValue = "";
            switch(key) {
                case Keys.D0:
                    returnValue = "0";
                    break;
                case Keys.D1:
                    returnValue = "1";
                    break;
                case Keys.D2:
                    returnValue = "2";
                    break;
                case Keys.D3:
                    returnValue = "3";
                    break;
                case Keys.D4:
                    returnValue = "4";
                    break;
                case Keys.D5:
                    returnValue = "5";
                    break;
                case Keys.D6:
                    returnValue = "6";
                    break;
                case Keys.D7:
                    returnValue = "7";
                    break;
                case Keys.D8:
                    returnValue = "8";
                    break;
                case Keys.D9:
                    returnValue = "9";
                    break;
                case Keys.Control:
                    returnValue = "CTRL";
                    break;
                case Keys.LControlKey:
                    returnValue = "L-CTRL";
                    break;
                case Keys.RControlKey:
                    returnValue = "R-CTRL";
                    break;
                default:
                    returnValue = key.ToString();
                    break;
            }
            return returnValue;
        }
    }
}
