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
            primaryHeand = "Prawa";
        public static void LoadSettings() {
            try {
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
                using (StreamReader sr = new StreamReader("C:\\Users\\"+userName+"\\AppData\\Local\\Packages\\122490a7-91ea-401c-8149-57d942987d02_5rymg8rs03t1m\\LocalState\\settings.conf")) {
                    String tmp = sr.ReadLine();
                    if (tmp.Equals("Lewa"))
                        primaryHeand = tmp;
                    path = sr.ReadLine();
                }
            } catch (Exception e) {

            }
        }
    }
}
