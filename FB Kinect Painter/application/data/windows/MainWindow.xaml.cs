
/*************************************************************************************/
/*                                  OUR REFERENCES                                   */
/*************************************************************************************/
using FB_Kinect_Painter.application.data.classes;
using FB_Kinect_Painter.application.code.windows;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
/*************************************************************************************/
/*                                     DEFAULT                                       */
/*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using FB_Kinect_Painter.application.code.classes;
using FB_Kinect_Painter.application.windows;

namespace FB_Kinect_Painter {
    public partial class MainWindow : Window {
        /**********************************************************************************************************************/
        private static double[] headerSize      = { 1.00 * FB_Application.screenWidth,
                                                    0.08 * FB_Application.screenHeight };

        private static double[] footerSize      = { 1.00 * FB_Application.screenWidth,
                                                    0.02 * FB_Application.screenHeight };

        private static double[] leftSideSize    = { 0.18 * FB_Application.screenWidth,
                                                    FB_Application.screenHeight - headerSize[1] - footerSize[1] };

        private static double[] rightSideSize   = { 0.09 * FB_Application.screenWidth,
                                                    FB_Application.screenHeight - headerSize[1] - footerSize[1] };

        private static double[] centerSize      = { FB_Application.screenWidth - leftSideSize[0] - rightSideSize[0],
                                                    FB_Application.screenHeight - headerSize[1] - footerSize[1] };

        private static double[] leftBarSize     = { leftSideSize[0],
                                                    0.50 * FB_Application.screenHeight };

        private static double[] rightBarSize    = { rightSideSize[0],
                                                    0.72 * FB_Application.screenHeight };

        private static double[] cameraViewSize = { rightSideSize[0],
                                                   0.18 * FB_Application.screenHeight };

        private static double[] toolOptionSize  = { leftSideSize[0],
                                                    0.32 * FB_Application.screenHeight };

        private static double[] activeToolsSize = { leftSideSize[0],
                                                    0.18 * FB_Application.screenHeight };

        private static double[] workSpaceSize   = { centerSize[0],
                                                    0.72 * FB_Application.screenHeight };

        private static double[] palleteColorsSize = { centerSize[0],
                                                      centerSize[1] - workSpaceSize[1] };

        // Buttons
        private static double leftButtonSize    = rightSideSize[0] * 0.90;
        private static double palleteButtonSize = palleteColorsSize[1] * 0.50;
        private static double rightButtonSize = rightSideSize[0] * 0.90;
        private static double buttonTextSize = leftButtonSize * 0.20;
        private static double buttonImageSize = leftButtonSize * 0.70;

        // Text Sizes
        private static double leftBarTextSize   = (leftSideSize[0] / 3) * 0.50;
        private static double toolOptionTextSize = (toolOptionSize[1] / 3) * 0.20;
        private static double headerTextSize = headerSize[1];
        private static double footerTextSize = footerSize[1] * 0.40;

        public WorkSheet workSheet;
        /**********************************************************************************************************************/
        private void VisualMainWindow() {
            this.header.Width = headerSize[0];
            this.header.Height = headerSize[1];
            CustomizeHeader();

            this.footer.Width = footerSize[0];
            this.footer.Height = footerSize[1];
            CustomizeFooter();

            this.leftSide.Width = leftSideSize[0];
            this.leftSide.Height = leftSideSize[1];
            this.leftSide.Margin = new Thickness(0, headerSize[1], 0, footerSize[1]);

            this.leftBar.Width = leftBarSize[0];
            this.leftBar.Height = leftBarSize[1];
            CustomizeLeftBar();
            
            this.toolOption.Width = toolOptionSize[0];
            this.toolOption.Height = toolOptionSize[1];
            this.toolOption.Margin = new Thickness(0, leftBarSize[1], 0, activeToolsSize[1]);
            CustomizeToolOption();

            this.activeTools.Width = activeToolsSize[0];
            this.activeTools.Height = activeToolsSize[1];
            CustomizeActiveTools();

            this.rightSide.Width = rightSideSize[0];
            this.rightSide.Height = rightSideSize[1];
            this.rightSide.Margin = new Thickness(0, headerSize[1], 0, footerSize[1]);

            this.rightBar.Width = rightBarSize[0];
            this.rightBar.Height = rightBarSize[1];
            CustomizeRightBar();

            this.cameraView.Width = cameraViewSize[0];
            this.cameraView.Height = cameraViewSize[1];
            CustomizeCameraView();

            this.center.Width = centerSize[0];
            this.center.Height = centerSize[1];
            this.center.Margin = new Thickness(leftSideSize[0], headerSize[1], rightSideSize[0], footerSize[1]);

            this.workSpace.Width = workSpaceSize[0];
            this.workSpace.Height = workSpaceSize[1];
            this.INK.Width = workSpaceSize[0] - 2 * (0.02 * FB_Application.screenHeight);
            this.INK.Height = workSpaceSize[1] - 0.02 * FB_Application.screenHeight;
            this.INK.Margin = new Thickness(0.02 * FB_Application.screenHeight, 0.02 * FB_Application.screenHeight, 
                                                  0.02 * FB_Application.screenHeight, 0);

            this.INK.UseCustomCursor = true;

            this.palleteColors.Width = palleteColorsSize[0];
            this.palleteColors.Height = palleteColorsSize[1];
            CustomizePalleteColors();
        }

        private void CustomizeHeader() {
            this.headerText.FontSize = headerTextSize;
            this.headerText.Foreground = FB_Application.appFontColor;
        }

        private void CustomizeFooter() {
            this.footerText.FontSize = footerTextSize;
            this.footerText.Foreground = FB_Application.appFontColor;
        }

        private void CustomizeLeftBar() {
            KinectTileButton[] leftBarButtons = { this.sprayButton, this.pencilButton, this.eraserButton, this.brushButton, this.selectButton };
            Image[] leftBarButtonsImage = { this.sprayButtonIcon, this.pencilButtonIcon, this.eraserButtonIcon, this.brushButtonIcon, this.selectButtonIcon };
            TextBlock[] leftBarButtonsText = { this.sprayButtonText, this.pencilButtonText, this.eraserButtonText, this.brushButtonText, this.selectButtonText };

            this.leftBarText.FontSize = leftBarTextSize;
            this.leftBarText.Foreground = FB_Application.appFontColor;

            for (int i = 0; i < this.leftBar.RowDefinitions.Count; i++) {
                if(i == 0) 
                    this.leftBar.RowDefinitions[i].Height = new GridLength(leftBarTextSize * 1.25);
                else
                    this.leftBar.RowDefinitions[i].Height = new GridLength(leftButtonSize);
            }

            for (int i = 0; i < leftBarButtons.Length; i++) {
                leftBarButtons[i].Width = leftButtonSize;
                leftBarButtons[i].Height = leftButtonSize;
                leftBarButtons[i].Background = FB_Application.appButtonColor;
                leftBarButtonsImage[i].Height = buttonImageSize;
                leftBarButtonsImage[i].Width = buttonImageSize;
                leftBarButtonsText[i].FontSize = buttonTextSize;
            }
        }

        private void CustomizeToolOption() {
            KinectTileButton[] toolOptionButtons = { this.moreThicknessButton, this.lessThicknessButton };

            this.toolOptionText.FontSize = toolOptionTextSize;
            this.toolOptionText.Foreground = FB_Application.appFontColor;
            this.thicknessValue.FontSize = toolOptionTextSize * 2.5;
            this.thicknessValue.Foreground = FB_Application.appFontColor;

            for (int i = 0; i < this.toolOption.RowDefinitions.Count; i++) {

                if (i < 2) {
                    this.toolOption.RowDefinitions[i].Height = new GridLength((toolOptionSize[1] / this.toolOption.RowDefinitions.Count) * 0.50);
                } else {
                    double x = 2 * (toolOptionSize[1] / this.toolOption.RowDefinitions.Count) * 0.50;
                    this.toolOption.RowDefinitions[i].Height = new GridLength((toolOptionSize[1] - x) / 2);
                }          
            }

            for(int i = 0; i < toolOptionButtons.Length; i++) {
                toolOptionButtons[i].Width = palleteButtonSize * 0.75;
                toolOptionButtons[i].Height = palleteButtonSize * 0.75;
                toolOptionButtons[i].Background = FB_Application.appButtonColor;
            }
        }

        private void CustomizeActiveTools() {
            this.activeToolsText.FontSize = toolOptionTextSize;
            this.activeToolsText.Foreground = FB_Application.appFontColor;

            this.activeTool.Width = palleteButtonSize * 0.75;
            this.activeTool.Height = palleteButtonSize * 0.75;

            this.activeColor.Width = palleteButtonSize * 0.75;
            this.activeColor.Height = palleteButtonSize * 0.75;

            for (int i = 0; i < this.activeTools.RowDefinitions.Count; i++) {
                if(i < 1) {
                    this.activeTools.RowDefinitions[i].Height = new GridLength((toolOptionSize[1] / this.toolOption.RowDefinitions.Count) * 0.50);
                } else {
                    double x = (toolOptionSize[1] / this.toolOption.RowDefinitions.Count) * 0.50;
                    this.activeTools.RowDefinitions[i].Height = new GridLength((activeToolsSize[1] - x) / 2);
                }
            }
        }

        private void CustomizePalleteColors() {
            KinectTileButton[] palleteColorsButtons = { this.blackColor, this.whiteColor, this.grayColor, this.darkGrayColor, this.darkRedColor,
                                                        this.brownColor, this.redColor, this.pinkColor, this.redOrangeColor, this.orangeColor,
                                                        this.yellowColor, this.lightYellowColor, this.greenColor, this.lightGreenColor, this.blueColor,
                                                        this.lightBlueColor, this.darkBlueColor, this.cadetBlueColor, this.blueVioletColor, this.violetColor,
                                                        this.yourColor1, this.yourColor2 };

            for(int i = 0; i < palleteColorsButtons.Length; i++) {
                palleteColorsButtons[i].Width = palleteButtonSize;
                palleteColorsButtons[i].Height = palleteButtonSize;
            }

        }

        private void CustomizeRightBar() {
            KinectTileButton[] rightBarButtons = { this.newButton, this.saveButton, this.loadButton, this.quitButton };
            Image[] rightBarButtonsImage = { this.newButtonIcon, this.saveButtonIcon, this.loadButtonIcon, this.quitButtonIcon };
            TextBlock[] rightBarButtonsText = { this.newButtonText, this.saveButtonText, this.loadButtonText, this.quitButtonText };

            this.rightBarText.FontSize = leftBarTextSize;
            this.rightBarText.Foreground = FB_Application.appFontColor;

            for (int i = 0; i < this.rightBar.RowDefinitions.Count; i++) {
                if (i == 0)
                    this.rightBar.RowDefinitions[i].Height = new GridLength(leftBarTextSize * 1.25);
                else
                    this.rightBar.RowDefinitions[i].Height = new GridLength(rightButtonSize);
            }

            for (int i = 0; i < rightBarButtons.Length; i++) {
                rightBarButtons[i].Width = rightButtonSize;
                rightBarButtons[i].Height = rightButtonSize;
                rightBarButtons[i].Background = FB_Application.appButtonColor;
                rightBarButtonsImage[i].Width = buttonImageSize;
                rightBarButtonsImage[i].Height = buttonImageSize;
                rightBarButtonsText[i].FontSize = buttonTextSize;
            }
        }

        private void CustomizeCameraView() {
            this.cameraViewText.FontSize = toolOptionTextSize;
            this.cameraViewText.Foreground = FB_Application.appFontColor;
        }

        /*****************************************************************************/
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs) {
            FB_Kinect.sensorChooser = new KinectSensorChooser();
            FB_Kinect.sensorChooser.KinectChanged += FB_Kinect.SensorChooserOnKinectChanged;
            FB_Application.mw.sensorChooserUi.KinectSensorChooser = FB_Kinect.sensorChooser;
            FB_Kinect.sensorChooser.Start();
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
        private void OnClickEraserButton(object sender, RoutedEventArgs routedEventArgs) {
            workSheet.SetPaintingTool("Eraser");
            thicknessValue.Text = workSheet.activePaintingTool.GetSize().ToString();
        }
        /*****************************************************************************/
        private void OnClickPencilButton(object sender, RoutedEventArgs routedEventArgs) {
            workSheet.SetPaintingTool("Pencil");
            thicknessValue.Text = workSheet.activePaintingTool.GetSize().ToString();
        }
        /*****************************************************************************/
        private void OnClickSelectButton(object sender, RoutedEventArgs routedEventArgs) {
            workSheet.SetPaintingTool("Select");
            thicknessValue.Text = workSheet.activePaintingTool.GetSize().ToString();
        }
        /*****************************************************************************/
        private void OnClickBrushButton(object sender, RoutedEventArgs routedEventArgs) {
            workSheet.SetPaintingTool("Brush");
            thicknessValue.Text = workSheet.activePaintingTool.GetSize().ToString();
        }
        /*****************************************************************************/
        private void OnClickSprayButton(object sender, RoutedEventArgs routedEventArgs) {
            workSheet.SetPaintingTool("Spray");
            thicknessValue.Text = workSheet.activePaintingTool.GetSize().ToString();
        }
        /*****************************************************************************/
        private void changeColor(object sender, RoutedEventArgs routedEventArgs) {
            String color = ((KinectTileButton)sender).Background.ToString();
            workSheet.SetToolColor(color);
            activeColor.Background = ((KinectTileButton)sender).Background;
        }
        /*****************************************************************************/
        private void OnClickLessThicknessButton(object sender, RoutedEventArgs routedEventArgs) {
            thicknessValue.Text = workSheet.activePaintingTool.DecrementSize().ToString();
        }
        /*****************************************************************************/
        private void OnClickMoreThicknessButton(object sender, RoutedEventArgs routedEventArgs) {
            thicknessValue.Text = workSheet.activePaintingTool.IncrementSize().ToString();
        }
        /*****************************************************************************/
        public MainWindow() {
            InitializeComponent();
            VisualMainWindow();
            Loaded += OnLoaded;
            workSheet = new WorkSheet(INK);
            Settings.LoadSettings();
            User.SetJoints(Settings.primaryHeand);
            FB_Application.mw = this;
        }
    }
}
