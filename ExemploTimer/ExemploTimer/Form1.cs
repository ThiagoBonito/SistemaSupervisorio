using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ExemploTimer
{
    public partial class Form1 : Form
    {
        Socket socketenviar = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
        IPEndPoint endereco = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9060);
        Random gerador = new Random();
        String temp;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            temp = gerador.Next(100).ToString();
            socketenviar.SendTo(Encoding.ASCII.GetBytes(temp), endereco);
            MessageBox.Show("Enviou: " + temp);
        }
    }
}
