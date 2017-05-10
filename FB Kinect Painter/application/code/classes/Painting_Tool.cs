using System;
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
            this.INK.DefaultDrawingAttributes.Width = this.Size;
            this.INK.DefaultDrawingAttributes.Height = this.Size;
        }

        public void SetSize(int value) {
            this.Size = value;
            SetActive();
        }

        public String GetName() {
            return name;
        }
                
        public virtual void Paint(double x, double y) {
            FB_Kinect.SetMousePosition(x, y, true);
            
        }

       
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
    }
}
