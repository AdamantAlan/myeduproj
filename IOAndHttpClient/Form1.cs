using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOAndHttpClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            using (HttpClient client = new HttpClient())
            {
               var req =  client.GetAsync("");
                var rez = req.Result;
            }

                int j = 0;
            while (j < 100)
            {
                progressBar1.Value++;
                j++;
                Thread.Sleep(100);
            }

        }
    }
}
