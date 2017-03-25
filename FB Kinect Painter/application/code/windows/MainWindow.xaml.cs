/*************************************************************************************/
/*                                  OUR REFERENCES                                   */
/*************************************************************************************/
using FB_Kinect_Painter.application.code.classes;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
using Microsoft.Kinect.Toolkit.Interaction;
/*************************************************************************************/
/*                                     DEFAULT                                       */
/*************************************************************************************/
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
/*************************************************************************************/
namespace FB_Kinect_Painter {
    public partial class MainWindow : Window {
        /*****************************************************************************/
        
        /*****************************************************************************/
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs) {
            FB_Kinect.sensorChooser = new KinectSensorChooser();
            FB_Kinect.sensorChooser.KinectChanged += FB_Kinect.SensorChooserOnKinectChanged;
            this.sensorChooserUi.KinectSensorChooser = FB_Kinect.sensorChooser;
            FB_Kinect.sensorChooser.Start();
        }
        /*****************************************************************************/
        private void OnClickPressMe(object sender, RoutedEventArgs routedEventArgs) {
            MessageBox.Show("Success!");
        }
        /*****************************************************************************/
        public MainWindow() {
            InitializeComponent();
            Loaded += OnLoaded;
        }
        /*****************************************************************************/
    }
}
/*************************************************************************************/
