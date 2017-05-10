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
using System.Windows.Shapes;

namespace FB_Kinect_Painter.application.code.windows {
    /// <summary>
    /// Interaction logic for DecisionWindow.xaml
    /// </summary>
    public partial class DecisionWindow : Window {
        public bool value = false;
        internal bool done = false;

        public DecisionWindow(String q, RoutedEventHandler eh) {
            InitializeComponent();
            Question.Content = q;
            Topmost = true;
            YesButton.Click += eh;
            Show();
        }

        private void OnClickYesButton(object sender, RoutedEventArgs routedEventArg) {
            value = true;
            done = true;
            Close();
        }

        private void OnClickNoButton(object sender, RoutedEventArgs routedEventArg) {
            value = false;
            done = true;
            Close();
        }
    }
}
