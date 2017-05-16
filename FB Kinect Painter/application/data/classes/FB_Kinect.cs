/*************************************************************************************/
/*                                  OUR REFERENCES                                   */
/*************************************************************************************/
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
using System.IO;
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
namespace FB_Kinect_Painter.application.data.classes {
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

                    //smoothing
                    TransformSmoothParameters smoothingParam = new TransformSmoothParameters();
                    {
                        smoothingParam.Smoothing = 0.7f;
                        smoothingParam.Correction = 0.3f;
                        smoothingParam.Prediction = 1.0f;
                        smoothingParam.JitterRadius = 1.0f;
                        smoothingParam.MaxDeviationRadius = 1.0f;
                    };
                    /*TransformSmoothParameters smoothingParam = new TransformSmoothParameters();
                    {
                        smoothingParam.Smoothing = 0.75f;
                        smoothingParam.Correction = 0.0f;
                        smoothingParam.Prediction = 0.0f;
                        smoothingParam.JitterRadius = 0.05f;
                        smoothingParam.MaxDeviationRadius = 0.04f;
                    };*/


                    //args.NewSensor.SkeletonStream.Enable();
                    args.NewSensor.SkeletonFrameReady += FramesReady;

                    args.NewSensor.SkeletonStream.Enable(smoothingParam);

                    try {
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                        args.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
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
                FB_Application.mw.kinectRegion.KinectSensor = args.NewSensor;
                FB_Application.mw.kinectRegion.IsCursorVisible = false;
            }
        }
        /*****************************************************************************/
        public static void ChangeKinectRegionExit(Window w) {
            //(w as ExitWindow).kinectRegion.KinectSensor = (FB_Kinect.mw as MainWindow).kinectRegion.KinectSensor;
            // (FB_Kinect.mw as MainWindow).kinectRegion.KinectSensor = null;

        }
        /*****************************************************************************/
        public static void ChangeKinectRegionMainWindow(Window w) {
            // (mw as MainWindow).kinectRegion.KinectSensor = (FB_Kinect.ew as ExitWindow).kinectRegion.KinectSensor;
            // (FB_Kinect.ew as ExitWindow).kinectRegion.KinectSensor = null;
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
                    CurosrUpdate(S);
                    Skeletons = null;
                }
            }
        }
        /*****************************************************************************/

        private static void CurosrUpdate(Skeleton S) {
            SkeletonPoint Sloc = S.Joints[JointType.HandRight].Position;
            DepthImagePoint Cloc = sensorChooser.Kinect.CoordinateMapper.MapSkeletonPointToDepthPoint(Sloc, DepthImageFormat.Resolution640x480Fps30);


            /*System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)((0.5+Sloc.X) * FB_Visual.GetScreenWidth()),
                                                                            (int)((0.5+(-1*Sloc.Y)) * FB_Visual.GetScreenHeight()+200));*/

            /*coding4fun
             var scaledJoint = S.Joints[JointType.HandRight].ScaleTo(FB_Visual.GetScreenWidth(), FB_Visual.GetScreenHeight(), .99f, .5f);
             System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)scaledJoint.Position.X,
                                                                            (int)scaledJoint.Position.Y);*/
            System.Windows.Point point = new System.Windows.Point();
            point.X = (0.5 + Sloc.X) * FB_Application.GetScreenWidth();
            point.Y = (0.5 + (-1 * Sloc.Y)) * FB_Application.GetScreenHeight() + 200;

            if (S.Joints[JointType.HandLeft].Position.Y > S.Joints[JointType.ElbowLeft].Position.Y) {
                FB_Application.mw.workSheet.activePaintingTool.Paint(point.X, point.Y);

                //mouse_event(MOUSEEVENTF_LEFTDOWN, (int)point.X, (int)point.Y, 0, 0);
            } else {
                //mouse_event(MOUSEEVENTF_LEFTUP, (int)point.X, (int)point.Y, 0, 0);
                SetMousePosition(point.X, point.Y, false);
            }
        }
        /*****************************************************************************/
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        static public void SetMousePosition(double x, double y, bool lb_pressed) {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)x, (int)y);
            if (lb_pressed) {
                mouse_event(MOUSEEVENTF_LEFTDOWN, (int)x, (int)y, 0, 0);
            } else {
                mouse_event(MOUSEEVENTF_LEFTUP, (int)x, (int)y, 0, 0);
            }
        }
    }
    /*********************************************************************************/
}
/*************************************************************************************/
