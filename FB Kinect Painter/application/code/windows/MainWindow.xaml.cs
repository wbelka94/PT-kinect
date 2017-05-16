/*************************************************************************************/
/*                                  OUR REFERENCES                                   */
/*************************************************************************************/
using FB_Kinect_Painter.application.code.classes;
using FB_Kinect_Painter.application.code.windows;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
/*************************************************************************************/
/*                                     DEFAULT                                       */
/*************************************************************************************/
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Ink;
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
            Window w = new ChoseFileWindow(workSheet.ReadFormFile);
            /*var fs = new FileStream("saved_pictures/FB_Kinect_Painter_6.bmp.fbkp", FileMode.Open, FileAccess.Read);
            StrokeCollection strokes = new StrokeCollection(fs);
            INK.Strokes = strokes;*/
        }
        /*****************************************************************************/
        private void OnClickSaveButton(object sender, RoutedEventArgs routedEventArgs) {
            workSheet.Save();
        }
        /*****************************************************************************/
        private void OnClickNewButton(object sender, RoutedEventArgs routedEventArgs) {
            DecisionWindow dw = new DecisionWindow("Jesteś pewien?", workSheet.New);          
             //workSheet.Clear();            
        }
        /*****************************************************************************/
        private void OnClickEraserButton(object sender, RoutedEventArgs routedEventArgs)
        {
            workSheet.SetPaintingTool("Eraser");
        }
        /*****************************************************************************/
        private void OnClickPencilButton(object sender, RoutedEventArgs routedEventArgs)
        {
            workSheet.SetPaintingTool("Pencil");
        }
        /*****************************************************************************/
        private void OnClickSelectButton(object sender, RoutedEventArgs routedEventArgs)
        {
            workSheet.SetPaintingTool("Select");
        }        
        /*****************************************************************************/
        private void OnClickBrushButton(object sender, RoutedEventArgs routedEventArgs)
        {
            workSheet.SetPaintingTool("Brush");
        }
        /*****************************************************************************/
        private void OnClickSprayButton(object sender, RoutedEventArgs routedEventArgs) {
            workSheet.SetPaintingTool("Spray");
        }
        /*****************************************************************************/
        private void changeColor(object sender, RoutedEventArgs routedEventArgs)
        {
            String color = ((KinectTileButton)sender).Background.ToString();
            workSheet.SetToolColor(color);
        }
        /*****************************************************************************/

        public WorkSheet workSheet;

        public MainWindow() {
            InitializeComponent();
            FB_Visual.VisualMainWindow(this);
            Loaded += OnLoaded;
            workSheet = new WorkSheet(INK);
            
        }

        
        /*****************************************************************************/
    }
}
/*************************************************************************************/
