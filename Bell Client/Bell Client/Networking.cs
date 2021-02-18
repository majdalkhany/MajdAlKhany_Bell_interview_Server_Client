using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;


/**
 * This class contains all the networking stuff that the client will need
 * to use in order to communicate with the server
 */

namespace Bell_Client
{
    class Networking
    {
        private string ip;
        private int port;
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        public Networking(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            this.tcpClient = new TcpClient(ip, port);
            this.networkStream = tcpClient.GetStream();
        }

        //Methods
        public void write(byte[] response)
        {
            this.networkStream.Write(response, 0, response.Length);
        }


        //Getters/setters
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
