using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArkRemoteAdmin.SourceRcon
{
    class Rcon
    {
        private Socket socket;

        public bool Connected
        {
            get { return socket != null && socket.Connected; }
        }

        public bool Connect(string host, int port)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException("host", "The host can not be empty");
            if (port < 1 || port > 655359)
                throw new ArgumentOutOfRangeException("port", "The provided port is not valid");

            if (socket == null)
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            socket.ReceiveTimeout = 5000;
            socket.ReceiveBufferSize = 4096;
            socket.SendTimeout = 5000;
            socket.SendBufferSize = 4096;
            IAsyncResult result = socket.BeginConnect(host, port, null, null);
            if (!result.AsyncWaitHandle.WaitOne(5000))
            {
                socket.Close();
                socket = null;
            }

            return Connected;
        }

        public void Disconnect()
        {
            if (Connected)
            {
                socket.Close();
                socket = null;
            }
        }

        public bool Authenticate(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password", "The password can not be empty");

            if (!Connected)
                throw new Exception("You must be connected before authenticating");

            RconPacket response = SendReceive(new RconPacket(PacketType.ServerdataAuth, password));

            return response.Id != -1;
        }

        public RconPacket SendReceive(RconPacket packet)
        {
            if (packet == null)
                throw new ArgumentNullException("packet");

            if (!Connected)
                throw new Exception("You must be connected before sending data");

            // Send
            socket.Send(packet);
            // Receive
            byte[] buffer = new byte[socket.ReceiveBufferSize], data;
            using (MemoryStream ms = new MemoryStream())
            {
                do
                {
                    int count = socket.Receive(buffer);
                    ms.Write(buffer, 0, count);

                    if (ms.Length == 4356)
                        Thread.Sleep(100);
                } while (socket.Available > 0);

                data = ms.ToArray();

                // Check package

            }

            return (RconPacket)data;
        }
    }
}
