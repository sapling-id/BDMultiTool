using BDMultiTool.Macros;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BDMultiTool {
    /// <summary>
    /// Interaction logic for MacroGallery.xaml
    /// </summary>
    public partial class MacroGallery : UserControl {
        public volatile ObservableCollection<MacroItemModel> macroItemModels;
        private MacroItemModel blankMacroItemToAddNewMacros;

        public MacroGallery() {
            InitializeComponent();
            blankMacroItemToAddNewMacros = new MacroItemModel();
            blankMacroItemToAddNewMacros.AddMode = true;
        }

        public void initialize() {
            macroItemModels = new ObservableCollection<MacroItemModel>();
            listBoxGallery.ItemsSource = macroItemModels;
            macroItemModels.Add(blankMacroItemToAddNewMacros);
        }


        public void addMacro(MacroItemModel macroItemModel) {
            if(macroItemModels.Contains(blankMacroItemToAddNewMacros)) {
                macroItemModels.Remove(blankMacroItemToAddNewMacros);
            }

            macroItemModels.Add(macroItemModel);
            macroItemModels.Add(blankMacroItemToAddNewMacros);
        }

    }
}
