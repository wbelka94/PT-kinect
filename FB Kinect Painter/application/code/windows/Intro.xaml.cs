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
namespace FB_Kinect_Painter.application.windows {
    public partial class Intro : Window {
        /*****************************************************************************/
        public Intro() {
            InitializeComponent();
            int countKinect = KinectSensor.KinectSensors.Count;
            FB_Kinect.iw = this;
        
           if (countKinect <= 0) {
                FB_Kinect.iw.Show();
                MessageBox.Show(FB_Kinect.ERR_NOKINECT_START, FB_Kinect.APP_NAME, MessageBoxButton.OK, MessageBoxImage.Error);
                FB_Kinect.iw.Close();
            } else {
                FB_Kinect.mw = new MainWindow();
                FB_Kinect.mw.Show();
                FB_Kinect.iw.Show();
            }
        }
        /*****************************************************************************/
    }
}
/*************************************************************************************/

