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
using System.Threading;

namespace SistemaSupervisorio
{
    public partial class Form1 : Form
    {
        Socket socketreceber = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
        EndPoint endereco = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9060);
        byte[] data = new byte[1024];
        int qtdbytes;
        Thread mythread;
        String dia, mes, ano, hora, numero;
        Dado umdado = new Dado();

        int contador = 0;
        public Form1()
        {
            InitializeComponent();
            socketreceber.Bind(endereco);     
        }
        public void meuProcesso()
        {
            while (true)
            {
                dia = DateTime.Now.Day.ToString();
                mes = DateTime.Now.Month.ToString();
                ano = DateTime.Now.Year.ToString();
                hora = DateTime.Now.TimeOfDay.ToString();
                qtdbytes = socketreceber.ReceiveFrom(data, ref endereco);
                numero = Encoding.ASCII.GetString(data, 0, qtdbytes);
                if(int.Parse(numero) > int.Parse(textBox1.Text))
                {
                    MessageBox.Show("O valor recebido ultrapassa o permitido pelo SetPoint");
                }
                listBox1.Invoke((Action)delegate ()
                {
                    listBox1.Items.Add($" {dia}/{mes}/{ano} às {hora} => {numero}");
                });
                chart1.Invoke((Action)delegate ()
                {
                    chart1.Series[0].Points.AddXY(contador,numero);
                    contador++;
                });
                umdado.dia = dia;
                umdado.mes = mes;
                umdado.ano = ano;
                umdado.dado = numero;
                umdado.hora = hora;
                DAL.Inserir(umdado);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Iniciar")
            {
                button1.Text = "Desativar";
                mythread = new Thread(new ThreadStart(this.meuProcesso));
                mythread.Start();
            }
            else
            {
                button1.Text = "Iniciar";
                mythread.Abort();
            }
        }
    }
}
