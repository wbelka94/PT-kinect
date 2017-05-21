using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
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
using FB_Kinect_Painter.application.data.classes;

namespace FB_Kinect_Painter.application.data.windows {
    /// <summary>
    /// Interaction logic for SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window {
        public SaveWindow() {
            InitializeComponent();
            this.closeButton.Background = FB_Application.appButtonColor;
            this.Show();
        }

        private void OnClickCloseButton(object sender, RoutedEventArgs routedEventArgs) {
            this.Close();
        }
    }
}
