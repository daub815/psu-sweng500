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
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Live;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;

namespace CameraTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            foreach (var device in EncoderDevices.FindDevices(EncoderDeviceType.Video))
            {
                this.VideoListBox.Items.Add(device.Name);
            }

            foreach (var device in EncoderDevices.FindDevices(EncoderDeviceType.Audio))
            {
                this.AudioListBox.Items.Add(device.Name);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (null != mJob)
            {
                mJob.Dispose();
            }
        }

        LiveJob mJob = null;
        LiveDeviceSource videoSource = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EncoderDevice selectedVideo = null;
            EncoderDevice selectedAudio = null;
            GetDevices(out selectedVideo, out selectedAudio);

            if (null != selectedVideo &&
                null != selectedAudio)
            {
                mJob = new LiveJob();
                videoSource = mJob.AddDeviceSource(selectedVideo, selectedAudio);

                if (videoSource.IsDialogSupported(ConfigurationDialog.VideoCapturePinDialog))
                {
                    videoSource.ShowConfigurationDialog(ConfigurationDialog.VideoCapturePinDialog, (new HandleRef(this.PreviewPanel, this.PreviewPanel.Handle)));
                }

                if (videoSource.IsDialogSupported(ConfigurationDialog.VideoCaptureDialog))
                {
                    videoSource.ShowConfigurationDialog(ConfigurationDialog.VideoCaptureDialog, (new HandleRef(this.PreviewPanel, this.PreviewPanel.Handle)));
                }

                if (videoSource.IsDialogSupported(ConfigurationDialog.VideoCrossbarDialog))
                {
                    videoSource.ShowConfigurationDialog(ConfigurationDialog.VideoCrossbarDialog, (new HandleRef(this.PreviewPanel, this.PreviewPanel.Handle)));
                }

                if (videoSource.IsDialogSupported(ConfigurationDialog.VideoPreviewPinDialog))
                {
                    videoSource.ShowConfigurationDialog(ConfigurationDialog.VideoPreviewPinDialog, (new HandleRef(this.PreviewPanel, this.PreviewPanel.Handle)));
                }

                if (videoSource.IsDialogSupported(ConfigurationDialog.VideoSecondCrossbarDialog))
                {
                    videoSource.ShowConfigurationDialog(ConfigurationDialog.VideoSecondCrossbarDialog, (new HandleRef(this.PreviewPanel, this.PreviewPanel.Handle)));
                }

                // Get the properties of the device video
                SourceProperties sp = videoSource.SourcePropertiesSnapshot();

                // Resize the preview panel to match the video device resolution set
                this.PreviewPanel.Size = new System.Drawing.Size(sp.Size.Width, sp.Size.Height);

                // Setup the output video resolution file as the preview
                this.mJob.OutputFormat.VideoProfile.Size = new System.Drawing.Size(sp.Size.Width, sp.Size.Height);

                videoSource.PreviewWindow = new PreviewWindow(new HandleRef(this.PreviewPanel, this.PreviewPanel.Handle));

                this.mJob.ActivateSource(videoSource);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (Bitmap bitmap = new Bitmap(this.PreviewPanel.Width, this.PreviewPanel.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    var rectanglePanelVideoPreview = this.PreviewPanel.Bounds;
                    var sourcePoints = this.PreviewPanel.PointToScreen(new System.Drawing.Point(this.PreviewPanel.ClientRectangle.X, this.PreviewPanel.ClientRectangle.Y));
                    g.CopyFromScreen(sourcePoints, System.Drawing.Point.Empty, rectanglePanelVideoPreview.Size);
                }
                
                string strGrabFileName = string.Format("C:\\Snapshot_{0:yyyyMMdd_hhmmss}.jpg", DateTime.Now);
                bitmap.Save(string.Format("Snapshot_{0:yyyyMMdd_hhmmss}.jpg", DateTime.Now), System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        protected void GetDevices(out EncoderDevice videoSource, out EncoderDevice audioSource)
        {
            var videoName = this.VideoListBox.SelectedItem as string;
            videoSource = EncoderDevices.FindDevices(EncoderDeviceType.Video).FirstOrDefault(v => string.Equals(videoName, v.Name));
            
            var audioName = this.AudioListBox.SelectedItem as string;
            audioSource = EncoderDevices.FindDevices(EncoderDeviceType.Audio).FirstOrDefault(a => string.Equals(audioName, a.Name)); 
        }
    }
}
