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
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window {
        public NotificationWindow() {
            InitializeComponent();
        }

        private void closeButton_MouseUp(object sender, MouseButtonEventArgs e) {
            //this.Visibility = Visibility.Hidden;
            ((Storyboard)FindResource("SlideOut")).Begin(this);
        }
    }
}
