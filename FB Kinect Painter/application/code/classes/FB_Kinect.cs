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
using System.Drawing;
/*************************************************************************************/
namespace FB_Kinect_Painter.application.code.classes {
    /*********************************************************************************/
    public static class FB_Kinect {
        /*****************************************************************************/
        /*                                 CONSTANTS                                 */
        /*****************************************************************************/
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
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
                    //args.NewSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();
                    args.NewSensor.SkeletonFrameReady += FramesReady;

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
        /*****************************************************************************/
        private static void FramesReady(object sender, SkeletonFrameReadyEventArgs e) {
            SkeletonFrame SFrame = e.OpenSkeletonFrame();
            if (SFrame == null)
                return;
            Skeleton[] Skeletons = new Skeleton[SFrame.SkeletonArrayLength];
            SFrame.CopySkeletonDataTo(Skeletons);

            foreach (Skeleton S in Skeletons) {
                if (S.TrackingState == SkeletonTrackingState.Tracked) {
                                           
                        GetJoint(JointType.HandRight, S);
                    Skeletons = null;
                    
                }
            }
        }
        /*****************************************************************************/
        private static System.Windows.Point GetJoint(JointType j, Skeleton S) {
            SkeletonPoint Sloc = S.Joints[j].Position;
            DepthImagePoint Cloc = sensorChooser.Kinect.CoordinateMapper.MapSkeletonPointToDepthPoint(Sloc, DepthImageFormat.Resolution640x480Fps30);
            System.Windows.Point point = Mouse.GetPosition(mw);
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)(Cloc.X * FB_Visual.proportionWidth),
                                                                            (int)(Cloc.Y * FB_Visual.proportionHeight));
            (mw as MainWindow).TMPlabel.Content = "X: " + Cloc.X + " Y: " + Cloc.Y + " Mouse X: " + point.X + " Y: " + point.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN, (int)Cloc.X, (int)Cloc.Y, 0, 0);

            return new System.Windows.Point(Cloc.X, Cloc.Y);
        }
        /*****************************************************************************/
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        
        /*public static Point GetMousePositionWindowsForms() {
            Point point;
            System.Threading.Thread.Sleep(2000);
            point = Mouse.GetPosition(mw);
            (mw as MainWindow).Content = "X: " + point.X + "Y: " + point.Y;
            return new Point(point.X, point.Y);
        }*/
    }
    /*********************************************************************************/
}
/*************************************************************************************/
