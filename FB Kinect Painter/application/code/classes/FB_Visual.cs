﻿/*************************************************************************************/
/*                                     DEFAULT                                       */
/*************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
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
/*************************************************************************************/
namespace FB_Kinect_Painter.application.code.classes {
    /*********************************************************************************/
    class FB_Visual {
        /*****************************************************************************/
        public static double proportionHeight;
        public static double proportionWidth;
        /*****************************************************************************/
        public static void CreateSaveBitmap(InkCanvas canvas, String path) {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
             (int)canvas.ActualWidth, (int)canvas.ActualHeight,
             96d, 96d, PixelFormats.Pbgra32);
            // needed otherwise the image output is black
            canvas.Measure(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight));
            canvas.Arrange(new Rect(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight)));

            renderBitmap.Render(canvas);

            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            String filename = path + "/FB_Kinect_Painter.bmp";            
            int i = 1;            
            while (File.Exists(filename)){
                filename = path + "/FB_Kinect_Painter_" + i + ".bmp";
                i++;
            }           

            using (FileStream file = File.Create(filename)) {
                encoder.Save(file);
            }
            AutoClosingMessageBox.Show("Zapisano do pliku: " + filename, "Caption", 2000);
        }
        /*****************************************************************************/
        private static int GetScreenHeight() {
            return (int)System.Windows.SystemParameters.PrimaryScreenHeight; 
        }
        /*****************************************************************************/
        private static int GetScreenWidth() {
            return (int)System.Windows.SystemParameters.PrimaryScreenWidth;
        }
        /*****************************************************************************/
        public static void VisualMainWindow(MainWindow mw) {
            int width = GetScreenWidth();
            int height = GetScreenHeight();

            FB_Visual.proportionHeight = height / 480;
            FB_Visual.proportionWidth = width / 640;

            (mw as MainWindow).cameraView.Width = 0.15 * width;
            (mw as MainWindow).cameraView.Height = 0.2 * height;
            MainWindowCameraView(mw);

            (mw as MainWindow).leftBar.Width = 0.15 * width;
            (mw as MainWindow).leftBar.Height = 0.9 * height;
            (mw as MainWindow).leftBar.Margin = new Thickness(0, 0.1 * height, 0, 0);
            

            (mw as MainWindow).rightBar.Width = 0.15 * width;
            (mw as MainWindow).rightBar.Height = (0.9 * height) - (mw as MainWindow).cameraView.Height;
            (mw as MainWindow).rightBar.Margin = new Thickness(0, 0.1 * height, 0, 0);
            MainWindowRightBar(mw);
            MainWindowLeftBar(mw);

            (mw as MainWindow).adSpace.Width = 1 * width - (mw as MainWindow).rightBar.Width - (mw as MainWindow).leftBar.Width;
            (mw as MainWindow).adSpace.Height = 0.1 * height;
            MainWindowAdSpace(mw); 

            (mw as MainWindow).palleteColors.Width = 0.7 * width;
            (mw as MainWindow).palleteColors.Height = 0.2 * height;
            (mw as MainWindow).palleteColors.Margin = new Thickness(0, 0, 0, 0);
            MainWindowPalleteColors(mw);

            (mw as MainWindow).workSpace.Width = 0.7 * width;
            (mw as MainWindow).workSpace.Height = 0.7 * height;
            (mw as MainWindow).workSpace.Margin = new Thickness((mw as MainWindow).leftBar.Width, (mw as MainWindow).adSpace.Height, 
                                                                (mw as MainWindow).rightBar.Width, (mw as MainWindow).palleteColors.Height);
        }
        /*****************************************************************************/
        private static void MainWindowLeftBar(MainWindow mw) {
            /*double width = (int)(mw as MainWindow).leftBar.Width * 0.7;                // chcemy kwadratowe przyciski ;)
            double height = (int)(mw as MainWindow).leftBar.Width * 0.7;*/
            double width = (int)(mw as MainWindow).rightBar.Height * 0.25;
            double height = (int)(mw as MainWindow).rightBar.Height * 0.25;

            (mw as MainWindow).leftBarRow0.Height = new System.Windows.GridLength(height);
            (mw as MainWindow).leftBarRow1.Height = new System.Windows.GridLength(height);
            (mw as MainWindow).leftBarRow2.Height = new System.Windows.GridLength(height);
            (mw as MainWindow).leftBarRow3.Height = new System.Windows.GridLength(height);
            (mw as MainWindow).leftBarRow4.Height = new System.Windows.GridLength(height);

            (mw as MainWindow).sprayButton.Width = width;
            (mw as MainWindow).sprayButton.Height = height;

            (mw as MainWindow).pencilButton.Width = width;
            (mw as MainWindow).pencilButton.Height = height;

            (mw as MainWindow).eraserButton.Width = width;
            (mw as MainWindow).eraserButton.Height = height;

            (mw as MainWindow).brushButton.Width = width;
            (mw as MainWindow).brushButton.Height = height;

            (mw as MainWindow).selectButton.Width = width;
            (mw as MainWindow).selectButton.Height = height;

        }
        /*****************************************************************************/
        private static void MainWindowRightBar(MainWindow mw) {
            double width = (int)(mw as MainWindow).rightBar.Height * 0.25;
            double height = (int)(mw as MainWindow).rightBar.Height * 0.25;

            (mw as MainWindow).rightBarRow0.Height = new System.Windows.GridLength(height);
            (mw as MainWindow).rightBarRow1.Height = new System.Windows.GridLength(height);
            (mw as MainWindow).rightBarRow2.Height = new System.Windows.GridLength(height);
            (mw as MainWindow).rightBarRow3.Height = new System.Windows.GridLength(height);

            (mw as MainWindow).quitButton.Width = width;        
            (mw as MainWindow).quitButton.Height = height;

            (mw as MainWindow).loadButton.Width = width;        
            (mw as MainWindow).loadButton.Height = height;

            (mw as MainWindow).saveButton.Width = width;        
            (mw as MainWindow).saveButton.Height = height;

            (mw as MainWindow).newButton.Width = width;
            (mw as MainWindow).newButton.Height = height;
        }
        /*****************************************************************************/
        private static void MainWindowCameraView(MainWindow mw) {
            (mw as MainWindow).kinectUserViever.Height = (mw as MainWindow).cameraView.Height;
        }
        /*****************************************************************************/
        private static void MainWindowAdSpace(MainWindow mw) {
            //mw.wbSample.Navigate("http://iamwojtas.pl");
            mw.wbSample.NavigateToString("<H1>Reklama</H1>");
        }
        /*****************************************************************************/
        private static void MainWindowPalleteColors(MainWindow mw) {
            (mw as MainWindow).palleteColorsRow0.Height = new System.Windows.GridLength((mw as MainWindow).palleteColors.Height / 2);
            (mw as MainWindow).palleteColorsRow1.Height = new System.Windows.GridLength((mw as MainWindow).palleteColors.Height / 2);

        /*    (mw as MainWindow).palleteColorsColumn0.Width = new System.Windows.GridLength((mw as MainWindow).palleteColors.Width / 11); // bo 11 kolumn :p
            (mw as MainWindow).palleteColorsColumn1.Width = new System.Windows.GridLength((mw as MainWindow).palleteColors.Width / 11);
            (mw as MainWindow).palleteColorsColumn2.Width = new System.Windows.GridLength((mw as MainWindow).palleteColors.Width / 11);
            (mw as MainWindow).palleteColorsColumn3.Width = new System.Windows.GridLength((mw as MainWindow).palleteColors.Width / 11);
            (mw as MainWindow).palleteColorsColumn4.Width = new System.Windows.GridLength((mw as MainWindow).palleteColors.Width / 11);
            (mw as MainWindow).palleteColorsColumn5.Width = new System.Windows.GridLength((mw as MainWindow).palleteColors.Width / 11);
            (mw as MainWindow).palleteColorsColumn6.Width = new System.Windows.GridLength((mw as MainWindow).palleteColors.Width / 11);
            (mw as MainWindow).palleteColorsColumn7.Width = new System.Windows.GridLength((mw as MainWindow).palleteColors.Width / 11);
            (mw as MainWindow).palleteColorsColumn8.Width = new System.Windows.GridLength((mw as MainWindow).palleteColors.Width / 11);
            (mw as MainWindow).palleteColorsColumn9.Width = new System.Windows.GridLength((mw as MainWindow).palleteColors.Width / 11);
            (mw as MainWindow).palleteColorsColumn10.Width = new System.Windows.GridLength((mw as MainWindow).palleteColors.Width / 11);*/

            (mw as MainWindow).blackColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).blackColor.Height = (mw as MainWindow).blackColor.Width;

            (mw as MainWindow).whiteColor.Width = (mw as MainWindow).palleteColors.Height / 2; ;
            (mw as MainWindow).whiteColor.Height = (mw as MainWindow).whiteColor.Width;

            (mw as MainWindow).grayColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).grayColor.Height = (mw as MainWindow).grayColor.Width;

            (mw as MainWindow).darkGrayColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).darkGrayColor.Height = (mw as MainWindow).darkGrayColor.Width;

            (mw as MainWindow).darkRedColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).darkRedColor.Height = (mw as MainWindow).darkRedColor.Width;

            (mw as MainWindow).brownColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).brownColor.Height = (mw as MainWindow).brownColor.Width;

            (mw as MainWindow).redColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).redColor.Height = (mw as MainWindow).redColor.Width;

            (mw as MainWindow).redColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).redColor.Height = (mw as MainWindow).redColor.Width;

            (mw as MainWindow).pinkColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).pinkColor.Height = (mw as MainWindow).pinkColor.Width;

            (mw as MainWindow).redOrangeColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).redOrangeColor.Height = (mw as MainWindow).redOrangeColor.Width;

            (mw as MainWindow).orangeColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).orangeColor.Height = (mw as MainWindow).orangeColor.Width;

            (mw as MainWindow).yellowColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).yellowColor.Height = (mw as MainWindow).yellowColor.Width;

            (mw as MainWindow).lightYellowColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).lightYellowColor.Height = (mw as MainWindow).lightYellowColor.Width;

            (mw as MainWindow).greenColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).greenColor.Height = (mw as MainWindow).greenColor.Width;

            (mw as MainWindow).lightGreenColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).lightGreenColor.Height = (mw as MainWindow).lightGreenColor.Width;

            (mw as MainWindow).blueColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).blueColor.Height = (mw as MainWindow).blueColor.Width;

            (mw as MainWindow).lightBlueColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).lightBlueColor.Height = (mw as MainWindow).lightBlueColor.Width;

            (mw as MainWindow).darkBlueColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).darkBlueColor.Height = (mw as MainWindow).darkBlueColor.Width;

            (mw as MainWindow).cadetBlueColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).cadetBlueColor.Height = (mw as MainWindow).cadetBlueColor.Width;

            (mw as MainWindow).blueVioletColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).blueVioletColor.Height = (mw as MainWindow).blueVioletColor.Width;

            (mw as MainWindow).violetColor.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).violetColor.Height = (mw as MainWindow).violetColor.Width;

            (mw as MainWindow).yourColor1.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).yourColor1.Height = (mw as MainWindow).yourColor1.Width;

            (mw as MainWindow).yourColor2.Width = (mw as MainWindow).palleteColors.Height / 2;
            (mw as MainWindow).yourColor2.Height = (mw as MainWindow).yourColor2.Width;
        }
        /*****************************************************************************/
    }
    /*********************************************************************************/
}
/*************************************************************************************/
