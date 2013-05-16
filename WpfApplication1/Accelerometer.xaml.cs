using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.Timers;

namespace WpfApplication1
{
    public partial class Accelerometer : Window
    {
        Timer kinectAccTimer; 
        KinectSensor kinect;
        public Accelerometer(KinectSensor sensor) : this()
        {
            kinect = sensor;
            kinect.Start();
            kinectAccTimer = new Timer();
            kinectAccTimer.Elapsed += kinectAccTimer_Elapsed;
            kinectAccTimer.Interval = 30;
            kinectAccTimer.Start();
        }

        Vector4 accdata;
        void kinectAccTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            accdata = kinect.AccelerometerGetCurrentReading();
            float x = accdata.X;
            float y = accdata.Y;
            double r = Math.Sqrt(x * x + y * y);
            double rad = Math.Asin(x/r);
            double deg = RadianToDegree(rad);
            //Console.WriteLine("x=" + x + " , x=" + y + " , r=" + r + " , deg=" + deg);
            Dispatcher.BeginInvoke(
                (Action) delegate()
                {
                    kinectangle.Angle = deg;
                }
            );
        }
        private double RadianToDegree(double rad)
        {
            return rad * (180.0 / Math.PI);
        }
        public Accelerometer()
        {
            InitializeComponent();
        }


    }
}
