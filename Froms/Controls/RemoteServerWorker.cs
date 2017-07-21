using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Net.Sockets;
using System.Net;

namespace Toto.WinForms
{
    public partial class RemoteServerWorker : UserControl
    {
        private const int TCP_PORT = 9102;
        private const int UDP_PORT = 9104;
        private const string FGF = "|";

        private TcpClient tcpClient;
        private TcpListener tcpListener;
        private UdpClient listener = new UdpClient(UDP_PORT);
        private string macAddress;
        private string ipAddress;

        public RemoteServerWorker()
        {
            InitializeComponent();
            tcpListener = new TcpListener(Dns.GetHostAddresses(Dns.GetHostName())[0], TCP_PORT);
            tcpListener.Start();

            bgworker.RunWorkerAsync();
        }

        private void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            StartListener();
        }



        private void StartListener()
        {
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                while (true)
                {
                    //接收服务器UDP广播包
                    byte[] buf = listener.Receive(ref groupEP);
                    string message = Encoding.Unicode.GetString(buf, 0, buf.Length);
                    bgworker.ReportProgress(buf.Length, message);

                    try
                    {
                        //请求服务器连接
                        string serverIp = message.Substring(0, message.IndexOf(FGF));
                        int tcpPort = Convert.ToInt32(message.Substring(message.IndexOf(FGF) + 1, message.Length - message.IndexOf(FGF) - 1));
                        tcpClient = new TcpClient(serverIp, tcpPort);

                        NetworkStream ns = tcpClient.GetStream();
                        byte[] data = Encoding.Unicode.GetBytes(Dns.GetHostName() + FGF + ipAddress + FGF + macAddress);
                        ns.Write(data, 0, data.Length);
                        ns.Close();
                        tcpClient.Close();
                        bgworker.ReportProgress(0, Encoding.Unicode.GetString(data));
                    } catch (Exception ex)
                    {
                        bgworker.ReportProgress(0, "Error:" + ex.Message);
                    }
                }

            } catch (Exception e)
            {
                bgworker.ReportProgress(0, "Error:" + e.Message);
            }

        }

        private void bgworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Tag = e.UserState.ToString() + "     Time:" + DateTime.Now.ToShortTimeString();
        }

        //接收远程控制包
        private void bgworkertcp_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    Byte[] buf = new Byte[1024];
                    TcpClient client = tcpListener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();

                    do
                    {
                        stream.Read(buf, 0, buf.Length);
                    }
                    while (stream.DataAvailable);

                    ExecCommand(Encoding.Unicode.GetString(buf, 0, buf.Length).Replace("\0", "").Trim());
                    stream.Close();
                    client.Close();
                }
            } catch (Exception ex)
            {
                bgworkertcp.ReportProgress(0, "Error:" + ex.Message);
            }
        }

        private void bgworkertcp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Tag = e.UserState.ToString() + "     Time:" + DateTime.Now.ToShortTimeString();
        }

        private void ExecCommand(string command)
        {
            if (command == "Reboot")
            {
                Reboot();
                bgworkertcp.ReportProgress(0, command);
            } else if (command == "PowerOff")
            {
                PowerOff();
                bgworkertcp.ReportProgress(0, command);
            } else if (command == "LogoOff")
            {
                LogoOff();
                bgworkertcp.ReportProgress(0, command);
            } else
            {
                MessageBox.Show(command);
            }
        }

        private void StopServer()
        {
            bgworker.CancelAsync();
            bgworkertcp.CancelAsync();
            listener.Close();
            tcpListener.Stop();
        }
    }
}
