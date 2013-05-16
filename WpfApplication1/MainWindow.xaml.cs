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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            Unloaded += new RoutedEventHandler(MainWindow_Unloaded);
        }
        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }
        void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            DiscoverKinectSensor();
        }
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            DiscoverKinectSensor();
        }
        private void DiscoverKinectSensor()
        {
            string info = "偵測到" + KinectSensor.KinectSensors.Count + "台感應器";
            TextBlock tb = new TextBlock() { Text = info , Foreground = Brushes.Red};
            status.Items.Add(tb);

            foreach (var s in KinectSensor.KinectSensors)
            {
                string i = "偵測到感應器,連線ID: " + s.DeviceConnectionId + " , 狀態" + s.Status; 
                TextBlock t = new TextBlock() { Text = i };
                status.Items.Add(t);
                if (s.Status == KinectStatus.Connected)
                {
                    String link = "使用" + s.DeviceConnectionId + "接收資訊";
                    TextBlock ltb = new TextBlock() { Text = link, Foreground = Brushes.Blue };
                    status.Items.Add(ltb);
                    Accelerometer sd = new Accelerometer(s);
                    sd.Show();
                }
            }
        }
    }
}
