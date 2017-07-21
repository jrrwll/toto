using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Toto.Net
{
    public class UdpCapsulation
    {
        private UdpClient client;
        private IPEndPoint remote;
        private byte[] buffer;


        public IPEndPoint Remote
        {
            get { return remote; }
            set { remote = value; }
        }

        public UdpCapsulation(int local_port)
        {
            client = new UdpClient(local_port);
            remote = new IPEndPoint( IPAddress.Parse( Dns.GetHostName() ), 0 );
        }
        public UdpCapsulation(int local_port, string hostname, int port)
        {
            client = new UdpClient( local_port );
            remote = new IPEndPoint( IPAddress.Parse( hostname ), port );
        }

        public void Send(string info)
        {
            buffer = Encoding.Unicode.GetBytes(info);
            client.Send( buffer, buffer.Length, remote);
            client.Close();
        }

        public void Receive(string info)
        {
            buffer = client.Receive(ref remote);
            client.Close();
        }

    }
}
