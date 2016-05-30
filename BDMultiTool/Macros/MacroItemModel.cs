using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMultiTool.Macros {
    public class MacroItemModel : INotifyPropertyChanged {

        private String macroName_ { get; set; }
        public String macroName
        {
            get { return macroName_; }
            set
            {
                macroName_ = value;
                OnPropertyChanged("macroName");
            }
        }

        private bool Paused_ { get; set; }
        public bool Paused
        {
            get { return Paused_; }
            set
            {
                Paused_ = value;
                OnPropertyChanged("Paused");
            }
        }

        private bool AddMode_ { get; set; }
        public bool AddMode
        {
            get { return AddMode_; }
            set
            {
                AddMode_ = value;
                OnPropertyChanged("AddMode");
            }
        }

        private bool NotPaused_ { get; set; }
        public bool NotPaused
        {
            get { return NotPaused_; }
            set
            {
                NotPaused_ = value;
                OnPropertyChanged("NotPaused");
            }
        }

        private String coolDownTime_ { get; set; }
        public String coolDownTime
        {
            get { return coolDownTime_; }
            set
            {
                coolDownTime_ = value;
                OnPropertyChanged("coolDownTime");
            }
        }

        private String keyString_ { get; set; }
        public String keyString
        {
            get { return keyString_; }
            set
            {
                keyString_ = value;
                OnPropertyChanged("keyString");
            }
        }

        private String lifeTime_ { get; set; }
        public String lifeTime
        {
            get { return lifeTime_; }
            set
            {
                lifeTime_ = value;
                OnPropertyChanged("lifeTime");
            }
        }

        private float coolDownPercentage_ { get; set; }
        public float coolDownPercentage
        {
            get { return coolDownPercentage_; }
            set
            {
                coolDownPercentage_ = value;
                OnPropertyChanged("coolDownPercentage");
            }
        }

        private float lifeTimePercentage_ { get; set; }
        public float lifeTimePercentage
        {
            get { return lifeTimePercentage_; }
            set
            {
                lifeTimePercentage_ = value;
                OnPropertyChanged("lifeTimePercentage");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
