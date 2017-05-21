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
                                               0.30 * thisWindowSize[1] };

        private static double[] contentSize = { 1.00 * thisWindowSize[0],
                                                0.40 * thisWindowSize[1] };

        private static double[] contentFileButtonSize = { 0.45 * thisWindowSize[0],
                                                          0.25 * thisWindowSize[0] };

        private static double[] footerSize = { 1.00 * thisWindowSize[0],
                                               0.30 * thisWindowSize[1] };

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
            this.headerText.FontSize = (headerSize[0] / headerSize[1]) * 15;

            this.contentScroll.Height = contentSize[1];
            this.contentScroll.Width = contentSize[0];

            this.footer.Width = footerSize[0];
            this.footer.Height = footerSize[1];
            this.backButton.Width = contentFileButtonSize[0] * 0.60;
            this.backButton.Height = contentFileButtonSize[1] * 0.60;
        }

        private void DisplayFiles() {
            foreach(string file in files) {
                KinectTileButton fb = new KinectTileButton();
                StackPanel sp = new StackPanel();
                //sp.Width = contentFileButtonSize[1] * 0.90;
                //sp.Height = contentFileButtonSize[0] * 0.90;
                sp.HorizontalAlignment = HorizontalAlignment.Center;
                sp.VerticalAlignment = VerticalAlignment.Center;


                string filebmp = file.Replace(".fbkp", "");

                BitmapImage image = new BitmapImage(new Uri("C:\\Users\\Florek\\Source\\Repos\\s\\PT-kinect\\FB Kinect Painter\\bin\\Debug\\" + filebmp));

                

                fb.Width = contentFileButtonSize[0];
                fb.Height = contentFileButtonSize[1];

                Image img = new Image();
                img.Source = image;
                img.Width = contentFileButtonSize[0] * 0.90;
                img.Height = contentFileButtonSize[1] * 0.90;

                sp.Children.Add(img);

                fb.Content = sp;
                fb.Click += eh;
                fb.Click += OnClickFileButton;
                content.Children.Add(fb);
            }
        }

        private void OnClickFileButton(object sender, RoutedEventArgs ea) {
            this.Close();
        }

        private void OnClickBackButton(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }

    
}
