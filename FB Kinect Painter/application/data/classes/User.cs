using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB_Kinect_Painter.application.data.classes {   

    public static class User {
        public static JointType firstHand = JointType.HandRight;
        public static JointType secondHand = JointType.HandLeft;
        public static JointType secondElbow = JointType.ElbowLeft;

        public static void SetJoints(String firstHeand) {
            if (firstHeand.Equals("Prawa")) {
                firstHand = JointType.HandRight;
                secondHand = JointType.HandLeft;
                secondElbow = JointType.ElbowLeft;
            }
            else if (firstHeand.Equals("Lewa")) {
                firstHand = JointType.HandLeft;
                secondHand = JointType.HandRight;
                secondElbow = JointType.ElbowRight;
            }
        }
    }
}