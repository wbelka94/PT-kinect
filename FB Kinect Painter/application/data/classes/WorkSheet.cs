﻿using FB_Kinect_Painter.application.data.classes;
using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FB_Kinect_Painter.application.data.windows;
using FB_Kinect_Painter;

namespace FB_Kinect_Painter.application.code.classes {
    public class WorkSheet {
        private Painting_Tool[] tools;
        private string fileName;
        public Painting_Tool activePaintingTool;
        private InkCanvas INK;
        private String path;
        private Color color;
        private StrokeCollection urStrokes;

        public WorkSheet(InkCanvas INK) {
            this.INK = INK;
            path = "C:\\Users\\Florek\\Source\\Repos\\s\\PT-kinect\\FB Kinect Painter\\bin\\Debug\\saved_pictures";
            fileName = "";
            urStrokes = new StrokeCollection();
            /*************************************init painting tools***********************************/
            tools = new Painting_Tool[5]
                { new Painting_Tool(INK, InkCanvasEditingMode.Ink, "Brush", "BrushCursor.cur", 6),
                    new Painting_Tool(INK, InkCanvasEditingMode.InkAndGesture, "Pencil", "PencilCursor.cur", 1),
                    new Painting_Tool(INK, InkCanvasEditingMode.EraseByPoint, "Eraser", "EraserCursor.cur", 6),
                    new Spray(INK, InkCanvasEditingMode.Ink, "Spray", "SprayCursor.cur", 20),
                    new Painting_Tool(INK, InkCanvasEditingMode.Select, "Select", "HandCursor.cur", 20),
                };

            //default pencil
            activePaintingTool = tools[1];
            activePaintingTool.SetActive();

        }

        public void Save() {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)INK.ActualWidth, (int)INK.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);

            // needed otherwise the image output is black
            INK.Measure(new Size((int)INK.ActualWidth, (int)INK.ActualHeight));
            INK.Arrange(new Rect(new Size((int)INK.ActualWidth, (int)INK.ActualHeight)));

            renderBitmap.Render(INK);

            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            /******************************************* ustalenie nazwy pliku *************************/
            if (fileName.Equals("")) {
                fileName = path + "/FB_Kinect_Painter.bmp";
                int i = 1;
                while (File.Exists(fileName)) {
                    fileName = path + "/FB_Kinect_Painter_" + i + ".bmp";
                    i++;
                }
            }

            using (FileStream file = File.Create(fileName)) {
                encoder.Save(file);
                file.Close();
            }
            using (FileStream file = File.Create(fileName + ".fbkp")) {
                INK.Strokes.Save(file);
                file.Close();
            }

            //AutoClosingMessageBox.Show("Zapisano do pliku: " + fileName, "Caption", 2000);
            SaveWindow sw = new SaveWindow();
        }

        public void ReadFormFile(object Sender, RoutedEventArgs routedEventArgs) {
            fileName = ((KinectTileButton)Sender).Tag.ToString();
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StrokeCollection strokes = new StrokeCollection(fs);
            fs.Close();
            INK.Strokes = strokes;
            fileName = fileName.Substring(0, fileName.Length - 5);
        }

        public void Clear(object Sender, RoutedEventArgs routedEventArgs) {
            INK.Strokes.Clear();
        }

        public void New(object Sender, RoutedEventArgs routedEventArgs) {
            INK.Strokes.Clear();
            fileName = "";
        }

        public void SetPaintingTool(String toolName) {
            foreach (Painting_Tool tool in tools) {
                if (tool.GetName() == toolName) {
                    activePaintingTool = tool;
                    activePaintingTool.SetActive();
                }
            }
        }

        public void SetToolColor(String c) {
            color = (Color)ColorConverter.ConvertFromString(c);
            INK.DefaultDrawingAttributes.Color = color;
        }

        public void setPath(String path) {
            this.path = path;
        }

        public void undoMove() {
            if (INK.Strokes.Count > 0) {
                urStrokes.Add(INK.Strokes.Last());
                INK.Strokes.Remove(INK.Strokes.Last());
            }
        }

        public void redoMove() {
            if (urStrokes.Count > 0) {
                INK.Strokes.Add(urStrokes.Last());
                urStrokes.Remove(urStrokes.Last());
            }
        }

    }
}
