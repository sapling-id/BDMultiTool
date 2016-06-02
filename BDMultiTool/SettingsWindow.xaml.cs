using BDMultiTool.Core.Notification;
using BDMultiTool.Persistence;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : UserControl {
        public SettingsWindow() {
            InitializeComponent();
        }

        private void resetWindowPositionButton_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Do you really want to reset the sub window's locations?", "Reset window location", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes) {
                PersistenceUnitThread.persistenceUnit.deleteByType(typeof(MovableUserControl).Name);
                ToasterThread.toaster.popToast("Info", "All windows have successfully been reset!");
            }

        }

        private void customButton_MouseEnter(object sender, MouseEventArgs e) {
            ((Button)sender).Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 114, 23, 25));
        }

        private void customButton_MouseLeave(object sender, MouseEventArgs e) {
            ((Button)sender).Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 249, 60, 64));
        }
    }
}
