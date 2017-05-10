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
namespace FB_Kinect_Painter.application.code.windows {
    /*********************************************************************************/
    public partial class ExitWindow : Window {
        /*****************************************************************************/
        public ExitWindow() {
            Topmost = true;
            InitializeComponent();
            FB_Kinect.ChangeKinectRegionExit(this);
        }
        /*****************************************************************************/
        private void OnExitWindowYesButton(object sender, RoutedEventArgs routedEventArgs) {
            FB_Kinect.ew.Close();
            FB_Kinect.mw.Close();
        }
        /*****************************************************************************/
        private void OnExitWindowNoButton(object sender, RoutedEventArgs routedEventArgs) {
            FB_Kinect.ChangeKinectRegionMainWindow(FB_Kinect.mw);
            FB_Kinect.ew.Close();
            FB_Kinect.ew = null;
        }
        /*****************************************************************************/
    }
    /*********************************************************************************/
}
/*************************************************************************************/
