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
        private void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            if (args.NewSensor == null)
            {
                MessageBox.Show(ERR_NOKINECT, APPNAME, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooser.KinectChanged += SensorChooserOnKinectChanged;
            this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.Start();
        }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }
    }
}
