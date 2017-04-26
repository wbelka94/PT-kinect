﻿/*************************************************************************************/
/*                                  OUR REFERENCES                                   */
/*************************************************************************************/
using FB_Kinect_Painter.application.code.classes;
using FB_Kinect_Painter.application.code.windows;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
using Microsoft.Kinect.Toolkit.Interaction;
/*************************************************************************************/
/*                                     DEFAULT                                       */
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
using System.Windows.Ink;
using System.Windows.Controls;
using System.IO;

/*************************************************************************************/
namespace FB_Kinect_Painter {
    public partial class MainWindow : Window {
        /*****************************************************************************/
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs) {
            FB_Kinect.sensorChooser = new KinectSensorChooser();
            FB_Kinect.sensorChooser.KinectChanged += FB_Kinect.SensorChooserOnKinectChanged;
            (FB_Kinect.mw as MainWindow).sensorChooserUi.KinectSensorChooser = FB_Kinect.sensorChooser;
            FB_Kinect.sensorChooser.Start();
        }
        /*****************************************************************************/
        private void wb_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e) {
            mshtml.IHTMLDocument2 dom = (mshtml.IHTMLDocument2)wbSample.Document;
            dom.body.style.overflow = "hidden";
        }
        /*****************************************************************************/
        private void OnClickQuitButton(object sender, RoutedEventArgs routedEventArgs) {
            if (FB_Kinect.ew == null) {
                FB_Kinect.ew = new ExitWindow();
            }
            FB_Kinect.ew.Show();
        }
        /*****************************************************************************/
        private void OnClickLoadButton(object sender, RoutedEventArgs routedEventArgs) {
            var fs = new FileStream("C:/Users/Wojtek/Desktop/FB_Kinect_Painter/FB_Kinect_Painter_13.bmp",
                FileMode.Open, FileAccess.Read);
            StrokeCollection strokes = new StrokeCollection(fs);
        }
        /*****************************************************************************/
        private void OnClickSaveButton(object sender, RoutedEventArgs routedEventArgs) {            
            FB_Visual.CreateSaveBitmap(INK, "C:/Users/Wojtek/Desktop/FB_Kinect_Painter");
        }
        /*****************************************************************************/
        private void OnClickNewButton(object sender, RoutedEventArgs routedEventArgs) {
            INK.Strokes.Clear();            
        }
        /*****************************************************************************/
        private void OnClickEraserButton(object sender, RoutedEventArgs routedEventArgs)
        {           
            INK.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }
        /*****************************************************************************/
        private void OnClickPencilButton(object sender, RoutedEventArgs routedEventArgs)
        {
            INK.EditingMode = InkCanvasEditingMode.Ink;
            INK.DefaultDrawingAttributes.Width = 1;
            INK.DefaultDrawingAttributes.Height = 1;
        }
        /*****************************************************************************/
        private void OnClickSelectButton(object sender, RoutedEventArgs routedEventArgs)
        {
            INK.EditingMode = InkCanvasEditingMode.Select;
        }        
        /*****************************************************************************/
        private void OnClickBrushButton(object sender, RoutedEventArgs routedEventArgs)
        {
            INK.EditingMode = InkCanvasEditingMode.Ink;
            INK.DefaultDrawingAttributes.Width = 6;
        }

        /*****************************************************************************/
        private void changeColor(object sender, RoutedEventArgs routedEventArgs)
        {
            String color = ((KinectTileButton)sender).Background.ToString();
            INK.DefaultDrawingAttributes.Color = (Color)ColorConverter.ConvertFromString(color);
        }
        /*****************************************************************************/

        public MainWindow() {
            InitializeComponent();
            FB_Visual.VisualMainWindow(this);
            Loaded += OnLoaded;
        }

        private void blackColor_Click(object sender, RoutedEventArgs e)
        {

        }
        /*****************************************************************************/
    }
}
/*************************************************************************************/
