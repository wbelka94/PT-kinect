using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FB_Kinect_Painter.application.data.classes {
    class Spray : Painting_Tool {
        public Spray(InkCanvas INK, InkCanvasEditingMode editingMode, string name, string cursor, int defaultSize) : base(INK, editingMode, name, cursor, defaultSize) {
        }
        public new void Paint(double x, double y) {
            Random rnd = new Random();
            double nx, ny;
            for (int i = 0; i < 10; i++) {
                nx = x + rnd.Next(0, 10);
                ny = y + rnd.Next(0, 10);
                FB_Kinect.SetMousePosition(nx, ny, true);
                FB_Kinect.SetMousePosition(nx, ny, false);
            }
        }
    }
}