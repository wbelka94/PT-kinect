using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FB_Kinect_Painter.application.data.classes {
    class Settings { 
        public static String path = "",
            primaryHeand = "right";
        public static void LoadSettings() {
            try {
                using (StreamReader sr = new StreamReader("C:/Users/Wojtek/Documents/Visual Studio 2015/Projects/PT-kinect/FB Kinect Painter/bin/Debug/settings.txt")) {
                    String tmp = sr.ReadLine();
                    if (tmp.Equals("left"))
                        primaryHeand = tmp;
                    path = sr.ReadLine();
                }
            } catch (Exception e) {

            }
        }
    }
}
