/*************************************************************************************/
/*                                  OUR REFERENCES                                   */
/*************************************************************************************/
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
using Microsoft.Kinect.Toolkit.Interaction;
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


namespace FB_Kinect_Painter
{
    /// <summary>

    /// </summary>
    public partial class MainWindow : Window
    {
        private const String APPNAME = "FB Kinect Painter 1.0 pre-alpha";
        private const String ERR_NOKINECT = "Nie znaleziono sensora Kinect. Aby kontynuować podłącz urządzenie!";
        private KinectSensorChooser sensorChooser;

        private void InitKinectInteractions(KinectChangedEventArgs args)
        {
            bool error = false;
            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.DepthStream.Range = DepthRange.Default;
                    args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                    error = true;
                }
            }

            if (args.NewSensor != null)
            {
                try
                {
                    args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();

                     try
                      {
                          args.NewSensor.DepthStream.Range = DepthRange.Default;
                          args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                          args.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                      }
                      catch (InvalidOperationException)
                      {
                          // Non Kinect for Windows devices do not support Near mode, so reset back to default mode.
                          args.NewSensor.DepthStream.Range = DepthRange.Default;
                          args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                          error = true;
                      }
                }
                catch (InvalidOperationException)
                {
                    error = true;
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
                if (!error)
                    kinectRegion.KinectSensor = args.NewSensor;
            }
        }

        private void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            if (args.NewSensor == null)
            {
                this.Hide();                
                MessageBox.Show(ERR_NOKINECT, APPNAME, MessageBoxButton.OK, MessageBoxImage.Warning);              
                
            }
            else
            {
                this.Show();
                InitKinectInteractions(args);
            }
            
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooser.KinectChanged += SensorChooserOnKinectChanged;
            this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.Start();
        }

        private void OnClickPressMe(object sender, RoutedEventArgs routedEventArgs)
        {
            MessageBox.Show("Success!");
        }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            new Window();
        }
    }
}
