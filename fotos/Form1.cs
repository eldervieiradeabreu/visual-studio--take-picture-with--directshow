using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace fotos
{
    public partial class Form1 : Form
    {
        bool busy = false;
        int _qtd;
        int qtfFotoBatida = 1;
        string _path;
        string _nomeOriginal;
        AForge.Video.DirectShow.VideoCaptureDevice videoSource;
        public Form1(int tempo, int qtd, string path, string nome)
        {
            InitializeComponent();
            AForge.Video.DirectShow.FilterInfoCollection videosources = new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);

            if (videosources != null)
            {
                videoSource = new AForge.Video.DirectShow.VideoCaptureDevice(videosources[0].MonikerString);
                videoSource.NewFrame += (s, e) =>
                {
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }

                    pictureBox1.Image = (Bitmap)e.Frame.Clone();
                };
                videoSource.Start();
            }
            timer1.Interval = tempo;
            _qtd = qtd;
            _path = path;
            _nomeOriginal = nome;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.OnClosed(e);

            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource = null;
            }
        }

        public bool teste() {
            busy = true;
            if (pictureBox1?.Image != null) {
                try
                {
                    var nome = "";
                    if (string.IsNullOrEmpty(_nomeOriginal))
                        nome = DateTime.Now.ToString("yyyyMMdd-hhmmss");
                    else {
                        nome = _nomeOriginal;
                        if (qtfFotoBatida > 1) {
                            nome += qtfFotoBatida;
                        }
                    }
                    


                    pictureBox1.Image.Save(_path + nome + ".png", System.Drawing.Imaging.ImageFormat.Png);
                    qtfFotoBatida++;
                    _qtd--;
                }
                catch (Exception ex) {
                    teste();
                }                
            }

            if (_qtd == 0) { 
                Application.Exit();
            }
            Thread.Sleep(2000);
            busy = false;
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!busy)
            {
                teste();
            }            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
