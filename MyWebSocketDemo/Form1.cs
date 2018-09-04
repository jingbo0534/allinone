using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWebSocketDemo
{
    public partial class MyServerCollection : Form
    {
        public MyServerCollection()
        {
            InitializeComponent();
            this.button1.Text = Resource1.Server1Start;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var server = new Server1();
            if (this.button1.Text == Resource1.Server1Start)
            {
                server.startWsFleck();
                this.button1.Text = Resource1.Server1Close;
            }
            else
            {
                server.close();
                this.button1.Text = Resource1.Server1Start;
            }
        }
    }
}
