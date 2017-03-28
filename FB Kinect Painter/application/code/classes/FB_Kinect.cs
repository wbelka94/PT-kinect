/*************************************************************************************/
/*                                  OUR REFERENCES                                   */
/*************************************************************************************/
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
using Microsoft.Kinect.Toolkit.Interaction;
using FB_Kinect_Painter.application.windows;
using FB_Kinect_Painter.application.code.windows;
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
namespace FB_Kinect_Painter.application.code.classes {
    /*********************************************************************************/
    public static class FB_Kinect {
        /*****************************************************************************/
        /*                                  ERRORS                                   */
        /*****************************************************************************/
        public const String ERR_NOKINECT = "Sensor Kinect został odłączony. Aby kontynuować podłącz urządzenie!";
        public const String ERR_NOKINECT_START = "Nie znaleziono sensora Kinect. Aplikacja zostanie zamknięta!";
        /*****************************************************************************/
        /*                                    APP                                    */
        /*****************************************************************************/
        public const String APP_NAME = "FB Kinect Painter 1.0 pre-alpha";
        /*****************************************************************************/
        public static KinectSensorChooser sensorChooser;
        public static Window mw; // główne okno aplikacji
        public static Window iw; // intro window
        public static Window ew = null; // exit window
        /*****************************************************************************/
        public static void InitKinectInteractions(object sender, KinectChangedEventArgs args) {
            bool error = false;

            if (args.OldSensor != null) {
                try {
                    args.OldSensor.DepthStream.Range = DepthRange.Default;
                    args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                } catch (InvalidOperationException) {
                    error = true;
                }
            }

            if (args.NewSensor != null) {
                try {
                    args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();

                    try {
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                        args.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                    } catch (InvalidOperationException) {
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                        error = true;
                    }
                } catch (InvalidOperationException) {
                    error = true;
                }
            }

            if (!error) {
                (mw as MainWindow).kinectRegion.KinectSensor = args.NewSensor;

            }
        }
        /*****************************************************************************/
        public static void ChangeKinectRegionExit(Window w) {
            (w as ExitWindow).kinectRegion.KinectSensor = (FB_Kinect.mw as MainWindow).kinectRegion.KinectSensor;
            (FB_Kinect.mw as MainWindow).kinectRegion.KinectSensor = null;
        }
        /*****************************************************************************/
        public static void ChangeKinectRegionMainWindow(Window w) {
            (mw as MainWindow).kinectRegion.KinectSensor = (FB_Kinect.ew as ExitWindow).kinectRegion.KinectSensor;
            (FB_Kinect.ew as ExitWindow).kinectRegion.KinectSensor = null;
        }
        /*****************************************************************************/
        public static void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args) {
            if (args.NewSensor == null) {
                MessageBox.Show(FB_Kinect.ERR_NOKINECT, FB_Kinect.APP_NAME, MessageBoxButton.OK, MessageBoxImage.Warning);
            } else {
                InitKinectInteractions(sender, args);
                iw.Close();        
            }
        }
    }
    /*********************************************************************************/
}
/*************************************************************************************/
