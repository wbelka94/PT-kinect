﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FB_Kinect_Painter.application.code.classes {
    public class Painting_Tool {
        private String name;
        private InkCanvas INK;
        private InkCanvasEditingMode editingMode;
        private int Size;
        public Painting_Tool(InkCanvas INK, InkCanvasEditingMode editingMode, String name, String cursor, int defaultSize) {
            this.name = name;
            this.INK = INK;
            this.editingMode = editingMode;
            this.Size = defaultSize;
            
        }

        public void SetActive() {
            
            this.INK.EditingMode = this.editingMode;
            if (name.Equals("Spray")) {
                this.INK.DefaultDrawingAttributes.Width = 1;
                this.INK.DefaultDrawingAttributes.Height = 1;
            } else {
                this.INK.DefaultDrawingAttributes.Width = this.Size;
                this.INK.DefaultDrawingAttributes.Height = this.Size;
            }
        }

        public void SetSize(int value) {
            this.Size = value;
            SetActive();
        }

        public String GetName() {
            return name;
        }
                
        public virtual void Paint(double x, double y) {
            if (!name.Equals("Spray")) {
                FB_Kinect.SetMousePosition(x, y, true);
            } else {
                Random rnd = new Random();
                double nx, ny;
                for (int i = 0; i < 3; i++) {
                    nx = x + rnd.Next(0, Size);
                    ny = y + rnd.Next(0, Size);
                    FB_Kinect.SetMousePosition(nx, ny, true);
                    FB_Kinect.SetMousePosition(nx+1, ny+1, true);
                    FB_Kinect.SetMousePosition(nx, ny, false);
                }
            }
            
        }

       
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
    }
}