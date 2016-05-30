using BDMultiTool.Macros;
using BDMultiTool.Utilities;
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
    /// Interaction logic for MacroAddControl.xaml
    /// </summary>
    public partial class MacroAddControl : UserControl {
        private ObservableCollection<BoxKeyboardPair> comboBoxPairs = new ObservableCollection<BoxKeyboardPair>();
        private ObservableCollection<BoxKeyboardPair> listBoxPairs = new ObservableCollection<BoxKeyboardPair>();

        public MacroAddControl() {
            InitializeComponent();
            System.Windows.Forms.Keys[] keys = Enum.GetValues(typeof(System.Windows.Forms.Keys)).Cast<System.Windows.Forms.Keys>().ToArray();

            foreach (System.Windows.Forms.Keys key in keys) {
                comboBoxPairs.Add(new BoxKeyboardPair(KeyToString.convert(key), key));
            }

            keysToSelect.DisplayMemberPath = "_Key";
            keysToSelect.SelectedValuePath = "_Value";
            keysToSelect.ItemsSource = comboBoxPairs;

            currentKeysToBeAdded.DisplayMemberPath = "_Key";
            currentKeysToBeAdded.SelectedValuePath = "_Value";
            currentKeysToBeAdded.ItemsSource = listBoxPairs;
        }

        
        private void deleteListBoxItem(object sender, RoutedEventArgs e) {
            listBoxPairs.Remove((BoxKeyboardPair)currentKeysToBeAdded.SelectedItem);

        }

        private void keysToSelect_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(keysToSelect.SelectedIndex >= 0) {
                listBoxPairs.Add(new BoxKeyboardPair(KeyToString.convert((System.Windows.Forms.Keys)keysToSelect.SelectedValue), (System.Windows.Forms.Keys)keysToSelect.SelectedValue));
            }

        }

        private void checkBox_Checked(object sender, RoutedEventArgs e) {
            lifeTime.IsEnabled = false;
        }

        private void disableLifeTimeCheckBox_Unchecked(object sender, RoutedEventArgs e) {
            lifeTime.IsEnabled = true;
        }

        private void addMacroButton_Click(object sender, RoutedEventArgs e) {
            CycleMacro currentMacro = new CycleMacro();

            foreach(BoxKeyboardPair currentBoxPair in listBoxPairs) {
                currentMacro.addKey(currentBoxPair._Value);
            }

            currentMacro.name = macroName.Text;

            try {
                currentMacro.interval = int.Parse(coolDownTime.Text);

                if ((bool)disableLifeTimeCheckBox.IsChecked) {
                    currentMacro.lifetime = -1;
                } else {
                    currentMacro.lifetime = int.Parse(lifeTime.Text);
                }

                MacroManagerThread.macroManager.addMacro(currentMacro);
            } catch(Exception exception) {
                MessageBox.Show("Wrong input, make sure to enter numbers into the interval and life time fields", "Parsing error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void resetFormButton_Click(object sender, RoutedEventArgs e) {
            macroName.Text = "Name";
            coolDownTime.Text = "2000";
            lifeTime.Text = "2000";
            disableLifeTimeCheckBox.IsChecked = false;
            keysToSelect.SelectedIndex = -1;
            listBoxPairs.Clear();
        }

        private void customButton_MouseEnter(object sender, MouseEventArgs e) {
            ((Button)sender).Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 114, 23, 25));
        }

        private void customButton_MouseLeave(object sender, MouseEventArgs e) {
            ((Button)sender).Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 249, 60, 64));
        }
    }
}
