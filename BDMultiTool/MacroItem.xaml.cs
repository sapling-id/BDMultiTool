using BDMultiTool.Macros;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for MacroItem.xaml
    /// </summary>
    public partial class MacroItem : UserControl {
        public MacroItem() {
            InitializeComponent();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e) {
            MacroManagerThread.macroManager.removeMacroByName((this.DataContext as MacroItemModel).macroName);

        }

        private void resetButton_Click(object sender, RoutedEventArgs e) {
            MacroManagerThread.macroManager.getMacroByName((this.DataContext as MacroItemModel).macroName).resetAll();
            (this.DataContext as MacroItemModel).Paused = true;
            (this.DataContext as MacroItemModel).NotPaused = false;
        }

        private void playButton_Click(object sender, RoutedEventArgs e) {
            MacroManagerThread.macroManager.getMacroByName((this.DataContext as MacroItemModel).macroName).resume();
            if(!MacroManagerThread.macroManager.getMacroByName((this.DataContext as MacroItemModel).macroName).paused) {
                (this.DataContext as MacroItemModel).Paused = false;
                (this.DataContext as MacroItemModel).NotPaused = true;
            }

        }

        private void pauseButton_Click(object sender, RoutedEventArgs e) {
            MacroManagerThread.macroManager.getMacroByName((this.DataContext as MacroItemModel).macroName).pause();
            if(MacroManagerThread.macroManager.getMacroByName((this.DataContext as MacroItemModel).macroName).paused) {
                (this.DataContext as MacroItemModel).Paused = true;
                (this.DataContext as MacroItemModel).NotPaused = false;
            }

        }

        private void customButton_MouseEnter(object sender, MouseEventArgs e) {
            ((Button)sender).Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(80, 230, 230, 230));
        }

        private void customButton_MouseLeave(object sender, MouseEventArgs e) {
            ((Button)sender).Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0));
        }

        private void addModeBackground_MouseUp(object sender, MouseButtonEventArgs e) {
            MacroManagerThread.macroManager.showCreateMacroMenu();
        }

        public void addModeActive(bool setActive) {
            if(setActive) {
                addModeBackground.Visibility = Visibility.Visible;
                addModeForeground.Visibility = Visibility.Visible;
            } else {
                addModeBackground.Visibility = Visibility.Hidden;
                addModeForeground.Visibility = Visibility.Hidden;
            }
        }
    }
}
