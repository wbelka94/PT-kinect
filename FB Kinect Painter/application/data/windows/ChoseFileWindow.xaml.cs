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
using FB_Kinect_Painter.application.data.classes;
using FB_Kinect_Painter.application.code.windows;

namespace FB_Kinect_Painter.application.code.windows {    

    public partial class ChoseFileWindow : Window {
        /**********************************************************************************************************************/
        private static double[] thisWindowSize = { 0.50 * FB_Application.screenWidth,
                                                   0.60 * FB_Application.screenHeight };

        private static double[] headerSize = { 1.00 * thisWindowSize[0],
                                               0.20 * thisWindowSize[1] };

        private static double[] contentSize = { 1.00 * thisWindowSize[0],
                                                thisWindowSize[1] - headerSize[1] };

        private static double[] contentFileButtonSize = { 0.20 * thisWindowSize[0],
                                                          0.25 * thisWindowSize[1] };

        /**********************************************************************************************************************/
        string[] files;
        RoutedEventHandler eh;
        public ChoseFileWindow(/*string[] files,*/ RoutedEventHandler eh) {
            InitializeComponent();
            this.eh = eh;
            try {
                this.files = Directory.GetFiles("saved_pictures", "*.fbkp");
            } catch (Exception e) {
                //title.Content = e.ToString();
            }
            VisualChooseFileWindow();
            DisplayFiles();
            this.Show();
        }

        private void VisualChooseFileWindow() {
            this.Width = thisWindowSize[0];
            this.Height = thisWindowSize[1];

            this.header.Width = headerSize[0];
            this.header.Height = headerSize[1];
            this.headerText.FontSize = (headerSize[0] / headerSize[1]) * 10;

            this.content.Width = contentSize[0];
            this.content.Height = contentSize[1];

            ScrollViewer sv = new ScrollViewer();
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            sv.IsEnabled = true;
            Grid.SetColumnSpan(sv, 4);
            Grid.SetRowSpan(sv, 2);
            this.content.Children.Add(sv);

            for (int i = 0; i < 25; i++) {
                var rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(this.content.Height * 0.50);
                this.content.RowDefinitions.Add(rowDefinition);
            }

            



        }

        private void DisplayFiles() {
            int c = 0, r = 0;
            foreach(string file in files) {
                KinectTileButton fb = new KinectTileButton();
                fb.Content = file;
                fb.Width = contentFileButtonSize[0];
                fb.Height = contentFileButtonSize[1];
                fb.Click += eh;
                fb.Click += OnClickFileButton;
                Grid.SetRow(fb, r);
                Grid.SetColumn(fb, c);

                //miniaturki
               /* BitmapImage bimg = new BitmapImage();
                bimg.BeginInit();
                string uri = file.Substring(0, file.Length - 5);
                bimg.UriSource = new Uri("C:\\Users\\Wojtek\\Documents\\Visual Studio 2015\\Projects\\PT-kinect\\FB Kinect Painter\\bin\\Debug\\" + uri);
                bimg.EndInit();
                Image img = new Image();
                img.Source = bimg;*/

                content.Children.Add(fb);
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
