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
using Microsoft.Kinect.Toolkit.Controls;

namespace FB_Kinect_Painter.application.code.windows {    

    public partial class ChoseFileWindow : Window {
        string[] files;
        RoutedEventHandler eh;
        public ChoseFileWindow(/*string[] files,*/ RoutedEventHandler eh) {
            InitializeComponent();
            this.eh = eh;
            try {
                this.files = Directory.GetFiles("saved_pictures", "*.fbkp");
            } catch(Exception e) {
                title.Content = e.ToString();
            }
            DisplayFiles();
            this.Show();
        }

        private void DisplayFiles() {
            int c = 0, r = 0;
            foreach(string file in files) {
                KinectTileButton fb = new KinectTileButton();
                fb.Content = file;
                fb.Width = 100;
                fb.Height = 100;
                fb.Click += eh;
                fb.Click += OnClickFileButton;
                Grid.SetRow(fb, r);
                Grid.SetColumn(fb, c);

                //miniaturki
                BitmapImage bimg = new BitmapImage();
                bimg.BeginInit();
                string uri = file.Substring(0, file.Length - 5);
                bimg.UriSource = new Uri("C:\\Users\\Wojtek\\Documents\\Visual Studio 2015\\Projects\\PT-kinect\\FB Kinect Painter\\bin\\Debug\\" + uri);
                bimg.EndInit();
                Image img = new Image();
                img.Source = bimg;

                main.Children.Add(fb);
                c++;
                if (c > 3) {
                    c = 0;
                    r++;
                }
                if (r > 3) {
                    return;
                }
            }
        }

        private void OnClickFileButton(object sender, RoutedEventArgs ea) {
            Close();
        }
    }

    
}
