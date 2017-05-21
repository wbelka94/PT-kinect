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
using System.IO;

namespace FB_Kinect_Painter.application.data.classes {
    public static class FB_Application {
        // Depth Stream Resolution
        private const double depthStreamHeight = 480.0;
        private const double depthStreamWidth = 640.0;
        // User Screen Resolution
        public static int screenHeight = GetScreenHeight();
        public static int screenWidth = GetScreenWidth();
        // Font 
        public static FontFamily appFont = new FontFamily(new Uri("pack://application:,,,/"), "./application/graphics/fonts/#Capture it");
        public static SolidColorBrush appFontColor = Brushes.White;
        // Button
        public static SolidColorBrush appButtonColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#000000"));
        // Windows
        public static MainWindow mw;
    
        public static int GetScreenHeight() {
            return (int)System.Windows.SystemParameters.PrimaryScreenHeight;
        }

        public static int GetScreenWidth() {
            return (int)System.Windows.SystemParameters.PrimaryScreenWidth;
        }
    }
}
