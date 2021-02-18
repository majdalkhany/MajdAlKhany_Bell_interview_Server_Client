using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

/**
 * This class contains all the networking stuff that the server will need
 * to use in order to communicate with the client
 */

namespace Bell_Server
{
    class Networking
    {
        private IPAddress ip;
        private int port;
        private TcpListener tcpListener;
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        public Networking(string ip, int port)
        {
            this.ip = Dns.GetHostEntry(ip).AddressList[0];
            this.port = port;
            this.tcpListener = new TcpListener(this.ip, 8080);
            this.tcpClient = default(TcpClient);
        }

        //methods
        
        public void acceptClients()
        {
           this.tcpClient = this.tcpListener.AcceptTcpClient();
        }
        public void getStream()
        {
            this.networkStream = this.tcpClient.GetStream();
        }

        public void read(byte[] response)
        {
            this.networkStream.Read(response, 0, response.Length);
        }
        public void write(byte[] response)
        {
            this.networkStream.Write(response, 0, response.Length);
        }

        //Getters/setters
        public TcpListener Server
        {
            get
            {
                return tcpListener;
            }
        }

        public TcpClient Client
        {
            get
            {
                return tcpClient;
            }
        }

        public NetworkStream Stream
        {
            get
            {
                return networkStream;
            }
        }


    }
}
