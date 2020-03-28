using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Win7Cracks
{
    public partial class frmDownload : Form
    {
        Uri uri;
        string path;
        Stopwatch sw = new Stopwatch();
        WebClient webClient;
        public frmDownload(Uri uri, string path)
        {
            this.uri = uri;
            this.path = path;
            InitializeComponent();
            label1.Text = uri.AbsoluteUri.Length > 100 ? uri.AbsoluteUri.Substring(0, 100) + "..." : uri.AbsoluteUri;
            label2.Text = path.Length > 100 ? path.Substring(0, 100) + "..." : path;
        }

        private void frmDownload_Load(object sender, EventArgs e)
        {
            webClient = new WebClient();
            sw.Start();
            webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            webClient.DownloadFileAsync(uri, path);
        }

        private void WebClient_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label3.Text = "Download speed: " + string.Format("{0:0.##}", e.BytesReceived / 8 / 1024 / sw.Elapsed.TotalSeconds) + "KB/s";
            label4.Text = "Downloaded: " + string.Format("{0:0.##}", e.BytesReceived / 1024 / 1024) + "MB";
            label5.Text = "File size: " + string.Format("{0:0.##}", e.TotalBytesToReceive / 1024 / 1024) + "MB";
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            sw.Stop();
            if(e.Cancelled)
            {
                try
                {
                    File.Delete(path);
                }
                catch
                {

                }
            }
            DialogResult = e.Cancelled ? DialogResult.Cancel : DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webClient.CancelAsync();
        }
    }
}
