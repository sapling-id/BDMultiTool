using BDMultiTool.Macros;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BDMultiTool {
    /// <summary>
    /// Interaction logic for Overlay.xaml
    /// </summary>
    public partial class Overlay : Window {
        private bool menuVisible;

        public Overlay() {
            this.Title = Guid.NewGuid().ToString();
            InitializeComponent();
            this.Background = null;
            menuVisible = false;
        }

        public MovableUserControl addWindowToGrid(UserControl userControl, String title) {
            MovableUserControl currentInnerWindow = new MovableUserControl(RootGrid);
            currentInnerWindow.setTitle(title);
            currentInnerWindow.setGridContent(userControl);
            currentInnerWindow.Visibility = Visibility.Hidden;
            RootGrid.Children.Add(currentInnerWindow);
            return currentInnerWindow;
        }

        private void mainMenu_Click(object sender, RoutedEventArgs e) {
            if(!menuVisible) {
                ((Storyboard)FindResource("SlideIn")).Begin(mainMenu);
                menuVisible = true;
            } else {
                ((Storyboard)FindResource("SlideOut")).Begin(mainMenu);
                menuVisible = false;
            }

        }

        private void toDoListMenu_Click(object sender, RoutedEventArgs e) {
        }

        private void macroMenu_Click(object sender, RoutedEventArgs e) {
            MacroManagerThread.macroManager.showMacroMenu();
        }

        private void exitMenu_Click(object sender, RoutedEventArgs e) {
            App.exit();
        }

        private void RootGrid_MouseDown(object sender, MouseButtonEventArgs e) {
            if(menuVisible) {
                ((Storyboard)FindResource("SlideOut")).Begin(mainMenu);
                menuVisible = false;
            }
        }

        private void customButton_MouseEnter(object sender, MouseEventArgs e) {
            ((Button)sender).Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(20, 250, 0, 0));
        }

        private void customButton_MouseLeave(object sender, MouseEventArgs e) {
            ((Button)sender).Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0));
        }
    }
}
