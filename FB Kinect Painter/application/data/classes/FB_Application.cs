using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB_Kinect_Painter.application.data.classes {
    public static class FB_Application {
        // Depth Stream Resolution
        private const double depthStreamHeight = 480.0;
        private const double depthStreamWidth = 640.0;
        // User Screen Resolution
        public static int screenHeight = GetScreenHeight();
        public static int screenWidth = GetScreenWidth();

        public static MainWindow mw;

        public static int GetScreenHeight() {
            return (int)System.Windows.SystemParameters.PrimaryScreenHeight;
        }

        public static int GetScreenWidth() {
            return (int)System.Windows.SystemParameters.PrimaryScreenWidth;
        }
    }
}
