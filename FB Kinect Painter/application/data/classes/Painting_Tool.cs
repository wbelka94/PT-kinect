using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using FB_Kinect_Painter;
using System.Windows.Media.Imaging;

namespace FB_Kinect_Painter.application.data.classes {
    public class Painting_Tool {
        private String name;
        private InkCanvas INK;
        private InkCanvasEditingMode editingMode;
        private int Size;
        private string cursor;
        public Painting_Tool(InkCanvas INK, InkCanvasEditingMode editingMode, String name, string cursor, int defaultSize) {
            this.name = name;
            this.INK = INK;
            this.editingMode = editingMode;
            this.Size = defaultSize;
            this.cursor = cursor;
        }

        public void SetActive() {
            this.INK.Cursor = new System.Windows.Input.Cursor(System.Windows.Forms.Application.StartupPath + cursor);
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

        public int DecrementSize() {
            if(Size == 1 || name.Equals("Pencil")) {
                return Size;
            }
            Size--;            
            SetActive();
            return Size;
        }

        public int IncrementSize() {
            if (name.Equals("Pencil"))
                return Size;
            Size++;
            SetActive();
            return Size;
        }


        public String GetName() {
            return name;
        }

        public int GetSize() {
            return Size;
        }

        public BitmapImage GetImage() {
            string cursor = this.cursor.Substring(0, this.cursor.Length - 3) + "bmp";
            BitmapImage image = new BitmapImage(new Uri(System.Windows.Forms.Application.StartupPath+cursor));
            return image;
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
                    FB_Kinect.SetMousePosition(nx + 1, ny + 1, true);
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
